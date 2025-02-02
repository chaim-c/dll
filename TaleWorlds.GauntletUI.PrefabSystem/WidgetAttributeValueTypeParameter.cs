using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000016 RID: 22
	public class WidgetAttributeValueTypeParameter : WidgetAttributeValueType
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002F01 File Offset: 0x00001101
		public override bool CheckValueType(string value)
		{
			return value.StartsWith("*");
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002F0E File Offset: 0x0000110E
		public override string GetAttributeValue(string value)
		{
			return value.Substring("*".Length);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002F20 File Offset: 0x00001120
		public override string GetSerializedValue(string value)
		{
			return "*" + value;
		}
	}
}
