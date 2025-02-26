﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard
{
	// Token: 0x02000014 RID: 20
	public class SPScoreboardUnitVM : ViewModel
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006918 File Offset: 0x00004B18
		public SPScoreboardUnitVM(BasicCharacterObject character)
		{
			this.Character = character;
			this.GainedSkills = new MBBindingList<SPScoreboardSkillItemVM>();
			this._skills = new List<SPScoreboardSkillItemVM>();
			this.Score = new SPScoreboardStatsVM(character.Name);
			CharacterCode.CreateFrom(character);
			this.IsHero = character.IsHero;
			this.Score.IsMainHero = (character == Game.Current.PlayerTroop);
			this.IsGainedAnySkills = false;
			if (character.IsHero)
			{
				foreach (SkillObject skill in Game.Current.ObjectManager.GetObjectTypeList<SkillObject>())
				{
					this._skills.Add(new SPScoreboardSkillItemVM(skill, character.GetSkillValue(skill)));
				}
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000069F4 File Offset: 0x00004BF4
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Score.RefreshValues();
			this.GainedSkills.ApplyActionOnAllItems(delegate(SPScoreboardSkillItemVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006A31 File Offset: 0x00004C31
		private void ExecuteActivateGainedSkills()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006A33 File Offset: 0x00004C33
		private void ExecuteDeactivateGainedSkills()
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006A35 File Offset: 0x00004C35
		public void UpdateScores(int numberRemaining, int numberDead, int numberWounded, int numberRouted, int numberKilled, int numberReadyToUpgrade)
		{
			this.Score.UpdateScores(numberRemaining, numberDead, numberWounded, numberRouted, numberKilled, numberReadyToUpgrade);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006A4C File Offset: 0x00004C4C
		public void UpdateHeroSkills(SkillObject gainedSkill, int currentSkill)
		{
			SPScoreboardSkillItemVM spscoreboardSkillItemVM = this._skills.First((SPScoreboardSkillItemVM s) => s.Skill == gainedSkill);
			spscoreboardSkillItemVM.UpdateSkill(currentSkill);
			if (!this.GainedSkills.Contains(spscoreboardSkillItemVM))
			{
				this.GainedSkills.Add(spscoreboardSkillItemVM);
			}
			this.IsGainedAnySkills = (this.GainedSkills.Count > 0);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006AB3 File Offset: 0x00004CB3
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00006ABB File Offset: 0x00004CBB
		[DataSourceProperty]
		public bool IsGainedAnySkills
		{
			get
			{
				return this._isGainedAnySkills;
			}
			set
			{
				if (value != this._isGainedAnySkills)
				{
					this._isGainedAnySkills = value;
					base.OnPropertyChangedWithValue(value, "IsGainedAnySkills");
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006AD9 File Offset: 0x00004CD9
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00006AE1 File Offset: 0x00004CE1
		[DataSourceProperty]
		public MBBindingList<SPScoreboardSkillItemVM> GainedSkills
		{
			get
			{
				return this._gainedSkills;
			}
			set
			{
				if (value != this._gainedSkills)
				{
					this._gainedSkills = value;
					base.OnPropertyChangedWithValue<MBBindingList<SPScoreboardSkillItemVM>>(value, "GainedSkills");
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006AFF File Offset: 0x00004CFF
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00006B07 File Offset: 0x00004D07
		[DataSourceProperty]
		public bool IsHero
		{
			get
			{
				return this._isHero;
			}
			set
			{
				if (value != this._isHero)
				{
					this._isHero = value;
					base.OnPropertyChangedWithValue(value, "IsHero");
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00006B25 File Offset: 0x00004D25
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00006B2D File Offset: 0x00004D2D
		[DataSourceProperty]
		public SPScoreboardStatsVM Score
		{
			get
			{
				return this._score;
			}
			set
			{
				if (value != this._score)
				{
					this._score = value;
					base.OnPropertyChangedWithValue<SPScoreboardStatsVM>(value, "Score");
				}
			}
		}

		// Token: 0x040000B7 RID: 183
		public readonly BasicCharacterObject Character;

		// Token: 0x040000B8 RID: 184
		private readonly List<SPScoreboardSkillItemVM> _skills;

		// Token: 0x040000B9 RID: 185
		private SPScoreboardStatsVM _score;

		// Token: 0x040000BA RID: 186
		private bool _isHero;

		// Token: 0x040000BB RID: 187
		private bool _isGainedAnySkills;

		// Token: 0x040000BC RID: 188
		private MBBindingList<SPScoreboardSkillItemVM> _gainedSkills;
	}
}
