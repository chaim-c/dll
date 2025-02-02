using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Base;
using MCM.Abstractions.Properties;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.Implementation
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AttributeSettingsPropertyDiscoverer : IAttributeSettingsPropertyDiscoverer, ISettingsPropertyDiscoverer
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005139 File Offset: 0x00003339
		public IEnumerable<string> DiscoveryTypes { get; } = new string[]
		{
			"attributes"
		};

		// Token: 0x060000DB RID: 219 RVA: 0x00005141 File Offset: 0x00003341
		public IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings)
		{
			foreach (ISettingsPropertyDefinition propertyDefinition in AttributeSettingsPropertyDiscoverer.GetPropertiesInternal(settings))
			{
				SettingsUtils.CheckIsValid(propertyDefinition, settings);
				yield return propertyDefinition;
				propertyDefinition = null;
			}
			IEnumerator<ISettingsPropertyDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005158 File Offset: 0x00003358
		private static IEnumerable<ISettingsPropertyDefinition> GetPropertiesInternal(BaseSettings settings)
		{
			Type type = settings.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "SubGroupDelimiter", true);
			char subGroupDelimiter = (((propertyInfo != null) ? propertyInfo.GetValue(settings) : null) as char?).GetValueOrDefault('/');
			foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				bool flag = property.Name == "Id";
				if (!flag)
				{
					bool flag2 = property.Name == "DisplayName";
					if (!flag2)
					{
						bool flag3 = property.Name == "FolderName";
						if (!flag3)
						{
							bool flag4 = property.Name == "FormatType";
							if (!flag4)
							{
								bool flag5 = property.Name == "SubFolder";
								if (!flag5)
								{
									bool flag6 = property.Name == "UIVersion";
									if (!flag6)
									{
										List<Attribute> attributes = property.GetCustomAttributes().ToList<Attribute>();
										object groupAttrObj = attributes.SingleOrDefault((Attribute a) => a is IPropertyGroupDefinition);
										IPropertyGroupDefinition propertyGroupDefinition;
										if (groupAttrObj == null)
										{
											propertyGroupDefinition = SettingPropertyGroupAttribute.Default;
										}
										else
										{
											IPropertyGroupDefinition propertyGroupDefinition2 = new PropertyGroupDefinitionWrapper(groupAttrObj);
											propertyGroupDefinition = propertyGroupDefinition2;
										}
										IPropertyGroupDefinition groupDefinition = propertyGroupDefinition;
										List<IPropertyDefinitionBase> propertyDefinitions = SettingsUtils.GetPropertyDefinitionWrappers(attributes).ToList<IPropertyDefinitionBase>();
										bool flag7 = propertyDefinitions.Count > 0;
										if (flag7)
										{
											yield return new SettingsPropertyDefinition(propertyDefinitions, groupDefinition, new PropertyRef(property, settings), subGroupDelimiter);
										}
										attributes = null;
										groupAttrObj = null;
										groupDefinition = null;
										propertyDefinitions = null;
										property = null;
									}
								}
							}
						}
					}
				}
			}
			PropertyInfo[] array = null;
			yield break;
		}
	}
}
