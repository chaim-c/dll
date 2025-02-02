using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000529 RID: 1321
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteDeviceIdCallbackInfoInternal : ICallbackInfoInternal, IGettable<DeleteDeviceIdCallbackInfo>, ISettable<DeleteDeviceIdCallbackInfo>, IDisposable
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x00032ABC File Offset: 0x00030CBC
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x00032AD4 File Offset: 0x00030CD4
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

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x00032AE0 File Offset: 0x00030CE0
		// (set) Token: 0x060021E8 RID: 8680 RVA: 0x00032B01 File Offset: 0x00030D01
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

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060021E9 RID: 8681 RVA: 0x00032B14 File Offset: 0x00030D14
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00032B2C File Offset: 0x00030D2C
		public void Set(ref DeleteDeviceIdCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00032B4C File Offset: 0x00030D4C
		public void Set(ref DeleteDeviceIdCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x00032B90 File Offset: 0x00030D90
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x00032B9F File Offset: 0x00030D9F
		public void Get(out DeleteDeviceIdCallbackInfo output)
		{
			output = default(DeleteDeviceIdCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F15 RID: 3861
		private Result m_ResultCode;

		// Token: 0x04000F16 RID: 3862
		private IntPtr m_ClientData;
	}
}
