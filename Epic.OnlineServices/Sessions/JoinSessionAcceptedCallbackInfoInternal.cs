using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EC RID: 236
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinSessionAcceptedCallbackInfo>, ISettable<JoinSessionAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0000B8D1 File Offset: 0x00009AD1
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

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0000B8FC File Offset: 0x00009AFC
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x0000B91D File Offset: 0x00009B1D
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

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000B930 File Offset: 0x00009B30
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x0000B948 File Offset: 0x00009B48
		public ulong UiEventId
		{
			get
			{
				return this.m_UiEventId;
			}
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000B952 File Offset: 0x00009B52
		public void Set(ref JoinSessionAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.UiEventId = other.UiEventId;
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000B97C File Offset: 0x00009B7C
		public void Set(ref JoinSessionAcceptedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000B9D5 File Offset: 0x00009BD5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public void Get(out JoinSessionAcceptedCallbackInfo output)
		{
			output = default(JoinSessionAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003A6 RID: 934
		private IntPtr m_ClientData;

		// Token: 0x040003A7 RID: 935
		private IntPtr m_LocalUserId;

		// Token: 0x040003A8 RID: 936
		private ulong m_UiEventId;
	}
}
