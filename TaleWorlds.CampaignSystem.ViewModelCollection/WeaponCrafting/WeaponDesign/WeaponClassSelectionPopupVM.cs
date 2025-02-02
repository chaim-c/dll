using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000E8 RID: 232
	public class WeaponClassSelectionPopupVM : ViewModel
	{
		// Token: 0x06001585 RID: 5509 RVA: 0x00050BD4 File Offset: 0x0004EDD4
		public WeaponClassSelectionPopupVM(ICraftingCampaignBehavior craftingBehavior, List<CraftingTemplate> templatesList, Action<int> onSelect, Func<CraftingTemplate, int> getUnlockedPiecesCount)
		{
			this.WeaponClasses = new MBBindingList<WeaponClassVM>();
			this._craftingBehavior = craftingBehavior;
			this._onSelect = onSelect;
			this._templatesList = templatesList;
			this._getUnlockedPiecesCount = getUnlockedPiecesCount;
			foreach (CraftingTemplate craftingTemplate in this._templatesList)
			{
				this.WeaponClasses.Add(new WeaponClassVM(this._templatesList.IndexOf(craftingTemplate), craftingTemplate, new Action<int>(this.ExecuteSelectWeaponClass)));
			}
			this.RefreshList();
			this.RefreshValues();
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00050C84 File Offset: 0x0004EE84
		private void RefreshList()
		{
			foreach (WeaponClassVM weaponClassVM in this.WeaponClasses)
			{
				WeaponClassVM weaponClassVM2 = weaponClassVM;
				Func<CraftingTemplate, int> getUnlockedPiecesCount = this._getUnlockedPiecesCount;
				weaponClassVM2.UnlockedPiecesCount = ((getUnlockedPiecesCount != null) ? getUnlockedPiecesCount(weaponClassVM.Template) : 0);
				weaponClassVM.HasNewlyUnlockedPieces = (weaponClassVM.NewlyUnlockedPieceCount > 0);
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00050CF8 File Offset: 0x0004EEF8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.PopupHeader = new TextObject("{=wZGj3qO1}Choose What to Craft", null).ToString();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00050D18 File Offset: 0x0004EF18
		public void UpdateNewlyUnlockedPiecesCount(List<CraftingPiece> newlyUnlockedPieces)
		{
			for (int i = 0; i < this.WeaponClasses.Count; i++)
			{
				WeaponClassVM weaponClassVM = this.WeaponClasses[i];
				int num = 0;
				for (int j = 0; j < newlyUnlockedPieces.Count; j++)
				{
					CraftingPiece craftingPiece = newlyUnlockedPieces[j];
					if (weaponClassVM.Template.IsPieceTypeUsable(craftingPiece.PieceType))
					{
						CraftingPiece craftingPiece2 = this.FindPieceInTemplate(weaponClassVM.Template, craftingPiece);
						if (craftingPiece2 != null && !craftingPiece2.IsHiddenOnDesigner && this._craftingBehavior.IsOpened(craftingPiece2, weaponClassVM.Template))
						{
							num++;
						}
					}
				}
				weaponClassVM.NewlyUnlockedPieceCount = num;
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00050DB8 File Offset: 0x0004EFB8
		private CraftingPiece FindPieceInTemplate(CraftingTemplate template, CraftingPiece piece)
		{
			foreach (CraftingPiece craftingPiece in template.Pieces)
			{
				if (piece.StringId == craftingPiece.StringId)
				{
					return craftingPiece;
				}
			}
			return null;
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00050E20 File Offset: 0x0004F020
		public void ExecuteSelectWeaponClass(int index)
		{
			if (this.WeaponClasses[index].IsSelected)
			{
				this.ExecuteClosePopup();
				return;
			}
			Action<int> onSelect = this._onSelect;
			if (onSelect != null)
			{
				onSelect(index);
			}
			this.ExecuteClosePopup();
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x00050E54 File Offset: 0x0004F054
		public void ExecuteClosePopup()
		{
			this.IsVisible = false;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00050E5D File Offset: 0x0004F05D
		public void ExecuteOpenPopup()
		{
			this.IsVisible = true;
			this.RefreshList();
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00050E6C File Offset: 0x0004F06C
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x00050E74 File Offset: 0x0004F074
		[DataSourceProperty]
		public string PopupHeader
		{
			get
			{
				return this._popupHeader;
			}
			set
			{
				if (value != this._popupHeader)
				{
					this._popupHeader = value;
					base.OnPropertyChangedWithValue<string>(value, "PopupHeader");
				}
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00050E97 File Offset: 0x0004F097
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x00050E9F File Offset: 0x0004F09F
		[DataSourceProperty]
		public bool IsVisible
		{
			get
			{
				return this._isVisible;
			}
			set
			{
				if (value != this._isVisible)
				{
					this._isVisible = value;
					base.OnPropertyChangedWithValue(value, "IsVisible");
					Game game = Game.Current;
					if (game == null)
					{
						return;
					}
					game.EventManager.TriggerEvent<CraftingWeaponClassSelectionOpenedEvent>(new CraftingWeaponClassSelectionOpenedEvent(this._isVisible));
				}
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00050EDC File Offset: 0x0004F0DC
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x00050EE4 File Offset: 0x0004F0E4
		[DataSourceProperty]
		public MBBindingList<WeaponClassVM> WeaponClasses
		{
			get
			{
				return this._weaponClasses;
			}
			set
			{
				if (value != this._weaponClasses)
				{
					this._weaponClasses = value;
					base.OnPropertyChangedWithValue<MBBindingList<WeaponClassVM>>(value, "WeaponClasses");
				}
			}
		}

		// Token: 0x04000A02 RID: 2562
		private readonly ICraftingCampaignBehavior _craftingBehavior;

		// Token: 0x04000A03 RID: 2563
		private readonly Action<int> _onSelect;

		// Token: 0x04000A04 RID: 2564
		private readonly List<CraftingTemplate> _templatesList;

		// Token: 0x04000A05 RID: 2565
		private readonly Func<CraftingTemplate, int> _getUnlockedPiecesCount;

		// Token: 0x04000A06 RID: 2566
		private string _popupHeader;

		// Token: 0x04000A07 RID: 2567
		private bool _isVisible;

		// Token: 0x04000A08 RID: 2568
		private MBBindingList<WeaponClassVM> _weaponClasses;
	}
}
