using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EA RID: 490
	public static class CombatSoundContainer
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0005E748 File Offset: 0x0005C948
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x0005E74F File Offset: 0x0005C94F
		public static int SoundCodeMissionCombatBluntHigh { get; private set; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0005E757 File Offset: 0x0005C957
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0005E75E File Offset: 0x0005C95E
		public static int SoundCodeMissionCombatBluntLow { get; private set; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0005E766 File Offset: 0x0005C966
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0005E76D File Offset: 0x0005C96D
		public static int SoundCodeMissionCombatBluntMed { get; private set; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0005E775 File Offset: 0x0005C975
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0005E77C File Offset: 0x0005C97C
		public static int SoundCodeMissionCombatBoulderHigh { get; private set; }

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001B32 RID: 6962 RVA: 0x0005E784 File Offset: 0x0005C984
		// (set) Token: 0x06001B33 RID: 6963 RVA: 0x0005E78B File Offset: 0x0005C98B
		public static int SoundCodeMissionCombatBoulderLow { get; private set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x0005E793 File Offset: 0x0005C993
		// (set) Token: 0x06001B35 RID: 6965 RVA: 0x0005E79A File Offset: 0x0005C99A
		public static int SoundCodeMissionCombatBoulderMed { get; private set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0005E7A2 File Offset: 0x0005C9A2
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0005E7A9 File Offset: 0x0005C9A9
		public static int SoundCodeMissionCombatCutHigh { get; private set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0005E7B1 File Offset: 0x0005C9B1
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0005E7B8 File Offset: 0x0005C9B8
		public static int SoundCodeMissionCombatCutLow { get; private set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0005E7C0 File Offset: 0x0005C9C0
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0005E7C7 File Offset: 0x0005C9C7
		public static int SoundCodeMissionCombatCutMed { get; private set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0005E7CF File Offset: 0x0005C9CF
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0005E7D6 File Offset: 0x0005C9D6
		public static int SoundCodeMissionCombatMissileHigh { get; private set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0005E7DE File Offset: 0x0005C9DE
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0005E7E5 File Offset: 0x0005C9E5
		public static int SoundCodeMissionCombatMissileLow { get; private set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0005E7ED File Offset: 0x0005C9ED
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0005E7F4 File Offset: 0x0005C9F4
		public static int SoundCodeMissionCombatMissileMed { get; private set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0005E7FC File Offset: 0x0005C9FC
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x0005E803 File Offset: 0x0005CA03
		public static int SoundCodeMissionCombatPierceHigh { get; private set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001B44 RID: 6980 RVA: 0x0005E80B File Offset: 0x0005CA0B
		// (set) Token: 0x06001B45 RID: 6981 RVA: 0x0005E812 File Offset: 0x0005CA12
		public static int SoundCodeMissionCombatPierceLow { get; private set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x0005E81A File Offset: 0x0005CA1A
		// (set) Token: 0x06001B47 RID: 6983 RVA: 0x0005E821 File Offset: 0x0005CA21
		public static int SoundCodeMissionCombatPierceMed { get; private set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x0005E829 File Offset: 0x0005CA29
		// (set) Token: 0x06001B49 RID: 6985 RVA: 0x0005E830 File Offset: 0x0005CA30
		public static int SoundCodeMissionCombatPunchHigh { get; private set; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001B4A RID: 6986 RVA: 0x0005E838 File Offset: 0x0005CA38
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x0005E83F File Offset: 0x0005CA3F
		public static int SoundCodeMissionCombatPunchLow { get; private set; }

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001B4C RID: 6988 RVA: 0x0005E847 File Offset: 0x0005CA47
		// (set) Token: 0x06001B4D RID: 6989 RVA: 0x0005E84E File Offset: 0x0005CA4E
		public static int SoundCodeMissionCombatPunchMed { get; private set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001B4E RID: 6990 RVA: 0x0005E856 File Offset: 0x0005CA56
		// (set) Token: 0x06001B4F RID: 6991 RVA: 0x0005E85D File Offset: 0x0005CA5D
		public static int SoundCodeMissionCombatThrowingAxeHigh { get; private set; }

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001B50 RID: 6992 RVA: 0x0005E865 File Offset: 0x0005CA65
		// (set) Token: 0x06001B51 RID: 6993 RVA: 0x0005E86C File Offset: 0x0005CA6C
		public static int SoundCodeMissionCombatThrowingAxeLow { get; private set; }

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001B52 RID: 6994 RVA: 0x0005E874 File Offset: 0x0005CA74
		// (set) Token: 0x06001B53 RID: 6995 RVA: 0x0005E87B File Offset: 0x0005CA7B
		public static int SoundCodeMissionCombatThrowingAxeMed { get; private set; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0005E883 File Offset: 0x0005CA83
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x0005E88A File Offset: 0x0005CA8A
		public static int SoundCodeMissionCombatThrowingDaggerHigh { get; private set; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x0005E892 File Offset: 0x0005CA92
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x0005E899 File Offset: 0x0005CA99
		public static int SoundCodeMissionCombatThrowingDaggerLow { get; private set; }

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0005E8A1 File Offset: 0x0005CAA1
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x0005E8A8 File Offset: 0x0005CAA8
		public static int SoundCodeMissionCombatThrowingDaggerMed { get; private set; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x0005E8B0 File Offset: 0x0005CAB0
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x0005E8B7 File Offset: 0x0005CAB7
		public static int SoundCodeMissionCombatThrowingStoneHigh { get; private set; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x0005E8BF File Offset: 0x0005CABF
		// (set) Token: 0x06001B5D RID: 7005 RVA: 0x0005E8C6 File Offset: 0x0005CAC6
		public static int SoundCodeMissionCombatThrowingStoneLow { get; private set; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x0005E8CE File Offset: 0x0005CACE
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x0005E8D5 File Offset: 0x0005CAD5
		public static int SoundCodeMissionCombatThrowingStoneMed { get; private set; }

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x0005E8DD File Offset: 0x0005CADD
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x0005E8E4 File Offset: 0x0005CAE4
		public static int SoundCodeMissionCombatChargeDamage { get; private set; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0005E8EC File Offset: 0x0005CAEC
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0005E8F3 File Offset: 0x0005CAF3
		public static int SoundCodeMissionCombatKick { get; private set; }

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0005E8FB File Offset: 0x0005CAFB
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0005E902 File Offset: 0x0005CB02
		public static int SoundCodeMissionCombatPlayerhit { get; private set; }

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x0005E90A File Offset: 0x0005CB0A
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0005E911 File Offset: 0x0005CB11
		public static int SoundCodeMissionCombatWoodShieldBash { get; private set; }

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x0005E919 File Offset: 0x0005CB19
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x0005E920 File Offset: 0x0005CB20
		public static int SoundCodeMissionCombatMetalShieldBash { get; private set; }

		// Token: 0x06001B6A RID: 7018 RVA: 0x0005E928 File Offset: 0x0005CB28
		static CombatSoundContainer()
		{
			CombatSoundContainer.UpdateMissionCombatSoundCodes();
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x0005E930 File Offset: 0x0005CB30
		private static void UpdateMissionCombatSoundCodes()
		{
			CombatSoundContainer.SoundCodeMissionCombatBluntHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/high");
			CombatSoundContainer.SoundCodeMissionCombatBluntLow = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/low");
			CombatSoundContainer.SoundCodeMissionCombatBluntMed = SoundEvent.GetEventIdFromString("event:/mission/combat/blunt/med");
			CombatSoundContainer.SoundCodeMissionCombatBoulderHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/high");
			CombatSoundContainer.SoundCodeMissionCombatBoulderLow = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/low");
			CombatSoundContainer.SoundCodeMissionCombatBoulderMed = SoundEvent.GetEventIdFromString("event:/mission/combat/boulder/med");
			CombatSoundContainer.SoundCodeMissionCombatCutHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/high");
			CombatSoundContainer.SoundCodeMissionCombatCutLow = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/low");
			CombatSoundContainer.SoundCodeMissionCombatCutMed = SoundEvent.GetEventIdFromString("event:/mission/combat/cut/med");
			CombatSoundContainer.SoundCodeMissionCombatMissileHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/high");
			CombatSoundContainer.SoundCodeMissionCombatMissileLow = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/low");
			CombatSoundContainer.SoundCodeMissionCombatMissileMed = SoundEvent.GetEventIdFromString("event:/mission/combat/missile/med");
			CombatSoundContainer.SoundCodeMissionCombatPierceHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/high");
			CombatSoundContainer.SoundCodeMissionCombatPierceLow = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/low");
			CombatSoundContainer.SoundCodeMissionCombatPierceMed = SoundEvent.GetEventIdFromString("event:/mission/combat/pierce/med");
			CombatSoundContainer.SoundCodeMissionCombatPunchHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/high");
			CombatSoundContainer.SoundCodeMissionCombatPunchLow = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/low");
			CombatSoundContainer.SoundCodeMissionCombatPunchMed = SoundEvent.GetEventIdFromString("event:/mission/combat/punch/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingAxeMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwing/med");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneHigh = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/high");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneLow = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/low");
			CombatSoundContainer.SoundCodeMissionCombatThrowingStoneMed = SoundEvent.GetEventIdFromString("event:/mission/combat/throwingstone/med");
			CombatSoundContainer.SoundCodeMissionCombatChargeDamage = SoundEvent.GetEventIdFromString("event:/mission/combat/charge/damage");
			CombatSoundContainer.SoundCodeMissionCombatKick = SoundEvent.GetEventIdFromString("event:/mission/combat/kick");
			CombatSoundContainer.SoundCodeMissionCombatPlayerhit = SoundEvent.GetEventIdFromString("event:/mission/combat/playerHit");
			CombatSoundContainer.SoundCodeMissionCombatWoodShieldBash = SoundEvent.GetEventIdFromString("event:/mission/combat/shield/bash");
			CombatSoundContainer.SoundCodeMissionCombatMetalShieldBash = SoundEvent.GetEventIdFromString("event:/mission/combat/shield/metal_bash");
		}
	}
}
