using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000132 RID: 306
	public class CharacterCreationGainedSkillItemVM : ViewModel
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0006A95B File Offset: 0x00068B5B
		// (set) Token: 0x06001DAF RID: 7599 RVA: 0x0006A963 File Offset: 0x00068B63
		public SkillObject SkillObj { get; private set; }

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0006A96C File Offset: 0x00068B6C
		public CharacterCreationGainedSkillItemVM(SkillObject skill)
		{
			this.FocusPointGainList = new MBBindingList<BoolItemWithActionVM>();
			this.SkillObj = skill;
			this.SkillId = this.SkillObj.StringId;
			this.Skill = new EncyclopediaSkillVM(skill, 0);
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x0006A9A4 File Offset: 0x00068BA4
		public void SetValue(int gainedFromOtherStages, int gainedFromCurrentStage)
		{
			this.FocusPointGainList.Clear();
			for (int i = 0; i < gainedFromOtherStages; i++)
			{
				this.FocusPointGainList.Add(new BoolItemWithActionVM(null, false, null));
			}
			for (int j = 0; j < gainedFromCurrentStage; j++)
			{
				this.FocusPointGainList.Add(new BoolItemWithActionVM(null, true, null));
			}
			this.HasIncreasedInCurrentStage = (gainedFromCurrentStage > 0);
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0006AA04 File Offset: 0x00068C04
		internal void ResetValues()
		{
			this.SetValue(0, 0);
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0006AA0E File Offset: 0x00068C0E
		// (set) Token: 0x06001DB4 RID: 7604 RVA: 0x0006AA16 File Offset: 0x00068C16
		[DataSourceProperty]
		public string SkillId
		{
			get
			{
				return this._skillId;
			}
			set
			{
				if (value != this._skillId)
				{
					this._skillId = value;
					base.OnPropertyChangedWithValue<string>(value, "SkillId");
				}
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x0006AA39 File Offset: 0x00068C39
		// (set) Token: 0x06001DB6 RID: 7606 RVA: 0x0006AA41 File Offset: 0x00068C41
		[DataSourceProperty]
		public EncyclopediaSkillVM Skill
		{
			get
			{
				return this._skill;
			}
			set
			{
				if (value != this._skill)
				{
					this._skill = value;
					base.OnPropertyChangedWithValue<EncyclopediaSkillVM>(value, "Skill");
				}
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x0006AA5F File Offset: 0x00068C5F
		// (set) Token: 0x06001DB8 RID: 7608 RVA: 0x0006AA67 File Offset: 0x00068C67
		[DataSourceProperty]
		public bool HasIncreasedInCurrentStage
		{
			get
			{
				return this._hasIncreasedInCurrentStage;
			}
			set
			{
				if (value != this._hasIncreasedInCurrentStage)
				{
					this._hasIncreasedInCurrentStage = value;
					base.OnPropertyChangedWithValue(value, "HasIncreasedInCurrentStage");
				}
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0006AA85 File Offset: 0x00068C85
		// (set) Token: 0x06001DBA RID: 7610 RVA: 0x0006AA8D File Offset: 0x00068C8D
		[DataSourceProperty]
		public MBBindingList<BoolItemWithActionVM> FocusPointGainList
		{
			get
			{
				return this._focusPointGainList;
			}
			set
			{
				if (value != this._focusPointGainList)
				{
					this._focusPointGainList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BoolItemWithActionVM>>(value, "FocusPointGainList");
				}
			}
		}

		// Token: 0x04000E01 RID: 3585
		private string _skillId;

		// Token: 0x04000E02 RID: 3586
		private EncyclopediaSkillVM _skill;

		// Token: 0x04000E03 RID: 3587
		private bool _hasIncreasedInCurrentStage;

		// Token: 0x04000E04 RID: 3588
		private MBBindingList<BoolItemWithActionVM> _focusPointGainList;
	}
}
