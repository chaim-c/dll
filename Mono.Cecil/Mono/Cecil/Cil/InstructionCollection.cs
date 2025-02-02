using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000014 RID: 20
	internal class InstructionCollection : Collection<Instruction>
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00005733 File Offset: 0x00003933
		internal InstructionCollection()
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000573B File Offset: 0x0000393B
		internal InstructionCollection(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005744 File Offset: 0x00003944
		protected override void OnAdd(Instruction item, int index)
		{
			if (index == 0)
			{
				return;
			}
			Instruction instruction = this.items[index - 1];
			instruction.next = item;
			item.previous = instruction;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005770 File Offset: 0x00003970
		protected override void OnInsert(Instruction item, int index)
		{
			if (this.size == 0)
			{
				return;
			}
			Instruction instruction = this.items[index];
			if (instruction == null)
			{
				Instruction instruction2 = this.items[index - 1];
				instruction2.next = item;
				item.previous = instruction2;
				return;
			}
			Instruction previous = instruction.previous;
			if (previous != null)
			{
				previous.next = item;
				item.previous = previous;
			}
			instruction.previous = item;
			item.next = instruction;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000057D4 File Offset: 0x000039D4
		protected override void OnSet(Instruction item, int index)
		{
			Instruction instruction = this.items[index];
			item.previous = instruction.previous;
			item.next = instruction.next;
			instruction.previous = null;
			instruction.next = null;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005810 File Offset: 0x00003A10
		protected override void OnRemove(Instruction item, int index)
		{
			Instruction previous = item.previous;
			if (previous != null)
			{
				previous.next = item.next;
			}
			Instruction next = item.next;
			if (next != null)
			{
				next.previous = item.previous;
			}
			item.previous = null;
			item.next = null;
		}
	}
}
