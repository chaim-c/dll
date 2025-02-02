using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020000AC RID: 172
	internal sealed class GenericParameterCollection : Collection<GenericParameter>
	{
		// Token: 0x06000664 RID: 1636 RVA: 0x00018635 File Offset: 0x00016835
		internal GenericParameterCollection(IGenericParameterProvider owner)
		{
			this.owner = owner;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00018644 File Offset: 0x00016844
		internal GenericParameterCollection(IGenericParameterProvider owner, int capacity) : base(capacity)
		{
			this.owner = owner;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00018654 File Offset: 0x00016854
		protected override void OnAdd(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00018660 File Offset: 0x00016860
		protected override void OnInsert(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
			for (int i = index; i < this.size; i++)
			{
				this.items[i].position = i + 1;
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00018696 File Offset: 0x00016896
		protected override void OnSet(GenericParameter item, int index)
		{
			this.UpdateGenericParameter(item, index);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000186A0 File Offset: 0x000168A0
		private void UpdateGenericParameter(GenericParameter item, int index)
		{
			item.owner = this.owner;
			item.position = index;
			item.type = this.owner.GenericParameterType;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000186C8 File Offset: 0x000168C8
		protected override void OnRemove(GenericParameter item, int index)
		{
			item.owner = null;
			item.position = -1;
			item.type = GenericParameterType.Type;
			for (int i = index + 1; i < this.size; i++)
			{
				this.items[i].position = i - 1;
			}
		}

		// Token: 0x0400043A RID: 1082
		private readonly IGenericParameterProvider owner;
	}
}
