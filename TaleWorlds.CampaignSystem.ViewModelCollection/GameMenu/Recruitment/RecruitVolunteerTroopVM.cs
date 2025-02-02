using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Recruitment
{
	// Token: 0x020000A1 RID: 161
	public class RecruitVolunteerTroopVM : ViewModel
	{
		// Token: 0x06000FEC RID: 4076 RVA: 0x0003E278 File Offset: 0x0003C478
		public RecruitVolunteerTroopVM(RecruitVolunteerVM owner, CharacterObject character, int index, Action<RecruitVolunteerTroopVM> onClick, Action<RecruitVolunteerTroopVM> onRemoveFromCart)
		{
			if (character != null)
			{
				this.NameText = character.Name.ToString();
				this._character = character;
				GameTexts.SetVariable("LEVEL", character.Level);
				this.Level = GameTexts.FindText("str_level_with_value", null).ToString();
				this.Character = character;
				this.Wage = this.Character.TroopWage;
				this.Cost = Campaign.Current.Models.PartyWageModel.GetTroopRecruitmentCost(this.Character, Hero.MainHero, false);
				this.IsTroopEmpty = false;
				CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(character, false);
				this.ImageIdentifier = new ImageIdentifierVM(characterCode);
				this.TierIconData = CampaignUIHelper.GetCharacterTierData(character, false);
				this.TypeIconData = CampaignUIHelper.GetCharacterTypeData(character, false);
			}
			else
			{
				this.IsTroopEmpty = true;
			}
			this.Owner = owner;
			if (this.Owner != null)
			{
				this._currentRelation = Hero.MainHero.GetRelation(this.Owner.OwnerHero);
			}
			this._maximumIndexCanBeRecruit = Campaign.Current.Models.VolunteerModel.MaximumIndexHeroCanRecruitFromHero(Hero.MainHero, this.Owner.OwnerHero, -101);
			for (int i = -100; i < 100; i++)
			{
				if (index < Campaign.Current.Models.VolunteerModel.MaximumIndexHeroCanRecruitFromHero(Hero.MainHero, this.Owner.OwnerHero, i))
				{
					this._requiredRelation = i;
					break;
				}
			}
			this._onClick = onClick;
			this.Index = index;
			this._onRemoveFromCart = onRemoveFromCart;
			this.RefreshValues();
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003E400 File Offset: 0x0003C600
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this._character != null)
			{
				this.NameText = this._character.Name.ToString();
				GameTexts.SetVariable("LEVEL", this._character.Level);
				this.Level = GameTexts.FindText("str_level_with_value", null).ToString();
			}
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003E45C File Offset: 0x0003C65C
		public void ExecuteRecruit()
		{
			if (this.CanBeRecruited)
			{
				this._onClick(this);
				return;
			}
			if (this.IsInCart)
			{
				this._onRemoveFromCart(this);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003E487 File Offset: 0x0003C687
		public void ExecuteOpenEncyclopedia()
		{
			if (this.Character != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Character.EncyclopediaLink);
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003E4AB File Offset: 0x0003C6AB
		public void ExecuteRemoveFromCart()
		{
			if (this.IsInCart)
			{
				this._onRemoveFromCart(this);
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003E4C4 File Offset: 0x0003C6C4
		public virtual void ExecuteBeginHint()
		{
			if (this._character != null)
			{
				if (this.PlayerHasEnoughRelation)
				{
					InformationManager.ShowTooltip(typeof(CharacterObject), new object[]
					{
						this._character
					});
					return;
				}
				List<TooltipProperty> list = new List<TooltipProperty>();
				string text = "";
				list.Add(new TooltipProperty(text, this._character.Name.ToString(), 1, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty(text, text, -1, false, TooltipProperty.TooltipPropertyFlags.None));
				GameTexts.SetVariable("LEVEL", this._character.Level);
				GameTexts.SetVariable("newline", "\n");
				list.Add(new TooltipProperty(text, GameTexts.FindText("str_level_with_value", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				GameTexts.SetVariable("REL1", this._currentRelation);
				GameTexts.SetVariable("REL2", this._requiredRelation);
				list.Add(new TooltipProperty(text, GameTexts.FindText("str_recruit_volunteers_not_enough_relation", null).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				InformationManager.ShowTooltip(typeof(List<TooltipProperty>), new object[]
				{
					list
				});
				return;
			}
			else
			{
				if (this.PlayerHasEnoughRelation)
				{
					MBInformationManager.ShowHint(GameTexts.FindText("str_recruit_volunteers_new_troop", null).ToString());
					return;
				}
				GameTexts.SetVariable("newline", "\n");
				GameTexts.SetVariable("REL1", this._currentRelation);
				GameTexts.SetVariable("REL2", this._requiredRelation);
				GameTexts.SetVariable("STR1", GameTexts.FindText("str_recruit_volunteers_new_troop", null));
				GameTexts.SetVariable("STR2", GameTexts.FindText("str_recruit_volunteers_not_enough_relation", null));
				MBInformationManager.ShowHint(GameTexts.FindText("str_string_newline_string", null).ToString());
				return;
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003E666 File Offset: 0x0003C866
		public virtual void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003E66D File Offset: 0x0003C86D
		public void ExecuteFocus()
		{
			if (!this.IsTroopEmpty)
			{
				Action<RecruitVolunteerTroopVM> onFocused = RecruitVolunteerTroopVM.OnFocused;
				if (onFocused == null)
				{
					return;
				}
				onFocused(this);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003E687 File Offset: 0x0003C887
		public void ExecuteUnfocus()
		{
			Action<RecruitVolunteerTroopVM> onFocused = RecruitVolunteerTroopVM.OnFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(null);
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0003E699 File Offset: 0x0003C899
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x0003E6A1 File Offset: 0x0003C8A1
		[DataSourceProperty]
		public string Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value != this._level)
				{
					this._level = value;
					base.OnPropertyChangedWithValue<string>(value, "Level");
				}
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0003E6C4 File Offset: 0x0003C8C4
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x0003E6CC File Offset: 0x0003C8CC
		[DataSourceProperty]
		public bool CanBeRecruited
		{
			get
			{
				return this._canBeRecruited;
			}
			set
			{
				if (value != this._canBeRecruited)
				{
					this._canBeRecruited = value;
					base.OnPropertyChangedWithValue(value, "CanBeRecruited");
				}
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0003E6EA File Offset: 0x0003C8EA
		// (set) Token: 0x06000FFA RID: 4090 RVA: 0x0003E6F2 File Offset: 0x0003C8F2
		[DataSourceProperty]
		public bool IsHiglightEnabled
		{
			get
			{
				return this._isHiglightEnabled;
			}
			set
			{
				if (value != this._isHiglightEnabled)
				{
					this._isHiglightEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsHiglightEnabled");
				}
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0003E710 File Offset: 0x0003C910
		// (set) Token: 0x06000FFC RID: 4092 RVA: 0x0003E718 File Offset: 0x0003C918
		[DataSourceProperty]
		public int Wage
		{
			get
			{
				return this._wage;
			}
			set
			{
				if (value != this._wage)
				{
					this._wage = value;
					base.OnPropertyChangedWithValue(value, "Wage");
				}
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0003E736 File Offset: 0x0003C936
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0003E73E File Offset: 0x0003C93E
		[DataSourceProperty]
		public int Cost
		{
			get
			{
				return this._cost;
			}
			set
			{
				if (value != this._cost)
				{
					this._cost = value;
					base.OnPropertyChangedWithValue(value, "Cost");
				}
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0003E75C File Offset: 0x0003C95C
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0003E764 File Offset: 0x0003C964
		[DataSourceProperty]
		public bool IsInCart
		{
			get
			{
				return this._isInCart;
			}
			set
			{
				if (value != this._isInCart)
				{
					this._isInCart = value;
					base.OnPropertyChangedWithValue(value, "IsInCart");
				}
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0003E782 File Offset: 0x0003C982
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x0003E78A File Offset: 0x0003C98A
		[DataSourceProperty]
		public bool IsTroopEmpty
		{
			get
			{
				return this._isTroopEmpty;
			}
			set
			{
				if (value != this._isTroopEmpty)
				{
					this._isTroopEmpty = value;
					base.OnPropertyChangedWithValue(value, "IsTroopEmpty");
				}
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001003 RID: 4099 RVA: 0x0003E7A8 File Offset: 0x0003C9A8
		// (set) Token: 0x06001004 RID: 4100 RVA: 0x0003E7B0 File Offset: 0x0003C9B0
		[DataSourceProperty]
		public bool PlayerHasEnoughRelation
		{
			get
			{
				return this._playerHasEnoughRelation;
			}
			set
			{
				if (value != this._playerHasEnoughRelation)
				{
					this._playerHasEnoughRelation = value;
					base.OnPropertyChangedWithValue(value, "PlayerHasEnoughRelation");
				}
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x0003E7CE File Offset: 0x0003C9CE
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x0003E7D6 File Offset: 0x0003C9D6
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x0003E7FC File Offset: 0x0003C9FC
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003E81F File Offset: 0x0003CA1F
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x0003E827 File Offset: 0x0003CA27
		[DataSourceProperty]
		public StringItemWithHintVM TierIconData
		{
			get
			{
				return this._tierIconData;
			}
			set
			{
				if (value != this._tierIconData)
				{
					this._tierIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TierIconData");
				}
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0003E845 File Offset: 0x0003CA45
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0003E84D File Offset: 0x0003CA4D
		[DataSourceProperty]
		public StringItemWithHintVM TypeIconData
		{
			get
			{
				return this._typeIconData;
			}
			set
			{
				if (value != this._typeIconData)
				{
					this._typeIconData = value;
					base.OnPropertyChangedWithValue<StringItemWithHintVM>(value, "TypeIconData");
				}
			}
		}

		// Token: 0x0400075D RID: 1885
		public static Action<RecruitVolunteerTroopVM> OnFocused;

		// Token: 0x0400075E RID: 1886
		private readonly Action<RecruitVolunteerTroopVM> _onClick;

		// Token: 0x0400075F RID: 1887
		private readonly Action<RecruitVolunteerTroopVM> _onRemoveFromCart;

		// Token: 0x04000760 RID: 1888
		private CharacterObject _character;

		// Token: 0x04000761 RID: 1889
		public CharacterObject Character;

		// Token: 0x04000762 RID: 1890
		public int Index;

		// Token: 0x04000763 RID: 1891
		private int _maximumIndexCanBeRecruit;

		// Token: 0x04000764 RID: 1892
		private int _requiredRelation;

		// Token: 0x04000765 RID: 1893
		public RecruitVolunteerVM Owner;

		// Token: 0x04000766 RID: 1894
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x04000767 RID: 1895
		private string _nameText;

		// Token: 0x04000768 RID: 1896
		private string _level;

		// Token: 0x04000769 RID: 1897
		private bool _canBeRecruited;

		// Token: 0x0400076A RID: 1898
		private bool _isInCart;

		// Token: 0x0400076B RID: 1899
		private int _wage;

		// Token: 0x0400076C RID: 1900
		private int _cost;

		// Token: 0x0400076D RID: 1901
		private bool _isTroopEmpty;

		// Token: 0x0400076E RID: 1902
		private bool _playerHasEnoughRelation;

		// Token: 0x0400076F RID: 1903
		private int _currentRelation;

		// Token: 0x04000770 RID: 1904
		private bool _isHiglightEnabled;

		// Token: 0x04000771 RID: 1905
		private StringItemWithHintVM _tierIconData;

		// Token: 0x04000772 RID: 1906
		private StringItemWithHintVM _typeIconData;
	}
}
