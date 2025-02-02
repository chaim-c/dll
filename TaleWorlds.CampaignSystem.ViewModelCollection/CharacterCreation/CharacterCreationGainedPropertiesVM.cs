using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x02000130 RID: 304
	public class CharacterCreationGainedPropertiesVM : ViewModel
	{
		// Token: 0x06001D9B RID: 7579 RVA: 0x0006A11C File Offset: 0x0006831C
		public CharacterCreationGainedPropertiesVM(CharacterCreation characterCreation, int currentIndex)
		{
			this._characterCreation = characterCreation;
			this._currentIndex = currentIndex;
			this._affectedAttributesMap = new Dictionary<CharacterAttribute, Tuple<int, int>>();
			this._affectedSkillMap = new Dictionary<SkillObject, Tuple<int, int>>();
			this.GainGroups = new MBBindingList<CharacterCreationGainGroupItemVM>();
			foreach (CharacterAttribute attributeObj in Attributes.All)
			{
				this.GainGroups.Add(new CharacterCreationGainGroupItemVM(attributeObj, this._characterCreation, this._currentIndex));
			}
			this.GainedTraits = new MBBindingList<EncyclopediaTraitItemVM>();
			this.UpdateValues();
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0006A1CC File Offset: 0x000683CC
		public void UpdateValues()
		{
			this._affectedAttributesMap.Clear();
			this._affectedSkillMap.Clear();
			this.GainGroups.ApplyActionOnAllItems(delegate(CharacterCreationGainGroupItemVM g)
			{
				g.ResetValues();
			});
			this.PopulateInitialValues();
			this.PopulateGainedAttributeValues();
			this.PopulateGainedTraitValues();
			foreach (KeyValuePair<CharacterAttribute, Tuple<int, int>> keyValuePair in this._affectedAttributesMap)
			{
				this.GetItemFromAttribute(keyValuePair.Key).SetValue(keyValuePair.Value.Item1, keyValuePair.Value.Item2);
			}
			foreach (KeyValuePair<SkillObject, Tuple<int, int>> keyValuePair2 in this._affectedSkillMap)
			{
				this.GetItemFromSkill(keyValuePair2.Key).SetValue(keyValuePair2.Value.Item1, keyValuePair2.Value.Item2);
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0006A2FC File Offset: 0x000684FC
		private void PopulateInitialValues()
		{
			foreach (SkillObject skillObject in Skills.All)
			{
				int focus = Hero.MainHero.HeroDeveloper.GetFocus(skillObject);
				if (this._affectedSkillMap.ContainsKey(skillObject))
				{
					Tuple<int, int> tuple = this._affectedSkillMap[skillObject];
					this._affectedSkillMap[skillObject] = new Tuple<int, int>(tuple.Item1 + focus, 0);
				}
				else
				{
					this._affectedSkillMap.Add(skillObject, new Tuple<int, int>(focus, 0));
				}
			}
			foreach (CharacterAttribute characterAttribute in Attributes.All)
			{
				int attributeValue = Hero.MainHero.GetAttributeValue(characterAttribute);
				if (this._affectedAttributesMap.ContainsKey(characterAttribute))
				{
					Tuple<int, int> tuple2 = this._affectedAttributesMap[characterAttribute];
					this._affectedAttributesMap[characterAttribute] = new Tuple<int, int>(tuple2.Item1 + attributeValue, 0);
				}
				else
				{
					this._affectedAttributesMap.Add(characterAttribute, new Tuple<int, int>(attributeValue, 0));
				}
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0006A440 File Offset: 0x00068640
		private void PopulateGainedAttributeValues()
		{
			for (int i = 0; i < this._characterCreation.CharacterCreationMenuCount; i++)
			{
				int selectedOptionId = this._characterCreation.GetSelectedOptions(i).Any<int>() ? this._characterCreation.GetSelectedOptions(i).First<int>() : -1;
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				if (selectedOptionId != -1)
				{
					CharacterCreationOption characterCreationOption = this._characterCreation.GetCurrentMenuOptions(i).SingleOrDefault((CharacterCreationOption o) => o.Id == selectedOptionId);
					if (characterCreationOption != null)
					{
						if (i == this._currentIndex)
						{
							num3 = characterCreationOption.AttributeLevelToAdd;
						}
						else
						{
							num4 += characterCreationOption.AttributeLevelToAdd;
						}
						if (characterCreationOption.EffectedAttribute != null)
						{
							if (this._affectedAttributesMap.ContainsKey(characterCreationOption.EffectedAttribute))
							{
								Tuple<int, int> tuple = this._affectedAttributesMap[characterCreationOption.EffectedAttribute];
								this._affectedAttributesMap[characterCreationOption.EffectedAttribute] = new Tuple<int, int>(tuple.Item1 + num4, tuple.Item2 + num3);
							}
							else
							{
								this._affectedAttributesMap.Add(characterCreationOption.EffectedAttribute, new Tuple<int, int>(num4, num3));
							}
						}
						if (i == this._currentIndex)
						{
							num = characterCreationOption.FocusToAdd;
						}
						else
						{
							num2 += characterCreationOption.FocusToAdd;
						}
						foreach (SkillObject key in characterCreationOption.AffectedSkills)
						{
							if (this._affectedSkillMap.ContainsKey(key))
							{
								Tuple<int, int> tuple2 = this._affectedSkillMap[key];
								this._affectedSkillMap[key] = new Tuple<int, int>(tuple2.Item1 + num2, tuple2.Item2 + num);
							}
							else
							{
								this._affectedSkillMap.Add(key, new Tuple<int, int>(num2, num));
							}
						}
					}
				}
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0006A628 File Offset: 0x00068828
		private void PopulateGainedTraitValues()
		{
			this.GainedTraits.Clear();
			for (int i = 0; i < this._characterCreation.CharacterCreationMenuCount; i++)
			{
				int selectedOptionId = this._characterCreation.GetSelectedOptions(i).Any<int>() ? this._characterCreation.GetSelectedOptions(i).First<int>() : -1;
				if (selectedOptionId != -1)
				{
					CharacterCreationOption characterCreationOption = this._characterCreation.GetCurrentMenuOptions(i).SingleOrDefault((CharacterCreationOption o) => o.Id == selectedOptionId);
					if (((characterCreationOption != null) ? characterCreationOption.AffectedTraits : null) != null && characterCreationOption.AffectedTraits.Count > 0)
					{
						using (List<TraitObject>.Enumerator enumerator = characterCreationOption.AffectedTraits.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								TraitObject effectedTrait = enumerator.Current;
								if (this.GainedTraits.FirstOrDefault((EncyclopediaTraitItemVM t) => t.TraitId == effectedTrait.StringId) == null)
								{
									this.GainedTraits.Add(new EncyclopediaTraitItemVM(effectedTrait, 1));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0006A754 File Offset: 0x00068954
		private CharacterCreationGainedAttributeItemVM GetItemFromAttribute(CharacterAttribute attribute)
		{
			CharacterCreationGainGroupItemVM characterCreationGainGroupItemVM = this.GainGroups.SingleOrDefault((CharacterCreationGainGroupItemVM g) => g.AttributeObj == attribute);
			if (characterCreationGainGroupItemVM == null)
			{
				return null;
			}
			return characterCreationGainGroupItemVM.Attribute;
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0006A790 File Offset: 0x00068990
		private CharacterCreationGainedSkillItemVM GetItemFromSkill(SkillObject skill)
		{
			Func<CharacterCreationGainedSkillItemVM, bool> <>9__2;
			CharacterCreationGainGroupItemVM characterCreationGainGroupItemVM = this.GainGroups.SingleOrDefault(delegate(CharacterCreationGainGroupItemVM g)
			{
				IEnumerable<CharacterCreationGainedSkillItemVM> skills = g.Skills;
				Func<CharacterCreationGainedSkillItemVM, bool> predicate;
				if ((predicate = <>9__2) == null)
				{
					predicate = (<>9__2 = ((CharacterCreationGainedSkillItemVM s) => s.SkillObj == skill));
				}
				return skills.SingleOrDefault(predicate) != null;
			});
			if (characterCreationGainGroupItemVM == null)
			{
				return null;
			}
			return characterCreationGainGroupItemVM.Skills.SingleOrDefault((CharacterCreationGainedSkillItemVM s) => s.SkillObj == skill);
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001DA2 RID: 7586 RVA: 0x0006A7DD File Offset: 0x000689DD
		// (set) Token: 0x06001DA3 RID: 7587 RVA: 0x0006A7E5 File Offset: 0x000689E5
		[DataSourceProperty]
		public MBBindingList<CharacterCreationGainGroupItemVM> GainGroups
		{
			get
			{
				return this._gainGroups;
			}
			set
			{
				if (value != this._gainGroups)
				{
					this._gainGroups = value;
					base.OnPropertyChangedWithValue<MBBindingList<CharacterCreationGainGroupItemVM>>(value, "GainGroups");
				}
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x0006A803 File Offset: 0x00068A03
		// (set) Token: 0x06001DA5 RID: 7589 RVA: 0x0006A80B File Offset: 0x00068A0B
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTraitItemVM> GainedTraits
		{
			get
			{
				return this._gainedTraits;
			}
			set
			{
				if (value != this._gainedTraits)
				{
					this._gainedTraits = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTraitItemVM>>(value, "GainedTraits");
				}
			}
		}

		// Token: 0x04000DF5 RID: 3573
		private readonly CharacterCreation _characterCreation;

		// Token: 0x04000DF6 RID: 3574
		private readonly int _currentIndex;

		// Token: 0x04000DF7 RID: 3575
		private readonly Dictionary<CharacterAttribute, Tuple<int, int>> _affectedAttributesMap;

		// Token: 0x04000DF8 RID: 3576
		private readonly Dictionary<SkillObject, Tuple<int, int>> _affectedSkillMap;

		// Token: 0x04000DF9 RID: 3577
		private MBBindingList<CharacterCreationGainGroupItemVM> _gainGroups;

		// Token: 0x04000DFA RID: 3578
		private MBBindingList<EncyclopediaTraitItemVM> _gainedTraits;
	}
}
