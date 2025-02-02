﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001D1 RID: 465
	public class CharacterCreationMenu
	{
		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0007EDD9 File Offset: 0x0007CFD9
		public MBReadOnlyList<CharacterCreationCategory> CharacterCreationCategories
		{
			get
			{
				return this._characterCreationCategories;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0007EDE4 File Offset: 0x0007CFE4
		public CharacterCreationCategory AddMenuCategory(CharacterCreationOnCondition condition = null)
		{
			CharacterCreationCategory characterCreationCategory = new CharacterCreationCategory(condition);
			this._characterCreationCategories.Add(characterCreationCategory);
			return characterCreationCategory;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0007EE05 File Offset: 0x0007D005
		public CharacterCreationMenu(TextObject title, TextObject text, CharacterCreationOnInit onInit, CharacterCreationMenu.MenuTypes menuType = CharacterCreationMenu.MenuTypes.MultipleChoice)
		{
			this.Title = title;
			this.Text = text;
			this.OnInit = onInit;
			this.SelectedOptions = new List<int>();
			this._characterCreationCategories = new MBList<CharacterCreationCategory>();
			this.MenuType = menuType;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0007EE40 File Offset: 0x0007D040
		public void ApplyFinalEffect(CharacterCreation characterCreation)
		{
			using (List<int>.Enumerator enumerator = this.SelectedOptions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					int selectedOption = enumerator.Current;
					Predicate<CharacterCreationOption> <>9__0;
					foreach (CharacterCreationCategory characterCreationCategory in this.CharacterCreationCategories)
					{
						if (characterCreationCategory.CategoryCondition == null || characterCreationCategory.CategoryCondition())
						{
							List<CharacterCreationOption> characterCreationOptions = characterCreationCategory.CharacterCreationOptions;
							Predicate<CharacterCreationOption> match;
							if ((match = <>9__0) == null)
							{
								match = (<>9__0 = ((CharacterCreationOption o) => o.Id == selectedOption));
							}
							CharacterCreationOption characterCreationOption = characterCreationOptions.Find(match);
							if (characterCreationOption.ApplyFinalEffects != null)
							{
								MBReadOnlyList<SkillObject> affectedSkills = characterCreationOption.AffectedSkills;
								List<SkillObject> skills = (affectedSkills != null) ? affectedSkills.ToList<SkillObject>() : null;
								MBReadOnlyList<TraitObject> affectedTraits = characterCreationOption.AffectedTraits;
								List<TraitObject> traits = (affectedTraits != null) ? affectedTraits.ToList<TraitObject>() : null;
								CharacterCreationContentBase.Instance.ApplySkillAndAttributeEffects(skills, characterCreationOption.FocusToAdd, characterCreationOption.SkillLevelToAdd, characterCreationOption.EffectedAttribute, characterCreationOption.AttributeLevelToAdd, traits, characterCreationOption.TraitLevelToAdd, characterCreationOption.RenownToAdd, characterCreationOption.GoldToAdd, characterCreationOption.UnspentFocusToAdd, characterCreationOption.UnspentAttributeToAdd);
								characterCreationOption.ApplyFinalEffects(characterCreation);
							}
						}
					}
				}
			}
		}

		// Token: 0x040008C3 RID: 2243
		public readonly CharacterCreationMenu.MenuTypes MenuType;

		// Token: 0x040008C4 RID: 2244
		public readonly TextObject Title;

		// Token: 0x040008C5 RID: 2245
		public readonly TextObject Text;

		// Token: 0x040008C6 RID: 2246
		public readonly CharacterCreationOnInit OnInit;

		// Token: 0x040008C7 RID: 2247
		private readonly MBList<CharacterCreationCategory> _characterCreationCategories;

		// Token: 0x040008C8 RID: 2248
		public readonly List<int> SelectedOptions;

		// Token: 0x0200055E RID: 1374
		public enum MenuTypes
		{
			// Token: 0x040016B0 RID: 5808
			MultipleChoice,
			// Token: 0x040016B1 RID: 5809
			SelectAllThatApply
		}
	}
}
