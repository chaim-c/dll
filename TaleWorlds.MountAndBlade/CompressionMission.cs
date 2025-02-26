﻿using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F1 RID: 753
	public static class CompressionMission
	{
		// Token: 0x04000F74 RID: 3956
		public static CompressionInfo.Float DebugScaleValueCompressionInfo = new CompressionInfo.Float(0.5f, 1.5f, 13);

		// Token: 0x04000F75 RID: 3957
		public static CompressionInfo.Integer AgentCompressionInfo = new CompressionInfo.Integer(-1, 11);

		// Token: 0x04000F76 RID: 3958
		public static CompressionInfo.Integer WeaponAttachmentIndexCompressionInfo = new CompressionInfo.Integer(0, 8);

		// Token: 0x04000F77 RID: 3959
		public static CompressionInfo.Integer AgentOffsetCompressionInfo = new CompressionInfo.Integer(0, 8);

		// Token: 0x04000F78 RID: 3960
		public static CompressionInfo.Integer AgentHealthCompressionInfo = new CompressionInfo.Integer(-1, 11);

		// Token: 0x04000F79 RID: 3961
		public static CompressionInfo.Integer AgentControllerCompressionInfo = new CompressionInfo.Integer(0, 2, true);

		// Token: 0x04000F7A RID: 3962
		public static CompressionInfo.Integer TeamCompressionInfo = new CompressionInfo.Integer(-1, 10);

		// Token: 0x04000F7B RID: 3963
		public static CompressionInfo.Integer TeamSideCompressionInfo = new CompressionInfo.Integer(-1, 4);

		// Token: 0x04000F7C RID: 3964
		public static CompressionInfo.Integer RoundEndReasonCompressionInfo = new CompressionInfo.Integer(-1, 2, true);

		// Token: 0x04000F7D RID: 3965
		public static CompressionInfo.Integer TeamScoreCompressionInfo = new CompressionInfo.Integer(-1023000, 1023000, true);

		// Token: 0x04000F7E RID: 3966
		public static CompressionInfo.Integer FactionCompressionInfo = new CompressionInfo.Integer(0, 4);

		// Token: 0x04000F7F RID: 3967
		public static CompressionInfo.Integer MissionOrderTypeCompressionInfo = new CompressionInfo.Integer(-1, 5);

		// Token: 0x04000F80 RID: 3968
		public static CompressionInfo.Integer MissionRoundCountCompressionInfo = new CompressionInfo.Integer(-1, 7);

		// Token: 0x04000F81 RID: 3969
		public static CompressionInfo.Integer MissionRoundStateCompressionInfo = new CompressionInfo.Integer(-1, 5, true);

		// Token: 0x04000F82 RID: 3970
		public static CompressionInfo.Integer RoundTimeCompressionInfo = new CompressionInfo.Integer(0, MultiplayerOptions.OptionType.RoundTimeLimit.GetMaximumValue(), true);

		// Token: 0x04000F83 RID: 3971
		public static CompressionInfo.Integer SelectedTroopIndexCompressionInfo = new CompressionInfo.Integer(-1, 15, true);

		// Token: 0x04000F84 RID: 3972
		public static CompressionInfo.Integer MissileCompressionInfo = new CompressionInfo.Integer(0, 10);

		// Token: 0x04000F85 RID: 3973
		public static CompressionInfo.Float MissileSpeedCompressionInfo = new CompressionInfo.Float(0f, 12, 0.05f);

		// Token: 0x04000F86 RID: 3974
		public static CompressionInfo.Integer MissileCollisionReactionCompressionInfo = new CompressionInfo.Integer(0, 3, true);

		// Token: 0x04000F87 RID: 3975
		public static CompressionInfo.Integer FlagCapturePointIndexCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x04000F88 RID: 3976
		public static CompressionInfo.Integer FlagpoleIndexCompressionInfo = new CompressionInfo.Integer(0, 5, true);

		// Token: 0x04000F89 RID: 3977
		public static CompressionInfo.Float FlagCapturePointDurationCompressionInfo = new CompressionInfo.Float(-1f, 14, 0.01f);

		// Token: 0x04000F8A RID: 3978
		public static CompressionInfo.Float FlagProgressCompressionInfo = new CompressionInfo.Float(-1f, 1f, 12);

		// Token: 0x04000F8B RID: 3979
		public static CompressionInfo.Float FlagClassicProgressCompressionInfo = new CompressionInfo.Float(0f, 1f, 11);

		// Token: 0x04000F8C RID: 3980
		public static CompressionInfo.Integer FlagDirectionEnumCompressionInfo = new CompressionInfo.Integer(-1, 2, true);

		// Token: 0x04000F8D RID: 3981
		public static CompressionInfo.Float FlagSpeedCompressionInfo = new CompressionInfo.Float(-1f, 14, 0.01f);

		// Token: 0x04000F8E RID: 3982
		public static CompressionInfo.Integer FlagCaptureResultCompressionInfo = new CompressionInfo.Integer(0, 3, true);

		// Token: 0x04000F8F RID: 3983
		public static CompressionInfo.Integer UsableGameObjectDestructionStateCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x04000F90 RID: 3984
		public static CompressionInfo.Float UsableGameObjectHealthCompressionInfo = new CompressionInfo.Float(-1f, 18, 0.1f);

		// Token: 0x04000F91 RID: 3985
		public static CompressionInfo.Float UsableGameObjectBlowMagnitude = new CompressionInfo.Float(0f, DestructableComponent.MaxBlowMagnitude, 8);

		// Token: 0x04000F92 RID: 3986
		public static CompressionInfo.Float UsableGameObjectBlowDirection = new CompressionInfo.Float(-1f, 1f, 7);

		// Token: 0x04000F93 RID: 3987
		public static CompressionInfo.Float CapturePointProgressCompressionInfo = new CompressionInfo.Float(0f, 1f, 10);

		// Token: 0x04000F94 RID: 3988
		public static CompressionInfo.Integer ItemSlotCompressionInfo = new CompressionInfo.Integer(0, 4, true);

		// Token: 0x04000F95 RID: 3989
		public static CompressionInfo.Integer WieldSlotCompressionInfo = new CompressionInfo.Integer(-1, 4, true);

		// Token: 0x04000F96 RID: 3990
		public static CompressionInfo.Integer ItemDataCompressionInfo = new CompressionInfo.Integer(0, 10);

		// Token: 0x04000F97 RID: 3991
		public static CompressionInfo.Integer WeaponReloadPhaseCompressionInfo = new CompressionInfo.Integer(0, 9, true);

		// Token: 0x04000F98 RID: 3992
		public static CompressionInfo.Integer WeaponUsageIndexCompressionInfo = new CompressionInfo.Integer(0, 2);

		// Token: 0x04000F99 RID: 3993
		public static CompressionInfo.Integer TauntIndexCompressionInfo = new CompressionInfo.Integer(0, TauntUsageManager.GetTauntItemCount() - 1, true);

		// Token: 0x04000F9A RID: 3994
		public static CompressionInfo.Integer BarkIndexCompressionInfo = new CompressionInfo.Integer(0, SkinVoiceManager.VoiceType.MpBarks.Length - 1, true);

		// Token: 0x04000F9B RID: 3995
		public static CompressionInfo.Integer UsageDirectionCompressionInfo = new CompressionInfo.Integer(-1, 9, true);

		// Token: 0x04000F9C RID: 3996
		public static CompressionInfo.Float SpawnedItemVelocityCompressionInfo = new CompressionInfo.Float(-100f, 100f, 12);

		// Token: 0x04000F9D RID: 3997
		public static CompressionInfo.Float SpawnedItemAngularVelocityCompressionInfo = new CompressionInfo.Float(-100f, 100f, 12);

		// Token: 0x04000F9E RID: 3998
		public static CompressionInfo.UnsignedInteger SpawnedItemWeaponSpawnFlagCompressionInfo = new CompressionInfo.UnsignedInteger(0U, EnumHelper.GetCombinedUIntEnumFlagsValue(typeof(Mission.WeaponSpawnFlags)), true);

		// Token: 0x04000F9F RID: 3999
		public static CompressionInfo.Integer RangedSiegeWeaponAmmoCompressionInfo = new CompressionInfo.Integer(0, 7);

		// Token: 0x04000FA0 RID: 4000
		public static CompressionInfo.Integer RangedSiegeWeaponAmmoIndexCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x04000FA1 RID: 4001
		public static CompressionInfo.Integer RangedSiegeWeaponStateCompressionInfo = new CompressionInfo.Integer(0, 8, true);

		// Token: 0x04000FA2 RID: 4002
		public static CompressionInfo.Integer SiegeLadderStateCompressionInfo = new CompressionInfo.Integer(0, 9, true);

		// Token: 0x04000FA3 RID: 4003
		public static CompressionInfo.Integer BatteringRamStateCompressionInfo = new CompressionInfo.Integer(0, 2, true);

		// Token: 0x04000FA4 RID: 4004
		public static CompressionInfo.Integer SiegeLadderAnimationStateCompressionInfo = new CompressionInfo.Integer(0, 2, true);

		// Token: 0x04000FA5 RID: 4005
		public static CompressionInfo.Float SiegeMachineComponentAngularSpeedCompressionInfo = new CompressionInfo.Float(-20f, 20f, 12);

		// Token: 0x04000FA6 RID: 4006
		public static CompressionInfo.Integer SiegeTowerGateStateCompressionInfo = new CompressionInfo.Integer(0, 3, true);

		// Token: 0x04000FA7 RID: 4007
		public static CompressionInfo.Integer NumberOfPacesCompressionInfo = new CompressionInfo.Integer(0, 3);

		// Token: 0x04000FA8 RID: 4008
		public static CompressionInfo.Float WalkingSpeedLimitCompressionInfo = new CompressionInfo.Float(-0.01f, 9, 0.01f);

		// Token: 0x04000FA9 RID: 4009
		public static CompressionInfo.Float StepSizeCompressionInfo = new CompressionInfo.Float(-0.01f, 7, 0.01f);

		// Token: 0x04000FAA RID: 4010
		public static CompressionInfo.Integer BoneIndexCompressionInfo = new CompressionInfo.Integer(0, 63, true);

		// Token: 0x04000FAB RID: 4011
		public static CompressionInfo.Integer AgentPrefabComponentIndexCompressionInfo = new CompressionInfo.Integer(0, 4);

		// Token: 0x04000FAC RID: 4012
		public static CompressionInfo.Integer MultiplayerPollRejectReasonCompressionInfo = new CompressionInfo.Integer(0, 3, true);

		// Token: 0x04000FAD RID: 4013
		public static CompressionInfo.Integer MultiplayerNotificationCompressionInfo = new CompressionInfo.Integer(0, MultiplayerGameNotificationsComponent.NotificationCount, true);

		// Token: 0x04000FAE RID: 4014
		public static CompressionInfo.Integer MultiplayerNotificationParameterCompressionInfo = new CompressionInfo.Integer(-1, 8);

		// Token: 0x04000FAF RID: 4015
		public static CompressionInfo.Integer PerkListIndexCompressionInfo = new CompressionInfo.Integer(0, 2);

		// Token: 0x04000FB0 RID: 4016
		public static CompressionInfo.Integer PerkIndexCompressionInfo = new CompressionInfo.Integer(0, 4);

		// Token: 0x04000FB1 RID: 4017
		public static CompressionInfo.Float FlagDominationMoraleCompressionInfo = new CompressionInfo.Float(-1f, 8, 0.01f);

		// Token: 0x04000FB2 RID: 4018
		public static CompressionInfo.Integer TdmGoldChangeCompressionInfo = new CompressionInfo.Integer(0, 2000, true);

		// Token: 0x04000FB3 RID: 4019
		public static CompressionInfo.Integer TdmGoldGainTypeCompressionInfo = new CompressionInfo.Integer(0, 12);

		// Token: 0x04000FB4 RID: 4020
		public static CompressionInfo.Integer DuelAreaIndexCompressionInfo = new CompressionInfo.Integer(0, 4);

		// Token: 0x04000FB5 RID: 4021
		public static CompressionInfo.Integer AutomatedBattleIndexCompressionInfo = new CompressionInfo.Integer(0, 10, true);

		// Token: 0x04000FB6 RID: 4022
		public static CompressionInfo.Integer SiegeMoraleCompressionInfo = new CompressionInfo.Integer(0, 1440, true);

		// Token: 0x04000FB7 RID: 4023
		public static CompressionInfo.Integer SiegeMoralePerFlagCompressionInfo = new CompressionInfo.Integer(0, 90, true);

		// Token: 0x04000FB8 RID: 4024
		public static CompressionInfo.Integer ActionSetCompressionInfo;

		// Token: 0x04000FB9 RID: 4025
		public static CompressionInfo.Integer MonsterUsageSetCompressionInfo;

		// Token: 0x04000FBA RID: 4026
		public static CompressionInfo.Integer OrderTypeCompressionInfo = new CompressionInfo.Integer(0, 42, true);

		// Token: 0x04000FBB RID: 4027
		public static CompressionInfo.Integer FormationClassCompressionInfo = new CompressionInfo.Integer(-1, 10, true);

		// Token: 0x04000FBC RID: 4028
		public static CompressionInfo.Float OrderPositionCompressionInfo = new CompressionInfo.Float(-100000f, 100000f, 24);

		// Token: 0x04000FBD RID: 4029
		public static CompressionInfo.Integer SynchedMissionObjectReadableRecordTypeIndex = new CompressionInfo.Integer(-1, 8);
	}
}
