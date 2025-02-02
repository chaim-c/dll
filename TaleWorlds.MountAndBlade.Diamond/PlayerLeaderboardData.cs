using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200014D RID: 333
	[Serializable]
	public class PlayerLeaderboardData
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600091E RID: 2334 RVA: 0x0000D91B File Offset: 0x0000BB1B
		// (set) Token: 0x0600091F RID: 2335 RVA: 0x0000D923 File Offset: 0x0000BB23
		public PlayerId PlayerId { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0000D92C File Offset: 0x0000BB2C
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000D934 File Offset: 0x0000BB34
		public string RankId { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0000D93D File Offset: 0x0000BB3D
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0000D945 File Offset: 0x0000BB45
		public int Rating { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0000D94E File Offset: 0x0000BB4E
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0000D956 File Offset: 0x0000BB56
		public string Name { get; set; }

		// Token: 0x06000926 RID: 2342 RVA: 0x0000D95F File Offset: 0x0000BB5F
		public PlayerLeaderboardData(PlayerId playerId, string rankId, int rating, string name)
		{
			this.PlayerId = playerId;
			this.RankId = rankId;
			this.Rating = rating;
			this.Name = name;
		}
	}
}
