using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000011 RID: 17
	public class WidgetAttributeKeyTypeParameter : WidgetAttributeKeyType
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00002E34 File Offset: 0x00001034
		public override bool CheckKeyType(string key)
		{
			return key.StartsWith("Parameter.");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002E41 File Offset: 0x00001041
		public override string GetKeyName(string key)
		{
			return key.Substring("Parameter.".Length);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E53 File Offset: 0x00001053
		public override string GetSerializedKey(string key)
		{
			return "Parameter." + key;
		}
	}
}
