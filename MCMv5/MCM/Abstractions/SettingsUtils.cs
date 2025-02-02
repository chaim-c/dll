using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection;
using BUTR.DependencyInjection.Logger;
using HarmonyLib.BUTR.Extensions;
using MCM.Abstractions.Base;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x02000068 RID: 104
	[NullableContext(1)]
	[Nullable(0)]
	public class SettingsUtils
	{
		// Token: 0x06000257 RID: 599 RVA: 0x00008EDC File Offset: 0x000070DC
		public static void CheckIsValid(ISettingsPropertyDefinition prop, [Nullable(2)] object settings)
		{
			bool flag = settings == null;
			if (flag)
			{
				throw new Exception("Settings is null.");
			}
			PropertyRef propertyRef = prop.PropertyReference as PropertyRef;
			bool flag2 = propertyRef != null;
			if (flag2)
			{
				bool flag3 = !propertyRef.PropertyInfo.CanRead;
				if (flag3)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Property ",
						propertyRef.PropertyInfo.Name,
						" in ",
						settings.GetType().FullName,
						" must have a getter."
					}));
				}
				bool flag4 = prop.SettingType != SettingType.Dropdown && !propertyRef.PropertyInfo.CanWrite;
				if (flag4)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Property ",
						propertyRef.PropertyInfo.Name,
						" in ",
						settings.GetType().FullName,
						" must have a setter."
					}));
				}
				SettingType settingType = prop.SettingType;
				bool flag5 = settingType - SettingType.Int <= 1;
				bool flag6 = flag5;
				if (flag6)
				{
					bool flag7 = prop.MinValue == prop.MaxValue;
					if (flag7)
					{
						throw new Exception(string.Concat(new string[]
						{
							"Property ",
							propertyRef.PropertyInfo.Name,
							" in ",
							settings.GetType().FullName,
							" is a numeric type but the MinValue and MaxValue are the same."
						}));
					}
				}
				bool flag8 = prop.SettingType > SettingType.Bool;
				if (flag8)
				{
					bool isToggle = prop.IsToggle;
					if (isToggle)
					{
						throw new Exception(string.Concat(new string[]
						{
							"Property ",
							propertyRef.PropertyInfo.Name,
							" in ",
							settings.GetType().FullName,
							" is marked as the main toggle for the group but is a numeric type. The main toggle must be a boolean type."
						}));
					}
				}
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000090B4 File Offset: 0x000072B4
		public static void ResetSettings(BaseSettings settings)
		{
			IWrapper wrapper = settings as IWrapper;
			BaseSettings copyWrapped;
			bool flag;
			if (wrapper != null)
			{
				object @object = wrapper.Object;
				object copy = Activator.CreateInstance((@object != null) ? @object.GetType() : null);
				if (copy != null)
				{
					copyWrapped = (Activator.CreateInstance(wrapper.GetType(), new object[]
					{
						copy
					}) as BaseSettings);
					flag = (copyWrapped != null);
					goto IL_48;
				}
			}
			flag = false;
			IL_48:
			bool flag2 = flag;
			if (flag2)
			{
				SettingsUtils.OverrideSettings(settings, copyWrapped);
			}
			else
			{
				BaseSettings copySettings = Activator.CreateInstance(settings.GetType()) as BaseSettings;
				bool flag3 = copySettings != null;
				if (flag3)
				{
					SettingsUtils.OverrideSettings(settings, copySettings);
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000913D File Offset: 0x0000733D
		public static void OverrideSettings(BaseSettings settings, BaseSettings overrideSettings)
		{
			SettingsUtils.OverrideValues(settings, overrideSettings);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009148 File Offset: 0x00007348
		public static bool Equals(BaseSettings settings1, BaseSettings settings2)
		{
			Dictionary<ValueTuple<string, string>, ISettingsPropertyDefinition> setDict = settings1.GetAllSettingPropertyDefinitions().ToDictionary((ISettingsPropertyDefinition x) => new ValueTuple<string, string>(x.DisplayName, x.GroupName), (ISettingsPropertyDefinition x) => x);
			Dictionary<ValueTuple<string, string>, ISettingsPropertyDefinition> setDict2 = settings2.GetAllSettingPropertyDefinitions().ToDictionary((ISettingsPropertyDefinition x) => new ValueTuple<string, string>(x.DisplayName, x.GroupName), (ISettingsPropertyDefinition x) => x);
			bool flag = setDict.Count != setDict2.Count;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				foreach (KeyValuePair<ValueTuple<string, string>, ISettingsPropertyDefinition> kv in setDict)
				{
					ISettingsPropertyDefinition spd2;
					bool flag2 = !setDict2.TryGetValue(kv.Key, out spd2) || !SettingsUtils.Equals(kv.Value, spd2);
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009278 File Offset: 0x00007478
		[NullableContext(2)]
		public static bool Equals(ISettingsPropertyDefinition currentDefinition, ISettingsPropertyDefinition newDefinition)
		{
			bool flag = currentDefinition == null || newDefinition == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				switch (currentDefinition.SettingType)
				{
				case SettingType.Bool:
				case SettingType.Int:
				case SettingType.Float:
				case SettingType.String:
				case SettingType.Button:
				{
					bool flag2 = currentDefinition.PropertyReference.Value == null || newDefinition.PropertyReference.Value == null;
					if (flag2)
					{
						result = false;
					}
					else
					{
						object original = currentDefinition.PropertyReference.Value;
						object @new = newDefinition.PropertyReference.Value;
						result = original.Equals(@new);
					}
					break;
				}
				case SettingType.Dropdown:
				{
					bool flag3 = currentDefinition.PropertyReference.Value == null || newDefinition.PropertyReference.Value == null;
					if (flag3)
					{
						result = false;
					}
					else
					{
						SelectedIndexWrapper original2 = new SelectedIndexWrapper(currentDefinition.PropertyReference.Value);
						SelectedIndexWrapper new2 = new SelectedIndexWrapper(newDefinition.PropertyReference.Value);
						result = original2.SelectedIndex.Equals(new2.SelectedIndex);
					}
					break;
				}
				default:
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009388 File Offset: 0x00007588
		public static void OverrideValues(BaseSettings current, BaseSettings @new)
		{
			Dictionary<string, SettingsPropertyGroupDefinition> currentDict = current.GetUnsortedSettingPropertyGroups().ToDictionary((SettingsPropertyGroupDefinition x) => x.GroupNameRaw, (SettingsPropertyGroupDefinition x) => x);
			foreach (SettingsPropertyGroupDefinition nspg in @new.GetUnsortedSettingPropertyGroups())
			{
				SettingsPropertyGroupDefinition spg;
				bool flag = currentDict.TryGetValue(nspg.GroupNameRaw, out spg);
				if (flag)
				{
					SettingsUtils.OverrideValues(spg, nspg);
				}
				else
				{
					IBUTRLogger<SettingsUtils> logger = GenericServiceProvider.GetService<IBUTRLogger<SettingsUtils>>();
					if (logger != null)
					{
						logger.LogWarning(string.Concat(new string[]
						{
							@new.Id,
							"::",
							nspg.GroupNameRaw,
							" was not found on, ",
							current.Id
						}), Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009490 File Offset: 0x00007690
		public static void OverrideValues(SettingsPropertyGroupDefinition current, SettingsPropertyGroupDefinition @new)
		{
			Dictionary<string, SettingsPropertyGroupDefinition> currentSubGroups = current.SubGroups.ToDictionary((SettingsPropertyGroupDefinition x) => x.GroupNameRaw, (SettingsPropertyGroupDefinition x) => x);
			Dictionary<string, ISettingsPropertyDefinition> currentSettingProperties = current.SettingProperties.ToDictionary((ISettingsPropertyDefinition x) => x.DisplayName, (ISettingsPropertyDefinition x) => x);
			foreach (SettingsPropertyGroupDefinition nspg in @new.SubGroups)
			{
				SettingsPropertyGroupDefinition spg;
				bool flag = currentSubGroups.TryGetValue(nspg.GroupNameRaw, out spg);
				if (flag)
				{
					SettingsUtils.OverrideValues(spg, nspg);
				}
				else
				{
					IBUTRLogger<SettingsUtils> logger = GenericServiceProvider.GetService<IBUTRLogger<SettingsUtils>>();
					if (logger != null)
					{
						logger.LogWarning(string.Concat(new string[]
						{
							@new.GroupName,
							"::",
							nspg.GroupNameRaw,
							" was not found on, ",
							current.GroupNameRaw
						}), Array.Empty<object>());
					}
				}
			}
			foreach (ISettingsPropertyDefinition nsp in @new.SettingProperties)
			{
				ISettingsPropertyDefinition sp;
				bool flag2 = currentSettingProperties.TryGetValue(nsp.DisplayName, out sp);
				if (flag2)
				{
					SettingsUtils.OverrideValues(sp, nsp);
				}
				else
				{
					IBUTRLogger<SettingsUtils> logger2 = GenericServiceProvider.GetService<IBUTRLogger<SettingsUtils>>();
					if (logger2 != null)
					{
						logger2.LogWarning(string.Concat(new string[]
						{
							@new.GroupNameRaw,
							"::",
							nsp.DisplayName,
							" was not found on, ",
							current.GroupNameRaw
						}), Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000969C File Offset: 0x0000789C
		public static void OverrideValues(ISettingsPropertyDefinition current, ISettingsPropertyDefinition @new)
		{
			bool flag = SettingsUtils.Equals(current, @new);
			if (!flag)
			{
				current.PropertyReference.Value = @new.PropertyReference.Value;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000096D0 File Offset: 0x000078D0
		public static bool IsForGenericDropdown(Type type)
		{
			bool implementsList = type.GetInterfaces().Any((Type x) => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>));
			bool hasSelectedIndex = AccessTools2.Property(type, "SelectedIndex", false) != null;
			return implementsList && hasSelectedIndex;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009723 File Offset: 0x00007923
		public static bool IsForTextDropdown(Type type)
		{
			return SettingsUtils.IsForGenericDropdown(type) && type.IsGenericType && AccessTools2.Property(type.GenericTypeArguments[0], "IsSelected", false) == null;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000974E File Offset: 0x0000794E
		public static bool IsForCheckboxDropdown(Type type)
		{
			return SettingsUtils.IsForGenericDropdown(type) && type.IsGenericType && AccessTools2.Property(type.GenericTypeArguments[0], "IsSelected", false) != null;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000977C File Offset: 0x0000797C
		[NullableContext(2)]
		public static bool IsForTextDropdown(object obj)
		{
			return obj != null && SettingsUtils.IsForTextDropdown(obj.GetType());
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000978F File Offset: 0x0000798F
		[NullableContext(2)]
		public static bool IsForCheckboxDropdown(object obj)
		{
			return obj != null && SettingsUtils.IsForCheckboxDropdown(obj.GetType());
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000097A2 File Offset: 0x000079A2
		public static IEnumerable<ISettingsPropertyDefinition> GetAllSettingPropertyDefinitions(SettingsPropertyGroupDefinition settingPropertyGroup1)
		{
			foreach (ISettingsPropertyDefinition settingProperty in settingPropertyGroup1.SettingProperties)
			{
				yield return settingProperty;
				settingProperty = null;
			}
			IEnumerator<ISettingsPropertyDefinition> enumerator = null;
			foreach (SettingsPropertyGroupDefinition settingPropertyGroup2 in settingPropertyGroup1.SubGroups)
			{
				foreach (ISettingsPropertyDefinition settingProperty2 in SettingsUtils.GetAllSettingPropertyDefinitions(settingPropertyGroup2))
				{
					yield return settingProperty2;
					settingProperty2 = null;
				}
				IEnumerator<ISettingsPropertyDefinition> enumerator3 = null;
				settingPropertyGroup2 = null;
			}
			IEnumerator<SettingsPropertyGroupDefinition> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000097B2 File Offset: 0x000079B2
		public static IEnumerable<SettingsPropertyGroupDefinition> GetAllSettingPropertyGroupDefinitions(SettingsPropertyGroupDefinition settingPropertyGroup)
		{
			yield return settingPropertyGroup;
			foreach (SettingsPropertyGroupDefinition settingPropertyGroup2 in settingPropertyGroup.SubGroups)
			{
				yield return settingPropertyGroup2;
				settingPropertyGroup2 = null;
			}
			IEnumerator<SettingsPropertyGroupDefinition> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000097C4 File Offset: 0x000079C4
		public static List<SettingsPropertyGroupDefinition> GetSettingsPropertyGroups(char subGroupDelimiter, IEnumerable<ISettingsPropertyDefinition> settingsPropertyDefinitions)
		{
			List<SettingsPropertyGroupDefinition> groups = new List<SettingsPropertyGroupDefinition>();
			foreach (ISettingsPropertyDefinition settingsPropertyDefinition in settingsPropertyDefinitions)
			{
				SettingsPropertyGroupDefinition group = SettingsUtils.GetGroupFor(subGroupDelimiter, settingsPropertyDefinition, groups);
				group.Add(settingsPropertyDefinition);
			}
			return groups;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009828 File Offset: 0x00007A28
		public static SettingsPropertyGroupDefinition GetGroupFor(char subGroupDelimiter, ISettingsPropertyDefinition sp, ICollection<SettingsPropertyGroupDefinition> rootCollection)
		{
			string groupName = sp.GroupName;
			bool flag = groupName.Contains(subGroupDelimiter);
			SettingsPropertyGroupDefinition group;
			if (flag)
			{
				string truncatedGroupName;
				string topGroupName = SettingsUtils.GetTopGroupName(subGroupDelimiter, groupName, out truncatedGroupName);
				SettingsPropertyGroupDefinition topGroup = rootCollection.GetGroupFromName(topGroupName);
				bool flag2 = topGroup == null;
				if (flag2)
				{
					topGroup = new SettingsPropertyGroupDefinition(topGroupName, -1).SetSubGroupDelimiter(subGroupDelimiter);
					rootCollection.Add(topGroup);
				}
				group = SettingsUtils.GetGroupForRecursive(subGroupDelimiter, truncatedGroupName, topGroup, sp);
			}
			else
			{
				group = rootCollection.GetGroupFromName(groupName);
				bool flag3 = group == null;
				if (flag3)
				{
					group = new SettingsPropertyGroupDefinition(groupName, sp.GroupOrder).SetSubGroupDelimiter(subGroupDelimiter);
					rootCollection.Add(group);
				}
			}
			return group;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000098C8 File Offset: 0x00007AC8
		public static SettingsPropertyGroupDefinition GetGroupForRecursive(char subGroupDelimiter, string groupName, SettingsPropertyGroupDefinition sgp, ISettingsPropertyDefinition sp)
		{
			for (;;)
			{
				bool flag = groupName.Contains(subGroupDelimiter);
				if (!flag)
				{
					break;
				}
				string truncatedGroupName;
				string topGroupName = SettingsUtils.GetTopGroupName(subGroupDelimiter, groupName, out truncatedGroupName);
				SettingsPropertyGroupDefinition topGroup = sgp.GetGroup(topGroupName);
				bool flag2 = topGroup == null;
				if (flag2)
				{
					topGroup = new SettingsPropertyGroupDefinition(topGroupName, -1).SetSubGroupDelimiter(subGroupDelimiter);
					sgp.Add(topGroup);
				}
				groupName = truncatedGroupName;
				sgp = topGroup;
			}
			SettingsPropertyGroupDefinition group = sgp.GetGroup(groupName);
			bool flag3 = group == null;
			if (flag3)
			{
				group = new SettingsPropertyGroupDefinition(groupName, sp.GroupOrder).SetSubGroupDelimiter(subGroupDelimiter);
				sgp.Add(group);
			}
			return group;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000996C File Offset: 0x00007B6C
		public static string GetTopGroupName(char subGroupDelimiter, string groupName, out string truncatedGroupName)
		{
			int index = groupName.IndexOf(subGroupDelimiter);
			string topGroupName = groupName.Substring(0, index);
			truncatedGroupName = groupName.Remove(0, index + 1);
			return topGroupName;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000999C File Offset: 0x00007B9C
		public static IEnumerable<IPropertyDefinitionBase> GetPropertyDefinitionWrappers(object property)
		{
			return SettingsUtils.GetPropertyDefinitionWrappers(new object[]
			{
				property
			});
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000099AD File Offset: 0x00007BAD
		public static IEnumerable<IPropertyDefinitionBase> GetPropertyDefinitionWrappers(IReadOnlyCollection<object> properties)
		{
			object propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionBool);
			bool flag = propAttr != null;
			if (flag)
			{
				yield return new PropertyDefinitionBoolWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionDropdown);
			bool flag2 = propAttr != null;
			if (flag2)
			{
				yield return new PropertyDefinitionDropdownWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionGroupToggle);
			bool flag3 = propAttr != null;
			if (flag3)
			{
				yield return new PropertyDefinitionGroupToggleWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionText);
			bool flag4 = propAttr != null;
			if (flag4)
			{
				yield return new PropertyDefinitionTextWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithActionFormat);
			bool flag5 = propAttr != null;
			if (flag5)
			{
				yield return new PropertyDefinitionWithActionFormatWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithCustomFormatter);
			bool flag6 = propAttr != null;
			if (flag6)
			{
				yield return new PropertyDefinitionWithCustomFormatterWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithEditableMinMax);
			bool flag7 = propAttr != null;
			if (flag7)
			{
				yield return new PropertyDefinitionWithEditableMinMaxWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithFormat);
			bool flag8 = propAttr != null;
			if (flag8)
			{
				yield return new PropertyDefinitionWithFormatWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithId);
			bool flag9 = propAttr != null;
			if (flag9)
			{
				yield return new PropertyDefinitionWithIdWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionWithMinMax);
			bool flag10 = propAttr != null;
			if (flag10)
			{
				yield return new PropertyDefinitionWithMinMaxWrapper(propAttr);
			}
			propAttr = properties.SingleOrDefault((object a) => a is IPropertyDefinitionButton);
			bool flag11 = propAttr != null;
			if (flag11)
			{
				yield return new PropertyDefinitionButtonWrapper(propAttr);
			}
			yield break;
		}
	}
}
