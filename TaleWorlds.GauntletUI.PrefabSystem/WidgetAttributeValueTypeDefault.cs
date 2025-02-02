using System;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x02000015 RID: 21
	public class WidgetAttributeValueTypeDefault : WidgetAttributeValueType
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002EF0 File Offset: 0x000010F0
		public override bool CheckValueType(string value)
		{
			return true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002EF3 File Offset: 0x000010F3
		public override string GetAttributeValue(string value)
		{
			return value;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002EF6 File Offset: 0x000010F6
		public override string GetSerializedValue(string value)
		{
			return value;
		}
	}
}
