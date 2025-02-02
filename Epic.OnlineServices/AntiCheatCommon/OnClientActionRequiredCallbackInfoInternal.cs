using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005FF RID: 1535
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnClientActionRequiredCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnClientActionRequiredCallbackInfo>, ISettable<OnClientActionRequiredCallbackInfo>, IDisposable
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0003AA28 File Offset: 0x00038C28
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x0003AA49 File Offset: 0x00038C49
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

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0003AA5C File Offset: 0x00038C5C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x0003AA74 File Offset: 0x00038C74
		// (set) Token: 0x0600275B RID: 10075 RVA: 0x0003AA8C File Offset: 0x00038C8C
		public IntPtr ClientHandle
		{
			get
			{
				return this.m_ClientHandle;
			}
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0003AA98 File Offset: 0x00038C98
		// (set) Token: 0x0600275D RID: 10077 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		public AntiCheatCommonClientAction ClientAction
		{
			get
			{
				return this.m_ClientAction;
			}
			set
			{
				this.m_ClientAction = value;
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0003AABC File Offset: 0x00038CBC
		// (set) Token: 0x0600275F RID: 10079 RVA: 0x0003AAD4 File Offset: 0x00038CD4
		public AntiCheatCommonClientActionReason ActionReasonCode
		{
			get
			{
				return this.m_ActionReasonCode;
			}
			set
			{
				this.m_ActionReasonCode = value;
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x0003AAE0 File Offset: 0x00038CE0
		// (set) Token: 0x06002761 RID: 10081 RVA: 0x0003AB01 File Offset: 0x00038D01
		public Utf8String ActionReasonDetailsString
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_ActionReasonDetailsString, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ActionReasonDetailsString);
			}
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x0003AB14 File Offset: 0x00038D14
		public void Set(ref OnClientActionRequiredCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.ClientAction = other.ClientAction;
			this.ActionReasonCode = other.ActionReasonCode;
			this.ActionReasonDetailsString = other.ActionReasonDetailsString;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x0003AB64 File Offset: 0x00038D64
		public void Set(ref OnClientActionRequiredCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientAction = other.Value.ClientAction;
				this.ActionReasonCode = other.Value.ActionReasonCode;
				this.ActionReasonDetailsString = other.Value.ActionReasonDetailsString;
			}
		}

		// Token: 0x06002764 RID: 10084 RVA: 0x0003ABE7 File Offset: 0x00038DE7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_ActionReasonDetailsString);
		}

		// Token: 0x06002765 RID: 10085 RVA: 0x0003AC0E File Offset: 0x00038E0E
		public void Get(out OnClientActionRequiredCallbackInfo output)
		{
			output = default(OnClientActionRequiredCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040011B4 RID: 4532
		private IntPtr m_ClientData;

		// Token: 0x040011B5 RID: 4533
		private IntPtr m_ClientHandle;

		// Token: 0x040011B6 RID: 4534
		private AntiCheatCommonClientAction m_ClientAction;

		// Token: 0x040011B7 RID: 4535
		private AntiCheatCommonClientActionReason m_ActionReasonCode;

		// Token: 0x040011B8 RID: 4536
		private IntPtr m_ActionReasonDetailsString;
	}
}
