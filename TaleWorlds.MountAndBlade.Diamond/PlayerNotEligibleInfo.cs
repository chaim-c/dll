using System;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200010B RID: 267
	[Serializable]
	public class PlayerNotEligibleInfo
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000075A0 File Offset: 0x000057A0
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x000075A8 File Offset: 0x000057A8
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000075B1 File Offset: 0x000057B1
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x000075B9 File Offset: 0x000057B9
		[JsonProperty]
		public PlayerNotEligibleError[] Errors { get; private set; }

		// Token: 0x060005AC RID: 1452 RVA: 0x000075C2 File Offset: 0x000057C2
		public PlayerNotEligibleInfo(PlayerId playerId, PlayerNotEligibleError[] errors)
		{
			this.PlayerId = playerId;
			this.Errors = errors;
		}
	}
}
