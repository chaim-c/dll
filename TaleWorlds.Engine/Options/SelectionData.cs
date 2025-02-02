using System;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x020000A0 RID: 160
	public struct SelectionData
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x0000D347 File Offset: 0x0000B547
		public SelectionData(bool isLocalizationId, string data)
		{
			this.IsLocalizationId = isLocalizationId;
			this.Data = data;
		}

		// Token: 0x040001FE RID: 510
		public bool IsLocalizationId;

		// Token: 0x040001FF RID: 511
		public string Data;
	}
}
