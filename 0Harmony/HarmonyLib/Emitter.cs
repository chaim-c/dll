using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono.Cecil.Cil;
using MonoMod.Utils.Cil;

namespace HarmonyLib
{
	// Token: 0x02000010 RID: 16
	internal class Emitter
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000035AA File Offset: 0x000017AA
		internal Emitter(ILGenerator il, bool debug)
		{
			this.il = il.GetProxiedShim<CecilILGenerator>();
			this.debug = debug;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035D0 File Offset: 0x000017D0
		internal Dictionary<int, CodeInstruction> GetInstructions()
		{
			return this.instructions;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000035D8 File Offset: 0x000017D8
		internal void AddInstruction(System.Reflection.Emit.OpCode opcode, object operand)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, operand));
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000035F2 File Offset: 0x000017F2
		internal int CurrentPos()
		{
			return this.il.ILOffset;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000035FF File Offset: 0x000017FF
		internal static string CodePos(int offset)
		{
			return string.Format("IL_{0:X4}: ", offset);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003611 File Offset: 0x00001811
		internal string CodePos()
		{
			return Emitter.CodePos(this.CurrentPos());
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003620 File Offset: 0x00001820
		internal void LogComment(string comment)
		{
			if (this.debug)
			{
				string str = string.Format("{0}// {1}", this.CodePos(), comment);
				FileLog.LogBuffered(str);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000364D File Offset: 0x0000184D
		internal void LogIL(System.Reflection.Emit.OpCode opcode)
		{
			if (this.debug)
			{
				FileLog.LogBuffered(string.Format("{0}{1}", this.CodePos(), opcode));
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003674 File Offset: 0x00001874
		internal void LogIL(System.Reflection.Emit.OpCode opcode, object arg, string extra = null)
		{
			if (this.debug)
			{
				string text = Emitter.FormatArgument(arg, extra);
				string text2 = (text.Length > 0) ? " " : "";
				string text3 = opcode.ToString();
				if (opcode.FlowControl == System.Reflection.Emit.FlowControl.Branch || opcode.FlowControl == System.Reflection.Emit.FlowControl.Cond_Branch)
				{
					text3 += " =>";
				}
				text3 = text3.PadRight(10);
				FileLog.LogBuffered(string.Format("{0}{1}{2}{3}", new object[]
				{
					this.CodePos(),
					text3,
					text2,
					text
				}));
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003708 File Offset: 0x00001908
		internal void LogAllLocalVariables()
		{
			if (!this.debug)
			{
				return;
			}
			this.il.IL.Body.Variables.Do(delegate(VariableDefinition v)
			{
				string str = string.Format("{0}Local var {1}: {2}{3}", new object[]
				{
					Emitter.CodePos(0),
					v.Index,
					v.VariableType.FullName,
					v.IsPinned ? "(pinned)" : ""
				});
				FileLog.LogBuffered(str);
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003758 File Offset: 0x00001958
		internal static string FormatArgument(object argument, string extra = null)
		{
			if (argument == null)
			{
				return "NULL";
			}
			Type type = argument.GetType();
			MethodBase methodBase = argument as MethodBase;
			if (methodBase != null)
			{
				return methodBase.FullDescription() + ((extra != null) ? (" " + extra) : "");
			}
			FieldInfo fieldInfo = argument as FieldInfo;
			if (fieldInfo != null)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler.AppendFormatted(fieldInfo.FieldType.FullDescription());
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(fieldInfo.DeclaringType.FullDescription());
				defaultInterpolatedStringHandler.AppendLiteral("::");
				defaultInterpolatedStringHandler.AppendFormatted(fieldInfo.Name);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			if (type == typeof(Label))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Label");
				defaultInterpolatedStringHandler.AppendFormatted<int>(((Label)argument).GetHashCode());
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			if (type == typeof(Label[]))
			{
				return "Labels" + string.Join(",", (from l in (Label[])argument
				select l.GetHashCode().ToString()).ToArray<string>());
			}
			if (type == typeof(LocalBuilder))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendFormatted<int>(((LocalBuilder)argument).LocalIndex);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted<Type>(((LocalBuilder)argument).LocalType);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			if (type == typeof(string))
			{
				return argument.ToString().ToLiteral("\"");
			}
			return argument.ToString().Trim();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003932 File Offset: 0x00001B32
		internal void MarkLabel(Label label)
		{
			if (this.debug)
			{
				FileLog.LogBuffered(this.CodePos() + Emitter.FormatArgument(label, null));
			}
			this.il.MarkLabel(label);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003964 File Offset: 0x00001B64
		internal void MarkBlockBefore(ExceptionBlock block, out Label? label)
		{
			label = null;
			switch (block.blockType)
			{
			case ExceptionBlockType.BeginExceptionBlock:
				if (this.debug)
				{
					FileLog.LogBuffered(".try");
					FileLog.LogBuffered("{");
					FileLog.ChangeIndent(1);
				}
				label = new Label?(this.il.BeginExceptionBlock());
				return;
			case ExceptionBlockType.BeginCatchBlock:
				if (this.debug)
				{
					this.LogIL(System.Reflection.Emit.OpCodes.Leave, new LeaveTry(), null);
					FileLog.ChangeIndent(-1);
					FileLog.LogBuffered("} // end try");
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 1);
					defaultInterpolatedStringHandler.AppendLiteral(".catch ");
					defaultInterpolatedStringHandler.AppendFormatted<Type>(block.catchType);
					FileLog.LogBuffered(defaultInterpolatedStringHandler.ToStringAndClear());
					FileLog.LogBuffered("{");
					FileLog.ChangeIndent(1);
				}
				this.il.BeginCatchBlock(block.catchType);
				return;
			case ExceptionBlockType.BeginExceptFilterBlock:
				if (this.debug)
				{
					this.LogIL(System.Reflection.Emit.OpCodes.Leave, new LeaveTry(), null);
					FileLog.ChangeIndent(-1);
					FileLog.LogBuffered("} // end try");
					FileLog.LogBuffered(".filter");
					FileLog.LogBuffered("{");
					FileLog.ChangeIndent(1);
				}
				this.il.BeginExceptFilterBlock();
				return;
			case ExceptionBlockType.BeginFaultBlock:
				if (this.debug)
				{
					this.LogIL(System.Reflection.Emit.OpCodes.Leave, new LeaveTry(), null);
					FileLog.ChangeIndent(-1);
					FileLog.LogBuffered("} // end try");
					FileLog.LogBuffered(".fault");
					FileLog.LogBuffered("{");
					FileLog.ChangeIndent(1);
				}
				this.il.BeginFaultBlock();
				return;
			case ExceptionBlockType.BeginFinallyBlock:
				if (this.debug)
				{
					this.LogIL(System.Reflection.Emit.OpCodes.Leave, new LeaveTry(), null);
					FileLog.ChangeIndent(-1);
					FileLog.LogBuffered("} // end try");
					FileLog.LogBuffered(".finally");
					FileLog.LogBuffered("{");
					FileLog.ChangeIndent(1);
				}
				this.il.BeginFinallyBlock();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003B38 File Offset: 0x00001D38
		internal void MarkBlockAfter(ExceptionBlock block)
		{
			ExceptionBlockType blockType = block.blockType;
			if (blockType == ExceptionBlockType.EndExceptionBlock)
			{
				if (this.debug)
				{
					this.LogIL(System.Reflection.Emit.OpCodes.Leave, new LeaveTry(), null);
					FileLog.ChangeIndent(-1);
					FileLog.LogBuffered("} // end handler");
				}
				this.il.EndExceptionBlock();
				return;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003B85 File Offset: 0x00001D85
		internal void Emit(System.Reflection.Emit.OpCode opcode)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, null));
			this.LogIL(opcode);
			this.il.Emit(opcode);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003BB2 File Offset: 0x00001DB2
		internal void Emit(System.Reflection.Emit.OpCode opcode, LocalBuilder local)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, local));
			this.LogIL(opcode, local, null);
			this.il.Emit(opcode, local);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003BE2 File Offset: 0x00001DE2
		internal void Emit(System.Reflection.Emit.OpCode opcode, FieldInfo field)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, field));
			this.LogIL(opcode, field, null);
			this.il.Emit(opcode, field);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C12 File Offset: 0x00001E12
		internal void Emit(System.Reflection.Emit.OpCode opcode, Label[] labels)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, labels));
			this.LogIL(opcode, labels, null);
			this.il.Emit(opcode, labels);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003C42 File Offset: 0x00001E42
		internal void Emit(System.Reflection.Emit.OpCode opcode, Label label)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, label));
			this.LogIL(opcode, label, null);
			this.il.Emit(opcode, label);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003C7C File Offset: 0x00001E7C
		internal void Emit(System.Reflection.Emit.OpCode opcode, string str)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, str));
			this.LogIL(opcode, str, null);
			this.il.Emit(opcode, str);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003CAC File Offset: 0x00001EAC
		internal void Emit(System.Reflection.Emit.OpCode opcode, float arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003CE6 File Offset: 0x00001EE6
		internal void Emit(System.Reflection.Emit.OpCode opcode, byte arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003D20 File Offset: 0x00001F20
		internal void Emit(System.Reflection.Emit.OpCode opcode, sbyte arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003D5A File Offset: 0x00001F5A
		internal void Emit(System.Reflection.Emit.OpCode opcode, double arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003D94 File Offset: 0x00001F94
		internal void Emit(System.Reflection.Emit.OpCode opcode, int arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal void Emit(System.Reflection.Emit.OpCode opcode, MethodInfo meth)
		{
			if (opcode.Equals(System.Reflection.Emit.OpCodes.Call) || opcode.Equals(System.Reflection.Emit.OpCodes.Callvirt) || opcode.Equals(System.Reflection.Emit.OpCodes.Newobj))
			{
				this.EmitCall(opcode, meth, null);
				return;
			}
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, meth));
			this.LogIL(opcode, meth, null);
			this.il.Emit(opcode, meth);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003E3F File Offset: 0x0000203F
		internal void Emit(System.Reflection.Emit.OpCode opcode, short arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003E79 File Offset: 0x00002079
		internal void Emit(System.Reflection.Emit.OpCode opcode, SignatureHelper signature)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, signature));
			this.LogIL(opcode, signature, null);
			this.il.Emit(opcode, signature);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003EA9 File Offset: 0x000020A9
		internal void Emit(System.Reflection.Emit.OpCode opcode, ConstructorInfo con)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, con));
			this.LogIL(opcode, con, null);
			this.il.Emit(opcode, con);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003ED9 File Offset: 0x000020D9
		internal void Emit(System.Reflection.Emit.OpCode opcode, Type cls)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, cls));
			this.LogIL(opcode, cls, null);
			this.il.Emit(opcode, cls);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003F09 File Offset: 0x00002109
		internal void Emit(System.Reflection.Emit.OpCode opcode, long arg)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, arg));
			this.LogIL(opcode, arg, null);
			this.il.Emit(opcode, arg);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003F44 File Offset: 0x00002144
		internal void EmitCall(System.Reflection.Emit.OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, methodInfo));
			string extra = (optionalParameterTypes != null && optionalParameterTypes.Length != 0) ? optionalParameterTypes.Description() : null;
			this.LogIL(opcode, methodInfo, extra);
			this.il.EmitCall(opcode, methodInfo, optionalParameterTypes);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003F94 File Offset: 0x00002194
		internal void EmitCalli(System.Reflection.Emit.OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, unmanagedCallConv));
			string extra = returnType.FullName + " " + parameterTypes.Description();
			this.LogIL(opcode, unmanagedCallConv, extra);
			this.il.EmitCalli(opcode, unmanagedCallConv, returnType, parameterTypes);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003FF4 File Offset: 0x000021F4
		internal void EmitCalli(System.Reflection.Emit.OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			this.instructions.Add(this.CurrentPos(), new CodeInstruction(opcode, callingConvention));
			string extra = string.Concat(new string[]
			{
				returnType.FullName,
				" ",
				parameterTypes.Description(),
				" ",
				optionalParameterTypes.Description()
			});
			this.LogIL(opcode, callingConvention, extra);
			this.il.EmitCalli(opcode, callingConvention, returnType, parameterTypes, optionalParameterTypes);
		}

		// Token: 0x04000011 RID: 17
		private readonly CecilILGenerator il;

		// Token: 0x04000012 RID: 18
		private readonly Dictionary<int, CodeInstruction> instructions = new Dictionary<int, CodeInstruction>();

		// Token: 0x04000013 RID: 19
		private readonly bool debug;
	}
}
