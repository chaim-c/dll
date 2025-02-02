using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Bannerlord.ModuleManager;
using ComparerExtensions;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using MCM.Common;
using MCM.UI.Actions;
using MCM.UI.GUI.ViewModels;
using Microsoft.Extensions.Logging;
using TaleWorlds.Localization;

namespace MCM.UI.Utils
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	internal static class UISettingsUtils
	{
		// Token: 0x0600005D RID: 93 RVA: 0x0000337C File Offset: 0x0000157C
		public static void OverrideValues(UndoRedoStack urs, BaseSettings current, BaseSettings @new)
		{
			Dictionary<string, SettingsPropertyGroupDefinition> currentDict = SettingPropertyDefinitionCache.GetSettingPropertyGroups(current).ToDictionary((SettingsPropertyGroupDefinition x) => x.GroupNameRaw, (SettingsPropertyGroupDefinition x) => x);
			foreach (SettingsPropertyGroupDefinition nspg in SettingPropertyDefinitionCache.GetAllSettingPropertyGroupDefinitions(@new))
			{
				SettingsPropertyGroupDefinition spg;
				bool flag = currentDict.TryGetValue(nspg.GroupNameRaw, out spg);
				if (flag)
				{
					UISettingsUtils.OverrideValues(urs, spg, nspg);
				}
				else
				{
					LoggerExtensions.LogWarning(MCMUISubModule.Logger, "{NewId}::{GroupName} was not found on, {CurrentId}", new object[]
					{
						@new.Id,
						nspg.GroupNameRaw,
						current.Id
					});
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003464 File Offset: 0x00001664
		public static void OverrideValues(UndoRedoStack urs, SettingsPropertyGroupDefinition current, SettingsPropertyGroupDefinition @new)
		{
			Dictionary<string, SettingsPropertyGroupDefinition> currentSubGroups = current.SubGroups.ToDictionary((SettingsPropertyGroupDefinition x) => x.GroupNameRaw, (SettingsPropertyGroupDefinition x) => x);
			Dictionary<string, ISettingsPropertyDefinition> currentSettingProperties = current.SettingProperties.ToDictionary((ISettingsPropertyDefinition x) => x.DisplayName, (ISettingsPropertyDefinition x) => x);
			foreach (SettingsPropertyGroupDefinition nspg in @new.SubGroups)
			{
				SettingsPropertyGroupDefinition spg;
				bool flag = currentSubGroups.TryGetValue(nspg.GroupNameRaw, out spg);
				if (flag)
				{
					UISettingsUtils.OverrideValues(urs, spg, nspg);
				}
				else
				{
					LoggerExtensions.LogWarning(MCMUISubModule.Logger, "{NewId}::{GroupName} was not found on, {CurrentId}", new object[]
					{
						@new.GroupNameRaw,
						nspg.GroupNameRaw,
						current.GroupNameRaw
					});
				}
			}
			foreach (ISettingsPropertyDefinition nsp in @new.SettingProperties)
			{
				ISettingsPropertyDefinition sp;
				bool flag2 = currentSettingProperties.TryGetValue(nsp.DisplayName, out sp);
				if (flag2)
				{
					UISettingsUtils.OverrideValues(urs, sp, nsp);
				}
				else
				{
					LoggerExtensions.LogWarning(MCMUISubModule.Logger, "{NewId}::{GroupName} was not found on, {CurrentId}", new object[]
					{
						@new.GroupNameRaw,
						nsp.DisplayName,
						current.GroupNameRaw
					});
				}
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003628 File Offset: 0x00001828
		public static void OverrideValues(UndoRedoStack urs, ISettingsPropertyDefinition current, ISettingsPropertyDefinition @new)
		{
			bool flag = SettingsUtils.Equals(current, @new);
			if (!flag)
			{
				switch (current.SettingType)
				{
				case SettingType.Bool:
				{
					object value = @new.PropertyReference.Value;
					if (value is bool)
					{
						bool val = (bool)value;
						urs.Do(new SetValueTypeAction<bool>(current.PropertyReference, val));
					}
					break;
				}
				case SettingType.Int:
				{
					object value = @new.PropertyReference.Value;
					if (value is int)
					{
						int val2 = (int)value;
						urs.Do(new SetValueTypeAction<int>(current.PropertyReference, val2));
					}
					break;
				}
				case SettingType.Float:
				{
					object value = @new.PropertyReference.Value;
					if (value is float)
					{
						float val3 = (float)value;
						urs.Do(new SetValueTypeAction<float>(current.PropertyReference, val3));
					}
					break;
				}
				case SettingType.String:
				{
					string val4 = @new.PropertyReference.Value as string;
					if (val4 != null)
					{
						urs.Do(new SetStringAction(current.PropertyReference, val4));
					}
					break;
				}
				case SettingType.Dropdown:
				{
					object val5 = @new.PropertyReference.Value;
					if (val5 != null)
					{
						urs.Do(new SetSelectedIndexAction(current.PropertyReference, val5));
					}
					break;
				}
				case SettingType.Button:
					if (!(@new.PropertyReference.Value is Action))
					{
					}
					break;
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000379C File Offset: 0x0000199C
		public static IEnumerable<object> GetDropdownValues(IRef @ref)
		{
			object value = @ref.Value;
			if (!true)
			{
			}
			IEnumerable<object> enumerableObj = value as IEnumerable<object>;
			IEnumerable<object> result;
			if (enumerableObj == null)
			{
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable == null)
				{
					result = Enumerable.Empty<object>();
				}
				else
				{
					result = enumerable.Cast<object>();
				}
			}
			else
			{
				result = enumerableObj;
			}
			if (!true)
			{
			}
			return result;
		}

		// Token: 0x0400001F RID: 31
		public static readonly IComparer<SettingsPropertyVM> SettingsPropertyVMComparer = (from x in KeyComparer<SettingsPropertyVM>
		orderby x.SettingPropertyDefinition.Order
		select x).ThenBy((SettingsPropertyVM x) => new TextObject(x.SettingPropertyDefinition.DisplayName, null).ToString(), new AlphanumComparatorFast());

		// Token: 0x04000020 RID: 32
		public static readonly IComparer<SettingsPropertyGroupVM> SettingsPropertyGroupVMComparer = (from x in KeyComparer<SettingsPropertyGroupVM>
		orderby x.SettingPropertyGroupDefinition.GroupNameRaw == SettingsPropertyGroupDefinition.DefaultGroupName descending, x.SettingPropertyGroupDefinition.Order
		select x).ThenBy((SettingsPropertyGroupVM x) => new TextObject(x.SettingPropertyGroupDefinition.GroupName, null).ToString(), new AlphanumComparatorFast());
	}
}
