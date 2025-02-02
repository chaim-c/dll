using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005A RID: 90
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnDisplaySettingsUpdatedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnDisplaySettingsUpdatedCallbackInfo>, ISettable<OnDisplaySettingsUpdatedCallbackInfo>, IDisposable
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000067D4 File Offset: 0x000049D4
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x000067F5 File Offset: 0x000049F5
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x00006808 File Offset: 0x00004A08
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00006820 File Offset: 0x00004A20
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00006841 File Offset: 0x00004A41
		public bool IsVisible
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsVisible, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsVisible);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00006854 File Offset: 0x00004A54
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00006875 File Offset: 0x00004A75
		public bool IsExclusiveInput
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsExclusiveInput, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsExclusiveInput);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00006885 File Offset: 0x00004A85
		public void Set(ref OnDisplaySettingsUpdatedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.IsVisible = other.IsVisible;
			this.IsExclusiveInput = other.IsExclusiveInput;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000068B0 File Offset: 0x00004AB0
		public void Set(ref OnDisplaySettingsUpdatedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.IsVisible = other.Value.IsVisible;
				this.IsExclusiveInput = other.Value.IsExclusiveInput;
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00006909 File Offset: 0x00004B09
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00006918 File Offset: 0x00004B18
		public void Get(out OnDisplaySettingsUpdatedCallbackInfo output)
		{
			output = default(OnDisplaySettingsUpdatedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400023B RID: 571
		private IntPtr m_ClientData;

		// Token: 0x0400023C RID: 572
		private int m_IsVisible;

		// Token: 0x0400023D RID: 573
		private int m_IsExclusiveInput;
	}
}
