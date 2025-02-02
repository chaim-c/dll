using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib.BUTR.Extensions;
using MCM.Common;

namespace MCM.Abstractions.Wrapper
{
	// Token: 0x0200009A RID: 154
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SettingsPropertyDefinitionWrapper : ISettingsPropertyDefinition, IPropertyDefinitionBase, IPropertyDefinitionBool, IPropertyDefinitionDropdown, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithEditableMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithCustomFormatter, IPropertyDefinitionWithId, IPropertyDefinitionText, IPropertyDefinitionGroupToggle, IPropertyGroupDefinition, IPropertyDefinitionButton
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000A652 File Offset: 0x00008852
		public IRef PropertyReference { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000A65A File Offset: 0x0000885A
		public SettingType SettingType { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000A662 File Offset: 0x00008862
		public string DisplayName { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000A66A File Offset: 0x0000886A
		public int Order { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000A672 File Offset: 0x00008872
		public bool RequireRestart { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000A67A File Offset: 0x0000887A
		public string HintText { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000A682 File Offset: 0x00008882
		public decimal MaxValue { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000A68A File Offset: 0x0000888A
		public decimal MinValue { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000A692 File Offset: 0x00008892
		public decimal EditableMinValue { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000A69A File Offset: 0x0000889A
		public decimal EditableMaxValue { get; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000A6A2 File Offset: 0x000088A2
		public int SelectedIndex { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000A6AA File Offset: 0x000088AA
		public string ValueFormat { get; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000A6B2 File Offset: 0x000088B2
		[Nullable(2)]
		public Type CustomFormatter { [NullableContext(2)] get; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000A6BA File Offset: 0x000088BA
		public string GroupName { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000A6C2 File Offset: 0x000088C2
		public int GroupOrder { get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000A6CA File Offset: 0x000088CA
		public string Id { get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000A6D2 File Offset: 0x000088D2
		public bool IsToggle { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000A6DA File Offset: 0x000088DA
		private char SubGroupDelimiter { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000A6E2 File Offset: 0x000088E2
		public string Content { get; }

		// Token: 0x06000349 RID: 841 RVA: 0x0000A6EC File Offset: 0x000088EC
		public SettingsPropertyDefinitionWrapper(object @object)
		{
			Type type = @object.GetType();
			PropertyInfo settingTypeProperty = AccessTools2.Property(type, "SettingType", true);
			PropertyInfo propertyProperty = AccessTools2.Property(type, "PropertyReference", true);
			PropertyInfo displayNameProperty = AccessTools2.Property(type, "DisplayName", true);
			PropertyInfo hintTextProperty = AccessTools2.Property(type, "HintText", true);
			PropertyInfo orderProperty = AccessTools2.Property(type, "Order", true);
			PropertyInfo requireRestartProperty = AccessTools2.Property(type, "RequireRestart", true);
			PropertyInfo groupNameProperty = AccessTools2.Property(type, "GroupName", true);
			PropertyInfo groupOrderProperty = AccessTools2.Property(type, "GroupOrder", true);
			PropertyInfo minValueProperty = AccessTools2.Property(type, "MinValue", true);
			PropertyInfo maxValueProperty = AccessTools2.Property(type, "MaxValue", true);
			PropertyInfo editableMinValueProperty = AccessTools2.Property(type, "EditableMinValue", true);
			PropertyInfo editableMaxValueProperty = AccessTools2.Property(type, "EditableMaxValue", true);
			PropertyInfo selectedIndexProperty = AccessTools2.Property(type, "SelectedIndex", true);
			PropertyInfo valueFormatProperty = AccessTools2.Property(type, "ValueFormat", true);
			PropertyInfo customFormatterProperty = AccessTools2.Property(type, "CustomFormatter", true);
			PropertyInfo idProperty = AccessTools2.Property(type, "Id", true);
			PropertyInfo isToggleProperty = AccessTools2.Property(type, "IsToggle", true);
			PropertyInfo subGroupDelimiterProperty = AccessTools2.Property(type, "SubGroupDelimiter", true);
			PropertyInfo contentProperty = AccessTools2.Property(type, "Content", true);
			object settingTypeObject = (settingTypeProperty != null) ? settingTypeProperty.GetValue(@object) : null;
			SettingType resultEnum;
			this.SettingType = ((settingTypeObject != null) ? (Enum.TryParse<SettingType>(settingTypeObject.ToString(), out resultEnum) ? resultEnum : SettingType.NONE) : SettingType.NONE);
			object value = (propertyProperty != null) ? propertyProperty.GetValue(@object) : null;
			IRef ref3;
			if (value == null)
			{
				IRef ref2 = new ProxyRef<object>(() => null, delegate(object _)
				{
				});
				ref3 = ref2;
			}
			else
			{
				IRef @ref = value as IRef;
				if (@ref == null)
				{
					IRef ref2 = new RefWrapper(value);
					ref3 = ref2;
				}
				else
				{
					ref3 = @ref;
				}
			}
			this.PropertyReference = ref3;
			object to = (displayNameProperty != null) ? displayNameProperty.GetValue(@object) : null;
			if (!true)
			{
			}
			string str = to as string;
			string text;
			if (str == null)
			{
				if (to == null)
				{
					text = "ERROR";
				}
				else
				{
					text = to.ToString();
				}
			}
			else
			{
				text = str;
			}
			if (!true)
			{
			}
			this.DisplayName = text;
			object to2 = (hintTextProperty != null) ? hintTextProperty.GetValue(@object) : null;
			if (!true)
			{
			}
			string str2 = to2 as string;
			if (str2 == null)
			{
				if (to2 == null)
				{
					text = "ERROR";
				}
				else
				{
					text = to2.ToString();
				}
			}
			else
			{
				text = str2;
			}
			if (!true)
			{
			}
			this.HintText = text;
			this.Order = (((orderProperty != null) ? orderProperty.GetValue(@object) : null) as int?).GetValueOrDefault(-1);
			this.RequireRestart = (((requireRestartProperty != null) ? requireRestartProperty.GetValue(@object) : null) as bool?).GetValueOrDefault(true);
			this.GroupName = ((((groupNameProperty != null) ? groupNameProperty.GetValue(@object) : null) as string) ?? string.Empty);
			this.GroupOrder = (((groupOrderProperty != null) ? groupOrderProperty.GetValue(@object) : null) as int?).GetValueOrDefault(-1);
			object minVal = (minValueProperty != null) ? minValueProperty.GetValue(@object) : null;
			this.MinValue = ((minVal != null) ? (minVal as decimal?).GetValueOrDefault() : 0m);
			object maxVal = (maxValueProperty != null) ? maxValueProperty.GetValue(@object) : null;
			this.MaxValue = ((maxVal != null) ? (maxVal as decimal?).GetValueOrDefault() : 0m);
			object eMinVal = (editableMinValueProperty != null) ? editableMinValueProperty.GetValue(@object) : null;
			this.EditableMinValue = ((eMinVal != null) ? (eMinVal as decimal?).GetValueOrDefault() : 0m);
			object eMaxValue = (editableMaxValueProperty != null) ? editableMaxValueProperty.GetValue(@object) : null;
			this.EditableMaxValue = ((eMaxValue != null) ? (eMaxValue as decimal?).GetValueOrDefault() : 0m);
			this.SelectedIndex = (((selectedIndexProperty != null) ? selectedIndexProperty.GetValue(@object) : null) as int?).GetValueOrDefault();
			string text2 = ((valueFormatProperty != null) ? valueFormatProperty.GetValue(@object) : null) as string;
			string text3 = text2;
			if (text3 == null)
			{
				SettingType settingType = this.SettingType;
				if (!true)
				{
				}
				if (settingType != SettingType.Int)
				{
					if (settingType != SettingType.Float)
					{
						text = string.Empty;
					}
					else
					{
						text = "0.00";
					}
				}
				else
				{
					text = "0";
				}
				if (!true)
				{
				}
				text3 = text;
			}
			this.ValueFormat = text3;
			this.CustomFormatter = (((customFormatterProperty != null) ? customFormatterProperty.GetValue(@object) : null) as Type);
			this.Id = ((((idProperty != null) ? idProperty.GetValue(@object) : null) as string) ?? string.Empty);
			this.IsToggle = (((isToggleProperty != null) ? isToggleProperty.GetValue(@object) : null) as bool?).GetValueOrDefault();
			this.SubGroupDelimiter = (((subGroupDelimiterProperty != null) ? subGroupDelimiterProperty.GetValue(@object) : null) as char?).GetValueOrDefault('/');
			this.Content = ((((contentProperty != null) ? contentProperty.GetValue(@object) : null) as string) ?? string.Empty);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000AC18 File Offset: 0x00008E18
		public SettingsPropertyDefinition Clone(bool keepRefs = true)
		{
			object localPropValue = this.PropertyReference.Value;
			IEnumerable<IPropertyDefinitionBase> propertyDefinitionWrappers = SettingsUtils.GetPropertyDefinitionWrappers(this);
			IPropertyGroupDefinition propertyGroupDefinition = new PropertyGroupDefinitionWrapper(this);
			IRef propertyReference;
			if (!keepRefs)
			{
				IRef @ref = new StorageRef(localPropValue);
				propertyReference = @ref;
			}
			else
			{
				propertyReference = this.PropertyReference;
			}
			return new SettingsPropertyDefinition(propertyDefinitionWrappers, propertyGroupDefinition, propertyReference, this.SubGroupDelimiter);
		}
	}
}
