using System;
using System.Collections.Generic;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x0200000C RID: 12
	public class VisualStateTemplate
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B68 File Offset: 0x00000D68
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B70 File Offset: 0x00000D70
		public string State { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002B79 File Offset: 0x00000D79
		public VisualStateTemplate()
		{
			this._attributes = new Dictionary<string, string>();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B8C File Offset: 0x00000D8C
		public void SetAttribute(string name, string value)
		{
			if (this._attributes.ContainsKey(name))
			{
				this._attributes[name] = value;
				return;
			}
			this._attributes.Add(name, value);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BB7 File Offset: 0x00000DB7
		public Dictionary<string, string> GetAttributes()
		{
			return this._attributes;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BBF File Offset: 0x00000DBF
		public void ClearAttribute(string name)
		{
			if (this._attributes.ContainsKey(name))
			{
				this._attributes.Remove(name);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BDC File Offset: 0x00000DDC
		public VisualState CreateVisualState(BrushFactory brushFactory, SpriteData spriteData, Dictionary<string, VisualDefinitionTemplate> visualDefinitionTemplates, Dictionary<string, ConstantDefinition> constants, Dictionary<string, WidgetAttributeTemplate> parameters, Dictionary<string, string> defaultParameters)
		{
			VisualState visualState = new VisualState(this.State);
			foreach (KeyValuePair<string, string> keyValuePair in this._attributes)
			{
				string key = keyValuePair.Key;
				string actualValueOf = ConstantDefinition.GetActualValueOf(keyValuePair.Value, brushFactory, spriteData, constants, parameters, defaultParameters);
				WidgetExtensions.SetWidgetAttributeFromString(visualState, key, actualValueOf, brushFactory, spriteData, visualDefinitionTemplates, constants, parameters, null, defaultParameters);
			}
			return visualState;
		}

		// Token: 0x04000025 RID: 37
		private Dictionary<string, string> _attributes;
	}
}
