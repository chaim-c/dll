using System;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x0200045A RID: 1114
	public struct Options
	{
		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x0002A1DD File Offset: 0x000283DD
		// (set) Token: 0x06001C7F RID: 7295 RVA: 0x0002A1E5 File Offset: 0x000283E5
		public Utf8String Type { get; set; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x0002A1EE File Offset: 0x000283EE
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x0002A1F6 File Offset: 0x000283F6
		public IntegratedPlatformManagementFlags Flags { get; set; }

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x0002A1FF File Offset: 0x000283FF
		// (set) Token: 0x06001C83 RID: 7299 RVA: 0x0002A207 File Offset: 0x00028407
		public IntPtr InitOptions { get; set; }

		// Token: 0x06001C84 RID: 7300 RVA: 0x0002A210 File Offset: 0x00028410
		internal void Set(ref OptionsInternal other)
		{
			this.Type = other.Type;
			this.Flags = other.Flags;
			this.InitOptions = other.InitOptions;
		}
	}
}
