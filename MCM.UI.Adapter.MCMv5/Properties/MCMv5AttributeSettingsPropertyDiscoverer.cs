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

namespace MCM.UI.Adapter.MCMv5.Properties
{
	// Token: 0x0200000C RID: 12
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MCMv5AttributeSettingsPropertyDiscoverer : IAttributeSettingsPropertyDiscoverer, ISettingsPropertyDiscoverer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002983 File Offset: 0x00000B83
		public IEnumerable<string> DiscoveryTypes { get; } = new string[]
		{
			"mcm_v5_attributes"
		};

		// Token: 0x06000022 RID: 34 RVA: 0x0000298B File Offset: 0x00000B8B
		public IEnumerable<ISettingsPropertyDefinition> GetProperties(BaseSettings settings)
		{
			if (!true)
			{
			}
			IWrapper wrapper = settings as IWrapper;
			object obj2;
			if (wrapper == null)
			{
				obj2 = settings;
			}
			else
			{
				obj2 = wrapper.Object;
			}
			if (!true)
			{
			}
			object obj = obj2;
			wrapper = null;
			foreach (ISettingsPropertyDefinition propertyDefinition in MCMv5AttributeSettingsPropertyDiscoverer.GetPropertiesInternal(obj))
			{
				SettingsUtils.CheckIsValid(propertyDefinition, obj);
				yield return propertyDefinition;
				propertyDefinition = null;
			}
			IEnumerator<ISettingsPropertyDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029A2 File Offset: 0x00000BA2
		private static IEnumerable<ISettingsPropertyDefinition> GetPropertiesInternal([Nullable(2)] object @object)
		{
			bool flag = @object == null;
			if (flag)
			{
				yield break;
			}
			Type type = @object.GetType();
			PropertyInfo propertyInfo = AccessTools2.Property(type, "SubGroupDelimiter", true);
			char subGroupDelimiter = (((propertyInfo != null) ? propertyInfo.GetValue(@object) : null) as char?).GetValueOrDefault('/');
			foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
			{
				bool flag2 = property.Name == "Id";
				if (!flag2)
				{
					bool flag3 = property.Name == "DisplayName";
					if (!flag3)
					{
						bool flag4 = property.Name == "FolderName";
						if (!flag4)
						{
							bool flag5 = property.Name == "FormatType";
							if (!flag5)
							{
								bool flag6 = property.Name == "SubFolder";
								if (!flag6)
								{
									bool flag7 = property.Name == "UIVersion";
									if (!flag7)
									{
										List<Attribute> attributes = property.GetCustomAttributes().ToList<Attribute>();
										object groupAttrObj = attributes.SingleOrDefault((Attribute a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyGroupDefinition"));
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
										List<IPropertyDefinitionBase> propertyDefinitions = MCMv5AttributeSettingsPropertyDiscoverer.GetPropertyDefinitionWrappers(attributes).ToList<IPropertyDefinitionBase>();
										bool flag8 = propertyDefinitions.Count > 0;
										if (flag8)
										{
											yield return new SettingsPropertyDefinition(propertyDefinitions, groupDefinition, new PropertyRef(property, @object), subGroupDelimiter);
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

		// Token: 0x06000024 RID: 36 RVA: 0x000029B2 File Offset: 0x00000BB2
		private static IEnumerable<IPropertyDefinitionBase> GetPropertyDefinitionWrappers(IReadOnlyCollection<object> properties)
		{
			object propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionBool"));
			bool flag = propAttr != null;
			if (flag)
			{
				yield return new PropertyDefinitionBoolWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionDropdown"));
			bool flag2 = propAttr != null;
			if (flag2)
			{
				yield return new PropertyDefinitionDropdownWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionGroupToggle"));
			bool flag3 = propAttr != null;
			if (flag3)
			{
				yield return new PropertyDefinitionGroupToggleWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionText"));
			bool flag4 = propAttr != null;
			if (flag4)
			{
				yield return new PropertyDefinitionTextWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithActionFormat"));
			bool flag5 = propAttr != null;
			if (flag5)
			{
				yield return new PropertyDefinitionWithActionFormatWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithCustomFormatter"));
			bool flag6 = propAttr != null;
			if (flag6)
			{
				yield return new PropertyDefinitionWithCustomFormatterWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithEditableMinMax"));
			bool flag7 = propAttr != null;
			if (flag7)
			{
				yield return new PropertyDefinitionWithEditableMinMaxWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithFormat"));
			bool flag8 = propAttr != null;
			if (flag8)
			{
				yield return new PropertyDefinitionWithFormatWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithId"));
			bool flag9 = propAttr != null;
			if (flag9)
			{
				yield return new PropertyDefinitionWithIdWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionWithMinMax"));
			bool flag10 = propAttr != null;
			if (flag10)
			{
				yield return new PropertyDefinitionWithMinMaxWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => ReflectionUtils.ImplementsEquivalentInterface(a.GetType(), "MCM.Abstractions.IPropertyDefinitionButton"));
			bool flag11 = propAttr != null;
			if (flag11)
			{
				yield return new PropertyDefinitionButtonWrapper(propAttr);
			}
			yield break;
		}
	}
}
