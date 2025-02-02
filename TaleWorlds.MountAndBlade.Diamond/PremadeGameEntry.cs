using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000158 RID: 344
	[Serializable]
	public class PremadeGameEntry
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0000E28D File Offset: 0x0000C48D
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x0000E295 File Offset: 0x0000C495
		[JsonProperty]
		public Guid Id { get; private set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x0000E29E File Offset: 0x0000C49E
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0000E2A6 File Offset: 0x0000C4A6
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0000E2B7 File Offset: 0x0000C4B7
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x0000E2D9 File Offset: 0x0000C4D9
		[JsonProperty]
		public string MapName { get; private set; }

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0000E2E2 File Offset: 0x0000C4E2
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0000E2EA File Offset: 0x0000C4EA
		[JsonProperty]
		public string FactionA { get; private set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x0000E2F3 File Offset: 0x0000C4F3
		// (set) Token: 0x0600099D RID: 2461 RVA: 0x0000E2FB File Offset: 0x0000C4FB
		[JsonProperty]
		public string FactionB { get; private set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0000E304 File Offset: 0x0000C504
		// (set) Token: 0x0600099F RID: 2463 RVA: 0x0000E30C File Offset: 0x0000C50C
		[JsonProperty]
		public bool IsPasswordProtected { get; private set; }

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x0000E315 File Offset: 0x0000C515
		// (set) Token: 0x060009A1 RID: 2465 RVA: 0x0000E31D File Offset: 0x0000C51D
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000E326 File Offset: 0x0000C526
		public PremadeGameEntry()
		{
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000E330 File Offset: 0x0000C530
		public PremadeGameEntry(Guid id, string name, string region, string gameType, string mapName, string factionA, string factionB, bool isPasswordProtected, PremadeGameType premadeGameType)
		{
			this.Id = id;
			this.Name = name;
			this.Region = region;
			this.GameType = gameType;
			this.MapName = mapName;
			this.FactionA = factionA;
			this.FactionB = factionB;
			this.IsPasswordProtected = isPasswordProtected;
			this.PremadeGameType = premadeGameType;
		}
	}
}
