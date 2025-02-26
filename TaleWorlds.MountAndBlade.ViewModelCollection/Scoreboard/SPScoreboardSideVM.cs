﻿using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard
{
	// Token: 0x02000010 RID: 16
	public class SPScoreboardSideVM : ViewModel
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00005AE0 File Offset: 0x00003CE0
		public SPScoreboardSideVM(TextObject name, Banner sideFlag)
		{
			this._nameTextObject = name;
			this.Parties = new MBBindingList<SPScoreboardPartyVM>();
			this.Score = new SPScoreboardStatsVM(this._nameTextObject);
			MBBindingList<SPScoreboardPartyVM> parties = this.Parties;
			this.SortController = new SPScoreboardSortControllerVM(ref parties);
			this.Parties = parties;
			if (sideFlag != null)
			{
				BannerCode bannerCode = BannerCode.CreateFrom(sideFlag);
				this.BannerVisual = new ImageIdentifierVM(bannerCode, true);
				this.BannerVisualSmall = new ImageIdentifierVM(bannerCode, false);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005B55 File Offset: 0x00003D55
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.Score.RefreshValues();
			this.Parties.ApplyActionOnAllItems(delegate(SPScoreboardPartyVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005B92 File Offset: 0x00003D92
		public void UpdateScores(IBattleCombatant battleCombatant, bool isPlayerParty, BasicCharacterObject character, int numberRemaining, int numberDead, int numberWounded, int numberRouted, int numberKilled, int numberReadyToUpgrade)
		{
			this.GetPartyAddIfNotExists(battleCombatant, isPlayerParty).UpdateScores(character, numberRemaining, numberDead, numberWounded, numberRouted, numberKilled, numberReadyToUpgrade);
			this.Score.UpdateScores(numberRemaining, numberDead, numberWounded, numberRouted, numberKilled, numberReadyToUpgrade);
			this.RefreshPower();
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00005BCB File Offset: 0x00003DCB
		public void UpdateHeroSkills(IBattleCombatant battleCombatant, bool isPlayerParty, BasicCharacterObject heroCharacter, SkillObject upgradedSkill)
		{
			this.GetPartyAddIfNotExists(battleCombatant, isPlayerParty).UpdateHeroSkills(heroCharacter, upgradedSkill);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public SPScoreboardPartyVM GetPartyAddIfNotExists(IBattleCombatant battleCombatant, bool isPlayerParty)
		{
			SPScoreboardPartyVM spscoreboardPartyVM = this.Parties.FirstOrDefault((SPScoreboardPartyVM p) => p.BattleCombatant == battleCombatant);
			if (spscoreboardPartyVM == null)
			{
				spscoreboardPartyVM = new SPScoreboardPartyVM(battleCombatant);
				if (isPlayerParty)
				{
					this.Parties.Insert(0, spscoreboardPartyVM);
				}
				else
				{
					this.Parties.Add(spscoreboardPartyVM);
				}
			}
			return spscoreboardPartyVM;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005C40 File Offset: 0x00003E40
		public SPScoreboardPartyVM GetParty(IBattleCombatant battleCombatant)
		{
			return this.Parties.FirstOrDefault((SPScoreboardPartyVM p) => p.BattleCombatant == battleCombatant);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005C74 File Offset: 0x00003E74
		public SPScoreboardStatsVM RemoveTroop(IBattleCombatant battleCombatant, BasicCharacterObject troop)
		{
			SPScoreboardPartyVM spscoreboardPartyVM = this.Parties.FirstOrDefault((SPScoreboardPartyVM p) => p.BattleCombatant == battleCombatant);
			SPScoreboardStatsVM spscoreboardStatsVM = spscoreboardPartyVM.RemoveUnit(troop);
			if (spscoreboardPartyVM.Members.Count == 0)
			{
				this.Parties.Remove(spscoreboardPartyVM);
			}
			this.Score.UpdateScores(-spscoreboardStatsVM.Remaining, -spscoreboardStatsVM.Dead, -spscoreboardStatsVM.Wounded, -spscoreboardStatsVM.Routed, -spscoreboardStatsVM.Kill, -spscoreboardStatsVM.ReadyToUpgrade);
			return spscoreboardStatsVM;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005D00 File Offset: 0x00003F00
		public void AddTroop(IBattleCombatant battleCombatant, BasicCharacterObject currentTroop, SPScoreboardStatsVM scoreToBringOver)
		{
			this.Parties.FirstOrDefault((SPScoreboardPartyVM p) => p.BattleCombatant == battleCombatant).AddUnit(currentTroop, scoreToBringOver);
			this.Score.UpdateScores(scoreToBringOver.Remaining, scoreToBringOver.Dead, scoreToBringOver.Wounded, scoreToBringOver.Routed, scoreToBringOver.Kill, scoreToBringOver.ReadyToUpgrade);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005D68 File Offset: 0x00003F68
		private void RefreshPower()
		{
			this.CurrentPower = 0f;
			this.InitialPower = 0f;
			foreach (SPScoreboardPartyVM spscoreboardPartyVM in this._parties)
			{
				this.InitialPower += spscoreboardPartyVM.InitialPower;
				this.CurrentPower += spscoreboardPartyVM.CurrentPower;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005DEC File Offset: 0x00003FEC
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00005DF4 File Offset: 0x00003FF4
		public float CurrentPower { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00005DFD File Offset: 0x00003FFD
		// (set) Token: 0x06000143 RID: 323 RVA: 0x00005E05 File Offset: 0x00004005
		public float InitialPower { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005E0E File Offset: 0x0000400E
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00005E16 File Offset: 0x00004016
		[DataSourceProperty]
		public ImageIdentifierVM BannerVisual
		{
			get
			{
				return this._bannerVisual;
			}
			set
			{
				if (value != this._bannerVisual)
				{
					this._bannerVisual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "BannerVisual");
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00005E34 File Offset: 0x00004034
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00005E3C File Offset: 0x0000403C
		[DataSourceProperty]
		public ImageIdentifierVM BannerVisualSmall
		{
			get
			{
				return this._bannerVisualSmall;
			}
			set
			{
				if (value != this._bannerVisualSmall)
				{
					this._bannerVisualSmall = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "BannerVisualSmall");
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00005E5A File Offset: 0x0000405A
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00005E62 File Offset: 0x00004062
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

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005E80 File Offset: 0x00004080
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00005E88 File Offset: 0x00004088
		[DataSourceProperty]
		public MBBindingList<SPScoreboardPartyVM> Parties
		{
			get
			{
				return this._parties;
			}
			set
			{
				if (value != this._parties)
				{
					this._parties = value;
					base.OnPropertyChangedWithValue<MBBindingList<SPScoreboardPartyVM>>(value, "Parties");
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005EA6 File Offset: 0x000040A6
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005EAE File Offset: 0x000040AE
		[DataSourceProperty]
		public SPScoreboardSortControllerVM SortController
		{
			get
			{
				return this._sortController;
			}
			set
			{
				if (value != this._sortController)
				{
					this._sortController = value;
					base.OnPropertyChanged("SortController");
				}
			}
		}

		// Token: 0x0400008B RID: 139
		private TextObject _nameTextObject;

		// Token: 0x0400008E RID: 142
		private MBBindingList<SPScoreboardPartyVM> _parties;

		// Token: 0x0400008F RID: 143
		private SPScoreboardStatsVM _score;

		// Token: 0x04000090 RID: 144
		private ImageIdentifierVM _bannerVisual;

		// Token: 0x04000091 RID: 145
		private ImageIdentifierVM _bannerVisualSmall;

		// Token: 0x04000092 RID: 146
		private SPScoreboardSortControllerVM _sortController;
	}
}
