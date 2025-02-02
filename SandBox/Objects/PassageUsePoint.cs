using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Objects
{
	// Token: 0x02000037 RID: 55
	public class PassageUsePoint : StandingPoint
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		public MBReadOnlyList<Agent> MovingAgents
		{
			get
			{
				return this._movingAgents;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
		public override Agent MovingAgent
		{
			get
			{
				if (this._movingAgents.Count <= 0)
				{
					return null;
				}
				return this._movingAgents[0];
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C816 File Offset: 0x0000AA16
		public PassageUsePoint()
		{
			base.IsInstantUse = true;
			this._movingAgents = new MBList<Agent>();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000C83B File Offset: 0x0000AA3B
		public Location ToLocation
		{
			get
			{
				if (!this._initialized)
				{
					this.InitializeLocation();
				}
				return this._toLocation;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000C851 File Offset: 0x0000AA51
		public override bool HasAIMovingTo
		{
			get
			{
				return this._movingAgents.Count > 0;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000C861 File Offset: 0x0000AA61
		public override FocusableObjectType FocusableObjectType
		{
			get
			{
				return FocusableObjectType.Door;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C864 File Offset: 0x0000AA64
		public override bool IsDisabledForAgent(Agent agent)
		{
			return agent.MountAgent != null || base.IsDeactivated || this.ToLocation == null || (agent.IsAIControlled && !this.ToLocation.CanAIEnter(CampaignMission.Current.Location.GetLocationCharacter(agent.Origin)));
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public override void AfterMissionStart()
		{
			this.DescriptionMessage = GameTexts.FindText("str_ui_door", null);
			this.ActionMessage = GameTexts.FindText("str_ui_default_door", null);
			if (this.ToLocation != null)
			{
				this.ActionMessage = GameTexts.FindText("str_key_action", null);
				this.ActionMessage.SetTextVariable("KEY", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("CombatHotKeyCategory", 13)));
				this.ActionMessage.SetTextVariable("ACTION", (this.ToLocation == null) ? GameTexts.FindText("str_ui_default_door", null) : this.ToLocation.DoorName);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001DE RID: 478 RVA: 0x0000C953 File Offset: 0x0000AB53
		public override bool DisableCombatActionsOnUse
		{
			get
			{
				return !base.IsInstantUse;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C95E File Offset: 0x0000AB5E
		protected override void OnInit()
		{
			base.OnInit();
			this.LockUserPositions = true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C970 File Offset: 0x0000AB70
		public override void OnUse(Agent userAgent)
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign || userAgent.IsAIControlled)
			{
				base.OnUse(userAgent);
				bool flag = false;
				if (this.ToLocation != null)
				{
					if (base.UserAgent.IsMainAgent)
					{
						if (!this.ToLocation.CanPlayerEnter())
						{
							InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=ILnr9eCQ}Door is locked!", null).ToString()));
						}
						else
						{
							flag = true;
							Campaign.Current.GameMenuManager.NextLocation = this.ToLocation;
							Campaign.Current.GameMenuManager.PreviousLocation = CampaignMission.Current.Location;
							Mission.Current.EndMission();
						}
					}
					else if (base.UserAgent.IsAIControlled)
					{
						LocationCharacter locationCharacter = CampaignMission.Current.Location.GetLocationCharacter(base.UserAgent.Origin);
						if (!this.ToLocation.CanAIEnter(locationCharacter))
						{
							MBDebug.ShowWarning("AI should not try to use passage ");
						}
						else
						{
							flag = true;
							LocationComplex.Current.ChangeLocation(locationCharacter, CampaignMission.Current.Location, this.ToLocation);
							base.UserAgent.FadeOut(false, true);
						}
					}
					if (flag)
					{
						Mission.Current.MakeSound(MiscSoundContainer.SoundCodeMovementFoleyDoorOpen, base.GameEntity.GetGlobalFrame().origin, true, false, -1, -1);
					}
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public override void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex)
		{
			base.OnUseStopped(userAgent, isSuccessful, preferenceIndex);
			if (this.LockUserFrames || this.LockUserPositions)
			{
				userAgent.ClearTargetFrame();
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public override bool IsUsableByAgent(Agent userAgent)
		{
			bool result = true;
			if (userAgent.IsAIControlled && (this.InteractionEntity.GetGlobalFrame().origin.AsVec2 - userAgent.Position.AsVec2).LengthSquared > 0.25f)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000CB28 File Offset: 0x0000AD28
		private void InitializeLocation()
		{
			if (string.IsNullOrEmpty(this.ToLocationId))
			{
				this._toLocation = null;
				this._initialized = true;
				return;
			}
			if (Mission.Current != null && Campaign.Current != null)
			{
				if (PlayerEncounter.LocationEncounter != null && CampaignMission.Current.Location != null)
				{
					this._toLocation = CampaignMission.Current.Location.GetPassageToLocation(this.ToLocationId);
				}
				this._initialized = true;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public override int GetMovingAgentCount()
		{
			return this._movingAgents.Count;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000CBA1 File Offset: 0x0000ADA1
		public override Agent GetMovingAgentWithIndex(int index)
		{
			return this._movingAgents[index];
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		public override void AddMovingAgent(Agent movingAgent)
		{
			this._movingAgents.Add(movingAgent);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		public override void RemoveMovingAgent(Agent movingAgent)
		{
			this._movingAgents.Remove(movingAgent);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		public override bool IsAIMovingTo(Agent agent)
		{
			return this._movingAgents.Contains(agent);
		}

		// Token: 0x040000AF RID: 175
		public string ToLocationId = "";

		// Token: 0x040000B0 RID: 176
		private bool _initialized;

		// Token: 0x040000B1 RID: 177
		private readonly MBList<Agent> _movingAgents;

		// Token: 0x040000B2 RID: 178
		private Location _toLocation;

		// Token: 0x040000B3 RID: 179
		private const float InteractionDistanceForAI = 0.5f;
	}
}
