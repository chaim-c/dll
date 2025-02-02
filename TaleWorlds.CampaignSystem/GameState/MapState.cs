using System;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200033C RID: 828
	public class MapState : GameState
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002ECC RID: 11980 RVA: 0x000C2119 File Offset: 0x000C0319
		// (set) Token: 0x06002ECD RID: 11981 RVA: 0x000C2121 File Offset: 0x000C0321
		public MenuContext MenuContext
		{
			get
			{
				return this._menuContext;
			}
			private set
			{
				this._menuContext = value;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x000C212A File Offset: 0x000C032A
		// (set) Token: 0x06002ECF RID: 11983 RVA: 0x000C213B File Offset: 0x000C033B
		public string GameMenuId
		{
			get
			{
				return Campaign.Current.MapStateData.GameMenuId;
			}
			set
			{
				Campaign.Current.MapStateData.GameMenuId = value;
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000C214D File Offset: 0x000C034D
		public bool AtMenu
		{
			get
			{
				return this.MenuContext != null;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000C2158 File Offset: 0x000C0358
		public bool MapConversationActive
		{
			get
			{
				return this._mapConversationActive;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x000C2160 File Offset: 0x000C0360
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x000C2168 File Offset: 0x000C0368
		public IMapStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x000C2171 File Offset: 0x000C0371
		public bool IsSimulationActive
		{
			get
			{
				return this._battleSimulation != null;
			}
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x000C217C File Offset: 0x000C037C
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnIdleTick(dt);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000C2196 File Offset: 0x000C0396
		private void RefreshHandler()
		{
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnRefreshState();
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000C21A8 File Offset: 0x000C03A8
		public void OnJoinArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000C21B0 File Offset: 0x000C03B0
		public void OnLeaveArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000C21B8 File Offset: 0x000C03B8
		public void OnDispersePlayerLeadedArmy()
		{
			this.RefreshHandler();
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000C21C0 File Offset: 0x000C03C0
		public void OnArmyCreated(MobileParty mobileParty)
		{
			this.RefreshHandler();
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000C21C8 File Offset: 0x000C03C8
		public void OnMainPartyEncounter()
		{
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMainPartyEncounter();
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000C21DA File Offset: 0x000C03DA
		public void ProcessTravel(Vec2 point)
		{
			MobileParty.MainParty.Ai.ForceAiNoPathMode = false;
			MobileParty.MainParty.Ai.SetMoveGoToPoint(point);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000C21FC File Offset: 0x000C03FC
		public void ProcessTravel(PartyBase party)
		{
			if (party.IsMobile)
			{
				MobileParty.MainParty.Ai.SetMoveEngageParty(party.MobileParty);
			}
			else
			{
				MobileParty.MainParty.Ai.SetMoveGoToSettlement(party.Settlement);
			}
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.StoppablePlay;
			MobileParty.MainParty.Ai.ForceAiNoPathMode = false;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000C2258 File Offset: 0x000C0458
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			if (Campaign.Current.SaveHandler.IsSaving)
			{
				Campaign.Current.SaveHandler.SaveTick();
				return;
			}
			if (this._battleSimulation != null)
			{
				this._battleSimulation.Tick(dt);
			}
			else if (this.AtMenu)
			{
				this.OnMenuModeTick(dt);
			}
			this.OnMapModeTick(dt);
			if (!Campaign.Current.SaveHandler.IsSaving)
			{
				Campaign.Current.SaveHandler.CampaignTick();
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x000C22D9 File Offset: 0x000C04D9
		private void OnMenuModeTick(float dt)
		{
			this.MenuContext.OnTick(dt);
			IMapStateHandler handler = this.Handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMenuModeTick(dt);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000C22F8 File Offset: 0x000C04F8
		private void OnMapModeTick(float dt)
		{
			if (this._closeScreenNextFrame)
			{
				Game.Current.GameStateManager.CleanStates(0);
				return;
			}
			if (this.Handler != null)
			{
				this.Handler.BeforeTick(dt);
			}
			if (Campaign.Current != null && base.GameStateManager.ActiveState == this)
			{
				Campaign.Current.RealTick(dt);
				IMapStateHandler handler = this.Handler;
				if (handler != null)
				{
					handler.Tick(dt);
				}
				IMapStateHandler handler2 = this.Handler;
				if (handler2 != null)
				{
					handler2.AfterTick(dt);
				}
				Campaign.Current.Tick();
				IMapStateHandler handler3 = this.Handler;
				if (handler3 == null)
				{
					return;
				}
				handler3.AfterWaitTick(dt);
			}
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000C2394 File Offset: 0x000C0594
		public void OnLoadingFinished()
		{
			if (!string.IsNullOrEmpty(this.GameMenuId))
			{
				this.EnterMenuMode();
			}
			this.RefreshHandler();
			if (Campaign.Current.CurrentMenuContext != null && Campaign.Current.CurrentMenuContext.GameMenu != null && Campaign.Current.CurrentMenuContext.GameMenu.IsWaitMenu)
			{
				Campaign.Current.CurrentMenuContext.GameMenu.StartWait();
			}
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x000C240C File Offset: 0x000C060C
		public void OnMapConversationStarts(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			this._mapConversationActive = true;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnMapConversationStarts(playerCharacterData, conversationPartnerData);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x000C2428 File Offset: 0x000C0628
		public void OnMapConversationOver()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnMapConversationOver();
			}
			this._mapConversationActive = false;
			if (Game.Current.GameStateManager.ActiveState is MapState)
			{
				MenuContext menuContext = this.MenuContext;
				if (menuContext != null)
				{
					menuContext.Refresh();
				}
			}
			this.RefreshHandler();
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x000C247A File Offset: 0x000C067A
		internal void OnSignalPeriodicEvents()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSignalPeriodicEvents();
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000C248C File Offset: 0x000C068C
		internal void OnHourlyTick()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnHourlyTick();
			}
			MenuContext menuContext = this.MenuContext;
			if (menuContext == null)
			{
				return;
			}
			menuContext.OnHourlyTick();
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000C24AF File Offset: 0x000C06AF
		protected override void OnActivate()
		{
			base.OnActivate();
			MenuContext menuContext = this.MenuContext;
			if (menuContext != null)
			{
				menuContext.Refresh();
			}
			this.RefreshHandler();
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000C24CE File Offset: 0x000C06CE
		public void EnterMenuMode()
		{
			this.MenuContext = MBObjectManager.Instance.CreateObject<MenuContext>();
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnEnteringMenuMode(this.MenuContext);
			}
			this.MenuContext.Refresh();
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000C2502 File Offset: 0x000C0702
		public void ExitMenuMode()
		{
			IMapStateHandler handler = this._handler;
			if (handler != null)
			{
				handler.OnExitingMenuMode();
			}
			this.MenuContext.Destroy();
			MBObjectManager.Instance.UnregisterObject(this.MenuContext);
			this.MenuContext = null;
			this.GameMenuId = null;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000C253E File Offset: 0x000C073E
		public void StartBattleSimulation()
		{
			this._battleSimulation = PlayerEncounter.Current.BattleSimulation;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleSimulationStarted(this._battleSimulation);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000C2566 File Offset: 0x000C0766
		public void EndBattleSimulation()
		{
			this._battleSimulation = null;
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnBattleSimulationEnded();
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000C257F File Offset: 0x000C077F
		public void OnPlayerSiegeActivated()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSiegeActivated();
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000C2591 File Offset: 0x000C0791
		public void OnPlayerSiegeDeactivated()
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnPlayerSiegeDeactivated();
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000C25A3 File Offset: 0x000C07A3
		public void OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
			IMapStateHandler handler = this._handler;
			if (handler == null)
			{
				return;
			}
			handler.OnSiegeEngineClick(siegeEngineFrame);
		}

		// Token: 0x04000DF9 RID: 3577
		private MenuContext _menuContext;

		// Token: 0x04000DFA RID: 3578
		private bool _mapConversationActive;

		// Token: 0x04000DFB RID: 3579
		private bool _closeScreenNextFrame;

		// Token: 0x04000DFC RID: 3580
		private IMapStateHandler _handler;

		// Token: 0x04000DFD RID: 3581
		private BattleSimulation _battleSimulation;
	}
}
