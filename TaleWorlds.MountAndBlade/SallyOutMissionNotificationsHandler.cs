using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000286 RID: 646
	public class SallyOutMissionNotificationsHandler
	{
		// Token: 0x060021E6 RID: 8678 RVA: 0x0007BB20 File Offset: 0x00079D20
		public SallyOutMissionNotificationsHandler(MissionAgentSpawnLogic spawnLogic, SallyOutMissionController sallyOutController)
		{
			this._spawnLogic = spawnLogic;
			this._sallyOutController = sallyOutController;
			this._spawnLogic.OnReinforcementsSpawned += this.OnReinforcementsSpawned;
			this._spawnLogic.OnInitialTroopsSpawned += this.OnInitialTroopsSpawned;
			this._besiegerSpawnedTroopCount = 0;
			this._notificationTimer = new BasicMissionTimer();
			this._notificationsQueue = new Queue<SallyOutMissionNotificationsHandler.NotificationType>();
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x0007BB94 File Offset: 0x00079D94
		public void OnBesiegedSideFallsbackToKeep()
		{
			if (this._isPlayerBesieged)
			{
				if (Mission.Current.PlayerTeam.FormationsIncludingEmpty.Any((Formation f) => f.IsAIControlled && f.CountOfUnits > 0))
				{
					this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.BesiegedSideTacticalRetreat);
					if (Mission.Current.MainAgent != null && Mission.Current.MainAgent.IsActive())
					{
						this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.BesiegedSidePlayerPullbackRequest);
						return;
					}
				}
			}
			else
			{
				this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.BesiegedSideTacticalRetreat);
			}
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0007BC20 File Offset: 0x00079E20
		public void OnAfterStart()
		{
			this._isPlayerBesieged = (Mission.Current.PlayerTeam.Side == BattleSideEnum.Defender);
			this.SetNotificationTimerEnabled(false, true);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0007BC42 File Offset: 0x00079E42
		public void OnMissionEnd()
		{
			this._spawnLogic.OnReinforcementsSpawned -= this.OnReinforcementsSpawned;
			this._spawnLogic.OnInitialTroopsSpawned -= this.OnInitialTroopsSpawned;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0007BC72 File Offset: 0x00079E72
		public void OnDeploymentFinished()
		{
			this.SetNotificationTimerEnabled(true, true);
			this._besiegerSiegeEngines = this._sallyOutController.BesiegerSiegeEngines;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x0007BC90 File Offset: 0x00079E90
		public void OnMissionTick(float dt)
		{
			if (this._notificationTimerEnabled && this._notificationTimer.ElapsedTime >= 5f)
			{
				this.CheckPeriodicNotifications();
				if (!this._notificationsQueue.IsEmpty<SallyOutMissionNotificationsHandler.NotificationType>())
				{
					SallyOutMissionNotificationsHandler.NotificationType type = this._notificationsQueue.Dequeue();
					this.SendNotification(type);
				}
				this._notificationTimer.Reset();
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0007BCE8 File Offset: 0x00079EE8
		private void SetNotificationTimerEnabled(bool value, bool resetTimer = true)
		{
			this._notificationTimerEnabled = value;
			if (resetTimer)
			{
				this._notificationTimer.Reset();
			}
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0007BD00 File Offset: 0x00079F00
		private void CheckPeriodicNotifications()
		{
			if (!this._objectiveMessageSent)
			{
				this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.SallyOutObjective);
				this._objectiveMessageSent = true;
			}
			if (!this._siegeEnginesDestroyedMessageSent && this.IsSiegeEnginesDestroyed())
			{
				this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.SiegeEnginesDestroyed);
				this._siegeEnginesDestroyedMessageSent = true;
			}
			if (!this._besiegersStrengtheningMessageSent && this._spawnLogic.NumberOfRemainingDefenderTroops == 0 && this._besiegerSpawnedTroopCount >= this._spawnLogic.NumberOfActiveDefenderTroops)
			{
				this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.BesiegerSideStrenghtening);
				this._besiegersStrengtheningMessageSent = true;
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x0007BD88 File Offset: 0x00079F88
		private void SendNotification(SallyOutMissionNotificationsHandler.NotificationType type)
		{
			int num = -1;
			if (this._isPlayerBesieged)
			{
				switch (type)
				{
				case SallyOutMissionNotificationsHandler.NotificationType.SallyOutObjective:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_besieged_objective_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/move");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegerSideStrenghtening:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_enemy_becoming_strong_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/retreat");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegerSideReinforcementsSpawned:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_enemy_reinforcements_arrived", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/reinforcements");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegedSideTacticalRetreat:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_allied_troops_tactical_retreat_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/retreat");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegedSidePlayerPullbackRequest:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_allied_pullback_or_take_command_message", null), 0, null, "");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.SiegeEnginesDestroyed:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_enemy_siege_engines_destroyed_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/move");
					break;
				}
			}
			else
			{
				switch (type)
				{
				case SallyOutMissionNotificationsHandler.NotificationType.SallyOutObjective:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_besieger_objective_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/move");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegerSideReinforcementsSpawned:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_allied_reinforcements_arrived", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/reinforcements");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.BesiegedSideTacticalRetreat:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_enemy_troops_fall_back_to_keep_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/move");
					break;
				case SallyOutMissionNotificationsHandler.NotificationType.SiegeEnginesDestroyed:
					MBInformationManager.AddQuickInformation(GameTexts.FindText("str_sally_out_allied_siege_engines_destroyed_message", null), 0, null, "");
					num = SoundEvent.GetEventIdFromString("event:/alerts/horns/move");
					break;
				}
			}
			if (num >= 0)
			{
				this.PlayNotificationSound(num);
			}
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x0007BF60 File Offset: 0x0007A160
		private void PlayNotificationSound(int soundId)
		{
			MatrixFrame cameraFrame = Mission.Current.GetCameraFrame();
			Vec3 position = cameraFrame.origin + cameraFrame.rotation.u;
			MBSoundEvent.PlaySound(soundId, position);
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x0007BF97 File Offset: 0x0007A197
		private void OnInitialTroopsSpawned(BattleSideEnum battleSide, int numberOfTroopsSpawned)
		{
			if (battleSide == BattleSideEnum.Attacker)
			{
				this._besiegerSpawnedTroopCount += numberOfTroopsSpawned;
			}
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x0007BFAB File Offset: 0x0007A1AB
		private void OnReinforcementsSpawned(BattleSideEnum battleSide, int numberOfTroopsSpawned)
		{
			if (battleSide == BattleSideEnum.Attacker)
			{
				this._besiegerSpawnedTroopCount += numberOfTroopsSpawned;
				this._notificationsQueue.Enqueue(SallyOutMissionNotificationsHandler.NotificationType.BesiegerSideReinforcementsSpawned);
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x0007BFCB File Offset: 0x0007A1CB
		private bool IsSiegeEnginesDestroyed()
		{
			if (this._besiegerSiegeEngines != null)
			{
				return this._besiegerSiegeEngines.All((SiegeWeapon siegeEngine) => siegeEngine.DestructionComponent.IsDestroyed);
			}
			return false;
		}

		// Token: 0x04000CAC RID: 3244
		private const float NotificationCheckInterval = 5f;

		// Token: 0x04000CAD RID: 3245
		private MissionAgentSpawnLogic _spawnLogic;

		// Token: 0x04000CAE RID: 3246
		private SallyOutMissionController _sallyOutController;

		// Token: 0x04000CAF RID: 3247
		private bool _isPlayerBesieged;

		// Token: 0x04000CB0 RID: 3248
		private MBReadOnlyList<SiegeWeapon> _besiegerSiegeEngines;

		// Token: 0x04000CB1 RID: 3249
		private Queue<SallyOutMissionNotificationsHandler.NotificationType> _notificationsQueue;

		// Token: 0x04000CB2 RID: 3250
		private BasicMissionTimer _notificationTimer;

		// Token: 0x04000CB3 RID: 3251
		private bool _notificationTimerEnabled = true;

		// Token: 0x04000CB4 RID: 3252
		private bool _objectiveMessageSent;

		// Token: 0x04000CB5 RID: 3253
		private bool _siegeEnginesDestroyedMessageSent;

		// Token: 0x04000CB6 RID: 3254
		private bool _besiegersStrengtheningMessageSent;

		// Token: 0x04000CB7 RID: 3255
		private int _besiegerSpawnedTroopCount;

		// Token: 0x02000539 RID: 1337
		private enum NotificationType
		{
			// Token: 0x04001C98 RID: 7320
			SallyOutObjective,
			// Token: 0x04001C99 RID: 7321
			BesiegerSideStrenghtening,
			// Token: 0x04001C9A RID: 7322
			BesiegerSideReinforcementsSpawned,
			// Token: 0x04001C9B RID: 7323
			BesiegedSideTacticalRetreat,
			// Token: 0x04001C9C RID: 7324
			BesiegedSidePlayerPullbackRequest,
			// Token: 0x04001C9D RID: 7325
			SiegeEnginesDestroyed
		}
	}
}
