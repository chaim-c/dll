using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party.PartyTroopManagerPopUp
{
	// Token: 0x02000032 RID: 50
	public class PartyUpgradeTroopVM : PartyTroopManagerVM
	{
		// Token: 0x060004EA RID: 1258 RVA: 0x00019F38 File Offset: 0x00018138
		public PartyUpgradeTroopVM(PartyVM partyVM) : base(partyVM)
		{
			this.RefreshValues();
			base.IsUpgradePopUp = true;
			this._openButtonEnabledHint = new TextObject("{=hRSezxnT}Some of your troops are ready to upgrade.", null);
			this._openButtonNoTroopsHint = new TextObject("{=fpE7BQ7f}You don't have any upgradable troops.", null);
			this._openButtonIrrelevantScreenHint = new TextObject("{=mdvnjI72}Troops are not upgradable in this screen.", null);
			this._openButtonUpgradesDisabledHint = new TextObject("{=R4rTlKMU}Troop upgrades are currently disabled.", null);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00019FA4 File Offset: 0x000181A4
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.TitleText = new TextObject("{=IgoxNz2H}Upgrade Troops", null).ToString();
			this.UpgradeCostText = new TextObject("{=SK8G9QpE}Upgrd. Cost", null).ToString();
			GameTexts.SetVariable("LEFT", new TextObject("{=6bx9IhpD}Upgrades", null).ToString());
			GameTexts.SetVariable("RIGHT", new TextObject("{=guxNZZWh}Requirements", null).ToString());
			this.UpgradesAndRequirementsText = GameTexts.FindText("str_LEFT_over_RIGHT", null).ToString();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001A030 File Offset: 0x00018230
		public void OnRanOutTroop(PartyCharacterVM troop)
		{
			if (!base.IsOpen)
			{
				return;
			}
			PartyTroopManagerItemVM item = base.Troops.FirstOrDefault((PartyTroopManagerItemVM x) => x.PartyCharacter == troop);
			base.Troops.Remove(item);
			this._disabledTroopsStartIndex--;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001A088 File Offset: 0x00018288
		public void OnTroopUpgraded()
		{
			if (!base.IsOpen)
			{
				return;
			}
			this._hasMadeChanges = true;
			for (int i = 0; i < this._disabledTroopsStartIndex; i++)
			{
				if (base.Troops[i].PartyCharacter.NumOfReadyToUpgradeTroops <= 0)
				{
					this._disabledTroopsStartIndex--;
					base.Troops.RemoveAt(i);
					i--;
				}
				else if (base.Troops[i].PartyCharacter.NumOfUpgradeableTroops <= 0)
				{
					this._disabledTroopsStartIndex--;
					PartyTroopManagerItemVM item = base.Troops[i];
					base.Troops.RemoveAt(i);
					base.Troops.Insert(this._disabledTroopsStartIndex, item);
					i--;
				}
			}
			base.UpdateLabels();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A151 File Offset: 0x00018351
		public override void OpenPopUp()
		{
			base.OpenPopUp();
			this.PopulateTroops();
			this.UpdateUpgradesOfAllTroops();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A165 File Offset: 0x00018365
		public override void ExecuteDone()
		{
			base.ExecuteDone();
			this._partyVM.OnUpgradePopUpClosed(false);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001A179 File Offset: 0x00018379
		public override void ExecuteCancel()
		{
			base.ShowCancelInquiry(new Action(this.ConfirmCancel));
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001A18E File Offset: 0x0001838E
		protected override void ConfirmCancel()
		{
			base.ConfirmCancel();
			this._partyVM.OnUpgradePopUpClosed(true);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001A1A4 File Offset: 0x000183A4
		private void UpdateUpgradesOfAllTroops()
		{
			foreach (PartyTroopManagerItemVM partyTroopManagerItemVM in base.Troops)
			{
				partyTroopManagerItemVM.PartyCharacter.InitializeUpgrades();
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001A1F4 File Offset: 0x000183F4
		private void PopulateTroops()
		{
			base.Troops = new MBBindingList<PartyTroopManagerItemVM>();
			this._disabledTroopsStartIndex = 0;
			foreach (PartyCharacterVM partyCharacterVM in this._partyVM.MainPartyTroops)
			{
				if (partyCharacterVM.IsTroopUpgradable)
				{
					base.Troops.Insert(this._disabledTroopsStartIndex, new PartyTroopManagerItemVM(partyCharacterVM, new Action<PartyTroopManagerItemVM>(base.SetFocusedCharacter)));
					this._disabledTroopsStartIndex++;
				}
				else if (partyCharacterVM.NumOfReadyToUpgradeTroops > 0)
				{
					base.Troops.Add(new PartyTroopManagerItemVM(partyCharacterVM, new Action<PartyTroopManagerItemVM>(base.SetFocusedCharacter)));
				}
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001A2B4 File Offset: 0x000184B4
		public override void ExecuteItemPrimaryAction()
		{
			PartyTroopManagerItemVM focusedTroop = base.FocusedTroop;
			PartyCharacterVM partyCharacterVM = (focusedTroop != null) ? focusedTroop.PartyCharacter : null;
			if (partyCharacterVM != null && partyCharacterVM.Upgrades.Count > 0 && partyCharacterVM.Upgrades[0].IsAvailable)
			{
				partyCharacterVM.Upgrades[0].ExecuteUpgrade();
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001A30C File Offset: 0x0001850C
		public override void ExecuteItemSecondaryAction()
		{
			PartyTroopManagerItemVM focusedTroop = base.FocusedTroop;
			PartyCharacterVM partyCharacterVM = (focusedTroop != null) ? focusedTroop.PartyCharacter : null;
			if (partyCharacterVM != null && partyCharacterVM.Upgrades.Count > 1 && partyCharacterVM.Upgrades[1].IsAvailable)
			{
				partyCharacterVM.Upgrades[1].ExecuteUpgrade();
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x0001A361 File Offset: 0x00018561
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x0001A369 File Offset: 0x00018569
		[DataSourceProperty]
		public string UpgradeCostText
		{
			get
			{
				return this._upgradeCostText;
			}
			set
			{
				if (value != this._upgradeCostText)
				{
					this._upgradeCostText = value;
					base.OnPropertyChangedWithValue<string>(value, "UpgradeCostText");
				}
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001A38C File Offset: 0x0001858C
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x0001A394 File Offset: 0x00018594
		[DataSourceProperty]
		public string UpgradesAndRequirementsText
		{
			get
			{
				return this._upgradesAndRequirementsText;
			}
			set
			{
				if (value != this._upgradesAndRequirementsText)
				{
					this._upgradesAndRequirementsText = value;
					base.OnPropertyChangedWithValue<string>(value, "UpgradesAndRequirementsText");
				}
			}
		}

		// Token: 0x0400021D RID: 541
		private int _disabledTroopsStartIndex = -1;

		// Token: 0x0400021E RID: 542
		private string _upgradeCostText;

		// Token: 0x0400021F RID: 543
		private string _upgradesAndRequirementsText;
	}
}
