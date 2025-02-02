using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200023A RID: 570
	public struct Info
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00017282 File Offset: 0x00015482
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0001728A File Offset: 0x0001548A
		public Status Status { get; set; }

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00017293 File Offset: 0x00015493
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0001729B File Offset: 0x0001549B
		public EpicAccountId UserId { get; set; }

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000172A4 File Offset: 0x000154A4
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x000172AC File Offset: 0x000154AC
		public Utf8String ProductId { get; set; }

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x000172B5 File Offset: 0x000154B5
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x000172BD File Offset: 0x000154BD
		public Utf8String ProductVersion { get; set; }

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x000172C6 File Offset: 0x000154C6
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x000172CE File Offset: 0x000154CE
		public Utf8String Platform { get; set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x000172D7 File Offset: 0x000154D7
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x000172DF File Offset: 0x000154DF
		public Utf8String RichText { get; set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000172E8 File Offset: 0x000154E8
		// (set) Token: 0x06000FB6 RID: 4022 RVA: 0x000172F0 File Offset: 0x000154F0
		public DataRecord[] Records { get; set; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x000172F9 File Offset: 0x000154F9
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00017301 File Offset: 0x00015501
		public Utf8String ProductName { get; set; }

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0001730A File Offset: 0x0001550A
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00017312 File Offset: 0x00015512
		public Utf8String IntegratedPlatform { get; set; }

		// Token: 0x06000FBB RID: 4027 RVA: 0x0001731C File Offset: 0x0001551C
		internal void Set(ref InfoInternal other)
		{
			this.Status = other.Status;
			this.UserId = other.UserId;
			this.ProductId = other.ProductId;
			this.ProductVersion = other.ProductVersion;
			this.Platform = other.Platform;
			this.RichText = other.RichText;
			this.Records = other.Records;
			this.ProductName = other.ProductName;
			this.IntegratedPlatform = other.IntegratedPlatform;
		}
	}
}
