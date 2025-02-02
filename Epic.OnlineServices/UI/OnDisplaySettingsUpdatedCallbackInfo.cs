using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000059 RID: 89
	public struct OnDisplaySettingsUpdatedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x00006758 File Offset: 0x00004958
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00006760 File Offset: 0x00004960
		public object ClientData { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00006769 File Offset: 0x00004969
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00006771 File Offset: 0x00004971
		public bool IsVisible { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000677A File Offset: 0x0000497A
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x00006782 File Offset: 0x00004982
		public bool IsExclusiveInput { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x0000678C File Offset: 0x0000498C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000067A7 File Offset: 0x000049A7
		internal void Set(ref OnDisplaySettingsUpdatedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.IsVisible = other.IsVisible;
			this.IsExclusiveInput = other.IsExclusiveInput;
		}
	}
}
