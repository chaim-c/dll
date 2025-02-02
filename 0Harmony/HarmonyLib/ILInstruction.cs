using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000012 RID: 18
	internal class ILInstruction
	{
		// Token: 0x0600006F RID: 111 RVA: 0x000045EC File Offset: 0x000027EC
		internal ILInstruction(OpCode opcode, object operand = null)
		{
			this.opcode = opcode;
			this.operand = operand;
			this.argument = operand;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004620 File Offset: 0x00002820
		internal CodeInstruction GetCodeInstruction()
		{
			CodeInstruction codeInstruction = new CodeInstruction(this.opcode, this.argument);
			if (this.opcode.OperandType == OperandType.InlineNone)
			{
				codeInstruction.operand = null;
			}
			codeInstruction.labels = this.labels;
			codeInstruction.blocks = this.blocks;
			return codeInstruction;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004670 File Offset: 0x00002870
		internal int GetSize()
		{
			int num = this.opcode.Size;
			switch (this.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				num += 4;
				break;
			case OperandType.InlineI8:
			case OperandType.InlineR:
				num += 8;
				break;
			case OperandType.InlineSwitch:
				num += (1 + ((Array)this.operand).Length) * 4;
				break;
			case OperandType.InlineVar:
				num += 2;
				break;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
				num++;
				break;
			}
			return num;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000471C File Offset: 0x0000291C
		public override string ToString()
		{
			string text = "";
			ILInstruction.AppendLabel(ref text, this);
			text = text + ": " + this.opcode.Name;
			if (this.operand == null)
			{
				return text;
			}
			text += " ";
			OperandType operandType = this.opcode.OperandType;
			if (operandType <= OperandType.InlineString)
			{
				if (operandType != OperandType.InlineBrTarget)
				{
					if (operandType != OperandType.InlineString)
					{
						goto IL_EC;
					}
					string str = text;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					defaultInterpolatedStringHandler.AppendFormatted<object>(this.operand);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					return str + defaultInterpolatedStringHandler.ToStringAndClear();
				}
			}
			else
			{
				if (operandType == OperandType.InlineSwitch)
				{
					ILInstruction[] array = (ILInstruction[])this.operand;
					for (int i = 0; i < array.Length; i++)
					{
						if (i > 0)
						{
							text += ",";
						}
						ILInstruction.AppendLabel(ref text, array[i]);
					}
					return text;
				}
				if (operandType != OperandType.ShortInlineBrTarget)
				{
					goto IL_EC;
				}
			}
			ILInstruction.AppendLabel(ref text, this.operand);
			return text;
			IL_EC:
			string str2 = text;
			object obj = this.operand;
			text = str2 + ((obj != null) ? obj.ToString() : null);
			return text;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004830 File Offset: 0x00002A30
		private static void AppendLabel(ref string str, object argument)
		{
			ILInstruction ilinstruction = argument as ILInstruction;
			string str2 = str;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
			defaultInterpolatedStringHandler.AppendLiteral("IL_");
			defaultInterpolatedStringHandler.AppendFormatted<object>(((ilinstruction != null) ? ilinstruction.offset.ToString("X4") : null) ?? argument);
			str = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x0400001B RID: 27
		internal int offset;

		// Token: 0x0400001C RID: 28
		internal OpCode opcode;

		// Token: 0x0400001D RID: 29
		internal object operand;

		// Token: 0x0400001E RID: 30
		internal object argument;

		// Token: 0x0400001F RID: 31
		internal List<Label> labels = new List<Label>();

		// Token: 0x04000020 RID: 32
		internal List<ExceptionBlock> blocks = new List<ExceptionBlock>();
	}
}
