using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000298 RID: 664
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileOptionsInternal : ISettable<ReadFileOptions>, IDisposable
	{
		// Token: 0x170004D1 RID: 1233
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0001AB69 File Offset: 0x00018D69
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (set) Token: 0x06001222 RID: 4642 RVA: 0x0001AB79 File Offset: 0x00018D79
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (set) Token: 0x06001223 RID: 4643 RVA: 0x0001AB89 File Offset: 0x00018D89
		public uint ReadChunkLengthBytes
		{
			set
			{
				this.m_ReadChunkLengthBytes = value;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0001AB94 File Offset: 0x00018D94
		public static OnReadFileDataCallbackInternal ReadFileDataCallback
		{
			get
			{
				bool flag = ReadFileOptionsInternal.s_ReadFileDataCallback == null;
				if (flag)
				{
					ReadFileOptionsInternal.s_ReadFileDataCallback = new OnReadFileDataCallbackInternal(PlayerDataStorageInterface.OnReadFileDataCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_ReadFileDataCallback;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0001ABCC File Offset: 0x00018DCC
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				bool flag = ReadFileOptionsInternal.s_FileTransferProgressCallback == null;
				if (flag)
				{
					ReadFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return ReadFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0001AC04 File Offset: 0x00018E04
		public void Set(ref ReadFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.ReadChunkLengthBytes = other.ReadChunkLengthBytes;
			this.m_ReadFileDataCallback = ((other.ReadFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnReadFileDataCallbackInternal>(ReadFileOptionsInternal.ReadFileDataCallback) : IntPtr.Zero);
			this.m_FileTransferProgressCallback = ((other.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(ReadFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0001AC80 File Offset: 0x00018E80
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

		// Token: 0x06001228 RID: 4648 RVA: 0x0001AD31 File Offset: 0x00018F31
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
			Helper.Dispose(ref this.m_ReadFileDataCallback);
			Helper.Dispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x040007FD RID: 2045
		private int m_ApiVersion;

		// Token: 0x040007FE RID: 2046
		private IntPtr m_LocalUserId;

		// Token: 0x040007FF RID: 2047
		private IntPtr m_Filename;

		// Token: 0x04000800 RID: 2048
		private uint m_ReadChunkLengthBytes;

		// Token: 0x04000801 RID: 2049
		private IntPtr m_ReadFileDataCallback;

		// Token: 0x04000802 RID: 2050
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x04000803 RID: 2051
		private static OnReadFileDataCallbackInternal s_ReadFileDataCallback;

		// Token: 0x04000804 RID: 2052
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
