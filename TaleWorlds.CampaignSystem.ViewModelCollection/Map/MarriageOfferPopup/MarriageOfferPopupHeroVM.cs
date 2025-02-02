using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MarriageOfferPopup
{
	// Token: 0x02000035 RID: 53
	public class MarriageOfferPopupHeroVM : ViewModel
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001ABAD File Offset: 0x00018DAD
		public Hero Hero { get; }

		// Token: 0x06000519 RID: 1305 RVA: 0x0001ABB5 File Offset: 0x00018DB5
		public MarriageOfferPopupHeroVM(Hero hero)
		{
			this.Hero = hero;
			this.Model = new HeroViewModel(CharacterViewModel.StanceTypes.None);
			this.FillHeroInformation();
			this.CreateClanBanner();
			this.RefreshValues();
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		public void Update()
		{
			TextObject textObject;
			if (!this._modelCreated && !CampaignUIHelper.IsHeroInformationHidden(this.Hero, out textObject))
			{
				this._modelCreated = true;
				this.CreateHeroModel();
			}
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001AC18 File Offset: 0x00018E18
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.EncyclopediaLinkWithName = this.Hero.EncyclopediaLinkWithName.ToString();
			this.AgeString = ((int)this.Hero.Age).ToString();
			this.OccupationString = CampaignUIHelper.GetHeroOccupationName(this.Hero);
			this.Relation = (int)this.Hero.GetRelationWithPlayer();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0001AC7E File Offset: 0x00018E7E
		public override void OnFinalize()
		{
			HeroViewModel model = this.Model;
			if (model != null)
			{
				model.OnFinalize();
			}
			MBBindingList<EncyclopediaTraitItemVM> traits = this.Traits;
			if (traits != null)
			{
				traits.Clear();
			}
			MBBindingList<MarriageOfferPopupHeroAttributeVM> skills = this.Skills;
			if (skills != null)
			{
				skills.Clear();
			}
			base.OnFinalize();
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001ACB9 File Offset: 0x00018EB9
		public void ExecuteHeroLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.Hero.EncyclopediaLink);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public void ExecuteClanLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this.Hero.Clan.EncyclopediaLink);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001ACF6 File Offset: 0x00018EF6
		private void CreateClanBanner()
		{
			this.ClanName = this.Hero.Clan.Name.ToString();
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(this.Hero.ClanBanner), true);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0001AD30 File Offset: 0x00018F30
		private void CreateHeroModel()
		{
			this.Model.FillFrom(this.Hero, -1, true, true);
			this.Model.SetEquipment(EquipmentIndex.ArmorItemEndSlot, default(EquipmentElement));
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001AD68 File Offset: 0x00018F68
		private void FillHeroInformation()
		{
			this.Traits = new MBBindingList<EncyclopediaTraitItemVM>();
			this.Skills = new MBBindingList<MarriageOfferPopupHeroAttributeVM>();
			foreach (CharacterAttribute attribute in Attributes.All)
			{
				this.Skills.Add(new MarriageOfferPopupHeroAttributeVM(this.Hero, attribute));
			}
			foreach (TraitObject traitObject in CampaignUIHelper.GetHeroTraits())
			{
				if (this.Hero.GetTraitLevel(traitObject) != 0)
				{
					this.Traits.Add(new EncyclopediaTraitItemVM(traitObject, this.Hero));
				}
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0001AE3C File Offset: 0x0001903C
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0001AE44 File Offset: 0x00019044
		[DataSourceProperty]
		public string EncyclopediaLinkWithName
		{
			get
			{
				return this._encyclopediaLinkWithName;
			}
			set
			{
				if (value != this._encyclopediaLinkWithName)
				{
					this._encyclopediaLinkWithName = value;
					base.OnPropertyChangedWithValue<string>(value, "EncyclopediaLinkWithName");
				}
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0001AE67 File Offset: 0x00019067
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0001AE6F File Offset: 0x0001906F
		[DataSourceProperty]
		public string AgeString
		{
			get
			{
				return this._ageString;
			}
			set
			{
				if (value != this._ageString)
				{
					this._ageString = value;
					base.OnPropertyChangedWithValue<string>(value, "AgeString");
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001AE92 File Offset: 0x00019092
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0001AE9A File Offset: 0x0001909A
		[DataSourceProperty]
		public string OccupationString
		{
			get
			{
				return this._occupationString;
			}
			set
			{
				if (value != this._occupationString)
				{
					this._occupationString = value;
					base.OnPropertyChangedWithValue<string>(value, "OccupationString");
				}
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0001AEBD File Offset: 0x000190BD
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0001AEC5 File Offset: 0x000190C5
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

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001AEE3 File Offset: 0x000190E3
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x0001AEEB File Offset: 0x000190EB
		[DataSourceProperty]
		public string ClanName
		{
			get
			{
				return this._clanName;
			}
			set
			{
				if (value != this._clanName)
				{
					this._clanName = value;
					base.OnPropertyChangedWithValue<string>(value, "ClanName");
				}
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0001AF0E File Offset: 0x0001910E
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x0001AF16 File Offset: 0x00019116
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

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0001AF34 File Offset: 0x00019134
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x0001AF3C File Offset: 0x0001913C
		[DataSourceProperty]
		public HeroViewModel Model
		{
			get
			{
				return this._model;
			}
			set
			{
				if (value != this._model)
				{
					this._model = value;
					base.OnPropertyChangedWithValue<HeroViewModel>(value, "Model");
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x0001AF5A File Offset: 0x0001915A
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x0001AF62 File Offset: 0x00019162
		[DataSourceProperty]
		public MBBindingList<EncyclopediaTraitItemVM> Traits
		{
			get
			{
				return this._traits;
			}
			set
			{
				if (value != this._traits)
				{
					this._traits = value;
					base.OnPropertyChangedWithValue<MBBindingList<EncyclopediaTraitItemVM>>(value, "Traits");
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0001AF80 File Offset: 0x00019180
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x0001AF88 File Offset: 0x00019188
		[DataSourceProperty]
		public MBBindingList<MarriageOfferPopupHeroAttributeVM> Skills
		{
			get
			{
				return this._skills;
			}
			set
			{
				if (value != this._skills)
				{
					this._skills = value;
					base.OnPropertyChangedWithValue<MBBindingList<MarriageOfferPopupHeroAttributeVM>>(value, "Skills");
				}
			}
		}

		// Token: 0x0400022B RID: 555
		private bool _modelCreated;

		// Token: 0x0400022D RID: 557
		private string _encyclopediaLinkWithName;

		// Token: 0x0400022E RID: 558
		private string _ageString;

		// Token: 0x0400022F RID: 559
		private string _occupationString;

		// Token: 0x04000230 RID: 560
		private int _relation;

		// Token: 0x04000231 RID: 561
		private string _clanName;

		// Token: 0x04000232 RID: 562
		private ImageIdentifierVM _clanBanner;

		// Token: 0x04000233 RID: 563
		private HeroViewModel _model;

		// Token: 0x04000234 RID: 564
		private MBBindingList<EncyclopediaTraitItemVM> _traits;

		// Token: 0x04000235 RID: 565
		private MBBindingList<MarriageOfferPopupHeroAttributeVM> _skills;
	}
}
