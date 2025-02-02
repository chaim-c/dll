using System;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MarriageOfferPopup
{
	// Token: 0x02000036 RID: 54
	public class MarriageOfferPopupVM : ViewModel
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public MarriageOfferPopupVM(Hero suitor, Hero maiden)
		{
			this._marriageBehavior = Campaign.Current.GetCampaignBehavior<IMarriageOfferCampaignBehavior>();
			if (suitor.Clan == Clan.PlayerClan)
			{
				this.OffereeClanMember = new MarriageOfferPopupHeroVM(suitor);
				this.OffererClanMember = new MarriageOfferPopupHeroVM(maiden);
			}
			else
			{
				this.OffereeClanMember = new MarriageOfferPopupHeroVM(maiden);
				this.OffererClanMember = new MarriageOfferPopupHeroVM(suitor);
			}
			this.ConsequencesList = new MBBindingList<BindingListStringItem>();
			this.RefreshValues();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001B01B File Offset: 0x0001921B
		public void Update()
		{
			MarriageOfferPopupHeroVM offereeClanMember = this.OffereeClanMember;
			if (offereeClanMember != null)
			{
				offereeClanMember.Update();
			}
			MarriageOfferPopupHeroVM offererClanMember = this.OffererClanMember;
			if (offererClanMember == null)
			{
				return;
			}
			offererClanMember.Update();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001B03E File Offset: 0x0001923E
		public void ExecuteAcceptOffer()
		{
			IMarriageOfferCampaignBehavior marriageBehavior = this._marriageBehavior;
			if (marriageBehavior == null)
			{
				return;
			}
			marriageBehavior.OnMarriageOfferAcceptedOnPopUp();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001B050 File Offset: 0x00019250
		public void ExecuteDeclineOffer()
		{
			IMarriageOfferCampaignBehavior marriageBehavior = this._marriageBehavior;
			if (marriageBehavior == null)
			{
				return;
			}
			marriageBehavior.OnMarriageOfferDeclinedOnPopUp();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001B064 File Offset: 0x00019264
		public override void RefreshValues()
		{
			base.RefreshValues();
			TextObject textObject = GameTexts.FindText("str_marriage_offer_from_clan", null);
			textObject.SetTextVariable("CLAN_NAME", this.OffererClanMember.Hero.Clan.Name);
			this.TitleText = textObject.ToString();
			this.ClanText = GameTexts.FindText("str_clan", null).ToString();
			this.AgeText = new TextObject("{=jaaQijQs}Age", null).ToString();
			this.OccupationText = new TextObject("{=GZxFIeiJ}Occupation", null).ToString();
			this.RelationText = new TextObject("{=BlidMNGT}Relation", null).ToString();
			this.ConsequencesText = new TextObject("{=Lm6Mkhru}Consequences", null).ToString();
			this.ButtonOkLabel = new TextObject("{=Y94H6XnK}Accept", null).ToString();
			this.ButtonCancelLabel = new TextObject("{=cOgmdp9e}Decline", null).ToString();
			this.ConsequencesList.Clear();
			IMarriageOfferCampaignBehavior marriageBehavior = this._marriageBehavior;
			foreach (TextObject textObject2 in (((marriageBehavior != null) ? marriageBehavior.GetMarriageAcceptedConsequences() : null) ?? new MBBindingList<TextObject>()))
			{
				this.ConsequencesList.Add(new BindingListStringItem("- " + textObject2.ToString()));
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001B1C4 File Offset: 0x000193C4
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey != null)
			{
				doneInputKey.OnFinalize();
			}
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			MarriageOfferPopupHeroVM offereeClanMember = this.OffereeClanMember;
			if (offereeClanMember != null)
			{
				offereeClanMember.OnFinalize();
			}
			MarriageOfferPopupHeroVM offererClanMember = this.OffererClanMember;
			if (offererClanMember == null)
			{
				return;
			}
			offererClanMember.OnFinalize();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001B21A File Offset: 0x0001941A
		public void ExecuteLink(string link)
		{
			Campaign.Current.EncyclopediaManager.GoToLink(link);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001B22C File Offset: 0x0001942C
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001B234 File Offset: 0x00019434
		[DataSourceProperty]
		public string TitleText
		{
			get
			{
				return this._titleText;
			}
			set
			{
				if (value != this._titleText)
				{
					this._titleText = value;
					base.OnPropertyChangedWithValue<string>(value, "TitleText");
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001B257 File Offset: 0x00019457
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001B25F File Offset: 0x0001945F
		[DataSourceProperty]
		public string ClanText
		{
			get
			{
				return this._clanText;
			}
			set
			{
				if (value != this._clanText)
				{
					this._clanText = value;
					base.OnPropertyChangedWithValue<string>(value, "ClanText");
				}
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0001B282 File Offset: 0x00019482
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0001B28A File Offset: 0x0001948A
		[DataSourceProperty]
		public string AgeText
		{
			get
			{
				return this._ageText;
			}
			set
			{
				if (value != this._ageText)
				{
					this._ageText = value;
					base.OnPropertyChangedWithValue<string>(value, "AgeText");
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0001B2AD File Offset: 0x000194AD
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0001B2B5 File Offset: 0x000194B5
		[DataSourceProperty]
		public string OccupationText
		{
			get
			{
				return this._occupationText;
			}
			set
			{
				if (value != this._occupationText)
				{
					this._occupationText = value;
					base.OnPropertyChangedWithValue<string>(value, "OccupationText");
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0001B2D8 File Offset: 0x000194D8
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0001B2E0 File Offset: 0x000194E0
		[DataSourceProperty]
		public string RelationText
		{
			get
			{
				return this._relationText;
			}
			set
			{
				if (value != this._relationText)
				{
					this._relationText = value;
					base.OnPropertyChangedWithValue<string>(value, "RelationText");
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0001B303 File Offset: 0x00019503
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0001B30B File Offset: 0x0001950B
		[DataSourceProperty]
		public string ConsequencesText
		{
			get
			{
				return this._consequencesText;
			}
			set
			{
				if (value != this._consequencesText)
				{
					this._consequencesText = value;
					base.OnPropertyChangedWithValue<string>(value, "ConsequencesText");
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0001B32E File Offset: 0x0001952E
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0001B336 File Offset: 0x00019536
		[DataSourceProperty]
		public MBBindingList<BindingListStringItem> ConsequencesList
		{
			get
			{
				return this._consequencesList;
			}
			set
			{
				if (value != this._consequencesList)
				{
					this._consequencesList = value;
					base.OnPropertyChangedWithValue<MBBindingList<BindingListStringItem>>(value, "ConsequencesList");
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0001B354 File Offset: 0x00019554
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0001B35C File Offset: 0x0001955C
		[DataSourceProperty]
		public string ButtonOkLabel
		{
			get
			{
				return this._buttonOkLabel;
			}
			set
			{
				if (value != this._buttonOkLabel)
				{
					this._buttonOkLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ButtonOkLabel");
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0001B37F File Offset: 0x0001957F
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001B387 File Offset: 0x00019587
		[DataSourceProperty]
		public string ButtonCancelLabel
		{
			get
			{
				return this._buttonCancelLabel;
			}
			set
			{
				if (value != this._buttonCancelLabel)
				{
					this._buttonCancelLabel = value;
					base.OnPropertyChangedWithValue<string>(value, "ButtonCancelLabel");
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0001B3AA File Offset: 0x000195AA
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0001B3B2 File Offset: 0x000195B2
		[DataSourceProperty]
		public bool IsEncyclopediaOpen
		{
			get
			{
				return this._isEncyclopediaOpen;
			}
			set
			{
				if (value != this._isEncyclopediaOpen)
				{
					this._isEncyclopediaOpen = value;
					base.OnPropertyChangedWithValue(value, "IsEncyclopediaOpen");
				}
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0001B3D0 File Offset: 0x000195D0
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0001B3D8 File Offset: 0x000195D8
		[DataSourceProperty]
		public MarriageOfferPopupHeroVM OffereeClanMember
		{
			get
			{
				return this._offereeClanMember;
			}
			set
			{
				if (value != this._offereeClanMember)
				{
					this._offereeClanMember = value;
					base.OnPropertyChangedWithValue<MarriageOfferPopupHeroVM>(value, "OffereeClanMember");
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0001B3F6 File Offset: 0x000195F6
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0001B3FE File Offset: 0x000195FE
		[DataSourceProperty]
		public MarriageOfferPopupHeroVM OffererClanMember
		{
			get
			{
				return this._offererClanMember;
			}
			set
			{
				if (value != this._offererClanMember)
				{
					this._offererClanMember = value;
					base.OnPropertyChangedWithValue<MarriageOfferPopupHeroVM>(value, "OffererClanMember");
				}
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001B41C File Offset: 0x0001961C
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001B42B File Offset: 0x0001962B
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0001B43A File Offset: 0x0001963A
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0001B442 File Offset: 0x00019642
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001B460 File Offset: 0x00019660
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0001B468 File Offset: 0x00019668
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x04000236 RID: 566
		private readonly IMarriageOfferCampaignBehavior _marriageBehavior;

		// Token: 0x04000237 RID: 567
		private string _titleText;

		// Token: 0x04000238 RID: 568
		private string _clanText;

		// Token: 0x04000239 RID: 569
		private string _ageText;

		// Token: 0x0400023A RID: 570
		private string _occupationText;

		// Token: 0x0400023B RID: 571
		private string _relationText;

		// Token: 0x0400023C RID: 572
		private string _consequencesText;

		// Token: 0x0400023D RID: 573
		private MBBindingList<BindingListStringItem> _consequencesList;

		// Token: 0x0400023E RID: 574
		private string _buttonOkLabel;

		// Token: 0x0400023F RID: 575
		private string _buttonCancelLabel;

		// Token: 0x04000240 RID: 576
		private bool _isEncyclopediaOpen;

		// Token: 0x04000241 RID: 577
		private MarriageOfferPopupHeroVM _offereeClanMember;

		// Token: 0x04000242 RID: 578
		private MarriageOfferPopupHeroVM _offererClanMember;

		// Token: 0x04000243 RID: 579
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000244 RID: 580
		private InputKeyItemVM _doneInputKey;
	}
}
