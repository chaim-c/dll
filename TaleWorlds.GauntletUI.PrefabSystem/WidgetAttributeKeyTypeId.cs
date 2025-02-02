using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000010 RID: 16
	public class WidgetAttributeKeyTypeId : WidgetAttributeKeyType
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00002E11 File Offset: 0x00001011
		public override bool CheckKeyType(string key)
		{
			return key == "Id";
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E1E File Offset: 0x0000101E
		public override string GetKeyName(string key)
		{
			return "Id";
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E25 File Offset: 0x00001025
		public override string GetSerializedKey(string key)
		{
			return "Id";
		}
	}
}
