using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromServer;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200027A RID: 634
	public class MissionBoundaryCrossingHandler : MissionLogic
	{
		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002176 RID: 8566 RVA: 0x00079F20 File Offset: 0x00078120
		// (remove) Token: 0x06002177 RID: 8567 RVA: 0x00079F58 File Offset: 0x00078158
		public event Action<float, float> StartTime;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002178 RID: 8568 RVA: 0x00079F90 File Offset: 0x00078190
		// (remove) Token: 0x06002179 RID: 8569 RVA: 0x00079FC8 File Offset: 0x000781C8
		public event Action StopTime;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600217A RID: 8570 RVA: 0x0007A000 File Offset: 0x00078200
		// (remove) Token: 0x0600217B RID: 8571 RVA: 0x0007A038 File Offset: 0x00078238
		public event Action<float> TimeCount;

		// Token: 0x0600217C RID: 8572 RVA: 0x0007A06D File Offset: 0x0007826D
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			if (GameNetwork.IsSessionActive)
			{
				this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Add);
			}
			if (GameNetwork.IsServer)
			{
				this._agentTimers = new Dictionary<Agent, MissionTimer>();
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0007A095 File Offset: 0x00078295
		public override void OnRemoveBehavior()
		{
			if (GameNetwork.IsSessionActive)
			{
				this.AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode.Remove);
			}
			base.OnRemoveBehavior();
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0007A0AC File Offset: 0x000782AC
		private void AddRemoveMessageHandlers(GameNetwork.NetworkMessageHandlerRegisterer.RegisterMode mode)
		{
			GameNetwork.NetworkMessageHandlerRegisterer networkMessageHandlerRegisterer = new GameNetwork.NetworkMessageHandlerRegisterer(mode);
			if (GameNetwork.IsClient)
			{
				networkMessageHandlerRegisterer.Register<SetBoundariesState>(new GameNetworkMessage.ServerMessageHandlerDelegate<SetBoundariesState>(this.HandleServerEventSetPeerBoundariesState));
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0007A0DC File Offset: 0x000782DC
		private void OnAgentWentOut(Agent agent, float startTimeInSeconds)
		{
			MissionTimer missionTimer = GameNetwork.IsClient ? MissionTimer.CreateSynchedTimerClient(startTimeInSeconds, 10f) : new MissionTimer(10f);
			if (GameNetwork.IsServer)
			{
				this._agentTimers.Add(agent, missionTimer);
				MissionPeer missionPeer = agent.MissionPeer;
				NetworkCommunicator networkCommunicator = (missionPeer != null) ? missionPeer.GetNetworkPeer() : null;
				if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
				{
					GameNetwork.BeginModuleEventAsServer(networkCommunicator);
					GameNetwork.WriteMessage(new SetBoundariesState(true, missionTimer.GetStartTime().NumberOfTicks));
					GameNetwork.EndModuleEventAsServer();
				}
			}
			if (base.Mission.MainAgent == agent)
			{
				this._mainAgentLeaveTimer = missionTimer;
				Action<float, float> startTime = this.StartTime;
				if (startTime != null)
				{
					startTime(10f, 0f);
				}
				MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
				Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
				if (Mission.Current.Mode == MissionMode.Battle)
				{
					MBSoundEvent.PlaySound(SoundEvent.GetEventIdFromString("event:/alerts/report/out_of_map"), position);
				}
			}
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0007A1D0 File Offset: 0x000783D0
		private void OnAgentWentInOrRemoved(Agent agent, bool isAgentRemoved)
		{
			if (GameNetwork.IsServer)
			{
				this._agentTimers.Remove(agent);
				if (!isAgentRemoved)
				{
					MissionPeer missionPeer = agent.MissionPeer;
					NetworkCommunicator networkCommunicator = (missionPeer != null) ? missionPeer.GetNetworkPeer() : null;
					if (networkCommunicator != null && !networkCommunicator.IsServerPeer)
					{
						GameNetwork.BeginModuleEventAsServer(networkCommunicator);
						GameNetwork.WriteMessage(new SetBoundariesState(false));
						GameNetwork.EndModuleEventAsServer();
					}
				}
			}
			if (base.Mission.MainAgent == agent)
			{
				this._mainAgentLeaveTimer = null;
				Action stopTime = this.StopTime;
				if (stopTime == null)
				{
					return;
				}
				stopTime();
			}
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0007A250 File Offset: 0x00078450
		private void HandleAgentPunishment(Agent agent)
		{
			if (GameNetwork.IsSessionActive)
			{
				if (GameNetwork.IsServer)
				{
					Blow b = new Blow(agent.Index);
					b.WeaponRecord.FillAsMeleeBlow(null, null, -1, 0);
					b.DamageType = DamageTypes.Blunt;
					b.BaseMagnitude = 10000f;
					b.WeaponRecord.WeaponClass = WeaponClass.Undefined;
					b.GlobalPosition = agent.Position;
					b.DamagedPercentage = 1f;
					agent.Die(b, Agent.KillInfo.Invalid);
					return;
				}
			}
			else
			{
				base.Mission.RetreatMission();
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0007A2D8 File Offset: 0x000784D8
		public override void OnClearScene()
		{
			if (GameNetwork.IsServer)
			{
				using (List<Agent>.Enumerator enumerator = this._agentTimers.Keys.ToList<Agent>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Agent agent = enumerator.Current;
						this.OnAgentWentInOrRemoved(agent, true);
					}
					return;
				}
			}
			if (this._mainAgentLeaveTimer != null)
			{
				if (base.Mission.MainAgent != null)
				{
					this.OnAgentWentInOrRemoved(base.Mission.MainAgent, true);
					return;
				}
				this._mainAgentLeaveTimer = null;
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0007A36C File Offset: 0x0007856C
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			this.OnAgentWentInOrRemoved(affectedAgent, true);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0007A378 File Offset: 0x00078578
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (GameNetwork.IsServer)
			{
				for (int i = base.Mission.Agents.Count - 1; i >= 0; i--)
				{
					Agent agent = base.Mission.Agents[i];
					if (agent.MissionPeer != null)
					{
						this.TickForAgentAsServer(agent);
					}
				}
			}
			else if (!GameNetwork.IsSessionActive && Agent.Main != null)
			{
				this.TickForMainAgent();
			}
			if (this._mainAgentLeaveTimer != null)
			{
				this._mainAgentLeaveTimer.Check(false);
				float obj = 1f - this._mainAgentLeaveTimer.GetRemainingTimeInSeconds(true) / 10f;
				Action<float> timeCount = this.TimeCount;
				if (timeCount == null)
				{
					return;
				}
				timeCount(obj);
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0007A428 File Offset: 0x00078628
		private void TickForMainAgent()
		{
			bool isAgentOutside = !base.Mission.IsPositionInsideBoundaries(Agent.Main.Position.AsVec2);
			bool isTimerActiveForAgent = this._mainAgentLeaveTimer != null;
			this.HandleAgentStateChange(Agent.Main, isAgentOutside, isTimerActiveForAgent, this._mainAgentLeaveTimer);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0007A474 File Offset: 0x00078674
		private void TickForAgentAsServer(Agent agent)
		{
			bool isAgentOutside = !base.Mission.IsPositionInsideBoundaries(agent.Position.AsVec2);
			bool flag = this._agentTimers.ContainsKey(agent);
			this.HandleAgentStateChange(agent, isAgentOutside, flag, flag ? this._agentTimers[agent] : null);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0007A4C6 File Offset: 0x000786C6
		private void HandleAgentStateChange(Agent agent, bool isAgentOutside, bool isTimerActiveForAgent, MissionTimer timerInstance)
		{
			if (isAgentOutside && !isTimerActiveForAgent)
			{
				this.OnAgentWentOut(agent, 0f);
				return;
			}
			if (!isAgentOutside && isTimerActiveForAgent)
			{
				this.OnAgentWentInOrRemoved(agent, false);
				return;
			}
			if (isAgentOutside && timerInstance.Check(false))
			{
				this.HandleAgentPunishment(agent);
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0007A500 File Offset: 0x00078700
		private void HandleServerEventSetPeerBoundariesState(SetBoundariesState message)
		{
			if (message.IsOutside)
			{
				this.OnAgentWentOut(base.Mission.MainAgent, message.StateStartTimeInSeconds);
				return;
			}
			this.OnAgentWentInOrRemoved(base.Mission.MainAgent, false);
		}

		// Token: 0x04000C73 RID: 3187
		private const float LeewayTime = 10f;

		// Token: 0x04000C77 RID: 3191
		private Dictionary<Agent, MissionTimer> _agentTimers;

		// Token: 0x04000C78 RID: 3192
		private MissionTimer _mainAgentLeaveTimer;
	}
}
