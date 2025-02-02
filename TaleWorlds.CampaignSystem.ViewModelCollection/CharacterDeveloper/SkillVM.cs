using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000126 RID: 294
	public class SkillVM : ViewModel
	{
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x00067E6E File Offset: 0x0006606E
		private int _boundAttributeCurrentValue
		{
			get
			{
				return this._developerVM.GetCurrentAttributePoint(this.Skill.CharacterAttribute);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00067E86 File Offset: 0x00066086
		private int _heroLevel
		{
			get
			{
				return this._developerVM.Hero.CharacterObject.Level;
			}
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x00067EA0 File Offset: 0x000660A0
		public SkillVM(SkillObject skill, CharacterVM developerVM, Action<PerkVM> onStartPerkSelection)
		{
			this._developerVM = developerVM;
			this.Skill = skill;
			this.MaxLevel = 300;
			this.SkillId = skill.StringId;
			this._onStartPerkSelection = onStartPerkSelection;
			this.IsInspected = false;
			this.Type = (skill.IsPartySkill ? SkillVM.SkillType.Party : (skill.IsLeaderSkill ? SkillVM.SkillType.Leader : SkillVM.SkillType.Default)).ToString();
			this.SkillEffects = new MBBindingList<BindingListStringItem>();
			this.Perks = new MBBindingList<PerkVM>();
			this.AddFocusHint = new HintViewModel();
			this._boundAttributeName = this.Skill.CharacterAttribute.Name;
			this.LearningRateTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetLearningRateTooltip(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this.Level, this._heroLevel, this._boundAttributeName));
			this.LearningLimitTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetLearningLimitTooltip(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this._boundAttributeName));
			this.InitializeValues();
			this._focusConceptObj = Concept.All.SingleOrDefault((Concept c) => c.StringId == "str_game_objects_skill_focus");
			this._skillConceptObj = Concept.All.SingleOrDefault((Concept c) => c.StringId == "str_game_objects_skills");
			this.RefreshValues();
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x00067FE8 File Offset: 0x000661E8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.AddFocusText = GameTexts.FindText("str_add_focus", null).ToString();
			this.HowToLearnText = this.Skill.HowToLearnSkillText.ToString();
			this.HowToLearnTitle = GameTexts.FindText("str_how_to_learn", null).ToString();
			this.DescriptionText = this.Skill.Description.ToString();
			this.NameText = this.Skill.Name.ToString();
			this.InitializeValues();
			this.RefreshWithCurrentValues();
			this.SkillEffects.ApplyActionOnAllItems(delegate(BindingListStringItem x)
			{
				x.RefreshValues();
			});
			this.Perks.ApplyActionOnAllItems(delegate(PerkVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000680CC File Offset: 0x000662CC
		public void InitializeValues()
		{
			if (this._developerVM.GetCharacterDeveloper() == null)
			{
				this.Level = 0;
			}
			else
			{
				this.Level = this._developerVM.GetCharacterDeveloper().Hero.GetSkillValue(this.Skill);
				this.NextLevel = this.Level + 1;
				this.CurrentSkillXP = this._developerVM.GetCharacterDeveloper().GetSkillXpProgress(this.Skill);
				this.XpRequiredForNextLevel = Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(this.Level + 1) - Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(this.Level);
				this.ProgressPercentage = 100.0 * (double)this._currentSkillXP / (double)this.XpRequiredForNextLevel;
				this.ProgressHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("CURRENT_XP", this.CurrentSkillXP.ToString());
					GameTexts.SetVariable("LEVEL_MAX_XP", this.XpRequiredForNextLevel.ToString());
					return GameTexts.FindText("str_current_xp_over_max", null).ToString();
				});
				GameTexts.SetVariable("CURRENT_XP", this.CurrentSkillXP.ToString());
				GameTexts.SetVariable("LEVEL_MAX_XP", this.XpRequiredForNextLevel.ToString());
				this.ProgressText = GameTexts.FindText("str_current_xp_over_max", null).ToString();
				this.SkillXPHint = new BasicTooltipViewModel(delegate()
				{
					GameTexts.SetVariable("REQUIRED_XP_FOR_NEXT_LEVEL", this.XpRequiredForNextLevel - this.CurrentSkillXP);
					return GameTexts.FindText("str_skill_xp_hint", null).ToString();
				});
			}
			this._orgFocusAmount = this._developerVM.GetCharacterDeveloper().GetFocus(this.Skill);
			this.CurrentFocusLevel = this._orgFocusAmount;
			this.CreateLists();
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0006823C File Offset: 0x0006643C
		public void RefreshWithCurrentValues()
		{
			float resultNumber = Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningRate(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this.Level, this._heroLevel, this._boundAttributeName, false).ResultNumber;
			GameTexts.SetVariable("COUNT", resultNumber.ToString("0.00"));
			this.CurrentLearningRateText = GameTexts.FindText("str_learning_rate_COUNT", null).ToString();
			this.CanLearnSkill = (resultNumber > 0f);
			this.LearningRate = resultNumber;
			this.FullLearningRateLevel = MathF.Round(Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningLimit(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this._boundAttributeName, false).ResultNumber);
			int requiredFocusPointsToAddFocusWithCurrentFocus = this._developerVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this.Skill);
			GameTexts.SetVariable("COSTAMOUNT", requiredFocusPointsToAddFocusWithCurrentFocus);
			this.FocusCostText = requiredFocusPointsToAddFocusWithCurrentFocus.ToString();
			GameTexts.SetVariable("COUNT", requiredFocusPointsToAddFocusWithCurrentFocus);
			GameTexts.SetVariable("RIGHT", "");
			GameTexts.SetVariable("LEFT", GameTexts.FindText("str_cost_COUNT", null));
			MBTextManager.SetTextVariable("FOCUS_ICON", GameTexts.FindText("str_html_focus_icon", null), false);
			this.NextLevelCostText = GameTexts.FindText("str_sf_text_with_focus_icon", null).ToString();
			this.RefreshCanAddFocus();
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0006838C File Offset: 0x0006658C
		public void CreateLists()
		{
			this.SkillEffects.Clear();
			this.Perks.Clear();
			int skillValue = this._developerVM.GetCharacterDeveloper().Hero.GetSkillValue(this.Skill);
			foreach (SkillEffect effect in from x in SkillEffect.All
			where x.EffectedSkills.Contains(this.Skill)
			select x)
			{
				this.SkillEffects.Add(new BindingListStringItem(CampaignUIHelper.GetSkillEffectText(effect, skillValue)));
			}
			foreach (PerkObject perkObject in from p in PerkObject.All
			where p.Skill == this.Skill
			orderby p.RequiredSkillValue
			select p)
			{
				PerkVM.PerkAlternativeType alternativeType = (perkObject.AlternativePerk == null) ? PerkVM.PerkAlternativeType.NoAlternative : ((perkObject.StringId.CompareTo(perkObject.AlternativePerk.StringId) < 0) ? PerkVM.PerkAlternativeType.FirstAlternative : PerkVM.PerkAlternativeType.SecondAlternative);
				PerkVM item = new PerkVM(perkObject, this.IsPerkAvailable(perkObject), alternativeType, new Action<PerkVM>(this.OnStartPerkSelection), new Action<PerkVM>(this.OnPerkSelectionOver), new Func<PerkObject, bool>(this.IsPerkSelected), new Func<PerkObject, bool>(this.IsPreviousPerkSelected));
				this.Perks.Add(item);
			}
			this.RefreshNumOfUnopenedPerks();
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00068520 File Offset: 0x00066720
		public void RefreshLists(SkillObject skill = null)
		{
			if (skill != null && skill != this.Skill)
			{
				return;
			}
			foreach (PerkVM perkVM in this.Perks)
			{
				perkVM.RefreshState();
			}
			this.RefreshNumOfUnopenedPerks();
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x00068580 File Offset: 0x00066780
		private void RefreshNumOfUnopenedPerks()
		{
			int num = 0;
			foreach (PerkVM perkVM in this.Perks)
			{
				if ((perkVM.CurrentState == PerkVM.PerkStates.EarnedButNotSelected || perkVM.CurrentState == PerkVM.PerkStates.EarnedPreviousPerkNotSelected) && (perkVM.AlternativeType == 1 || perkVM.AlternativeType == 0))
				{
					num++;
				}
			}
			this.NumOfUnopenedPerks = num;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000685F8 File Offset: 0x000667F8
		private bool IsPerkSelected(PerkObject perk)
		{
			return this._developerVM.GetCharacterDeveloper().GetPerkValue(perk) || this._developerVM.PerkSelection.IsPerkSelected(perk);
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00068620 File Offset: 0x00066820
		private bool IsPreviousPerkSelected(PerkObject perk)
		{
			IEnumerable<PerkObject> source = from p in PerkObject.All
			where p.Skill == perk.Skill && p.RequiredSkillValue < perk.RequiredSkillValue
			select p;
			if (!source.Any<PerkObject>())
			{
				return true;
			}
			PerkObject perkObject = source.MaxBy((PerkObject p) => p.RequiredSkillValue - perk.RequiredSkillValue);
			return this.IsPerkSelected(perkObject) || (perkObject.AlternativePerk != null && this.IsPerkSelected(perkObject.AlternativePerk));
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006868F File Offset: 0x0006688F
		private bool IsPerkAvailable(PerkObject perk)
		{
			return perk.RequiredSkillValue <= (float)this.Level;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000686A4 File Offset: 0x000668A4
		public void RefreshCanAddFocus()
		{
			bool playerHasEnoughPoints = this._developerVM.UnspentCharacterPoints >= this._developerVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this.Skill);
			bool isMaxedSkill = this._currentFocusLevel >= Campaign.Current.Models.CharacterDevelopmentModel.MaxFocusPerSkill;
			string addFocusHintString = CampaignUIHelper.GetAddFocusHintString(playerHasEnoughPoints, isMaxedSkill, this.CurrentFocusLevel, this._boundAttributeCurrentValue, this.Level, this._developerVM.GetCharacterDeveloper(), this.Skill);
			this.AddFocusHint.HintText = (string.IsNullOrEmpty(addFocusHintString) ? TextObject.Empty : new TextObject("{=!}" + addFocusHintString, null));
			this.CanAddFocus = this._developerVM.CanAddFocusToSkillWithFocusAmount(this._currentFocusLevel);
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00068760 File Offset: 0x00066960
		public void ExecuteAddFocus()
		{
			if (this.CanAddFocus)
			{
				this._developerVM.UnspentCharacterPoints -= this._developerVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this.Skill);
				int currentFocusLevel = this.CurrentFocusLevel;
				this.CurrentFocusLevel = currentFocusLevel + 1;
				this._developerVM.RefreshCharacterValues();
				this.RefreshWithCurrentValues();
				MBInformationManager.HideInformations();
				Game.Current.EventManager.TriggerEvent<FocusAddedByPlayerEvent>(new FocusAddedByPlayerEvent(this._developerVM.Hero, this.Skill));
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000687E3 File Offset: 0x000669E3
		public void ExecuteShowFocusConcept()
		{
			if (this._focusConceptObj != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._focusConceptObj.EncyclopediaLink);
				return;
			}
			Debug.FailedAssert("Couldn't find Focus encyclopedia page", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterDeveloper\\SkillVM.cs", "ExecuteShowFocusConcept", 259);
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x00068821 File Offset: 0x00066A21
		public void ExecuteShowSkillConcept()
		{
			if (this._focusConceptObj != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this._skillConceptObj.EncyclopediaLink);
				return;
			}
			Debug.FailedAssert("Couldn't find Focus encyclopedia page", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem.ViewModelCollection\\CharacterDeveloper\\SkillVM.cs", "ExecuteShowSkillConcept", 271);
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0006885F File Offset: 0x00066A5F
		public void ExecuteInspect()
		{
			this._developerVM.SetCurrentSkill(this);
			this.RefreshCanAddFocus();
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00068873 File Offset: 0x00066A73
		public void ResetChanges()
		{
			this.CurrentFocusLevel = this._orgFocusAmount;
			this.Perks.ApplyActionOnAllItems(delegate(PerkVM p)
			{
				p.RefreshState();
			});
			this.RefreshNumOfUnopenedPerks();
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000688B1 File Offset: 0x00066AB1
		public bool IsThereAnyChanges()
		{
			return this.CurrentFocusLevel != this._orgFocusAmount;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000688C4 File Offset: 0x00066AC4
		public void ApplyChanges()
		{
			for (int i = 0; i < this.CurrentFocusLevel - this._orgFocusAmount; i++)
			{
				this._developerVM.GetCharacterDeveloper().AddFocus(this.Skill, 1, true);
			}
			this._orgFocusAmount = this.CurrentFocusLevel;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00068910 File Offset: 0x00066B10
		private void OnStartPerkSelection(PerkVM perk)
		{
			this._onStartPerkSelection(perk);
			if (perk.AlternativeType != 0)
			{
				this.Perks.SingleOrDefault((PerkVM p) => p.Perk == perk.Perk.AlternativePerk).IsInSelection = true;
			}
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00068968 File Offset: 0x00066B68
		private void OnPerkSelectionOver(PerkVM perk)
		{
			if (perk.AlternativeType != 0)
			{
				this.Perks.SingleOrDefault((PerkVM p) => p.Perk == perk.Perk.AlternativePerk).IsInSelection = false;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000689AC File Offset: 0x00066BAC
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x000689B4 File Offset: 0x00066BB4
		[DataSourceProperty]
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue<string>(value, "Type");
				}
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000689D7 File Offset: 0x00066BD7
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x000689DF File Offset: 0x00066BDF
		[DataSourceProperty]
		public string DescriptionText
		{
			get
			{
				return this._descriptionText;
			}
			set
			{
				if (value != this._descriptionText)
				{
					this._descriptionText = value;
					base.OnPropertyChangedWithValue<string>(value, "DescriptionText");
				}
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00068A02 File Offset: 0x00066C02
		// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x00068A0A File Offset: 0x00066C0A
		[DataSourceProperty]
		public string HowToLearnText
		{
			get
			{
				return this._howToLearnText;
			}
			set
			{
				if (value != this._howToLearnText)
				{
					this._howToLearnText = value;
					base.OnPropertyChangedWithValue<string>(value, "HowToLearnText");
				}
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00068A2D File Offset: 0x00066C2D
		// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x00068A35 File Offset: 0x00066C35
		[DataSourceProperty]
		public string HowToLearnTitle
		{
			get
			{
				return this._howToLearnTitle;
			}
			set
			{
				if (value != this._howToLearnTitle)
				{
					this._howToLearnTitle = value;
					base.OnPropertyChangedWithValue<string>(value, "HowToLearnTitle");
				}
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00068A58 File Offset: 0x00066C58
		// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x00068A60 File Offset: 0x00066C60
		[DataSourceProperty]
		public bool CanAddFocus
		{
			get
			{
				return this._canAddFocus;
			}
			set
			{
				if (value != this._canAddFocus)
				{
					this._canAddFocus = value;
					base.OnPropertyChangedWithValue(value, "CanAddFocus");
				}
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x00068A7E File Offset: 0x00066C7E
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x00068A86 File Offset: 0x00066C86
		[DataSourceProperty]
		public bool CanLearnSkill
		{
			get
			{
				return this._canLearnSkill;
			}
			set
			{
				if (value != this._canLearnSkill)
				{
					this._canLearnSkill = value;
					base.OnPropertyChangedWithValue(value, "CanLearnSkill");
				}
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00068AA4 File Offset: 0x00066CA4
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x00068AAC File Offset: 0x00066CAC
		[DataSourceProperty]
		public string NextLevelLearningRateText
		{
			get
			{
				return this._nextLevelLearningRateText;
			}
			set
			{
				if (value != this._nextLevelLearningRateText)
				{
					this._nextLevelLearningRateText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextLevelLearningRateText");
				}
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00068ACF File Offset: 0x00066CCF
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x00068AD7 File Offset: 0x00066CD7
		[DataSourceProperty]
		public string NextLevelCostText
		{
			get
			{
				return this._nextLevelCostText;
			}
			set
			{
				if (value != this._nextLevelCostText)
				{
					this._nextLevelCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "NextLevelCostText");
				}
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00068AFA File Offset: 0x00066CFA
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x00068B02 File Offset: 0x00066D02
		[DataSourceProperty]
		public BasicTooltipViewModel ProgressHint
		{
			get
			{
				return this._progressHint;
			}
			set
			{
				if (value != this._progressHint)
				{
					this._progressHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "ProgressHint");
				}
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x00068B20 File Offset: 0x00066D20
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x00068B28 File Offset: 0x00066D28
		[DataSourceProperty]
		public BasicTooltipViewModel SkillXPHint
		{
			get
			{
				return this._skillXPHint;
			}
			set
			{
				if (value != this._skillXPHint)
				{
					this._skillXPHint = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "SkillXPHint");
				}
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00068B46 File Offset: 0x00066D46
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x00068B4E File Offset: 0x00066D4E
		[DataSourceProperty]
		public HintViewModel AddFocusHint
		{
			get
			{
				return this._addFocusHint;
			}
			set
			{
				if (value != this._addFocusHint)
				{
					this._addFocusHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "AddFocusHint");
				}
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x00068B6C File Offset: 0x00066D6C
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x00068B74 File Offset: 0x00066D74
		[DataSourceProperty]
		public BasicTooltipViewModel LearningLimitTooltip
		{
			get
			{
				return this._learningLimitTooltip;
			}
			set
			{
				if (value != this._learningLimitTooltip)
				{
					this._learningLimitTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "LearningLimitTooltip");
				}
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x00068B92 File Offset: 0x00066D92
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x00068B9A File Offset: 0x00066D9A
		[DataSourceProperty]
		public BasicTooltipViewModel LearningRateTooltip
		{
			get
			{
				return this._learningRateTooltip;
			}
			set
			{
				if (value != this._learningRateTooltip)
				{
					this._learningRateTooltip = value;
					base.OnPropertyChangedWithValue<BasicTooltipViewModel>(value, "LearningRateTooltip");
				}
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x00068BB8 File Offset: 0x00066DB8
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x00068BC0 File Offset: 0x00066DC0
		[DataSourceProperty]
		public double ProgressPercentage
		{
			get
			{
				return this._progressPercentage;
			}
			set
			{
				if (value != this._progressPercentage)
				{
					this._progressPercentage = value;
					base.OnPropertyChangedWithValue(value, "ProgressPercentage");
				}
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00068BDE File Offset: 0x00066DDE
		// (set) Token: 0x06001D0A RID: 7434 RVA: 0x00068BE6 File Offset: 0x00066DE6
		[DataSourceProperty]
		public float LearningRate
		{
			get
			{
				return this._learningRate;
			}
			set
			{
				if (value != this._learningRate)
				{
					this._learningRate = value;
					base.OnPropertyChangedWithValue(value, "LearningRate");
				}
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00068C04 File Offset: 0x00066E04
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x00068C0C File Offset: 0x00066E0C
		[DataSourceProperty]
		public int CurrentSkillXP
		{
			get
			{
				return this._currentSkillXP;
			}
			set
			{
				if (value != this._currentSkillXP)
				{
					this._currentSkillXP = value;
					base.OnPropertyChangedWithValue(value, "CurrentSkillXP");
				}
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x00068C2A File Offset: 0x00066E2A
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x00068C32 File Offset: 0x00066E32
		[DataSourceProperty]
		public int NextLevel
		{
			get
			{
				return this._nextLevel;
			}
			set
			{
				if (value != this._nextLevel)
				{
					this._nextLevel = value;
					base.OnPropertyChangedWithValue(value, "NextLevel");
				}
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x00068C50 File Offset: 0x00066E50
		// (set) Token: 0x06001D10 RID: 7440 RVA: 0x00068C58 File Offset: 0x00066E58
		[DataSourceProperty]
		public int FullLearningRateLevel
		{
			get
			{
				return this._fullLearningRateLevel;
			}
			set
			{
				if (value != this._fullLearningRateLevel)
				{
					this._fullLearningRateLevel = value;
					base.OnPropertyChangedWithValue(value, "FullLearningRateLevel");
				}
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06001D11 RID: 7441 RVA: 0x00068C76 File Offset: 0x00066E76
		// (set) Token: 0x06001D12 RID: 7442 RVA: 0x00068C7E File Offset: 0x00066E7E
		[DataSourceProperty]
		public int XpRequiredForNextLevel
		{
			get
			{
				return this._xpRequiredForNextLevel;
			}
			set
			{
				if (value != this._xpRequiredForNextLevel)
				{
					this._xpRequiredForNextLevel = value;
					base.OnPropertyChangedWithValue(value, "XpRequiredForNextLevel");
				}
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x00068C9C File Offset: 0x00066E9C
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x00068CA4 File Offset: 0x00066EA4
		[DataSourceProperty]
		public int NumOfUnopenedPerks
		{
			get
			{
				return this._numOfUnopenedPerks;
			}
			set
			{
				if (value != this._numOfUnopenedPerks)
				{
					this._numOfUnopenedPerks = value;
					base.OnPropertyChangedWithValue(value, "NumOfUnopenedPerks");
				}
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x00068CC2 File Offset: 0x00066EC2
		// (set) Token: 0x06001D16 RID: 7446 RVA: 0x00068CCA File Offset: 0x00066ECA
		[DataSourceProperty]
		public string ProgressText
		{
			get
			{
				return this._progressText;
			}
			set
			{
				if (value != this._progressText)
				{
					this._progressText = value;
					base.OnPropertyChangedWithValue<string>(value, "ProgressText");
				}
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00068CED File Offset: 0x00066EED
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x00068CF5 File Offset: 0x00066EF5
		[DataSourceProperty]
		public string FocusCostText
		{
			get
			{
				return this._focusCostText;
			}
			set
			{
				if (value != this._focusCostText)
				{
					this._focusCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "FocusCostText");
				}
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x00068D18 File Offset: 0x00066F18
		// (set) Token: 0x06001D1A RID: 7450 RVA: 0x00068D20 File Offset: 0x00066F20
		[DataSourceProperty]
		public MBBindingList<PerkVM> Perks
		{
			get
			{
				return this._perks;
			}
			set
			{
				if (value != this._perks)
				{
					this._perks = value;
					base.OnPropertyChangedWithValue<MBBindingList<PerkVM>>(value, "Perks");
				}
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x00068D3E File Offset: 0x00066F3E
		// (set) Token: 0x06001D1C RID: 7452 RVA: 0x00068D46 File Offset: 0x00066F46
		[DataSourceProperty]
		public MBBindingList<BindingListStringItem> SkillEffects
		{
			get
			{
				return this._skillEffects;
			}
			set
			{
				if (value != this._skillEffects)
				{
					this._skillEffects = value;
					base.OnPropertyChangedWithValue<MBBindingList<BindingListStringItem>>(value, "SkillEffects");
				}
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x00068D64 File Offset: 0x00066F64
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x00068D6C File Offset: 0x00066F6C
		[DataSourceProperty]
		public int MaxLevel
		{
			get
			{
				return this._maxLevel;
			}
			set
			{
				if (value != this._maxLevel)
				{
					this._maxLevel = value;
					base.OnPropertyChangedWithValue(value, "MaxLevel");
				}
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00068D8A File Offset: 0x00066F8A
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00068D92 File Offset: 0x00066F92
		[DataSourceProperty]
		public string CurrentLearningRateText
		{
			get
			{
				return this._currentLearningRateText;
			}
			set
			{
				if (value != this._currentLearningRateText)
				{
					this._currentLearningRateText = value;
					base.OnPropertyChangedWithValue<string>(value, "CurrentLearningRateText");
				}
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00068DB5 File Offset: 0x00066FB5
		// (set) Token: 0x06001D22 RID: 7458 RVA: 0x00068DBD File Offset: 0x00066FBD
		[DataSourceProperty]
		public int CurrentFocusLevel
		{
			get
			{
				return this._currentFocusLevel;
			}
			set
			{
				if (value != this._currentFocusLevel)
				{
					this._currentFocusLevel = value;
					base.OnPropertyChangedWithValue(value, "CurrentFocusLevel");
				}
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00068DDB File Offset: 0x00066FDB
		// (set) Token: 0x06001D24 RID: 7460 RVA: 0x00068DE3 File Offset: 0x00066FE3
		[DataSourceProperty]
		public string AddFocusText
		{
			get
			{
				return this._addFocusText;
			}
			set
			{
				if (value != this._addFocusText)
				{
					this._addFocusText = value;
					base.OnPropertyChangedWithValue<string>(value, "AddFocusText");
				}
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00068E06 File Offset: 0x00067006
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00068E0E File Offset: 0x0006700E
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

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00068E31 File Offset: 0x00067031
		// (set) Token: 0x06001D28 RID: 7464 RVA: 0x00068E39 File Offset: 0x00067039
		[DataSourceProperty]
		public bool IsInspected
		{
			get
			{
				return this._isInspected;
			}
			set
			{
				if (value != this._isInspected)
				{
					this._isInspected = value;
					base.OnPropertyChangedWithValue(value, "IsInspected");
				}
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00068E57 File Offset: 0x00067057
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x00068E5F File Offset: 0x0006705F
		[DataSourceProperty]
		public string NameText
		{
			get
			{
				return this._nameText;
			}
			set
			{
				if (value != this._nameText)
				{
					this._nameText = value;
					base.OnPropertyChangedWithValue<string>(value, "NameText");
				}
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x00068E82 File Offset: 0x00067082
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x00068E8A File Offset: 0x0006708A
		[DataSourceProperty]
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (value != this._level)
				{
					this._level = value;
					base.OnPropertyChangedWithValue(value, "Level");
				}
			}
		}

		// Token: 0x04000DA2 RID: 3490
		public const int MAX_SKILL_LEVEL = 300;

		// Token: 0x04000DA3 RID: 3491
		public readonly SkillObject Skill;

		// Token: 0x04000DA4 RID: 3492
		private readonly CharacterVM _developerVM;

		// Token: 0x04000DA5 RID: 3493
		private readonly TextObject _boundAttributeName;

		// Token: 0x04000DA6 RID: 3494
		private readonly Concept _focusConceptObj;

		// Token: 0x04000DA7 RID: 3495
		private readonly Concept _skillConceptObj;

		// Token: 0x04000DA8 RID: 3496
		private readonly Action<PerkVM> _onStartPerkSelection;

		// Token: 0x04000DA9 RID: 3497
		private int _orgFocusAmount;

		// Token: 0x04000DAA RID: 3498
		private MBBindingList<BindingListStringItem> _skillEffects;

		// Token: 0x04000DAB RID: 3499
		private MBBindingList<PerkVM> _perks;

		// Token: 0x04000DAC RID: 3500
		private BasicTooltipViewModel _progressHint;

		// Token: 0x04000DAD RID: 3501
		private HintViewModel _addFocusHint;

		// Token: 0x04000DAE RID: 3502
		private BasicTooltipViewModel _skillXPHint;

		// Token: 0x04000DAF RID: 3503
		private BasicTooltipViewModel _learningLimitTooltip;

		// Token: 0x04000DB0 RID: 3504
		private BasicTooltipViewModel _learningRateTooltip;

		// Token: 0x04000DB1 RID: 3505
		private string _nameText;

		// Token: 0x04000DB2 RID: 3506
		private string _skillId;

		// Token: 0x04000DB3 RID: 3507
		private string _addFocusText;

		// Token: 0x04000DB4 RID: 3508
		private string _focusCostText;

		// Token: 0x04000DB5 RID: 3509
		private string _currentLearningRateText;

		// Token: 0x04000DB6 RID: 3510
		private string _nextLevelLearningRateText;

		// Token: 0x04000DB7 RID: 3511
		private string _nextLevelCostText;

		// Token: 0x04000DB8 RID: 3512
		private string _howToLearnText;

		// Token: 0x04000DB9 RID: 3513
		private string _howToLearnTitle;

		// Token: 0x04000DBA RID: 3514
		private string _type;

		// Token: 0x04000DBB RID: 3515
		private string _progressText;

		// Token: 0x04000DBC RID: 3516
		private string _descriptionText;

		// Token: 0x04000DBD RID: 3517
		private int _level = -1;

		// Token: 0x04000DBE RID: 3518
		private int _maxLevel;

		// Token: 0x04000DBF RID: 3519
		private int _currentFocusLevel;

		// Token: 0x04000DC0 RID: 3520
		private int _currentSkillXP;

		// Token: 0x04000DC1 RID: 3521
		private int _xpRequiredForNextLevel;

		// Token: 0x04000DC2 RID: 3522
		private int _nextLevel;

		// Token: 0x04000DC3 RID: 3523
		private int _fullLearningRateLevel;

		// Token: 0x04000DC4 RID: 3524
		private int _numOfUnopenedPerks;

		// Token: 0x04000DC5 RID: 3525
		private bool _isInspected;

		// Token: 0x04000DC6 RID: 3526
		private bool _canAddFocus;

		// Token: 0x04000DC7 RID: 3527
		private bool _canLearnSkill;

		// Token: 0x04000DC8 RID: 3528
		private float _learningRate;

		// Token: 0x04000DC9 RID: 3529
		private double _progressPercentage;

		// Token: 0x0200028C RID: 652
		private enum SkillType
		{
			// Token: 0x04001212 RID: 4626
			Default,
			// Token: 0x04001213 RID: 4627
			Party,
			// Token: 0x04001214 RID: 4628
			Leader
		}
	}
}
