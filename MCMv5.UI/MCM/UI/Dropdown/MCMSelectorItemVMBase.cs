using System;
using System.Runtime.CompilerServices;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace MCM.UI.Dropdown
{
	// Token: 0x0200002D RID: 45
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class MCMSelectorItemVMBase : ViewModel
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000082EE File Offset: 0x000064EE
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000082F6 File Offset: 0x000064F6
		[DataSourceProperty]
		public bool CanBeSelected
		{
			get
			{
				return this._canBeSelected;
			}
			set
			{
				base.SetField<bool>(ref this._canBeSelected, value, "CanBeSelected");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000830B File Offset: 0x0000650B
		// (set) Token: 0x06000193 RID: 403 RVA: 0x00008313 File Offset: 0x00006513
		[DataSourceProperty]
		public string StringItem
		{
			get
			{
				return this._stringItem;
			}
			set
			{
				base.SetField<string>(ref this._stringItem, value, "StringItem");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008328 File Offset: 0x00006528
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00008330 File Offset: 0x00006530
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				base.SetField<HintViewModel>(ref this._hint, value, "Hint");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00008345 File Offset: 0x00006545
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000834D File Offset: 0x0000654D
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				base.SetField<bool>(ref this._isSelected, value, "IsSelected");
			}
		}

		// Token: 0x0400006B RID: 107
		protected string _stringItem;

		// Token: 0x0400006C RID: 108
		protected bool _canBeSelected = true;

		// Token: 0x0400006D RID: 109
		protected HintViewModel _hint;

		// Token: 0x0400006E RID: 110
		protected bool _isSelected;
	}
}
