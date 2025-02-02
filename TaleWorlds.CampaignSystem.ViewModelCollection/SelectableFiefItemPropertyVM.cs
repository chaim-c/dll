using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x0200001A RID: 26
	public class SelectableFiefItemPropertyVM : SelectableItemPropertyVM
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000B4AB File Offset: 0x000096AB
		public SelectableFiefItemPropertyVM(string name, string value, int changeAmount, SelectableItemPropertyVM.PropertyType type, BasicTooltipViewModel hint = null, bool isWarning = false) : base(name, value, isWarning, hint)
		{
			this.ChangeAmount = changeAmount;
			base.Type = (int)type;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000B4C8 File Offset: 0x000096C8
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000B4D0 File Offset: 0x000096D0
		[DataSourceProperty]
		public int ChangeAmount
		{
			get
			{
				return this._changeAmount;
			}
			set
			{
				if (value != this._changeAmount)
				{
					this._changeAmount = value;
					base.OnPropertyChangedWithValue(value, "ChangeAmount");
				}
			}
		}

		// Token: 0x040000B8 RID: 184
		private int _changeAmount;
	}
}
