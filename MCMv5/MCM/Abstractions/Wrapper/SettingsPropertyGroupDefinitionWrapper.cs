using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200009B RID: 155
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SettingsPropertyGroupDefinitionWrapper : SettingsPropertyGroupDefinition
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000AC60 File Offset: 0x00008E60
		[return: Nullable(2)]
		private static string GetGroupName(object @object)
		{
			PropertyInfo propInfo = @object.GetType().GetProperty("GroupName");
			return ((propInfo != null) ? propInfo.GetValue(@object) : null) as string;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000AC98 File Offset: 0x00008E98
		[return: Nullable(2)]
		private static string GetGroupNameOverride(object @object)
		{
			PropertyInfo propInfo = @object.GetType().GetProperty("GroupNameOverride");
			return ((propInfo != null) ? propInfo.GetValue(@object) : null) as string;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		private static int? GetGroupOrder(object @object)
		{
			PropertyInfo propInfo = @object.GetType().GetProperty("Order");
			return ((propInfo != null) ? propInfo.GetValue(@object) : null) as int?;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000AD0C File Offset: 0x00008F0C
		private static char? GetSubGroupDelimiter(object @object)
		{
			PropertyInfo propInfo = @object.GetType().GetProperty("SubGroupDelimiter");
			return ((propInfo != null) ? propInfo.GetValue(@object) : null) as char?;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000AD48 File Offset: 0x00008F48
		public SettingsPropertyGroupDefinitionWrapper(object @object) : base(SettingsPropertyGroupDefinitionWrapper.GetGroupName(@object) ?? "ERROR", SettingsPropertyGroupDefinitionWrapper.GetGroupOrder(@object).GetValueOrDefault(-1))
		{
			base.SetSubGroupDelimiter(SettingsPropertyGroupDefinitionWrapper.GetSubGroupDelimiter(@object).GetValueOrDefault('/'));
			this.subGroups.AddRange(SettingsPropertyGroupDefinitionWrapper.GetSubGroups(@object).SortDefault());
			this.settingProperties.AddRange(SettingsPropertyGroupDefinitionWrapper.GetSettingProperties(@object).SortDefault());
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		private static IEnumerable<SettingsPropertyGroupDefinition> GetSubGroups(object @object)
		{
			PropertyInfo subGroupsProperty = AccessTools2.Property(@object.GetType(), "SubGroups", true) ?? AccessTools2.Property(@object.GetType(), "SettingPropertyGroups", true);
			PropertyInfo propertyInfo = subGroupsProperty;
			object obj2 = (propertyInfo != null) ? propertyInfo.GetValue(@object) : null;
			IEnumerable list = obj2 as IEnumerable;
			bool flag = list != null;
			if (flag)
			{
				foreach (object obj in list)
				{
					yield return new SettingsPropertyGroupDefinitionWrapper(obj);
					obj = null;
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		private static IEnumerable<ISettingsPropertyDefinition> GetSettingProperties(object @object)
		{
			PropertyInfo settingPropertiesProperty = AccessTools2.Property(@object.GetType(), "SettingProperties", true);
			PropertyInfo propertyInfo = settingPropertiesProperty;
			object obj2 = (propertyInfo != null) ? propertyInfo.GetValue(@object) : null;
			IEnumerable list = obj2 as IEnumerable;
			bool flag = list != null;
			if (flag)
			{
				foreach (object obj in list)
				{
					yield return new SettingsPropertyDefinitionWrapper(obj);
					obj = null;
				}
				IEnumerator enumerator = null;
			}
			yield break;
			yield break;
		}
	}
}
