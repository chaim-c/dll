using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200042A RID: 1066
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserCallbackInfoInternal : ICallbackInfoInternal, IGettable<CreateUserCallbackInfo>, ISettable<CreateUserCallbackInfo>, IDisposable
	{
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000289A8 File Offset: 0x00026BA8
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x000289C0 File Offset: 0x00026BC0
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

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x000289CC File Offset: 0x00026BCC
		// (set) Token: 0x06001B72 RID: 7026 RVA: 0x000289ED File Offset: 0x00026BED
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00028A00 File Offset: 0x00026C00
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x00028A18 File Offset: 0x00026C18
		// (set) Token: 0x06001B75 RID: 7029 RVA: 0x00028A39 File Offset: 0x00026C39
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

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x00028A4C File Offset: 0x00026C4C
		// (set) Token: 0x06001B77 RID: 7031 RVA: 0x00028A6D File Offset: 0x00026C6D
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

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00028A80 File Offset: 0x00026C80
		// (set) Token: 0x06001B79 RID: 7033 RVA: 0x00028AA1 File Offset: 0x00026CA1
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

		// Token: 0x06001B7A RID: 7034 RVA: 0x00028AB4 File Offset: 0x00026CB4
		public void Set(ref CreateUserCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.KWSUserId = other.KWSUserId;
			this.IsMinor = other.IsMinor;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x00028B04 File Offset: 0x00026D04
		public void Set(ref CreateUserCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.KWSUserId = other.Value.KWSUserId;
				this.IsMinor = other.Value.IsMinor;
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x00028B87 File Offset: 0x00026D87
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_KWSUserId);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x00028BAE File Offset: 0x00026DAE
		public void Get(out CreateUserCallbackInfo output)
		{
			output = default(CreateUserCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C34 RID: 3124
		private Result m_ResultCode;

		// Token: 0x04000C35 RID: 3125
		private IntPtr m_ClientData;

		// Token: 0x04000C36 RID: 3126
		private IntPtr m_LocalUserId;

		// Token: 0x04000C37 RID: 3127
		private IntPtr m_KWSUserId;

		// Token: 0x04000C38 RID: 3128
		private int m_IsMinor;
	}
}
