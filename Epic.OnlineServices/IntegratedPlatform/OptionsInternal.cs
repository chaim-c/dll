using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.IntegratedPlatform
{
	// Token: 0x0200045B RID: 1115
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OptionsInternal : IGettable<Options>, ISettable<Options>, IDisposable
	{
		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0002A23C File Offset: 0x0002843C
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0002A25D File Offset: 0x0002845D
		public Utf8String Type
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Type, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Type);
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0002A270 File Offset: 0x00028470
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0002A288 File Offset: 0x00028488
		public IntegratedPlatformManagementFlags Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0002A294 File Offset: 0x00028494
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x0002A2AC File Offset: 0x000284AC
		public IntPtr InitOptions
		{
			get
			{
				return this.m_InitOptions;
			}
			set
			{
				this.m_InitOptions = value;
			}
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x0002A2B6 File Offset: 0x000284B6
		public void Set(ref Options other)
		{
			this.m_ApiVersion = 1;
			this.Type = other.Type;
			this.Flags = other.Flags;
			this.InitOptions = other.InitOptions;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x0002A2E8 File Offset: 0x000284E8
		public void Set(ref Options? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Type = other.Value.Type;
				this.Flags = other.Value.Flags;
				this.InitOptions = other.Value.InitOptions;
			}
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x0002A348 File Offset: 0x00028548
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Type);
			Helper.Dispose(ref this.m_InitOptions);
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x0002A363 File Offset: 0x00028563
		public void Get(out Options output)
		{
			output = default(Options);
			output.Set(ref this);
		}

		// Token: 0x04000CA4 RID: 3236
		private int m_ApiVersion;

		// Token: 0x04000CA5 RID: 3237
		private IntPtr m_Type;

		// Token: 0x04000CA6 RID: 3238
		private IntegratedPlatformManagementFlags m_Flags;

		// Token: 0x04000CA7 RID: 3239
		private IntPtr m_InitOptions;
	}
}
