using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029D RID: 669
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct WriteFileDataCallbackInfoInternal : ICallbackInfoInternal, IGettable<WriteFileDataCallbackInfo>, ISettable<WriteFileDataCallbackInfo>, IDisposable
	{
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x0001B048 File Offset: 0x00019248
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0001B069 File Offset: 0x00019269
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

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0001B07C File Offset: 0x0001927C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x0001B094 File Offset: 0x00019294
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x0001B0B5 File Offset: 0x000192B5
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

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x0001B0C8 File Offset: 0x000192C8
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x0001B0E9 File Offset: 0x000192E9
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x0001B0FC File Offset: 0x000192FC
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x0001B114 File Offset: 0x00019314
		public uint DataBufferLengthBytes
		{
			get
			{
				return this.m_DataBufferLengthBytes;
			}
			set
			{
				this.m_DataBufferLengthBytes = value;
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0001B11E File Offset: 0x0001931E
		public void Set(ref WriteFileDataCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.DataBufferLengthBytes = other.DataBufferLengthBytes;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0001B158 File Offset: 0x00019358
		public void Set(ref WriteFileDataCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.DataBufferLengthBytes = other.Value.DataBufferLengthBytes;
			}
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0001B1C6 File Offset: 0x000193C6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0001B1ED File Offset: 0x000193ED
		public void Get(out WriteFileDataCallbackInfo output)
		{
			output = default(WriteFileDataCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000815 RID: 2069
		private IntPtr m_ClientData;

		// Token: 0x04000816 RID: 2070
		private IntPtr m_LocalUserId;

		// Token: 0x04000817 RID: 2071
		private IntPtr m_Filename;

		// Token: 0x04000818 RID: 2072
		private uint m_DataBufferLengthBytes;
	}
}
