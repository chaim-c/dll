using System;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000135 RID: 309
	public class CharacterCreationOptionVM : StringItemWithActionVM
	{
		// Token: 0x06001DDB RID: 7643 RVA: 0x0006B011 File Offset: 0x00069211
		public CharacterCreationOptionVM(Action<object> onExecute, string item, object identifier) : base(onExecute, item, identifier)
		{
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0006B01C File Offset: 0x0006921C
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x0006B024 File Offset: 0x00069224
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
					base.ExecuteAction();
				}
			}
		}

		// Token: 0x04000E14 RID: 3604
		private bool _isSelected;
	}
}
