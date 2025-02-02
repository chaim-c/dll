using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CharacterDevelopment;

namespace SandBox.ViewModelCollection
{
	// Token: 0x02000006 RID: 6
	public class SkillObjectComparer : IComparer<PerkObject>
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000041F8 File Offset: 0x000023F8
		public int Compare(PerkObject x, PerkObject y)
		{
			int skillObjectTypeSortIndex = SandBoxUIHelper.GetSkillObjectTypeSortIndex(x.Skill);
			int num = SandBoxUIHelper.GetSkillObjectTypeSortIndex(y.Skill).CompareTo(skillObjectTypeSortIndex);
			if (num != 0)
			{
				return num;
			}
			return this.ResolveEquality(x, y);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004234 File Offset: 0x00002434
		private int ResolveEquality(PerkObject x, PerkObject y)
		{
			return x.RequiredSkillValue.CompareTo(y.RequiredSkillValue);
		}
	}
}
