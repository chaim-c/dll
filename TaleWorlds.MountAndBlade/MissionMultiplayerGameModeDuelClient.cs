using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.MissionRepresentatives;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200029F RID: 671
	public class MissionMultiplayerGameModeDuelClient : MissionMultiplayerGameModeBaseClient
	{
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x00084B5A File Offset: 0x00082D5A
		public override bool IsGameModeUsingGold
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x00084B5D File Offset: 0x00082D5D
		public override bool IsGameModeTactical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x00084B60 File Offset: 0x00082D60
		public override bool IsGameModeUsingRoundCountdown
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00084B63 File Offset: 0x00082D63
		public override bool IsGameModeUsingAllowCultureChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x00084B66 File Offset: 0x00082D66
		public override bool IsGameModeUsingAllowTroopChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x00084B69 File Offset: 0x00082D69
		public override MultiplayerGameType GameType
		{
			get
			{
				return MultiplayerGameType.Duel;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x00084B6C File Offset: 0x00082D6C
		public bool IsInDuel
		{
			get
			{
				MissionPeer component = GameNetwork.MyPeer.GetComponent<MissionPeer>();
				bool? flag;
				if (component == null)
				{
					flag = null;
				}
				else
				{
					Team team = component.Team;
					flag = ((team != null) ? new bool?(team.IsDefender) : null);
				}
				return flag ?? false;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x00084BC3 File Offset: 0x00082DC3
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00084BCB File Offset: 0x00082DCB
		public DuelMissionRepresentative MyRepresentative { get; private set; }

		// Token: 0x060023DC RID: 9180 RVA: 0x00084BD4 File Offset: 0x00082DD4
		private void OnMyClientSynchronized()
		{
			this.MyRepresentative = GameNetwork.MyPeer.GetComponent<DuelMissionRepresentative>();
			Action onMyRepresentativeAssigned = this.OnMyRepresentativeAssigned;
			if (onMyRepresentativeAssigned != null)
			{
				onMyRepresentativeAssigned();
			}
			this.MyRepresentative.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x00084C03 File Offset: 0x00082E03
		public override int GetGoldAmount()
		{
			return 0;
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00084C06 File Offset: 0x00082E06
		public override void OnGoldAmountChangedForRepresentative(MissionRepresentativeBase representative, int goldAmount)
		{
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00084C08 File Offset: 0x00082E08
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			base.MissionNetworkComponent.OnMyClientSynchronized += this.OnMyClientSynchronized;
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00084C27 File Offset: 0x00082E27
		public override void OnRemoveBehavior()
		{
			base.OnRemoveBehavior();
			base.MissionNetworkComponent.OnMyClientSynchronized -= this.OnMyClientSynchronized;
			if (this.MyRepresentative != null)
			{
				this.MyRepresentative.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
			}
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00084C5A File Offset: 0x00082E5A
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			if (this.MyRepresentative != null)
			{
				this.MyRepresentative.CheckHasRequestFromAndRemoveRequestIfNeeded(affectedAgent.MissionPeer);
			}
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x00084C84 File Offset: 0x00082E84
		public override bool CanRequestCultureChange()
		{
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			return ((missionPeer != null) ? missionPeer.Team : null) != null && missionPeer.Team.IsAttacker;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00084CC0 File Offset: 0x00082EC0
		public override bool CanRequestTroopChange()
		{
			NetworkCommunicator myPeer = GameNetwork.MyPeer;
			MissionPeer missionPeer = (myPeer != null) ? myPeer.GetComponent<MissionPeer>() : null;
			return ((missionPeer != null) ? missionPeer.Team : null) != null && missionPeer.Team.IsAttacker;
		}

		// Token: 0x04000D1D RID: 3357
		public Action OnMyRepresentativeAssigned;
	}
}
