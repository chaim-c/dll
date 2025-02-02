using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MCM.Abstractions;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Base.PerCampaign;
using MCM.Abstractions.Base.PerSave;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.Wrapper;

namespace MCM.Implementation.FluentBuilder
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DefaultSettingsBuilder : BaseSettingsBuilder
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000058DA File Offset: 0x00003ADA
		private Dictionary<string, ISettingsPropertyGroupBuilder> PropertyGroups { get; } = new Dictionary<string, ISettingsPropertyGroupBuilder>();

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000058E2 File Offset: 0x00003AE2
		private Dictionary<string, ISettingsPresetBuilder> Presets { get; } = new Dictionary<string, ISettingsPresetBuilder>();

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000058EA File Offset: 0x00003AEA
		private string Id { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000058F2 File Offset: 0x00003AF2
		private string DisplayName { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000058FA File Offset: 0x00003AFA
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005902 File Offset: 0x00003B02
		private string FolderName { get; set; } = string.Empty;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000590B File Offset: 0x00003B0B
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005913 File Offset: 0x00003B13
		private string SubFolder { get; set; } = string.Empty;

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000591C File Offset: 0x00003B1C
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00005924 File Offset: 0x00003B24
		private string Format { get; set; } = "memory";

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000592D File Offset: 0x00003B2D
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00005935 File Offset: 0x00003B35
		private int UIVersion { get; set; } = 1;

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000593E File Offset: 0x00003B3E
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005946 File Offset: 0x00003B46
		private char SubGroupDelimiter { get; set; } = '/';

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000594F File Offset: 0x00003B4F
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005957 File Offset: 0x00003B57
		[Nullable(2)]
		private PropertyChangedEventHandler OnPropertyChanged { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x060000FC RID: 252 RVA: 0x00005960 File Offset: 0x00003B60
		public DefaultSettingsBuilder(string id, string displayName)
		{
			this.Id = id;
			this.DisplayName = displayName;
			this.CreateGroup(SettingsPropertyGroupDefinition.DefaultGroupName, delegate(ISettingsPropertyGroupBuilder _)
			{
			});
			this.CreatePreset("default", "{=BaseSettings_Default}Default", delegate(ISettingsPresetBuilder _)
			{
			});
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005A24 File Offset: 0x00003C24
		public override ISettingsBuilder SetFolderName(string value)
		{
			this.FolderName = value;
			return this;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A40 File Offset: 0x00003C40
		public override ISettingsBuilder SetSubFolder(string value)
		{
			this.SubFolder = value;
			return this;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005A5C File Offset: 0x00003C5C
		public override ISettingsBuilder SetFormat(string value)
		{
			this.Format = value;
			return this;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005A78 File Offset: 0x00003C78
		public override ISettingsBuilder SetUIVersion(int value)
		{
			this.UIVersion = value;
			return this;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005A94 File Offset: 0x00003C94
		public override ISettingsBuilder SetSubGroupDelimiter(char value)
		{
			this.SubGroupDelimiter = value;
			return this;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005AB0 File Offset: 0x00003CB0
		public override ISettingsBuilder SetOnPropertyChanged(PropertyChangedEventHandler value)
		{
			this.OnPropertyChanged = value;
			return this;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005ACC File Offset: 0x00003CCC
		public override ISettingsBuilder CreateGroup(string name, Action<ISettingsPropertyGroupBuilder> builder)
		{
			bool flag = !this.PropertyGroups.ContainsKey(name);
			if (flag)
			{
				this.PropertyGroups[name] = new DefaultSettingsPropertyGroupBuilder(name);
			}
			builder(this.PropertyGroups[name]);
			return this;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005B18 File Offset: 0x00003D18
		public override ISettingsBuilder CreatePreset(string id, string name, Action<ISettingsPresetBuilder> builder)
		{
			bool flag = !this.Presets.ContainsKey(id);
			if (flag)
			{
				this.Presets[id] = new DefaultSettingsPresetBuilder(id, name);
			}
			builder(this.Presets[id]);
			return this;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005B68 File Offset: 0x00003D68
		public override ISettingsBuilder WithoutDefaultPreset()
		{
			this.Presets.Remove("default");
			return this;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005B8C File Offset: 0x00003D8C
		public override FluentGlobalSettings BuildAsGlobal()
		{
			ISettingsPresetBuilder preset;
			bool flag = this.Presets.TryGetValue("default", out preset);
			if (flag)
			{
				foreach (SettingsPropertyDefinition property in this.GetSettingProperties())
				{
					preset.SetPropertyValue(property.Id, property.PropertyReference.Value);
				}
			}
			return new FluentGlobalSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.Format, this.UIVersion, this.SubGroupDelimiter, this.OnPropertyChanged, this.GetSettingPropertyGroups(), this.Presets.Values);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005C54 File Offset: 0x00003E54
		public override FluentPerSaveSettings BuildAsPerSave()
		{
			ISettingsPresetBuilder preset;
			bool flag = this.Presets.TryGetValue("default", out preset);
			if (flag)
			{
				foreach (SettingsPropertyDefinition property in this.GetSettingProperties())
				{
					preset.SetPropertyValue(property.Id, property.PropertyReference.Value);
				}
			}
			return new FluentPerSaveSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.UIVersion, this.SubGroupDelimiter, this.OnPropertyChanged, this.GetSettingPropertyGroups(), this.Presets.Values);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005D14 File Offset: 0x00003F14
		public override FluentPerCampaignSettings BuildAsPerCampaign()
		{
			ISettingsPresetBuilder preset;
			bool flag = this.Presets.TryGetValue("default", out preset);
			if (flag)
			{
				foreach (SettingsPropertyDefinition property in this.GetSettingProperties())
				{
					preset.SetPropertyValue(property.Id, property.PropertyReference.Value);
				}
			}
			return new FluentPerCampaignSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.UIVersion, this.SubGroupDelimiter, this.OnPropertyChanged, this.GetSettingPropertyGroups(), this.Presets.Values);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DD4 File Offset: 0x00003FD4
		private IEnumerable<SettingsPropertyGroupDefinition> GetSettingPropertyGroups()
		{
			return SettingsUtils.GetSettingsPropertyGroups(this.SubGroupDelimiter, this.GetSettingProperties());
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005DE7 File Offset: 0x00003FE7
		private IEnumerable<SettingsPropertyDefinition> GetSettingProperties()
		{
			foreach (ISettingsPropertyGroupBuilder settingsPropertyGroup in this.PropertyGroups.Values)
			{
				foreach (ISettingsPropertyBuilder settingsProperty in settingsPropertyGroup.Properties.Values)
				{
					yield return new SettingsPropertyDefinition(settingsProperty.GetDefinitions(), new PropertyGroupDefinitionWrapper(settingsPropertyGroup.GetPropertyGroupDefinition()), settingsProperty.PropertyReference, this.SubGroupDelimiter);
					settingsProperty = null;
				}
				Dictionary<string, ISettingsPropertyBuilder>.ValueCollection.Enumerator enumerator2 = default(Dictionary<string, ISettingsPropertyBuilder>.ValueCollection.Enumerator);
				settingsPropertyGroup = null;
			}
			Dictionary<string, ISettingsPropertyGroupBuilder>.ValueCollection.Enumerator enumerator = default(Dictionary<string, ISettingsPropertyGroupBuilder>.ValueCollection.Enumerator);
			yield break;
			yield break;
		}
	}
}
