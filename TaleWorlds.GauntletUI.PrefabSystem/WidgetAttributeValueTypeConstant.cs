using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000014 RID: 20
	public class WidgetAttributeValueTypeConstant : WidgetAttributeValueType
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002EBC File Offset: 0x000010BC
		public override bool CheckValueType(string value)
		{
			return value.StartsWith("!");
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002EC9 File Offset: 0x000010C9
		public override string GetAttributeValue(string value)
		{
			return value.Substring("!".Length);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002EDB File Offset: 0x000010DB
		public override string GetSerializedValue(string value)
		{
			return "!" + value;
		}
	}
}
