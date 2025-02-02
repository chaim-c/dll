using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000CF RID: 207
	public class EncyclopediaTraitItemVM : ViewModel
	{
		// Token: 0x060013C2 RID: 5058 RVA: 0x0004BDCC File Offset: 0x00049FCC
		public EncyclopediaTraitItemVM(TraitObject traitObj, int value)
		{
			this._traitObj = traitObj;
			this.TraitId = traitObj.StringId;
			this.Value = value;
			string traitTooltipText = CampaignUIHelper.GetTraitTooltipText(traitObj, this.Value);
			this.Hint = new HintViewModel(new TextObject("{=!}" + traitTooltipText, null), null);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004BE23 File Offset: 0x0004A023
		public EncyclopediaTraitItemVM(TraitObject traitObj, Hero hero) : this(traitObj, hero.GetTraitLevel(traitObj))
		{
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0004BE33 File Offset: 0x0004A033
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x0004BE3B File Offset: 0x0004A03B
		[DataSourceProperty]
		public string TraitId
		{
			get
			{
				return this._traitId;
			}
			set
			{
				if (value != this._traitId)
				{
					this._traitId = value;
					base.OnPropertyChangedWithValue<string>(value, "TraitId");
				}
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0004BE5E File Offset: 0x0004A05E
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x0004BE66 File Offset: 0x0004A066
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

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0004BE84 File Offset: 0x0004A084
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0004BE8C File Offset: 0x0004A08C
		[DataSourceProperty]
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (value != this._value)
				{
					this._value = value;
					base.OnPropertyChangedWithValue(value, "Value");
				}
			}
		}

		// Token: 0x04000922 RID: 2338
		private readonly TraitObject _traitObj;

		// Token: 0x04000923 RID: 2339
		private string _traitId;

		// Token: 0x04000924 RID: 2340
		private int _value;

		// Token: 0x04000925 RID: 2341
		private HintViewModel _hint;
	}
}
