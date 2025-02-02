using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MCM.Abstractions.Attributes.v1;
using MCM.Abstractions.Wrapper;
using MCM.Common;

namespace MCM.Abstractions
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SettingsPropertyDefinition : ISettingsPropertyDefinition, IPropertyDefinitionBase, IPropertyDefinitionBool, IPropertyDefinitionDropdown, IPropertyDefinitionWithMinMax, IPropertyDefinitionWithEditableMinMax, IPropertyDefinitionWithFormat, IPropertyDefinitionWithCustomFormatter, IPropertyDefinitionWithId, IPropertyDefinitionText, IPropertyDefinitionGroupToggle, IPropertyGroupDefinition, IPropertyDefinitionButton
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008113 File Offset: 0x00006313
		public string Id { get; } = string.Empty;

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000811B File Offset: 0x0000631B
		public IRef PropertyReference { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008123 File Offset: 0x00006323
		public SettingType SettingType { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000812B File Offset: 0x0000632B
		public string DisplayName { get; } = string.Empty;

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008133 File Offset: 0x00006333
		public int Order { get; } = -1;

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000813B File Offset: 0x0000633B
		public bool RequireRestart { get; } = 1;

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001EE RID: 494 RVA: 0x00008143 File Offset: 0x00006343
		public string HintText { get; } = string.Empty;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000814B File Offset: 0x0000634B
		public decimal MaxValue { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x00008153 File Offset: 0x00006353
		public decimal MinValue { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000815B File Offset: 0x0000635B
		public decimal EditableMinValue { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008163 File Offset: 0x00006363
		public decimal EditableMaxValue { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000816B File Offset: 0x0000636B
		public int SelectedIndex { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008173 File Offset: 0x00006373
		public string ValueFormat { get; } = string.Empty;

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000817B File Offset: 0x0000637B
		[Nullable(2)]
		public Type CustomFormatter { [NullableContext(2)] get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00008183 File Offset: 0x00006383
		public string GroupName { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000818B File Offset: 0x0000638B
		public bool IsToggle { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00008193 File Offset: 0x00006393
		public int GroupOrder { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000819B File Offset: 0x0000639B
		private char SubGroupDelimiter { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FA RID: 506 RVA: 0x000081A3 File Offset: 0x000063A3
		public string Content { get; } = string.Empty;

		// Token: 0x060001FB RID: 507 RVA: 0x000081AB File Offset: 0x000063AB
		public SettingsPropertyDefinition(IPropertyDefinitionBase propertyDefinition, IPropertyGroupDefinition propertyGroupDefinition, IRef propertyReference, char subGroupDelimiter) : this(new IPropertyDefinitionBase[]
		{
			propertyDefinition
		}, propertyGroupDefinition, propertyReference, subGroupDelimiter)
		{
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000081C4 File Offset: 0x000063C4
		public SettingsPropertyDefinition(IEnumerable<IPropertyDefinitionBase> propertyDefinitions, IPropertyGroupDefinition propertyGroupDefinition, IRef propertyReference, char subGroupDelimiter)
		{
			this.SubGroupDelimiter = subGroupDelimiter;
			string[] groups = propertyGroupDefinition.GroupName.Split(new char[]
			{
				this.SubGroupDelimiter
			});
			this.GroupName = string.Join(this.SubGroupDelimiter.ToString(), groups);
			this.GroupOrder = propertyGroupDefinition.GroupOrder;
			this.PropertyReference = propertyReference;
			PropertyRef propertyRef = this.PropertyReference as PropertyRef;
			bool flag = propertyRef != null;
			if (flag)
			{
				this.Id = propertyRef.PropertyInfo.Name;
			}
			bool flag2 = this.PropertyReference.Type == typeof(bool);
			if (flag2)
			{
				this.SettingType = 0;
			}
			else
			{
				bool flag3 = this.PropertyReference.Type == typeof(int);
				if (flag3)
				{
					this.SettingType = 1;
				}
				else
				{
					bool flag4 = this.PropertyReference.Type == typeof(float);
					if (flag4)
					{
						this.SettingType = 2;
					}
					else
					{
						bool flag5 = this.PropertyReference.Type == typeof(string);
						if (flag5)
						{
							this.SettingType = 3;
						}
						else
						{
							bool flag6 = SettingsUtils.IsForGenericDropdown(this.PropertyReference.Type);
							if (flag6)
							{
								this.SettingType = 4;
							}
							else
							{
								bool flag7 = this.PropertyReference.Type == typeof(Action);
								if (flag7)
								{
									this.SettingType = 5;
								}
							}
						}
					}
				}
			}
			foreach (IPropertyDefinitionBase propertyDefinition in propertyDefinitions)
			{
				IPropertyDefinitionBase propertyBase;
				bool flag8;
				if (propertyDefinition != null)
				{
					propertyBase = propertyDefinition;
					flag8 = true;
				}
				else
				{
					flag8 = false;
				}
				bool flag9 = flag8;
				if (flag9)
				{
					this.DisplayName = propertyBase.DisplayName;
					this.Order = propertyBase.Order;
					this.RequireRestart = propertyBase.RequireRestart;
					this.HintText = propertyBase.HintText;
				}
				SettingPropertyAttribute settingPropertyAttribute = propertyDefinition as SettingPropertyAttribute;
				bool flag10 = settingPropertyAttribute != null;
				if (flag10)
				{
					this.MinValue = settingPropertyAttribute.MinValue;
					this.MaxValue = settingPropertyAttribute.MaxValue;
					this.EditableMinValue = settingPropertyAttribute.MinValue;
					this.EditableMaxValue = settingPropertyAttribute.MaxValue;
				}
				IPropertyDefinitionBool propertyDefinitionBool = propertyDefinition as IPropertyDefinitionBool;
				bool flag11 = propertyDefinitionBool != null;
				if (flag11)
				{
				}
				IPropertyDefinitionWithMinMax propertyDefinitionWithMinMax = propertyDefinition as IPropertyDefinitionWithMinMax;
				bool flag12 = propertyDefinitionWithMinMax != null;
				if (flag12)
				{
					this.MinValue = propertyDefinitionWithMinMax.MinValue;
					this.MaxValue = propertyDefinitionWithMinMax.MaxValue;
					this.EditableMinValue = propertyDefinitionWithMinMax.MinValue;
					this.EditableMaxValue = propertyDefinitionWithMinMax.MaxValue;
				}
				IPropertyDefinitionWithEditableMinMax propertyDefinitionWithEditableMinMax = propertyDefinition as IPropertyDefinitionWithEditableMinMax;
				bool flag13 = propertyDefinitionWithEditableMinMax != null;
				if (flag13)
				{
					this.EditableMinValue = propertyDefinitionWithEditableMinMax.EditableMinValue;
					this.EditableMaxValue = propertyDefinitionWithEditableMinMax.EditableMaxValue;
				}
				IPropertyDefinitionWithFormat propertyDefinitionWithFormat = propertyDefinition as IPropertyDefinitionWithFormat;
				bool flag14 = propertyDefinitionWithFormat != null;
				if (flag14)
				{
					this.ValueFormat = propertyDefinitionWithFormat.ValueFormat;
				}
				IPropertyDefinitionWithCustomFormatter propertyDefinitionWithCustomFormatter = propertyDefinition as IPropertyDefinitionWithCustomFormatter;
				bool flag15 = propertyDefinitionWithCustomFormatter != null;
				if (flag15)
				{
					this.CustomFormatter = propertyDefinitionWithCustomFormatter.CustomFormatter;
				}
				IPropertyDefinitionWithActionFormat propertyDefinitionWithActionFormat = propertyDefinition as IPropertyDefinitionWithActionFormat;
				bool flag16 = propertyDefinitionWithActionFormat != null;
				if (flag16)
				{
				}
				IPropertyDefinitionText propertyDefinitionText = propertyDefinition as IPropertyDefinitionText;
				bool flag17 = propertyDefinitionText != null;
				if (flag17)
				{
				}
				IPropertyDefinitionDropdown propertyDefinitionDropdown = propertyDefinition as IPropertyDefinitionDropdown;
				bool flag18 = propertyDefinitionDropdown != null;
				if (flag18)
				{
					this.SelectedIndex = propertyDefinitionDropdown.SelectedIndex;
				}
				IPropertyDefinitionWithId propertyDefinitionWithId = propertyDefinition as IPropertyDefinitionWithId;
				bool flag19 = propertyDefinitionWithId != null;
				if (flag19)
				{
					this.Id = propertyDefinitionWithId.Id;
				}
				IPropertyDefinitionGroupToggle propertyDefinitionGroupToggle = propertyDefinition as IPropertyDefinitionGroupToggle;
				bool flag20 = propertyDefinitionGroupToggle != null;
				if (flag20)
				{
					this.IsToggle = propertyDefinitionGroupToggle.IsToggle;
				}
				IPropertyDefinitionButton propertyDefinitionButton = propertyDefinition as IPropertyDefinitionButton;
				bool flag21 = propertyDefinitionButton != null;
				if (flag21)
				{
					this.Content = propertyDefinitionButton.Content;
				}
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000085F4 File Offset: 0x000067F4
		public override string ToString()
		{
			return "[" + this.GroupName + "]: " + this.DisplayName;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008614 File Offset: 0x00006814
		public SettingsPropertyDefinition Clone(bool keepRefs = true)
		{
			object localPropValue = this.PropertyReference.Value;
			IRef ref2;
			if (!keepRefs)
			{
				ICloneable cloneable = localPropValue as ICloneable;
				IRef @ref = new StorageRef((cloneable != null) ? cloneable.Clone() : localPropValue);
				ref2 = @ref;
			}
			else
			{
				ref2 = this.PropertyReference;
			}
			IRef value = ref2;
			return new SettingsPropertyDefinition(SettingsUtils.GetPropertyDefinitionWrappers(this), new PropertyGroupDefinitionWrapper(this), value, this.SubGroupDelimiter);
		}
	}
}
