using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009F RID: 159
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileDataCallbackInfo>, ISettable<ReadFileDataCallbackInfo>, IDisposable
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00008B6C File Offset: 0x00006D6C
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00008B8D File Offset: 0x00006D8D
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00008BB8 File Offset: 0x00006DB8
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x00008BD9 File Offset: 0x00006DD9
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

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x00008BEC File Offset: 0x00006DEC
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x00008C0D File Offset: 0x00006E0D
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

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00008C20 File Offset: 0x00006E20
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00008C38 File Offset: 0x00006E38
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

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00008C44 File Offset: 0x00006E44
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00008C65 File Offset: 0x00006E65
		public bool IsLastChunk
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsLastChunk, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsLastChunk);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00008C78 File Offset: 0x00006E78
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00008C9F File Offset: 0x00006E9F
		public ArraySegment<byte> DataChunk
		{
			get
			{
				ArraySegment<byte> result;
				Helper.Get(this.m_DataChunk, out result, this.m_DataChunkLengthBytes);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DataChunk, out this.m_DataChunkLengthBytes);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public void Set(ref ReadFileDataCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
			this.IsLastChunk = other.IsLastChunk;
			this.DataChunk = other.DataChunk;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00008D14 File Offset: 0x00006F14
		public void Set(ref ReadFileDataCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
				this.IsLastChunk = other.Value.IsLastChunk;
				this.DataChunk = other.Value.DataChunk;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00008DAF File Offset: 0x00006FAF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
			Helper.Dispose(ref this.m_DataChunk);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00008DE2 File Offset: 0x00006FE2
		public void Get(out ReadFileDataCallbackInfo output)
		{
			output = default(ReadFileDataCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040002D1 RID: 721
		private IntPtr m_ClientData;

		// Token: 0x040002D2 RID: 722
		private IntPtr m_LocalUserId;

		// Token: 0x040002D3 RID: 723
		private IntPtr m_Filename;

		// Token: 0x040002D4 RID: 724
		private uint m_TotalFileSizeBytes;

		// Token: 0x040002D5 RID: 725
		private int m_IsLastChunk;

		// Token: 0x040002D6 RID: 726
		private uint m_DataChunkLengthBytes;

		// Token: 0x040002D7 RID: 727
		private IntPtr m_DataChunk;
	}
}
