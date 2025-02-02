using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000276 RID: 630
	public class HighlightsController : MissionLogic
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x00077BAF File Offset: 0x00075DAF
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x00077BB6 File Offset: 0x00075DB6
		private protected static List<HighlightsController.HighlightType> HighlightTypes { protected get; private set; }

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00077BBE File Offset: 0x00075DBE
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x00077BC5 File Offset: 0x00075DC5
		public static bool IsHighlightsInitialized { get; private set; }

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00077BCD File Offset: 0x00075DCD
		public bool IsAnyHighlightSaved
		{
			get
			{
				return this._savedHighlightGroups.Count > 0;
			}
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x00077BE0 File Offset: 0x00075DE0
		public static void RemoveHighlights()
		{
			if (HighlightsController.IsHighlightsInitialized)
			{
				foreach (HighlightsController.HighlightType highlightType in HighlightsController.HighlightTypes)
				{
					Highlights.RemoveHighlight(highlightType.Id);
				}
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x00077C40 File Offset: 0x00075E40
		public HighlightsController.HighlightType GetHighlightTypeWithId(string highlightId)
		{
			return HighlightsController.HighlightTypes.First((HighlightsController.HighlightType h) => h.Id == highlightId);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x00077C70 File Offset: 0x00075E70
		private void SaveVideo(string highlightID, string groupID, int startDelta, int endDelta)
		{
			Highlights.SaveVideo(highlightID, groupID, startDelta, endDelta);
			if (!this._savedHighlightGroups.Contains(groupID))
			{
				this._savedHighlightGroups.Add(groupID);
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00077C98 File Offset: 0x00075E98
		public override void AfterStart()
		{
			if (!HighlightsController.IsHighlightsInitialized)
			{
				HighlightsController.HighlightTypes = new List<HighlightsController.HighlightType>
				{
					new HighlightsController.HighlightType("hlid_killing_spree", "Killing Spree", "grpid_incidents", -2010, 3000, 0.25f, float.MaxValue, true),
					new HighlightsController.HighlightType("hlid_high_ranged_shot_difficulty", "Sharpshooter", "grpid_incidents", -5000, 3000, 0.25f, float.MaxValue, true),
					new HighlightsController.HighlightType("hlid_archer_salvo_kills", "Death from Above", "grpid_incidents", -5004, 3000, 0.5f, 150f, false),
					new HighlightsController.HighlightType("hlid_couched_lance_against_mounted_opponent", "Lance A Lot", "grpid_incidents", -5000, 3000, 0.25f, float.MaxValue, true),
					new HighlightsController.HighlightType("hlid_cavalry_charge_first_impact", "Cavalry Charge First Impact", "grpid_incidents", -5000, 5000, 0.25f, float.MaxValue, false),
					new HighlightsController.HighlightType("hlid_headshot_kill", "Headshot!", "grpid_incidents", -5000, 3000, 0.25f, 150f, true),
					new HighlightsController.HighlightType("hlid_burning_ammunition_kill", "Burn Baby", "grpid_incidents", -5000, 3000, 0.25f, 100f, true),
					new HighlightsController.HighlightType("hlid_throwing_weapon_kill_against_charging_enemy", "Throwing Weapon Kill Against Charging Enemy", "grpid_incidents", -5000, 3000, 0.25f, 150f, true)
				};
				Highlights.Initialize();
				foreach (HighlightsController.HighlightType highlightType in HighlightsController.HighlightTypes)
				{
					Highlights.AddHighlight(highlightType.Id, highlightType.Description);
				}
				HighlightsController.IsHighlightsInitialized = true;
			}
			foreach (string id in this._highlightGroupIds)
			{
				Highlights.OpenGroup(id);
			}
			this._highlightSaveQueue = new List<HighlightsController.Highlight>();
			this._playerKillTimes = new List<float>();
			this._archerSalvoKillTimes = new List<float>();
			this._cavalryChargeHitTimes = new List<float>();
			this._savedHighlightGroups = new List<string>();
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00077F04 File Offset: 0x00076104
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectorAgent != null && affectedAgent != null && affectorAgent.IsHuman && affectedAgent.IsHuman && (agentState == AgentState.Killed || agentState == AgentState.Unconscious))
			{
				bool flag = affectorAgent.Team != null && affectorAgent.Team.IsPlayerTeam;
				bool isMainAgent = affectorAgent.IsMainAgent;
				if ((((isMainAgent || flag) && !affectedAgent.Team.IsPlayerAlly && killingBlow.WeaponClass == 12) || killingBlow.WeaponClass == 13) && this.CanSaveHighlight(this.GetHighlightTypeWithId("hlid_archer_salvo_kills"), affectedAgent.Position))
				{
					if (!this._isArcherSalvoHappening)
					{
						this._archerSalvoKillTimes.RemoveAll((float ht) => ht + 4f < Mission.Current.CurrentTime);
					}
					this._archerSalvoKillTimes.Add(Mission.Current.CurrentTime);
					if (this._archerSalvoKillTimes.Count >= 5)
					{
						this._isArcherSalvoHappening = true;
					}
				}
				if (isMainAgent && this.CanSaveHighlight(this.GetHighlightTypeWithId("hlid_killing_spree"), affectedAgent.Position))
				{
					if (!this._isKillingSpreeHappening)
					{
						this._playerKillTimes.RemoveAll((float ht) => ht + 10f < Mission.Current.CurrentTime);
					}
					this._playerKillTimes.Add(Mission.Current.CurrentTime);
					if (this._playerKillTimes.Count >= 4)
					{
						this._isKillingSpreeHappening = true;
					}
				}
				HighlightsController.Highlight highlight = default(HighlightsController.Highlight);
				highlight.Start = Mission.Current.CurrentTime;
				highlight.End = Mission.Current.CurrentTime;
				bool flag2 = false;
				if (isMainAgent && killingBlow.WeaponRecordWeaponFlags.HasAllFlags(WeaponFlags.Burning))
				{
					highlight.HighlightType = this.GetHighlightTypeWithId("hlid_burning_ammunition_kill");
					flag2 = true;
				}
				if (isMainAgent && killingBlow.IsMissile && killingBlow.IsHeadShot())
				{
					highlight.HighlightType = this.GetHighlightTypeWithId("hlid_headshot_kill");
					flag2 = true;
				}
				if (isMainAgent && killingBlow.IsMissile && affectedAgent.HasMount && affectedAgent.IsDoingPassiveAttack && (killingBlow.WeaponClass == 19 || killingBlow.WeaponClass == 20))
				{
					highlight.HighlightType = this.GetHighlightTypeWithId("hlid_throwing_weapon_kill_against_charging_enemy");
					flag2 = true;
				}
				if (this._isFirstImpact && affectorAgent.Formation != null && affectorAgent.Formation.PhysicalClass.IsMeleeCavalry() && affectorAgent.Formation.GetReadonlyMovementOrderReference() == MovementOrder.MovementOrderCharge && this.CanSaveHighlight(this.GetHighlightTypeWithId("hlid_cavalry_charge_first_impact"), affectedAgent.Position))
				{
					this._cavalryChargeHitTimes.RemoveAll((float ht) => ht + 3f < Mission.Current.CurrentTime);
					this._cavalryChargeHitTimes.Add(Mission.Current.CurrentTime);
					if (this._cavalryChargeHitTimes.Count >= 5)
					{
						highlight.HighlightType = this.GetHighlightTypeWithId("hlid_cavalry_charge_first_impact");
						highlight.Start = this._cavalryChargeHitTimes[0];
						highlight.End = this._cavalryChargeHitTimes[this._cavalryChargeHitTimes.Count - 1];
						flag2 = true;
						this._isFirstImpact = false;
						this._cavalryChargeHitTimes.Clear();
					}
				}
				if (flag2)
				{
					this.SaveHighlight(highlight, affectedAgent.Position);
				}
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x00078250 File Offset: 0x00076450
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent != null && affectedAgent != null && affectorAgent.IsHuman && affectedAgent.IsHuman)
			{
				bool isMainAgent = affectorAgent.IsMainAgent;
				HighlightsController.Highlight highlight = default(HighlightsController.Highlight);
				highlight.Start = Mission.Current.CurrentTime;
				highlight.End = Mission.Current.CurrentTime;
				bool flag = false;
				if (isMainAgent && shotDifficulty >= 7.5f)
				{
					highlight.HighlightType = this.GetHighlightTypeWithId("hlid_high_ranged_shot_difficulty");
					flag = true;
				}
				if (isMainAgent && affectedAgent.HasMount && blow.AttackType == AgentAttackType.Standard && affectorAgent.HasMount && affectorAgent.IsDoingPassiveAttack)
				{
					highlight.HighlightType = this.GetHighlightTypeWithId("hlid_couched_lance_against_mounted_opponent");
					flag = true;
				}
				if (this._isFirstImpact && affectorAgent.Formation != null && affectorAgent.Formation.PhysicalClass.IsMeleeCavalry() && affectorAgent.Formation.GetReadonlyMovementOrderReference() == MovementOrder.MovementOrderCharge && this.CanSaveHighlight(this.GetHighlightTypeWithId("hlid_cavalry_charge_first_impact"), affectedAgent.Position))
				{
					this._cavalryChargeHitTimes.RemoveAll((float ht) => ht + 3f < Mission.Current.CurrentTime);
					this._cavalryChargeHitTimes.Add(Mission.Current.CurrentTime);
					if (this._cavalryChargeHitTimes.Count >= 5)
					{
						highlight.HighlightType = this.GetHighlightTypeWithId("hlid_cavalry_charge_first_impact");
						highlight.Start = this._cavalryChargeHitTimes[0];
						highlight.End = this._cavalryChargeHitTimes[this._cavalryChargeHitTimes.Count - 1];
						flag = true;
						this._isFirstImpact = false;
						this._cavalryChargeHitTimes.Clear();
					}
				}
				if (flag)
				{
					this.SaveHighlight(highlight, affectedAgent.Position);
				}
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x00078420 File Offset: 0x00076620
		public override void OnMissionTick(float dt)
		{
			if (this._isArcherSalvoHappening && this._archerSalvoKillTimes[0] + 4f < Mission.Current.CurrentTime)
			{
				HighlightsController.Highlight highlight;
				highlight.HighlightType = this.GetHighlightTypeWithId("hlid_archer_salvo_kills");
				highlight.Start = this._archerSalvoKillTimes[0];
				highlight.End = this._archerSalvoKillTimes[this._archerSalvoKillTimes.Count - 1];
				this.SaveHighlight(highlight);
				this._isArcherSalvoHappening = false;
				this._archerSalvoKillTimes.Clear();
			}
			if (this._isKillingSpreeHappening && this._playerKillTimes[0] + 10f < Mission.Current.CurrentTime)
			{
				HighlightsController.Highlight highlight2;
				highlight2.HighlightType = this.GetHighlightTypeWithId("hlid_killing_spree");
				highlight2.Start = this._playerKillTimes[0];
				highlight2.End = this._playerKillTimes[this._playerKillTimes.Count - 1];
				this.SaveHighlight(highlight2);
				this._isKillingSpreeHappening = false;
				this._playerKillTimes.Clear();
			}
			this.TickHighlightsToBeSaved();
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0007853C File Offset: 0x0007673C
		protected override void OnEndMission()
		{
			base.OnEndMission();
			foreach (string id in this._highlightGroupIds)
			{
				Highlights.CloseGroup(id, false);
			}
			this._highlightSaveQueue = null;
			this._lastSavedHighlightData = null;
			this._playerKillTimes = null;
			this._archerSalvoKillTimes = null;
			this._cavalryChargeHitTimes = null;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x000785B8 File Offset: 0x000767B8
		public static void AddHighlightType(HighlightsController.HighlightType highlightType)
		{
			if (!HighlightsController.HighlightTypes.Any((HighlightsController.HighlightType h) => h.Id == highlightType.Id))
			{
				if (HighlightsController.IsHighlightsInitialized)
				{
					Highlights.AddHighlight(highlightType.Id, highlightType.Description);
				}
				HighlightsController.HighlightTypes.Add(highlightType);
			}
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0007861C File Offset: 0x0007681C
		public void SaveHighlight(HighlightsController.Highlight highlight)
		{
			this._highlightSaveQueue.Add(highlight);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0007862A File Offset: 0x0007682A
		public void SaveHighlight(HighlightsController.Highlight highlight, Vec3 position)
		{
			if (this.CanSaveHighlight(highlight.HighlightType, position))
			{
				this._highlightSaveQueue.Add(highlight);
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00078648 File Offset: 0x00076848
		public bool CanSaveHighlight(HighlightsController.HighlightType highlightType, Vec3 position)
		{
			return highlightType.MaxHighlightDistance >= Mission.Current.Scene.LastFinalRenderCameraFrame.origin.Distance(position) && highlightType.MinVisibilityScore <= this.GetPlayerIsLookingAtPositionScore(position) && (!highlightType.IsVisibilityRequired || this.CanSeePosition(position));
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x000786A0 File Offset: 0x000768A0
		public float GetPlayerIsLookingAtPositionScore(Vec3 position)
		{
			Vec3 vec = -Mission.Current.Scene.LastFinalRenderCameraFrame.rotation.u;
			Vec3 origin = Mission.Current.Scene.LastFinalRenderCameraFrame.origin;
			return MathF.Max(Vec3.DotProduct(vec.NormalizedCopy(), (position - origin).NormalizedCopy()), 0f);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00078708 File Offset: 0x00076908
		public bool CanSeePosition(Vec3 position)
		{
			Vec3 origin = Mission.Current.Scene.LastFinalRenderCameraFrame.origin;
			float num;
			return !Mission.Current.Scene.RayCastForClosestEntityOrTerrain(origin, position, out num, 0.01f, BodyFlags.CameraCollisionRayCastExludeFlags) || MathF.Abs(position.Distance(origin) - num) < 0.1f;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00078761 File Offset: 0x00076961
		public void ShowSummary()
		{
			if (this.IsAnyHighlightSaved)
			{
				Highlights.OpenSummary(this._savedHighlightGroups);
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00078778 File Offset: 0x00076978
		private void TickHighlightsToBeSaved()
		{
			if (this._highlightSaveQueue != null)
			{
				if (this._lastSavedHighlightData != null && this._highlightSaveQueue.Count > 0)
				{
					float item = this._lastSavedHighlightData.Item1;
					float item2 = this._lastSavedHighlightData.Item2;
					float num = item2 - (item2 - item) * 0.5f;
					for (int i = 0; i < this._highlightSaveQueue.Count; i++)
					{
						float start = this._highlightSaveQueue[i].Start;
						HighlightsController.Highlight highlight = this._highlightSaveQueue[i];
						if (start - (float)highlight.HighlightType.StartDelta < num)
						{
							this._highlightSaveQueue.Remove(this._highlightSaveQueue[i]);
							i--;
						}
					}
				}
				if (this._highlightSaveQueue.Count > 0)
				{
					float start2 = this._highlightSaveQueue[0].Start;
					HighlightsController.Highlight highlight = this._highlightSaveQueue[0];
					float num2 = start2 + (float)(highlight.HighlightType.StartDelta / 1000);
					float end = this._highlightSaveQueue[0].End;
					highlight = this._highlightSaveQueue[0];
					float num3 = end + (float)(highlight.HighlightType.EndDelta / 1000);
					for (int j = 1; j < this._highlightSaveQueue.Count; j++)
					{
						float start3 = this._highlightSaveQueue[j].Start;
						highlight = this._highlightSaveQueue[j];
						float num4 = start3 + (float)(highlight.HighlightType.StartDelta / 1000);
						float end2 = this._highlightSaveQueue[j].End;
						highlight = this._highlightSaveQueue[j];
						float num5 = end2 + (float)(highlight.HighlightType.EndDelta / 1000);
						if (num4 < num2)
						{
							num2 = num4;
						}
						if (num5 > num3)
						{
							num3 = num5;
						}
					}
					highlight = this._highlightSaveQueue[0];
					string id = highlight.HighlightType.Id;
					highlight = this._highlightSaveQueue[0];
					this.SaveVideo(id, highlight.HighlightType.GroupId, (int)(num2 - Mission.Current.CurrentTime) * 1000, (int)(num3 - Mission.Current.CurrentTime) * 1000);
					this._lastSavedHighlightData = new Tuple<float, float>(num2, num3);
					this._highlightSaveQueue.Clear();
				}
			}
		}

		// Token: 0x04000C4A RID: 3146
		private bool _isKillingSpreeHappening;

		// Token: 0x04000C4B RID: 3147
		private List<float> _playerKillTimes;

		// Token: 0x04000C4C RID: 3148
		private const int MinKillingSpreeKills = 4;

		// Token: 0x04000C4D RID: 3149
		private const float MaxKillingSpreeDuration = 10f;

		// Token: 0x04000C4E RID: 3150
		private const float HighShotDifficultyThreshold = 7.5f;

		// Token: 0x04000C4F RID: 3151
		private bool _isArcherSalvoHappening;

		// Token: 0x04000C50 RID: 3152
		private List<float> _archerSalvoKillTimes;

		// Token: 0x04000C51 RID: 3153
		private const int MinArcherSalvoKills = 5;

		// Token: 0x04000C52 RID: 3154
		private const float MaxArcherSalvoDuration = 4f;

		// Token: 0x04000C53 RID: 3155
		private bool _isFirstImpact = true;

		// Token: 0x04000C54 RID: 3156
		private List<float> _cavalryChargeHitTimes;

		// Token: 0x04000C55 RID: 3157
		private const float CavalryChargeImpactTimeFrame = 3f;

		// Token: 0x04000C56 RID: 3158
		private const int MinCavalryChargeHits = 5;

		// Token: 0x04000C57 RID: 3159
		private Tuple<float, float> _lastSavedHighlightData;

		// Token: 0x04000C58 RID: 3160
		private List<HighlightsController.Highlight> _highlightSaveQueue;

		// Token: 0x04000C59 RID: 3161
		private const float IgnoreIfOverlapsLastVideoPercent = 0.5f;

		// Token: 0x04000C5A RID: 3162
		private List<string> _savedHighlightGroups;

		// Token: 0x04000C5B RID: 3163
		private List<string> _highlightGroupIds = new List<string>
		{
			"grpid_incidents",
			"grpid_achievements"
		};

		// Token: 0x02000527 RID: 1319
		public struct HighlightType
		{
			// Token: 0x17000981 RID: 2433
			// (get) Token: 0x060038AC RID: 14508 RVA: 0x000E2F67 File Offset: 0x000E1167
			// (set) Token: 0x060038AD RID: 14509 RVA: 0x000E2F6F File Offset: 0x000E116F
			public string Id { get; private set; }

			// Token: 0x17000982 RID: 2434
			// (get) Token: 0x060038AE RID: 14510 RVA: 0x000E2F78 File Offset: 0x000E1178
			// (set) Token: 0x060038AF RID: 14511 RVA: 0x000E2F80 File Offset: 0x000E1180
			public string Description { get; private set; }

			// Token: 0x17000983 RID: 2435
			// (get) Token: 0x060038B0 RID: 14512 RVA: 0x000E2F89 File Offset: 0x000E1189
			// (set) Token: 0x060038B1 RID: 14513 RVA: 0x000E2F91 File Offset: 0x000E1191
			public string GroupId { get; private set; }

			// Token: 0x17000984 RID: 2436
			// (get) Token: 0x060038B2 RID: 14514 RVA: 0x000E2F9A File Offset: 0x000E119A
			// (set) Token: 0x060038B3 RID: 14515 RVA: 0x000E2FA2 File Offset: 0x000E11A2
			public int StartDelta { get; private set; }

			// Token: 0x17000985 RID: 2437
			// (get) Token: 0x060038B4 RID: 14516 RVA: 0x000E2FAB File Offset: 0x000E11AB
			// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000E2FB3 File Offset: 0x000E11B3
			public int EndDelta { get; private set; }

			// Token: 0x17000986 RID: 2438
			// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000E2FBC File Offset: 0x000E11BC
			// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000E2FC4 File Offset: 0x000E11C4
			public float MinVisibilityScore { get; private set; }

			// Token: 0x17000987 RID: 2439
			// (get) Token: 0x060038B8 RID: 14520 RVA: 0x000E2FCD File Offset: 0x000E11CD
			// (set) Token: 0x060038B9 RID: 14521 RVA: 0x000E2FD5 File Offset: 0x000E11D5
			public float MaxHighlightDistance { get; private set; }

			// Token: 0x17000988 RID: 2440
			// (get) Token: 0x060038BA RID: 14522 RVA: 0x000E2FDE File Offset: 0x000E11DE
			// (set) Token: 0x060038BB RID: 14523 RVA: 0x000E2FE6 File Offset: 0x000E11E6
			public bool IsVisibilityRequired { get; private set; }

			// Token: 0x060038BC RID: 14524 RVA: 0x000E2FEF File Offset: 0x000E11EF
			public HighlightType(string id, string description, string groupId, int startDelta, int endDelta, float minVisibilityScore, float maxHighlightDistance, bool isVisibilityRequired)
			{
				this.Id = id;
				this.Description = description;
				this.GroupId = groupId;
				this.StartDelta = startDelta;
				this.EndDelta = endDelta;
				this.MinVisibilityScore = minVisibilityScore;
				this.MaxHighlightDistance = maxHighlightDistance;
				this.IsVisibilityRequired = isVisibilityRequired;
			}
		}

		// Token: 0x02000528 RID: 1320
		public struct Highlight
		{
			// Token: 0x04001C55 RID: 7253
			public HighlightsController.HighlightType HighlightType;

			// Token: 0x04001C56 RID: 7254
			public float Start;

			// Token: 0x04001C57 RID: 7255
			public float End;
		}
	}
}
