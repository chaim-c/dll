using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000018 RID: 24
	public class HeroVM : ViewModel
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000B035 File Offset: 0x00009235
		public Hero Hero { get; }

		// Token: 0x06000165 RID: 357 RVA: 0x0000B040 File Offset: 0x00009240
		public HeroVM(Hero hero, bool useCivilian = false)
		{
			if (hero != null)
			{
				CharacterCode characterCode = CampaignUIHelper.GetCharacterCode(hero.CharacterObject, useCivilian);
				this.ImageIdentifier = new ImageIdentifierVM(characterCode);
				this.ClanBanner = new ImageIdentifierVM(hero.ClanBanner);
				this.ClanBanner_9 = new ImageIdentifierVM(BannerCode.CreateFrom(hero.ClanBanner), true);
				this.Relation = HeroVM.GetRelation(hero);
				this.IsDead = !hero.IsAlive;
				TextObject textObject;
				this.IsChild = (!CampaignUIHelper.IsHeroInformationHidden(hero, out textObject) && FaceGen.GetMaturityTypeWithAge(hero.Age) <= BodyMeshMaturityType.Child);
			}
			else
			{
				this.ImageIdentifier = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.ClanBanner = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.ClanBanner_9 = new ImageIdentifierVM(ImageIdentifierType.Null);
				this.Relation = 0;
			}
			this.Hero = hero;
			this.RefreshValues();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B122 File Offset: 0x00009322
		public override void RefreshValues()
		{
			base.RefreshValues();
			if (this.Hero != null)
			{
				this.NameText = this.Hero.Name.ToString();
				return;
			}
			this.NameText = "";
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B154 File Offset: 0x00009354
		public void ExecuteLink()
		{
			if (this.Hero != null)
			{
				Campaign.Current.EncyclopediaManager.GoToLink(this.Hero.EncyclopediaLink);
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B178 File Offset: 0x00009378
		public virtual void ExecuteBeginHint()
		{
			if (this.Hero != null)
			{
				InformationManager.ShowTooltip(typeof(Hero), new object[]
				{
					this.Hero,
					false
				});
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B1A9 File Offset: 0x000093A9
		public virtual void ExecuteEndHint()
		{
			if (this.Hero != null)
			{
				MBInformationManager.HideInformations();
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000B1B8 File Offset: 0x000093B8
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000B1C0 File Offset: 0x000093C0
		[DataSourceProperty]
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (value != this._isDead)
				{
					this._isDead = value;
					base.OnPropertyChangedWithValue(value, "IsDead");
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B1DE File Offset: 0x000093DE
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000B1E6 File Offset: 0x000093E6
		[DataSourceProperty]
		public bool IsChild
		{
			get
			{
				return this._isChild;
			}
			set
			{
				if (value != this._isChild)
				{
					this._isChild = value;
					base.OnPropertyChangedWithValue(value, "IsChild");
				}
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B204 File Offset: 0x00009404
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000B20C File Offset: 0x0000940C
		[DataSourceProperty]
		public int Relation
		{
			get
			{
				return this._relation;
			}
			set
			{
				if (value != this._relation)
				{
					this._relation = value;
					base.OnPropertyChangedWithValue(value, "Relation");
				}
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000B22A File Offset: 0x0000942A
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000B232 File Offset: 0x00009432
		[DataSourceProperty]
		public ImageIdentifierVM ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (value != this._imageIdentifier)
				{
					this._imageIdentifier = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ImageIdentifier");
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000B250 File Offset: 0x00009450
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000B258 File Offset: 0x00009458
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000B27B File Offset: 0x0000947B
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000B283 File Offset: 0x00009483
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner
		{
			get
			{
				return this._clanBanner;
			}
			set
			{
				if (value != this._clanBanner)
				{
					this._clanBanner = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner");
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000B2A1 File Offset: 0x000094A1
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000B2A9 File Offset: 0x000094A9
		[DataSourceProperty]
		public ImageIdentifierVM ClanBanner_9
		{
			get
			{
				return this._clanBanner_9;
			}
			set
			{
				if (value != this._clanBanner_9)
				{
					this._clanBanner_9 = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "ClanBanner_9");
				}
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B2C7 File Offset: 0x000094C7
		public static int GetRelation(Hero hero)
		{
			if (hero == null)
			{
				return -101;
			}
			if (hero == Hero.MainHero)
			{
				return 101;
			}
			if (ViewModel.UIDebugMode)
			{
				return MBRandom.RandomInt(-100, 100);
			}
			return Hero.MainHero.GetRelation(hero);
		}

		// Token: 0x040000A9 RID: 169
		private ImageIdentifierVM _imageIdentifier;

		// Token: 0x040000AA RID: 170
		private ImageIdentifierVM _clanBanner;

		// Token: 0x040000AB RID: 171
		private ImageIdentifierVM _clanBanner_9;

		// Token: 0x040000AC RID: 172
		private string _nameText;

		// Token: 0x040000AD RID: 173
		private int _relation = -102;

		// Token: 0x040000AE RID: 174
		private bool _isDead = true;

		// Token: 0x040000AF RID: 175
		private bool _isChild;
	}
}
