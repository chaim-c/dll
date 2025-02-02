using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000043 RID: 67
	public class MissionAgentContourControllerView : MissionView
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001B019 File Offset: 0x00019219
		private bool _isAllowedByOption
		{
			get
			{
				return !BannerlordConfig.HideBattleUI || GameNetwork.IsMultiplayer;
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001B02C File Offset: 0x0001922C
		public MissionAgentContourControllerView()
		{
			this._contourAgents = new List<Agent>();
			this._isMultiplayer = GameNetwork.IsSessionActive;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001B0CA File Offset: 0x000192CA
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this._isAllowedByOption)
			{
				bool getUIDebugMode = NativeConfig.GetUIDebugMode;
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private void PopulateContourListWithAgents()
		{
			this._contourAgents.Clear();
			Mission mission = base.Mission;
			bool flag;
			if (mission == null)
			{
				flag = (null != null);
			}
			else
			{
				Team playerTeam = mission.PlayerTeam;
				flag = (((playerTeam != null) ? playerTeam.PlayerOrderController : null) != null);
			}
			if (flag)
			{
				foreach (Formation formation in Mission.Current.PlayerTeam.PlayerOrderController.SelectedFormations)
				{
					formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						if (!agent.IsMainAgent)
						{
							this._contourAgents.Add(agent);
						}
					}, null);
				}
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001B17C File Offset: 0x0001937C
		public override void OnFocusGained(Agent agent, IFocusable focusableObject, bool isInteractable)
		{
			base.OnFocusGained(agent, focusableObject, isInteractable);
			bool isAllowedByOption = this._isAllowedByOption;
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001B18E File Offset: 0x0001938E
		public override void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
			base.OnFocusLost(agent, focusableObject);
			if (this._isAllowedByOption)
			{
				this.RemoveContourFromFocusedAgent();
				this._currentFocusedAgent = null;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001B1AD File Offset: 0x000193AD
		private void AddContourToFocusedAgent()
		{
			if (this._currentFocusedAgent != null && !this._isContourAppliedToFocusedAgent)
			{
				MBAgentVisuals agentVisuals = this._currentFocusedAgent.AgentVisuals;
				if (agentVisuals != null)
				{
					agentVisuals.SetContourColor(new uint?(this._focusedContourColor), true);
				}
				this._isContourAppliedToFocusedAgent = true;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001B1E8 File Offset: 0x000193E8
		private void RemoveContourFromFocusedAgent()
		{
			if (this._currentFocusedAgent != null && this._isContourAppliedToFocusedAgent)
			{
				if (this._contourAgents.Contains(this._currentFocusedAgent))
				{
					MBAgentVisuals agentVisuals = this._currentFocusedAgent.AgentVisuals;
					if (agentVisuals != null)
					{
						agentVisuals.SetContourColor(new uint?(this._nonFocusedContourColor), true);
					}
				}
				else
				{
					MBAgentVisuals agentVisuals2 = this._currentFocusedAgent.AgentVisuals;
					if (agentVisuals2 != null)
					{
						agentVisuals2.SetContourColor(null, true);
					}
				}
				this._isContourAppliedToFocusedAgent = false;
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001B264 File Offset: 0x00019464
		private void ApplyContourToAllAgents()
		{
			if (!this._isContourAppliedToAllAgents)
			{
				foreach (Agent agent in this._contourAgents)
				{
					uint value = (agent == this._currentFocusedAgent) ? this._focusedContourColor : (this._isMultiplayer ? this._friendlyContourColor : this._nonFocusedContourColor);
					MBAgentVisuals agentVisuals = agent.AgentVisuals;
					if (agentVisuals != null)
					{
						agentVisuals.SetContourColor(new uint?(value), true);
					}
				}
				this._isContourAppliedToAllAgents = true;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001B300 File Offset: 0x00019500
		private void RemoveContourFromAllAgents()
		{
			if (this._isContourAppliedToAllAgents)
			{
				foreach (Agent agent in this._contourAgents)
				{
					if (this._currentFocusedAgent == null || agent != this._currentFocusedAgent)
					{
						MBAgentVisuals agentVisuals = agent.AgentVisuals;
						if (agentVisuals != null)
						{
							agentVisuals.SetContourColor(null, true);
						}
					}
				}
				this._isContourAppliedToAllAgents = false;
			}
		}

		// Token: 0x0400022C RID: 556
		private const bool IsEnabled = false;

		// Token: 0x0400022D RID: 557
		private uint _nonFocusedContourColor = new Color(0.85f, 0.85f, 0.85f, 1f).ToUnsignedInteger();

		// Token: 0x0400022E RID: 558
		private uint _focusedContourColor = new Color(1f, 0.84f, 0.35f, 1f).ToUnsignedInteger();

		// Token: 0x0400022F RID: 559
		private uint _friendlyContourColor = new Color(0.44f, 0.83f, 0.26f, 1f).ToUnsignedInteger();

		// Token: 0x04000230 RID: 560
		private List<Agent> _contourAgents;

		// Token: 0x04000231 RID: 561
		private Agent _currentFocusedAgent;

		// Token: 0x04000232 RID: 562
		private bool _isContourAppliedToAllAgents;

		// Token: 0x04000233 RID: 563
		private bool _isContourAppliedToFocusedAgent;

		// Token: 0x04000234 RID: 564
		private bool _isMultiplayer;
	}
}
