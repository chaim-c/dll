using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.MissionLogics.Towns;
using SandBox.Objects;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.ViewModelCollection.Missions.NameMarker
{
	// Token: 0x02000027 RID: 39
	public class MissionNameMarkerVM : ViewModel
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0000F346 File Offset: 0x0000D546
		// (set) Token: 0x06000323 RID: 803 RVA: 0x0000F34E File Offset: 0x0000D54E
		public bool IsTargetsAdded { get; private set; }

		// Token: 0x06000324 RID: 804 RVA: 0x0000F358 File Offset: 0x0000D558
		public MissionNameMarkerVM(Mission mission, Camera missionCamera, Dictionary<Agent, SandBoxUIHelper.IssueQuestFlags> additionalTargetAgents, Dictionary<string, ValueTuple<Vec3, string, string>> additionalGenericTargets)
		{
			this.Targets = new MBBindingList<MissionNameMarkerTargetVM>();
			this._distanceComparer = new MissionNameMarkerVM.MarkerDistanceComparer();
			this._missionCamera = missionCamera;
			this._additionalTargetAgents = additionalTargetAgents;
			this._additionalGenericTargets = additionalGenericTargets;
			this._genericTargets = new Dictionary<string, MissionNameMarkerTargetVM>();
			this._mission = mission;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F3FD File Offset: 0x0000D5FD
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Targets.ApplyActionOnAllItems(delegate(MissionNameMarkerTargetVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F42F File Offset: 0x0000D62F
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Targets.ApplyActionOnAllItems(delegate(MissionNameMarkerTargetVM x)
			{
				x.OnFinalize();
			});
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F464 File Offset: 0x0000D664
		public void Tick(float dt)
		{
			if (!this.IsTargetsAdded)
			{
				if (this._mission.MainAgent != null)
				{
					if (this._additionalTargetAgents != null)
					{
						foreach (KeyValuePair<Agent, SandBoxUIHelper.IssueQuestFlags> keyValuePair in this._additionalTargetAgents)
						{
							this.AddAgentTarget(keyValuePair.Key, true);
							this.UpdateAdditionalTargetAgentQuestStatus(keyValuePair.Key, keyValuePair.Value);
						}
					}
					if (this._additionalGenericTargets != null)
					{
						foreach (KeyValuePair<string, ValueTuple<Vec3, string, string>> keyValuePair2 in this._additionalGenericTargets)
						{
							this.AddGenericMarker(keyValuePair2.Key, keyValuePair2.Value.Item1, keyValuePair2.Value.Item2, keyValuePair2.Value.Item3);
						}
					}
					foreach (Agent agent in this._mission.Agents)
					{
						this.AddAgentTarget(agent, false);
					}
					if (Hero.MainHero.CurrentSettlement != null)
					{
						List<CommonAreaMarker> list = (from x in this._mission.ActiveMissionObjects.FindAllWithType<CommonAreaMarker>()
						where x.GameEntity.HasTag("alley_marker")
						select x).ToList<CommonAreaMarker>();
						if (Hero.MainHero.CurrentSettlement.Alleys.Count > 0)
						{
							foreach (CommonAreaMarker commonAreaMarker in list)
							{
								Alley alley = commonAreaMarker.GetAlley();
								if (alley != null && alley.Owner != null)
								{
									this.Targets.Add(new MissionNameMarkerTargetVM(commonAreaMarker));
								}
							}
						}
						foreach (PassageUsePoint passageUsePoint in from passage in this._mission.ActiveMissionObjects.FindAllWithType<PassageUsePoint>().ToList<PassageUsePoint>()
						where passage.ToLocation != null && !this.PassagePointFilter.Exists((string s) => passage.ToLocation.Name.Contains(s))
						select passage)
						{
							if (!passageUsePoint.ToLocation.CanBeReserved || passageUsePoint.ToLocation.IsReserved)
							{
								this.Targets.Add(new MissionNameMarkerTargetVM(passageUsePoint));
							}
						}
						if (this._mission.HasMissionBehavior<WorkshopMissionHandler>())
						{
							foreach (Tuple<Workshop, GameEntity> tuple in from s in this._mission.GetMissionBehavior<WorkshopMissionHandler>().WorkshopSignEntities.ToList<Tuple<Workshop, GameEntity>>()
							where s.Item1.WorkshopType != null
							select s)
							{
								this.Targets.Add(new MissionNameMarkerTargetVM(tuple.Item1.WorkshopType, tuple.Item2.GlobalPosition - this._defaultHeightOffset));
							}
						}
					}
				}
				this.IsTargetsAdded = true;
			}
			if (this.IsEnabled)
			{
				this.UpdateTargetScreenPositions();
				this._fadeOutTimerStarted = false;
				this._fadeOutTimer = 0f;
				this._prevEnabledState = this.IsEnabled;
			}
			else
			{
				if (this._prevEnabledState)
				{
					this._fadeOutTimerStarted = true;
				}
				if (this._fadeOutTimerStarted)
				{
					this._fadeOutTimer += dt;
				}
				if (this._fadeOutTimer < 2f)
				{
					this.UpdateTargetScreenPositions();
				}
				else
				{
					this._fadeOutTimerStarted = false;
				}
			}
			this._prevEnabledState = this.IsEnabled;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F834 File Offset: 0x0000DA34
		private void UpdateTargetScreenPositions()
		{
			foreach (MissionNameMarkerTargetVM missionNameMarkerTargetVM in this.Targets)
			{
				float a = -100f;
				float b = -100f;
				float num = 0f;
				Vec3 v = (missionNameMarkerTargetVM.TargetAgent != null) ? this._agentHeightOffset : this._defaultHeightOffset;
				MBWindowManager.WorldToScreenInsideUsableArea(this._missionCamera, missionNameMarkerTargetVM.WorldPosition + v, ref a, ref b, ref num);
				if (num > 0f)
				{
					missionNameMarkerTargetVM.ScreenPosition = new Vec2(a, b);
					missionNameMarkerTargetVM.Distance = (int)(missionNameMarkerTargetVM.WorldPosition - this._missionCamera.Position).Length;
				}
				else
				{
					missionNameMarkerTargetVM.Distance = -1;
					missionNameMarkerTargetVM.ScreenPosition = new Vec2(-100f, -100f);
				}
			}
			this.Targets.Sort(this._distanceComparer);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F938 File Offset: 0x0000DB38
		public void OnConversationEnd()
		{
			foreach (Agent agent in this._mission.Agents)
			{
				this.AddAgentTarget(agent, false);
			}
			foreach (MissionNameMarkerTargetVM missionNameMarkerTargetVM in this.Targets)
			{
				if (!missionNameMarkerTargetVM.IsAdditionalTargetAgent)
				{
					missionNameMarkerTargetVM.UpdateQuestStatus();
				}
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F9D4 File Offset: 0x0000DBD4
		public void OnAgentBuild(Agent agent)
		{
			this.AddAgentTarget(agent, false);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F9DE File Offset: 0x0000DBDE
		public void OnAgentRemoved(Agent agent)
		{
			this.RemoveAgentTarget(agent);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
		public void OnAgentDeleted(Agent agent)
		{
			this.RemoveAgentTarget(agent);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public void UpdateAdditionalTargetAgentQuestStatus(Agent agent, SandBoxUIHelper.IssueQuestFlags issueQuestFlags)
		{
			MissionNameMarkerTargetVM missionNameMarkerTargetVM = this.Targets.FirstOrDefault((MissionNameMarkerTargetVM t) => t.TargetAgent == agent);
			if (missionNameMarkerTargetVM == null)
			{
				return;
			}
			missionNameMarkerTargetVM.UpdateQuestStatus(issueQuestFlags);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public void AddGenericMarker(string markerIdentifier, Vec3 markerPosition, string name, string iconType)
		{
			MissionNameMarkerTargetVM missionNameMarkerTargetVM;
			if (this._genericTargets.TryGetValue(markerIdentifier, out missionNameMarkerTargetVM))
			{
				Debug.FailedAssert("Marker with identifier: " + markerIdentifier + " already exists", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Missions\\NameMarker\\MissionNameMarkerVM.cs", "AddGenericMarker", 229);
				return;
			}
			MissionNameMarkerTargetVM missionNameMarkerTargetVM2 = new MissionNameMarkerTargetVM(markerPosition, name, iconType);
			this._genericTargets.Add(markerIdentifier, missionNameMarkerTargetVM2);
			this.Targets.Add(missionNameMarkerTargetVM2);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000FA94 File Offset: 0x0000DC94
		public void RemoveGenericMarker(string markerIdentifier)
		{
			MissionNameMarkerTargetVM item;
			if (this._genericTargets.TryGetValue(markerIdentifier, out item))
			{
				this._genericTargets.Remove(markerIdentifier);
				this.Targets.Remove(item);
				return;
			}
			Debug.FailedAssert("Marker with identifier: " + markerIdentifier + " does not exist", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.ViewModelCollection\\Missions\\NameMarker\\MissionNameMarkerVM.cs", "RemoveGenericMarker", 248);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
		public void AddAgentTarget(Agent agent, bool isAdditional = false)
		{
			Agent agent2 = agent;
			if (((agent2 != null) ? agent2.Character : null) != null && agent != Agent.Main && agent.IsActive() && !this.Targets.Any((MissionNameMarkerTargetVM t) => t.TargetAgent == agent))
			{
				bool flag5;
				if (!isAdditional && !agent.Character.IsHero)
				{
					Settlement currentSettlement = Settlement.CurrentSettlement;
					bool flag;
					if (currentSettlement == null)
					{
						flag = false;
					}
					else
					{
						LocationComplex locationComplex = currentSettlement.LocationComplex;
						bool? flag2;
						if (locationComplex == null)
						{
							flag2 = null;
						}
						else
						{
							LocationCharacter locationCharacter = locationComplex.FindCharacter(agent);
							flag2 = ((locationCharacter != null) ? new bool?(locationCharacter.IsVisualTracked) : null);
						}
						bool? flag3 = flag2;
						bool flag4 = true;
						flag = (flag3.GetValueOrDefault() == flag4 & flag3 != null);
					}
					CharacterObject characterObject;
					if (!flag && ((characterObject = (agent.Character as CharacterObject)) == null || (characterObject.Occupation != Occupation.RansomBroker && characterObject.Occupation != Occupation.Tavernkeeper)))
					{
						BasicCharacterObject character = agent.Character;
						Settlement currentSettlement2 = Settlement.CurrentSettlement;
						object obj;
						if (currentSettlement2 == null)
						{
							obj = null;
						}
						else
						{
							CultureObject culture = currentSettlement2.Culture;
							obj = ((culture != null) ? culture.Blacksmith : null);
						}
						if (character != obj)
						{
							BasicCharacterObject character2 = agent.Character;
							Settlement currentSettlement3 = Settlement.CurrentSettlement;
							object obj2;
							if (currentSettlement3 == null)
							{
								obj2 = null;
							}
							else
							{
								CultureObject culture2 = currentSettlement3.Culture;
								obj2 = ((culture2 != null) ? culture2.Barber : null);
							}
							if (character2 != obj2)
							{
								BasicCharacterObject character3 = agent.Character;
								Settlement currentSettlement4 = Settlement.CurrentSettlement;
								object obj3;
								if (currentSettlement4 == null)
								{
									obj3 = null;
								}
								else
								{
									CultureObject culture3 = currentSettlement4.Culture;
									obj3 = ((culture3 != null) ? culture3.TavernGamehost : null);
								}
								if (character3 != obj3)
								{
									flag5 = (agent.Character.StringId == "sp_hermit");
									goto IL_1A3;
								}
							}
						}
					}
				}
				flag5 = true;
				IL_1A3:
				if (flag5)
				{
					MissionNameMarkerTargetVM item = new MissionNameMarkerTargetVM(agent, isAdditional);
					this.Targets.Add(item);
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		public void RemoveAgentTarget(Agent agent)
		{
			if (this.Targets.SingleOrDefault((MissionNameMarkerTargetVM t) => t.TargetAgent == agent) != null)
			{
				this.Targets.Remove(this.Targets.Single((MissionNameMarkerTargetVM t) => t.TargetAgent == agent));
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000FD18 File Offset: 0x0000DF18
		private void UpdateTargetStates(bool state)
		{
			foreach (MissionNameMarkerTargetVM missionNameMarkerTargetVM in this.Targets)
			{
				missionNameMarkerTargetVM.IsEnabled = state;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000FD64 File Offset: 0x0000DF64
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		[DataSourceProperty]
		public MBBindingList<MissionNameMarkerTargetVM> Targets
		{
			get
			{
				return this._targets;
			}
			set
			{
				if (value != this._targets)
				{
					this._targets = value;
					base.OnPropertyChangedWithValue<MBBindingList<MissionNameMarkerTargetVM>>(value, "Targets");
				}
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000FD8A File Offset: 0x0000DF8A
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000FD92 File Offset: 0x0000DF92
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (value != this._isEnabled)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
					this.UpdateTargetStates(value);
					Game.Current.EventManager.TriggerEvent<MissionNameMarkerToggleEvent>(new MissionNameMarkerToggleEvent(value));
				}
			}
		}

		// Token: 0x040001A0 RID: 416
		private readonly Camera _missionCamera;

		// Token: 0x040001A1 RID: 417
		private readonly Mission _mission;

		// Token: 0x040001A2 RID: 418
		private Vec3 _agentHeightOffset = new Vec3(0f, 0f, 0.35f, -1f);

		// Token: 0x040001A3 RID: 419
		private Vec3 _defaultHeightOffset = new Vec3(0f, 0f, 2f, -1f);

		// Token: 0x040001A4 RID: 420
		private bool _prevEnabledState;

		// Token: 0x040001A5 RID: 421
		private bool _fadeOutTimerStarted;

		// Token: 0x040001A6 RID: 422
		private float _fadeOutTimer;

		// Token: 0x040001A7 RID: 423
		private Dictionary<Agent, SandBoxUIHelper.IssueQuestFlags> _additionalTargetAgents;

		// Token: 0x040001A8 RID: 424
		private Dictionary<string, ValueTuple<Vec3, string, string>> _additionalGenericTargets;

		// Token: 0x040001A9 RID: 425
		private Dictionary<string, MissionNameMarkerTargetVM> _genericTargets;

		// Token: 0x040001AA RID: 426
		private readonly MissionNameMarkerVM.MarkerDistanceComparer _distanceComparer;

		// Token: 0x040001AB RID: 427
		private readonly List<string> PassagePointFilter = new List<string>
		{
			"Empty Shop"
		};

		// Token: 0x040001AC RID: 428
		private MBBindingList<MissionNameMarkerTargetVM> _targets;

		// Token: 0x040001AD RID: 429
		private bool _isEnabled;

		// Token: 0x0200008C RID: 140
		private class MarkerDistanceComparer : IComparer<MissionNameMarkerTargetVM>
		{
			// Token: 0x06000570 RID: 1392 RVA: 0x00014DAC File Offset: 0x00012FAC
			public int Compare(MissionNameMarkerTargetVM x, MissionNameMarkerTargetVM y)
			{
				return y.Distance.CompareTo(x.Distance);
			}
		}
	}
}
