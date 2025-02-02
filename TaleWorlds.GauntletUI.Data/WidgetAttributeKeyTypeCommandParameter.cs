using System;
using TaleWorlds.GauntletUI.PrefabSystem;

namespace TaleWorlds.GauntletUI.Data
{
	// Token: 0x0200000E RID: 14
	public class WidgetAttributeKeyTypeCommandParameter : WidgetAttributeKeyType
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000043AD File Offset: 0x000025AD
		public override bool CheckKeyType(string key)
		{
			return key.StartsWith("CommandParameter.");
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000043BA File Offset: 0x000025BA
		public override string GetKeyName(string key)
		{
			return key.Substring("CommandParameter.".Length);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000043CC File Offset: 0x000025CC
		public override string GetSerializedKey(string key)
		{
			return "CommandParameter." + key;
		}
	}
}
