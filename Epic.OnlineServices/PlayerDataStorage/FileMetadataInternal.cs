using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000270 RID: 624
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : IGettable<FileMetadata>, ISettable<FileMetadata>, IDisposable
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00019444 File Offset: 0x00017644
		// (set) Token: 0x0600110E RID: 4366 RVA: 0x0001945C File Offset: 0x0001765C
		public uint FileSizeBytes
		{
			get
			{
				return this.m_FileSizeBytes;
			}
			set
			{
				this.m_FileSizeBytes = value;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00019468 File Offset: 0x00017668
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00019489 File Offset: 0x00017689
		public Utf8String MD5Hash
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_MD5Hash, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_MD5Hash);
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0001949C File Offset: 0x0001769C
		// (set) Token: 0x06001112 RID: 4370 RVA: 0x000194BD File Offset: 0x000176BD
		public Utf8String Filename
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Filename, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x000194D0 File Offset: 0x000176D0
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x000194F1 File Offset: 0x000176F1
		public DateTimeOffset? LastModifiedTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_LastModifiedTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LastModifiedTime);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x00019504 File Offset: 0x00017704
		// (set) Token: 0x06001116 RID: 4374 RVA: 0x0001951C File Offset: 0x0001771C
		public uint UnencryptedDataSizeBytes
		{
			get
			{
				return this.m_UnencryptedDataSizeBytes;
			}
			set
			{
				this.m_UnencryptedDataSizeBytes = value;
			}
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00019528 File Offset: 0x00017728
		public void Set(ref FileMetadata other)
		{
			this.m_ApiVersion = 3;
			this.FileSizeBytes = other.FileSizeBytes;
			this.MD5Hash = other.MD5Hash;
			this.Filename = other.Filename;
			this.LastModifiedTime = other.LastModifiedTime;
			this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x00019580 File Offset: 0x00017780
		public void Set(ref FileMetadata? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.FileSizeBytes = other.Value.FileSizeBytes;
				this.MD5Hash = other.Value.MD5Hash;
				this.Filename = other.Value.Filename;
				this.LastModifiedTime = other.Value.LastModifiedTime;
				this.UnencryptedDataSizeBytes = other.Value.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0001960A File Offset: 0x0001780A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_MD5Hash);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00019625 File Offset: 0x00017825
		public void Get(out FileMetadata output)
		{
			output = default(FileMetadata);
			output.Set(ref this);
		}

		// Token: 0x040007A3 RID: 1955
		private int m_ApiVersion;

		// Token: 0x040007A4 RID: 1956
		private uint m_FileSizeBytes;

		// Token: 0x040007A5 RID: 1957
		private IntPtr m_MD5Hash;

		// Token: 0x040007A6 RID: 1958
		private IntPtr m_Filename;

		// Token: 0x040007A7 RID: 1959
		private long m_LastModifiedTime;

		// Token: 0x040007A8 RID: 1960
		private uint m_UnencryptedDataSizeBytes;
	}
}
