using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000508 RID: 1288
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendCustomInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<SendCustomInviteCallbackInfo>, ISettable<SendCustomInviteCallbackInfo>, IDisposable
	{
		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000312E4 File Offset: 0x0002F4E4
		// (set) Token: 0x06002123 RID: 8483 RVA: 0x000312FC File Offset: 0x0002F4FC
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

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x00031308 File Offset: 0x0002F508
		// (set) Token: 0x06002125 RID: 8485 RVA: 0x00031329 File Offset: 0x0002F529
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

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002126 RID: 8486 RVA: 0x0003133C File Offset: 0x0002F53C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00031354 File Offset: 0x0002F554
		// (set) Token: 0x06002128 RID: 8488 RVA: 0x00031375 File Offset: 0x0002F575
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

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002129 RID: 8489 RVA: 0x00031388 File Offset: 0x0002F588
		// (set) Token: 0x0600212A RID: 8490 RVA: 0x000313AF File Offset: 0x0002F5AF
		public ProductUserId[] TargetUserIds
		{
			get
			{
				ProductUserId[] result;
				Helper.GetHandle<ProductUserId>(this.m_TargetUserIds, out result, this.m_TargetUserIdsCount);
				return result;
			}
			set
			{
				Helper.Set<ProductUserId>(value, ref this.m_TargetUserIds, out this.m_TargetUserIdsCount);
			}
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000313C5 File Offset: 0x0002F5C5
		public void Set(ref SendCustomInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserIds = other.TargetUserIds;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x000313FC File Offset: 0x0002F5FC
		public void Set(ref SendCustomInviteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserIds = other.Value.TargetUserIds;
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0003146A File Offset: 0x0002F66A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserIds);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x00031491 File Offset: 0x0002F691
		public void Get(out SendCustomInviteCallbackInfo output)
		{
			output = default(SendCustomInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000EB4 RID: 3764
		private Result m_ResultCode;

		// Token: 0x04000EB5 RID: 3765
		private IntPtr m_ClientData;

		// Token: 0x04000EB6 RID: 3766
		private IntPtr m_LocalUserId;

		// Token: 0x04000EB7 RID: 3767
		private IntPtr m_TargetUserIds;

		// Token: 0x04000EB8 RID: 3768
		private uint m_TargetUserIdsCount;
	}
}
