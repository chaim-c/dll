using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000191 RID: 401
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ParticipantMetadataInternal : IGettable<ParticipantMetadata>, ISettable<ParticipantMetadata>, IDisposable
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00010DD8 File Offset: 0x0000EFD8
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00010DF9 File Offset: 0x0000EFF9
		public Utf8String Key
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00010E0C File Offset: 0x0000F00C
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00010E2D File Offset: 0x0000F02D
		public Utf8String Value
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Value, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Value);
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00010E3D File Offset: 0x0000F03D
		public void Set(ref ParticipantMetadata other)
		{
			this.m_ApiVersion = 1;
			this.Key = other.Key;
			this.Value = other.Value;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00010E64 File Offset: 0x0000F064
		public void Set(ref ParticipantMetadata? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Value.Key;
				this.Value = other.Value.Value;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00010EAF File Offset: 0x0000F0AF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Key);
			Helper.Dispose(ref this.m_Value);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00010ECA File Offset: 0x0000F0CA
		public void Get(out ParticipantMetadata output)
		{
			output = default(ParticipantMetadata);
			output.Set(ref this);
		}

		// Token: 0x04000533 RID: 1331
		private int m_ApiVersion;

		// Token: 0x04000534 RID: 1332
		private IntPtr m_Key;

		// Token: 0x04000535 RID: 1333
		private IntPtr m_Value;
	}
}
