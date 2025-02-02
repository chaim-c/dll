using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020000A1 RID: 161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : ISettable<ReadFileOptions>, IDisposable
	{
		// Token: 0x1700010D RID: 269
		// (set) Token: 0x0600060D RID: 1549 RVA: 0x00008E49 File Offset: 0x00007049
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700010E RID: 270
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x00008E59 File Offset: 0x00007059
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x1700010F RID: 271
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x00008E69 File Offset: 0x00007069
		public uint ReadChunkLengthBytes
		{
			set
			{
				this.m_ReadChunkLengthBytes = value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00008E74 File Offset: 0x00007074
		public static OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				bool flag = ReadFileOptionsInternal.s_ReadFileDataCallback == null;
				if (flag)
				{
					ReadFileOptionsInternal.s_ReadFileDataCallback = new OnReadFileDataCallbackInternal(TitleStorageInterface.OnReadFileDataCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_ReadFileDataCallback;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00008EAC File Offset: 0x000070AC
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				bool flag = ReadFileOptionsInternal.s_FileTransferProgressCallback == null;
				if (flag)
				{
					ReadFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(TitleStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00008EE4 File Offset: 0x000070E4
		public void Set(ref ReadFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.ReadChunkLengthBytes = other.ReadChunkLengthBytes;
			this.m_ReadFileDataCallback = ((other.ReadFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnReadFileDataCallbackInternal>(ReadFileOptionsInternal.ReadFileDataCallback) : IntPtr.Zero);
			this.m_FileTransferProgressCallback = ((other.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(ReadFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00008F60 File Offset: 0x00007160
		public void Set(ref ReadFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.ReadChunkLengthBytes = other.Value.ReadChunkLengthBytes;
				this.m_ReadFileDataCallback = ((other.Value.ReadFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnReadFileDataCallbackInternal>(ReadFileOptionsInternal.ReadFileDataCallback) : IntPtr.Zero);
				this.m_FileTransferProgressCallback = ((other.Value.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(ReadFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00009011 File Offset: 0x00007211
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
			Helper.Dispose(ref this.m_ReadFileDataCallback);
			Helper.Dispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x040002DD RID: 733
		private int m_ApiVersion;

		// Token: 0x040002DE RID: 734
		private IntPtr m_LocalUserId;

		// Token: 0x040002DF RID: 735
		private IntPtr m_Filename;

		// Token: 0x040002E0 RID: 736
		private uint m_ReadChunkLengthBytes;

		// Token: 0x040002E1 RID: 737
		private IntPtr m_ReadFileDataCallback;

		// Token: 0x040002E2 RID: 738
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x040002E3 RID: 739
		private static OnReadFileDataCallbackInternal s_ReadFileDataCallback;

		// Token: 0x040002E4 RID: 740
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
