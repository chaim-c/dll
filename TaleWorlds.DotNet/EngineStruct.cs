using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000014 RID: 20
	public class EngineStruct : Attribute
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002E5F File Offset: 0x0000105F
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002E67 File Offset: 0x00001067
		public string EngineType { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002E78 File Offset: 0x00001078
		public string AlternateDotNetType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002E89 File Offset: 0x00001089
		public bool IgnoreMemberOffsetTest { get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x00002E92 File Offset: 0x00001092
		public EngineStruct(string engineType, bool ignoreMemberOffsetTest = false)
		{
			this.EngineType = engineType;
			this.AlternateDotNetType = null;
			this.IgnoreMemberOffsetTest = ignoreMemberOffsetTest;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002EAF File Offset: 0x000010AF
		public EngineStruct(string engineType, string alternateDotNetType, bool ignoreMemberOffsetTest = false)
		{
			this.EngineType = engineType;
			this.AlternateDotNetType = alternateDotNetType;
			this.IgnoreMemberOffsetTest = ignoreMemberOffsetTest;
		}
	}
}
