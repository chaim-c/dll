using System;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000131 RID: 305
	public class CharacterCreationGainGroupItemVM : ViewModel
	{
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x0006A829 File Offset: 0x00068A29
		// (set) Token: 0x06001DA7 RID: 7591 RVA: 0x0006A831 File Offset: 0x00068A31
		public CharacterAttribute AttributeObj { get; private set; }

		// Token: 0x06001DA8 RID: 7592 RVA: 0x0006A83C File Offset: 0x00068A3C
		public CharacterCreationGainGroupItemVM(CharacterAttribute attributeObj, CharacterCreation characterCreation, int currentIndex)
		{
			this.AttributeObj = attributeObj;
			this._characterCreation = characterCreation;
			this._currentIndex = currentIndex;
			this.Skills = new MBBindingList<CharacterCreationGainedSkillItemVM>();
			this.Attribute = new CharacterCreationGainedAttributeItemVM(this.AttributeObj);
			foreach (SkillObject skill in this.AttributeObj.Skills)
			{
				this.Skills.Add(new CharacterCreationGainedSkillItemVM(skill));
			}
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x0006A8D8 File Offset: 0x00068AD8
		public void ResetValues()
		{
			this.Attribute.ResetValues();
			this.Skills.ApplyActionOnAllItems(delegate(CharacterCreationGainedSkillItemVM s)
			{
				s.ResetValues();
			});
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x0006A90F File Offset: 0x00068B0F
		// (set) Token: 0x06001DAB RID: 7595 RVA: 0x0006A917 File Offset: 0x00068B17
		[DataSourceProperty]
		public MBBindingList<CharacterCreationGainedSkillItemVM> Skills
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
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationGainedSkillItemVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0006A935 File Offset: 0x00068B35
		// (set) Token: 0x06001DAD RID: 7597 RVA: 0x0006A93D File Offset: 0x00068B3D
		[DataSourceProperty]
		public CharacterCreationGainedAttributeItemVM Attribute
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
					base.OnPropertyChangedWithValue<CharacterCreationGainedAttributeItemVM>(value, "Attribute");
				}
			}
		}

		// Token: 0x04000DFC RID: 3580
		private readonly CharacterCreation _characterCreation;

		// Token: 0x04000DFD RID: 3581
		private readonly int _currentIndex;

		// Token: 0x04000DFE RID: 3582
		private MBBindingList<CharacterCreationGainedSkillItemVM> _skills;

		// Token: 0x04000DFF RID: 3583
		private CharacterCreationGainedAttributeItemVM _attribute;
	}
}
