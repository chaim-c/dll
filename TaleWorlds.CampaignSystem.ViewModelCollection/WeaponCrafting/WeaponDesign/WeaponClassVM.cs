using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting.WeaponDesign
{
	// Token: 0x020000EB RID: 235
	public class WeaponClassVM : ViewModel
	{
		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00050F42 File Offset: 0x0004F142
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x00050F4A File Offset: 0x0004F14A
		public int NewlyUnlockedPieceCount { get; set; }

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00050F53 File Offset: 0x0004F153
		public CraftingTemplate Template { get; }

		// Token: 0x0600159C RID: 5532 RVA: 0x00050F5C File Offset: 0x0004F15C
		public WeaponClassVM(int selectionIndex, CraftingTemplate template, Action<int> onSelect)
		{
			this._onSelect = onSelect;
			this.SelectionIndex = selectionIndex;
			this.Template = template;
			this._selectedPieces = new Dictionary<CraftingPiece.PieceTypes, string>
			{
				{
					CraftingPiece.PieceTypes.Blade,
					null
				},
				{
					CraftingPiece.PieceTypes.Guard,
					null
				},
				{
					CraftingPiece.PieceTypes.Handle,
					null
				},
				{
					CraftingPiece.PieceTypes.Pommel,
					null
				}
			};
			this.RefreshValues();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00050FB8 File Offset: 0x0004F1B8
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.TemplateName = this.Template.TemplateName.ToString();
			this.UnlockedPiecesLabelText = new TextObject("{=OGbskMfz}Unlocked Parts:", null).ToString();
			this.WeaponType = this.Template.StringId;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00051008 File Offset: 0x0004F208
		public void RegisterSelectedPiece(CraftingPiece.PieceTypes type, string pieceID)
		{
			string a;
			if (this._selectedPieces.TryGetValue(type, out a) && a != pieceID)
			{
				this._selectedPieces[type] = pieceID;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0005103C File Offset: 0x0004F23C
		public string GetSelectedPieceData(CraftingPiece.PieceTypes type)
		{
			string result;
			if (this._selectedPieces.TryGetValue(type, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0005105C File Offset: 0x0004F25C
		public void ExecuteSelect()
		{
			Action<int> onSelect = this._onSelect;
			if (onSelect == null)
			{
				return;
			}
			onSelect(this.SelectionIndex);
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00051074 File Offset: 0x0004F274
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0005107C File Offset: 0x0004F27C
		[DataSourceProperty]
		public bool HasNewlyUnlockedPieces
		{
			get
			{
				return this._hasNewlyUnlockedPieces;
			}
			set
			{
				if (value != this._hasNewlyUnlockedPieces)
				{
					this._hasNewlyUnlockedPieces = value;
					base.OnPropertyChangedWithValue(value, "HasNewlyUnlockedPieces");
				}
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0005109A File Offset: 0x0004F29A
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x000510A2 File Offset: 0x0004F2A2
		[DataSourceProperty]
		public string UnlockedPiecesLabelText
		{
			get
			{
				return this._unlockedPiecesLabelText;
			}
			set
			{
				if (value != this._unlockedPiecesLabelText)
				{
					this._unlockedPiecesLabelText = value;
					base.OnPropertyChangedWithValue<string>(value, "UnlockedPiecesLabelText");
				}
			}
		}

		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000510C5 File Offset: 0x0004F2C5
		// (set) Token: 0x060015A6 RID: 5542 RVA: 0x000510CD File Offset: 0x0004F2CD
		[DataSourceProperty]
		public int UnlockedPiecesCount
		{
			get
			{
				return this._unlockedPiecesCount;
			}
			set
			{
				if (value != this._unlockedPiecesCount)
				{
					this._unlockedPiecesCount = value;
					base.OnPropertyChangedWithValue(value, "UnlockedPiecesCount");
				}
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000510EB File Offset: 0x0004F2EB
		// (set) Token: 0x060015A8 RID: 5544 RVA: 0x000510F3 File Offset: 0x0004F2F3
		[DataSourceProperty]
		public string TemplateName
		{
			get
			{
				return this._templateName;
			}
			set
			{
				if (value != this._templateName)
				{
					this._templateName = value;
					base.OnPropertyChangedWithValue<string>(value, "TemplateName");
				}
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00051116 File Offset: 0x0004F316
		// (set) Token: 0x060015AA RID: 5546 RVA: 0x0005111E File Offset: 0x0004F31E
		[DataSourceProperty]
		public bool IsSelected
		{
			get
			{
				return this._isSelected;
			}
			set
			{
				if (value != this._isSelected)
				{
					this._isSelected = value;
					base.OnPropertyChangedWithValue(value, "IsSelected");
				}
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x0005113C File Offset: 0x0004F33C
		// (set) Token: 0x060015AC RID: 5548 RVA: 0x00051144 File Offset: 0x0004F344
		[DataSourceProperty]
		public int SelectionIndex
		{
			get
			{
				return this._selectionIndex;
			}
			set
			{
				if (value != this._selectionIndex)
				{
					this._selectionIndex = value;
					base.OnPropertyChangedWithValue(value, "SelectionIndex");
				}
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00051162 File Offset: 0x0004F362
		// (set) Token: 0x060015AE RID: 5550 RVA: 0x0005116A File Offset: 0x0004F36A
		[DataSourceProperty]
		public string WeaponType
		{
			get
			{
				return this._weaponType;
			}
			set
			{
				if (value != this._weaponType)
				{
					this._weaponType = value;
					base.OnPropertyChangedWithValue<string>(value, "WeaponType");
				}
			}
		}

		// Token: 0x04000A0D RID: 2573
		private Action<int> _onSelect;

		// Token: 0x04000A0E RID: 2574
		private Dictionary<CraftingPiece.PieceTypes, string> _selectedPieces;

		// Token: 0x04000A0F RID: 2575
		private bool _hasNewlyUnlockedPieces;

		// Token: 0x04000A10 RID: 2576
		private string _unlockedPiecesLabelText;

		// Token: 0x04000A11 RID: 2577
		private int _unlockedPiecesCount;

		// Token: 0x04000A12 RID: 2578
		private string _templateName;

		// Token: 0x04000A13 RID: 2579
		private bool _isSelected;

		// Token: 0x04000A14 RID: 2580
		private int _selectionIndex;

		// Token: 0x04000A15 RID: 2581
		private string _weaponType;
	}
}
