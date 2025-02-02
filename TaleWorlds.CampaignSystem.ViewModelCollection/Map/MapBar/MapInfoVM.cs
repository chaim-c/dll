using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Library.Information;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapBar
{
	// Token: 0x02000053 RID: 83
	public class MapInfoVM : ViewModel
	{
		// Token: 0x060005FB RID: 1531 RVA: 0x0001D338 File Offset: 0x0001B538
		public MapInfoVM()
		{
			this.DenarTooltip = CampaignUIHelper.GetDenarTooltip();
			this.HealthHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPlayerHitpointsTooltip());
			this.InfluenceHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetInfluenceTooltip(Clan.PlayerClan));
			this.AvailableTroopsHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetMainPartyHealthTooltip());
			this.ExtendHint = new HintViewModel(GameTexts.FindText("str_map_extend_bar_hint", null), null);
			this.SpeedHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartySpeedTooltip());
			this.ViewDistanceHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetViewDistanceTooltip());
			this.TroopWageHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartyWageTooltip());
			this.MoraleHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartyMoraleTooltip(MobileParty.MainParty));
			this.DailyConsumptionHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetPartyFoodTooltip(MobileParty.MainParty));
			this._viewDataTracker = Campaign.Current.GetCampaignBehavior<IViewDataTracker>();
			this.IsInfoBarExtended = this._viewDataTracker.GetMapBarExtendedState();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001D531 File Offset: 0x0001B731
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.UpdatePlayerInfo(true);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001D540 File Offset: 0x0001B740
		public void Tick()
		{
			this.IsMainHeroSick = (Hero.MainHero != null && Hero.IsMainHeroIll);
			Hero mainHero = Hero.MainHero;
			this.IsInfoBarEnabled = (mainHero != null && mainHero.IsAlive);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001D56E File Offset: 0x0001B76E
		public void Refresh()
		{
			this.UpdatePlayerInfo(false);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001D578 File Offset: 0x0001B778
		private void UpdatePlayerInfo(bool updateForced)
		{
			int totalWage = MobileParty.MainParty.TotalWage;
			ExplainedNumber explainedNumber = Campaign.Current.Models.ClanFinanceModel.CalculateClanGoldChange(Clan.PlayerClan, true, false, true);
			this.IsDenarTooltipWarning = ((float)Hero.MainHero.Gold + explainedNumber.ResultNumber < 0f);
			this.IsInfluenceTooltipWarning = (Hero.MainHero.Clan.Influence < -100f);
			this.IsMoraleTooltipWarning = (MobileParty.MainParty.Morale < (float)Campaign.Current.Models.PartyDesertionModel.GetMoraleThresholdForTroopDesertion(MobileParty.MainParty));
			int numDaysForFoodToLast = MobileParty.MainParty.GetNumDaysForFoodToLast();
			this.IsDailyConsumptionTooltipWarning = (numDaysForFoodToLast < 1);
			this.IsAvailableTroopsTooltipWarning = (PartyBase.MainParty.PartySizeLimit < PartyBase.MainParty.NumberOfAllMembers || PartyBase.MainParty.PrisonerSizeLimit < PartyBase.MainParty.NumberOfPrisoners);
			this.IsHealthTooltipWarning = Hero.MainHero.IsWounded;
			if (this.Denars != Hero.MainHero.Gold || updateForced)
			{
				this.Denars = Hero.MainHero.Gold;
				this.DenarsWithAbbrText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(this.Denars);
			}
			if (this.Influence != (int)Hero.MainHero.Clan.Influence || updateForced)
			{
				this.Influence = (int)Hero.MainHero.Clan.Influence;
				this.InfluenceWithAbbrText = CampaignUIHelper.GetAbbreviatedValueTextFromValue(this.Influence);
			}
			this.Morale = (int)MobileParty.MainParty.Morale;
			this.TotalFood = (int)((MobileParty.MainParty.Food > 0f) ? MobileParty.MainParty.Food : 0f);
			this.TotalTroops = PartyBase.MainParty.MemberRoster.TotalManCount;
			this.AvailableTroopsText = CampaignUIHelper.GetPartyNameplateText(PartyBase.MainParty);
			int num = (int)MathF.Clamp((float)(Hero.MainHero.HitPoints * 100 / CharacterObject.PlayerCharacter.MaxHitPoints()), 1f, 100f);
			if (this.Health != num || updateForced)
			{
				this.Health = num;
				GameTexts.SetVariable("NUMBER", this.Health);
				this.HealthTextWithPercentage = GameTexts.FindText("str_NUMBER_percent", null).ToString();
			}
			float num2 = MathF.Round(MobileParty.MainParty.Morale, 1);
			if (this._latestMorale != num2 || updateForced)
			{
				this._latestMorale = num2;
				MBTextManager.SetTextVariable("BASE_EFFECT", num2.ToString("0.0"), false);
			}
			float num3 = MobileParty.MainParty.CurrentNavigationFace.IsValid() ? MobileParty.MainParty.Speed : 0f;
			if (this._latestSpeed != num3 || updateForced)
			{
				this._latestSpeed = num3;
				this.Speed = CampaignUIHelper.FloatToString(num3);
			}
			float seeingRange = MobileParty.MainParty.SeeingRange;
			if (this._latestSeeingRange != seeingRange || updateForced)
			{
				this._latestSeeingRange = seeingRange;
				this.ViewDistance = CampaignUIHelper.FloatToString(seeingRange);
			}
			if (this._latestTotalWage != totalWage || updateForced)
			{
				this._latestTotalWage = totalWage;
				this.TroopWage = totalWage.ToString();
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001D89B File Offset: 0x0001BA9B
		// (set) Token: 0x06000601 RID: 1537 RVA: 0x0001D8A3 File Offset: 0x0001BAA3
		[DataSourceProperty]
		public bool IsHealthTooltipWarning
		{
			get
			{
				return this._isHealthTooltipWarning;
			}
			set
			{
				if (value != this._isHealthTooltipWarning)
				{
					this._isHealthTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsHealthTooltipWarning");
				}
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0001D8C1 File Offset: 0x0001BAC1
		// (set) Token: 0x06000603 RID: 1539 RVA: 0x0001D8C9 File Offset: 0x0001BAC9
		[DataSourceProperty]
		public bool IsMainHeroSick
		{
			get
			{
				return this._isMainHeroSick;
			}
			set
			{
				if (value != this._isMainHeroSick)
				{
					this._isMainHeroSick = value;
					base.OnPropertyChangedWithValue(value, "IsMainHeroSick");
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001D8E7 File Offset: 0x0001BAE7
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001D8EF File Offset: 0x0001BAEF
		[DataSourceProperty]
		public HintViewModel ExtendHint
		{
			get
			{
				return this._extendHint;
			}
			set
			{
				if (value != this._extendHint)
				{
					this._extendHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "ExtendHint");
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0001D90D File Offset: 0x0001BB0D
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x0001D915 File Offset: 0x0001BB15
		[DataSourceProperty]
		public TooltipTriggerVM DenarTooltip
		{
			get
			{
				return this._denarTooltip;
			}
			set
			{
				if (value != this._denarTooltip)
				{
					this._denarTooltip = value;
					base.OnPropertyChangedWithValue<TooltipTriggerVM>(value, "DenarTooltip");
				}
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0001D933 File Offset: 0x0001BB33
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0001D93B File Offset: 0x0001BB3B
		[DataSourceProperty]
		public BasicTooltipViewModel InfluenceHint
		{
			get
			{
				return this._influenceHint;
			}
			set
			{
				if (value != this._influenceHint)
				{
					this._influenceHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "InfluenceHint");
				}
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0001D959 File Offset: 0x0001BB59
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x0001D961 File Offset: 0x0001BB61
		[DataSourceProperty]
		public BasicTooltipViewModel AvailableTroopsHint
		{
			get
			{
				return this._availableTroopsHint;
			}
			set
			{
				if (value != this._availableTroopsHint)
				{
					this._availableTroopsHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "AvailableTroopsHint");
				}
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0001D97F File Offset: 0x0001BB7F
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x0001D987 File Offset: 0x0001BB87
		[DataSourceProperty]
		public BasicTooltipViewModel HealthHint
		{
			get
			{
				return this._healthHint;
			}
			set
			{
				if (value != this._healthHint)
				{
					this._healthHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "HealthHint");
				}
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0001D9A5 File Offset: 0x0001BBA5
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001D9AD File Offset: 0x0001BBAD
		[DataSourceProperty]
		public BasicTooltipViewModel DailyConsumptionHint
		{
			get
			{
				return this._dailyConsumptionHint;
			}
			set
			{
				if (value != this._dailyConsumptionHint)
				{
					this._dailyConsumptionHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DailyConsumptionHint");
				}
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0001D9CB File Offset: 0x0001BBCB
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0001D9D3 File Offset: 0x0001BBD3
		[DataSourceProperty]
		public BasicTooltipViewModel MoraleHint
		{
			get
			{
				return this._moraleHint;
			}
			set
			{
				if (value != this._moraleHint)
				{
					this._moraleHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "MoraleHint");
				}
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001D9F1 File Offset: 0x0001BBF1
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001D9F9 File Offset: 0x0001BBF9
		[DataSourceProperty]
		public BasicTooltipViewModel SpeedHint
		{
			get
			{
				return this._speedHint;
			}
			set
			{
				if (value != this._speedHint)
				{
					this._speedHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SpeedHint");
				}
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001DA17 File Offset: 0x0001BC17
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0001DA1F File Offset: 0x0001BC1F
		[DataSourceProperty]
		public BasicTooltipViewModel ViewDistanceHint
		{
			get
			{
				return this._viewDistanceHint;
			}
			set
			{
				if (value != this._viewDistanceHint)
				{
					this._viewDistanceHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ViewDistanceHint");
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001DA3D File Offset: 0x0001BC3D
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0001DA45 File Offset: 0x0001BC45
		[DataSourceProperty]
		public BasicTooltipViewModel TrainingFactorHint
		{
			get
			{
				return this._trainingFactorHint;
			}
			set
			{
				if (value != this._trainingFactorHint)
				{
					this._trainingFactorHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "TrainingFactorHint");
				}
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0001DA63 File Offset: 0x0001BC63
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001DA6B File Offset: 0x0001BC6B
		[DataSourceProperty]
		public BasicTooltipViewModel TroopWageHint
		{
			get
			{
				return this._troopWageHint;
			}
			set
			{
				if (value != this._troopWageHint)
				{
					this._troopWageHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "TroopWageHint");
				}
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0001DA89 File Offset: 0x0001BC89
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0001DA91 File Offset: 0x0001BC91
		[DataSourceProperty]
		public bool IsDenarTooltipWarning
		{
			get
			{
				return this._isDenarTooltipWarning;
			}
			set
			{
				if (value != this._isDenarTooltipWarning)
				{
					this._isDenarTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsDenarTooltipWarning");
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001DAAF File Offset: 0x0001BCAF
		// (set) Token: 0x0600061D RID: 1565 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		[DataSourceProperty]
		public bool IsInfluenceTooltipWarning
		{
			get
			{
				return this._isInfluenceTooltipWarning;
			}
			set
			{
				if (value != this._isInfluenceTooltipWarning)
				{
					this._isInfluenceTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsInfluenceTooltipWarning");
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001DAD5 File Offset: 0x0001BCD5
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001DADD File Offset: 0x0001BCDD
		[DataSourceProperty]
		public bool IsMoraleTooltipWarning
		{
			get
			{
				return this._isMoraleTooltipWarning;
			}
			set
			{
				if (value != this._isMoraleTooltipWarning)
				{
					this._isMoraleTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsMoraleTooltipWarning");
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001DAFB File Offset: 0x0001BCFB
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001DB03 File Offset: 0x0001BD03
		[DataSourceProperty]
		public bool IsDailyConsumptionTooltipWarning
		{
			get
			{
				return this._isDailyConsumptionTooltipWarning;
			}
			set
			{
				if (value != this._isDailyConsumptionTooltipWarning)
				{
					this._isDailyConsumptionTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsDailyConsumptionTooltipWarning");
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0001DB21 File Offset: 0x0001BD21
		// (set) Token: 0x06000623 RID: 1571 RVA: 0x0001DB29 File Offset: 0x0001BD29
		[DataSourceProperty]
		public bool IsAvailableTroopsTooltipWarning
		{
			get
			{
				return this._isAvailableTroopsTooltipWarning;
			}
			set
			{
				if (value != this._isAvailableTroopsTooltipWarning)
				{
					this._isAvailableTroopsTooltipWarning = value;
					base.OnPropertyChangedWithValue(value, "IsAvailableTroopsTooltipWarning");
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0001DB47 File Offset: 0x0001BD47
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0001DB4F File Offset: 0x0001BD4F
		[DataSourceProperty]
		public string DenarsWithAbbrText
		{
			get
			{
				return this._denarsWithAbbrText;
			}
			set
			{
				if (value != this._denarsWithAbbrText)
				{
					this._denarsWithAbbrText = value;
					base.OnPropertyChangedWithValue<string>(value, "DenarsWithAbbrText");
				}
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001DB72 File Offset: 0x0001BD72
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0001DB7A File Offset: 0x0001BD7A
		[DataSourceProperty]
		public int Denars
		{
			get
			{
				return this._denars;
			}
			set
			{
				if (value != this._denars)
				{
					this._denars = value;
					base.OnPropertyChangedWithValue(value, "Denars");
				}
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x0001DB98 File Offset: 0x0001BD98
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		[DataSourceProperty]
		public int Influence
		{
			get
			{
				return this._influence;
			}
			set
			{
				if (value != this._influence)
				{
					this._influence = value;
					base.OnPropertyChangedWithValue(value, "Influence");
				}
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001DBBE File Offset: 0x0001BDBE
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001DBC6 File Offset: 0x0001BDC6
		[DataSourceProperty]
		public string InfluenceWithAbbrText
		{
			get
			{
				return this._influenceWithAbbrText;
			}
			set
			{
				if (value != this._influenceWithAbbrText)
				{
					this._influenceWithAbbrText = value;
					base.OnPropertyChangedWithValue<string>(value, "InfluenceWithAbbrText");
				}
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001DBE9 File Offset: 0x0001BDE9
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001DBF1 File Offset: 0x0001BDF1
		[DataSourceProperty]
		public int Morale
		{
			get
			{
				return this._morale;
			}
			set
			{
				if (value != this._morale)
				{
					this._morale = value;
					base.OnPropertyChangedWithValue(value, "Morale");
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001DC0F File Offset: 0x0001BE0F
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001DC17 File Offset: 0x0001BE17
		[DataSourceProperty]
		public int TotalFood
		{
			get
			{
				return this._totalFood;
			}
			set
			{
				if (value != this._totalFood)
				{
					this._totalFood = value;
					base.OnPropertyChangedWithValue(value, "TotalFood");
				}
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0001DC35 File Offset: 0x0001BE35
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0001DC3D File Offset: 0x0001BE3D
		[DataSourceProperty]
		public int Health
		{
			get
			{
				return this._health;
			}
			set
			{
				if (value != this._health)
				{
					this._health = value;
					base.OnPropertyChangedWithValue(value, "Health");
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001DC5B File Offset: 0x0001BE5B
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0001DC63 File Offset: 0x0001BE63
		[DataSourceProperty]
		public string HealthTextWithPercentage
		{
			get
			{
				return this._healthTextWithPercentage;
			}
			set
			{
				if (value != this._healthTextWithPercentage)
				{
					this._healthTextWithPercentage = value;
					base.OnPropertyChangedWithValue<string>(value, "HealthTextWithPercentage");
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001DC86 File Offset: 0x0001BE86
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0001DC8E File Offset: 0x0001BE8E
		[DataSourceProperty]
		public string AvailableTroopsText
		{
			get
			{
				return this._availableTroopsText;
			}
			set
			{
				if (value != this._availableTroopsText)
				{
					this._availableTroopsText = value;
					base.OnPropertyChangedWithValue<string>(value, "AvailableTroopsText");
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001DCB1 File Offset: 0x0001BEB1
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001DCB9 File Offset: 0x0001BEB9
		[DataSourceProperty]
		public int TotalTroops
		{
			get
			{
				return this._totalTroops;
			}
			set
			{
				if (value != this._totalTroops)
				{
					this._totalTroops = value;
					base.OnPropertyChangedWithValue(value, "TotalTroops");
				}
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001DCD7 File Offset: 0x0001BED7
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001DCDF File Offset: 0x0001BEDF
		[DataSourceProperty]
		public string Speed
		{
			get
			{
				return this._speed;
			}
			set
			{
				if (value != this._speed)
				{
					this._speed = value;
					base.OnPropertyChangedWithValue<string>(value, "Speed");
				}
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001DD02 File Offset: 0x0001BF02
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x0001DD0A File Offset: 0x0001BF0A
		[DataSourceProperty]
		public string ViewDistance
		{
			get
			{
				return this._viewDistance;
			}
			set
			{
				if (value != this._viewDistance)
				{
					this._viewDistance = value;
					base.OnPropertyChangedWithValue<string>(value, "ViewDistance");
				}
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001DD2D File Offset: 0x0001BF2D
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x0001DD35 File Offset: 0x0001BF35
		[DataSourceProperty]
		public string TrainingFactor
		{
			get
			{
				return this._trainingFactor;
			}
			set
			{
				if (value != this._trainingFactor)
				{
					this._trainingFactor = value;
					base.OnPropertyChangedWithValue<string>(value, "TrainingFactor");
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001DD58 File Offset: 0x0001BF58
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001DD60 File Offset: 0x0001BF60
		[DataSourceProperty]
		public string TroopWage
		{
			get
			{
				return this._troopWage;
			}
			set
			{
				if (value != this._troopWage)
				{
					this._troopWage = value;
					base.OnPropertyChangedWithValue<string>(value, "TroopWage");
				}
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001DD83 File Offset: 0x0001BF83
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001DD8B File Offset: 0x0001BF8B
		[DataSourceProperty]
		public bool IsInfoBarExtended
		{
			get
			{
				return this._isInfoBarExtended;
			}
			set
			{
				if (value != this._isInfoBarExtended)
				{
					this._isInfoBarExtended = value;
					this._viewDataTracker.SetMapBarExtendedState(value);
					base.OnPropertyChangedWithValue(value, "IsInfoBarExtended");
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001DDB5 File Offset: 0x0001BFB5
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001DDBD File Offset: 0x0001BFBD
		[DataSourceProperty]
		public bool IsInfoBarEnabled
		{
			get
			{
				return this._isInfoBarEnabled;
			}
			set
			{
				if (value != this._isInfoBarEnabled)
				{
					this._isInfoBarEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsInfoBarEnabled");
				}
			}
		}

		// Token: 0x0400029D RID: 669
		private int _latestTotalWage = -1;

		// Token: 0x0400029E RID: 670
		private float _latestSeeingRange = -1f;

		// Token: 0x0400029F RID: 671
		private float _latestSpeed = -1f;

		// Token: 0x040002A0 RID: 672
		private float _latestMorale = -1f;

		// Token: 0x040002A1 RID: 673
		private IViewDataTracker _viewDataTracker;

		// Token: 0x040002A2 RID: 674
		private string _speed;

		// Token: 0x040002A3 RID: 675
		private string _viewDistance;

		// Token: 0x040002A4 RID: 676
		private string _trainingFactor;

		// Token: 0x040002A5 RID: 677
		private string _troopWage;

		// Token: 0x040002A6 RID: 678
		private string _healthTextWithPercentage;

		// Token: 0x040002A7 RID: 679
		private string _denarsWithAbbrText = "";

		// Token: 0x040002A8 RID: 680
		private string _influenceWithAbbrText = "";

		// Token: 0x040002A9 RID: 681
		private string _availableTroopsText;

		// Token: 0x040002AA RID: 682
		private int _denars = -1;

		// Token: 0x040002AB RID: 683
		private int _influence = -1;

		// Token: 0x040002AC RID: 684
		private int _morale = -1;

		// Token: 0x040002AD RID: 685
		private int _totalFood;

		// Token: 0x040002AE RID: 686
		private int _health;

		// Token: 0x040002AF RID: 687
		private int _totalTroops;

		// Token: 0x040002B0 RID: 688
		private bool _isInfoBarExtended;

		// Token: 0x040002B1 RID: 689
		private bool _isInfoBarEnabled;

		// Token: 0x040002B2 RID: 690
		private bool _isDenarTooltipWarning;

		// Token: 0x040002B3 RID: 691
		private bool _isHealthTooltipWarning;

		// Token: 0x040002B4 RID: 692
		private bool _isInfluenceTooltipWarning;

		// Token: 0x040002B5 RID: 693
		private bool _isMoraleTooltipWarning;

		// Token: 0x040002B6 RID: 694
		private bool _isDailyConsumptionTooltipWarning;

		// Token: 0x040002B7 RID: 695
		private bool _isAvailableTroopsTooltipWarning;

		// Token: 0x040002B8 RID: 696
		private bool _isMainHeroSick;

		// Token: 0x040002B9 RID: 697
		private TooltipTriggerVM _denarTooltip;

		// Token: 0x040002BA RID: 698
		private BasicTooltipViewModel _influenceHint;

		// Token: 0x040002BB RID: 699
		private BasicTooltipViewModel _availableTroopsHint;

		// Token: 0x040002BC RID: 700
		private BasicTooltipViewModel _healthHint;

		// Token: 0x040002BD RID: 701
		private BasicTooltipViewModel _dailyConsumptionHint;

		// Token: 0x040002BE RID: 702
		private BasicTooltipViewModel _moraleHint;

		// Token: 0x040002BF RID: 703
		private BasicTooltipViewModel _trainingFactorHint;

		// Token: 0x040002C0 RID: 704
		private BasicTooltipViewModel _troopWageHint;

		// Token: 0x040002C1 RID: 705
		private BasicTooltipViewModel _speedHint;

		// Token: 0x040002C2 RID: 706
		private BasicTooltipViewModel _viewDistanceHint;

		// Token: 0x040002C3 RID: 707
		private HintViewModel _extendHint;
	}
}
