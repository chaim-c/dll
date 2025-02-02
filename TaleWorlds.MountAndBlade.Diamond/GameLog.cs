using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public class GameLog
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x00008398 File Offset: 0x00006598
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x000083A0 File Offset: 0x000065A0
		public int Id { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x000083A9 File Offset: 0x000065A9
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x000083B1 File Offset: 0x000065B1
		public GameLogType Type { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x000083BA File Offset: 0x000065BA
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x000083C2 File Offset: 0x000065C2
		public PlayerId Player { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x000083CB File Offset: 0x000065CB
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x000083D3 File Offset: 0x000065D3
		public float GameTime { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x000083DC File Offset: 0x000065DC
		// (set) Token: 0x0600063D RID: 1597 RVA: 0x000083E4 File Offset: 0x000065E4
		public Dictionary<string, string> Data { get; set; }

		// Token: 0x0600063E RID: 1598 RVA: 0x000083ED File Offset: 0x000065ED
		public GameLog()
		{
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000083F5 File Offset: 0x000065F5
		public GameLog(GameLogType type, PlayerId player, float gameTime)
		{
			this.Type = type;
			this.Player = player;
			this.GameTime = gameTime;
			this.Data = new Dictionary<string, string>();
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00008420 File Offset: 0x00006620
		public string GetDataAsString()
		{
			string result = "{}";
			try
			{
				result = JsonConvert.SerializeObject(this.Data, Formatting.None);
			}
			catch (Exception)
			{
			}
			return result;
		}
	}
}
