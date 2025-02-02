using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay
{
	// Token: 0x020000A6 RID: 166
	public class GameMenuOverlay : ViewModel
	{
		// Token: 0x06001094 RID: 4244 RVA: 0x00041490 File Offset: 0x0003F690
		public GameMenuOverlay()
		{
			this.ContextList = new MBBindingList<StringItemWithEnabledAndHintVM>();
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000414B1 File Offset: 0x0003F6B1
		public override void RefreshValues()
		{
			base.RefreshValues();
			GameMenuPartyItemVM contextMenuItem = this._contextMenuItem;
			if (contextMenuItem == null)
			{
				return;
			}
			contextMenuItem.RefreshValues();
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000414C9 File Offset: 0x0003F6C9
		protected virtual void ExecuteOnSetAsActiveContextMenuItem(GameMenuPartyItemVM troop)
		{
			this._contextMenuItem = troop;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000414D2 File Offset: 0x0003F6D2
		public virtual void ExecuteOnOverlayClosed()
		{
			if (!this._closedHandled)
			{
				CampaignEventDispatcher.Instance.OnCharacterPortraitPopUpClosed();
				this._closedHandled = true;
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000414ED File Offset: 0x0003F6ED
		public virtual void ExecuteOnOverlayOpened()
		{
			this._closedHandled = false;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000414F6 File Offset: 0x0003F6F6
		public override void OnFinalize()
		{
			base.OnFinalize();
			if (!this._closedHandled)
			{
				this.ExecuteOnOverlayClosed();
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0004150C File Offset: 0x0003F70C
		protected void ExecuteTroopAction(object o)
		{
			switch ((GameMenuOverlay.MenuOverlayContextList)o)
			{
			case GameMenuOverlay.MenuOverlayContextList.Encyclopedia:
				if (this._contextMenuItem.Character != null)
				{
					if (this._contextMenuItem.Character.IsHero)
					{
						Campaign.Current.EncyclopediaManager.GoToLink(this._contextMenuItem.Character.HeroObject.EncyclopediaLink);
					}
					else
					{
						Debug.FailedAssert("Character object in menu overlay", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\GameMenuOverlay.cs", "ExecuteTroopAction", 101);
						Campaign.Current.EncyclopediaManager.GoToLink(this._contextMenuItem.Character.EncyclopediaLink);
					}
				}
				else if (this._contextMenuItem.Party != null)
				{
					CharacterObject visualPartyLeader = CampaignUIHelper.GetVisualPartyLeader(this._contextMenuItem.Party);
					if (visualPartyLeader != null)
					{
						Campaign.Current.EncyclopediaManager.GoToLink(visualPartyLeader.EncyclopediaLink);
					}
				}
				else if (this._contextMenuItem.Settlement != null)
				{
					Campaign.Current.EncyclopediaManager.GoToLink(this._contextMenuItem.Settlement.EncyclopediaLink);
				}
				break;
			case GameMenuOverlay.MenuOverlayContextList.Conversation:
				if (this._contextMenuItem.Character != null)
				{
					if (this._contextMenuItem.Character.IsHero)
					{
						if (PlayerEncounter.Current != null || LocationComplex.Current != null || Campaign.Current.CurrentMenuContext != null)
						{
							Location location = LocationComplex.Current.GetLocationOfCharacter(this._contextMenuItem.Character.HeroObject);
							if (location.StringId == "alley")
							{
								location = LocationComplex.Current.GetLocationWithId("center");
							}
							CampaignEventDispatcher.Instance.OnPlayerStartTalkFromMenu(this._contextMenuItem.Character.HeroObject);
							PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(location, null, this._contextMenuItem.Character, null);
						}
						else
						{
							EncounterManager.StartPartyEncounter(PartyBase.MainParty, this._contextMenuItem.Character.HeroObject.PartyBelongedTo.Party);
						}
					}
					else
					{
						Debug.FailedAssert("Character object in menu overlay", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\GameMenuOverlay.cs", "ExecuteTroopAction", 145);
					}
				}
				break;
			case GameMenuOverlay.MenuOverlayContextList.QuickConversation:
				if (this._contextMenuItem.Character != null)
				{
					if (this._contextMenuItem.Character.IsHero)
					{
						if (PlayerEncounter.Current != null || LocationComplex.Current != null || Campaign.Current.CurrentMenuContext != null)
						{
							CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, null, false, false, false, true, false, false), new ConversationCharacterData(this._contextMenuItem.Character, null, false, false, false, true, false, false));
						}
						else
						{
							EncounterManager.StartPartyEncounter(PartyBase.MainParty, this._contextMenuItem.Character.HeroObject.PartyBelongedTo.Party);
						}
					}
					else
					{
						Debug.FailedAssert("Character object in menu overlay", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\GameMenuOverlay.cs", "ExecuteTroopAction", 168);
					}
				}
				break;
			case GameMenuOverlay.MenuOverlayContextList.ConverseWithLeader:
			{
				PartyBase party = this._contextMenuItem.Party;
				if (((party != null) ? party.LeaderHero : null) != null)
				{
					if (Settlement.CurrentSettlement != null || LocationComplex.Current != null || Campaign.Current.CurrentMenuContext != null)
					{
						this.ConverseWithLeader(PartyBase.MainParty, this._contextMenuItem.Party);
					}
					else
					{
						EncounterManager.StartPartyEncounter(PartyBase.MainParty, this._contextMenuItem.Party);
					}
				}
				break;
			}
			case GameMenuOverlay.MenuOverlayContextList.ArmyDismiss:
			{
				PartyBase party2 = this._contextMenuItem.Party;
				if (((party2 != null) ? party2.MobileParty.Army : null) != null && this._contextMenuItem.Party.MapEvent == null && this._contextMenuItem.Party.MobileParty.Army.LeaderParty != this._contextMenuItem.Party.MobileParty)
				{
					if (this._contextMenuItem.Party.MobileParty.Army.LeaderParty == MobileParty.MainParty && this._contextMenuItem.Party.MobileParty.Army.Parties.Count <= 2)
					{
						DisbandArmyAction.ApplyByNotEnoughParty(this._contextMenuItem.Party.MobileParty.Army);
					}
					else
					{
						this._contextMenuItem.Party.MobileParty.Army = null;
						this._contextMenuItem.Party.MobileParty.Ai.SetMoveModeHold();
					}
				}
				break;
			}
			case GameMenuOverlay.MenuOverlayContextList.ManageGarrison:
				if (this._contextMenuItem.Party != null)
				{
					PartyScreenManager.OpenScreenAsManageTroops(this._contextMenuItem.Party.MobileParty);
				}
				break;
			case GameMenuOverlay.MenuOverlayContextList.DonateTroops:
				if (this._contextMenuItem.Party != null)
				{
					if (this._contextMenuItem.Party.MobileParty.IsGarrison)
					{
						PartyScreenManager.OpenScreenAsDonateGarrisonWithCurrentSettlement();
					}
					else
					{
						PartyScreenManager.OpenScreenAsDonateTroops(this._contextMenuItem.Party.MobileParty);
					}
				}
				break;
			case GameMenuOverlay.MenuOverlayContextList.JoinArmy:
			{
				CharacterObject character = this._contextMenuItem.Character;
				if (character != null && character.IsHero && this._contextMenuItem.Character.HeroObject.PartyBelongedTo != null)
				{
					MobileParty.MainParty.Army = this._contextMenuItem.Character.HeroObject.PartyBelongedTo.Army;
					MobileParty.MainParty.Army.AddPartyToMergedParties(MobileParty.MainParty);
					MenuContext currentMenuContext = Campaign.Current.CurrentMenuContext;
					if (currentMenuContext != null)
					{
						currentMenuContext.Refresh();
					}
				}
				break;
			}
			case GameMenuOverlay.MenuOverlayContextList.TakeToParty:
			{
				CharacterObject character2 = this._contextMenuItem.Character;
				if (character2 != null && character2.IsHero && this._contextMenuItem.Character.HeroObject.PartyBelongedTo == null)
				{
					Settlement currentSettlement = this._contextMenuItem.Character.HeroObject.CurrentSettlement;
					bool flag;
					if (currentSettlement == null)
					{
						flag = false;
					}
					else
					{
						MBReadOnlyList<Hero> notables = currentSettlement.Notables;
						bool? flag2 = (notables != null) ? new bool?(notables.Contains(this._contextMenuItem.Character.HeroObject)) : null;
						bool flag3 = true;
						flag = (flag2.GetValueOrDefault() == flag3 & flag2 != null);
					}
					if (flag)
					{
						LeaveSettlementAction.ApplyForCharacterOnly(this._contextMenuItem.Character.HeroObject);
					}
					AddHeroToPartyAction.Apply(this._contextMenuItem.Character.HeroObject, MobileParty.MainParty, true);
				}
				break;
			}
			case GameMenuOverlay.MenuOverlayContextList.ManageTroops:
			{
				PartyBase party3 = this._contextMenuItem.Party;
				if (((party3 != null) ? party3.MobileParty : null) != null && this._contextMenuItem.Party.MobileParty.ActualClan == Clan.PlayerClan)
				{
					PartyScreenManager.OpenScreenAsManageTroopsAndPrisoners(this._contextMenuItem.Party.MobileParty, null);
				}
				break;
			}
			}
			if (!this._closedHandled)
			{
				CampaignEventDispatcher.Instance.OnCharacterPortraitPopUpClosed();
				this._closedHandled = true;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00041B98 File Offset: 0x0003FD98
		private void ConverseWithLeader(PartyBase mainParty1, PartyBase party2)
		{
			bool flag;
			if (mainParty1.Side != BattleSideEnum.Attacker)
			{
				PlayerEncounter playerEncounter = PlayerEncounter.Current;
				flag = (playerEncounter != null && playerEncounter.PlayerSide == BattleSideEnum.Attacker);
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			if (LocationComplex.Current == null || flag2)
			{
				CampaignMapConversation.OpenConversation(new ConversationCharacterData(CharacterObject.PlayerCharacter, mainParty1, false, false, false, false, false, false), new ConversationCharacterData(ConversationHelper.GetConversationCharacterPartyLeader(party2), party2, false, false, false, false, false, false));
				return;
			}
			Location locationOfCharacter = LocationComplex.Current.GetLocationOfCharacter(party2.LeaderHero);
			CampaignEventDispatcher.Instance.OnPlayerStartTalkFromMenu(party2.LeaderHero);
			PlayerEncounter.LocationEncounter.CreateAndOpenMissionController(locationOfCharacter, null, party2.LeaderHero.CharacterObject, null);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00041C35 File Offset: 0x0003FE35
		public void ClearOverlay()
		{
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00041C38 File Offset: 0x0003FE38
		public static GameMenuOverlay GetOverlay(GameOverlays.MenuOverlayType menuOverlayType)
		{
			if (menuOverlayType == GameOverlays.MenuOverlayType.Encounter)
			{
				return new EncounterMenuOverlayVM();
			}
			if (menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithParties || menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithCharacters || menuOverlayType == GameOverlays.MenuOverlayType.SettlementWithBoth)
			{
				return new SettlementMenuOverlayVM(menuOverlayType);
			}
			Debug.FailedAssert("Game menu overlay: " + menuOverlayType.ToString() + " could not be found", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\GameMenu\\Overlay\\GameMenuOverlay.cs", "GetOverlay", 295);
			return null;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00041C93 File Offset: 0x0003FE93
		public virtual void Refresh()
		{
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x00041C95 File Offset: 0x0003FE95
		public virtual void UpdateOverlayType(GameOverlays.MenuOverlayType newType)
		{
			this.Refresh();
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00041C9D File Offset: 0x0003FE9D
		public virtual void OnFrameTick(float dt)
		{
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00041C9F File Offset: 0x0003FE9F
		public void HourlyTick()
		{
			this.Refresh();
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00041CA7 File Offset: 0x0003FEA7
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00041CAF File Offset: 0x0003FEAF
		[DataSourceProperty]
		public bool IsContextMenuEnabled
		{
			get
			{
				return this._isContextMenuEnabled;
			}
			set
			{
				this._isContextMenuEnabled = value;
				base.OnPropertyChangedWithValue(value, "IsContextMenuEnabled");
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00041CC4 File Offset: 0x0003FEC4
		// (set) Token: 0x060010A5 RID: 4261 RVA: 0x00041CCC File Offset: 0x0003FECC
		[DataSourceProperty]
		public bool IsInitializationOver
		{
			get
			{
				return this._isInitializationOver;
			}
			set
			{
				this._isInitializationOver = value;
				base.OnPropertyChangedWithValue(value, "IsInitializationOver");
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00041CE1 File Offset: 0x0003FEE1
		// (set) Token: 0x060010A7 RID: 4263 RVA: 0x00041CE9 File Offset: 0x0003FEE9
		[DataSourceProperty]
		public bool IsInfoBarExtended
		{
			get
			{
				return this._isInfoBarExtended;
			}
			set
			{
				this._isInfoBarExtended = value;
				base.OnPropertyChangedWithValue(value, "IsInfoBarExtended");
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060010A8 RID: 4264 RVA: 0x00041CFE File Offset: 0x0003FEFE
		// (set) Token: 0x060010A9 RID: 4265 RVA: 0x00041D06 File Offset: 0x0003FF06
		[DataSourceProperty]
		public MBBindingList<StringItemWithEnabledAndHintVM> ContextList
		{
			get
			{
				return this._contextList;
			}
			set
			{
				if (value != this._contextList)
				{
					this._contextList = value;
					base.OnPropertyChangedWithValue<MBBindingList<StringItemWithEnabledAndHintVM>>(value, "ContextList");
				}
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x00041D24 File Offset: 0x0003FF24
		// (set) Token: 0x060010AB RID: 4267 RVA: 0x00041D2C File Offset: 0x0003FF2C
		[DataSourceProperty]
		public int CurrentOverlayType
		{
			get
			{
				return this._currentOverlayType;
			}
			set
			{
				if (value != this._currentOverlayType)
				{
					this._currentOverlayType = value;
					base.OnPropertyChangedWithValue(value, "CurrentOverlayType");
				}
			}
		}

		// Token: 0x040007B4 RID: 1972
		public string GameMenuOverlayName;

		// Token: 0x040007B5 RID: 1973
		private bool _closedHandled = true;

		// Token: 0x040007B6 RID: 1974
		private bool _isContextMenuEnabled;

		// Token: 0x040007B7 RID: 1975
		private int _currentOverlayType = -1;

		// Token: 0x040007B8 RID: 1976
		private bool _isInfoBarExtended;

		// Token: 0x040007B9 RID: 1977
		private bool _isInitializationOver;

		// Token: 0x040007BA RID: 1978
		private MBBindingList<StringItemWithEnabledAndHintVM> _contextList;

		// Token: 0x040007BB RID: 1979
		protected GameMenuPartyItemVM _contextMenuItem;

		// Token: 0x020001ED RID: 493
		protected internal enum MenuOverlayContextList
		{
			// Token: 0x040010A2 RID: 4258
			Encyclopedia,
			// Token: 0x040010A3 RID: 4259
			Conversation,
			// Token: 0x040010A4 RID: 4260
			QuickConversation,
			// Token: 0x040010A5 RID: 4261
			ConverseWithLeader,
			// Token: 0x040010A6 RID: 4262
			ArmyDismiss,
			// Token: 0x040010A7 RID: 4263
			ManageGarrison,
			// Token: 0x040010A8 RID: 4264
			DonateTroops,
			// Token: 0x040010A9 RID: 4265
			JoinArmy,
			// Token: 0x040010AA RID: 4266
			TakeToParty,
			// Token: 0x040010AB RID: 4267
			ManageTroops
		}
	}
}
