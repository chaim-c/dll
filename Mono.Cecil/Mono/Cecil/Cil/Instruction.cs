using System;
using System.Text;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200000E RID: 14
	public sealed class Instruction
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000490A File Offset: 0x00002B0A
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004912 File Offset: 0x00002B12
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000491B File Offset: 0x00002B1B
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004923 File Offset: 0x00002B23
		public OpCode OpCode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000492C File Offset: 0x00002B2C
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004934 File Offset: 0x00002B34
		public object Operand
		{
			get
			{
				return this.operand;
			}
			set
			{
				this.operand = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000493D File Offset: 0x00002B3D
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00004945 File Offset: 0x00002B45
		public Instruction Previous
		{
			get
			{
				return this.previous;
			}
			set
			{
				this.previous = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000494E File Offset: 0x00002B4E
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004956 File Offset: 0x00002B56
		public Instruction Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000495F File Offset: 0x00002B5F
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004967 File Offset: 0x00002B67
		public SequencePoint SequencePoint
		{
			get
			{
				return this.sequence_point;
			}
			set
			{
				this.sequence_point = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004970 File Offset: 0x00002B70
		internal Instruction(int offset, OpCode opCode)
		{
			this.offset = offset;
			this.opcode = opCode;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004986 File Offset: 0x00002B86
		internal Instruction(OpCode opcode, object operand)
		{
			this.opcode = opcode;
			this.operand = operand;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000499C File Offset: 0x00002B9C
		public int GetSize()
		{
			int size = this.opcode.Size;
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
				return size + 4;
			case OperandType.InlineI8:
			case OperandType.InlineR:
				return size + 8;
			case OperandType.InlineSwitch:
				return size + (1 + ((Instruction[])this.operand).Length) * 4;
			case OperandType.InlineVar:
			case OperandType.InlineArg:
				return size + 2;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
			case OperandType.ShortInlineArg:
				return size + 1;
			}
			return size;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004A40 File Offset: 0x00002C40
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Instruction.AppendLabel(stringBuilder, this);
			stringBuilder.Append(':');
			stringBuilder.Append(' ');
			stringBuilder.Append(this.opcode.Name);
			if (this.operand == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append(' ');
			OperandType operandType = this.opcode.OperandType;
			if (operandType != OperandType.InlineBrTarget)
			{
				switch (operandType)
				{
				case OperandType.InlineString:
					stringBuilder.Append('"');
					stringBuilder.Append(this.operand);
					stringBuilder.Append('"');
					goto IL_E2;
				case OperandType.InlineSwitch:
				{
					Instruction[] array = (Instruction[])this.operand;
					for (int i = 0; i < array.Length; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(',');
						}
						Instruction.AppendLabel(stringBuilder, array[i]);
					}
					goto IL_E2;
				}
				default:
					if (operandType != OperandType.ShortInlineBrTarget)
					{
						stringBuilder.Append(this.operand);
						goto IL_E2;
					}
					break;
				}
			}
			Instruction.AppendLabel(stringBuilder, (Instruction)this.operand);
			IL_E2:
			return stringBuilder.ToString();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004B35 File Offset: 0x00002D35
		private static void AppendLabel(StringBuilder builder, Instruction instruction)
		{
			builder.Append("IL_");
			builder.Append(instruction.offset.ToString("x4"));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004B5A File Offset: 0x00002D5A
		public static Instruction Create(OpCode opcode)
		{
			if (opcode.OperandType != OperandType.InlineNone)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, null);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004B78 File Offset: 0x00002D78
		public static Instruction Create(OpCode opcode, TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (opcode.OperandType != OperandType.InlineType && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, type);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004BB0 File Offset: 0x00002DB0
		public static Instruction Create(OpCode opcode, CallSite site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			if (opcode.Code != Code.Calli)
			{
				throw new ArgumentException("code");
			}
			return new Instruction(opcode, site);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004BDD File Offset: 0x00002DDD
		public static Instruction Create(OpCode opcode, MethodReference method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (opcode.OperandType != OperandType.InlineMethod && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, method);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004C14 File Offset: 0x00002E14
		public static Instruction Create(OpCode opcode, FieldReference field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (opcode.OperandType != OperandType.InlineField && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, field);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004C4B File Offset: 0x00002E4B
		public static Instruction Create(OpCode opcode, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (opcode.OperandType != OperandType.InlineString)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004C78 File Offset: 0x00002E78
		public static Instruction Create(OpCode opcode, sbyte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI && opcode != OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public static Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI || opcode == OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004CDA File Offset: 0x00002EDA
		public static Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType != OperandType.InlineI)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004CFD File Offset: 0x00002EFD
		public static Instruction Create(OpCode opcode, long value)
		{
			if (opcode.OperandType != OperandType.InlineI8)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004D20 File Offset: 0x00002F20
		public static Instruction Create(OpCode opcode, float value)
		{
			if (opcode.OperandType != OperandType.ShortInlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004D44 File Offset: 0x00002F44
		public static Instruction Create(OpCode opcode, double value)
		{
			if (opcode.OperandType != OperandType.InlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004D67 File Offset: 0x00002F67
		public static Instruction Create(OpCode opcode, Instruction target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (opcode.OperandType != OperandType.InlineBrTarget && opcode.OperandType != OperandType.ShortInlineBrTarget)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, target);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004D9D File Offset: 0x00002F9D
		public static Instruction Create(OpCode opcode, Instruction[] targets)
		{
			if (targets == null)
			{
				throw new ArgumentNullException("targets");
			}
			if (opcode.OperandType != OperandType.InlineSwitch)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, targets);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004DCA File Offset: 0x00002FCA
		public static Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (opcode.OperandType != OperandType.ShortInlineVar && opcode.OperandType != OperandType.InlineVar)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, variable);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004E02 File Offset: 0x00003002
		public static Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			if (opcode.OperandType != OperandType.ShortInlineArg && opcode.OperandType != OperandType.InlineArg)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, parameter);
		}

		// Token: 0x04000117 RID: 279
		internal int offset;

		// Token: 0x04000118 RID: 280
		internal OpCode opcode;

		// Token: 0x04000119 RID: 281
		internal object operand;

		// Token: 0x0400011A RID: 282
		internal Instruction previous;

		// Token: 0x0400011B RID: 283
		internal Instruction next;

		// Token: 0x0400011C RID: 284
		private SequencePoint sequence_point;
	}
}
