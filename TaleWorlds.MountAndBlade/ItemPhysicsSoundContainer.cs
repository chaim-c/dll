using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EB RID: 491
	public static class ItemPhysicsSoundContainer
	{
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x0005EB1D File Offset: 0x0005CD1D
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0005EB24 File Offset: 0x0005CD24
		public static int SoundCodePhysicsBoulderDefault { get; private set; }

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x0005EB2C File Offset: 0x0005CD2C
		// (set) Token: 0x06001B6F RID: 7023 RVA: 0x0005EB33 File Offset: 0x0005CD33
		public static int SoundCodePhysicsArrowlikeDefault { get; private set; }

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0005EB3B File Offset: 0x0005CD3B
		// (set) Token: 0x06001B71 RID: 7025 RVA: 0x0005EB42 File Offset: 0x0005CD42
		public static int SoundCodePhysicsBowlikeDefault { get; private set; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0005EB4A File Offset: 0x0005CD4A
		// (set) Token: 0x06001B73 RID: 7027 RVA: 0x0005EB51 File Offset: 0x0005CD51
		public static int SoundCodePhysicsDaggerlikeDefault { get; private set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0005EB59 File Offset: 0x0005CD59
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x0005EB60 File Offset: 0x0005CD60
		public static int SoundCodePhysicsGreatswordlikeDefault { get; private set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x0005EB68 File Offset: 0x0005CD68
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x0005EB6F File Offset: 0x0005CD6F
		public static int SoundCodePhysicsShieldlikeDefault { get; private set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x0005EB77 File Offset: 0x0005CD77
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x0005EB7E File Offset: 0x0005CD7E
		public static int SoundCodePhysicsSpearlikeDefault { get; private set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x0005EB86 File Offset: 0x0005CD86
		// (set) Token: 0x06001B7B RID: 7035 RVA: 0x0005EB8D File Offset: 0x0005CD8D
		public static int SoundCodePhysicsSwordlikeDefault { get; private set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x0005EB95 File Offset: 0x0005CD95
		// (set) Token: 0x06001B7D RID: 7037 RVA: 0x0005EB9C File Offset: 0x0005CD9C
		public static int SoundCodePhysicsBoulderWood { get; private set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0005EBA4 File Offset: 0x0005CDA4
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0005EBAB File Offset: 0x0005CDAB
		public static int SoundCodePhysicsArrowlikeWood { get; private set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x0005EBB3 File Offset: 0x0005CDB3
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x0005EBBA File Offset: 0x0005CDBA
		public static int SoundCodePhysicsBowlikeWood { get; private set; }

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0005EBC2 File Offset: 0x0005CDC2
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x0005EBC9 File Offset: 0x0005CDC9
		public static int SoundCodePhysicsDaggerlikeWood { get; private set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0005EBD1 File Offset: 0x0005CDD1
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		public static int SoundCodePhysicsGreatswordlikeWood { get; private set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0005EBE0 File Offset: 0x0005CDE0
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0005EBE7 File Offset: 0x0005CDE7
		public static int SoundCodePhysicsShieldlikeWood { get; private set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0005EBEF File Offset: 0x0005CDEF
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x0005EBF6 File Offset: 0x0005CDF6
		public static int SoundCodePhysicsSpearlikeWood { get; private set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x0005EBFE File Offset: 0x0005CDFE
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x0005EC05 File Offset: 0x0005CE05
		public static int SoundCodePhysicsSwordlikeWood { get; private set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x0005EC0D File Offset: 0x0005CE0D
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x0005EC14 File Offset: 0x0005CE14
		public static int SoundCodePhysicsBoulderStone { get; private set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001B8E RID: 7054 RVA: 0x0005EC1C File Offset: 0x0005CE1C
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x0005EC23 File Offset: 0x0005CE23
		public static int SoundCodePhysicsArrowlikeStone { get; private set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0005EC2B File Offset: 0x0005CE2B
		// (set) Token: 0x06001B91 RID: 7057 RVA: 0x0005EC32 File Offset: 0x0005CE32
		public static int SoundCodePhysicsBowlikeStone { get; private set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001B92 RID: 7058 RVA: 0x0005EC3A File Offset: 0x0005CE3A
		// (set) Token: 0x06001B93 RID: 7059 RVA: 0x0005EC41 File Offset: 0x0005CE41
		public static int SoundCodePhysicsDaggerlikeStone { get; private set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x0005EC49 File Offset: 0x0005CE49
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x0005EC50 File Offset: 0x0005CE50
		public static int SoundCodePhysicsGreatswordlikeStone { get; private set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B96 RID: 7062 RVA: 0x0005EC58 File Offset: 0x0005CE58
		// (set) Token: 0x06001B97 RID: 7063 RVA: 0x0005EC5F File Offset: 0x0005CE5F
		public static int SoundCodePhysicsShieldlikeStone { get; private set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x0005EC67 File Offset: 0x0005CE67
		// (set) Token: 0x06001B99 RID: 7065 RVA: 0x0005EC6E File Offset: 0x0005CE6E
		public static int SoundCodePhysicsSpearlikeStone { get; private set; }

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06001B9A RID: 7066 RVA: 0x0005EC76 File Offset: 0x0005CE76
		// (set) Token: 0x06001B9B RID: 7067 RVA: 0x0005EC7D File Offset: 0x0005CE7D
		public static int SoundCodePhysicsSwordlikeStone { get; private set; }

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0005EC85 File Offset: 0x0005CE85
		// (set) Token: 0x06001B9D RID: 7069 RVA: 0x0005EC8C File Offset: 0x0005CE8C
		public static int SoundCodePhysicsWater { get; private set; }

		// Token: 0x06001B9E RID: 7070 RVA: 0x0005EC94 File Offset: 0x0005CE94
		static ItemPhysicsSoundContainer()
		{
			ItemPhysicsSoundContainer.UpdateItemPhysicsSoundCodes();
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0005EC9C File Offset: 0x0005CE9C
		private static void UpdateItemPhysicsSoundCodes()
		{
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderDefault = SoundEvent.GetEventIdFromString("event:/physics/boulder/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/bowlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/spearlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeDefault = SoundEvent.GetEventIdFromString("event:/physics/swordlike/default");
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderWood = SoundEvent.GetEventIdFromString("event:/physics/boulder/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeWood = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeWood = SoundEvent.GetEventIdFromString("event:/physics/bowlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeWood = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeWood = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeWood = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeWood = SoundEvent.GetEventIdFromString("event:/physics/spearlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeWood = SoundEvent.GetEventIdFromString("event:/physics/swordlike/wood");
			ItemPhysicsSoundContainer.SoundCodePhysicsBoulderStone = SoundEvent.GetEventIdFromString("event:/physics/boulder/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsArrowlikeStone = SoundEvent.GetEventIdFromString("event:/physics/arrowlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsBowlikeStone = SoundEvent.GetEventIdFromString("event:/physics/bowlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsDaggerlikeStone = SoundEvent.GetEventIdFromString("event:/physics/daggerlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsGreatswordlikeStone = SoundEvent.GetEventIdFromString("event:/physics/greatswordlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsShieldlikeStone = SoundEvent.GetEventIdFromString("event:/physics/shieldlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsSpearlikeStone = SoundEvent.GetEventIdFromString("event:/physics/spearlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsSwordlikeStone = SoundEvent.GetEventIdFromString("event:/physics/swordlike/stone");
			ItemPhysicsSoundContainer.SoundCodePhysicsWater = SoundEvent.GetEventIdFromString("event:/physics/water");
		}
	}
}
