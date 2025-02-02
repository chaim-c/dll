using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.PerSave;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000B1 RID: 177
	[NullableContext(1)]
	[Nullable(0)]
	public class FluentPerSaveSettings : PerSaveSettings, IFluentSettings
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000B45C File Offset: 0x0000965C
		public sealed override string Id { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B464 File Offset: 0x00009664
		public sealed override string DisplayName { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000B46C File Offset: 0x0000966C
		public sealed override string FolderName { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000B474 File Offset: 0x00009674
		public sealed override string SubFolder { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000B47C File Offset: 0x0000967C
		public override string DiscoveryType
		{
			get
			{
				return "fluent";
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000B483 File Offset: 0x00009683
		public sealed override int UIVersion { get; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000B48B File Offset: 0x0000968B
		public sealed override char SubGroupDelimiter { get; }

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060003AC RID: 940 RVA: 0x0000B493 File Offset: 0x00009693
		// (remove) Token: 0x060003AD RID: 941 RVA: 0x0000B49D File Offset: 0x0000969D
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000B4A7 File Offset: 0x000096A7
		public List<SettingsPropertyGroupDefinition> SettingPropertyGroups { get; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000B4AF File Offset: 0x000096AF
		private List<ISettingsPresetBuilder> Presets { get; }

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public FluentPerSaveSettings(string id, string displayName, string folderName, string subFolder, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets)
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

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B528 File Offset: 0x00009728
		public void Register()
		{
			IEnumerable<IFluentPerSaveSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentPerSaveSettingsContainer>>() ?? Enumerable.Empty<IFluentPerSaveSettingsContainer>();
			foreach (IFluentPerSaveSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Register(this);
				}
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B58C File Offset: 0x0000978C
		public void Unregister()
		{
			IEnumerable<IFluentPerSaveSettingsContainer> containers = GenericServiceProvider.GetService<IEnumerable<IFluentPerSaveSettingsContainer>>() ?? Enumerable.Empty<IFluentPerSaveSettingsContainer>();
			foreach (IFluentPerSaveSettingsContainer container in containers)
			{
				if (container != null)
				{
					container.Unregister(this);
				}
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public override BaseSettings CreateNew()
		{
			return new FluentPerSaveSettings(this.Id, this.DisplayName, this.FolderName, this.SubFolder, this.UIVersion, this.SubGroupDelimiter, null, from g in this.SettingPropertyGroups
			select g.Clone(false), this.Presets);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000B657 File Offset: 0x00009857
		public sealed override IEnumerable<ISettingsPreset> GetBuiltInPresets()
		{
			return from presetBuilder in this.Presets
			select presetBuilder.Build(this.CreateNew());
		}
	}
}
