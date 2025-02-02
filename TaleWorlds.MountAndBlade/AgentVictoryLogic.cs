using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000262 RID: 610
	public class AgentVictoryLogic : MissionLogic
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600206F RID: 8303 RVA: 0x0007363C File Offset: 0x0007183C
		public AgentVictoryLogic.CheerActionGroupEnum CheerActionGroup
		{
			get
			{
				return this._cheerActionGroup;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x00073644 File Offset: 0x00071844
		public AgentVictoryLogic.CheerReactionTimeSettings CheerReactionTimerData
		{
			get
			{
				return this._cheerReactionTimerData;
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0007364C File Offset: 0x0007184C
		public override void AfterStart()
		{
			base.Mission.MissionCloseTimeAfterFinish = 60f;
			this._cheeringAgents = new List<AgentVictoryLogic.CheeringAgent>();
			this.SetCheerReactionTimerSettings(1f, 8f);
			if (base.Mission.PlayerTeam != null)
			{
				base.Mission.PlayerTeam.PlayerOrderController.OnOrderIssued += new OnOrderIssuedDelegate(this.MasterOrderControllerOnOrderIssued);
			}
			Mission.Current.IsBattleInRetreatEvent += this.CheckIfIsInRetreat;
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000736C8 File Offset: 0x000718C8
		private void MasterOrderControllerOnOrderIssued(OrderType orderType, IEnumerable<Formation> appliedFormations, OrderController orderController, object[] delegateparams)
		{
			MBList<Formation> mblist = appliedFormations.ToMBList<Formation>();
			for (int i = this._cheeringAgents.Count - 1; i >= 0; i--)
			{
				Agent agent = this._cheeringAgents[i].Agent;
				if (mblist.Contains(agent.Formation))
				{
					this._cheeringAgents[i].OrderReceived();
				}
			}
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x00073728 File Offset: 0x00071928
		public void SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum cheerActionGroup = AgentVictoryLogic.CheerActionGroupEnum.None)
		{
			this._cheerActionGroup = cheerActionGroup;
			switch (this._cheerActionGroup)
			{
			case AgentVictoryLogic.CheerActionGroupEnum.LowCheerActions:
				this._selectedCheerActions = this._lowCheerActions;
				return;
			case AgentVictoryLogic.CheerActionGroupEnum.MidCheerActions:
				this._selectedCheerActions = this._midCheerActions;
				return;
			case AgentVictoryLogic.CheerActionGroupEnum.HighCheerActions:
				this._selectedCheerActions = this._highCheerActions;
				return;
			default:
				this._selectedCheerActions = null;
				return;
			}
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00073787 File Offset: 0x00071987
		public void SetCheerReactionTimerSettings(float minDuration = 1f, float maxDuration = 8f)
		{
			this._cheerReactionTimerData = new AgentVictoryLogic.CheerReactionTimeSettings(minDuration, maxDuration);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00073796 File Offset: 0x00071996
		public override void OnClearScene()
		{
			this._cheeringAgents.Clear();
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x000737A4 File Offset: 0x000719A4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			VictoryComponent component = affectedAgent.GetComponent<VictoryComponent>();
			if (component != null)
			{
				affectedAgent.RemoveComponent(component);
			}
			for (int i = 0; i < this._cheeringAgents.Count; i++)
			{
				if (this._cheeringAgents[i].Agent == affectedAgent)
				{
					this._cheeringAgents.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x000737FA File Offset: 0x000719FA
		protected override void OnEndMission()
		{
			Mission.Current.IsBattleInRetreatEvent -= this.CheckIfIsInRetreat;
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00073812 File Offset: 0x00071A12
		public override void OnMissionTick(float dt)
		{
			if (this._cheeringAgents.Count > 0)
			{
				this.CheckAnimationAndVoice();
			}
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00073828 File Offset: 0x00071A28
		private void CheckAnimationAndVoice()
		{
			for (int i = this._cheeringAgents.Count - 1; i >= 0; i--)
			{
				Agent agent = this._cheeringAgents[i].Agent;
				bool gotOrderRecently = this._cheeringAgents[i].GotOrderRecently;
				bool isCheeringOnRetreat = this._cheeringAgents[i].IsCheeringOnRetreat;
				VictoryComponent component = agent.GetComponent<VictoryComponent>();
				if (component != null)
				{
					HumanAIComponent component2 = agent.GetComponent<HumanAIComponent>();
					bool flag = ((component2 != null) ? component2.GetCurrentlyMovingGameObject() : null) != null;
					bool flag2 = agent.GetCurrentAnimationFlag(0).HasAnyFlag(AnimFlags.anf_synch_with_ladder_movement) || agent.GetCurrentAnimationFlag(1).HasAnyFlag(AnimFlags.anf_synch_with_ladder_movement);
					if (this.CheckIfIsInRetreat() && gotOrderRecently && !flag && !flag2)
					{
						agent.RemoveComponent(component);
						agent.SetActionChannel(1, ActionIndexCache.act_none, false, (ulong)agent.GetCurrentActionPriority(1), 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
						if (MBRandom.RandomFloat > 0.25f)
						{
							agent.MakeVoice(SkinVoiceManager.VoiceType.Yell, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
						}
						if (isCheeringOnRetreat)
						{
							agent.ClearTargetFrame();
						}
						this._cheeringAgents.RemoveAt(i);
					}
					else if (component.CheckTimer())
					{
						if (!agent.IsActive())
						{
							Debug.FailedAssert("Agent trying to cheer without being active", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\AgentVictoryLogic.cs", "CheckAnimationAndVoice", 234);
							Debug.Print("Agent trying to cheer without being active", 0, Debug.DebugColor.White, 17592186044416UL);
						}
						bool flag3;
						this.ChooseWeaponToCheerWithCheerAndUpdateTimer(agent, out flag3);
						if (flag3)
						{
							component.ChangeTimerDuration(6f, 12f);
						}
					}
				}
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000739BC File Offset: 0x00071BBC
		private void SelectVictoryCondition(BattleSideEnum side)
		{
			if (this._cheerActionGroup == AgentVictoryLogic.CheerActionGroupEnum.None)
			{
				BattleObserverMissionLogic missionBehavior = Mission.Current.GetMissionBehavior<BattleObserverMissionLogic>();
				if (missionBehavior != null)
				{
					float deathToBuiltAgentRatioForSide = missionBehavior.GetDeathToBuiltAgentRatioForSide(side);
					if (deathToBuiltAgentRatioForSide < 0.25f)
					{
						this.SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum.HighCheerActions);
						return;
					}
					if (deathToBuiltAgentRatioForSide < 0.75f)
					{
						this.SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum.MidCheerActions);
						return;
					}
					this.SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum.LowCheerActions);
					return;
				}
				else
				{
					this.SetCheerActionGroup(AgentVictoryLogic.CheerActionGroupEnum.MidCheerActions);
				}
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x00073A18 File Offset: 0x00071C18
		public void SetTimersOfVictoryReactionsOnBattleEnd(BattleSideEnum side)
		{
			this._isInRetreat = false;
			this.SelectVictoryCondition(side);
			foreach (Team team in base.Mission.Teams)
			{
				if (team.Side == side)
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation.CountOfUnits > 0)
						{
							formation.SetMovementOrder(MovementOrder.MovementOrderStop);
						}
					}
				}
			}
			using (List<Agent>.Enumerator enumerator3 = base.Mission.Agents.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					Agent agent = enumerator3.Current;
					if (agent.IsHuman && agent.IsAIControlled && agent.Team != null && side == agent.Team.Side && agent.CurrentWatchState == Agent.WatchState.Alarmed && agent.GetComponent<VictoryComponent>() == null)
					{
						if (this._cheeringAgents.AnyQ((AgentVictoryLogic.CheeringAgent a) => a.Agent == agent))
						{
							Debug.FailedAssert("Adding a duplicate agent in _cheeringAgents", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\AgentVictoryLogic.cs", "SetTimersOfVictoryReactionsOnBattleEnd", 308);
							Debug.Print("Adding a duplicate agent in _cheeringAgents", 0, Debug.DebugColor.White, 17592186044416UL);
						}
						agent.AddComponent(new VictoryComponent(agent, new RandomTimer(base.Mission.CurrentTime, this._cheerReactionTimerData.MinDuration, this._cheerReactionTimerData.MaxDuration)));
						this._cheeringAgents.Add(new AgentVictoryLogic.CheeringAgent(agent, false));
					}
				}
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00073C54 File Offset: 0x00071E54
		public void SetTimersOfVictoryReactionsOnRetreat(BattleSideEnum side)
		{
			this._isInRetreat = true;
			this.SelectVictoryCondition(side);
			List<Agent> list = (from agent in base.Mission.Agents
			where agent.IsHuman && agent.IsAIControlled && agent.Team.Side == side
			select agent).ToList<Agent>();
			int num = (int)((float)list.Count * 0.5f);
			List<Agent> list2 = new List<Agent>();
			int num2 = 0;
			while (num2 < list.Count && list2.Count != num)
			{
				Agent agent3 = list[num2];
				EquipmentIndex wieldedItemIndex = agent3.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				bool flag = wieldedItemIndex != EquipmentIndex.None && agent3.Equipment[wieldedItemIndex].Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnAnyAction);
				EquipmentIndex wieldedItemIndex2 = agent3.GetWieldedItemIndex(Agent.HandIndex.OffHand);
				bool flag2 = wieldedItemIndex2 != EquipmentIndex.None && agent3.Equipment[wieldedItemIndex2].Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnAnyAction);
				HumanAIComponent component = agent3.GetComponent<HumanAIComponent>();
				bool flag3 = ((component != null) ? component.GetCurrentlyMovingGameObject() : null) != null;
				bool flag4 = agent3.GetCurrentAnimationFlag(0).HasAnyFlag(AnimFlags.anf_synch_with_ladder_movement) || agent3.GetCurrentAnimationFlag(1).HasAnyFlag(AnimFlags.anf_synch_with_ladder_movement);
				if (!flag && !flag2 && !agent3.IsUsingGameObject && !flag3 && !flag4)
				{
					int num3 = list.Count - num2;
					int num4 = num - list2.Count;
					int num5 = num3 - num4;
					float num6 = MBMath.ClampFloat((float)(num - num5) / (float)num, 0f, 1f);
					float num7;
					Vec3 v;
					if (num6 < 1f && agent3.TryGetImmediateEnemyAgentMovementData(out num7, out v))
					{
						float maximumForwardUnlimitedSpeed = agent3.MaximumForwardUnlimitedSpeed;
						float num8 = num7;
						if (maximumForwardUnlimitedSpeed > num8)
						{
							float num9 = (agent3.Position - v).LengthSquared / (maximumForwardUnlimitedSpeed - num8);
							if (num9 < 900f)
							{
								float num10 = num6 - -1f;
								float num11 = num9 / 900f;
								num6 = -1f + num10 * num11;
							}
						}
					}
					if (MBRandom.RandomFloat <= 0.5f + 0.5f * num6)
					{
						list2.Add(agent3);
					}
				}
				num2++;
			}
			foreach (Agent agent2 in list2)
			{
				MatrixFrame frame = agent2.Frame;
				Vec2 asVec = frame.origin.AsVec2;
				Vec3 f = frame.rotation.f;
				agent2.SetTargetPositionAndDirectionSynched(ref asVec, ref f);
				this.SetTimersOfVictoryReactionsForSingleAgent(agent2, this._cheerReactionTimerData.MinDuration, this._cheerReactionTimerData.MaxDuration, true);
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x00073F14 File Offset: 0x00072114
		public void SetTimersOfVictoryReactionsOnTournamentVictoryForAgent(Agent agent, float minStartTime, float maxStartTime)
		{
			this._selectedCheerActions = this._midCheerActions;
			this.SetTimersOfVictoryReactionsForSingleAgent(agent, minStartTime, maxStartTime, false);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00073F2C File Offset: 0x0007212C
		private void SetTimersOfVictoryReactionsForSingleAgent(Agent agent, float minStartTime, float maxStartTime, bool isCheeringOnRetreat)
		{
			if (agent.IsActive() && agent.IsHuman && agent.IsAIControlled)
			{
				if (this._cheeringAgents.AnyQ((AgentVictoryLogic.CheeringAgent a) => a.Agent == agent))
				{
					Debug.FailedAssert("Adding a duplicate agent in _cheeringAgents", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\AgentVictoryLogic.cs", "SetTimersOfVictoryReactionsForSingleAgent", 412);
					Debug.Print("Adding a duplicate agent in _cheeringAgents", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				agent.AddComponent(new VictoryComponent(agent, new RandomTimer(base.Mission.CurrentTime, minStartTime, maxStartTime)));
				this._cheeringAgents.Add(new AgentVictoryLogic.CheeringAgent(agent, isCheeringOnRetreat));
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00074000 File Offset: 0x00072200
		private void ChooseWeaponToCheerWithCheerAndUpdateTimer(Agent cheerAgent, out bool resetTimer)
		{
			resetTimer = false;
			if (cheerAgent.GetCurrentActionType(1) != Agent.ActionCodeType.EquipUnequip)
			{
				EquipmentIndex wieldedItemIndex = cheerAgent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
				bool flag = wieldedItemIndex != EquipmentIndex.None && !cheerAgent.Equipment[wieldedItemIndex].Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnAnyAction);
				if (!flag)
				{
					EquipmentIndex equipmentIndex = EquipmentIndex.None;
					for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex2++)
					{
						if (!cheerAgent.Equipment[equipmentIndex2].IsEmpty && !cheerAgent.Equipment[equipmentIndex2].Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnAnyAction))
						{
							equipmentIndex = equipmentIndex2;
							break;
						}
					}
					if (equipmentIndex == EquipmentIndex.None)
					{
						if (wieldedItemIndex != EquipmentIndex.None)
						{
							cheerAgent.TryToSheathWeaponInHand(Agent.HandIndex.MainHand, Agent.WeaponWieldActionType.WithAnimation);
						}
						else
						{
							flag = true;
						}
					}
					else
					{
						cheerAgent.TryToWieldWeaponInSlot(equipmentIndex, Agent.WeaponWieldActionType.WithAnimation, false);
					}
				}
				if (flag)
				{
					ActionIndexCache[] array = this._selectedCheerActions;
					if (cheerAgent.HasMount)
					{
						array = this._midCheerActions;
					}
					cheerAgent.SetActionChannel(1, array[MBRandom.RandomInt(array.Length)], false, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
					cheerAgent.MakeVoice(SkinVoiceManager.VoiceType.Victory, SkinVoiceManager.CombatVoiceNetworkPredictionType.NoPrediction);
					resetTimer = true;
				}
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00074132 File Offset: 0x00072332
		private bool CheckIfIsInRetreat()
		{
			return this._isInRetreat;
		}

		// Token: 0x04000BF6 RID: 3062
		private const float HighCheerThreshold = 0.25f;

		// Token: 0x04000BF7 RID: 3063
		private const float MidCheerThreshold = 0.75f;

		// Token: 0x04000BF8 RID: 3064
		private const float YellOnCheerCancelProbability = 0.25f;

		// Token: 0x04000BF9 RID: 3065
		private AgentVictoryLogic.CheerActionGroupEnum _cheerActionGroup;

		// Token: 0x04000BFA RID: 3066
		private AgentVictoryLogic.CheerReactionTimeSettings _cheerReactionTimerData;

		// Token: 0x04000BFB RID: 3067
		private readonly ActionIndexCache[] _lowCheerActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_cheering_low_01"),
			ActionIndexCache.Create("act_cheering_low_02"),
			ActionIndexCache.Create("act_cheering_low_03"),
			ActionIndexCache.Create("act_cheering_low_04"),
			ActionIndexCache.Create("act_cheering_low_05"),
			ActionIndexCache.Create("act_cheering_low_06"),
			ActionIndexCache.Create("act_cheering_low_07"),
			ActionIndexCache.Create("act_cheering_low_08"),
			ActionIndexCache.Create("act_cheering_low_09"),
			ActionIndexCache.Create("act_cheering_low_10")
		};

		// Token: 0x04000BFC RID: 3068
		private readonly ActionIndexCache[] _midCheerActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_cheer_1"),
			ActionIndexCache.Create("act_cheer_2"),
			ActionIndexCache.Create("act_cheer_3"),
			ActionIndexCache.Create("act_cheer_4")
		};

		// Token: 0x04000BFD RID: 3069
		private readonly ActionIndexCache[] _highCheerActions = new ActionIndexCache[]
		{
			ActionIndexCache.Create("act_cheering_high_01"),
			ActionIndexCache.Create("act_cheering_high_02"),
			ActionIndexCache.Create("act_cheering_high_03"),
			ActionIndexCache.Create("act_cheering_high_04"),
			ActionIndexCache.Create("act_cheering_high_05"),
			ActionIndexCache.Create("act_cheering_high_06"),
			ActionIndexCache.Create("act_cheering_high_07"),
			ActionIndexCache.Create("act_cheering_high_08")
		};

		// Token: 0x04000BFE RID: 3070
		private ActionIndexCache[] _selectedCheerActions;

		// Token: 0x04000BFF RID: 3071
		private List<AgentVictoryLogic.CheeringAgent> _cheeringAgents;

		// Token: 0x04000C00 RID: 3072
		private bool _isInRetreat;

		// Token: 0x02000510 RID: 1296
		public enum CheerActionGroupEnum
		{
			// Token: 0x04001C10 RID: 7184
			None,
			// Token: 0x04001C11 RID: 7185
			LowCheerActions,
			// Token: 0x04001C12 RID: 7186
			MidCheerActions,
			// Token: 0x04001C13 RID: 7187
			HighCheerActions
		}

		// Token: 0x02000511 RID: 1297
		public struct CheerReactionTimeSettings
		{
			// Token: 0x0600384E RID: 14414 RVA: 0x000E1DA7 File Offset: 0x000DFFA7
			public CheerReactionTimeSettings(float minDuration, float maxDuration)
			{
				this.MinDuration = minDuration;
				this.MaxDuration = maxDuration;
			}

			// Token: 0x04001C14 RID: 7188
			public readonly float MinDuration;

			// Token: 0x04001C15 RID: 7189
			public readonly float MaxDuration;
		}

		// Token: 0x02000512 RID: 1298
		private class CheeringAgent
		{
			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x0600384F RID: 14415 RVA: 0x000E1DB7 File Offset: 0x000DFFB7
			// (set) Token: 0x06003850 RID: 14416 RVA: 0x000E1DBF File Offset: 0x000DFFBF
			public bool GotOrderRecently { get; private set; }

			// Token: 0x06003851 RID: 14417 RVA: 0x000E1DC8 File Offset: 0x000DFFC8
			public CheeringAgent(Agent agent, bool isCheeringOnRetreat)
			{
				this.Agent = agent;
				this.IsCheeringOnRetreat = isCheeringOnRetreat;
			}

			// Token: 0x06003852 RID: 14418 RVA: 0x000E1DDE File Offset: 0x000DFFDE
			public void OrderReceived()
			{
				this.GotOrderRecently = true;
			}

			// Token: 0x04001C16 RID: 7190
			public readonly Agent Agent;

			// Token: 0x04001C17 RID: 7191
			public readonly bool IsCheeringOnRetreat;
		}
	}
}
