using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002BC RID: 700
	public abstract class MissionRepresentativeBase : PeerComponent
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x000918F4 File Offset: 0x0008FAF4
		protected MissionRepresentativeBase.PlayerTypes PlayerType
		{
			get
			{
				if (!base.Peer.Communicator.IsNetworkActive)
				{
					return MissionRepresentativeBase.PlayerTypes.Bot;
				}
				if (!base.Peer.Communicator.IsServerPeer)
				{
					return MissionRepresentativeBase.PlayerTypes.Client;
				}
				return MissionRepresentativeBase.PlayerTypes.Server;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x0009191F File Offset: 0x0008FB1F
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x00091927 File Offset: 0x0008FB27
		public Agent ControlledAgent { get; private set; }

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x00091930 File Offset: 0x0008FB30
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x00091970 File Offset: 0x0008FB70
		public int Gold
		{
			get
			{
				if (this._gold < 0)
				{
					return this._gold;
				}
				bool flag;
				MultiplayerOptions.Instance.GetOptionFromOptionType(MultiplayerOptions.OptionType.UnlimitedGold, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions).GetValue(out flag);
				if (!flag)
				{
					return this._gold;
				}
				return 2000;
			}
			private set
			{
				if (value < 0)
				{
					this._gold = value;
					return;
				}
				bool flag;
				MultiplayerOptions.Instance.GetOptionFromOptionType(MultiplayerOptions.OptionType.UnlimitedGold, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions).GetValue(out flag);
				this._gold = ((!flag) ? value : 2000);
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000919AE File Offset: 0x0008FBAE
		public MissionPeer MissionPeer
		{
			get
			{
				if (this._missionPeer == null)
				{
					this._missionPeer = base.GetComponent<MissionPeer>();
				}
				return this._missionPeer;
			}
		}

		// Token: 0x14000073 RID: 115
		// (add) Token: 0x0600269B RID: 9883 RVA: 0x000919CC File Offset: 0x0008FBCC
		// (remove) Token: 0x0600269C RID: 9884 RVA: 0x00091A04 File Offset: 0x0008FC04
		public event Action OnGoldUpdated;

		// Token: 0x0600269E RID: 9886 RVA: 0x00091A41 File Offset: 0x0008FC41
		public void SetAgent(Agent agent)
		{
			this.ControlledAgent = agent;
			if (this.ControlledAgent != null)
			{
				this.ControlledAgent.MissionRepresentative = this;
				this.OnAgentSpawned();
			}
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x00091A64 File Offset: 0x0008FC64
		public virtual void OnAgentSpawned()
		{
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00091A66 File Offset: 0x0008FC66
		public virtual void Tick(float dt)
		{
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00091A68 File Offset: 0x0008FC68
		public void UpdateGold(int gold)
		{
			this.Gold = gold;
			Action onGoldUpdated = this.OnGoldUpdated;
			if (onGoldUpdated == null)
			{
				return;
			}
			onGoldUpdated();
		}

		// Token: 0x04000E56 RID: 3670
		private int _gold;

		// Token: 0x04000E57 RID: 3671
		private MissionPeer _missionPeer;

		// Token: 0x0200057E RID: 1406
		protected enum PlayerTypes
		{
			// Token: 0x04001D5C RID: 7516
			Bot,
			// Token: 0x04001D5D RID: 7517
			Client,
			// Token: 0x04001D5E RID: 7518
			Server
		}
	}
}
