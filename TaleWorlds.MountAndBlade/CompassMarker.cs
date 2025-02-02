using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200024F RID: 591
	public class CompassMarker
	{
		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001F89 RID: 8073 RVA: 0x000700A8 File Offset: 0x0006E2A8
		// (set) Token: 0x06001F8A RID: 8074 RVA: 0x000700B0 File Offset: 0x0006E2B0
		public string Id { get; private set; }

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001F8B RID: 8075 RVA: 0x000700B9 File Offset: 0x0006E2B9
		// (set) Token: 0x06001F8C RID: 8076 RVA: 0x000700C1 File Offset: 0x0006E2C1
		public float Angle { get; private set; }

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001F8D RID: 8077 RVA: 0x000700CA File Offset: 0x0006E2CA
		// (set) Token: 0x06001F8E RID: 8078 RVA: 0x000700D2 File Offset: 0x0006E2D2
		public bool IsPrimary { get; private set; }

		// Token: 0x06001F8F RID: 8079 RVA: 0x000700DB File Offset: 0x0006E2DB
		public CompassMarker(string id, float angle, bool isPrimary)
		{
			this.Id = id;
			this.Angle = angle % 360f;
			this.IsPrimary = isPrimary;
		}
	}
}
