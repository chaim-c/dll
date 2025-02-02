using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.Global;

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	public class FluentGlobalSettings : GlobalSettings, IFluentSettings
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000BFEF File Offset: 0x0000A1EF
		public sealed override string Id { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000BFF7 File Offset: 0x0000A1F7
		public sealed override string DisplayName { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000BFFF File Offset: 0x0000A1FF
		public sealed override string FolderName { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000C007 File Offset: 0x0000A207
		public sealed override string SubFolder { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000C00F File Offset: 0x0000A20F
		public sealed override string FormatType { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000C017 File Offset: 0x0000A217
		public override string DiscoveryType
		{
			get
			{
				return "fluent";
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000C01E File Offset: 0x0000A21E
		public sealed override int UIVersion { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000C026 File Offset: 0x0000A226
		public sealed override char SubGroupDelimiter { get; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060003FE RID: 1022 RVA: 0x0000C02E File Offset: 0x0000A22E
		// (remove) Token: 0x060003FF RID: 1023 RVA: 0x0000C038 File Offset: 0x0000A238
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000C042 File Offset: 0x0000A242
		public List<SettingsPropertyGroupDefinition> SettingPropertyGroups { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000C04A File Offset: 0x0000A24A
		private List<ISettingsPresetBuilder> Presets { get; }

		// Token: 0x06000402 RID: 1026 RVA: 0x0000C054 File Offset: 0x0000A254
		public FluentGlobalSettings(string id, string displayName, string folderName, string subFolder, string format, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets)
		{
			this.Id = id;
			this.DisplayName = displayName;
			this.FolderName = folderName;
			this.SubFolder = subFolder;
			this.FormatType = format;
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

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C0CC File Offset: 0x0000A2CC
		public void Register()
		{
			IEnumerable<IFluentGlobalSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentGlobalSettingsContainer>>() ?? Enumerable.Empty<IFluentGlobalSettingsContainer>();
			foreach (IFluentGlobalSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Register(this);
				}
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000C130 File Offset: 0x0000A330
		public void Unregister()
		{
			IEnumerable<IFluentGlobalSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentGlobalSettingsContainer>>() ?? Enumerable.Empty<IFluentGlobalSettingsContainer>();
			foreach (IFluentGlobalSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Unregister(this);
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000C194 File Offset: 0x0000A394
		public sealed override BaseSettings CreateNew()
		{
			return new FluentGlobalSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.FormatType, this.UIVersion, this.SubGroupDelimiter, null, from g in this.SettingPropertyGroups
			select g.Clone(false), this.Presets);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000C201 File Offset: 0x0000A401
		public sealed override IEnumerable<ISettingsPreset> GetBuiltInPresets()
		{
			return from presetBuilder in this.Presets
			select presetBuilder.Build(this.CreateNew());
		}
	}
}
