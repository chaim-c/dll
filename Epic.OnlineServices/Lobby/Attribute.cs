using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000330 RID: 816
	public struct Attribute
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public AttributeData? Data { get; set; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x0001FDC5 File Offset: 0x0001DFC5
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x0001FDCD File Offset: 0x0001DFCD
		public LobbyAttributeVisibility Visibility { get; set; }

		// Token: 0x06001584 RID: 5508 RVA: 0x0001FDD6 File Offset: 0x0001DFD6
		internal void Set(ref AttributeInternal other)
		{
			this.Data = other.Data;
			this.Visibility = other.Visibility;
		}
	}
}
