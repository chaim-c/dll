using System;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x0200000F RID: 15
	public class WidgetAttributeKeyTypeDataSource : WidgetAttributeKeyType
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000043E1 File Offset: 0x000025E1
		public override bool CheckKeyType(string key)
		{
			return key == "DataSource";
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000043EE File Offset: 0x000025EE
		public override string GetKeyName(string key)
		{
			return "DataSource";
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043F5 File Offset: 0x000025F5
		public override string GetSerializedKey(string key)
		{
			return "DataSource";
		}
	}
}
