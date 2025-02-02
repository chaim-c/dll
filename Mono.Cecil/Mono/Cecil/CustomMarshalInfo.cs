using System;

namespace Mono.Cecil
{
	// Token: 0x020000B3 RID: 179
	public sealed class CustomMarshalInfo : MarshalInfo
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00018798 File Offset: 0x00016998
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x000187A0 File Offset: 0x000169A0
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x000187A9 File Offset: 0x000169A9
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x000187B1 File Offset: 0x000169B1
		public string UnmanagedType
		{
			get
			{
				return this.unmanaged_type;
			}
			set
			{
				this.unmanaged_type = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000187BA File Offset: 0x000169BA
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x000187C2 File Offset: 0x000169C2
		public TypeReference ManagedType
		{
			get
			{
				return this.managed_type;
			}
			set
			{
				this.managed_type = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x000187CB File Offset: 0x000169CB
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x000187D3 File Offset: 0x000169D3
		public string Cookie
		{
			get
			{
				return this.cookie;
			}
			set
			{
				this.cookie = value;
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000187DC File Offset: 0x000169DC
		public CustomMarshalInfo() : base(NativeType.CustomMarshaler)
		{
		}

		// Token: 0x0400044C RID: 1100
		internal Guid guid;

		// Token: 0x0400044D RID: 1101
		internal string unmanaged_type;

		// Token: 0x0400044E RID: 1102
		internal TypeReference managed_type;

		// Token: 0x0400044F RID: 1103
		internal string cookie;
	}
}
