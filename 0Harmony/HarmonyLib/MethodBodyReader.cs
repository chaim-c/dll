using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MonoMod.Utils;
using MonoMod.Utils.Cil;

namespace HarmonyLib
{
	// Token: 0x02000015 RID: 21
	internal class MethodBodyReader
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004A78 File Offset: 0x00002C78
		internal static List<ILInstruction> GetInstructions(ILGenerator generator, MethodBase method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			MethodBodyReader methodBodyReader = new MethodBodyReader(method, generator);
			methodBodyReader.DeclareVariables(null);
			methodBodyReader.GenerateInstructions();
			return methodBodyReader.ilInstructions;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004AB0 File Offset: 0x00002CB0
		internal MethodBodyReader(MethodBase method, ILGenerator generator)
		{
			this.generator = generator;
			this.method = method;
			this.module = method.Module;
			MethodBody methodBody = method.GetMethodBody();
			int? num;
			if (methodBody == null)
			{
				num = null;
			}
			else
			{
				byte[] ilasByteArray = methodBody.GetILAsByteArray();
				num = ((ilasByteArray != null) ? new int?(ilasByteArray.Length) : null);
			}
			int? num2 = num;
			if (num2.GetValueOrDefault() == 0)
			{
				this.ilBytes = new ByteBuffer(Array.Empty<byte>());
				this.ilInstructions = new List<ILInstruction>();
			}
			else
			{
				byte[] ilasByteArray2 = methodBody.GetILAsByteArray();
				if (ilasByteArray2 == null)
				{
					throw new ArgumentException("Can not get IL bytes of method " + method.FullDescription());
				}
				this.ilBytes = new ByteBuffer(ilasByteArray2);
				this.ilInstructions = new List<ILInstruction>((ilasByteArray2.Length + 1) / 2);
			}
			Type declaringType = method.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				try
				{
					this.typeArguments = declaringType.GetGenericArguments();
				}
				catch
				{
					this.typeArguments = null;
				}
			}
			if (method.IsGenericMethod)
			{
				try
				{
					this.methodArguments = method.GetGenericArguments();
				}
				catch
				{
					this.methodArguments = null;
				}
			}
			if (!method.IsStatic)
			{
				this.this_parameter = new MethodBodyReader.ThisParameter(method);
			}
			this.parameters = method.GetParameters();
			List<LocalVariableInfo> list;
			if (methodBody == null)
			{
				list = null;
			}
			else
			{
				IList<LocalVariableInfo> list2 = methodBody.LocalVariables;
				list = ((list2 != null) ? list2.ToList<LocalVariableInfo>() : null);
			}
			this.localVariables = (list ?? new List<LocalVariableInfo>());
			this.exceptions = (((methodBody != null) ? methodBody.ExceptionHandlingClauses : null) ?? new List<ExceptionHandlingClause>());
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00004C3C File Offset: 0x00002E3C
		internal void SetDebugging(bool debug)
		{
			this.debug = debug;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004C48 File Offset: 0x00002E48
		internal void GenerateInstructions()
		{
			while (this.ilBytes.position < this.ilBytes.buffer.Length)
			{
				int position = this.ilBytes.position;
				ILInstruction ilinstruction = new ILInstruction(this.ReadOpCode(), null)
				{
					offset = position
				};
				this.ReadOperand(ilinstruction);
				this.ilInstructions.Add(ilinstruction);
			}
			this.HandleNativeMethod();
			this.ResolveBranches();
			this.ParseExceptions();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004CB8 File Offset: 0x00002EB8
		internal void HandleNativeMethod()
		{
			MethodInfo methodInfo = this.method as MethodInfo;
			if (methodInfo == null)
			{
				return;
			}
			DllImportAttribute dllImportAttribute = methodInfo.GetCustomAttributes(false).OfType<DllImportAttribute>().FirstOrDefault<DllImportAttribute>();
			if (dllImportAttribute == null)
			{
				return;
			}
			Type declaringType = methodInfo.DeclaringType;
			string assemblyName = (((declaringType != null) ? declaringType.FullName : null) ?? "").Replace(".", "_") + "_" + methodInfo.Name;
			AssemblyName assemblyName2 = new AssemblyName(assemblyName);
			AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName2, AssemblyBuilderAccess.Run);
			ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName2.Name);
			TypeBuilder typeBuilder = moduleBuilder.DefineType("NativeMethodHolder", TypeAttributes.Public | TypeAttributes.UnicodeClass);
			MethodBuilder methodBuilder = typeBuilder.DefinePInvokeMethod(methodInfo.Name, dllImportAttribute.Value, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.PinvokeImpl, CallingConventions.Standard, methodInfo.ReturnType, (from x in methodInfo.GetParameters()
			select x.ParameterType).ToArray<Type>(), dllImportAttribute.CallingConvention, dllImportAttribute.CharSet);
			methodBuilder.SetImplementationFlags(methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.PreserveSig);
			Type type = typeBuilder.CreateType();
			MethodInfo operand = type.GetMethod(methodInfo.Name);
			int num = this.method.GetParameters().Length;
			for (int i = 0; i < num; i++)
			{
				this.ilInstructions.Add(new ILInstruction(OpCodes.Ldarg, i)
				{
					offset = 0
				});
			}
			this.ilInstructions.Add(new ILInstruction(OpCodes.Call, operand)
			{
				offset = num
			});
			this.ilInstructions.Add(new ILInstruction(OpCodes.Ret, null)
			{
				offset = num + 5
			});
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004E62 File Offset: 0x00003062
		internal void DeclareVariables(LocalBuilder[] existingVariables)
		{
			if (this.generator == null)
			{
				return;
			}
			if (existingVariables != null)
			{
				this.variables = existingVariables;
				return;
			}
			this.variables = (from lvi in this.localVariables
			select this.generator.DeclareLocal(lvi.LocalType, lvi.IsPinned)).ToArray<LocalBuilder>();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004E9C File Offset: 0x0000309C
		private void ResolveBranches()
		{
			foreach (ILInstruction ilinstruction in this.ilInstructions)
			{
				OperandType operandType = ilinstruction.opcode.OperandType;
				if (operandType != OperandType.InlineBrTarget)
				{
					if (operandType == OperandType.InlineSwitch)
					{
						int[] array = (int[])ilinstruction.operand;
						ILInstruction[] array2 = new ILInstruction[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array2[i] = this.GetInstruction(array[i], false);
						}
						ilinstruction.operand = array2;
						continue;
					}
					if (operandType != OperandType.ShortInlineBrTarget)
					{
						continue;
					}
				}
				ilinstruction.operand = this.GetInstruction((int)ilinstruction.operand, false);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004F60 File Offset: 0x00003160
		private void ParseExceptions()
		{
			foreach (ExceptionHandlingClause exceptionHandlingClause in this.exceptions)
			{
				int tryOffset = exceptionHandlingClause.TryOffset;
				int handlerOffset = exceptionHandlingClause.HandlerOffset;
				int offset = exceptionHandlingClause.HandlerOffset + exceptionHandlingClause.HandlerLength - 1;
				ILInstruction instruction = this.GetInstruction(tryOffset, false);
				instruction.blocks.Add(new ExceptionBlock(ExceptionBlockType.BeginExceptionBlock, null));
				ILInstruction instruction2 = this.GetInstruction(offset, true);
				instruction2.blocks.Add(new ExceptionBlock(ExceptionBlockType.EndExceptionBlock, null));
				switch (exceptionHandlingClause.Flags)
				{
				case ExceptionHandlingClauseOptions.Clause:
				{
					ILInstruction instruction3 = this.GetInstruction(handlerOffset, false);
					instruction3.blocks.Add(new ExceptionBlock(ExceptionBlockType.BeginCatchBlock, exceptionHandlingClause.CatchType));
					break;
				}
				case ExceptionHandlingClauseOptions.Filter:
				{
					ILInstruction instruction4 = this.GetInstruction(exceptionHandlingClause.FilterOffset, false);
					instruction4.blocks.Add(new ExceptionBlock(ExceptionBlockType.BeginExceptFilterBlock, null));
					break;
				}
				case ExceptionHandlingClauseOptions.Finally:
				{
					ILInstruction instruction5 = this.GetInstruction(handlerOffset, false);
					instruction5.blocks.Add(new ExceptionBlock(ExceptionBlockType.BeginFinallyBlock, null));
					break;
				}
				case ExceptionHandlingClauseOptions.Fault:
				{
					ILInstruction instruction6 = this.GetInstruction(handlerOffset, false);
					instruction6.blocks.Add(new ExceptionBlock(ExceptionBlockType.BeginFaultBlock, null));
					break;
				}
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000050C0 File Offset: 0x000032C0
		private bool EndsInDeadCode(List<CodeInstruction> list)
		{
			int count = list.Count;
			if (count < 2 || list.Last<CodeInstruction>().opcode != OpCodes.Throw)
			{
				return false;
			}
			return list.GetRange(0, count - 1).All((CodeInstruction code) => code.opcode != OpCodes.Ret);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00005120 File Offset: 0x00003320
		internal List<CodeInstruction> FinalizeILCodes(Emitter emitter, List<MethodInfo> transpilers, List<Label> endLabels, out bool hasReturnCode, out bool methodEndsInDeadCode)
		{
			hasReturnCode = false;
			methodEndsInDeadCode = false;
			if (this.generator == null)
			{
				return null;
			}
			Label label;
			foreach (ILInstruction ilinstruction in this.ilInstructions)
			{
				OperandType operandType = ilinstruction.opcode.OperandType;
				if (operandType != OperandType.InlineBrTarget)
				{
					if (operandType != OperandType.InlineSwitch)
					{
						if (operandType != OperandType.ShortInlineBrTarget)
						{
							continue;
						}
					}
					else
					{
						ILInstruction[] array = ilinstruction.operand as ILInstruction[];
						if (array != null)
						{
							List<Label> list = new List<Label>();
							foreach (ILInstruction ilinstruction2 in array)
							{
								Label item = this.generator.DefineLabel();
								ilinstruction2.labels.Add(item);
								list.Add(item);
							}
							ilinstruction.argument = list.ToArray();
							continue;
						}
						continue;
					}
				}
				ILInstruction ilinstruction3 = ilinstruction.operand as ILInstruction;
				if (ilinstruction3 != null)
				{
					label = this.generator.DefineLabel();
					ilinstruction3.labels.Add(label);
					ilinstruction.argument = label;
				}
			}
			CodeTranspiler codeTranspiler = new CodeTranspiler(this.ilInstructions);
			transpilers.Do(new Action<MethodInfo>(codeTranspiler.Add));
			List<CodeInstruction> result = codeTranspiler.GetResult(this.generator, this.method);
			if (emitter == null)
			{
				return result;
			}
			emitter.LogComment("start original");
			if (this.debug)
			{
				List<string> buffer = FileLog.GetBuffer(true);
				emitter.LogAllLocalVariables();
				FileLog.LogBuffered(buffer);
			}
			hasReturnCode = result.Any((CodeInstruction code) => code.opcode == OpCodes.Ret);
			methodEndsInDeadCode = this.EndsInDeadCode(result);
			for (;;)
			{
				CodeInstruction codeInstruction2 = result.LastOrDefault<CodeInstruction>();
				if (codeInstruction2 == null || codeInstruction2.opcode != OpCodes.Ret)
				{
					break;
				}
				endLabels.AddRange(codeInstruction2.labels);
				result.RemoveAt(result.Count - 1);
			}
			Action<Label> <>9__2;
			Action<ExceptionBlock> <>9__3;
			Action<ExceptionBlock> <>9__4;
			result.Do(delegate(CodeInstruction codeInstruction)
			{
				IEnumerable<Label> labels = codeInstruction.labels;
				Action<Label> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(Label label)
					{
						emitter.MarkLabel(label);
					});
				}
				labels.Do(action);
				IEnumerable<ExceptionBlock> blocks = codeInstruction.blocks;
				Action<ExceptionBlock> action2;
				if ((action2 = <>9__3) == null)
				{
					action2 = (<>9__3 = delegate(ExceptionBlock block)
					{
						Label? label4;
						emitter.MarkBlockBefore(block, out label4);
					});
				}
				blocks.Do(action2);
				OpCode opCode = codeInstruction.opcode;
				object obj = codeInstruction.operand;
				if (opCode == OpCodes.Ret)
				{
					Label label2 = this.generator.DefineLabel();
					opCode = OpCodes.Br;
					obj = label2;
					endLabels.Add(label2);
				}
				OpCode opCode2;
				if (MethodBodyReader.shortJumps.TryGetValue(opCode, out opCode2))
				{
					opCode = opCode2;
				}
				OperandType operandType2 = opCode.OperandType;
				if (operandType2 != OperandType.InlineNone)
				{
					if (operandType2 != OperandType.InlineSig)
					{
						if (obj == null)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
							defaultInterpolatedStringHandler.AppendLiteral("Wrong null argument: ");
							defaultInterpolatedStringHandler.AppendFormatted<CodeInstruction>(codeInstruction);
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						emitter.AddInstruction(opCode, obj);
						emitter.LogIL(opCode, obj, null);
						this.generator.DynEmit(opCode, obj);
					}
					else
					{
						CecilILGenerator proxiedShim = this.generator.GetProxiedShim<CecilILGenerator>();
						if (proxiedShim == null)
						{
							throw new NotSupportedException();
						}
						if (obj == null)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(21, 1);
							defaultInterpolatedStringHandler.AppendLiteral("Wrong null argument: ");
							defaultInterpolatedStringHandler.AppendFormatted<CodeInstruction>(codeInstruction);
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						if (!(obj is ICallSiteGenerator))
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 2);
							defaultInterpolatedStringHandler.AppendLiteral("Wrong Emit argument type ");
							defaultInterpolatedStringHandler.AppendFormatted<Type>(obj.GetType());
							defaultInterpolatedStringHandler.AppendLiteral(" in ");
							defaultInterpolatedStringHandler.AppendFormatted<CodeInstruction>(codeInstruction);
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
						emitter.AddInstruction(opCode, obj);
						emitter.LogIL(opCode, obj, null);
						proxiedShim.Emit(opCode, (ICallSiteGenerator)obj);
					}
				}
				else
				{
					emitter.Emit(opCode);
				}
				IEnumerable<ExceptionBlock> blocks2 = codeInstruction.blocks;
				Action<ExceptionBlock> action3;
				if ((action3 = <>9__4) == null)
				{
					action3 = (<>9__4 = delegate(ExceptionBlock block)
					{
						emitter.MarkBlockAfter(block);
					});
				}
				blocks2.Do(action3);
			});
			emitter.LogComment("end original" + (methodEndsInDeadCode ? " (has dead code end)" : ""));
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00005388 File Offset: 0x00003588
		private static void GetMemberInfoValue(MemberInfo info, out object result)
		{
			result = null;
			MemberTypes memberType = info.MemberType;
			if (memberType <= MemberTypes.Method)
			{
				switch (memberType)
				{
				case MemberTypes.Constructor:
					result = (ConstructorInfo)info;
					return;
				case MemberTypes.Event:
					result = (EventInfo)info;
					return;
				case MemberTypes.Constructor | MemberTypes.Event:
					break;
				case MemberTypes.Field:
					result = (FieldInfo)info;
					return;
				default:
					if (memberType != MemberTypes.Method)
					{
						return;
					}
					result = (MethodInfo)info;
					return;
				}
			}
			else if (memberType != MemberTypes.Property)
			{
				if (memberType != MemberTypes.TypeInfo && memberType != MemberTypes.NestedType)
				{
					return;
				}
				result = (Type)info;
				return;
			}
			else
			{
				result = (PropertyInfo)info;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00005408 File Offset: 0x00003608
		private void ReadOperand(ILInstruction instruction)
		{
			switch (instruction.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
			{
				int num = this.ilBytes.ReadInt32();
				instruction.operand = num + this.ilBytes.position;
				return;
			}
			case OperandType.InlineField:
			{
				int metadataToken = this.ilBytes.ReadInt32();
				instruction.operand = this.module.ResolveField(metadataToken, this.typeArguments, this.methodArguments);
				Type declaringType = ((MemberInfo)instruction.operand).DeclaringType;
				if (declaringType != null)
				{
					declaringType.FixReflectionCacheAuto();
				}
				instruction.argument = (FieldInfo)instruction.operand;
				return;
			}
			case OperandType.InlineI:
			{
				int num2 = this.ilBytes.ReadInt32();
				instruction.operand = num2;
				instruction.argument = (int)instruction.operand;
				return;
			}
			case OperandType.InlineI8:
			{
				long num3 = this.ilBytes.ReadInt64();
				instruction.operand = num3;
				instruction.argument = (long)instruction.operand;
				return;
			}
			case OperandType.InlineMethod:
			{
				int metadataToken2 = this.ilBytes.ReadInt32();
				instruction.operand = this.module.ResolveMethod(metadataToken2, this.typeArguments, this.methodArguments);
				Type declaringType2 = ((MemberInfo)instruction.operand).DeclaringType;
				if (declaringType2 != null)
				{
					declaringType2.FixReflectionCacheAuto();
				}
				if (instruction.operand is ConstructorInfo)
				{
					instruction.argument = (ConstructorInfo)instruction.operand;
					return;
				}
				instruction.argument = (MethodInfo)instruction.operand;
				return;
			}
			case OperandType.InlineNone:
				instruction.argument = null;
				return;
			case OperandType.InlineR:
			{
				double num4 = this.ilBytes.ReadDouble();
				instruction.operand = num4;
				instruction.argument = (double)instruction.operand;
				return;
			}
			case OperandType.InlineSig:
			{
				int metadataToken3 = this.ilBytes.ReadInt32();
				byte[] array = this.module.ResolveSignature(metadataToken3);
				InlineSignature inlineSignature = InlineSignatureParser.ImportCallSite(this.module, array);
				instruction.operand = inlineSignature;
				instruction.argument = inlineSignature;
				Debugger.Log(0, "TEST", "METHOD " + this.method.FullDescription() + "\n");
				Debugger.Log(0, "TEST", "Signature Blob = " + (from b in array
				select string.Format("0x{0:x02}", b)).Aggregate((string a, string b) => a + " " + b) + "\n");
				int level = 0;
				string category = "TEST";
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(13, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Signature = ");
				defaultInterpolatedStringHandler.AppendFormatted<InlineSignature>(inlineSignature);
				defaultInterpolatedStringHandler.AppendLiteral("\n");
				Debugger.Log(level, category, defaultInterpolatedStringHandler.ToStringAndClear());
				Debugger.Break();
				return;
			}
			case OperandType.InlineString:
			{
				int metadataToken4 = this.ilBytes.ReadInt32();
				instruction.operand = this.module.ResolveString(metadataToken4);
				instruction.argument = (string)instruction.operand;
				return;
			}
			case OperandType.InlineSwitch:
			{
				int num5 = this.ilBytes.ReadInt32();
				int num6 = this.ilBytes.position + 4 * num5;
				int[] array2 = new int[num5];
				for (int i = 0; i < num5; i++)
				{
					array2[i] = this.ilBytes.ReadInt32() + num6;
				}
				instruction.operand = array2;
				return;
			}
			case OperandType.InlineTok:
			{
				int metadataToken5 = this.ilBytes.ReadInt32();
				instruction.operand = this.module.ResolveMember(metadataToken5, this.typeArguments, this.methodArguments);
				Type declaringType3 = ((MemberInfo)instruction.operand).DeclaringType;
				if (declaringType3 != null)
				{
					declaringType3.FixReflectionCacheAuto();
				}
				MethodBodyReader.GetMemberInfoValue((MemberInfo)instruction.operand, out instruction.argument);
				return;
			}
			case OperandType.InlineType:
			{
				int metadataToken6 = this.ilBytes.ReadInt32();
				instruction.operand = this.module.ResolveType(metadataToken6, this.typeArguments, this.methodArguments);
				((Type)instruction.operand).FixReflectionCacheAuto();
				instruction.argument = (Type)instruction.operand;
				return;
			}
			case OperandType.InlineVar:
			{
				short num7 = this.ilBytes.ReadInt16();
				if (!MethodBodyReader.TargetsLocalVariable(instruction.opcode))
				{
					instruction.operand = this.GetParameter((int)num7);
					instruction.argument = num7;
					return;
				}
				LocalVariableInfo localVariable = this.GetLocalVariable((int)num7);
				if (localVariable == null)
				{
					instruction.argument = num7;
					return;
				}
				instruction.operand = localVariable;
				LocalBuilder[] array3 = this.variables;
				instruction.argument = (((array3 != null) ? array3[localVariable.LocalIndex] : null) ?? localVariable);
				return;
			}
			case OperandType.ShortInlineBrTarget:
			{
				sbyte b5 = (sbyte)this.ilBytes.ReadByte();
				instruction.operand = (int)b5 + this.ilBytes.position;
				return;
			}
			case OperandType.ShortInlineI:
			{
				if (instruction.opcode == OpCodes.Ldc_I4_S)
				{
					sbyte b2 = (sbyte)this.ilBytes.ReadByte();
					instruction.operand = b2;
					instruction.argument = (sbyte)instruction.operand;
					return;
				}
				byte b3 = this.ilBytes.ReadByte();
				instruction.operand = b3;
				instruction.argument = (byte)instruction.operand;
				return;
			}
			case OperandType.ShortInlineR:
			{
				float num8 = this.ilBytes.ReadSingle();
				instruction.operand = num8;
				instruction.argument = (float)instruction.operand;
				return;
			}
			case OperandType.ShortInlineVar:
			{
				byte b4 = this.ilBytes.ReadByte();
				if (!MethodBodyReader.TargetsLocalVariable(instruction.opcode))
				{
					instruction.operand = this.GetParameter((int)b4);
					instruction.argument = b4;
					return;
				}
				LocalVariableInfo localVariable2 = this.GetLocalVariable((int)b4);
				if (localVariable2 == null)
				{
					instruction.argument = b4;
					return;
				}
				instruction.operand = localVariable2;
				LocalBuilder[] array4 = this.variables;
				instruction.argument = (((array4 != null) ? array4[localVariable2.LocalIndex] : null) ?? localVariable2);
				return;
			}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005A08 File Offset: 0x00003C08
		private ILInstruction GetInstruction(int offset, bool isEndOfInstruction)
		{
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (offset < 0)
			{
				string paramName = "offset";
				object actualValue = offset;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Instruction offset ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
				defaultInterpolatedStringHandler.AppendLiteral(" is less than 0");
				throw new ArgumentOutOfRangeException(paramName, actualValue, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			int num = this.ilInstructions.Count - 1;
			ILInstruction ilinstruction = this.ilInstructions[num];
			if (offset > ilinstruction.offset + ilinstruction.GetSize() - 1)
			{
				string paramName2 = "offset";
				object actualValue2 = offset;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(47, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Instruction offset ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(offset);
				defaultInterpolatedStringHandler.AppendLiteral(" is outside valid range 0 - ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(ilinstruction.offset + ilinstruction.GetSize() - 1);
				throw new ArgumentOutOfRangeException(paramName2, actualValue2, defaultInterpolatedStringHandler.ToStringAndClear());
			}
			int i = 0;
			int num2 = num;
			while (i <= num2)
			{
				int num3 = i + (num2 - i) / 2;
				ilinstruction = this.ilInstructions[num3];
				if (isEndOfInstruction)
				{
					if (offset == ilinstruction.offset + ilinstruction.GetSize() - 1)
					{
						return ilinstruction;
					}
				}
				else if (offset == ilinstruction.offset)
				{
					return ilinstruction;
				}
				if (offset < ilinstruction.offset)
				{
					num2 = num3 - 1;
				}
				else
				{
					i = num3 + 1;
				}
			}
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
			defaultInterpolatedStringHandler.AppendLiteral("Cannot find instruction for ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(offset, "X4");
			throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00005B67 File Offset: 0x00003D67
		private static bool TargetsLocalVariable(OpCode opcode)
		{
			return opcode.Name.Contains("loc");
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005B7A File Offset: 0x00003D7A
		private LocalVariableInfo GetLocalVariable(int index)
		{
			List<LocalVariableInfo> list = this.localVariables;
			if (list == null)
			{
				return null;
			}
			return list[index];
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005B8E File Offset: 0x00003D8E
		private ParameterInfo GetParameter(int index)
		{
			if (index == 0)
			{
				return this.this_parameter;
			}
			return this.parameters[index - 1];
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005BA4 File Offset: 0x00003DA4
		private OpCode ReadOpCode()
		{
			byte b = this.ilBytes.ReadByte();
			if (b == 254)
			{
				return MethodBodyReader.two_bytes_opcodes[(int)this.ilBytes.ReadByte()];
			}
			return MethodBodyReader.one_byte_opcodes[(int)b];
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00005BE8 File Offset: 0x00003DE8
		[MethodImpl(MethodImplOptions.Synchronized)]
		static MethodBodyReader()
		{
			FieldInfo[] fields = typeof(OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				OpCode opCode = (OpCode)fieldInfo.GetValue(null);
				if (opCode.OpCodeType != OpCodeType.Nternal)
				{
					if (opCode.Size == 1)
					{
						MethodBodyReader.one_byte_opcodes[(int)opCode.Value] = opCode;
					}
					else
					{
						MethodBodyReader.two_bytes_opcodes[(int)(opCode.Value & 255)] = opCode;
					}
				}
			}
		}

		// Token: 0x04000023 RID: 35
		private readonly ILGenerator generator;

		// Token: 0x04000024 RID: 36
		private readonly MethodBase method;

		// Token: 0x04000025 RID: 37
		private bool debug;

		// Token: 0x04000026 RID: 38
		private readonly Module module;

		// Token: 0x04000027 RID: 39
		private readonly Type[] typeArguments;

		// Token: 0x04000028 RID: 40
		private readonly Type[] methodArguments;

		// Token: 0x04000029 RID: 41
		private readonly ByteBuffer ilBytes;

		// Token: 0x0400002A RID: 42
		private readonly ParameterInfo this_parameter;

		// Token: 0x0400002B RID: 43
		private readonly ParameterInfo[] parameters;

		// Token: 0x0400002C RID: 44
		private readonly IList<ExceptionHandlingClause> exceptions;

		// Token: 0x0400002D RID: 45
		private readonly List<ILInstruction> ilInstructions;

		// Token: 0x0400002E RID: 46
		private readonly List<LocalVariableInfo> localVariables;

		// Token: 0x0400002F RID: 47
		private LocalBuilder[] variables;

		// Token: 0x04000030 RID: 48
		private static readonly Dictionary<OpCode, OpCode> shortJumps = new Dictionary<OpCode, OpCode>
		{
			{
				OpCodes.Leave_S,
				OpCodes.Leave
			},
			{
				OpCodes.Brfalse_S,
				OpCodes.Brfalse
			},
			{
				OpCodes.Brtrue_S,
				OpCodes.Brtrue
			},
			{
				OpCodes.Beq_S,
				OpCodes.Beq
			},
			{
				OpCodes.Bge_S,
				OpCodes.Bge
			},
			{
				OpCodes.Bgt_S,
				OpCodes.Bgt
			},
			{
				OpCodes.Ble_S,
				OpCodes.Ble
			},
			{
				OpCodes.Blt_S,
				OpCodes.Blt
			},
			{
				OpCodes.Bne_Un_S,
				OpCodes.Bne_Un
			},
			{
				OpCodes.Bge_Un_S,
				OpCodes.Bge_Un
			},
			{
				OpCodes.Bgt_Un_S,
				OpCodes.Bgt_Un
			},
			{
				OpCodes.Ble_Un_S,
				OpCodes.Ble_Un
			},
			{
				OpCodes.Br_S,
				OpCodes.Br
			},
			{
				OpCodes.Blt_Un_S,
				OpCodes.Blt_Un
			}
		};

		// Token: 0x04000031 RID: 49
		private static readonly OpCode[] one_byte_opcodes = new OpCode[225];

		// Token: 0x04000032 RID: 50
		private static readonly OpCode[] two_bytes_opcodes = new OpCode[31];

		// Token: 0x02000063 RID: 99
		private class ThisParameter : ParameterInfo
		{
			// Token: 0x06000439 RID: 1081 RVA: 0x00015046 File Offset: 0x00013246
			internal ThisParameter(MethodBase method)
			{
				this.MemberImpl = method;
				this.ClassImpl = method.DeclaringType;
				this.NameImpl = "this";
				this.PositionImpl = -1;
			}
		}
	}
}
