using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement.Supporters
{
	// Token: 0x02000111 RID: 273
	public class ClanSupporterGroupVM : ViewModel
	{
		// Token: 0x06001A60 RID: 6752 RVA: 0x0005F64B File Offset: 0x0005D84B
		public ClanSupporterGroupVM(TextObject groupName, float influenceBonus, Action<ClanSupporterGroupVM> onSelection)
		{
			this._groupNameText = groupName;
			this._influenceBonus = influenceBonus;
			this._onSelection = onSelection;
			this.Supporters = new MBBindingList<ClanSupporterItemVM>();
			this.RefreshValues();
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0005F679 File Offset: 0x0005D879
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Refresh();
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0005F688 File Offset: 0x0005D888
		public void AddSupporter(Hero hero)
		{
			if (!this.Supporters.Any((ClanSupporterItemVM x) => x.Hero.Hero == hero))
			{
				this.Supporters.Add(new ClanSupporterItemVM(hero));
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0005F6D4 File Offset: 0x0005D8D4
		public void Refresh()
		{
			TextObject textObject = GameTexts.FindText("str_amount_with_influence_icon", null);
			this.TotalInfluenceBonus = (float)this.Supporters.Count * this._influenceBonus;
			TextObject textObject2 = GameTexts.FindText("str_plus_with_number", null);
			textObject2.SetTextVariable("NUMBER", this.TotalInfluenceBonus.ToString("F2"));
			textObject.SetTextVariable("AMOUNT", textObject2.ToString());
			textObject.SetTextVariable("INFLUENCE_ICON", "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
			this.TotalInfluence = textObject.ToString();
			TextObject textObject3 = GameTexts.FindText("str_RANK_with_NUM_between_parenthesis", null);
			textObject3.SetTextVariable("RANK", this._groupNameText.ToString());
			textObject3.SetTextVariable("NUMBER", this.Supporters.Count);
			this.Name = textObject3.ToString();
			TextObject textObject4 = new TextObject("{=cZCOa00c}{SUPPORTER_RANK} Supporters ({NUM})", null);
			textObject4.SetTextVariable("SUPPORTER_RANK", this._groupNameText.ToString());
			textObject4.SetTextVariable("NUM", this.Supporters.Count);
			this.TitleText = textObject4.ToString();
			TextObject textObject5 = new TextObject("{=jdbT6nc9}Each {SUPPORTER_RANK} supporter provides {INFLUENCE_BONUS} per day.", null);
			textObject5.SetTextVariable("SUPPORTER_RANK", this._groupNameText.ToString());
			textObject5.SetTextVariable("INFLUENCE_BONUS", this._influenceBonus.ToString("F2") + "{=!}<img src=\"General\\Icons\\Influence@2x\" extend=\"7\">");
			this.InfluenceBonusDescription = textObject5.ToString();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0005F846 File Offset: 0x0005DA46
		public void ExecuteSelect()
		{
			Action<ClanSupporterGroupVM> onSelection = this._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0005F859 File Offset: 0x0005DA59
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x0005F861 File Offset: 0x0005DA61
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0005F884 File Offset: 0x0005DA84
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x0005F88C File Offset: 0x0005DA8C
		[DataSourceProperty]
		public float TotalInfluenceBonus
		{
			get
			{
				return this._totalInfluenceBonus;
			}
			private set
			{
				if (value != this._totalInfluenceBonus)
				{
					this._totalInfluenceBonus = value;
					base.OnPropertyChangedWithValue(value, "TotalInfluenceBonus");
				}
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0005F8AA File Offset: 0x0005DAAA
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x0005F8B2 File Offset: 0x0005DAB2
		[DataSourceProperty]
		public string InfluenceBonusDescription
		{
			get
			{
				return this._influenceBonusDescription;
			}
			set
			{
				if (value != this._influenceBonusDescription)
				{
					this._influenceBonusDescription = value;
					base.OnPropertyChangedWithValue<string>(value, "InfluenceBonusDescription");
				}
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0005F8D5 File Offset: 0x0005DAD5
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x0005F8DD File Offset: 0x0005DADD
		[DataSourceProperty]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value != this._name)
				{
					this._name = value;
					base.OnPropertyChangedWithValue<string>(value, "Name");
				}
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0005F900 File Offset: 0x0005DB00
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x0005F908 File Offset: 0x0005DB08
		[DataSourceProperty]
		public string TotalInfluence
		{
			get
			{
				return this._totalInfluence;
			}
			set
			{
				if (value != this._totalInfluence)
				{
					this._totalInfluence = value;
					base.OnPropertyChangedWithValue<string>(value, "TotalInfluence");
				}
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0005F92B File Offset: 0x0005DB2B
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x0005F933 File Offset: 0x0005DB33
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
				}
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0005F951 File Offset: 0x0005DB51
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x0005F959 File Offset: 0x0005DB59
		[DataSourceProperty]
		public MBBindingList<ClanSupporterItemVM> Supporters
		{
			get
			{
				return this._supporters;
			}
			set
			{
				if (value != this._supporters)
				{
					this._supporters = value;
					base.OnPropertyChangedWithValue<MBBindingList<ClanSupporterItemVM>>(value, "Supporters");
				}
			}
		}

		// Token: 0x04000C77 RID: 3191
		private TextObject _groupNameText;

		// Token: 0x04000C78 RID: 3192
		private float _influenceBonus;

		// Token: 0x04000C79 RID: 3193
		private Action<ClanSupporterGroupVM> _onSelection;

		// Token: 0x04000C7A RID: 3194
		private string _titleText;

		// Token: 0x04000C7B RID: 3195
		private string _influenceBonusDescription;

		// Token: 0x04000C7C RID: 3196
		private string _name;

		// Token: 0x04000C7D RID: 3197
		private string _totalInfluence;

		// Token: 0x04000C7E RID: 3198
		private bool _isSelected;

		// Token: 0x04000C7F RID: 3199
		private MBBindingList<ClanSupporterItemVM> _supporters;

		// Token: 0x04000C80 RID: 3200
		private float _totalInfluenceBonus;
	}
}
