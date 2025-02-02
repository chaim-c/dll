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

namespace MCM.Abstractions.Base.Global
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExternalGlobalSettings : FluentGlobalSettings
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		private static SettingsPropertyDefinition FromXml(IPropertyGroupDefinition group, PropertyBaseXmlModel xmlModel, char subGroupDelimiter)
		{
			if (!true)
			{
			}
			PropertyBoolXmlModel model = xmlModel as PropertyBoolXmlModel;
			SettingsPropertyDefinition result;
			if (model == null)
			{
				PropertyDropdownXmlModel model2 = xmlModel as PropertyDropdownXmlModel;
				if (model2 == null)
				{
					PropertyFloatingIntegerXmlModel model3 = xmlModel as PropertyFloatingIntegerXmlModel;
					if (model3 == null)
					{
						PropertyIntegerXmlModel model4 = xmlModel as PropertyIntegerXmlModel;
						if (model4 == null)
						{
							PropertyTextXmlModel model5 = xmlModel as PropertyTextXmlModel;
							if (model5 == null)
							{
								if (!true)
								{
								}
								<PrivateImplementationDetails>.ThrowInvalidOperationException();
							}
							else
							{
								result = new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(model5), group, new StorageRef<string>(model5.Value), subGroupDelimiter);
							}
						}
						else
						{
							result = new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(model4), group, new StorageRef<int>((int)model4.Value), subGroupDelimiter);
						}
					}
					else
					{
						result = new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(model3), group, new StorageRef<float>((float)model3.Value), subGroupDelimiter);
					}
				}
				else
				{
					result = new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(model2), group, new StorageRef<Dropdown<string>>(new Dropdown<string>(model2.Values, model2.SelectedIndex)), subGroupDelimiter);
				}
			}
			else
			{
				result = new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(model), group, new StorageRef<bool>(model.Value), subGroupDelimiter);
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		[NullableContext(2)]
		public static ExternalGlobalSettings CreateFromXmlFile([Nullable(1)] string filePath, PropertyChangedEventHandler propertyChanged = null)
		{
			ExternalGlobalSettings result;
			using (FileStream xmlStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				SettingsXmlModel settingsXmlModel = SerializationUtils.DeserializeXml<SettingsXmlModel>(xmlStream);
				bool flag = settingsXmlModel == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					char subGroupDelimiter = settingsXmlModel.SubGroupDelimiter[0];
					IEnumerable<SettingsPropertyDefinition> props = settingsXmlModel.Groups.SelectMany((PropertyGroupXmlModel g) => from p in g.Properties
					select ExternalGlobalSettings.FromXml(g, p, subGroupDelimiter)).Concat(from p in settingsXmlModel.Properties
					select ExternalGlobalSettings.FromXml(SettingsPropertyGroupDefinition.DefaultGroup, p, subGroupDelimiter));
					List<SettingsPropertyGroupDefinition> propGroups = SettingsUtils.GetSettingsPropertyGroups(subGroupDelimiter, props);
					result = new ExternalGlobalSettings(settingsXmlModel.Id, settingsXmlModel.DisplayName, settingsXmlModel.FolderName, settingsXmlModel.SubFolder, settingsXmlModel.FormatType, settingsXmlModel.UIVersion, subGroupDelimiter, propertyChanged, propGroups, new List<ISettingsPresetBuilder>(), filePath);
				}
			}
			return result;
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000BFB4 File Offset: 0x0000A1B4
		public string FilePath { get; set; }

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000BFC0 File Offset: 0x0000A1C0
		private ExternalGlobalSettings(string id, string displayName, string folderName, string subFolder, string format, int uiVersion, char subGroupDelimiter, [Nullable(2)] PropertyChangedEventHandler onPropertyChanged, IEnumerable<SettingsPropertyGroupDefinition> settingPropertyGroups, IEnumerable<ISettingsPresetBuilder> presets, string filePath) : base(id, displayName, folderName, subFolder, format, uiVersion, subGroupDelimiter, onPropertyChanged, settingPropertyGroups, presets)
		{
			this.FilePath = filePath;
		}
	}
}
