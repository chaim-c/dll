using System;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x02000011 RID: 17
	public class WidgetAttributeValueTypeBindingPath : WidgetAttributeValueType
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00004438 File Offset: 0x00002638
		public override bool CheckValueType(string value)
		{
			return value.Length > 2 && value[0] == '{' && value[value.Length - 1] == '}';
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004462 File Offset: 0x00002662
		public override string GetAttributeValue(string value)
		{
			return value.Substring(1, value.Length - 2);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004473 File Offset: 0x00002673
		public override string GetSerializedValue(string value)
		{
			return "{" + value + "}";
		}
	}
}
