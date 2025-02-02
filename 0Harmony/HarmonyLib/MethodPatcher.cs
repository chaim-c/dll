using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x02000016 RID: 22
	internal class MethodPatcher
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00005D8C File Offset: 0x00003F8C
		internal MethodPatcher(MethodBase original, MethodBase source, List<MethodInfo> prefixes, List<MethodInfo> postfixes, List<MethodInfo> transpilers, List<MethodInfo> finalizers, bool debug)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			this.debug = debug;
			this.original = original;
			this.source = source;
			this.prefixes = prefixes;
			this.postfixes = postfixes;
			this.transpilers = transpilers;
			this.finalizers = finalizers;
			if (debug)
			{
				FileLog.LogBuffered("### Patch: " + original.FullDescription());
				FileLog.FlushBuffer();
			}
			this.idx = prefixes.Count + postfixes.Count + finalizers.Count;
			this.returnType = AccessTools.GetReturnedType(original);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 1);
			defaultInterpolatedStringHandler.AppendLiteral("_Patch");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.idx);
			this.patch = MethodPatcher.CreateDynamicMethod(original, defaultInterpolatedStringHandler.ToStringAndClear(), debug);
			if (this.patch == null)
			{
				throw new Exception("Could not create replacement method");
			}
			this.il = this.patch.GetILGenerator();
			this.emitter = new Emitter(this.il, debug);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005E98 File Offset: 0x00004098
		internal MethodInfo CreateReplacement(out Dictionary<int, CodeInstruction> finalInstructions)
		{
			LocalBuilder[] existingVariables = MethodPatcher.DeclareOriginalLocalVariables(this.il, this.source ?? this.original);
			Dictionary<string, LocalBuilder> privateVars = new Dictionary<string, LocalBuilder>();
			List<MethodInfo> list = this.prefixes.Union(this.postfixes).Union(this.finalizers).ToList<MethodInfo>();
			LocalBuilder localBuilder = null;
			if (this.idx > 0)
			{
				localBuilder = this.DeclareLocalVariable(this.returnType, true);
				privateVars["__result"] = localBuilder;
			}
			if (list.Any((MethodInfo fix) => fix.GetParameters().Any((ParameterInfo p) => p.Name == "__resultRef")) && this.returnType.IsByRef)
			{
				LocalBuilder localBuilder2 = this.il.DeclareLocal(typeof(RefResult<>).MakeGenericType(new Type[]
				{
					this.returnType.GetElementType()
				}));
				this.emitter.Emit(OpCodes.Ldnull);
				this.emitter.Emit(OpCodes.Stloc, localBuilder2);
				privateVars["__resultRef"] = localBuilder2;
			}
			if (list.Any((MethodInfo fix) => fix.GetParameters().Any((ParameterInfo p) => p.Name == "__args")))
			{
				this.PrepareArgumentArray();
				LocalBuilder localBuilder3 = this.il.DeclareLocal(typeof(object[]));
				this.emitter.Emit(OpCodes.Stloc, localBuilder3);
				privateVars["__args"] = localBuilder3;
			}
			Label? label = null;
			LocalBuilder localBuilder4 = null;
			IEnumerable<MethodInfo> enumerable = this.prefixes;
			Func<MethodInfo, bool> predicate;
			if ((predicate = MethodPatcher.<>O.<0>__PrefixAffectsOriginal) == null)
			{
				predicate = (MethodPatcher.<>O.<0>__PrefixAffectsOriginal = new Func<MethodInfo, bool>(MethodPatcher.PrefixAffectsOriginal));
			}
			bool flag = enumerable.Any(predicate);
			bool flag2 = list.Any((MethodInfo fix) => fix.GetParameters().Any((ParameterInfo p) => p.Name == "__runOriginal"));
			if (flag || flag2)
			{
				localBuilder4 = this.DeclareLocalVariable(typeof(bool), false);
				this.emitter.Emit(OpCodes.Ldc_I4_1);
				this.emitter.Emit(OpCodes.Stloc, localBuilder4);
				if (flag)
				{
					label = new Label?(this.il.DefineLabel());
				}
			}
			list.ForEach(delegate(MethodInfo fix)
			{
				if (fix.DeclaringType != null && !privateVars.ContainsKey(fix.DeclaringType.AssemblyQualifiedName))
				{
					(from patchParam in fix.GetParameters()
					where patchParam.Name == "__state"
					select patchParam).Do(delegate(ParameterInfo patchParam)
					{
						LocalBuilder value = this.DeclareLocalVariable(patchParam.ParameterType, false);
						privateVars[fix.DeclaringType.AssemblyQualifiedName] = value;
					});
				}
			});
			LocalBuilder local = null;
			if (this.finalizers.Count > 0)
			{
				local = this.DeclareLocalVariable(typeof(bool), false);
				privateVars["__exception"] = this.DeclareLocalVariable(typeof(Exception), false);
				Label? label2;
				this.emitter.MarkBlockBefore(new ExceptionBlock(ExceptionBlockType.BeginExceptionBlock, null), out label2);
			}
			this.AddPrefixes(privateVars, localBuilder4);
			if (label != null)
			{
				this.emitter.Emit(OpCodes.Ldloc, localBuilder4);
				this.emitter.Emit(OpCodes.Brfalse, label.Value);
			}
			MethodCopier methodCopier = new MethodCopier(this.source ?? this.original, this.il, existingVariables);
			methodCopier.SetDebugging(this.debug);
			foreach (MethodInfo transpiler in this.transpilers)
			{
				methodCopier.AddTranspiler(transpiler);
			}
			methodCopier.AddTranspiler(PatchTools.m_GetExecutingAssemblyReplacementTranspiler);
			List<Label> list2 = new List<Label>();
			bool flag3;
			bool flag4;
			methodCopier.Finalize(this.emitter, list2, out flag3, out flag4);
			foreach (Label label3 in list2)
			{
				this.emitter.MarkLabel(label3);
			}
			if (localBuilder != null && flag3)
			{
				this.emitter.Emit(OpCodes.Stloc, localBuilder);
			}
			if (label != null)
			{
				this.emitter.MarkLabel(label.Value);
			}
			this.AddPostfixes(privateVars, localBuilder4, false);
			if (localBuilder != null && (flag3 || (flag4 && label != null)))
			{
				this.emitter.Emit(OpCodes.Ldloc, localBuilder);
			}
			bool flag5 = this.AddPostfixes(privateVars, localBuilder4, true);
			bool flag6 = this.finalizers.Count > 0;
			if (flag6)
			{
				if (flag5)
				{
					this.emitter.Emit(OpCodes.Stloc, localBuilder);
					this.emitter.Emit(OpCodes.Ldloc, localBuilder);
				}
				this.AddFinalizers(privateVars, localBuilder4, false);
				this.emitter.Emit(OpCodes.Ldc_I4_1);
				this.emitter.Emit(OpCodes.Stloc, local);
				Label label4 = this.il.DefineLabel();
				this.emitter.Emit(OpCodes.Ldloc, privateVars["__exception"]);
				this.emitter.Emit(OpCodes.Brfalse, label4);
				this.emitter.Emit(OpCodes.Ldloc, privateVars["__exception"]);
				this.emitter.Emit(OpCodes.Throw);
				this.emitter.MarkLabel(label4);
				Label? label5;
				this.emitter.MarkBlockBefore(new ExceptionBlock(ExceptionBlockType.BeginCatchBlock, null), out label5);
				this.emitter.Emit(OpCodes.Stloc, privateVars["__exception"]);
				this.emitter.Emit(OpCodes.Ldloc, local);
				Label label6 = this.il.DefineLabel();
				this.emitter.Emit(OpCodes.Brtrue, label6);
				bool flag7 = this.AddFinalizers(privateVars, localBuilder4, true);
				this.emitter.MarkLabel(label6);
				Label label7 = this.il.DefineLabel();
				this.emitter.Emit(OpCodes.Ldloc, privateVars["__exception"]);
				this.emitter.Emit(OpCodes.Brfalse, label7);
				if (flag7)
				{
					this.emitter.Emit(OpCodes.Rethrow);
				}
				else
				{
					this.emitter.Emit(OpCodes.Ldloc, privateVars["__exception"]);
					this.emitter.Emit(OpCodes.Throw);
				}
				this.emitter.MarkLabel(label7);
				this.emitter.MarkBlockAfter(new ExceptionBlock(ExceptionBlockType.EndExceptionBlock, null));
				if (localBuilder != null)
				{
					this.emitter.Emit(OpCodes.Ldloc, localBuilder);
				}
			}
			if (!flag4 || label != null || flag6 || this.postfixes.Count > 0)
			{
				this.emitter.Emit(OpCodes.Ret);
			}
			finalInstructions = this.emitter.GetInstructions();
			if (this.debug)
			{
				FileLog.LogBuffered("DONE");
				FileLog.LogBuffered("");
				FileLog.FlushBuffer();
			}
			return this.patch.Generate();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000656C File Offset: 0x0000476C
		internal static DynamicMethodDefinition CreateDynamicMethod(MethodBase original, string suffix, bool debug)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			Type declaringType = original.DeclaringType;
			string text = (((declaringType != null) ? declaringType.FullName : null) ?? "GLOBALTYPE") + "." + original.Name + suffix;
			text = text.Replace("<>", "");
			ParameterInfo[] parameters = original.GetParameters();
			List<Type> list = new List<Type>();
			list.AddRange(parameters.Types());
			if (!original.IsStatic)
			{
				if (AccessTools.IsStruct(original.DeclaringType))
				{
					list.Insert(0, original.DeclaringType.MakeByRefType());
				}
				else
				{
					list.Insert(0, original.DeclaringType);
				}
			}
			Type returnedType = AccessTools.GetReturnedType(original);
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(text, returnedType, list.ToArray());
			int num = (!original.IsStatic) ? 1 : 0;
			if (!original.IsStatic)
			{
				dynamicMethodDefinition.Definition.Parameters[0].Name = "this";
			}
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterDefinition parameterDefinition = dynamicMethodDefinition.Definition.Parameters[i + num];
				parameterDefinition.Attributes = (Mono.Cecil.ParameterAttributes)parameters[i].Attributes;
				parameterDefinition.Name = parameters[i].Name;
			}
			if (debug)
			{
				List<string> list2 = (from p in list
				select p.FullDescription()).ToList<string>();
				if (list.Count == dynamicMethodDefinition.Definition.Parameters.Count)
				{
					for (int j = 0; j < list.Count; j++)
					{
						List<string> list3 = list2;
						int index = j;
						list3[index] = list3[index] + " " + dynamicMethodDefinition.Definition.Parameters[j].Name;
					}
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(29, 4);
				defaultInterpolatedStringHandler.AppendLiteral("### Replacement: static ");
				defaultInterpolatedStringHandler.AppendFormatted(returnedType.FullDescription());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				Type declaringType2 = original.DeclaringType;
				defaultInterpolatedStringHandler.AppendFormatted(((declaringType2 != null) ? declaringType2.FullName : null) ?? "GLOBALTYPE");
				defaultInterpolatedStringHandler.AppendLiteral("::");
				defaultInterpolatedStringHandler.AppendFormatted(text);
				defaultInterpolatedStringHandler.AppendLiteral("(");
				defaultInterpolatedStringHandler.AppendFormatted(list2.Join(null, ", "));
				defaultInterpolatedStringHandler.AppendLiteral(")");
				FileLog.Log(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			return dynamicMethodDefinition;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000067E0 File Offset: 0x000049E0
		internal static LocalBuilder[] DeclareOriginalLocalVariables(ILGenerator il, MethodBase member)
		{
			MethodBody methodBody = member.GetMethodBody();
			IList<LocalVariableInfo> list = (methodBody != null) ? methodBody.LocalVariables : null;
			if (list == null)
			{
				return Array.Empty<LocalBuilder>();
			}
			return (from lvi in list
			select il.DeclareLocal(lvi.LocalType, lvi.IsPinned)).ToArray<LocalBuilder>();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006830 File Offset: 0x00004A30
		private LocalBuilder DeclareLocalVariable(Type type, bool isReturnValue = false)
		{
			if (type.IsByRef)
			{
				if (isReturnValue)
				{
					LocalBuilder localBuilder = this.il.DeclareLocal(type);
					this.emitter.Emit(OpCodes.Ldc_I4_1);
					this.emitter.Emit(OpCodes.Newarr, type.GetElementType());
					this.emitter.Emit(OpCodes.Ldc_I4_0);
					this.emitter.Emit(OpCodes.Ldelema, type.GetElementType());
					this.emitter.Emit(OpCodes.Stloc, localBuilder);
					return localBuilder;
				}
				type = type.GetElementType();
			}
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			if (AccessTools.IsClass(type))
			{
				LocalBuilder localBuilder2 = this.il.DeclareLocal(type);
				this.emitter.Emit(OpCodes.Ldnull);
				this.emitter.Emit(OpCodes.Stloc, localBuilder2);
				return localBuilder2;
			}
			if (AccessTools.IsStruct(type))
			{
				LocalBuilder localBuilder3 = this.il.DeclareLocal(type);
				this.emitter.Emit(OpCodes.Ldloca, localBuilder3);
				this.emitter.Emit(OpCodes.Initobj, type);
				return localBuilder3;
			}
			if (AccessTools.IsValue(type))
			{
				LocalBuilder localBuilder4 = this.il.DeclareLocal(type);
				if (type == typeof(float))
				{
					this.emitter.Emit(OpCodes.Ldc_R4, 0f);
				}
				else if (type == typeof(double))
				{
					this.emitter.Emit(OpCodes.Ldc_R8, 0.0);
				}
				else if (type == typeof(long) || type == typeof(ulong))
				{
					this.emitter.Emit(OpCodes.Ldc_I8, 0L);
				}
				else
				{
					this.emitter.Emit(OpCodes.Ldc_I4, 0);
				}
				this.emitter.Emit(OpCodes.Stloc, localBuilder4);
				return localBuilder4;
			}
			return null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00006A08 File Offset: 0x00004C08
		private static OpCode LoadIndOpCodeFor(Type type)
		{
			if (type.IsEnum)
			{
				return OpCodes.Ldind_I4;
			}
			if (type == typeof(float))
			{
				return OpCodes.Ldind_R4;
			}
			if (type == typeof(double))
			{
				return OpCodes.Ldind_R8;
			}
			if (type == typeof(byte))
			{
				return OpCodes.Ldind_U1;
			}
			if (type == typeof(ushort))
			{
				return OpCodes.Ldind_U2;
			}
			if (type == typeof(uint))
			{
				return OpCodes.Ldind_U4;
			}
			if (type == typeof(ulong))
			{
				return OpCodes.Ldind_I8;
			}
			if (type == typeof(sbyte))
			{
				return OpCodes.Ldind_I1;
			}
			if (type == typeof(short))
			{
				return OpCodes.Ldind_I2;
			}
			if (type == typeof(int))
			{
				return OpCodes.Ldind_I4;
			}
			if (type == typeof(long))
			{
				return OpCodes.Ldind_I8;
			}
			return OpCodes.Ldind_Ref;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006B18 File Offset: 0x00004D18
		private static OpCode StoreIndOpCodeFor(Type type)
		{
			if (type.IsEnum)
			{
				return OpCodes.Stind_I4;
			}
			if (type == typeof(float))
			{
				return OpCodes.Stind_R4;
			}
			if (type == typeof(double))
			{
				return OpCodes.Stind_R8;
			}
			if (type == typeof(byte))
			{
				return OpCodes.Stind_I1;
			}
			if (type == typeof(ushort))
			{
				return OpCodes.Stind_I2;
			}
			if (type == typeof(uint))
			{
				return OpCodes.Stind_I4;
			}
			if (type == typeof(ulong))
			{
				return OpCodes.Stind_I8;
			}
			if (type == typeof(sbyte))
			{
				return OpCodes.Stind_I1;
			}
			if (type == typeof(short))
			{
				return OpCodes.Stind_I2;
			}
			if (type == typeof(int))
			{
				return OpCodes.Stind_I4;
			}
			if (type == typeof(long))
			{
				return OpCodes.Stind_I8;
			}
			return OpCodes.Stind_Ref;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006C28 File Offset: 0x00004E28
		private void InitializeOutParameter(int argIndex, Type type)
		{
			if (type.IsByRef)
			{
				type = type.GetElementType();
			}
			this.emitter.Emit(OpCodes.Ldarg, argIndex);
			if (AccessTools.IsStruct(type))
			{
				this.emitter.Emit(OpCodes.Initobj, type);
				return;
			}
			if (!AccessTools.IsValue(type))
			{
				this.emitter.Emit(OpCodes.Ldnull);
				this.emitter.Emit(OpCodes.Stind_Ref);
				return;
			}
			if (type == typeof(float))
			{
				this.emitter.Emit(OpCodes.Ldc_R4, 0f);
				this.emitter.Emit(OpCodes.Stind_R4);
				return;
			}
			if (type == typeof(double))
			{
				this.emitter.Emit(OpCodes.Ldc_R8, 0.0);
				this.emitter.Emit(OpCodes.Stind_R8);
				return;
			}
			if (type == typeof(long))
			{
				this.emitter.Emit(OpCodes.Ldc_I8, 0L);
				this.emitter.Emit(OpCodes.Stind_I8);
				return;
			}
			this.emitter.Emit(OpCodes.Ldc_I4, 0);
			this.emitter.Emit(OpCodes.Stind_I4);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006D68 File Offset: 0x00004F68
		private bool EmitOriginalBaseMethod()
		{
			MethodInfo methodInfo = this.original as MethodInfo;
			if (methodInfo != null)
			{
				this.emitter.Emit(OpCodes.Ldtoken, methodInfo);
			}
			else
			{
				ConstructorInfo constructorInfo = this.original as ConstructorInfo;
				if (constructorInfo == null)
				{
					return false;
				}
				this.emitter.Emit(OpCodes.Ldtoken, constructorInfo);
			}
			Type reflectedType = this.original.ReflectedType;
			if (reflectedType.IsGenericType)
			{
				this.emitter.Emit(OpCodes.Ldtoken, reflectedType);
			}
			this.emitter.Emit(OpCodes.Call, reflectedType.IsGenericType ? MethodPatcher.m_GetMethodFromHandle2 : MethodPatcher.m_GetMethodFromHandle1);
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00006E08 File Offset: 0x00005008
		private void EmitCallParameter(MethodInfo patch, Dictionary<string, LocalBuilder> variables, LocalBuilder runOriginalVariable, bool allowFirsParamPassthrough, out LocalBuilder tmpInstanceBoxingVar, out LocalBuilder tmpObjectVar, out bool refResultUsed, List<KeyValuePair<LocalBuilder, Type>> tmpBoxVars)
		{
			tmpInstanceBoxingVar = null;
			tmpObjectVar = null;
			refResultUsed = false;
			bool flag = !this.original.IsStatic;
			ParameterInfo[] parameters = this.original.GetParameters();
			string[] originalParameterNames = (from p in parameters
			select p.Name).ToArray<string>();
			Type declaringType = this.original.DeclaringType;
			List<ParameterInfo> list = patch.GetParameters().ToList<ParameterInfo>();
			if (allowFirsParamPassthrough && patch.ReturnType != typeof(void) && list.Count > 0 && list[0].ParameterType == patch.ReturnType)
			{
				list.RemoveRange(0, 1);
			}
			foreach (ParameterInfo parameterInfo in list)
			{
				LocalBuilder local3;
				if (parameterInfo.Name == "__originalMethod")
				{
					if (!this.EmitOriginalBaseMethod())
					{
						this.emitter.Emit(OpCodes.Ldnull);
					}
				}
				else if (parameterInfo.Name == "__runOriginal")
				{
					if (runOriginalVariable != null)
					{
						this.emitter.Emit(OpCodes.Ldloc, runOriginalVariable);
					}
					else
					{
						this.emitter.Emit(OpCodes.Ldc_I4_0);
					}
				}
				else if (parameterInfo.Name == "__instance")
				{
					if (this.original.IsStatic)
					{
						this.emitter.Emit(OpCodes.Ldnull);
					}
					else
					{
						Type parameterType = parameterInfo.ParameterType;
						bool isByRef = parameterType.IsByRef;
						bool flag2 = parameterType == typeof(object) || parameterType == typeof(object).MakeByRefType();
						if (AccessTools.IsStruct(declaringType))
						{
							if (flag2)
							{
								if (isByRef)
								{
									this.emitter.Emit(OpCodes.Ldarg_0);
									this.emitter.Emit(OpCodes.Ldobj, declaringType);
									this.emitter.Emit(OpCodes.Box, declaringType);
									tmpInstanceBoxingVar = this.il.DeclareLocal(typeof(object));
									this.emitter.Emit(OpCodes.Stloc, tmpInstanceBoxingVar);
									this.emitter.Emit(OpCodes.Ldloca, tmpInstanceBoxingVar);
								}
								else
								{
									this.emitter.Emit(OpCodes.Ldarg_0);
									this.emitter.Emit(OpCodes.Ldobj, declaringType);
									this.emitter.Emit(OpCodes.Box, declaringType);
								}
							}
							else if (isByRef)
							{
								this.emitter.Emit(OpCodes.Ldarg_0);
							}
							else
							{
								this.emitter.Emit(OpCodes.Ldarg_0);
								this.emitter.Emit(OpCodes.Ldobj, declaringType);
							}
						}
						else if (isByRef)
						{
							this.emitter.Emit(OpCodes.Ldarga, 0);
						}
						else
						{
							this.emitter.Emit(OpCodes.Ldarg_0);
						}
					}
				}
				else if (parameterInfo.Name == "__args")
				{
					LocalBuilder local;
					if (variables.TryGetValue("__args", out local))
					{
						this.emitter.Emit(OpCodes.Ldloc, local);
					}
					else
					{
						this.emitter.Emit(OpCodes.Ldnull);
					}
				}
				else if (parameterInfo.Name.StartsWith("___", StringComparison.Ordinal))
				{
					string text = parameterInfo.Name.Substring("___".Length);
					IEnumerable<char> enumerable = text;
					Func<char, bool> predicate;
					if ((predicate = MethodPatcher.<>O.<1>__IsDigit) == null)
					{
						predicate = (MethodPatcher.<>O.<1>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					FieldInfo fieldInfo;
					if (enumerable.All(predicate))
					{
						fieldInfo = AccessTools.DeclaredField(declaringType, int.Parse(text));
						if (fieldInfo == null)
						{
							throw new ArgumentException("No field found at given index in class " + (((declaringType != null) ? declaringType.AssemblyQualifiedName : null) ?? "null"), text);
						}
					}
					else
					{
						fieldInfo = AccessTools.Field(declaringType, text);
						if (fieldInfo == null)
						{
							throw new ArgumentException("No such field defined in class " + (((declaringType != null) ? declaringType.AssemblyQualifiedName : null) ?? "null"), text);
						}
					}
					if (fieldInfo.IsStatic)
					{
						this.emitter.Emit(parameterInfo.ParameterType.IsByRef ? OpCodes.Ldsflda : OpCodes.Ldsfld, fieldInfo);
					}
					else
					{
						this.emitter.Emit(OpCodes.Ldarg_0);
						this.emitter.Emit(parameterInfo.ParameterType.IsByRef ? OpCodes.Ldflda : OpCodes.Ldfld, fieldInfo);
					}
				}
				else if (parameterInfo.Name == "__state")
				{
					OpCode opcode = parameterInfo.ParameterType.IsByRef ? OpCodes.Ldloca : OpCodes.Ldloc;
					Type declaringType2 = patch.DeclaringType;
					LocalBuilder local2;
					if (variables.TryGetValue(((declaringType2 != null) ? declaringType2.AssemblyQualifiedName : null) ?? "null", out local2))
					{
						this.emitter.Emit(opcode, local2);
					}
					else
					{
						this.emitter.Emit(OpCodes.Ldnull);
					}
				}
				else if (parameterInfo.Name == "__result")
				{
					if (this.returnType == typeof(void))
					{
						throw new Exception("Cannot get result from void method " + this.original.FullDescription());
					}
					Type type = parameterInfo.ParameterType;
					if (type.IsByRef && !this.returnType.IsByRef)
					{
						type = type.GetElementType();
					}
					if (!type.IsAssignableFrom(this.returnType))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(55, 4);
						defaultInterpolatedStringHandler.AppendLiteral("Cannot assign method return type ");
						defaultInterpolatedStringHandler.AppendFormatted(this.returnType.FullName);
						defaultInterpolatedStringHandler.AppendLiteral(" to ");
						defaultInterpolatedStringHandler.AppendFormatted("__result");
						defaultInterpolatedStringHandler.AppendLiteral(" type ");
						defaultInterpolatedStringHandler.AppendFormatted(type.FullName);
						defaultInterpolatedStringHandler.AppendLiteral(" for method ");
						defaultInterpolatedStringHandler.AppendFormatted(this.original.FullDescription());
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					OpCode opcode2 = (parameterInfo.ParameterType.IsByRef && !this.returnType.IsByRef) ? OpCodes.Ldloca : OpCodes.Ldloc;
					if (this.returnType.IsValueType && parameterInfo.ParameterType == typeof(object).MakeByRefType())
					{
						opcode2 = OpCodes.Ldloc;
					}
					this.emitter.Emit(opcode2, variables["__result"]);
					if (this.returnType.IsValueType)
					{
						if (parameterInfo.ParameterType == typeof(object))
						{
							this.emitter.Emit(OpCodes.Box, this.returnType);
						}
						else if (parameterInfo.ParameterType == typeof(object).MakeByRefType())
						{
							this.emitter.Emit(OpCodes.Box, this.returnType);
							tmpObjectVar = this.il.DeclareLocal(typeof(object));
							this.emitter.Emit(OpCodes.Stloc, tmpObjectVar);
							this.emitter.Emit(OpCodes.Ldloca, tmpObjectVar);
						}
					}
				}
				else if (parameterInfo.Name == "__resultRef")
				{
					if (!this.returnType.IsByRef)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 3);
						defaultInterpolatedStringHandler.AppendLiteral("Cannot use ");
						defaultInterpolatedStringHandler.AppendFormatted("__resultRef");
						defaultInterpolatedStringHandler.AppendLiteral(" with non-ref return type ");
						defaultInterpolatedStringHandler.AppendFormatted(this.returnType.FullName);
						defaultInterpolatedStringHandler.AppendLiteral(" of method ");
						defaultInterpolatedStringHandler.AppendFormatted(this.original.FullDescription());
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					Type parameterType2 = parameterInfo.ParameterType;
					Type type2 = typeof(RefResult<>).MakeGenericType(new Type[]
					{
						this.returnType.GetElementType()
					}).MakeByRefType();
					if (parameterType2 != type2)
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 4);
						defaultInterpolatedStringHandler.AppendLiteral("Wrong type of ");
						defaultInterpolatedStringHandler.AppendFormatted("__resultRef");
						defaultInterpolatedStringHandler.AppendLiteral(" for method ");
						defaultInterpolatedStringHandler.AppendFormatted(this.original.FullDescription());
						defaultInterpolatedStringHandler.AppendLiteral(". Expected ");
						defaultInterpolatedStringHandler.AppendFormatted(type2.FullName);
						defaultInterpolatedStringHandler.AppendLiteral(", got ");
						defaultInterpolatedStringHandler.AppendFormatted(parameterType2.FullName);
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					this.emitter.Emit(OpCodes.Ldloca, variables["__resultRef"]);
					refResultUsed = true;
				}
				else if (variables.TryGetValue(parameterInfo.Name, out local3))
				{
					OpCode opcode3 = parameterInfo.ParameterType.IsByRef ? OpCodes.Ldloca : OpCodes.Ldloc;
					this.emitter.Emit(opcode3, local3);
				}
				else
				{
					int argumentIndex;
					if (parameterInfo.Name.StartsWith("__", StringComparison.Ordinal))
					{
						string s = parameterInfo.Name.Substring("__".Length);
						if (!int.TryParse(s, out argumentIndex))
						{
							throw new Exception("Parameter " + parameterInfo.Name + " does not contain a valid index");
						}
						if (argumentIndex < 0 || argumentIndex >= parameters.Length)
						{
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(28, 1);
							defaultInterpolatedStringHandler.AppendLiteral("No parameter found at index ");
							defaultInterpolatedStringHandler.AppendFormatted<int>(argumentIndex);
							throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
					else
					{
						argumentIndex = patch.GetArgumentIndex(originalParameterNames, parameterInfo);
						if (argumentIndex == -1)
						{
							HarmonyMethod mergedFromType = HarmonyMethodExtensions.GetMergedFromType(parameterInfo.ParameterType);
							HarmonyMethod harmonyMethod = mergedFromType;
							MethodType value = harmonyMethod.methodType.GetValueOrDefault();
							if (harmonyMethod.methodType == null)
							{
								value = MethodType.Normal;
								harmonyMethod.methodType = new MethodType?(value);
							}
							MethodBase originalMethod = mergedFromType.GetOriginalMethod();
							MethodInfo methodInfo = originalMethod as MethodInfo;
							if (methodInfo != null)
							{
								ConstructorInfo constructor = parameterInfo.ParameterType.GetConstructor(new Type[]
								{
									typeof(object),
									typeof(IntPtr)
								});
								if (constructor != null)
								{
									if (methodInfo.IsStatic)
									{
										this.emitter.Emit(OpCodes.Ldnull);
									}
									else
									{
										this.emitter.Emit(OpCodes.Ldarg_0);
										if (declaringType != null && declaringType.IsValueType)
										{
											this.emitter.Emit(OpCodes.Ldobj, declaringType);
											this.emitter.Emit(OpCodes.Box, declaringType);
										}
									}
									if (!methodInfo.IsStatic && !mergedFromType.nonVirtualDelegate)
									{
										this.emitter.Emit(OpCodes.Dup);
										this.emitter.Emit(OpCodes.Ldvirtftn, methodInfo);
									}
									else
									{
										this.emitter.Emit(OpCodes.Ldftn, methodInfo);
									}
									this.emitter.Emit(OpCodes.Newobj, constructor);
									continue;
								}
							}
							throw new Exception("Parameter \"" + parameterInfo.Name + "\" not found in method " + this.original.FullDescription());
						}
					}
					Type parameterType3 = parameters[argumentIndex].ParameterType;
					Type type3 = parameterType3.IsByRef ? parameterType3.GetElementType() : parameterType3;
					Type parameterType4 = parameterInfo.ParameterType;
					Type type4 = parameterType4.IsByRef ? parameterType4.GetElementType() : parameterType4;
					bool flag3 = !parameters[argumentIndex].IsOut && !parameterType3.IsByRef;
					bool flag4 = !parameterInfo.IsOut && !parameterType4.IsByRef;
					bool flag5 = type3.IsValueType && !type4.IsValueType;
					int arg = argumentIndex + ((flag > false) ? 1 : 0);
					if (flag3 == flag4)
					{
						this.emitter.Emit(OpCodes.Ldarg, arg);
						if (flag5)
						{
							if (flag4)
							{
								this.emitter.Emit(OpCodes.Box, type3);
							}
							else
							{
								this.emitter.Emit(OpCodes.Ldobj, type3);
								this.emitter.Emit(OpCodes.Box, type3);
								LocalBuilder localBuilder = this.il.DeclareLocal(type4);
								this.emitter.Emit(OpCodes.Stloc, localBuilder);
								this.emitter.Emit(OpCodes.Ldloca_S, localBuilder);
								tmpBoxVars.Add(new KeyValuePair<LocalBuilder, Type>(localBuilder, type3));
							}
						}
					}
					else if (flag3 && !flag4)
					{
						if (flag5)
						{
							this.emitter.Emit(OpCodes.Ldarg, arg);
							this.emitter.Emit(OpCodes.Box, type3);
							LocalBuilder local4 = this.il.DeclareLocal(type4);
							this.emitter.Emit(OpCodes.Stloc, local4);
							this.emitter.Emit(OpCodes.Ldloca_S, local4);
						}
						else
						{
							this.emitter.Emit(OpCodes.Ldarga, arg);
						}
					}
					else
					{
						this.emitter.Emit(OpCodes.Ldarg, arg);
						if (flag5)
						{
							this.emitter.Emit(OpCodes.Ldobj, type3);
							this.emitter.Emit(OpCodes.Box, type3);
						}
						else if (type3.IsValueType)
						{
							this.emitter.Emit(OpCodes.Ldobj, type3);
						}
						else
						{
							this.emitter.Emit(MethodPatcher.LoadIndOpCodeFor(parameters[argumentIndex].ParameterType));
						}
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007B58 File Offset: 0x00005D58
		private static bool PrefixAffectsOriginal(MethodInfo fix)
		{
			if (fix.ReturnType == typeof(bool))
			{
				return true;
			}
			return fix.GetParameters().Any(delegate(ParameterInfo p)
			{
				string name = p.Name;
				Type parameterType = p.ParameterType;
				return !(name == "__instance") && !(name == "__originalMethod") && !(name == "__state") && (p.IsOut || p.IsRetval || parameterType.IsByRef || (!AccessTools.IsValue(parameterType) && !AccessTools.IsStruct(parameterType)));
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00007BA8 File Offset: 0x00005DA8
		private void AddPrefixes(Dictionary<string, LocalBuilder> variables, LocalBuilder runOriginalVariable)
		{
			Action<KeyValuePair<LocalBuilder, Type>> <>9__2;
			this.prefixes.Do(delegate(MethodInfo fix)
			{
				Label? label = MethodPatcher.PrefixAffectsOriginal(fix) ? new Label?(this.il.DefineLabel()) : null;
				if (label != null)
				{
					this.emitter.Emit(OpCodes.Ldloc, runOriginalVariable);
					this.emitter.Emit(OpCodes.Brfalse, label.Value);
				}
				List<KeyValuePair<LocalBuilder, Type>> list = new List<KeyValuePair<LocalBuilder, Type>>();
				LocalBuilder localBuilder;
				LocalBuilder localBuilder2;
				bool flag;
				this.EmitCallParameter(fix, variables, runOriginalVariable, false, out localBuilder, out localBuilder2, out flag, list);
				this.emitter.Emit(OpCodes.Call, fix);
				if (fix.GetParameters().Any((ParameterInfo p) => p.Name == "__args"))
				{
					this.RestoreArgumentArray(variables);
				}
				if (localBuilder != null)
				{
					this.emitter.Emit(OpCodes.Ldarg_0);
					this.emitter.Emit(OpCodes.Ldloc, localBuilder);
					this.emitter.Emit(OpCodes.Unbox_Any, this.original.DeclaringType);
					this.emitter.Emit(OpCodes.Stobj, this.original.DeclaringType);
				}
				if (flag)
				{
					Label label2 = this.il.DefineLabel();
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Brfalse_S, label2);
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Callvirt, AccessTools.Method(variables["__resultRef"].LocalType, "Invoke", null, null));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
					this.emitter.Emit(OpCodes.Ldnull);
					this.emitter.Emit(OpCodes.Stloc, variables["__resultRef"]);
					this.emitter.MarkLabel(label2);
					this.emitter.Emit(OpCodes.Nop);
				}
				else if (localBuilder2 != null)
				{
					this.emitter.Emit(OpCodes.Ldloc, localBuilder2);
					this.emitter.Emit(OpCodes.Unbox_Any, AccessTools.GetReturnedType(this.original));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
				}
				IEnumerable<KeyValuePair<LocalBuilder, Type>> sequence = list;
				Action<KeyValuePair<LocalBuilder, Type>> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(KeyValuePair<LocalBuilder, Type> tmpBoxVar)
					{
						this.emitter.Emit(this.original.IsStatic ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1);
						this.emitter.Emit(OpCodes.Ldloc, tmpBoxVar.Key);
						this.emitter.Emit(OpCodes.Unbox_Any, tmpBoxVar.Value);
						this.emitter.Emit(OpCodes.Stobj, tmpBoxVar.Value);
					});
				}
				sequence.Do(action);
				Type left = fix.ReturnType;
				if (left != typeof(void))
				{
					if (left != typeof(bool))
					{
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 2);
						defaultInterpolatedStringHandler.AppendLiteral("Prefix patch ");
						defaultInterpolatedStringHandler.AppendFormatted<MethodInfo>(fix);
						defaultInterpolatedStringHandler.AppendLiteral(" has not \"bool\" or \"void\" return type: ");
						defaultInterpolatedStringHandler.AppendFormatted<Type>(fix.ReturnType);
						throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
					}
					this.emitter.Emit(OpCodes.Stloc, runOriginalVariable);
				}
				if (label != null)
				{
					this.emitter.MarkLabel(label.Value);
					this.emitter.Emit(OpCodes.Nop);
				}
			});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007BE8 File Offset: 0x00005DE8
		private bool AddPostfixes(Dictionary<string, LocalBuilder> variables, LocalBuilder runOriginalVariable, bool passthroughPatches)
		{
			bool result = false;
			Action<KeyValuePair<LocalBuilder, Type>> <>9__3;
			(from fix in this.postfixes
			where passthroughPatches == (fix.ReturnType != typeof(void))
			select fix).Do(delegate(MethodInfo fix)
			{
				List<KeyValuePair<LocalBuilder, Type>> list = new List<KeyValuePair<LocalBuilder, Type>>();
				LocalBuilder localBuilder;
				LocalBuilder localBuilder2;
				bool flag;
				this.EmitCallParameter(fix, variables, runOriginalVariable, true, out localBuilder, out localBuilder2, out flag, list);
				this.emitter.Emit(OpCodes.Call, fix);
				if (fix.GetParameters().Any((ParameterInfo p) => p.Name == "__args"))
				{
					this.RestoreArgumentArray(variables);
				}
				if (localBuilder != null)
				{
					this.emitter.Emit(OpCodes.Ldarg_0);
					this.emitter.Emit(OpCodes.Ldloc, localBuilder);
					this.emitter.Emit(OpCodes.Unbox_Any, this.original.DeclaringType);
					this.emitter.Emit(OpCodes.Stobj, this.original.DeclaringType);
				}
				if (flag)
				{
					Label label = this.il.DefineLabel();
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Brfalse_S, label);
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Callvirt, AccessTools.Method(variables["__resultRef"].LocalType, "Invoke", null, null));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
					this.emitter.Emit(OpCodes.Ldnull);
					this.emitter.Emit(OpCodes.Stloc, variables["__resultRef"]);
					this.emitter.MarkLabel(label);
					this.emitter.Emit(OpCodes.Nop);
				}
				else if (localBuilder2 != null)
				{
					this.emitter.Emit(OpCodes.Ldloc, localBuilder2);
					this.emitter.Emit(OpCodes.Unbox_Any, AccessTools.GetReturnedType(this.original));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
				}
				IEnumerable<KeyValuePair<LocalBuilder, Type>> sequence = list;
				Action<KeyValuePair<LocalBuilder, Type>> action;
				if ((action = <>9__3) == null)
				{
					action = (<>9__3 = delegate(KeyValuePair<LocalBuilder, Type> tmpBoxVar)
					{
						this.emitter.Emit(this.original.IsStatic ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1);
						this.emitter.Emit(OpCodes.Ldloc, tmpBoxVar.Key);
						this.emitter.Emit(OpCodes.Unbox_Any, tmpBoxVar.Value);
						this.emitter.Emit(OpCodes.Stobj, tmpBoxVar.Value);
					});
				}
				sequence.Do(action);
				if (!(fix.ReturnType != typeof(void)))
				{
					return;
				}
				ParameterInfo parameterInfo = fix.GetParameters().FirstOrDefault<ParameterInfo>();
				bool flag2 = parameterInfo != null && fix.ReturnType == parameterInfo.ParameterType;
				if (flag2)
				{
					result = true;
					return;
				}
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
				if (parameterInfo != null)
				{
					defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(79, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Return type of pass through postfix ");
					defaultInterpolatedStringHandler.AppendFormatted<MethodInfo>(fix);
					defaultInterpolatedStringHandler.AppendLiteral(" does not match type of its first parameter");
					throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(45, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Postfix patch ");
				defaultInterpolatedStringHandler.AppendFormatted<MethodInfo>(fix);
				defaultInterpolatedStringHandler.AppendLiteral(" must have a \"void\" return type");
				throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
			});
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00007C4C File Offset: 0x00005E4C
		private bool AddFinalizers(Dictionary<string, LocalBuilder> variables, LocalBuilder runOriginalVariable, bool catchExceptions)
		{
			bool rethrowPossible = true;
			Action<KeyValuePair<LocalBuilder, Type>> <>9__2;
			this.finalizers.Do(delegate(MethodInfo fix)
			{
				if (catchExceptions)
				{
					Label? label;
					this.emitter.MarkBlockBefore(new ExceptionBlock(ExceptionBlockType.BeginExceptionBlock, null), out label);
				}
				List<KeyValuePair<LocalBuilder, Type>> list = new List<KeyValuePair<LocalBuilder, Type>>();
				LocalBuilder localBuilder;
				LocalBuilder localBuilder2;
				bool flag;
				this.EmitCallParameter(fix, variables, runOriginalVariable, false, out localBuilder, out localBuilder2, out flag, list);
				this.emitter.Emit(OpCodes.Call, fix);
				if (fix.GetParameters().Any((ParameterInfo p) => p.Name == "__args"))
				{
					this.RestoreArgumentArray(variables);
				}
				if (localBuilder != null)
				{
					this.emitter.Emit(OpCodes.Ldarg_0);
					this.emitter.Emit(OpCodes.Ldloc, localBuilder);
					this.emitter.Emit(OpCodes.Unbox_Any, this.original.DeclaringType);
					this.emitter.Emit(OpCodes.Stobj, this.original.DeclaringType);
				}
				if (flag)
				{
					Label label2 = this.il.DefineLabel();
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Brfalse_S, label2);
					this.emitter.Emit(OpCodes.Ldloc, variables["__resultRef"]);
					this.emitter.Emit(OpCodes.Callvirt, AccessTools.Method(variables["__resultRef"].LocalType, "Invoke", null, null));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
					this.emitter.Emit(OpCodes.Ldnull);
					this.emitter.Emit(OpCodes.Stloc, variables["__resultRef"]);
					this.emitter.MarkLabel(label2);
					this.emitter.Emit(OpCodes.Nop);
				}
				else if (localBuilder2 != null)
				{
					this.emitter.Emit(OpCodes.Ldloc, localBuilder2);
					this.emitter.Emit(OpCodes.Unbox_Any, AccessTools.GetReturnedType(this.original));
					this.emitter.Emit(OpCodes.Stloc, variables["__result"]);
				}
				IEnumerable<KeyValuePair<LocalBuilder, Type>> sequence = list;
				Action<KeyValuePair<LocalBuilder, Type>> action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate(KeyValuePair<LocalBuilder, Type> tmpBoxVar)
					{
						this.emitter.Emit(this.original.IsStatic ? OpCodes.Ldarg_0 : OpCodes.Ldarg_1);
						this.emitter.Emit(OpCodes.Ldloc, tmpBoxVar.Key);
						this.emitter.Emit(OpCodes.Unbox_Any, tmpBoxVar.Value);
						this.emitter.Emit(OpCodes.Stobj, tmpBoxVar.Value);
					});
				}
				sequence.Do(action);
				if (fix.ReturnType != typeof(void))
				{
					this.emitter.Emit(OpCodes.Stloc, variables["__exception"]);
					rethrowPossible = false;
				}
				if (catchExceptions)
				{
					Label? label3;
					this.emitter.MarkBlockBefore(new ExceptionBlock(ExceptionBlockType.BeginCatchBlock, null), out label3);
					this.emitter.Emit(OpCodes.Pop);
					this.emitter.MarkBlockAfter(new ExceptionBlock(ExceptionBlockType.EndExceptionBlock, null));
				}
			});
			return rethrowPossible;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00007CA0 File Offset: 0x00005EA0
		private void PrepareArgumentArray()
		{
			ParameterInfo[] parameters = this.original.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				int argIndex = num++ + ((!this.original.IsStatic) ? 1 : 0);
				if (parameterInfo.IsOut || parameterInfo.IsRetval)
				{
					this.InitializeOutParameter(argIndex, parameterInfo.ParameterType);
				}
			}
			this.emitter.Emit(OpCodes.Ldc_I4, parameters.Length);
			this.emitter.Emit(OpCodes.Newarr, typeof(object));
			num = 0;
			int num2 = 0;
			foreach (ParameterInfo parameterInfo2 in parameters)
			{
				int arg = num++ + ((!this.original.IsStatic) ? 1 : 0);
				Type type = parameterInfo2.ParameterType;
				bool isByRef = type.IsByRef;
				if (isByRef)
				{
					type = type.GetElementType();
				}
				this.emitter.Emit(OpCodes.Dup);
				this.emitter.Emit(OpCodes.Ldc_I4, num2++);
				this.emitter.Emit(OpCodes.Ldarg, arg);
				if (isByRef)
				{
					if (AccessTools.IsStruct(type))
					{
						this.emitter.Emit(OpCodes.Ldobj, type);
					}
					else
					{
						this.emitter.Emit(MethodPatcher.LoadIndOpCodeFor(type));
					}
				}
				if (type.IsValueType)
				{
					this.emitter.Emit(OpCodes.Box, type);
				}
				this.emitter.Emit(OpCodes.Stelem_Ref);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007E2C File Offset: 0x0000602C
		private void RestoreArgumentArray(Dictionary<string, LocalBuilder> variables)
		{
			ParameterInfo[] parameters = this.original.GetParameters();
			int num = 0;
			int num2 = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				int arg = num++ + ((!this.original.IsStatic) ? 1 : 0);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
					this.emitter.Emit(OpCodes.Ldarg, arg);
					this.emitter.Emit(OpCodes.Ldloc, variables["__args"]);
					this.emitter.Emit(OpCodes.Ldc_I4, num2);
					this.emitter.Emit(OpCodes.Ldelem_Ref);
					if (type.IsValueType)
					{
						this.emitter.Emit(OpCodes.Unbox_Any, type);
						if (AccessTools.IsStruct(type))
						{
							this.emitter.Emit(OpCodes.Stobj, type);
						}
						else
						{
							this.emitter.Emit(MethodPatcher.StoreIndOpCodeFor(type));
						}
					}
					else
					{
						this.emitter.Emit(OpCodes.Castclass, type);
						this.emitter.Emit(OpCodes.Stind_Ref);
					}
				}
				else
				{
					this.emitter.Emit(OpCodes.Ldloc, variables["__args"]);
					this.emitter.Emit(OpCodes.Ldc_I4, num2);
					this.emitter.Emit(OpCodes.Ldelem_Ref);
					if (type.IsValueType)
					{
						this.emitter.Emit(OpCodes.Unbox_Any, type);
					}
					else
					{
						this.emitter.Emit(OpCodes.Castclass, type);
					}
					this.emitter.Emit(OpCodes.Starg, arg);
				}
				num2++;
			}
		}

		// Token: 0x04000033 RID: 51
		private const string INSTANCE_PARAM = "__instance";

		// Token: 0x04000034 RID: 52
		private const string ORIGINAL_METHOD_PARAM = "__originalMethod";

		// Token: 0x04000035 RID: 53
		private const string ARGS_ARRAY_VAR = "__args";

		// Token: 0x04000036 RID: 54
		private const string RESULT_VAR = "__result";

		// Token: 0x04000037 RID: 55
		private const string RESULT_REF_VAR = "__resultRef";

		// Token: 0x04000038 RID: 56
		private const string STATE_VAR = "__state";

		// Token: 0x04000039 RID: 57
		private const string EXCEPTION_VAR = "__exception";

		// Token: 0x0400003A RID: 58
		private const string RUN_ORIGINAL_VAR = "__runOriginal";

		// Token: 0x0400003B RID: 59
		private const string PARAM_INDEX_PREFIX = "__";

		// Token: 0x0400003C RID: 60
		private const string INSTANCE_FIELD_PREFIX = "___";

		// Token: 0x0400003D RID: 61
		private readonly bool debug;

		// Token: 0x0400003E RID: 62
		private readonly MethodBase original;

		// Token: 0x0400003F RID: 63
		private readonly MethodBase source;

		// Token: 0x04000040 RID: 64
		private readonly List<MethodInfo> prefixes;

		// Token: 0x04000041 RID: 65
		private readonly List<MethodInfo> postfixes;

		// Token: 0x04000042 RID: 66
		private readonly List<MethodInfo> transpilers;

		// Token: 0x04000043 RID: 67
		private readonly List<MethodInfo> finalizers;

		// Token: 0x04000044 RID: 68
		private readonly int idx;

		// Token: 0x04000045 RID: 69
		private readonly Type returnType;

		// Token: 0x04000046 RID: 70
		private readonly DynamicMethodDefinition patch;

		// Token: 0x04000047 RID: 71
		private readonly ILGenerator il;

		// Token: 0x04000048 RID: 72
		private readonly Emitter emitter;

		// Token: 0x04000049 RID: 73
		private static readonly MethodInfo m_GetMethodFromHandle1 = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
		{
			typeof(RuntimeMethodHandle)
		});

		// Token: 0x0400004A RID: 74
		private static readonly MethodInfo m_GetMethodFromHandle2 = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[]
		{
			typeof(RuntimeMethodHandle),
			typeof(RuntimeTypeHandle)
		});

		// Token: 0x02000066 RID: 102
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000138 RID: 312
			public static Func<MethodInfo, bool> <0>__PrefixAffectsOriginal;

			// Token: 0x04000139 RID: 313
			public static Func<char, bool> <1>__IsDigit;
		}
	}
}
