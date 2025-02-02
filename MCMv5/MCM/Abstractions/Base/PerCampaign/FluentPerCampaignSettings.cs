using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.PerCampaign;

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B8 RID: 184
	[NullableContext(1)]
	[Nullable(0)]
	public class FluentPerCampaignSettings : PerCampaignSettings, IFluentSettings
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000BA28 File Offset: 0x00009C28
		public sealed override string Id { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000BA30 File Offset: 0x00009C30
		public sealed override string DisplayName { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000BA38 File Offset: 0x00009C38
		public sealed override string FolderName { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000BA40 File Offset: 0x00009C40
		public sealed override string SubFolder { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000BA48 File Offset: 0x00009C48
		public override string DiscoveryType
		{
			get
			{
				return "fluent";
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000BA4F File Offset: 0x00009C4F
		public sealed override int UIVersion { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000BA57 File Offset: 0x00009C57
		public sealed override char SubGroupDelimiter { get; }

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060003D6 RID: 982 RVA: 0x0000BA5F File Offset: 0x00009C5F
		// (remove) Token: 0x060003D7 RID: 983 RVA: 0x0000BA69 File Offset: 0x00009C69
		[Nullable(2)]
		public sealed override event PropertyChangedEventHandler PropertyChanged
		{
			[NullableContext(2)]
			add
			{
				base.PropertyChanged += value;
			}
			[NullableContext(2)]
			remove
			{
				base.PropertyChanged -= value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000BA73 File Offset: 0x00009C73
		public List<SettingsPropertyGroupDefinition> SettingPropertyGroups { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000BA7B File Offset: 0x00009C7B
		private List<ISettingsPresetBuilder> Presets { get; }

		// Token: 0x060003DA RID: 986 RVA: 0x0000BA84 File Offset: 0x00009C84
		public FluentPerCampaignSettings(string id, string displayName, string folderName, string subFolder, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets)
		{
			this.Id = id;
			this.DisplayName = displayName;
			this.FolderName = folderName;
			this.SubFolder = subFolder;
			this.UIVersion = uiVersion;
			this.SubGroupDelimiter = subGroupDelimiter;
			this.SettingPropertyGroups = settingPropertyGroups.ToList<SettingsPropertyGroupDefinition>();
			this.Presets = presets.ToList<ISettingsPresetBuilder>();
			bool flag = onPropertyChanged != null;
			if (flag)
			{
				this.PropertyChanged += onPropertyChanged;
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		public void Register()
		{
			IEnumerable<IFluentPerCampaignSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentPerCampaignSettingsContainer>>() ?? Enumerable.Empty<IFluentPerCampaignSettingsContainer>();
			foreach (IFluentPerCampaignSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Register(this);
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000BB58 File Offset: 0x00009D58
		public void Unregister()
		{
			IEnumerable<IFluentPerCampaignSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentPerCampaignSettingsContainer>>() ?? Enumerable.Empty<IFluentPerCampaignSettingsContainer>();
			foreach (IFluentPerCampaignSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Unregister(this);
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public override BaseSettings CreateNew()
		{
			return new FluentPerCampaignSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.UIVersion, this.SubGroupDelimiter, null, from g in this.SettingPropertyGroups
			select g.Clone(false), this.Presets);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000BC23 File Offset: 0x00009E23
		public sealed override IEnumerable<ISettingsPreset> GetBuiltInPresets()
		{
			return from presetBuilder in this.Presets
			select presetBuilder.Build(this.CreateNew());
		}
	}
}
