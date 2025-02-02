using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation
{
	// Token: 0x0200012C RID: 300
	public class CharacterCreationClanNamingStageVM : CharacterCreationStageBaseVM
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x00069454 File Offset: 0x00067654
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x0006945C File Offset: 0x0006765C
		public BasicCharacterObject Character { get; private set; }

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x00069465 File Offset: 0x00067665
		// (set) Token: 0x06001D58 RID: 7512 RVA: 0x0006946D File Offset: 0x0006766D
		public int ShieldSlotIndex { get; private set; } = 3;

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00069476 File Offset: 0x00067676
		// (set) Token: 0x06001D5A RID: 7514 RVA: 0x0006947E File Offset: 0x0006767E
		public ItemRosterElement ShieldRosterElement { get; private set; }

		// Token: 0x06001D5B RID: 7515 RVA: 0x00069488 File Offset: 0x00067688
		public CharacterCreationClanNamingStageVM(BasicCharacterObject character, Banner banner, CharacterCreation characterCreation, Action affirmativeAction, TextObject affirmativeActionText, Action negativeAction, TextObject negativeActionText, int currentStageIndex, int totalStagesCount, int furthestIndex, Action<int> goToIndex) : base(characterCreation, affirmativeAction, affirmativeActionText, negativeAction, negativeActionText, currentStageIndex, totalStagesCount, furthestIndex, goToIndex)
		{
			this.Character = character;
			this.ClanName = Hero.MainHero.Clan.Name.ToString();
			ItemObject item = this.FindShield();
			this.ShieldRosterElement = new ItemRosterElement(item, 1, null);
			this.ClanBanner = new ImageIdentifierVM(BannerCode.CreateFrom(Hero.MainHero.Clan.Banner), true);
			this.RefreshValues();
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00069510 File Offset: 0x00067710
		public override void RefreshValues()
		{
			base.RefreshValues();
			base.Title = new TextObject("{=wNUcqcJP}Clan Name", null).ToString();
			base.Description = new TextObject("{=JJiKk4ow}Select your family name: ", null).ToString();
			this.BottomHintText = new TextObject("{=dbBAJ8yi}You can change your banner and clan name later on clan screen", null).ToString();
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00069568 File Offset: 0x00067768
		public override bool CanAdvanceToNextStage()
		{
			Tuple<bool, string> tuple = FactionHelper.IsClanNameApplicable(this.ClanName);
			this.ClanNameNotApplicableReason = tuple.Item2;
			return tuple.Item1;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00069593 File Offset: 0x00067793
		public override void OnNextStage()
		{
			this._affirmativeAction();
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x000695A0 File Offset: 0x000677A0
		public override void OnPreviousStage()
		{
			this._negativeAction();
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x000695B0 File Offset: 0x000677B0
		private ItemObject FindShield()
		{
			for (int i = 0; i < 4; i++)
			{
				EquipmentElement equipmentFromSlot = this.Character.Equipment.GetEquipmentFromSlot((EquipmentIndex)i);
				ItemObject item = equipmentFromSlot.Item;
				if (((item != null) ? item.PrimaryWeapon : null) != null && equipmentFromSlot.Item.PrimaryWeapon.IsShield && equipmentFromSlot.Item.IsUsingTableau)
				{
					return equipmentFromSlot.Item;
				}
			}
			foreach (ItemObject itemObject in Game.Current.ObjectManager.GetObjectTypeList<ItemObject>())
			{
				if (itemObject.PrimaryWeapon != null && itemObject.PrimaryWeapon.IsShield && itemObject.IsUsingTableau)
				{
					return itemObject;
				}
			}
			return null;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00069688 File Offset: 0x00067888
		public override void OnFinalize()
		{
			base.OnFinalize();
			InputKeyItemVM cancelInputKey = this.CancelInputKey;
			if (cancelInputKey != null)
			{
				cancelInputKey.OnFinalize();
			}
			InputKeyItemVM doneInputKey = this.DoneInputKey;
			if (doneInputKey == null)
			{
				return;
			}
			doneInputKey.OnFinalize();
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000696B1 File Offset: 0x000678B1
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000696C0 File Offset: 0x000678C0
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000696CF File Offset: 0x000678CF
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x000696D7 File Offset: 0x000678D7
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

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000696F5 File Offset: 0x000678F5
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x000696FD File Offset: 0x000678FD
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

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0006971B File Offset: 0x0006791B
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x00069723 File Offset: 0x00067923
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
					base.OnPropertyChanged("CanAdvance");
				}
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00069751 File Offset: 0x00067951
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00069759 File Offset: 0x00067959
		[DataSourceProperty]
		public string ClanNameNotApplicableReason
		{
			get
			{
				return this._clanNameNotApplicableReason;
			}
			set
			{
				if (value != this._clanNameNotApplicableReason)
				{
					this._clanNameNotApplicableReason = value;
					base.OnPropertyChangedWithValue<string>(value, "ClanNameNotApplicableReason");
				}
			}
		}

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0006977C File Offset: 0x0006797C
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x00069784 File Offset: 0x00067984
		[DataSourceProperty]
		public string BottomHintText
		{
			get
			{
				return this._bottomHintText;
			}
			set
			{
				if (value != this._bottomHintText)
				{
					this._bottomHintText = value;
					base.OnPropertyChangedWithValue<string>(value, "BottomHintText");
				}
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000697A7 File Offset: 0x000679A7
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000697AF File Offset: 0x000679AF
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

		// Token: 0x04000DDE RID: 3550
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000DDF RID: 3551
		private InputKeyItemVM _doneInputKey;

		// Token: 0x04000DE0 RID: 3552
		private string _clanName;

		// Token: 0x04000DE1 RID: 3553
		private string _clanNameNotApplicableReason;

		// Token: 0x04000DE2 RID: 3554
		private string _bottomHintText;

		// Token: 0x04000DE3 RID: 3555
		private ImageIdentifierVM _clanBanner;
	}
}
