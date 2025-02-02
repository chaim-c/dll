using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000C1 RID: 193
	internal sealed class ParameterDefinitionCollection : Collection<ParameterDefinition>
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x00019769 File Offset: 0x00017969
		internal ParameterDefinitionCollection(IMethodSignature method)
		{
			this.method = method;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00019778 File Offset: 0x00017978
		internal ParameterDefinitionCollection(IMethodSignature method, int capacity) : base(capacity)
		{
			this.method = method;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00019788 File Offset: 0x00017988
		protected override void OnAdd(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000197A0 File Offset: 0x000179A0
		protected override void OnInsert(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
			for (int i = index; i < this.size; i++)
			{
				this.items[i].index = i + 1;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000197E1 File Offset: 0x000179E1
		protected override void OnSet(ParameterDefinition item, int index)
		{
			item.method = this.method;
			item.index = index;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000197F8 File Offset: 0x000179F8
		protected override void OnRemove(ParameterDefinition item, int index)
		{
			item.method = null;
			item.index = -1;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].index = i - 1;
			}
		}

		// Token: 0x04000499 RID: 1177
		private readonly IMethodSignature method;
	}
}
