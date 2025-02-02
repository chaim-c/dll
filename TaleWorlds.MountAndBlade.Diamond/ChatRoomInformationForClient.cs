using System;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public class ChatRoomInformationForClient
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00007308 File Offset: 0x00005508
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00007310 File Offset: 0x00005510
		[JsonProperty]
		public Guid RoomId { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00007319 File Offset: 0x00005519
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00007321 File Offset: 0x00005521
		[JsonProperty]
		public string Name { get; private set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000732A File Offset: 0x0000552A
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00007332 File Offset: 0x00005532
		[JsonProperty]
		public string Endpoint { get; private set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000733B File Offset: 0x0000553B
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00007343 File Offset: 0x00005543
		[JsonProperty]
		public string RoomColor { get; private set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x0000734C File Offset: 0x0000554C
		public ChatRoomInformationForClient()
		{
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00007354 File Offset: 0x00005554
		public ChatRoomInformationForClient(Guid roomId, string name, string endpoint, string color)
		{
			this.RoomId = roomId;
			this.Name = name;
			this.Endpoint = endpoint;
			this.RoomColor = color;
		}
	}
}
