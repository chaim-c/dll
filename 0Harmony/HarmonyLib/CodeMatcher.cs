using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x0200004A RID: 74
	public class CodeMatcher
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0001141E File Offset: 0x0000F61E
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00011426 File Offset: 0x0000F626
		public int Pos { get; private set; } = -1;

		// Token: 0x0600034D RID: 845 RVA: 0x0001142F File Offset: 0x0000F62F
		private void FixStart()
		{
			this.Pos = Math.Max(0, this.Pos);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00011443 File Offset: 0x0000F643
		private void SetOutOfBounds(int direction)
		{
			this.Pos = ((direction > 0) ? this.Length : -1);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00011458 File Offset: 0x0000F658
		public int Length
		{
			get
			{
				return this.codes.Count;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00011465 File Offset: 0x0000F665
		public bool IsValid
		{
			get
			{
				return this.Pos >= 0 && this.Pos < this.Length;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00011480 File Offset: 0x0000F680
		public bool IsInvalid
		{
			get
			{
				return this.Pos < 0 || this.Pos >= this.Length;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0001149E File Offset: 0x0000F69E
		public int Remaining
		{
			get
			{
				return this.Length - Math.Max(0, this.Pos);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000353 RID: 851 RVA: 0x000114B3 File Offset: 0x0000F6B3
		public ref OpCode Opcode
		{
			get
			{
				return ref this.codes[this.Pos].opcode;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000114CB File Offset: 0x0000F6CB
		public ref object Operand
		{
			get
			{
				return ref this.codes[this.Pos].operand;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000114E3 File Offset: 0x0000F6E3
		public ref List<Label> Labels
		{
			get
			{
				return ref this.codes[this.Pos].labels;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000114FB File Offset: 0x0000F6FB
		public ref List<ExceptionBlock> Blocks
		{
			get
			{
				return ref this.codes[this.Pos].blocks;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011513 File Offset: 0x0000F713
		public CodeMatcher()
		{
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011538 File Offset: 0x0000F738
		public CodeMatcher(IEnumerable<CodeInstruction> instructions, ILGenerator generator = null)
		{
			this.generator = generator;
			this.codes = (from c in instructions
			select new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000115A0 File Offset: 0x0000F7A0
		public CodeMatcher Clone()
		{
			return new CodeMatcher(this.codes, this.generator)
			{
				Pos = this.Pos,
				lastMatches = this.lastMatches,
				lastError = this.lastError,
				lastMatchCall = this.lastMatchCall
			};
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600035A RID: 858 RVA: 0x000115EE File Offset: 0x0000F7EE
		public CodeInstruction Instruction
		{
			get
			{
				return this.codes[this.Pos];
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011601 File Offset: 0x0000F801
		public CodeInstruction InstructionAt(int offset)
		{
			return this.codes[this.Pos + offset];
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011616 File Offset: 0x0000F816
		public List<CodeInstruction> Instructions()
		{
			return this.codes;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001161E File Offset: 0x0000F81E
		public IEnumerable<CodeInstruction> InstructionEnumeration()
		{
			return this.codes.AsEnumerable<CodeInstruction>();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001162B File Offset: 0x0000F82B
		public List<CodeInstruction> Instructions(int count)
		{
			return (from c in this.codes.GetRange(this.Pos, count)
			select new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011668 File Offset: 0x0000F868
		public List<CodeInstruction> InstructionsInRange(int start, int end)
		{
			List<CodeInstruction> range = this.codes;
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			range = range.GetRange(start, end - start + 1);
			return (from c in range
			select new CodeInstruction(c)).ToList<CodeInstruction>();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000116BD File Offset: 0x0000F8BD
		public List<CodeInstruction> InstructionsWithOffsets(int startOffset, int endOffset)
		{
			return this.InstructionsInRange(this.Pos + startOffset, this.Pos + endOffset);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000116D5 File Offset: 0x0000F8D5
		public List<Label> DistinctLabels(IEnumerable<CodeInstruction> instructions)
		{
			return instructions.SelectMany((CodeInstruction instruction) => instruction.labels).Distinct<Label>().ToList<Label>();
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011708 File Offset: 0x0000F908
		public bool ReportFailure(MethodBase method, Action<string> logger)
		{
			if (this.IsValid)
			{
				return false;
			}
			string value = this.lastError ?? "Unexpected code";
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
			defaultInterpolatedStringHandler.AppendFormatted(value);
			defaultInterpolatedStringHandler.AppendLiteral(" in ");
			defaultInterpolatedStringHandler.AppendFormatted<MethodBase>(method);
			logger(defaultInterpolatedStringHandler.ToStringAndClear());
			return true;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011762 File Offset: 0x0000F962
		public CodeMatcher ThrowIfInvalid(string explanation)
		{
			if (explanation == null)
			{
				throw new ArgumentNullException("explanation");
			}
			if (this.IsInvalid)
			{
				throw new InvalidOperationException(explanation + " - Current state is invalid");
			}
			return this;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001178C File Offset: 0x0000F98C
		public CodeMatcher ThrowIfNotMatch(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfInvalid(explanation);
			if (!this.MatchSequence(this.Pos, matches))
			{
				throw new InvalidOperationException(explanation + " - Match failed");
			}
			return this;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private void ThrowIfNotMatch(string explanation, int direction, CodeMatch[] matches)
		{
			this.ThrowIfInvalid(explanation);
			int pos = this.Pos;
			try
			{
				if (this.Match(matches, direction, false).IsInvalid)
				{
					throw new InvalidOperationException(explanation + " - Match failed");
				}
			}
			finally
			{
				this.Pos = pos;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011810 File Offset: 0x0000FA10
		public CodeMatcher ThrowIfNotMatchForward(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfNotMatch(explanation, 1, matches);
			return this;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001181C File Offset: 0x0000FA1C
		public CodeMatcher ThrowIfNotMatchBack(string explanation, params CodeMatch[] matches)
		{
			this.ThrowIfNotMatch(explanation, -1, matches);
			return this;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011828 File Offset: 0x0000FA28
		public CodeMatcher ThrowIfFalse(string explanation, Func<CodeMatcher, bool> stateCheckFunc)
		{
			if (stateCheckFunc == null)
			{
				throw new ArgumentNullException("stateCheckFunc");
			}
			this.ThrowIfInvalid(explanation);
			if (!stateCheckFunc(this))
			{
				throw new InvalidOperationException(explanation + " - Check function returned false");
			}
			return this;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001185B File Offset: 0x0000FA5B
		public CodeMatcher SetInstruction(CodeInstruction instruction)
		{
			this.codes[this.Pos] = instruction;
			return this;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011870 File Offset: 0x0000FA70
		public CodeMatcher SetInstructionAndAdvance(CodeInstruction instruction)
		{
			this.SetInstruction(instruction);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011896 File Offset: 0x0000FA96
		public unsafe CodeMatcher Set(OpCode opcode, object operand)
		{
			*this.Opcode = opcode;
			*this.Operand = operand;
			return this;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public CodeMatcher SetAndAdvance(OpCode opcode, object operand)
		{
			this.Set(opcode, operand);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000118D8 File Offset: 0x0000FAD8
		public unsafe CodeMatcher SetOpcodeAndAdvance(OpCode opcode)
		{
			*this.Opcode = opcode;
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011904 File Offset: 0x0000FB04
		public unsafe CodeMatcher SetOperandAndAdvance(object operand)
		{
			*this.Operand = operand;
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001192A File Offset: 0x0000FB2A
		public CodeMatcher DeclareLocal(Type variableType, out LocalBuilder localVariable)
		{
			localVariable = this.generator.DeclareLocal(variableType);
			return this;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001193B File Offset: 0x0000FB3B
		public CodeMatcher DefineLabel(out Label label)
		{
			label = this.generator.DefineLabel();
			return this;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001194F File Offset: 0x0000FB4F
		public unsafe CodeMatcher CreateLabel(out Label label)
		{
			label = this.generator.DefineLabel();
			this.Labels->Add(label);
			return this;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00011975 File Offset: 0x0000FB75
		public CodeMatcher CreateLabelAt(int position, out Label label)
		{
			label = this.generator.DefineLabel();
			this.AddLabelsAt(position, new Label[]
			{
				label
			});
			return this;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public CodeMatcher CreateLabelWithOffsets(int offset, out Label label)
		{
			label = this.generator.DefineLabel();
			return this.AddLabelsAt(this.Pos + offset, new Label[]
			{
				label
			});
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public unsafe CodeMatcher AddLabels(IEnumerable<Label> labels)
		{
			this.Labels->AddRange(labels);
			return this;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public CodeMatcher AddLabelsAt(int position, IEnumerable<Label> labels)
		{
			this.codes[position].labels.AddRange(labels);
			return this;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011A02 File Offset: 0x0000FC02
		public CodeMatcher SetJumpTo(OpCode opcode, int destination, out Label label)
		{
			this.CreateLabelAt(destination, out label);
			return this.Set(opcode, label);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011A1F File Offset: 0x0000FC1F
		public CodeMatcher Insert(params CodeInstruction[] instructions)
		{
			this.codes.InsertRange(this.Pos, instructions);
			return this;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011A34 File Offset: 0x0000FC34
		public CodeMatcher Insert(IEnumerable<CodeInstruction> instructions)
		{
			this.codes.InsertRange(this.Pos, instructions);
			return this;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011A4C File Offset: 0x0000FC4C
		public CodeMatcher InsertBranch(OpCode opcode, int destination)
		{
			Label label;
			this.CreateLabelAt(destination, out label);
			this.codes.Insert(this.Pos, new CodeInstruction(opcode, label));
			return this;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011A84 File Offset: 0x0000FC84
		public CodeMatcher InsertAndAdvance(params CodeInstruction[] instructions)
		{
			foreach (CodeInstruction codeInstruction in instructions)
			{
				this.Insert(new CodeInstruction[]
				{
					codeInstruction
				});
				int pos = this.Pos;
				this.Pos = pos + 1;
			}
			return this;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		public CodeMatcher InsertAndAdvance(IEnumerable<CodeInstruction> instructions)
		{
			foreach (CodeInstruction codeInstruction in instructions)
			{
				this.InsertAndAdvance(new CodeInstruction[]
				{
					codeInstruction
				});
			}
			return this;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00011B1C File Offset: 0x0000FD1C
		public CodeMatcher InsertBranchAndAdvance(OpCode opcode, int destination)
		{
			this.InsertBranch(opcode, destination);
			int pos = this.Pos;
			this.Pos = pos + 1;
			return this;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011B43 File Offset: 0x0000FD43
		public CodeMatcher RemoveInstruction()
		{
			this.codes.RemoveAt(this.Pos);
			return this;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011B57 File Offset: 0x0000FD57
		public CodeMatcher RemoveInstructions(int count)
		{
			this.codes.RemoveRange(this.Pos, count);
			return this;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public CodeMatcher RemoveInstructionsInRange(int start, int end)
		{
			if (start > end)
			{
				int num = start;
				start = end;
				end = num;
			}
			this.codes.RemoveRange(start, end - start + 1);
			return this;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011B8A File Offset: 0x0000FD8A
		public CodeMatcher RemoveInstructionsWithOffsets(int startOffset, int endOffset)
		{
			return this.RemoveInstructionsInRange(this.Pos + startOffset, this.Pos + endOffset);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011BA2 File Offset: 0x0000FDA2
		public CodeMatcher Advance(int offset)
		{
			this.Pos += offset;
			if (!this.IsValid)
			{
				this.SetOutOfBounds(offset);
			}
			return this;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011BC2 File Offset: 0x0000FDC2
		public CodeMatcher Start()
		{
			this.Pos = 0;
			return this;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011BCC File Offset: 0x0000FDCC
		public CodeMatcher End()
		{
			this.Pos = this.Length - 1;
			return this;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011BDD File Offset: 0x0000FDDD
		public CodeMatcher SearchForward(Func<CodeInstruction, bool> predicate)
		{
			return this.Search(predicate, 1);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00011BE7 File Offset: 0x0000FDE7
		public CodeMatcher SearchBackwards(Func<CodeInstruction, bool> predicate)
		{
			return this.Search(predicate, -1);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00011BF4 File Offset: 0x0000FDF4
		private CodeMatcher Search(Func<CodeInstruction, bool> predicate, int direction)
		{
			this.FixStart();
			while (this.IsValid && !predicate(this.Instruction))
			{
				this.Pos += direction;
			}
			string text;
			if (!this.IsInvalid)
			{
				text = null;
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Cannot find ");
				defaultInterpolatedStringHandler.AppendFormatted<Func<CodeInstruction, bool>>(predicate);
				text = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			this.lastError = text;
			return this;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011C64 File Offset: 0x0000FE64
		public CodeMatcher MatchStartForward(params CodeMatch[] matches)
		{
			return this.Match(matches, 1, false);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011C6F File Offset: 0x0000FE6F
		public CodeMatcher MatchEndForward(params CodeMatch[] matches)
		{
			return this.Match(matches, 1, true);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011C7A File Offset: 0x0000FE7A
		public CodeMatcher MatchStartBackwards(params CodeMatch[] matches)
		{
			return this.Match(matches, -1, false);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011C85 File Offset: 0x0000FE85
		public CodeMatcher MatchEndBackwards(params CodeMatch[] matches)
		{
			return this.Match(matches, -1, true);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00011C90 File Offset: 0x0000FE90
		private CodeMatcher Match(CodeMatch[] matches, int direction, bool useEnd)
		{
			this.lastMatchCall = delegate()
			{
				this.FixStart();
				while (this.IsValid)
				{
					if (this.MatchSequence(this.Pos, matches))
					{
						if (useEnd)
						{
							this.Pos += matches.Length - 1;
							break;
						}
						break;
					}
					else
					{
						this.Pos += direction;
					}
				}
				this.lastError = (this.IsInvalid ? ("Cannot find " + matches.Join(null, ", ")) : null);
				return this;
			};
			return this.lastMatchCall();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011CDC File Offset: 0x0000FEDC
		public CodeMatcher Repeat(Action<CodeMatcher> matchAction, Action<string> notFoundAction = null)
		{
			int num = 0;
			if (this.lastMatchCall == null)
			{
				throw new InvalidOperationException("No previous Match operation - cannot repeat");
			}
			while (this.IsValid)
			{
				matchAction(this);
				this.lastMatchCall();
				num++;
			}
			this.lastMatchCall = null;
			if (num == 0 && notFoundAction != null)
			{
				notFoundAction(this.lastError);
			}
			return this;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011D37 File Offset: 0x0000FF37
		public CodeInstruction NamedMatch(string name)
		{
			return this.lastMatches[name];
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00011D48 File Offset: 0x0000FF48
		private bool MatchSequence(int start, CodeMatch[] matches)
		{
			if (start < 0)
			{
				return false;
			}
			this.lastMatches = new Dictionary<string, CodeInstruction>();
			foreach (CodeMatch codeMatch in matches)
			{
				if (start >= this.Length || !codeMatch.Matches(this.codes, this.codes[start]))
				{
					return false;
				}
				if (codeMatch.name != null)
				{
					this.lastMatches.Add(codeMatch.name, this.codes[start]);
				}
				start++;
			}
			return true;
		}

		// Token: 0x040000D5 RID: 213
		private readonly ILGenerator generator;

		// Token: 0x040000D6 RID: 214
		private readonly List<CodeInstruction> codes = new List<CodeInstruction>();

		// Token: 0x040000D8 RID: 216
		private Dictionary<string, CodeInstruction> lastMatches = new Dictionary<string, CodeInstruction>();

		// Token: 0x040000D9 RID: 217
		private string lastError;

		// Token: 0x040000DA RID: 218
		private CodeMatcher.MatchDelegate lastMatchCall;

		// Token: 0x020001A8 RID: 424
		// (Invoke) Token: 0x06000743 RID: 1859
		private delegate CodeMatcher MatchDelegate();
	}
}
