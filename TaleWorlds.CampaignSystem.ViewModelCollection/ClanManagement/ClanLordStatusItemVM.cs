using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement
{
	// Token: 0x02000109 RID: 265
	public class ClanLordStatusItemVM : ViewModel
	{
		// Token: 0x060018D4 RID: 6356 RVA: 0x0005ABF8 File Offset: 0x00058DF8
		public ClanLordStatusItemVM(ClanLordStatusItemVM.LordStatus status, TextObject hintText)
		{
			this.Type = (int)status;
			this.Hint = new HintViewModel(hintText, null);
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x060018D5 RID: 6357 RVA: 0x0005AC1B File Offset: 0x00058E1B
		// (set) Token: 0x060018D6 RID: 6358 RVA: 0x0005AC23 File Offset: 0x00058E23
		[DataSourceProperty]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0005AC41 File Offset: 0x00058E41
		// (set) Token: 0x060018D8 RID: 6360 RVA: 0x0005AC49 File Offset: 0x00058E49
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x04000BBF RID: 3007
		private int _type = -1;

		// Token: 0x04000BC0 RID: 3008
		private HintViewModel _hint;

		// Token: 0x02000241 RID: 577
		public enum LordStatus
		{
			// Token: 0x04001146 RID: 4422
			Dead,
			// Token: 0x04001147 RID: 4423
			Married,
			// Token: 0x04001148 RID: 4424
			Pregnant,
			// Token: 0x04001149 RID: 4425
			InBattle,
			// Token: 0x0400114A RID: 4426
			InSiege,
			// Token: 0x0400114B RID: 4427
			Child,
			// Token: 0x0400114C RID: 4428
			Prisoner,
			// Token: 0x0400114D RID: 4429
			Sick
		}
	}
}
