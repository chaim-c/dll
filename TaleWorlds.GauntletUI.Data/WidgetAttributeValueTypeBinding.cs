using System;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000010 RID: 16
	public class WidgetAttributeValueTypeBinding : WidgetAttributeValueType
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004404 File Offset: 0x00002604
		public override bool CheckValueType(string value)
		{
			return value.StartsWith("@");
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004411 File Offset: 0x00002611
		public override string GetAttributeValue(string value)
		{
			return value.Substring("@".Length);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004423 File Offset: 0x00002623
		public override string GetSerializedValue(string value)
		{
			return "@" + value;
		}
	}
}
