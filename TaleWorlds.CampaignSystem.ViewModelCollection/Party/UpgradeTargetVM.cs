using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x0200002D RID: 45
	public class UpgradeTargetVM : ViewModel
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x00018E30 File Offset: 0x00017030
		public UpgradeTargetVM(int upgradeIndex, CharacterObject character, CharacterCode upgradeCharacterCode, Action<int, int> onUpgraded, Action<UpgradeTargetVM> onFocused)
		{
			this._upgradeIndex = upgradeIndex;
			this._originalCharacter = character;
			this._upgradeTarget = this._originalCharacter.UpgradeTargets[upgradeIndex];
			this._onUpgraded = onUpgraded;
			this._onFocused = onFocused;
			PerkObject perkRequirement;
			Campaign.Current.Models.PartyTroopUpgradeModel.DoesPartyHaveRequiredPerksForUpgrade(PartyBase.MainParty, this._originalCharacter, this._upgradeTarget, out perkRequirement);
			this.Requirements = new UpgradeRequirementsVM();
			this.Requirements.SetItemRequirement(this._upgradeTarget.UpgradeRequiresItemFromCategory);
			this.Requirements.SetPerkRequirement(perkRequirement);
			this.TroopImage = new ImageIdentifierVM(upgradeCharacterCode);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00018ED5 File Offset: 0x000170D5
		public override void RefreshValues()
		{
			base.RefreshValues();
			UpgradeRequirementsVM requirements = this.Requirements;
			if (requirements == null)
			{
				return;
			}
			requirements.RefreshValues();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00018EF0 File Offset: 0x000170F0
		public void Refresh(int upgradableAmount, string hint, bool isAvailable, bool isInsufficient, bool itemRequirementsMet, bool perkRequirementsMet)
		{
			this.AvailableUpgrades = upgradableAmount;
			this.Hint = new HintViewModel(new TextObject("{=!}" + hint, null), null);
			this.IsAvailable = isAvailable;
			this.IsInsufficient = isInsufficient;
			UpgradeRequirementsVM requirements = this.Requirements;
			if (requirements == null)
			{
				return;
			}
			requirements.SetRequirementsMet(itemRequirementsMet, perkRequirementsMet);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00018F44 File Offset: 0x00017144
		public void ExecuteUpgradeEncyclopediaLink()
		{
			Campaign.Current.EncyclopediaManager.GoToLink(this._upgradeTarget.EncyclopediaLink);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00018F60 File Offset: 0x00017160
		public void ExecuteUpgrade()
		{
			if (this.IsAvailable && !this.IsInsufficient)
			{
				Action<int, int> onUpgraded = this._onUpgraded;
				if (onUpgraded == null)
				{
					return;
				}
				onUpgraded(this._upgradeIndex, this.AvailableUpgrades);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00018F8E File Offset: 0x0001718E
		public void ExecuteSetFocused()
		{
			if (this._upgradeTarget != null)
			{
				Action<UpgradeTargetVM> onFocused = this._onFocused;
				if (onFocused == null)
				{
					return;
				}
				onFocused(this);
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00018FA9 File Offset: 0x000171A9
		public void ExecuteSetUnfocused()
		{
			Action<UpgradeTargetVM> onFocused = this._onFocused;
			if (onFocused == null)
			{
				return;
			}
			onFocused(null);
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00018FBC File Offset: 0x000171BC
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00018FC4 File Offset: 0x000171C4
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

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00018FE2 File Offset: 0x000171E2
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00018FEA File Offset: 0x000171EA
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

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00019008 File Offset: 0x00017208
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00019010 File Offset: 0x00017210
		[DataSourceProperty]
		public UpgradeRequirementsVM Requirements
		{
			get
			{
				return this._requirements;
			}
			set
			{
				if (value != this._requirements)
				{
					this._requirements = value;
					base.OnPropertyChangedWithValue<UpgradeRequirementsVM>(value, "Requirements");
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001902E File Offset: 0x0001722E
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00019036 File Offset: 0x00017236
		[DataSourceProperty]
		public ImageIdentifierVM TroopImage
		{
			get
			{
				return this._troopImage;
			}
			set
			{
				if (value != this._troopImage)
				{
					this._troopImage = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "TroopImage");
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00019054 File Offset: 0x00017254
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001905C File Offset: 0x0001725C
		[DataSourceProperty]
		public HintViewModel Hint
		{
			get
			{
				return this._hint;
			}
			set
			{
				if (value != this._hint)
				{
					this._hint = value;
					base.OnPropertyChangedWithValue<HintViewModel>(value, "Hint");
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001907A File Offset: 0x0001727A
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00019082 File Offset: 0x00017282
		[DataSourceProperty]
		public int AvailableUpgrades
		{
			get
			{
				return this._availableUpgrades;
			}
			set
			{
				if (value != this._availableUpgrades)
				{
					this._availableUpgrades = value;
					base.OnPropertyChangedWithValue(value, "AvailableUpgrades");
				}
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x000190A0 File Offset: 0x000172A0
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x000190A8 File Offset: 0x000172A8
		[DataSourceProperty]
		public bool IsAvailable
		{
			get
			{
				return this._isAvailable;
			}
			set
			{
				if (value != this._isAvailable)
				{
					this._isAvailable = value;
					base.OnPropertyChangedWithValue(value, "IsAvailable");
				}
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x000190C6 File Offset: 0x000172C6
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000190CE File Offset: 0x000172CE
		[DataSourceProperty]
		public bool IsInsufficient
		{
			get
			{
				return this._isInsufficient;
			}
			set
			{
				if (value != this._isInsufficient)
				{
					this._isInsufficient = value;
					base.OnPropertyChangedWithValue(value, "IsInsufficient");
				}
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x000190EC File Offset: 0x000172EC
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x000190F4 File Offset: 0x000172F4
		[DataSourceProperty]
		public bool IsHighlighted
		{
			get
			{
				return this._isHighlighted;
			}
			set
			{
				if (value != this._isHighlighted)
				{
					this._isHighlighted = value;
					base.OnPropertyChangedWithValue(value, "IsHighlighted");
				}
			}
		}

		// Token: 0x040001E3 RID: 483
		private CharacterObject _originalCharacter;

		// Token: 0x040001E4 RID: 484
		private CharacterObject _upgradeTarget;

		// Token: 0x040001E5 RID: 485
		private Action<int, int> _onUpgraded;

		// Token: 0x040001E6 RID: 486
		private Action<UpgradeTargetVM> _onFocused;

		// Token: 0x040001E7 RID: 487
		private int _upgradeIndex;

		// Token: 0x040001E8 RID: 488
		private InputKeyItemVM _primaryActionInputKey;

		// Token: 0x040001E9 RID: 489
		private InputKeyItemVM _secondaryActionInputKey;

		// Token: 0x040001EA RID: 490
		private UpgradeRequirementsVM _requirements;

		// Token: 0x040001EB RID: 491
		private ImageIdentifierVM _troopImage;

		// Token: 0x040001EC RID: 492
		private HintViewModel _hint;

		// Token: 0x040001ED RID: 493
		private int _availableUpgrades;

		// Token: 0x040001EE RID: 494
		private bool _isAvailable;

		// Token: 0x040001EF RID: 495
		private bool _isInsufficient;

		// Token: 0x040001F0 RID: 496
		private bool _isHighlighted;
	}
}
