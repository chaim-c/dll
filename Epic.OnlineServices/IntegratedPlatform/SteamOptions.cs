using System;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x0200045C RID: 1116
	public struct SteamOptions
	{
		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0002A375 File Offset: 0x00028575
		// (set) Token: 0x06001C90 RID: 7312 RVA: 0x0002A37D File Offset: 0x0002857D
		public Utf8String OverrideLibraryPath { get; set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x0002A386 File Offset: 0x00028586
		// (set) Token: 0x06001C92 RID: 7314 RVA: 0x0002A38E File Offset: 0x0002858E
		public uint SteamMajorVersion { get; set; }

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0002A397 File Offset: 0x00028597
		// (set) Token: 0x06001C94 RID: 7316 RVA: 0x0002A39F File Offset: 0x0002859F
		public uint SteamMinorVersion { get; set; }

		// Token: 0x06001C95 RID: 7317 RVA: 0x0002A3A8 File Offset: 0x000285A8
		internal void Set(ref SteamOptionsInternal other)
		{
			this.OverrideLibraryPath = other.OverrideLibraryPath;
			this.SteamMajorVersion = other.SteamMajorVersion;
			this.SteamMinorVersion = other.SteamMinorVersion;
		}
	}
}
