using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001EC RID: 492
	public static class MiscSoundContainer
	{
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0005EE20 File Offset: 0x0005D020
		// (set) Token: 0x06001BA1 RID: 7073 RVA: 0x0005EE27 File Offset: 0x0005D027
		public static int SoundCodeMovementFoleyDoorOpen { get; private set; }

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x0005EE2F File Offset: 0x0005D02F
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x0005EE36 File Offset: 0x0005D036
		public static int SoundCodeMovementFoleyDoorClose { get; private set; }

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x0005EE3E File Offset: 0x0005D03E
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x0005EE45 File Offset: 0x0005D045
		public static int SoundCodeAmbientNodeSiegeBallistaFire { get; private set; }

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0005EE4D File Offset: 0x0005D04D
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x0005EE54 File Offset: 0x0005D054
		public static int SoundCodeAmbientNodeSiegeMangonelFire { get; private set; }

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0005EE5C File Offset: 0x0005D05C
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x0005EE63 File Offset: 0x0005D063
		public static int SoundCodeAmbientNodeSiegeTrebuchetFire { get; private set; }

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0005EE6B File Offset: 0x0005D06B
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x0005EE72 File Offset: 0x0005D072
		public static int SoundCodeAmbientNodeSiegeBallistaHit { get; private set; }

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0005EE7A File Offset: 0x0005D07A
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x0005EE81 File Offset: 0x0005D081
		public static int SoundCodeAmbientNodeSiegeBoulderHit { get; private set; }

		// Token: 0x06001BAE RID: 7086 RVA: 0x0005EE89 File Offset: 0x0005D089
		static MiscSoundContainer()
		{
			MiscSoundContainer.UpdateMiscSoundCodes();
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x0005EE90 File Offset: 0x0005D090
		private static void UpdateMiscSoundCodes()
		{
			MiscSoundContainer.SoundCodeMovementFoleyDoorOpen = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/door_open");
			MiscSoundContainer.SoundCodeMovementFoleyDoorClose = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/door_close");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/ballista_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeMangonelFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/mangonel_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeTrebuchetFire = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/trebuchet_fire");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBallistaHit = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/ballista_hit");
			MiscSoundContainer.SoundCodeAmbientNodeSiegeBoulderHit = SoundEvent.GetEventIdFromString("event:/map/ambient/node/siege/boulder_hit");
		}
	}
}
