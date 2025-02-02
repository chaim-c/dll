using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000A4 RID: 164
	[MenuOverlay("EncounterMenuOverlay")]
	public class EncounterMenuOverlayVM : GameMenuOverlay
	{
		// Token: 0x06001055 RID: 4181 RVA: 0x0003F97C File Offset: 0x0003DB7C
		public EncounterMenuOverlayVM()
		{
			this.AttackerPartyList = new MBBindingList<GameMenuPartyItemVM>();
			this.DefenderPartyList = new MBBindingList<GameMenuPartyItemVM>();
			base.CurrentOverlayType = 1;
			this.AttackerMoraleHint = new BasicTooltipViewModel(() => this.GetEncounterSideMoraleTooltip(BattleSideEnum.Attacker));
			this.DefenderMoraleHint = new BasicTooltipViewModel(() => this.GetEncounterSideMoraleTooltip(BattleSideEnum.Defender));
			this.AttackerFoodHint = new BasicTooltipViewModel(() => this.GetEncounterSideFoodTooltip(BattleSideEnum.Attacker));
			this.DefenderFoodHint = new BasicTooltipViewModel(() => this.GetEncounterSideFoodTooltip(BattleSideEnum.Defender));
			this.DefenderWallHint = new BasicTooltipViewModel();
			base.IsInitializationOver = false;
			this.UpdateLists();
			this.UpdateProperties();
			base.IsInitializationOver = true;
			this.RefreshValues();
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0003FA34 File Offset: 0x0003DC34
		private void SetAttackerAndDefenderParties(out bool attackerChanged, out bool defenderChanged)
		{
			attackerChanged = false;
			defenderChanged = false;
			if (MobileParty.MainParty.MapEvent != null)
			{
				PartyBase leaderParty = MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker);
				if (leaderParty.IsSettlement)
				{
					if (this._attackerLeadingParty == null || this._attackerLeadingParty.Party != leaderParty)
					{
						attackerChanged = true;
						this._attackerLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), leaderParty.Settlement);
					}
				}
				else if (this._attackerLeadingParty == null || this._attackerLeadingParty.Party != leaderParty)
				{
					attackerChanged = true;
					this._attackerLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), leaderParty, false);
				}
				PartyBase leaderParty2 = MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender);
				if (leaderParty2.IsSettlement)
				{
					if (this._defenderLeadingParty == null || this._defenderLeadingParty.Party != leaderParty2)
					{
						defenderChanged = true;
						this._defenderLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), leaderParty2.Settlement);
						return;
					}
				}
				else if (this._defenderLeadingParty == null || this._defenderLeadingParty.Party != leaderParty2)
				{
					defenderChanged = true;
					this._defenderLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), leaderParty2, false);
					return;
				}
			}
			else
			{
				Settlement settlement = Settlement.CurrentSettlement ?? PlayerSiege.PlayerSiegeEvent.BesiegedSettlement;
				SiegeEvent siegeEvent = settlement.SiegeEvent;
				if (siegeEvent != null)
				{
					if (this._defenderLeadingParty == null || this._defenderLeadingParty.Settlement != settlement)
					{
						defenderChanged = true;
						this._defenderLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), settlement);
					}
					if (this._attackerLeadingParty == null || this._attackerLeadingParty.Party != siegeEvent.BesiegerCamp.LeaderParty.Party)
					{
						attackerChanged = true;
						this._attackerLeadingParty = new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), siegeEvent.BesiegerCamp.LeaderParty.Party, false);
					}
					this.DefenderWallHint = new BasicTooltipViewModel(() => CampaignUIHelper.GetSiegeWallTooltip(settlement.Town.GetWallLevel(), MathF.Ceiling(settlement.SettlementTotalWallHitPoints)));
					this.IsSiege = true;
					return;
				}
				Debug.FailedAssert("Encounter overlay is open but MapEvent AND SiegeEvent is null", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\EncounterMenuOverlayVM.cs", "SetAttackerAndDefenderParties", 113);
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0003FC5C File Offset: 0x0003DE5C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.AttackerBannerHint = new HintViewModel(GameTexts.FindText("str_attacker_banner", null), null);
			this.DefenderBannerHint = new HintViewModel(GameTexts.FindText("str_defender_banner", null), null);
			this.AttackerTroopNumHint = new HintViewModel(GameTexts.FindText("str_number_of_healthy_attacker_soldiers", null), null);
			this.DefenderTroopNumHint = new HintViewModel(GameTexts.FindText("str_number_of_healthy_defender_soldiers", null), null);
			base.ContextList.Add(new StringItemWithEnabledAndHintVM(new Action<object>(base.ExecuteTroopAction), GameTexts.FindText("str_menu_overlay_context_list", GameMenuOverlay.MenuOverlayContextList.Encyclopedia.ToString()).ToString(), true, GameMenuOverlay.MenuOverlayContextList.Encyclopedia, null));
			this.AttackerPartyList.ApplyActionOnAllItems(delegate(GameMenuPartyItemVM x)
			{
				x.RefreshValues();
			});
			this.DefenderPartyList.ApplyActionOnAllItems(delegate(GameMenuPartyItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0003FD64 File Offset: 0x0003DF64
		public override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (MobileParty.MainParty.MapEvent != null && this.AttackerPartyList.Count + this.DefenderPartyList.Count != MobileParty.MainParty.MapEvent.InvolvedParties.Count<PartyBase>())
			{
				this.UpdateLists();
			}
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x0003FDB7 File Offset: 0x0003DFB7
		public override void Refresh()
		{
			base.IsInitializationOver = false;
			this.UpdateLists();
			this.UpdateProperties();
			base.IsInitializationOver = true;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
		private void UpdateProperties()
		{
			if (this.IsSiege)
			{
				GameMenuPartyItemVM defenderLeadingParty = this._defenderLeadingParty;
				bool flag = ((defenderLeadingParty != null) ? defenderLeadingParty.Settlement : null) != null;
				float num = 0f;
				float num2 = 0f;
				if (flag)
				{
					ValueTuple<int, int> townFoodAndMarketStocks = TownHelpers.GetTownFoodAndMarketStocks((flag ? this._defenderLeadingParty : this._attackerLeadingParty).Settlement.Town);
					num2 = (float)(townFoodAndMarketStocks.Item1 + townFoodAndMarketStocks.Item2) / -this._defenderLeadingParty.Settlement.Town.FoodChangeWithoutMarketStocks;
				}
				foreach (GameMenuPartyItemVM gameMenuPartyItemVM in this.DefenderPartyList)
				{
					num += gameMenuPartyItemVM.Party.MobileParty.Morale;
					if (!flag)
					{
						num2 += gameMenuPartyItemVM.Party.MobileParty.Food / -gameMenuPartyItemVM.Party.MobileParty.FoodChange;
					}
				}
				num /= (float)this.DefenderPartyList.Count;
				if (!flag)
				{
					num2 /= (float)this.AttackerPartyList.Count;
				}
				num2 = (float)Math.Max((int)Math.Ceiling((double)num2), 0);
				MBTextManager.SetTextVariable("DAY_NUM", num2.ToString(), false);
				MBTextManager.SetTextVariable("PLURAL", (num2 > 1f) ? 1 : 0);
				this.DefenderPartyFood = GameTexts.FindText("str_party_food_left", null).ToString();
				this.DefenderPartyMorale = num.ToString("0.0");
				num = 0f;
				num2 = 0f;
				if (!flag)
				{
					if (this._attackerLeadingParty.Settlement != null)
					{
						num2 = this._attackerLeadingParty.Settlement.Town.FoodStocks / this._attackerLeadingParty.Settlement.Town.FoodChangeWithoutMarketStocks;
					}
					else if (this._attackerLeadingParty.Party.MobileParty.CurrentSettlement != null)
					{
						num2 = this._attackerLeadingParty.Party.MobileParty.CurrentSettlement.Town.FoodStocks / this._attackerLeadingParty.Party.MobileParty.CurrentSettlement.Town.FoodChangeWithoutMarketStocks;
					}
					else
					{
						Settlement currentSettlement = Settlement.CurrentSettlement;
						if (((currentSettlement != null) ? currentSettlement.SiegeEvent : null) != null)
						{
							num2 = Settlement.CurrentSettlement.Town.FoodStocks / Settlement.CurrentSettlement.Town.FoodChangeWithoutMarketStocks;
						}
						else
						{
							Debug.FailedAssert("There are no settlements involved in the siege", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\EncounterMenuOverlayVM.cs", "UpdateProperties", 207);
						}
					}
				}
				else
				{
					Settlement currentSettlement2 = Settlement.CurrentSettlement;
					if (((currentSettlement2 != null) ? currentSettlement2.SiegeEvent : null) != null)
					{
						num2 = Settlement.CurrentSettlement.Town.FoodStocks / Settlement.CurrentSettlement.Town.FoodChangeWithoutMarketStocks;
					}
				}
				foreach (GameMenuPartyItemVM gameMenuPartyItemVM2 in this.AttackerPartyList)
				{
					num += gameMenuPartyItemVM2.Party.MobileParty.Morale;
					if (flag)
					{
						num2 += gameMenuPartyItemVM2.Party.MobileParty.Food / -gameMenuPartyItemVM2.Party.MobileParty.FoodChange;
					}
				}
				num /= (float)this.AttackerPartyList.Count;
				if (flag)
				{
					num2 /= (float)this.AttackerPartyList.Count;
				}
				num2 = (float)Math.Max((int)Math.Ceiling((double)num2), 0);
				MBTextManager.SetTextVariable("DAY_NUM", num2.ToString(), false);
				MBTextManager.SetTextVariable("PLURAL", (num2 > 1f) ? 1 : 0);
				this.AttackerPartyFood = GameTexts.FindText("str_party_food_left", null).ToString();
				this.AttackerPartyMorale = num.ToString("0.0");
				Settlement settlement;
				if ((settlement = Settlement.CurrentSettlement) == null)
				{
					SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
					settlement = ((playerSiegeEvent != null) ? playerSiegeEvent.BesiegedSettlement : null);
				}
				Settlement settlement2 = settlement;
				if (settlement2 != null)
				{
					this.DefenderWallHitPoints = MathF.Ceiling(settlement2.SettlementTotalWallHitPoints).ToString();
				}
			}
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000401BC File Offset: 0x0003E3BC
		private void UpdateLists()
		{
			if (MobileParty.MainParty.MapEvent == null)
			{
				Settlement settlement;
				if ((settlement = Settlement.CurrentSettlement) == null)
				{
					SiegeEvent playerSiegeEvent = PlayerSiege.PlayerSiegeEvent;
					settlement = ((playerSiegeEvent != null) ? playerSiegeEvent.BesiegedSettlement : null);
				}
				if (settlement == null)
				{
					return;
				}
			}
			bool flag;
			bool flag2;
			this.SetAttackerAndDefenderParties(out flag, out flag2);
			if (this._defenderLeadingParty != null && flag2)
			{
				this.DefenderPartyList.Insert(0, this._defenderLeadingParty);
			}
			if (this._attackerLeadingParty != null && flag)
			{
				this.AttackerPartyList.Insert(0, this._attackerLeadingParty);
			}
			List<PartyBase> list = new List<PartyBase>();
			List<PartyBase> list2 = new List<PartyBase>();
			List<int> list3 = new List<int>();
			List<int> list4 = new List<int>();
			List<PartyBase> list5 = new List<PartyBase>();
			if (MobileParty.MainParty.MapEvent != null)
			{
				list5.AddRange(MobileParty.MainParty.MapEvent.InvolvedParties);
				this.IsSiege = false;
			}
			else
			{
				Settlement settlement2 = Settlement.CurrentSettlement ?? PlayerSiege.PlayerSiegeEvent.BesiegedSettlement;
				if (settlement2.SiegeEvent == null)
				{
					this.PowerComparer = new PowerLevelComparer(1.0, 1.0);
					return;
				}
				SiegeEvent siegeEvent = settlement2.SiegeEvent;
				list5.AddRange(siegeEvent.GetInvolvedPartiesForEventType(MapEvent.BattleTypes.Siege));
				this.IsSiege = true;
			}
			foreach (PartyBase partyBase in list5)
			{
				bool flag3;
				if (MobileParty.MainParty.MapEvent != null)
				{
					flag3 = (partyBase.Side == BattleSideEnum.Defender);
				}
				else
				{
					flag3 = (Settlement.CurrentSettlement ?? PlayerSiege.PlayerSiegeEvent.BesiegedSettlement).SiegeEvent.GetSiegeEventSide(BattleSideEnum.Defender).HasInvolvedPartyForEventType(partyBase, MapEvent.BattleTypes.Siege);
				}
				List<PartyBase> list6 = flag3 ? list2 : list;
				List<int> list7 = flag3 ? list4 : list3;
				if (partyBase.IsActive && partyBase.MemberRoster.Count > 0)
				{
					int numberOfHealthyMembers = partyBase.NumberOfHealthyMembers;
					int num = 0;
					while (num < list7.Count && numberOfHealthyMembers <= list7[num])
					{
						num++;
					}
					list7.Add(partyBase.NumberOfHealthyMembers);
					list6.Insert(num, partyBase);
				}
			}
			if (this.PowerComparer == null)
			{
				this.PowerComparer = new PowerLevelComparer((double)list2.Sum((PartyBase party) => party.TotalStrength), (double)list.Sum((PartyBase party) => party.TotalStrength));
			}
			else
			{
				float num2 = list2.Sum((PartyBase party) => party.TotalStrength);
				float num3 = list.Sum((PartyBase party) => party.TotalStrength);
				this.PowerComparer.Update((double)num2, (double)num3, (double)num2, (double)num3);
			}
			List<PartyBase> list8 = (from p in list
			orderby p.NumberOfAllMembers descending
			select p).ToList<PartyBase>();
			List<PartyBase> list9 = (from enemy in this.AttackerPartyList
			select enemy.Party).ToList<PartyBase>();
			List<PartyBase> list10 = list8.Except(list9).ToList<PartyBase>();
			list10.Remove(this._attackerLeadingParty.Party);
			foreach (PartyBase partyBase2 in list9.Except(list8).ToList<PartyBase>())
			{
				for (int i = this.AttackerPartyList.Count - 1; i >= 0; i--)
				{
					if (this.AttackerPartyList[i].Party == partyBase2)
					{
						this.AttackerPartyList.RemoveAt(i);
					}
				}
			}
			if (this.IsSiege)
			{
				list10 = (from x in list10
				where x.MemberRoster.TotalHealthyCount > 0
				select x).ToList<PartyBase>();
			}
			foreach (PartyBase item in list10)
			{
				this.AttackerPartyList.Add(new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), item, false));
			}
			List<PartyBase> list11 = (from p in list2
			orderby p.NumberOfAllMembers descending
			select p).ToList<PartyBase>();
			List<PartyBase> list12 = (from ally in this.DefenderPartyList
			select ally.Party).ToList<PartyBase>();
			List<PartyBase> list13 = list11.Except(list12).ToList<PartyBase>();
			list13.Remove(this._defenderLeadingParty.Party);
			foreach (PartyBase partyBase3 in list12.Except(list11).ToList<PartyBase>())
			{
				for (int j = this.DefenderPartyList.Count - 1; j >= 0; j--)
				{
					if (this.DefenderPartyList[j].Party == partyBase3)
					{
						this.DefenderPartyList.RemoveAt(j);
					}
				}
			}
			if (this.IsSiege)
			{
				list13 = (from x in list13
				where x.MemberRoster.TotalHealthyCount > 0
				select x).ToList<PartyBase>();
			}
			foreach (PartyBase item2 in list13)
			{
				this.DefenderPartyList.Add(new GameMenuPartyItemVM(new Action<GameMenuPartyItemVM>(this.ExecuteOnSetAsActiveContextMenuItem), item2, false));
			}
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM in this.DefenderPartyList)
			{
				gameMenuPartyItemVM.RefreshProperties();
			}
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM2 in this.AttackerPartyList)
			{
				gameMenuPartyItemVM2.RefreshProperties();
			}
			this.DefenderPartyCount = 0;
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM3 in this.DefenderPartyList)
			{
				if (gameMenuPartyItemVM3.Party != null)
				{
					this.DefenderPartyCount += gameMenuPartyItemVM3.Party.NumberOfHealthyMembers;
				}
			}
			this.AttackerPartyCount = 0;
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM4 in this.AttackerPartyList)
			{
				if (gameMenuPartyItemVM4.Party != null)
				{
					this.AttackerPartyCount += gameMenuPartyItemVM4.Party.NumberOfHealthyMembers;
				}
			}
			this.DefenderPartyCountLbl = this.DefenderPartyCount.ToString();
			this.AttackerPartyCountLbl = this.AttackerPartyCount.ToString();
			if (MobileParty.MainParty.MapEvent != null)
			{
				PartyBase leaderParty = MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Attacker);
				PartyBase leaderParty2 = MobileParty.MainParty.MapEvent.GetLeaderParty(BattleSideEnum.Defender);
				if (this._attackerLeadingParty.Party == leaderParty2 || this._defenderLeadingParty.Party == leaderParty)
				{
					GameMenuPartyItemVM attackerLeadingParty = this._attackerLeadingParty;
					this._attackerLeadingParty = this._defenderLeadingParty;
					this._defenderLeadingParty = attackerLeadingParty;
				}
			}
			this.TitleText = (this.IsSiege ? GameTexts.FindText("str_siege", null).ToString() : (this.TitleText = GameTexts.FindText("str_battle", null).ToString()));
			IFaction faction = (this._defenderLeadingParty.Party == null) ? this._defenderLeadingParty.Settlement.MapFaction : this._defenderLeadingParty.Party.MapFaction;
			IFaction faction2 = (this._attackerLeadingParty.Party == null) ? this._attackerLeadingParty.Settlement.MapFaction : this._attackerLeadingParty.Party.MapFaction;
			Banner banner = (this._defenderLeadingParty.Party == null) ? this._defenderLeadingParty.Settlement.OwnerClan.Banner : this._defenderLeadingParty.Party.Banner;
			Banner banner2 = (this._attackerLeadingParty.Party == null) ? this._attackerLeadingParty.Settlement.OwnerClan.Banner : this._attackerLeadingParty.Party.Banner;
			this.DefenderPartyBanner = new ImageIdentifierVM(BannerCode.CreateFrom(banner), true);
			this.AttackerPartyBanner = new ImageIdentifierVM(BannerCode.CreateFrom(banner2), true);
			string defenderColor;
			if (faction != null && faction is Kingdom)
			{
				defenderColor = Color.FromUint(((Kingdom)faction).PrimaryBannerColor).ToString();
			}
			else
			{
				uint? num4;
				if (faction == null)
				{
					num4 = null;
				}
				else
				{
					Banner banner3 = faction.Banner;
					num4 = ((banner3 != null) ? new uint?(banner3.GetPrimaryColor()) : null);
				}
				defenderColor = Color.FromUint(num4 ?? Color.White.ToUnsignedInteger()).ToString();
			}
			string attackerColor;
			if (faction2 != null && faction2 is Kingdom)
			{
				attackerColor = Color.FromUint(((Kingdom)faction2).PrimaryBannerColor).ToString();
			}
			else
			{
				uint? num5;
				if (faction2 == null)
				{
					num5 = null;
				}
				else
				{
					Banner banner4 = faction2.Banner;
					num5 = ((banner4 != null) ? new uint?(banner4.GetPrimaryColor()) : null);
				}
				attackerColor = Color.FromUint(num5 ?? Color.White.ToUnsignedInteger()).ToString();
			}
			this.PowerComparer.SetColors(defenderColor, attackerColor);
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00040C10 File Offset: 0x0003EE10
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x00040C18 File Offset: 0x0003EE18
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00040C3B File Offset: 0x0003EE3B
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x00040C43 File Offset: 0x0003EE43
		[DataSourceProperty]
		public ImageIdentifierVM DefenderPartyBanner
		{
			get
			{
				return this._defenderPartyBanner;
			}
			set
			{
				if (value != this._defenderPartyBanner)
				{
					this._defenderPartyBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "DefenderPartyBanner");
				}
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00040C61 File Offset: 0x0003EE61
		// (set) Token: 0x06001061 RID: 4193 RVA: 0x00040C69 File Offset: 0x0003EE69
		[DataSourceProperty]
		public ImageIdentifierVM AttackerPartyBanner
		{
			get
			{
				return this._attackerPartyBanner;
			}
			set
			{
				if (value != this._attackerPartyBanner)
				{
					this._attackerPartyBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "AttackerPartyBanner");
				}
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00040C87 File Offset: 0x0003EE87
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x00040C8F File Offset: 0x0003EE8F
		[DataSourceProperty]
		public PowerLevelComparer PowerComparer
		{
			get
			{
				return this._powerComparer;
			}
			set
			{
				if (value != this._powerComparer)
				{
					this._powerComparer = value;
					base.OnPropertyChangedWithValue<PowerLevelComparer>(value, "PowerComparer");
				}
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00040CAD File Offset: 0x0003EEAD
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00040CB5 File Offset: 0x0003EEB5
		[DataSourceProperty]
		public MBBindingList<GameMenuPartyItemVM> AttackerPartyList
		{
			get
			{
				return this._attackerPartyList;
			}
			set
			{
				if (value != this._attackerPartyList)
				{
					this._attackerPartyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPartyItemVM>>(value, "AttackerPartyList");
				}
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00040CD3 File Offset: 0x0003EED3
		// (set) Token: 0x06001067 RID: 4199 RVA: 0x00040CDB File Offset: 0x0003EEDB
		[DataSourceProperty]
		public MBBindingList<GameMenuPartyItemVM> DefenderPartyList
		{
			get
			{
				return this._defenderPartyList;
			}
			set
			{
				if (value != this._defenderPartyList)
				{
					this._defenderPartyList = value;
					base.OnPropertyChangedWithValue<MBBindingList<GameMenuPartyItemVM>>(value, "DefenderPartyList");
				}
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001068 RID: 4200 RVA: 0x00040CF9 File Offset: 0x0003EEF9
		// (set) Token: 0x06001069 RID: 4201 RVA: 0x00040D01 File Offset: 0x0003EF01
		[DataSourceProperty]
		public string DefenderPartyMorale
		{
			get
			{
				return this._defenderPartyMorale;
			}
			set
			{
				if (value != this._defenderPartyMorale)
				{
					this._defenderPartyMorale = value;
					base.OnPropertyChangedWithValue<string>(value, "DefenderPartyMorale");
				}
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x00040D24 File Offset: 0x0003EF24
		// (set) Token: 0x0600106B RID: 4203 RVA: 0x00040D2C File Offset: 0x0003EF2C
		[DataSourceProperty]
		public string AttackerPartyMorale
		{
			get
			{
				return this._attackerPartyMorale;
			}
			set
			{
				if (value != this._attackerPartyMorale)
				{
					this._attackerPartyMorale = value;
					base.OnPropertyChangedWithValue<string>(value, "AttackerPartyMorale");
				}
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00040D4F File Offset: 0x0003EF4F
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x00040D57 File Offset: 0x0003EF57
		[DataSourceProperty]
		public int DefenderPartyCount
		{
			get
			{
				return this._defenderPartyCount;
			}
			set
			{
				if (value != this._defenderPartyCount)
				{
					this._defenderPartyCount = value;
					base.OnPropertyChangedWithValue(value, "DefenderPartyCount");
				}
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00040D75 File Offset: 0x0003EF75
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x00040D7D File Offset: 0x0003EF7D
		[DataSourceProperty]
		public int AttackerPartyCount
		{
			get
			{
				return this._attackerPartyCount;
			}
			set
			{
				if (value != this._attackerPartyCount)
				{
					this._attackerPartyCount = value;
					base.OnPropertyChangedWithValue(value, "AttackerPartyCount");
				}
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00040D9B File Offset: 0x0003EF9B
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x00040DA3 File Offset: 0x0003EFA3
		[DataSourceProperty]
		public string DefenderPartyFood
		{
			get
			{
				return this._defenderPartyFood;
			}
			set
			{
				if (value != this._defenderPartyFood)
				{
					this._defenderPartyFood = value;
					base.OnPropertyChangedWithValue<string>(value, "DefenderPartyFood");
				}
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x00040DC6 File Offset: 0x0003EFC6
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x00040DCE File Offset: 0x0003EFCE
		[DataSourceProperty]
		public string AttackerPartyFood
		{
			get
			{
				return this._attackerPartyFood;
			}
			set
			{
				if (value != this._attackerPartyFood)
				{
					this._attackerPartyFood = value;
					base.OnPropertyChangedWithValue<string>(value, "AttackerPartyFood");
				}
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x00040DF1 File Offset: 0x0003EFF1
		// (set) Token: 0x06001075 RID: 4213 RVA: 0x00040DF9 File Offset: 0x0003EFF9
		public string DefenderWallHitPoints
		{
			get
			{
				return this._defenderWallHitPoints;
			}
			set
			{
				if (value != this._defenderWallHitPoints)
				{
					this._defenderWallHitPoints = value;
					base.OnPropertyChangedWithValue<string>(value, "DefenderWallHitPoints");
				}
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x00040E1C File Offset: 0x0003F01C
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x00040E24 File Offset: 0x0003F024
		[DataSourceProperty]
		public bool IsSiege
		{
			get
			{
				return this._isSiege;
			}
			set
			{
				if (value != this._isSiege)
				{
					this._isSiege = value;
					base.OnPropertyChangedWithValue(value, "IsSiege");
				}
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00040E42 File Offset: 0x0003F042
		// (set) Token: 0x06001079 RID: 4217 RVA: 0x00040E4A File Offset: 0x0003F04A
		[DataSourceProperty]
		public string DefenderPartyCountLbl
		{
			get
			{
				return this._defenderPartyCountLbl;
			}
			set
			{
				if (value != this._defenderPartyCountLbl)
				{
					this._defenderPartyCountLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DefenderPartyCountLbl");
				}
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600107A RID: 4218 RVA: 0x00040E6D File Offset: 0x0003F06D
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x00040E75 File Offset: 0x0003F075
		[DataSourceProperty]
		public string AttackerPartyCountLbl
		{
			get
			{
				return this._attackerPartyCountLbl;
			}
			set
			{
				if (value != this._attackerPartyCountLbl)
				{
					this._attackerPartyCountLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "AttackerPartyCountLbl");
				}
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600107C RID: 4220 RVA: 0x00040E98 File Offset: 0x0003F098
		// (set) Token: 0x0600107D RID: 4221 RVA: 0x00040EA0 File Offset: 0x0003F0A0
		[DataSourceProperty]
		public HintViewModel AttackerBannerHint
		{
			get
			{
				return this._attackerBannerHint;
			}
			set
			{
				if (value != this._attackerBannerHint)
				{
					this._attackerBannerHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AttackerBannerHint");
				}
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00040EBE File Offset: 0x0003F0BE
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00040EC6 File Offset: 0x0003F0C6
		[DataSourceProperty]
		public HintViewModel DefenderBannerHint
		{
			get
			{
				return this._defenderBannerHint;
			}
			set
			{
				if (value != this._defenderBannerHint)
				{
					this._defenderBannerHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DefenderBannerHint");
				}
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x00040EE4 File Offset: 0x0003F0E4
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x00040EEC File Offset: 0x0003F0EC
		[DataSourceProperty]
		public HintViewModel AttackerTroopNumHint
		{
			get
			{
				return this._attackerTroopNumHint;
			}
			set
			{
				if (value != this._attackerTroopNumHint)
				{
					this._attackerTroopNumHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AttackerTroopNumHint");
				}
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00040F0A File Offset: 0x0003F10A
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x00040F12 File Offset: 0x0003F112
		[DataSourceProperty]
		public HintViewModel DefenderTroopNumHint
		{
			get
			{
				return this._defenderTroopNumHint;
			}
			set
			{
				if (value != this._defenderTroopNumHint)
				{
					this._defenderTroopNumHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "DefenderTroopNumHint");
				}
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x00040F30 File Offset: 0x0003F130
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x00040F38 File Offset: 0x0003F138
		[DataSourceProperty]
		public BasicTooltipViewModel DefenderWallHint
		{
			get
			{
				return this._defenderWallHint;
			}
			set
			{
				if (value != this._defenderWallHint)
				{
					this._defenderWallHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DefenderWallHint");
				}
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00040F56 File Offset: 0x0003F156
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x00040F5E File Offset: 0x0003F15E
		[DataSourceProperty]
		public BasicTooltipViewModel DefenderFoodHint
		{
			get
			{
				return this._defenderFoodHint;
			}
			set
			{
				if (value != this._defenderFoodHint)
				{
					this._defenderFoodHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DefenderFoodHint");
				}
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00040F7C File Offset: 0x0003F17C
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00040F84 File Offset: 0x0003F184
		[DataSourceProperty]
		public BasicTooltipViewModel AttackerFoodHint
		{
			get
			{
				return this._attackerFoodHint;
			}
			set
			{
				if (value != this._attackerFoodHint)
				{
					this._attackerFoodHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "AttackerFoodHint");
				}
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00040FA2 File Offset: 0x0003F1A2
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x00040FAA File Offset: 0x0003F1AA
		[DataSourceProperty]
		public BasicTooltipViewModel AttackerMoraleHint
		{
			get
			{
				return this._attackerMoraleHint;
			}
			set
			{
				if (value != this._attackerMoraleHint)
				{
					this._attackerMoraleHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "AttackerMoraleHint");
				}
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00040FC8 File Offset: 0x0003F1C8
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x00040FD0 File Offset: 0x0003F1D0
		[DataSourceProperty]
		public BasicTooltipViewModel DefenderMoraleHint
		{
			get
			{
				return this._defenderMoraleHint;
			}
			set
			{
				if (value != this._defenderMoraleHint)
				{
					this._defenderMoraleHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "DefenderMoraleHint");
				}
			}
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		private List<TooltipProperty> GetEncounterSideFoodTooltip(BattleSideEnum side)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			GameMenuPartyItemVM defenderLeadingParty = this._defenderLeadingParty;
			bool flag = ((defenderLeadingParty != null) ? defenderLeadingParty.Settlement : null) != null;
			bool flag2 = (flag && flag && side == BattleSideEnum.Defender) || (!flag && side == BattleSideEnum.Attacker);
			if (this.IsSiege && flag2)
			{
				list.Add(new TooltipProperty(new TextObject("{=OSsSBHKe}Settlement's Food", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.Title));
				GameMenuPartyItemVM gameMenuPartyItemVM = flag ? this._defenderLeadingParty : this._attackerLeadingParty;
				Town town;
				if (gameMenuPartyItemVM == null)
				{
					town = null;
				}
				else
				{
					Settlement settlement = gameMenuPartyItemVM.Settlement;
					town = ((settlement != null) ? settlement.Town : null);
				}
				Town town2 = town;
				float foodChange = (town2 != null) ? town2.FoodChangeWithoutMarketStocks : 0f;
				ValueTuple<int, int> townFoodAndMarketStocks = TownHelpers.GetTownFoodAndMarketStocks(town2);
				list.Add(new TooltipProperty(new TextObject("{=EkFDvG7z}Settlement Food Stocks", null).ToString(), townFoodAndMarketStocks.Item1.ToString("F0"), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				if (townFoodAndMarketStocks.Item2 != 0)
				{
					list.Add(new TooltipProperty(new TextObject("{=HTtWslIx}Market Food Stocks", null).ToString(), townFoodAndMarketStocks.Item2.ToString("F0"), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
				list.Add(new TooltipProperty(new TextObject("{=laznt9ZK}Settlement Food Change", null).ToString(), foodChange.ToString("F2"), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				list.Add(new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
				list.Add(new TooltipProperty(new TextObject("{=DNXD37JL}Settlement's Days Until Food Runs Out", null).ToString(), CampaignUIHelper.GetDaysUntilNoFood((float)(townFoodAndMarketStocks.Item1 + townFoodAndMarketStocks.Item2), foodChange), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				if (((town2 != null) ? town2.Settlement : null) != null && SettlementHelper.IsGarrisonStarving(town2.Settlement))
				{
					list.Add(new TooltipProperty(new TextObject("{=0rmpC7jf}The Garrison is Starving", null).ToString(), string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
			}
			else
			{
				list.Add(new TooltipProperty(new TextObject("{=Q8dhryRX}Parties' Food", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.Title));
				MBBindingList<GameMenuPartyItemVM> mbbindingList = (side == BattleSideEnum.Attacker) ? this.AttackerPartyList : this.DefenderPartyList;
				double num = 0.0;
				foreach (GameMenuPartyItemVM gameMenuPartyItemVM2 in mbbindingList)
				{
					float val = gameMenuPartyItemVM2.Party.MobileParty.Food / -gameMenuPartyItemVM2.Party.MobileParty.FoodChange;
					num += (double)Math.Max(val, 0f);
					string daysUntilNoFood = CampaignUIHelper.GetDaysUntilNoFood(gameMenuPartyItemVM2.Party.MobileParty.Food, gameMenuPartyItemVM2.Party.MobileParty.FoodChange);
					list.Add(new TooltipProperty(gameMenuPartyItemVM2.Party.MobileParty.Name.ToString(), daysUntilNoFood, 0, false, TooltipProperty.TooltipPropertyFlags.None));
				}
				list.Add(new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
				list.Add(new TooltipProperty(new TextObject("{=rwKBR4NE}Average Days Until Food Runs Out", null).ToString(), MathF.Ceiling(num / (double)mbbindingList.Count).ToString(), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			}
			return list;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00041334 File Offset: 0x0003F534
		private List<TooltipProperty> GetEncounterSideMoraleTooltip(BattleSideEnum side)
		{
			List<TooltipProperty> list = new List<TooltipProperty>();
			list.Add(new TooltipProperty(new TextObject("{=QBB0KQ2Z}Parties' Average Morale", null).ToString(), "", 0, false, TooltipProperty.TooltipPropertyFlags.Title));
			MBBindingList<GameMenuPartyItemVM> mbbindingList = (side == BattleSideEnum.Attacker) ? this.AttackerPartyList : this.DefenderPartyList;
			double num = 0.0;
			foreach (GameMenuPartyItemVM gameMenuPartyItemVM in mbbindingList)
			{
				list.Add(new TooltipProperty(gameMenuPartyItemVM.Party.MobileParty.Name.ToString(), gameMenuPartyItemVM.Party.MobileParty.Morale.ToString("0.0"), 0, false, TooltipProperty.TooltipPropertyFlags.None));
				num += (double)gameMenuPartyItemVM.Party.MobileParty.Morale;
			}
			list.Add(new TooltipProperty("", string.Empty, 0, false, TooltipProperty.TooltipPropertyFlags.RundownSeperator));
			list.Add(new TooltipProperty(new TextObject("{=eoVW9z54}Average Morale", null).ToString(), (num / (double)mbbindingList.Count).ToString("0.0"), 0, false, TooltipProperty.TooltipPropertyFlags.None));
			return list;
		}

		// Token: 0x04000794 RID: 1940
		private GameMenuPartyItemVM _defenderLeadingParty;

		// Token: 0x04000795 RID: 1941
		private GameMenuPartyItemVM _attackerLeadingParty;

		// Token: 0x04000796 RID: 1942
		private string _titleText;

		// Token: 0x04000797 RID: 1943
		private ImageIdentifierVM _defenderPartyBanner;

		// Token: 0x04000798 RID: 1944
		private ImageIdentifierVM _attackerPartyBanner;

		// Token: 0x04000799 RID: 1945
		private MBBindingList<GameMenuPartyItemVM> _attackerPartyList;

		// Token: 0x0400079A RID: 1946
		private MBBindingList<GameMenuPartyItemVM> _defenderPartyList;

		// Token: 0x0400079B RID: 1947
		private string _attackerPartyMorale;

		// Token: 0x0400079C RID: 1948
		private string _defenderPartyMorale;

		// Token: 0x0400079D RID: 1949
		private int _attackerPartyCount;

		// Token: 0x0400079E RID: 1950
		private int _defenderPartyCount;

		// Token: 0x0400079F RID: 1951
		private string _attackerPartyFood;

		// Token: 0x040007A0 RID: 1952
		private string _defenderPartyFood;

		// Token: 0x040007A1 RID: 1953
		private string _defenderWallHitPoints;

		// Token: 0x040007A2 RID: 1954
		private string _defenderPartyCountLbl;

		// Token: 0x040007A3 RID: 1955
		private string _attackerPartyCountLbl;

		// Token: 0x040007A4 RID: 1956
		private bool _isSiege;

		// Token: 0x040007A5 RID: 1957
		private PowerLevelComparer _powerComparer;

		// Token: 0x040007A6 RID: 1958
		private HintViewModel _attackerBannerHint;

		// Token: 0x040007A7 RID: 1959
		private HintViewModel _defenderBannerHint;

		// Token: 0x040007A8 RID: 1960
		private HintViewModel _attackerTroopNumHint;

		// Token: 0x040007A9 RID: 1961
		private HintViewModel _defenderTroopNumHint;

		// Token: 0x040007AA RID: 1962
		private BasicTooltipViewModel _defenderWallHint;

		// Token: 0x040007AB RID: 1963
		private BasicTooltipViewModel _defenderFoodHint;

		// Token: 0x040007AC RID: 1964
		private BasicTooltipViewModel _attackerFoodHint;

		// Token: 0x040007AD RID: 1965
		private BasicTooltipViewModel _attackerMoraleHint;

		// Token: 0x040007AE RID: 1966
		private BasicTooltipViewModel _defenderMoraleHint;
	}
}
