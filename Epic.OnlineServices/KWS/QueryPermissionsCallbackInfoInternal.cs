using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000448 RID: 1096
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPermissionsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryPermissionsCallbackInfo>, ISettable<QueryPermissionsCallbackInfo>, IDisposable
	{
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0002983C File Offset: 0x00027A3C
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x00029854 File Offset: 0x00027A54
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

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00029860 File Offset: 0x00027A60
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x00029881 File Offset: 0x00027A81
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

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00029894 File Offset: 0x00027A94
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000298AC File Offset: 0x00027AAC
		// (set) Token: 0x06001C25 RID: 7205 RVA: 0x000298CD File Offset: 0x00027ACD
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

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000298E0 File Offset: 0x00027AE0
		// (set) Token: 0x06001C27 RID: 7207 RVA: 0x00029901 File Offset: 0x00027B01
		public Utf8String KWSUserId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_KWSUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_KWSUserId);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x00029914 File Offset: 0x00027B14
		// (set) Token: 0x06001C29 RID: 7209 RVA: 0x00029935 File Offset: 0x00027B35
		public Utf8String DateOfBirth
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DateOfBirth, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DateOfBirth);
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00029948 File Offset: 0x00027B48
		// (set) Token: 0x06001C2B RID: 7211 RVA: 0x00029969 File Offset: 0x00027B69
		public bool IsMinor
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsMinor, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsMinor);
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0002997C File Offset: 0x00027B7C
		public void Set(ref QueryPermissionsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.KWSUserId = other.KWSUserId;
			this.DateOfBirth = other.DateOfBirth;
			this.IsMinor = other.IsMinor;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000299D8 File Offset: 0x00027BD8
		public void Set(ref QueryPermissionsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.KWSUserId = other.Value.KWSUserId;
				this.DateOfBirth = other.Value.DateOfBirth;
				this.IsMinor = other.Value.IsMinor;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x00029A73 File Offset: 0x00027C73
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_KWSUserId);
			Helper.Dispose(ref this.m_DateOfBirth);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x00029AA6 File Offset: 0x00027CA6
		public void Get(out QueryPermissionsCallbackInfo output)
		{
			output = default(QueryPermissionsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C70 RID: 3184
		private Result m_ResultCode;

		// Token: 0x04000C71 RID: 3185
		private IntPtr m_ClientData;

		// Token: 0x04000C72 RID: 3186
		private IntPtr m_LocalUserId;

		// Token: 0x04000C73 RID: 3187
		private IntPtr m_KWSUserId;

		// Token: 0x04000C74 RID: 3188
		private IntPtr m_DateOfBirth;

		// Token: 0x04000C75 RID: 3189
		private int m_IsMinor;
	}
}
