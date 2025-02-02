using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000019 RID: 25
	public struct OpCode
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00005857 File Offset: 0x00003A57
		public string Name
		{
			get
			{
				return OpCodeNames.names[(int)this.Code];
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005865 File Offset: 0x00003A65
		public int Size
		{
			get
			{
				if (this.op1 != 255)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005877 File Offset: 0x00003A77
		public byte Op1
		{
			get
			{
				return this.op1;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000587F File Offset: 0x00003A7F
		public byte Op2
		{
			get
			{
				return this.op2;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005887 File Offset: 0x00003A87
		public short Value
		{
			get
			{
				if (this.op1 != 255)
				{
					return (short)((int)this.op1 << 8 | (int)this.op2);
				}
				return (short)this.op2;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000058AD File Offset: 0x00003AAD
		public Code Code
		{
			get
			{
				return (Code)this.code;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000058B5 File Offset: 0x00003AB5
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow_control;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000058BD File Offset: 0x00003ABD
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.opcode_type;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000058C5 File Offset: 0x00003AC5
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.operand_type;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000058CD File Offset: 0x00003ACD
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_pop;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000058D5 File Offset: 0x00003AD5
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.stack_behavior_push;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000058E0 File Offset: 0x00003AE0
		internal OpCode(int x, int y)
		{
			this.op1 = (byte)(x & 255);
			this.op2 = (byte)(x >> 8 & 255);
			this.code = (byte)(x >> 16 & 255);
			this.flow_control = (byte)(x >> 24 & 255);
			this.opcode_type = (byte)(y & 255);
			this.operand_type = (byte)(y >> 8 & 255);
			this.stack_behavior_pop = (byte)(y >> 16 & 255);
			this.stack_behavior_push = (byte)(y >> 24 & 255);
			if (this.op1 == 255)
			{
				OpCodes.OneByteOpCode[(int)this.op2] = this;
				return;
			}
			OpCodes.TwoBytesOpCode[(int)this.op2] = this;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000059B1 File Offset: 0x00003BB1
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000059BC File Offset: 0x00003BBC
		public override bool Equals(object obj)
		{
			if (!(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return this.op1 == opCode.op1 && this.op2 == opCode.op2;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000059FA File Offset: 0x00003BFA
		public bool Equals(OpCode opcode)
		{
			return this.op1 == opcode.op1 && this.op2 == opcode.op2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005A1C File Offset: 0x00003C1C
		public static bool operator ==(OpCode one, OpCode other)
		{
			return one.op1 == other.op1 && one.op2 == other.op2;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005A40 File Offset: 0x00003C40
		public static bool operator !=(OpCode one, OpCode other)
		{
			return one.op1 != other.op1 || one.op2 != other.op2;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005A67 File Offset: 0x00003C67
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000172 RID: 370
		private readonly byte op1;

		// Token: 0x04000173 RID: 371
		private readonly byte op2;

		// Token: 0x04000174 RID: 372
		private readonly byte code;

		// Token: 0x04000175 RID: 373
		private readonly byte flow_control;

		// Token: 0x04000176 RID: 374
		private readonly byte opcode_type;

		// Token: 0x04000177 RID: 375
		private readonly byte operand_type;

		// Token: 0x04000178 RID: 376
		private readonly byte stack_behavior_pop;

		// Token: 0x04000179 RID: 377
		private readonly byte stack_behavior_push;
	}
}
