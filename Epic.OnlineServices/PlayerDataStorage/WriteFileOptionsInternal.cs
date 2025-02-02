using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029F RID: 671
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileOptionsInternal : ISettable<WriteFileOptions>, IDisposable
	{
		// Token: 0x170004ED RID: 1261
		// (set) Token: 0x06001261 RID: 4705 RVA: 0x0001B254 File Offset: 0x00019454
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0001B264 File Offset: 0x00019464
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (set) Token: 0x06001263 RID: 4707 RVA: 0x0001B274 File Offset: 0x00019474
		public uint ChunkLengthBytes
		{
			set
			{
				this.m_ChunkLengthBytes = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0001B280 File Offset: 0x00019480
		public static OnWriteFileDataCallbackInternal WriteFileDataCallback
		{
			get
			{
				bool flag = WriteFileOptionsInternal.s_WriteFileDataCallback == null;
				if (flag)
				{
					WriteFileOptionsInternal.s_WriteFileDataCallback = new OnWriteFileDataCallbackInternal(PlayerDataStorageInterface.OnWriteFileDataCallbackInternalImplementation);
				}
				return WriteFileOptionsInternal.s_WriteFileDataCallback;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0001B2B8 File Offset: 0x000194B8
		public static OnFileTransferProgressCallbackInternal FileTransferProgressCallback
		{
			get
			{
				bool flag = WriteFileOptionsInternal.s_FileTransferProgressCallback == null;
				if (flag)
				{
					WriteFileOptionsInternal.s_FileTransferProgressCallback = new OnFileTransferProgressCallbackInternal(PlayerDataStorageInterface.OnFileTransferProgressCallbackInternalImplementation);
				}
				return WriteFileOptionsInternal.s_FileTransferProgressCallback;
			}
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public void Set(ref WriteFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.ChunkLengthBytes = other.ChunkLengthBytes;
			this.m_WriteFileDataCallback = ((other.WriteFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnWriteFileDataCallbackInternal>(WriteFileOptionsInternal.WriteFileDataCallback) : IntPtr.Zero);
			this.m_FileTransferProgressCallback = ((other.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(WriteFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0001B36C File Offset: 0x0001956C
		public void Set(ref WriteFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.ChunkLengthBytes = other.Value.ChunkLengthBytes;
				this.m_WriteFileDataCallback = ((other.Value.WriteFileDataCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnWriteFileDataCallbackInternal>(WriteFileOptionsInternal.WriteFileDataCallback) : IntPtr.Zero);
				this.m_FileTransferProgressCallback = ((other.Value.FileTransferProgressCallback != null) ? Marshal.GetFunctionPointerForDelegate<OnFileTransferProgressCallbackInternal>(WriteFileOptionsInternal.FileTransferProgressCallback) : IntPtr.Zero);
			}
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0001B41D File Offset: 0x0001961D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
			Helper.Dispose(ref this.m_WriteFileDataCallback);
			Helper.Dispose(ref this.m_FileTransferProgressCallback);
		}

		// Token: 0x0400081E RID: 2078
		private int m_ApiVersion;

		// Token: 0x0400081F RID: 2079
		private IntPtr m_LocalUserId;

		// Token: 0x04000820 RID: 2080
		private IntPtr m_Filename;

		// Token: 0x04000821 RID: 2081
		private uint m_ChunkLengthBytes;

		// Token: 0x04000822 RID: 2082
		private IntPtr m_WriteFileDataCallback;

		// Token: 0x04000823 RID: 2083
		private IntPtr m_FileTransferProgressCallback;

		// Token: 0x04000824 RID: 2084
		private static OnWriteFileDataCallbackInternal s_WriteFileDataCallback;

		// Token: 0x04000825 RID: 2085
		private static OnFileTransferProgressCallbackInternal s_FileTransferProgressCallback;
	}
}
