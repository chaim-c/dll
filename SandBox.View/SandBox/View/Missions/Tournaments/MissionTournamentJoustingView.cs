using System;
using SandBox.Tournaments.AgentControllers;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;

namespace SandBox.View.Missions.Tournaments
{
	// Token: 0x02000022 RID: 34
	public class MissionTournamentJoustingView : MissionView
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x0000C678 File Offset: 0x0000A878
		public override void AfterStart()
		{
			base.AfterStart();
			this._gameSystem = Game.Current;
			this._messageUIHandler = base.Mission.GetMissionBehavior<MissionMessageUIHandler>();
			this._scoreUIHandler = base.Mission.GetMissionBehavior<MissionScoreUIHandler>();
			this._tournamentJoustingMissionController = base.Mission.GetMissionBehavior<TournamentJoustingMissionController>();
			this._tournamentJoustingMissionController.VictoryAchieved += this.OnVictoryAchieved;
			this._tournamentJoustingMissionController.PointGanied += this.OnPointGanied;
			this._tournamentJoustingMissionController.Disqualified += this.OnDisqualified;
			this._tournamentJoustingMissionController.Unconscious += this.OnUnconscious;
			this._tournamentJoustingMissionController.AgentStateChanged += this.OnAgentStateChanged;
			int num = 0;
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman)
				{
					this._scoreUIHandler.SetName(agent.Name.ToString(), num);
					num++;
				}
			}
			this.SetJoustingBanners();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		private void RefreshScoreBoard()
		{
			int num = 0;
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsHuman)
				{
					JoustingAgentController controller = agent.GetController<JoustingAgentController>();
					this._scoreUIHandler.SaveScore(controller.Score, num);
					num++;
				}
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000C824 File Offset: 0x0000AA24
		private void SetJoustingBanners()
		{
			GameEntity banner0 = base.Mission.Scene.FindEntityWithTag("banner_0");
			GameEntity banner1 = base.Mission.Scene.FindEntityWithTag("banner_1");
			Banner banner = Banner.CreateOneColoredEmptyBanner(6);
			Banner banner2 = Banner.CreateOneColoredEmptyBanner(8);
			if (banner0 != null)
			{
				Action<Texture> setAction = delegate(Texture tex)
				{
					Material material = Mesh.GetFromResource("banner_test").GetMaterial().CreateCopy();
					if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
					{
						material.SetTexture(Material.MBTextureType.DiffuseMap2, tex);
					}
					banner0.SetMaterialForAllMeshes(material);
				};
				banner.GetTableauTextureLarge(setAction);
			}
			if (banner1 != null)
			{
				Action<Texture> setAction2 = delegate(Texture tex)
				{
					Material material = Mesh.GetFromResource("banner_test").GetMaterial().CreateCopy();
					if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
					{
						material.SetTexture(Material.MBTextureType.DiffuseMap2, tex);
					}
					banner1.SetMaterialForAllMeshes(material);
				};
				banner2.GetTableauTextureLarge(setAction2);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000C8C3 File Offset: 0x0000AAC3
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon attackerWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			this.RefreshScoreBoard();
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000C8CB File Offset: 0x0000AACB
		private void OnVictoryAchieved(Agent affectorAgent, Agent affectedAgent)
		{
			this.ShowMessage(affectorAgent, GameTexts.FindText("str_tournament_joust_player_victory", null).ToString(), 8f, true);
			this.ShowMessage(affectedAgent, GameTexts.FindText("str_tournament_joust_opponent_victory", null).ToString(), 8f, true);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000C907 File Offset: 0x0000AB07
		private void OnPointGanied(Agent affectorAgent, Agent affectedAgent)
		{
			this.ShowMessage(affectorAgent, GameTexts.FindText("str_tournament_joust_you_gain_point", null).ToString(), 5f, true);
			this.ShowMessage(affectedAgent, GameTexts.FindText("str_tournament_joust_opponent_gain_point", null).ToString(), 5f, true);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000C943 File Offset: 0x0000AB43
		private void OnDisqualified(Agent affectorAgent, Agent affectedAgent)
		{
			this.ShowMessage(affectedAgent, GameTexts.FindText("str_tournament_joust_opponent_disqualified", null).ToString(), 5f, true);
			this.ShowMessage(affectorAgent, GameTexts.FindText("str_tournament_joust_you_disqualified", null).ToString(), 5f, true);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000C97F File Offset: 0x0000AB7F
		private void OnUnconscious(Agent affectorAgent, Agent affectedAgent)
		{
			this.ShowMessage(affectedAgent, GameTexts.FindText("str_tournament_joust_you_become_unconscious", null).ToString(), 5f, true);
			this.ShowMessage(affectorAgent, GameTexts.FindText("str_tournament_joust_opponent_become_unconscious", null).ToString(), 5f, true);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000C9BB File Offset: 0x0000ABBB
		public void ShowMessage(string str, float duration, bool hasPriority = true)
		{
			this._messageUIHandler.ShowMessage(str, duration, hasPriority);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		public void ShowMessage(Agent agent, string str, float duration, bool hasPriority = true)
		{
			if (agent.Character == this._gameSystem.PlayerTroop)
			{
				this.ShowMessage(str, duration, hasPriority);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		public void DeleteMessage(string str)
		{
			this._messageUIHandler.DeleteMessage(str);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public void DeleteMessage(Agent agent, string str)
		{
			this.DeleteMessage(str);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000CA04 File Offset: 0x0000AC04
		private void OnAgentStateChanged(Agent agent, JoustingAgentController.JoustingAgentState state)
		{
			string text;
			switch (state)
			{
			case JoustingAgentController.JoustingAgentState.GoingToBackStart:
				text = "";
				break;
			case JoustingAgentController.JoustingAgentState.GoToStartPosition:
				text = "str_tournament_joust_go_to_starting_position";
				break;
			case JoustingAgentController.JoustingAgentState.WaitInStartPosition:
				text = "str_tournament_joust_wait_in_starting_position";
				break;
			case JoustingAgentController.JoustingAgentState.WaitingOpponent:
				text = "str_tournament_joust_wait_opponent_to_go_starting_position";
				break;
			case JoustingAgentController.JoustingAgentState.Ready:
				text = "str_tournament_joust_go";
				break;
			case JoustingAgentController.JoustingAgentState.StartRiding:
				text = "";
				break;
			case JoustingAgentController.JoustingAgentState.Riding:
				text = "";
				break;
			case JoustingAgentController.JoustingAgentState.RidingAtWrongSide:
				text = "str_tournament_joust_wrong_side";
				break;
			case JoustingAgentController.JoustingAgentState.SwordDuel:
				text = "";
				break;
			default:
				throw new ArgumentOutOfRangeException("value");
			}
			if (text == "")
			{
				this.ShowMessage(agent, "", 15f, true);
			}
			else
			{
				this.ShowMessage(agent, GameTexts.FindText(text, null).ToString(), float.PositiveInfinity, true);
			}
			if (state == JoustingAgentController.JoustingAgentState.SwordDuel)
			{
				this.ShowMessage(agent, GameTexts.FindText("str_tournament_joust_duel_on_foot", null).ToString(), 8f, true);
			}
		}

		// Token: 0x0400007B RID: 123
		private MissionScoreUIHandler _scoreUIHandler;

		// Token: 0x0400007C RID: 124
		private MissionMessageUIHandler _messageUIHandler;

		// Token: 0x0400007D RID: 125
		private TournamentJoustingMissionController _tournamentJoustingMissionController;

		// Token: 0x0400007E RID: 126
		private Game _gameSystem;
	}
}
