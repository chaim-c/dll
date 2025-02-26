﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000269 RID: 617
	public class BattleHighlightsController : MissionLogic
	{
		// Token: 0x060020CF RID: 8399 RVA: 0x00075B98 File Offset: 0x00073D98
		public override void AfterStart()
		{
			this._highlightsController = Mission.Current.GetMissionBehavior<HighlightsController>();
			foreach (HighlightsController.HighlightType highlightType in this._highlightTypes)
			{
				HighlightsController.AddHighlightType(highlightType);
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x00075BF8 File Offset: 0x00073DF8
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectorAgent != null && affectedAgent != null && affectorAgent.IsHuman && affectedAgent.IsHuman && (agentState == AgentState.Killed || agentState == AgentState.Unconscious))
			{
				bool flag = affectorAgent != null && affectorAgent.Team.IsPlayerTeam;
				bool flag2 = affectorAgent != null && affectorAgent.IsMainAgent;
				HighlightsController.Highlight highlight = default(HighlightsController.Highlight);
				highlight.Start = Mission.Current.CurrentTime;
				highlight.End = Mission.Current.CurrentTime;
				bool flag3 = false;
				if ((flag2 || flag) && (killingBlow.WeaponRecordWeaponFlags.HasAllFlags(WeaponFlags.Burning) || killingBlow.WeaponRecordWeaponFlags.HasAllFlags(WeaponFlags.AffectsArea)))
				{
					highlight.HighlightType = this._highlightsController.GetHighlightTypeWithId("hlid_wall_break_kill");
					flag3 = true;
				}
				Team team = affectedAgent.Team;
				MBReadOnlyList<Agent> mbreadOnlyList = (team != null) ? team.TeamAgents : null;
				bool flag4;
				if (mbreadOnlyList != null)
				{
					flag4 = mbreadOnlyList.Any((Agent agent) => agent.State != AgentState.Killed && agent.State != AgentState.Unconscious);
				}
				else
				{
					flag4 = true;
				}
				bool flag5 = flag4;
				if (!flag5 && (flag2 || flag))
				{
					highlight.HighlightType = this._highlightsController.GetHighlightTypeWithId("hlid_kill_last_enemy_on_battlefield");
					flag3 = true;
				}
				if (flag2)
				{
					Team team2 = affectorAgent.Team;
					MBReadOnlyList<Agent> mbreadOnlyList2 = (team2 != null) ? team2.TeamAgents : null;
					bool flag6;
					if (mbreadOnlyList2 != null)
					{
						flag6 = mbreadOnlyList2.Any((Agent agent) => !agent.IsMainAgent && agent.State != AgentState.Killed && agent.State != AgentState.Unconscious);
					}
					else
					{
						flag6 = true;
					}
					if (!flag6 && !flag5)
					{
						highlight.HighlightType = this._highlightsController.GetHighlightTypeWithId("hlid_win_battle_as_last_man_standing");
						flag3 = true;
					}
				}
				if (flag3)
				{
					this._highlightsController.SaveHighlight(highlight, affectedAgent.Position);
				}
			}
		}

		// Token: 0x04000C2C RID: 3116
		private List<HighlightsController.HighlightType> _highlightTypes = new List<HighlightsController.HighlightType>
		{
			new HighlightsController.HighlightType("hlid_kill_last_enemy_on_battlefield", "Take No Prisoners", "grpid_incidents", -5000, 3000, 0f, float.MaxValue, false),
			new HighlightsController.HighlightType("hlid_win_battle_as_last_man_standing", "Last Man Standing", "grpid_incidents", -5000, 3000, 0f, float.MaxValue, false),
			new HighlightsController.HighlightType("hlid_wall_break_kill", "Wall Break Kill", "grpid_incidents", -5000, 3000, 0.25f, 100f, true)
		};

		// Token: 0x04000C2D RID: 3117
		private HighlightsController _highlightsController;
	}
}
