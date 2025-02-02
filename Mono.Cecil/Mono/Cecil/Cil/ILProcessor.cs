using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x0200000D RID: 13
	public sealed class ILProcessor
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000045A0 File Offset: 0x000027A0
		public MethodBody Body
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000045A8 File Offset: 0x000027A8
		internal ILProcessor(MethodBody body)
		{
			this.body = body;
			this.instructions = body.Instructions;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000045C3 File Offset: 0x000027C3
		public Instruction Create(OpCode opcode)
		{
			return Instruction.Create(opcode);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000045CB File Offset: 0x000027CB
		public Instruction Create(OpCode opcode, TypeReference type)
		{
			return Instruction.Create(opcode, type);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000045D4 File Offset: 0x000027D4
		public Instruction Create(OpCode opcode, CallSite site)
		{
			return Instruction.Create(opcode, site);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000045DD File Offset: 0x000027DD
		public Instruction Create(OpCode opcode, MethodReference method)
		{
			return Instruction.Create(opcode, method);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000045E6 File Offset: 0x000027E6
		public Instruction Create(OpCode opcode, FieldReference field)
		{
			return Instruction.Create(opcode, field);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000045EF File Offset: 0x000027EF
		public Instruction Create(OpCode opcode, string value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000045F8 File Offset: 0x000027F8
		public Instruction Create(OpCode opcode, sbyte value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004604 File Offset: 0x00002804
		public Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType == OperandType.ShortInlineVar)
			{
				return Instruction.Create(opcode, this.body.Variables[(int)value]);
			}
			if (opcode.OperandType == OperandType.ShortInlineArg)
			{
				return Instruction.Create(opcode, this.body.GetParameter((int)value));
			}
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000465C File Offset: 0x0000285C
		public Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType == OperandType.InlineVar)
			{
				return Instruction.Create(opcode, this.body.Variables[value]);
			}
			if (opcode.OperandType == OperandType.InlineArg)
			{
				return Instruction.Create(opcode, this.body.GetParameter(value));
			}
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000046B1 File Offset: 0x000028B1
		public Instruction Create(OpCode opcode, long value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000046BA File Offset: 0x000028BA
		public Instruction Create(OpCode opcode, float value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000046C3 File Offset: 0x000028C3
		public Instruction Create(OpCode opcode, double value)
		{
			return Instruction.Create(opcode, value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000046CC File Offset: 0x000028CC
		public Instruction Create(OpCode opcode, Instruction target)
		{
			return Instruction.Create(opcode, target);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000046D5 File Offset: 0x000028D5
		public Instruction Create(OpCode opcode, Instruction[] targets)
		{
			return Instruction.Create(opcode, targets);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000046DE File Offset: 0x000028DE
		public Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			return Instruction.Create(opcode, variable);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000046E7 File Offset: 0x000028E7
		public Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			return Instruction.Create(opcode, parameter);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000046F0 File Offset: 0x000028F0
		public void Emit(OpCode opcode)
		{
			this.Append(this.Create(opcode));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000046FF File Offset: 0x000028FF
		public void Emit(OpCode opcode, TypeReference type)
		{
			this.Append(this.Create(opcode, type));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000470F File Offset: 0x0000290F
		public void Emit(OpCode opcode, MethodReference method)
		{
			this.Append(this.Create(opcode, method));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000471F File Offset: 0x0000291F
		public void Emit(OpCode opcode, CallSite site)
		{
			this.Append(this.Create(opcode, site));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000472F File Offset: 0x0000292F
		public void Emit(OpCode opcode, FieldReference field)
		{
			this.Append(this.Create(opcode, field));
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000473F File Offset: 0x0000293F
		public void Emit(OpCode opcode, string value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000474F File Offset: 0x0000294F
		public void Emit(OpCode opcode, byte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000475F File Offset: 0x0000295F
		public void Emit(OpCode opcode, sbyte value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000476F File Offset: 0x0000296F
		public void Emit(OpCode opcode, int value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000477F File Offset: 0x0000297F
		public void Emit(OpCode opcode, long value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000478F File Offset: 0x0000298F
		public void Emit(OpCode opcode, float value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000479F File Offset: 0x0000299F
		public void Emit(OpCode opcode, double value)
		{
			this.Append(this.Create(opcode, value));
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000047AF File Offset: 0x000029AF
		public void Emit(OpCode opcode, Instruction target)
		{
			this.Append(this.Create(opcode, target));
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000047BF File Offset: 0x000029BF
		public void Emit(OpCode opcode, Instruction[] targets)
		{
			this.Append(this.Create(opcode, targets));
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000047CF File Offset: 0x000029CF
		public void Emit(OpCode opcode, VariableDefinition variable)
		{
			this.Append(this.Create(opcode, variable));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047DF File Offset: 0x000029DF
		public void Emit(OpCode opcode, ParameterDefinition parameter)
		{
			this.Append(this.Create(opcode, parameter));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000047F0 File Offset: 0x000029F0
		public void InsertBefore(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = this.instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			this.instructions.Insert(num, instruction);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004844 File Offset: 0x00002A44
		public void InsertAfter(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = this.instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			this.instructions.Insert(num + 1, instruction);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004898 File Offset: 0x00002A98
		public void Append(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instructions.Add(instruction);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000048B4 File Offset: 0x00002AB4
		public void Replace(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.InsertAfter(target, instruction);
			this.Remove(target);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000048E1 File Offset: 0x00002AE1
		public void Remove(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			if (!this.instructions.Remove(instruction))
			{
				throw new ArgumentOutOfRangeException("instruction");
			}
		}

		// Token: 0x04000115 RID: 277
		private readonly MethodBody body;

		// Token: 0x04000116 RID: 278
		private readonly Collection<Instruction> instructions;
	}
}
