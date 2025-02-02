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

namespace MCM.Abstractions.Base.PerCampaign
{
	// Token: 0x020000B7 RID: 183
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExternalPerCampaignSettings : FluentPerCampaignSettings
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0000B944 File Offset: 0x00009B44
		[return: Nullable(2)]
		public static ExternalPerCampaignSettings CreateFromXmlStream(Stream xmlStream, Func<IPropertyDefinitionBase, IRef> assignRefDelegate, [Nullable(2)] PropertyChangedEventHandler propertyChanged = null)
		{
			SettingsXmlModel settingsXmlModel = SerializationUtils.DeserializeXml<SettingsXmlModel>(xmlStream);
			bool flag = settingsXmlModel == null;
			ExternalPerCampaignSettings result;
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
				result = new ExternalPerCampaignSettings(settingsXmlModel.Id, settingsXmlModel.DisplayName, settingsXmlModel.FolderName, settingsXmlModel.SubFolder, settingsXmlModel.UIVersion, subGroupDelimiter, propertyChanged, propGroups, new List<ISettingsPresetBuilder>());
			}
			return result;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000BA04 File Offset: 0x00009C04
		private ExternalPerCampaignSettings(string id, string displayName, string folderName, string subFolder, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets) : base(id, displayName, folderName, subFolder, uiVersion, subGroupDelimiter, onPropertyChanged, settingPropertyGroups, presets)
		{
		}
	}
}
