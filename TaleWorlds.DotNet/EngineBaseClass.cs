using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200000F RID: 15
	public abstract class EngineBaseClass : Attribute
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002DA4 File Offset: 0x00000FA4
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00002DAC File Offset: 0x00000FAC
		public string EngineType { get; set; }

		// Token: 0x06000043 RID: 67 RVA: 0x00002DB5 File Offset: 0x00000FB5
		protected EngineBaseClass(string engineType)
		{
			this.EngineType = engineType;
		}
	}
}
