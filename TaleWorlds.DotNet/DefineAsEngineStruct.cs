using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public class DefineAsEngineStruct : Attribute
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002864 File Offset: 0x00000A64
		public Type Type { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000286D File Offset: 0x00000A6D
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002875 File Offset: 0x00000A75
		public string EngineType { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000287E File Offset: 0x00000A7E
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002886 File Offset: 0x00000A86
		public bool IgnoreMemberOffsetTest { get; set; }

		// Token: 0x06000031 RID: 49 RVA: 0x0000288F File Offset: 0x00000A8F
		public DefineAsEngineStruct(Type type, string engineType, bool ignoreMemberOffsetTest = false)
		{
			this.Type = type;
			this.EngineType = engineType;
			this.IgnoreMemberOffsetTest = ignoreMemberOffsetTest;
		}
	}
}
