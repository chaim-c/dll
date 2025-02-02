using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement
{
	// Token: 0x0200013D RID: 317
	public class ArmyManagementItemVM : ViewModel
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0006DC2F File Offset: 0x0006BE2F
		public float DistInTime { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x0006DC37 File Offset: 0x0006BE37
		public float _distance { get; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0006DC3F File Offset: 0x0006BE3F
		public Clan Clan { get; }

		// Token: 0x06001ECF RID: 7887 RVA: 0x0006DC48 File Offset: 0x0006BE48
		public ArmyManagementItemVM(Action<ArmyManagementItemVM> onAddToCart, Action<ArmyManagementItemVM> onRemove, Action<ArmyManagementItemVM> onFocus, MobileParty mobileParty)
		{
			ArmyManagementCalculationModel armyManagementCalculationModel = Campaign.Current.Models.ArmyManagementCalculationModel;
			this._onAddToCart = onAddToCart;
			this._onRemove = onRemove;
			this._onFocus = onFocus;
			this.Party = mobileParty;
			this._eligibilityReason = TextObject.Empty;
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(mobileParty.LeaderHero.ClanBanner), true);
			CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(mobileParty.LeaderHero.CharacterObject, false);
			this.LordFace = new ImageIdentifierVM(characterCode);
			this.Relation = armyManagementCalculationModel.GetPartyRelation(mobileParty.LeaderHero);
			this.Strength = this.Party.MemberRoster.TotalManCount;
			this._distance = Campaign.Current.Models.MapDistanceModel.GetDistance(this.Party, MobileParty.MainParty);
			this.DistInTime = (float)MathF.Ceiling(this._distance / this.Party.Speed);
			this.Clan = mobileParty.LeaderHero.Clan;
			this.IsMainHero = mobileParty.IsMainParty;
			this.UpdateEligibility();
			this.Cost = armyManagementCalculationModel.CalculatePartyInfluenceCost(MobileParty.MainParty, mobileParty);
			this.IsTransferDisabled = (this.IsMainHero || PlayerSiege.PlayerSiegeEvent != null);
			this.RefreshValues();
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x0006DDA8 File Offset: 0x0006BFA8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.InArmyText = GameTexts.FindText("str_in_army", null).ToString();
			this.LeaderNameText = this.Party.LeaderHero.Name.ToString();
			this.NameText = this.Party.Name.ToString();
			if (!this.Party.IsMainParty)
			{
				this.DistanceText = (((int)this._distance < 5) ? GameTexts.FindText("str_nearby", null).ToString() : CampaignUIHelper.GetPartyDistanceByTimeText((float)((int)this._distance), this.Party.Speed));
			}
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x0006DE49 File Offset: 0x0006C049
		public void ExecuteAction()
		{
			if (this.IsInCart)
			{
				this.OnRemove();
				return;
			}
			this.OnAddToCart();
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x0006DE60 File Offset: 0x0006C060
		private void OnRemove()
		{
			if (!this.IsMainHero)
			{
				this._onRemove(this);
				this.UpdateEligibility();
			}
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x0006DE7C File Offset: 0x0006C07C
		private void OnAddToCart()
		{
			this.UpdateEligibility();
			if (this.IsEligible)
			{
				this._onAddToCart(this);
			}
			this.UpdateEligibility();
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x0006DE9E File Offset: 0x0006C09E
		public void ExecuteSetFocused()
		{
			this.IsFocused = true;
			Action<ArmyManagementItemVM> onFocus = this._onFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(this);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x0006DEB8 File Offset: 0x0006C0B8
		public void ExecuteSetUnfocused()
		{
			this.IsFocused = false;
			Action<ArmyManagementItemVM> onFocus = this._onFocus;
			if (onFocus == null)
			{
				return;
			}
			onFocus(null);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x0006DED4 File Offset: 0x0006C0D4
		public void UpdateEligibility()
		{
			GameModels models = Campaign.Current.Models;
			ArmyManagementCalculationModel armyManagementCalculationModel = (models != null) ? models.ArmyManagementCalculationModel : null;
			float num = (armyManagementCalculationModel != null) ? armyManagementCalculationModel.GetPartySizeScore(this.Party) : 0f;
			IDisbandPartyCampaignBehavior behavior = Campaign.Current.CampaignBehaviorManager.GetBehavior<IDisbandPartyCampaignBehavior>();
			bool isEligible = false;
			this._eligibilityReason = TextObject.Empty;
			if (!this.CanJoinBackWithoutCost)
			{
				if (PlayerSiege.PlayerSiegeEvent != null)
				{
					this._eligibilityReason = GameTexts.FindText("str_action_disabled_reason_siege", null);
				}
				else if (this.Party == null)
				{
					this._eligibilityReason = new TextObject("{=f6vTzVar}Does not have a mobile party.", null);
				}
				else
				{
					Hero leaderHero = this.Party.LeaderHero;
					IFaction mapFaction = Hero.MainHero.MapFaction;
					if (leaderHero == ((mapFaction != null) ? mapFaction.Leader : null))
					{
						this._eligibilityReason = new TextObject("{=ipLqVv1f}You cannot invite the ruler's party to your army.", null);
					}
					else
					{
						if (this.Party.Army != null)
						{
							Army army = this.Party.Army;
							MobileParty partyBelongedTo = Hero.MainHero.PartyBelongedTo;
							if (army != ((partyBelongedTo != null) ? partyBelongedTo.Army : null))
							{
								this._eligibilityReason = new TextObject("{=aROohsat}Already in another army.", null);
								goto IL_217;
							}
						}
						if (this.Party.Army != null)
						{
							Army army2 = this.Party.Army;
							MobileParty partyBelongedTo2 = Hero.MainHero.PartyBelongedTo;
							if (army2 == ((partyBelongedTo2 != null) ? partyBelongedTo2.Army : null))
							{
								this._eligibilityReason = new TextObject("{=Vq8yavES}Already in army.", null);
								goto IL_217;
							}
						}
						if (this.Party.MapEvent != null || this.Party.SiegeEvent != null)
						{
							this._eligibilityReason = new TextObject("{=pkbUiKFJ}Currently fighting an enemy.", null);
						}
						else if (num <= 0.4f)
						{
							this._eligibilityReason = new TextObject("{=SVJlOYCB}Party has less men than 40% of it's party size limit.", null);
						}
						else if (this.IsInCart)
						{
							this._eligibilityReason = new TextObject("{=idRXFzQ6}Already added to the army.", null);
						}
						else if (this.Party.IsDisbanding || (behavior != null && behavior.IsPartyWaitingForDisband(this.Party)))
						{
							this._eligibilityReason = new TextObject("{=tFGM0yav}This party is disbanding.", null);
						}
						else if (armyManagementCalculationModel != null && !armyManagementCalculationModel.CheckPartyEligibility(this.Party))
						{
							this._eligibilityReason = new TextObject("{=nuK4Afnr}Party is not eligible to join the army.", null);
						}
						else
						{
							isEligible = true;
						}
					}
				}
			}
			else
			{
				isEligible = true;
			}
			IL_217:
			this.IsEligible = isEligible;
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x0006E100 File Offset: 0x0006C300
		public void ExecuteBeginHint()
		{
			if (!this.IsEligible)
			{
				MBInformationManager.ShowHint(this._eligibilityReason.ToString());
				return;
			}
			InformationManager.ShowTooltip(typeof(MobileParty), new object[]
			{
				this.Party,
				true,
				true
			});
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x0006E156 File Offset: 0x0006C356
		public void ExecuteEndHint()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x0006E15D File Offset: 0x0006C35D
		public void ExecuteOpenEncyclopedia()
		{
			MobileParty party = this.Party;
			if (((party != null) ? party.LeaderHero : null) != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Party.LeaderHero.EncyclopediaLink);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0006E192 File Offset: 0x0006C392
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x0006E19A File Offset: 0x0006C39A
		[DataSourceProperty]
		public InputKeyItemVM RemoveInputKey
		{
			get
			{
				return this._removeInputKey;
			}
			set
			{
				if (value != this._removeInputKey)
				{
					this._removeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RemoveInputKey");
				}
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0006E1B8 File Offset: 0x0006C3B8
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
		[DataSourceProperty]
		public bool IsEligible
		{
			get
			{
				return this._isEligible;
			}
			set
			{
				if (value != this._isEligible)
				{
					this._isEligible = value;
					base.OnPropertyChangedWithValue(value, "IsEligible");
				}
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0006E1DE File Offset: 0x0006C3DE
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x0006E1E6 File Offset: 0x0006C3E6
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

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06001EE0 RID: 7904 RVA: 0x0006E204 File Offset: 0x0006C404
		// (set) Token: 0x06001EE1 RID: 7905 RVA: 0x0006E20C File Offset: 0x0006C40C
		[DataSourceProperty]
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (value != this._isMainHero)
				{
					this._isMainHero = value;
					base.OnPropertyChangedWithValue(value, "IsMainHero");
				}
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x0006E22A File Offset: 0x0006C42A
		// (set) Token: 0x06001EE3 RID: 7907 RVA: 0x0006E232 File Offset: 0x0006C432
		[DataSourceProperty]
		public int Strength
		{
			get
			{
				return this._strength;
			}
			set
			{
				if (value != this._strength)
				{
					this._strength = value;
					base.OnPropertyChangedWithValue(value, "Strength");
				}
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0006E250 File Offset: 0x0006C450
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0006E258 File Offset: 0x0006C458
		[DataSourceProperty]
		public string DistanceText
		{
			get
			{
				return this._distanceText;
			}
			set
			{
				if (value != this._distanceText)
				{
					this._distanceText = value;
					base.OnPropertyChangedWithValue<string>(value, "DistanceText");
				}
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0006E27B File Offset: 0x0006C47B
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0006E283 File Offset: 0x0006C483
		[DataSourceProperty]
		public string InArmyText
		{
			get
			{
				return this._inArmyText;
			}
			set
			{
				if (value != this._inArmyText)
				{
					this._inArmyText = value;
					base.OnPropertyChangedWithValue<string>(value, "InArmyText");
				}
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0006E2A6 File Offset: 0x0006C4A6
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0006E2AE File Offset: 0x0006C4AE
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

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0006E2CC File Offset: 0x0006C4CC
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
		[DataSourceProperty]
		public int Relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				if (value != this._relation)
				{
					this._relation = value;
					base.OnPropertyChangedWithValue(value, "Relation");
				}
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0006E2F2 File Offset: 0x0006C4F2
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0006E2FA File Offset: 0x0006C4FA
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0006E318 File Offset: 0x0006C518
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x0006E320 File Offset: 0x0006C520
		[DataSourceProperty]
		public ImageIdentifierVM LordFace
		{
			get
			{
				return this._lordFace;
			}
			set
			{
				if (value != this._lordFace)
				{
					this._lordFace = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "LordFace");
				}
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0006E33E File Offset: 0x0006C53E
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0006E346 File Offset: 0x0006C546
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

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x0006E369 File Offset: 0x0006C569
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0006E371 File Offset: 0x0006C571
		[DataSourceProperty]
		public bool IsAlreadyWithPlayer
		{
			get
			{
				return this._isAlreadyWithPlayer;
			}
			set
			{
				if (value != this._isAlreadyWithPlayer)
				{
					this._isAlreadyWithPlayer = value;
					base.OnPropertyChangedWithValue(value, "IsAlreadyWithPlayer");
				}
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x0006E38F File Offset: 0x0006C58F
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0006E397 File Offset: 0x0006C597
		[DataSourceProperty]
		public bool IsTransferDisabled
		{
			get
			{
				return this._isTransferDisabled;
			}
			set
			{
				if (value != this._isTransferDisabled)
				{
					this._isTransferDisabled = value;
					base.OnPropertyChangedWithValue(value, "IsTransferDisabled");
				}
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0006E3B5 File Offset: 0x0006C5B5
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0006E3BD File Offset: 0x0006C5BD
		[DataSourceProperty]
		public string LeaderNameText
		{
			get
			{
				return this._leaderNameText;
			}
			set
			{
				if (value != this._leaderNameText)
				{
					this._leaderNameText = value;
					base.OnPropertyChangedWithValue<string>(value, "LeaderNameText");
				}
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x0006E3E0 File Offset: 0x0006C5E0
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x0006E3E8 File Offset: 0x0006C5E8
		[DataSourceProperty]
		public bool IsFocused
		{
			get
			{
				return this._isFocused;
			}
			set
			{
				if (value != this._isFocused)
				{
					this._isFocused = value;
					base.OnPropertyChangedWithValue(value, "IsFocused");
				}
			}
		}

		// Token: 0x04000E80 RID: 3712
		private readonly Action<ArmyManagementItemVM> _onAddToCart;

		// Token: 0x04000E81 RID: 3713
		private readonly Action<ArmyManagementItemVM> _onRemove;

		// Token: 0x04000E82 RID: 3714
		private readonly Action<ArmyManagementItemVM> _onFocus;

		// Token: 0x04000E83 RID: 3715
		public readonly MobileParty Party;

		// Token: 0x04000E84 RID: 3716
		private const float _minimumPartySizeScoreNeeded = 0.4f;

		// Token: 0x04000E85 RID: 3717
		public bool CanJoinBackWithoutCost;

		// Token: 0x04000E86 RID: 3718
		private TextObject _eligibilityReason;

		// Token: 0x04000E87 RID: 3719
		private InputKeyItemVM _removeInputKey;

		// Token: 0x04000E88 RID: 3720
		private ImageIdentifierVM _clanBanner;

		// Token: 0x04000E89 RID: 3721
		private ImageIdentifierVM _lordFace;

		// Token: 0x04000E8A RID: 3722
		private string _nameText;

		// Token: 0x04000E8B RID: 3723
		private string _inArmyText;

		// Token: 0x04000E8C RID: 3724
		private string _leaderNameText;

		// Token: 0x04000E8D RID: 3725
		private int _relation = -102;

		// Token: 0x04000E8E RID: 3726
		private int _strength = -1;

		// Token: 0x04000E8F RID: 3727
		private string _distanceText;

		// Token: 0x04000E90 RID: 3728
		private int _cost = -1;

		// Token: 0x04000E91 RID: 3729
		private bool _isEligible;

		// Token: 0x04000E92 RID: 3730
		private bool _isMainHero;

		// Token: 0x04000E93 RID: 3731
		private bool _isInCart;

		// Token: 0x04000E94 RID: 3732
		private bool _isAlreadyWithPlayer;

		// Token: 0x04000E95 RID: 3733
		private bool _isTransferDisabled;

		// Token: 0x04000E96 RID: 3734
		private bool _isFocused;
	}
}
