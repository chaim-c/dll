using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200051F RID: 1311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateDeviceIdCallbackInfoInternal : ICallbackInfoInternal, IGettable<CreateDeviceIdCallbackInfo>, ISettable<CreateDeviceIdCallbackInfo>, IDisposable
	{
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00032574 File Offset: 0x00030774
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x0003258C File Offset: 0x0003078C
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

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00032598 File Offset: 0x00030798
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x000325B9 File Offset: 0x000307B9
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

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x000325CC File Offset: 0x000307CC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000325E4 File Offset: 0x000307E4
		public void Set(ref CreateDeviceIdCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00032604 File Offset: 0x00030804
		public void Set(ref CreateDeviceIdCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x00032648 File Offset: 0x00030848
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00032657 File Offset: 0x00030857
		public void Get(out CreateDeviceIdCallbackInfo output)
		{
			output = default(CreateDeviceIdCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F00 RID: 3840
		private Result m_ResultCode;

		// Token: 0x04000F01 RID: 3841
		private IntPtr m_ClientData;
	}
}
