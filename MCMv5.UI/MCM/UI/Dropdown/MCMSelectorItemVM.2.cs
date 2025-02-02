using System;
using System.Runtime.CompilerServices;

namespace MCM.UI.Dropdown
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	internal class MCMSelectorItemVM<T> : MCMSelectorItemVMBase where T : class
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000829D File Offset: 0x0000649D
		public T OriginalItem { get; }

		// Token: 0x0600018D RID: 397 RVA: 0x000082A5 File Offset: 0x000064A5
		public MCMSelectorItemVM(T @object)
		{
			this.OriginalItem = @object;
			this.RefreshValues();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000082BD File Offset: 0x000064BD
		[NullableContext(2)]
		public override string ToString()
		{
			return base.StringItem;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000082C5 File Offset: 0x000064C5
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._stringItem = (this.OriginalItem.ToString() ?? "ERROR");
		}
	}
}
