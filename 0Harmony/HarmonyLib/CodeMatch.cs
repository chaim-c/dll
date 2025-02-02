using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000049 RID: 73
	public class CodeMatch : CodeInstruction
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00010BCC File Offset: 0x0000EDCC
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00010BF2 File Offset: 0x0000EDF2
		[Obsolete("Use opcodeSet instead")]
		public List<OpCode> opcodes
		{
			get
			{
				HashSet<OpCode> hashSet = this.opcodeSet;
				List<OpCode> list = new List<OpCode>(hashSet.Count);
				list.AddRange(hashSet);
				return list;
			}
			set
			{
				this.opcodeSet = new HashSet<OpCode>(value);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010C00 File Offset: 0x0000EE00
		internal CodeMatch Set(object operand, string name)
		{
			if (this.operand == null)
			{
				this.operand = operand;
			}
			if (operand != null)
			{
				this.operands.Add(operand);
			}
			if (this.name == null)
			{
				this.name = name;
			}
			return this;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00010C30 File Offset: 0x0000EE30
		internal CodeMatch Set(OpCode opcode, object operand, string name)
		{
			this.opcode = opcode;
			this.opcodeSet.Add(opcode);
			if (this.operand == null)
			{
				this.operand = operand;
			}
			if (operand != null)
			{
				this.operands.Add(operand);
			}
			if (this.name == null)
			{
				this.name = name;
			}
			return this;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010C80 File Offset: 0x0000EE80
		public CodeMatch(OpCode? opcode = null, object operand = null, string name = null)
		{
			if (opcode != null)
			{
				OpCode valueOrDefault = opcode.GetValueOrDefault();
				this.opcode = valueOrDefault;
				this.opcodeSet.Add(valueOrDefault);
			}
			if (operand != null)
			{
				this.operands.Add(operand);
			}
			this.operand = operand;
			this.name = name;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010D04 File Offset: 0x0000EF04
		public static CodeMatch WithOpcodes(HashSet<OpCode> opcodes, object operand = null, string name = null)
		{
			return new CodeMatch(null, operand, name)
			{
				opcodeSet = opcodes
			};
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010D28 File Offset: 0x0000EF28
		public CodeMatch(Expression<Action> expression, string name = null)
		{
			this.opcodeSet.UnionWith(CodeInstructionExtensions.opcodesCalling);
			this.operand = SymbolExtensions.GetMethodInfo(expression);
			if (this.operand != null)
			{
				this.operands.Add(this.operand);
			}
			this.name = name;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		public CodeMatch(LambdaExpression expression, string name = null)
		{
			this.opcodeSet.UnionWith(CodeInstructionExtensions.opcodesCalling);
			this.operand = SymbolExtensions.GetMethodInfo(expression);
			if (this.operand != null)
			{
				this.operands.Add(this.operand);
			}
			this.name = name;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010E1F File Offset: 0x0000F01F
		public CodeMatch(CodeInstruction instruction, string name = null) : this(new OpCode?(instruction.opcode), instruction.operand, name)
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010E3C File Offset: 0x0000F03C
		public CodeMatch(Func<CodeInstruction, bool> predicate, string name = null)
		{
			this.predicate = predicate;
			this.name = name;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010E8C File Offset: 0x0000F08C
		internal bool Matches(List<CodeInstruction> codes, CodeInstruction instruction)
		{
			if (this.predicate != null)
			{
				return this.predicate(instruction);
			}
			if (this.opcodeSet.Count > 0 && !this.opcodeSet.Contains(instruction.opcode))
			{
				return false;
			}
			if (this.operands.Count > 0 && !this.operands.Contains(instruction.operand))
			{
				return false;
			}
			if (this.labels.Count > 0 && !this.labels.Intersect(instruction.labels).Any<Label>())
			{
				return false;
			}
			if (this.blocks.Count > 0 && !this.blocks.Intersect(instruction.blocks).Any<ExceptionBlock>())
			{
				return false;
			}
			if (this.jumpsFrom.Count > 0 && !(from index in this.jumpsFrom
			select codes[index].operand).OfType<Label>().Intersect(instruction.labels).Any<Label>())
			{
				return false;
			}
			if (this.jumpsTo.Count > 0)
			{
				object operand = instruction.operand;
				if (operand == null || operand.GetType() != typeof(Label))
				{
					return false;
				}
				Label label = (Label)operand;
				IEnumerable<int> second = from idx in Enumerable.Range(0, codes.Count)
				where codes[idx].labels.Contains(label)
				select idx;
				if (!this.jumpsTo.Intersect(second).Any<int>())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011018 File Offset: 0x0000F218
		public static CodeMatch IsLdarg(int? n = null)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.IsLdarg(n), null);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011044 File Offset: 0x0000F244
		public static CodeMatch IsLdarga(int? n = null)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.IsLdarga(n), null);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011070 File Offset: 0x0000F270
		public static CodeMatch IsStarg(int? n = null)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.IsStarg(n), null);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001109C File Offset: 0x0000F29C
		public static CodeMatch IsLdloc(LocalBuilder variable = null)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.IsLdloc(variable), null);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000110C8 File Offset: 0x0000F2C8
		public static CodeMatch IsStloc(LocalBuilder variable = null)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.IsStloc(variable), null);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public static CodeMatch Calls(MethodInfo method)
		{
			return CodeMatch.WithOpcodes(CodeInstructionExtensions.opcodesCalling, method, null);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00011102 File Offset: 0x0000F302
		public static CodeMatch LoadsConstant()
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsConstant(), null);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001112C File Offset: 0x0000F32C
		public static CodeMatch LoadsConstant(long number)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsConstant(number), null);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00011158 File Offset: 0x0000F358
		public static CodeMatch LoadsConstant(double number)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsConstant(number), null);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011184 File Offset: 0x0000F384
		public static CodeMatch LoadsConstant(Enum e)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsConstant(e), null);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000111B0 File Offset: 0x0000F3B0
		public static CodeMatch LoadsConstant(string str)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsConstant(str), null);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000111DC File Offset: 0x0000F3DC
		public static CodeMatch LoadsField(FieldInfo field, bool byAddress = false)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.LoadsField(field, byAddress), null);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011210 File Offset: 0x0000F410
		public static CodeMatch StoresField(FieldInfo field)
		{
			return new CodeMatch((CodeInstruction instruction) => instruction.StoresField(field), null);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001123C File Offset: 0x0000F43C
		public static CodeMatch Calls(Expression<Action> expression)
		{
			return new CodeMatch(expression, null);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00011245 File Offset: 0x0000F445
		public static CodeMatch Calls(LambdaExpression expression)
		{
			return new CodeMatch(expression, null);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0001124E File Offset: 0x0000F44E
		public static CodeMatch LoadsLocal(bool useAddress = false, string name = null)
		{
			return CodeMatch.WithOpcodes(useAddress ? CodeInstructionExtensions.opcodesLoadingLocalByAddress : CodeInstructionExtensions.opcodesLoadingLocalNormal, null, name);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00011266 File Offset: 0x0000F466
		public static CodeMatch StoresLocal(string name = null)
		{
			return CodeMatch.WithOpcodes(CodeInstructionExtensions.opcodesStoringLocal, null, name);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011274 File Offset: 0x0000F474
		public static CodeMatch LoadsArgument(bool useAddress = false, string name = null)
		{
			return CodeMatch.WithOpcodes(useAddress ? CodeInstructionExtensions.opcodesLoadingArgumentByAddress : CodeInstructionExtensions.opcodesLoadingArgumentNormal, null, name);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001128C File Offset: 0x0000F48C
		public static CodeMatch StoresArgument(string name = null)
		{
			return CodeMatch.WithOpcodes(CodeInstructionExtensions.opcodesStoringArgument, null, name);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001129A File Offset: 0x0000F49A
		public static CodeMatch Branches(string name = null)
		{
			return CodeMatch.WithOpcodes(CodeInstructionExtensions.opcodesBranching, null, name);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000112A8 File Offset: 0x0000F4A8
		public override string ToString()
		{
			string text = "[";
			if (this.name != null)
			{
				text = text + this.name + ": ";
			}
			if (this.opcodeSet.Count > 0)
			{
				text = text + "opcodes=" + this.opcodeSet.Join(null, ", ") + " ";
			}
			if (this.operands.Count > 0)
			{
				text = text + "operands=" + this.operands.Join(null, ", ") + " ";
			}
			if (this.labels.Count > 0)
			{
				text = text + "labels=" + this.labels.Join(null, ", ") + " ";
			}
			if (this.blocks.Count > 0)
			{
				text = text + "blocks=" + this.blocks.Join(null, ", ") + " ";
			}
			if (this.jumpsFrom.Count > 0)
			{
				text = text + "jumpsFrom=" + this.jumpsFrom.Join(null, ", ") + " ";
			}
			if (this.jumpsTo.Count > 0)
			{
				text = text + "jumpsTo=" + this.jumpsTo.Join(null, ", ") + " ";
			}
			if (this.predicate != null)
			{
				text += "predicate=yes ";
			}
			return text.TrimEnd(Array.Empty<char>()) + "]";
		}

		// Token: 0x040000CF RID: 207
		public string name;

		// Token: 0x040000D0 RID: 208
		public HashSet<OpCode> opcodeSet = new HashSet<OpCode>();

		// Token: 0x040000D1 RID: 209
		public List<object> operands = new List<object>();

		// Token: 0x040000D2 RID: 210
		public List<int> jumpsFrom = new List<int>();

		// Token: 0x040000D3 RID: 211
		public List<int> jumpsTo = new List<int>();

		// Token: 0x040000D4 RID: 212
		public Func<CodeInstruction, bool> predicate;
	}
}
