using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using MCM.Abstractions.FluentBuilder;
using MCM.Abstractions.Utils;
using MCM.Abstractions.Xml;
using MCM.Common;

namespace MCM.Abstractions.Base.PerSave
{
	// Token: 0x020000B0 RID: 176
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExternalPerSaveSettings : FluentPerSaveSettings
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x0000B378 File Offset: 0x00009578
		[return: Nullable(2)]
		public static ExternalPerSaveSettings CreateFromXmlStream(Stream xmlStream, Func<IPropertyDefinitionBase, IRef> assignRefDelegate, [Nullable(2)] PropertyChangedEventHandler propertyChanged = null)
		{
			SettingsXmlModel settingsXmlModel = SerializationUtils.DeserializeXml<SettingsXmlModel>(xmlStream);
			bool flag = settingsXmlModel == null;
			ExternalPerSaveSettings result;
			if (flag)
			{
				result = null;
			}
			else
			{
				char subGroupDelimiter = settingsXmlModel.SubGroupDelimiter[0];
				IEnumerable<SettingsPropertyDefinition> props = settingsXmlModel.Groups.SelectMany((PropertyGroupXmlModel g) => from p in g.Properties
				select new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(p), g, assignRefDelegate(p), subGroupDelimiter)).Concat(from p in settingsXmlModel.Properties
				select new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(p), SettingsPropertyGroupDefinition.DefaultGroup, assignRefDelegate(p), subGroupDelimiter));
				List<SettingsPropertyGroupDefinition> propGroups = SettingsUtils.GetSettingsPropertyGroups(subGroupDelimiter, props);
				result = new ExternalPerSaveSettings(settingsXmlModel.Id, settingsXmlModel.DisplayName, settingsXmlModel.FolderName, settingsXmlModel.SubFolder, settingsXmlModel.UIVersion, subGroupDelimiter, propertyChanged, propGroups, new List<ISettingsPresetBuilder>());
			}
			return result;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000B438 File Offset: 0x00009638
		private ExternalPerSaveSettings(string id, string displayName, string folderName, string subFolder, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets) : base(id, displayName, folderName, subFolder, uiVersion, subGroupDelimiter, onPropertyChanged, settingPropertyGroups, presets)
		{
		}
	}
}
