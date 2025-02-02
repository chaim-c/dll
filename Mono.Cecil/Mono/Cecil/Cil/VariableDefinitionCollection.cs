using System;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000013 RID: 19
	internal class VariableDefinitionCollection : Collection<VariableDefinition>
	{
		// Token: 0x0600011E RID: 286 RVA: 0x000056A0 File Offset: 0x000038A0
		internal VariableDefinitionCollection()
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000056A8 File Offset: 0x000038A8
		internal VariableDefinitionCollection(int capacity) : base(capacity)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000056B1 File Offset: 0x000038B1
		protected override void OnAdd(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000056BC File Offset: 0x000038BC
		protected override void OnInsert(VariableDefinition item, int index)
		{
			item.index = index;
			for (int i = index; i < this.size; i++)
			{
				this.items[i].index = i + 1;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000056F1 File Offset: 0x000038F1
		protected override void OnSet(VariableDefinition item, int index)
		{
			item.index = index;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000056FC File Offset: 0x000038FC
		protected override void OnRemove(VariableDefinition item, int index)
		{
			item.index = -1;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].index = i - 1;
			}
		}
	}
}
