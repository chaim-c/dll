using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200023D RID: 573
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinGameAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<JoinGameAcceptedCallbackInfo>, ISettable<JoinGameAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00017824 File Offset: 0x00015A24
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00017845 File Offset: 0x00015A45
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

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00017858 File Offset: 0x00015A58
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00017870 File Offset: 0x00015A70
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00017891 File Offset: 0x00015A91
		public Utf8String JoinInfo
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_JoinInfo, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_JoinInfo);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x000178A4 File Offset: 0x00015AA4
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x000178C5 File Offset: 0x00015AC5
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x000178D8 File Offset: 0x00015AD8
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x000178F9 File Offset: 0x00015AF9
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0001790C File Offset: 0x00015B0C
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00017924 File Offset: 0x00015B24
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

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00017930 File Offset: 0x00015B30
		public void Set(ref JoinGameAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.JoinInfo = other.JoinInfo;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.UiEventId = other.UiEventId;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00017980 File Offset: 0x00015B80
		public void Set(ref JoinGameAcceptedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.JoinInfo = other.Value.JoinInfo;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00017A03 File Offset: 0x00015C03
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_JoinInfo);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00017A36 File Offset: 0x00015C36
		public void Get(out JoinGameAcceptedCallbackInfo output)
		{
			output = default(JoinGameAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400071C RID: 1820
		private IntPtr m_ClientData;

		// Token: 0x0400071D RID: 1821
		private IntPtr m_JoinInfo;

		// Token: 0x0400071E RID: 1822
		private IntPtr m_LocalUserId;

		// Token: 0x0400071F RID: 1823
		private IntPtr m_TargetUserId;

		// Token: 0x04000720 RID: 1824
		private ulong m_UiEventId;
	}
}
