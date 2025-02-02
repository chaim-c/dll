using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000013 RID: 19
	public class CustomEngineStructMemberData : Attribute
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DD5 File Offset: 0x00000FD5
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002DDD File Offset: 0x00000FDD
		public string CustomMemberName { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DE6 File Offset: 0x00000FE6
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002DEE File Offset: 0x00000FEE
		public bool IgnoreMemberOffsetTest { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DF7 File Offset: 0x00000FF7
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002DFF File Offset: 0x00000FFF
		public bool PublicPrivateModifierFlippedInNative { get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002E08 File Offset: 0x00001008
		public CustomEngineStructMemberData(string customMemberName)
		{
			this.CustomMemberName = customMemberName;
			this.IgnoreMemberOffsetTest = false;
			this.PublicPrivateModifierFlippedInNative = false;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E25 File Offset: 0x00001025
		public CustomEngineStructMemberData(string customMemberName, bool ignoreMemberOffsetTest)
		{
			this.CustomMemberName = customMemberName;
			this.IgnoreMemberOffsetTest = ignoreMemberOffsetTest;
			this.PublicPrivateModifierFlippedInNative = false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E42 File Offset: 0x00001042
		public CustomEngineStructMemberData(bool publicPrivateModifierFlippedInNative)
		{
			this.CustomMemberName = null;
			this.IgnoreMemberOffsetTest = false;
			this.PublicPrivateModifierFlippedInNative = publicPrivateModifierFlippedInNative;
		}
	}
}
