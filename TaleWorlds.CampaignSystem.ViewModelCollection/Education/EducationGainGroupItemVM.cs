using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Education
{
	// Token: 0x020000D4 RID: 212
	public class EducationGainGroupItemVM : ViewModel
	{
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0004CBA7 File Offset: 0x0004ADA7
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x0004CBAF File Offset: 0x0004ADAF
		public CharacterAttribute AttributeObj { get; private set; }

		// Token: 0x060013F7 RID: 5111 RVA: 0x0004CBB8 File Offset: 0x0004ADB8
		public EducationGainGroupItemVM(CharacterAttribute attributeObj)
		{
			this.AttributeObj = attributeObj;
			this.Skills = new MBBindingList<EducationGainedSkillItemVM>();
			this.Attribute = new EducationGainedAttributeItemVM(this.AttributeObj);
			foreach (SkillObject skill in this.AttributeObj.Skills)
			{
				this.Skills.Add(new EducationGainedSkillItemVM(skill));
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0004CC44 File Offset: 0x0004AE44
		public void ResetValues()
		{
			this.Attribute.ResetValues();
			this.Skills.ApplyActionOnAllItems(delegate(EducationGainedSkillItemVM s)
			{
				s.ResetValues();
			});
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0004CC7B File Offset: 0x0004AE7B
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x0004CC83 File Offset: 0x0004AE83
		[DataSourceProperty]
		public MBBindingList<EducationGainedSkillItemVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<EducationGainedSkillItemVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0004CCA1 File Offset: 0x0004AEA1
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x0004CCA9 File Offset: 0x0004AEA9
		[DataSourceProperty]
		public EducationGainedAttributeItemVM Attribute
		{
			get
			{
				return this._attribute;
			}
			set
			{
				if (value != this._attribute)
				{
					this._attribute = value;
					base.OnPropertyChangedWithValue<EducationGainedAttributeItemVM>(value, "Attribute");
				}
			}
		}

		// Token: 0x0400093C RID: 2364
		private MBBindingList<EducationGainedSkillItemVM> _skills;

		// Token: 0x0400093D RID: 2365
		private EducationGainedAttributeItemVM _attribute;
	}
}
