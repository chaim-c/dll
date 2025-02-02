using System;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x0200031A RID: 794
	public struct LogMessage
	{
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x0001F9D3 File Offset: 0x0001DBD3
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0001F9DB File Offset: 0x0001DBDB
		public Utf8String Category { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x0001F9E4 File Offset: 0x0001DBE4
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0001F9EC File Offset: 0x0001DBEC
		public Utf8String Message { get; set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0001F9F5 File Offset: 0x0001DBF5
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x0001F9FD File Offset: 0x0001DBFD
		public LogLevel Level { get; set; }

		// Token: 0x0600154C RID: 5452 RVA: 0x0001FA06 File Offset: 0x0001DC06
		internal void Set(ref LogMessageInternal other)
		{
			this.Category = other.Category;
			this.Message = other.Message;
			this.Level = other.Level;
		}
	}
}
