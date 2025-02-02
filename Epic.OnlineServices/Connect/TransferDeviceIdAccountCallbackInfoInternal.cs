using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000561 RID: 1377
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransferDeviceIdAccountCallbackInfoInternal : ICallbackInfoInternal, IGettable<TransferDeviceIdAccountCallbackInfo>, ISettable<TransferDeviceIdAccountCallbackInfo>, IDisposable
	{
		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x00034098 File Offset: 0x00032298
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x000340B0 File Offset: 0x000322B0
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000340BC File Offset: 0x000322BC
		// (set) Token: 0x06002331 RID: 9009 RVA: 0x000340DD File Offset: 0x000322DD
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

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x000340F0 File Offset: 0x000322F0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00034108 File Offset: 0x00032308
		// (set) Token: 0x06002334 RID: 9012 RVA: 0x00034129 File Offset: 0x00032329
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

		// Token: 0x06002335 RID: 9013 RVA: 0x00034139 File Offset: 0x00032339
		public void Set(ref TransferDeviceIdAccountCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00034164 File Offset: 0x00032364
		public void Set(ref TransferDeviceIdAccountCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000341BD File Offset: 0x000323BD
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000341D8 File Offset: 0x000323D8
		public void Get(out TransferDeviceIdAccountCallbackInfo output)
		{
			output = default(TransferDeviceIdAccountCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F78 RID: 3960
		private Result m_ResultCode;

		// Token: 0x04000F79 RID: 3961
		private IntPtr m_ClientData;

		// Token: 0x04000F7A RID: 3962
		private IntPtr m_LocalUserId;
	}
}
