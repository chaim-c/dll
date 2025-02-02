using System;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items
{
	// Token: 0x020000CE RID: 206
	public class EncyclopediaSkillVM : ViewModel
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x0004BCCF File Offset: 0x00049ECF
		public EncyclopediaSkillVM(SkillObject skill, int skillValue)
		{
			this._skill = skill;
			this.SkillValue = skillValue;
			this.SkillId = skill.StringId;
			this.RefreshValues();
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004BCF8 File Offset: 0x00049EF8
		public override void RefreshValues()
		{
			base.RefreshValues();
			string name = this._skill.Name.ToString();
			string desc = this._skill.Description.ToString();
			this.Hint = new BasicTooltipViewModel(delegate()
			{
				GameTexts.SetVariable("STR1", name);
				GameTexts.SetVariable("STR2", desc);
				return GameTexts.FindText("str_string_newline_string", null).ToString();
			});
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004BD54 File Offset: 0x00049F54
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x0004BD5C File Offset: 0x00049F5C
		[DataSourceProperty]
		public BasicTooltipViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0004BD7A File Offset: 0x00049F7A
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x0004BD82 File Offset: 0x00049F82
		[DataSourceProperty]
		public int SkillValue
		{
			get
			{
				return this._skillValue;
			}
			set
			{
				if (value != this._skillValue)
				{
					this._skillValue = value;
					base.OnPropertyChangedWithValue(value, "SkillValue");
				}
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0004BDA0 File Offset: 0x00049FA0
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0004BDA8 File Offset: 0x00049FA8
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

		// Token: 0x0400091E RID: 2334
		private readonly SkillObject _skill;

		// Token: 0x0400091F RID: 2335
		private string _skillId;

		// Token: 0x04000920 RID: 2336
		private int _skillValue;

		// Token: 0x04000921 RID: 2337
		private BasicTooltipViewModel _hint;
	}
}
