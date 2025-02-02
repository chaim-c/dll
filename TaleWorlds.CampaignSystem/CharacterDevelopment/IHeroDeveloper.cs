using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CharacterDevelopment
{
	// Token: 0x02000351 RID: 849
	public interface IHeroDeveloper
	{
		// Token: 0x06003035 RID: 12341
		void SetInitialLevel(int i);

		// Token: 0x06003036 RID: 12342
		void AddSkillXp(SkillObject skill, float rawXp, bool isAffectedByFocusFactor = true, bool shouldNotify = true);

		// Token: 0x06003037 RID: 12343
		void InitializeHeroDeveloper(bool isByNaturalGrowth = false, CharacterObject template = null);

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003038 RID: 12344
		// (set) Token: 0x06003039 RID: 12345
		int UnspentFocusPoints { get; set; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600303A RID: 12346
		// (set) Token: 0x0600303B RID: 12347
		int UnspentAttributePoints { get; set; }

		// Token: 0x0600303C RID: 12348
		void ClearUnspentPoints();

		// Token: 0x0600303D RID: 12349
		void AddFocus(SkillObject skill, int changeAmount, bool checkUnspentFocusPoints = true);

		// Token: 0x0600303E RID: 12350
		void RemoveFocus(SkillObject skill, int changeAmount);

		// Token: 0x0600303F RID: 12351
		void SetInitialSkillLevel(SkillObject skill, int newSkillValue);

		// Token: 0x06003040 RID: 12352
		void InitializeSkillXp(SkillObject skill);

		// Token: 0x06003041 RID: 12353
		void ClearHero();

		// Token: 0x06003042 RID: 12354
		void AddPerk(PerkObject perk);

		// Token: 0x06003043 RID: 12355
		float GetFocusFactor(SkillObject skill);

		// Token: 0x06003044 RID: 12356
		void AddAttribute(CharacterAttribute attribute, int changeAmount, bool checkUnspentPoints = true);

		// Token: 0x06003045 RID: 12357
		void RemoveAttribute(CharacterAttribute attrib, int changeAmount);

		// Token: 0x06003046 RID: 12358
		void ChangeSkillLevel(SkillObject skill, int changeAmount, bool shouldNotify = true);

		// Token: 0x06003047 RID: 12359
		int GetFocus(SkillObject skill);

		// Token: 0x06003048 RID: 12360
		bool CanAddFocusToSkill(SkillObject skill);

		// Token: 0x06003049 RID: 12361
		void AfterLoad();

		// Token: 0x0600304A RID: 12362
		int GetTotalSkillPoints();

		// Token: 0x0600304B RID: 12363
		int GetXpRequiredForLevel(int level);

		// Token: 0x0600304C RID: 12364
		int GetRequiredFocusPointsToAddFocus(SkillObject skill);

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600304D RID: 12365
		int TotalXp { get; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600304E RID: 12366
		Hero Hero { get; }

		// Token: 0x0600304F RID: 12367
		int GetSkillXpProgress(SkillObject skill);

		// Token: 0x06003050 RID: 12368
		bool GetPerkValue(PerkObject perk);

		// Token: 0x06003051 RID: 12369
		void DevelopCharacterStats();
	}
}
