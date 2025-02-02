using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000296 RID: 662
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReadFileDataCallbackInfoInternal : ICallbackInfoInternal, IGettable<ReadFileDataCallbackInfo>, ISettable<ReadFileDataCallbackInfo>, IDisposable
	{
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0001A88C File Offset: 0x00018A8C
		// (set) Token: 0x06001207 RID: 4615 RVA: 0x0001A8AD File Offset: 0x00018AAD
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

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0001A8F9 File Offset: 0x00018AF9
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

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0001A90C File Offset: 0x00018B0C
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x0001A92D File Offset: 0x00018B2D
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

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x0001A940 File Offset: 0x00018B40
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x0001A958 File Offset: 0x00018B58
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

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0001A964 File Offset: 0x00018B64
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x0001A985 File Offset: 0x00018B85
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

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0001A998 File Offset: 0x00018B98
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x0001A9BF File Offset: 0x00018BBF
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

		// Token: 0x06001213 RID: 4627 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		public void Set(ref ReadFileDataCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
			this.IsLastChunk = other.IsLastChunk;
			this.DataChunk = other.DataChunk;
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x0001AA34 File Offset: 0x00018C34
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

		// Token: 0x06001215 RID: 4629 RVA: 0x0001AACF File Offset: 0x00018CCF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
			Helper.Dispose(ref this.m_DataChunk);
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0001AB02 File Offset: 0x00018D02
		public void Get(out ReadFileDataCallbackInfo output)
		{
			output = default(ReadFileDataCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040007F1 RID: 2033
		private IntPtr m_ClientData;

		// Token: 0x040007F2 RID: 2034
		private IntPtr m_LocalUserId;

		// Token: 0x040007F3 RID: 2035
		private IntPtr m_Filename;

		// Token: 0x040007F4 RID: 2036
		private uint m_TotalFileSizeBytes;

		// Token: 0x040007F5 RID: 2037
		private int m_IsLastChunk;

		// Token: 0x040007F6 RID: 2038
		private uint m_DataChunkLengthBytes;

		// Token: 0x040007F7 RID: 2039
		private IntPtr m_DataChunk;
	}
}
