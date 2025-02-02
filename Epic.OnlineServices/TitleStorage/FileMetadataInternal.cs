using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000083 RID: 131
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileMetadataInternal : IGettable<FileMetadata>, ISettable<FileMetadata>, IDisposable
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00007DC8 File Offset: 0x00005FC8
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00007DE0 File Offset: 0x00005FE0
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

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00007DEC File Offset: 0x00005FEC
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x00007E0D File Offset: 0x0000600D
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

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x00007E20 File Offset: 0x00006020
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x00007E41 File Offset: 0x00006041
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

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x00007E54 File Offset: 0x00006054
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x00007E6C File Offset: 0x0000606C
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

		// Token: 0x0600053C RID: 1340 RVA: 0x00007E76 File Offset: 0x00006076
		public void Set(ref FileMetadata other)
		{
			this.m_ApiVersion = 2;
			this.FileSizeBytes = other.FileSizeBytes;
			this.MD5Hash = other.MD5Hash;
			this.Filename = other.Filename;
			this.UnencryptedDataSizeBytes = other.UnencryptedDataSizeBytes;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00007EB4 File Offset: 0x000060B4
		public void Set(ref FileMetadata? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.FileSizeBytes = other.Value.FileSizeBytes;
				this.MD5Hash = other.Value.MD5Hash;
				this.Filename = other.Value.Filename;
				this.UnencryptedDataSizeBytes = other.Value.UnencryptedDataSizeBytes;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00007F29 File Offset: 0x00006129
		public void Dispose()
		{
			Helper.Dispose(ref this.m_MD5Hash);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00007F44 File Offset: 0x00006144
		public void Get(out FileMetadata output)
		{
			output = default(FileMetadata);
			output.Set(ref this);
		}

		// Token: 0x04000298 RID: 664
		private int m_ApiVersion;

		// Token: 0x04000299 RID: 665
		private uint m_FileSizeBytes;

		// Token: 0x0400029A RID: 666
		private IntPtr m_MD5Hash;

		// Token: 0x0400029B RID: 667
		private IntPtr m_Filename;

		// Token: 0x0400029C RID: 668
		private uint m_UnencryptedDataSizeBytes;
	}
}
