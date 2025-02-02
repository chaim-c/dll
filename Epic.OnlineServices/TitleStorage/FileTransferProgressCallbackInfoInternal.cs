using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x02000085 RID: 133
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FileTransferProgressCallbackInfoInternal : ICallbackInfoInternal, IGettable<FileTransferProgressCallbackInfo>, ISettable<FileTransferProgressCallbackInfo>, IDisposable
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x00008018 File Offset: 0x00006218
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00008039 File Offset: 0x00006239
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000804C File Offset: 0x0000624C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x00008064 File Offset: 0x00006264
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x00008085 File Offset: 0x00006285
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00008098 File Offset: 0x00006298
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x000080B9 File Offset: 0x000062B9
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

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x000080CC File Offset: 0x000062CC
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x000080E4 File Offset: 0x000062E4
		public uint BytesTransferred
		{
			get
			{
				return this.m_BytesTransferred;
			}
			set
			{
				this.m_BytesTransferred = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x000080F0 File Offset: 0x000062F0
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x00008108 File Offset: 0x00006308
		public uint TotalFileSizeBytes
		{
			get
			{
				return this.m_TotalFileSizeBytes;
			}
			set
			{
				this.m_TotalFileSizeBytes = value;
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00008114 File Offset: 0x00006314
		public void Set(ref FileTransferProgressCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.BytesTransferred = other.BytesTransferred;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00008164 File Offset: 0x00006364
		public void Set(ref FileTransferProgressCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.BytesTransferred = other.Value.BytesTransferred;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000081E7 File Offset: 0x000063E7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000820E File Offset: 0x0000640E
		public void Get(out FileTransferProgressCallbackInfo output)
		{
			output = default(FileTransferProgressCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040002A2 RID: 674
		private IntPtr m_ClientData;

		// Token: 0x040002A3 RID: 675
		private IntPtr m_LocalUserId;

		// Token: 0x040002A4 RID: 676
		private IntPtr m_Filename;

		// Token: 0x040002A5 RID: 677
		private uint m_BytesTransferred;

		// Token: 0x040002A6 RID: 678
		private uint m_TotalFileSizeBytes;
	}
}
