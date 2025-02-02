using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Abstractions.FluentBuilder;

namespace MCM.Implementation.FluentBuilder
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DefaultSettingsPresetBuilder : ISettingsPresetBuilder
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005E09 File Offset: 0x00004009
		private string Id { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005E11 File Offset: 0x00004011
		private string Name { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005E19 File Offset: 0x00004019
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private IDictionary<string, object> PropertyValues { [return: Nullable(new byte[]
		{
			1,
			1,
			2
		})] get; } = new Dictionary<string, object>();

		// Token: 0x06000110 RID: 272 RVA: 0x00005E21 File Offset: 0x00004021
		public DefaultSettingsPresetBuilder(string id, string name)
		{
			this.Id = id;
			this.Name = name;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005E44 File Offset: 0x00004044
		public ISettingsPresetBuilder SetPropertyValue(string property, [Nullable(2)] object value)
		{
			bool flag = !this.PropertyValues.ContainsKey(property);
			if (flag)
			{
				this.PropertyValues[property] = value;
			}
			return this;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005E78 File Offset: 0x00004078
		public ISettingsPreset Build(BaseSettings settings)
		{
			return new MemorySettingsPreset(settings.Id, this.Id, this.Name, delegate()
			{
				List<ISettingsPropertyDefinition> settingsProperties = settings.GetAllSettingPropertyDefinitions().ToList<ISettingsPropertyDefinition>();
				foreach (KeyValuePair<string, object> overridePropertyKeyValue in this.PropertyValues)
				{
					string overridePropertyId = overridePropertyKeyValue.Key;
					object overridePropertyValue = overridePropertyKeyValue.Value;
					ISettingsPropertyDefinition property = settingsProperties.Find((ISettingsPropertyDefinition x) => x.Id == overridePropertyId);
					bool flag = property != null;
					if (flag)
					{
						property.PropertyReference.Value = overridePropertyValue;
					}
				}
				return settings;
			});
		}
	}
}
