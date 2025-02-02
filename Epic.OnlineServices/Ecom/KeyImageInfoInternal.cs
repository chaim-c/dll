using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C3 RID: 1219
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KeyImageInfoInternal : IGettable<KeyImageInfo>, ISettable<KeyImageInfo>, IDisposable
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0002EE84 File Offset: 0x0002D084
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x0002EEA5 File Offset: 0x0002D0A5
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

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x0002EEB8 File Offset: 0x0002D0B8
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x0002EED9 File Offset: 0x0002D0D9
		public Utf8String Url
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Url, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Url);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x0002EEEC File Offset: 0x0002D0EC
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x0002EF04 File Offset: 0x0002D104
		public uint Width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x0002EF10 File Offset: 0x0002D110
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x0002EF28 File Offset: 0x0002D128
		public uint Height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0002EF32 File Offset: 0x0002D132
		public void Set(ref KeyImageInfo other)
		{
			this.m_ApiVersion = 1;
			this.Type = other.Type;
			this.Url = other.Url;
			this.Width = other.Width;
			this.Height = other.Height;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0002EF70 File Offset: 0x0002D170
		public void Set(ref KeyImageInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Type = other.Value.Type;
				this.Url = other.Value.Url;
				this.Width = other.Value.Width;
				this.Height = other.Value.Height;
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0002EFE5 File Offset: 0x0002D1E5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Type);
			Helper.Dispose(ref this.m_Url);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0002F000 File Offset: 0x0002D200
		public void Get(out KeyImageInfo output)
		{
			output = default(KeyImageInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E1B RID: 3611
		private int m_ApiVersion;

		// Token: 0x04000E1C RID: 3612
		private IntPtr m_Type;

		// Token: 0x04000E1D RID: 3613
		private IntPtr m_Url;

		// Token: 0x04000E1E RID: 3614
		private uint m_Width;

		// Token: 0x04000E1F RID: 3615
		private uint m_Height;
	}
}
