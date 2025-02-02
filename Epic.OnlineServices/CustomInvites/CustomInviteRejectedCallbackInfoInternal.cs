using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F7 RID: 1271
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CustomInviteRejectedCallbackInfoInternal : ICallbackInfoInternal, IGettable<CustomInviteRejectedCallbackInfo>, ISettable<CustomInviteRejectedCallbackInfo>, IDisposable
	{
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x00030598 File Offset: 0x0002E798
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x000305B9 File Offset: 0x0002E7B9
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x000305CC File Offset: 0x0002E7CC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x000305E4 File Offset: 0x0002E7E4
		// (set) Token: 0x06002099 RID: 8345 RVA: 0x00030605 File Offset: 0x0002E805
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00030618 File Offset: 0x0002E818
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x00030639 File Offset: 0x0002E839
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

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x0003064C File Offset: 0x0002E84C
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x0003066D File Offset: 0x0002E86D
		public Utf8String CustomInviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CustomInviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CustomInviteId);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00030680 File Offset: 0x0002E880
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x000306A1 File Offset: 0x0002E8A1
		public Utf8String Payload
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Payload, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Payload);
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000306B4 File Offset: 0x0002E8B4
		public void Set(ref CustomInviteRejectedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x00030704 File Offset: 0x0002E904
		public void Set(ref CustomInviteRejectedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
				this.CustomInviteId = other.Value.CustomInviteId;
				this.Payload = other.Value.Payload;
			}
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00030787 File Offset: 0x0002E987
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_CustomInviteId);
			Helper.Dispose(ref this.m_Payload);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000307C6 File Offset: 0x0002E9C6
		public void Get(out CustomInviteRejectedCallbackInfo output)
		{
			output = default(CustomInviteRejectedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E87 RID: 3719
		private IntPtr m_ClientData;

		// Token: 0x04000E88 RID: 3720
		private IntPtr m_TargetUserId;

		// Token: 0x04000E89 RID: 3721
		private IntPtr m_LocalUserId;

		// Token: 0x04000E8A RID: 3722
		private IntPtr m_CustomInviteId;

		// Token: 0x04000E8B RID: 3723
		private IntPtr m_Payload;
	}
}
