using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000644 RID: 1604
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AndroidInitializeOptionsSystemInitializeOptionsInternal : IGettable<AndroidInitializeOptionsSystemInitializeOptions>, ISettable<AndroidInitializeOptionsSystemInitializeOptions>, IDisposable
	{
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060028D4 RID: 10452 RVA: 0x0003CC24 File Offset: 0x0003AE24
		// (set) Token: 0x060028D5 RID: 10453 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		public IntPtr Reserved
		{
			get
			{
				return this.m_Reserved;
			}
			set
			{
				this.m_Reserved = value;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060028D6 RID: 10454 RVA: 0x0003CC48 File Offset: 0x0003AE48
		// (set) Token: 0x060028D7 RID: 10455 RVA: 0x0003CC69 File Offset: 0x0003AE69
		public Utf8String OptionalInternalDirectory
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_OptionalInternalDirectory, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OptionalInternalDirectory);
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060028D8 RID: 10456 RVA: 0x0003CC7C File Offset: 0x0003AE7C
		// (set) Token: 0x060028D9 RID: 10457 RVA: 0x0003CC9D File Offset: 0x0003AE9D
		public Utf8String OptionalExternalDirectory
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_OptionalExternalDirectory, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OptionalExternalDirectory);
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x0003CCAD File Offset: 0x0003AEAD
		public void Set(ref AndroidInitializeOptionsSystemInitializeOptions other)
		{
			this.m_ApiVersion = 2;
			this.Reserved = other.Reserved;
			this.OptionalInternalDirectory = other.OptionalInternalDirectory;
			this.OptionalExternalDirectory = other.OptionalExternalDirectory;
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x0003CCE0 File Offset: 0x0003AEE0
		public void Set(ref AndroidInitializeOptionsSystemInitializeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.Reserved = other.Value.Reserved;
				this.OptionalInternalDirectory = other.Value.OptionalInternalDirectory;
				this.OptionalExternalDirectory = other.Value.OptionalExternalDirectory;
			}
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x0003CD40 File Offset: 0x0003AF40
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Reserved);
			Helper.Dispose(ref this.m_OptionalInternalDirectory);
			Helper.Dispose(ref this.m_OptionalExternalDirectory);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x0003CD67 File Offset: 0x0003AF67
		public void Get(out AndroidInitializeOptionsSystemInitializeOptions output)
		{
			output = default(AndroidInitializeOptionsSystemInitializeOptions);
			output.Set(ref this);
		}

		// Token: 0x0400126F RID: 4719
		private int m_ApiVersion;

		// Token: 0x04001270 RID: 4720
		private IntPtr m_Reserved;

		// Token: 0x04001271 RID: 4721
		private IntPtr m_OptionalInternalDirectory;

		// Token: 0x04001272 RID: 4722
		private IntPtr m_OptionalExternalDirectory;
	}
}
