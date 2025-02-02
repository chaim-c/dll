using System;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp
{
	// Token: 0x02000031 RID: 49
	public abstract class PartyTroopManagerVM : ViewModel
	{
		// Token: 0x060004B4 RID: 1204
		public abstract void ExecuteItemPrimaryAction();

		// Token: 0x060004B5 RID: 1205
		public abstract void ExecuteItemSecondaryAction();

		// Token: 0x060004B6 RID: 1206 RVA: 0x000197B8 File Offset: 0x000179B8
		public PartyTroopManagerVM(PartyVM partyVM)
		{
			this._partyVM = partyVM;
			this.Troops = new MBBindingList<PartyTroopManagerItemVM>();
			this.OpenButtonHint = new HintViewModel();
			this.RefreshValues();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001981C File Offset: 0x00017A1C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.AvatarText = new TextObject("{=5tbWdY1j}Avatar", null).ToString();
			this.NameText = new TextObject("{=PDdh1sBj}Name", null).ToString();
			this.CountText = new TextObject("{=zFDoDbNj}Count", null).ToString();
			this.DoneLbl = GameTexts.FindText("str_done", null).ToString();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00019887 File Offset: 0x00017A87
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.DoneInputKey.OnFinalize();
			InputKeyItemVM primaryActionInputKey = this.PrimaryActionInputKey;
			if (primaryActionInputKey != null)
			{
				primaryActionInputKey.OnFinalize();
			}
			InputKeyItemVM secondaryActionInputKey = this.SecondaryActionInputKey;
			if (secondaryActionInputKey == null)
			{
				return;
			}
			secondaryActionInputKey.OnFinalize();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000198BC File Offset: 0x00017ABC
		public virtual void OpenPopUp()
		{
			this._partyVM.PartyScreenLogic.SavePartyScreenData();
			this._initialGoldChange = this._partyVM.PartyScreenLogic.CurrentData.PartyGoldChangeAmount;
			this._initialHorseChange = this._partyVM.PartyScreenLogic.CurrentData.PartyHorseChangeAmount;
			this._initialMoraleChange = this._partyVM.PartyScreenLogic.CurrentData.PartyMoraleChangeAmount;
			this.UpdateLabels();
			this._hasMadeChanges = false;
			this.IsOpen = true;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001993E File Offset: 0x00017B3E
		public virtual void ExecuteDone()
		{
			this.IsOpen = false;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00019947 File Offset: 0x00017B47
		protected virtual void ConfirmCancel()
		{
			this._partyVM.PartyScreenLogic.ResetToLastSavedPartyScreenData(false);
			this.IsOpen = false;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00019964 File Offset: 0x00017B64
		public void UpdateOpenButtonHint(bool isDisabled, bool isIrrelevant, bool isUpgradesDisabled)
		{
			TextObject hintText;
			if (isIrrelevant)
			{
				hintText = this._openButtonIrrelevantScreenHint;
			}
			else if (isUpgradesDisabled)
			{
				hintText = this._openButtonUpgradesDisabledHint;
			}
			else if (isDisabled)
			{
				hintText = this._openButtonNoTroopsHint;
			}
			else
			{
				hintText = this._openButtonEnabledHint;
			}
			this.OpenButtonHint.HintText = hintText;
		}

		// Token: 0x060004BD RID: 1213
		public abstract void ExecuteCancel();

		// Token: 0x060004BE RID: 1214 RVA: 0x000199AC File Offset: 0x00017BAC
		protected void ShowCancelInquiry(Action confirmCancel)
		{
			if (this._hasMadeChanges)
			{
				string text = new TextObject("{=a8NoW1Q2}Are you sure you want to cancel your changes?", null).ToString();
				InformationManager.ShowInquiry(new InquiryData("", text, true, true, GameTexts.FindText("str_yes", null).ToString(), GameTexts.FindText("str_no", null).ToString(), delegate()
				{
					confirmCancel();
				}, null, "", 0f, null, null, null), false, false);
				return;
			}
			confirmCancel();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x00019A3C File Offset: 0x00017C3C
		protected void UpdateLabels()
		{
			MBTextManager.SetTextVariable("PAY_OR_GET", 0);
			int num = this._partyVM.PartyScreenLogic.CurrentData.PartyGoldChangeAmount - this._initialGoldChange;
			int num2 = this._partyVM.PartyScreenLogic.CurrentData.PartyHorseChangeAmount - this._initialHorseChange;
			int num3 = this._partyVM.PartyScreenLogic.CurrentData.PartyMoraleChangeAmount - this._initialMoraleChange;
			MBTextManager.SetTextVariable("LABEL_ICON", "{=!}<img src=\"General\\Icons\\Coin@2x\" extend=\"8\">", false);
			MBTextManager.SetTextVariable("TRADE_AMOUNT", MathF.Abs(num));
			this.GoldChangeText = ((num == 0) ? "" : GameTexts.FindText("str_party_generic_label", null).ToString());
			MBTextManager.SetTextVariable("LABEL_ICON", "{=!}<img src=\"StdAssets\\ItemIcons\\Mount\" extend=\"16\">", false);
			MBTextManager.SetTextVariable("TRADE_AMOUNT", MathF.Abs(num2));
			this.HorseChangeText = ((num2 == 0) ? "" : GameTexts.FindText("str_party_generic_label", null).ToString());
			MBTextManager.SetTextVariable("LABEL_ICON", "{=!}<img src=\"General\\Icons\\Morale@2x\" extend=\"8\">", false);
			MBTextManager.SetTextVariable("TRADE_AMOUNT", MathF.Abs(num3));
			this.MoraleChangeText = ((num3 == 0) ? "" : GameTexts.FindText("str_party_generic_label", null).ToString());
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00019B6C File Offset: 0x00017D6C
		protected void SetFocusedCharacter(PartyTroopManagerItemVM troop)
		{
			this.FocusedTroop = troop;
			this.IsFocusedOnACharacter = (troop != null);
			if (this.FocusedTroop == null)
			{
				this.IsPrimaryActionAvailable = false;
				this.IsSecondaryActionAvailable = false;
				return;
			}
			if (this.IsUpgradePopUp)
			{
				MBBindingList<UpgradeTargetVM> upgrades = this.FocusedTroop.PartyCharacter.Upgrades;
				this.IsPrimaryActionAvailable = (upgrades.Count > 0 && upgrades[0].IsAvailable);
				this.IsSecondaryActionAvailable = (upgrades.Count > 1 && upgrades[1].IsAvailable);
				return;
			}
			this.IsPrimaryActionAvailable = false;
			this.IsSecondaryActionAvailable = this.FocusedTroop.IsTroopRecruitable;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00019C0F File Offset: 0x00017E0F
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00019C1E File Offset: 0x00017E1E
		public void SetPrimaryActionInputKey(HotKey hotKey)
		{
			this.PrimaryActionInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00019C2D File Offset: 0x00017E2D
		public void SetSecondaryActionInputKey(HotKey hotKey)
		{
			this.SecondaryActionInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00019C3C File Offset: 0x00017E3C
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00019C44 File Offset: 0x00017E44
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00019C62 File Offset: 0x00017E62
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00019C6A File Offset: 0x00017E6A
		[DataSourceProperty]
		public InputKeyItemVM PrimaryActionInputKey
		{
			get
			{
				return this._primaryActionInputKey;
			}
			set
			{
				if (value != this._primaryActionInputKey)
				{
					this._primaryActionInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "PrimaryActionInputKey");
				}
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00019C88 File Offset: 0x00017E88
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00019C90 File Offset: 0x00017E90
		[DataSourceProperty]
		public InputKeyItemVM SecondaryActionInputKey
		{
			get
			{
				return this._secondaryActionInputKey;
			}
			set
			{
				if (value != this._secondaryActionInputKey)
				{
					this._secondaryActionInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "SecondaryActionInputKey");
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00019CAE File Offset: 0x00017EAE
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00019CB6 File Offset: 0x00017EB6
		[DataSourceProperty]
		public bool IsFocusedOnACharacter
		{
			get
			{
				return this._isFocusedOnACharacter;
			}
			set
			{
				if (value != this._isFocusedOnACharacter)
				{
					this._isFocusedOnACharacter = value;
					base.OnPropertyChangedWithValue(value, "IsFocusedOnACharacter");
				}
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00019CD4 File Offset: 0x00017ED4
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00019CDC File Offset: 0x00017EDC
		[DataSourceProperty]
		public bool IsOpen
		{
			get
			{
				return this._isOpen;
			}
			set
			{
				if (value != this._isOpen)
				{
					this._isOpen = value;
					base.OnPropertyChangedWithValue(value, "IsOpen");
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00019CFA File Offset: 0x00017EFA
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00019D02 File Offset: 0x00017F02
		[DataSourceProperty]
		public bool IsUpgradePopUp
		{
			get
			{
				return this._isUpgradePopUp;
			}
			set
			{
				if (value != this._isUpgradePopUp)
				{
					this._isUpgradePopUp = value;
					base.OnPropertyChangedWithValue(value, "IsUpgradePopUp");
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00019D20 File Offset: 0x00017F20
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x00019D28 File Offset: 0x00017F28
		[DataSourceProperty]
		public bool IsPrimaryActionAvailable
		{
			get
			{
				return this._isPrimaryActionAvailable;
			}
			set
			{
				if (value != this._isPrimaryActionAvailable)
				{
					this._isPrimaryActionAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsPrimaryActionAvailable");
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00019D46 File Offset: 0x00017F46
		// (set) Token: 0x060004D3 RID: 1235 RVA: 0x00019D4E File Offset: 0x00017F4E
		[DataSourceProperty]
		public bool IsSecondaryActionAvailable
		{
			get
			{
				return this._isSecondaryActionAvailable;
			}
			set
			{
				if (value != this._isSecondaryActionAvailable)
				{
					this._isSecondaryActionAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsSecondaryActionAvailable");
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00019D6C File Offset: 0x00017F6C
		// (set) Token: 0x060004D5 RID: 1237 RVA: 0x00019D74 File Offset: 0x00017F74
		[DataSourceProperty]
		public PartyTroopManagerItemVM FocusedTroop
		{
			get
			{
				return this._focusedTroop;
			}
			set
			{
				if (value != this._focusedTroop)
				{
					this._focusedTroop = value;
					base.OnPropertyChangedWithValue<PartyTroopManagerItemVM>(value, "FocusedTroop");
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00019D92 File Offset: 0x00017F92
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00019D9A File Offset: 0x00017F9A
		[DataSourceProperty]
		public MBBindingList<PartyTroopManagerItemVM> Troops
		{
			get
			{
				return this._troops;
			}
			set
			{
				if (value != this._troops)
				{
					this._troops = value;
					base.OnPropertyChangedWithValue<MBBindingList<PartyTroopManagerItemVM>>(value, "Troops");
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00019DB8 File Offset: 0x00017FB8
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00019DC0 File Offset: 0x00017FC0
		[DataSourceProperty]
		public HintViewModel OpenButtonHint
		{
			get
			{
				return this._openButtonHint;
			}
			set
			{
				if (value != this._openButtonHint)
				{
					this._openButtonHint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "OpenButtonHint");
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00019DDE File Offset: 0x00017FDE
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00019DE6 File Offset: 0x00017FE6
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

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00019E09 File Offset: 0x00018009
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00019E11 File Offset: 0x00018011
		[DataSourceProperty]
		public string AvatarText
		{
			get
			{
				return this._avatarText;
			}
			set
			{
				if (value != this._avatarText)
				{
					this._avatarText = value;
					base.OnPropertyChangedWithValue<string>(value, "AvatarText");
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00019E34 File Offset: 0x00018034
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00019E3C File Offset: 0x0001803C
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

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00019E5F File Offset: 0x0001805F
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00019E67 File Offset: 0x00018067
		[DataSourceProperty]
		public string CountText
		{
			get
			{
				return this._countText;
			}
			set
			{
				if (value != this._countText)
				{
					this._countText = value;
					base.OnPropertyChangedWithValue<string>(value, "CountText");
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00019E8A File Offset: 0x0001808A
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00019E92 File Offset: 0x00018092
		[DataSourceProperty]
		public string GoldChangeText
		{
			get
			{
				return this._goldChangeText;
			}
			set
			{
				if (value != this._goldChangeText)
				{
					this._goldChangeText = value;
					base.OnPropertyChangedWithValue<string>(value, "GoldChangeText");
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00019EB5 File Offset: 0x000180B5
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00019EBD File Offset: 0x000180BD
		[DataSourceProperty]
		public string HorseChangeText
		{
			get
			{
				return this._horseChangeText;
			}
			set
			{
				if (value != this._horseChangeText)
				{
					this._horseChangeText = value;
					base.OnPropertyChangedWithValue<string>(value, "HorseChangeText");
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00019EE0 File Offset: 0x000180E0
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00019EE8 File Offset: 0x000180E8
		[DataSourceProperty]
		public string MoraleChangeText
		{
			get
			{
				return this._moraleChangeText;
			}
			set
			{
				if (value != this._moraleChangeText)
				{
					this._moraleChangeText = value;
					base.OnPropertyChangedWithValue<string>(value, "MoraleChangeText");
				}
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00019F0B File Offset: 0x0001810B
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00019F13 File Offset: 0x00018113
		[DataSourceProperty]
		public string DoneLbl
		{
			get
			{
				return this._doneLbl;
			}
			set
			{
				if (value != this._doneLbl)
				{
					this._doneLbl = value;
					base.OnPropertyChangedWithValue<string>(value, "DoneLbl");
				}
			}
		}

		// Token: 0x04000201 RID: 513
		protected PartyVM _partyVM;

		// Token: 0x04000202 RID: 514
		protected bool _hasMadeChanges;

		// Token: 0x04000203 RID: 515
		protected TextObject _openButtonEnabledHint = TextObject.Empty;

		// Token: 0x04000204 RID: 516
		protected TextObject _openButtonNoTroopsHint = TextObject.Empty;

		// Token: 0x04000205 RID: 517
		protected TextObject _openButtonIrrelevantScreenHint = TextObject.Empty;

		// Token: 0x04000206 RID: 518
		protected TextObject _openButtonUpgradesDisabledHint = TextObject.Empty;

		// Token: 0x04000207 RID: 519
		private int _initialGoldChange;

		// Token: 0x04000208 RID: 520
		private int _initialHorseChange;

		// Token: 0x04000209 RID: 521
		private int _initialMoraleChange;

		// Token: 0x0400020A RID: 522
		private InputKeyItemVM _doneInputKey;

		// Token: 0x0400020B RID: 523
		private InputKeyItemVM _primaryActionInputKey;

		// Token: 0x0400020C RID: 524
		private InputKeyItemVM _secondaryActionInputKey;

		// Token: 0x0400020D RID: 525
		private bool _isFocusedOnACharacter;

		// Token: 0x0400020E RID: 526
		private bool _isOpen;

		// Token: 0x0400020F RID: 527
		private bool _isUpgradePopUp;

		// Token: 0x04000210 RID: 528
		private bool _isPrimaryActionAvailable;

		// Token: 0x04000211 RID: 529
		private bool _isSecondaryActionAvailable;

		// Token: 0x04000212 RID: 530
		private PartyTroopManagerItemVM _focusedTroop;

		// Token: 0x04000213 RID: 531
		private MBBindingList<PartyTroopManagerItemVM> _troops;

		// Token: 0x04000214 RID: 532
		private HintViewModel _openButtonHint;

		// Token: 0x04000215 RID: 533
		private string _titleText;

		// Token: 0x04000216 RID: 534
		private string _avatarText;

		// Token: 0x04000217 RID: 535
		private string _nameText;

		// Token: 0x04000218 RID: 536
		private string _countText;

		// Token: 0x04000219 RID: 537
		private string _goldChangeText;

		// Token: 0x0400021A RID: 538
		private string _horseChangeText;

		// Token: 0x0400021B RID: 539
		private string _moraleChangeText;

		// Token: 0x0400021C RID: 540
		private string _doneLbl;
	}
}
