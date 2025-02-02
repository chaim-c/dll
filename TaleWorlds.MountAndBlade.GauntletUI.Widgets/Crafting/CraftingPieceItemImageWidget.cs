using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015B RID: 347
	public class CraftingPieceItemImageWidget : ImageWidget
	{
		// Token: 0x06001249 RID: 4681 RVA: 0x000324A6 File Offset: 0x000306A6
		public CraftingPieceItemImageWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000324AF File Offset: 0x000306AF
		private void UpdateSelfBrush()
		{
			if (this.DontHavePieceBrush == null || this.HasPieceBrush == null)
			{
				return;
			}
			base.Brush = (this.PlayerHasPiece ? this.HasPieceBrush : this.DontHavePieceBrush);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000324DE File Offset: 0x000306DE
		private void UpdateMaterialBrush()
		{
			if (this.DontHavePieceMaterialBrush == null || this.HasPieceMaterialBrush == null || this.ImageIdentifier == null)
			{
				return;
			}
			this.ImageIdentifier.Brush = (this.PlayerHasPiece ? this.HasPieceMaterialBrush : this.DontHavePieceMaterialBrush);
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x0003251A File Offset: 0x0003071A
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00032522 File Offset: 0x00030722
		public ImageIdentifierWidget ImageIdentifier
		{
			get
			{
				return this._imageIdentifier;
			}
			set
			{
				if (this._imageIdentifier != value)
				{
					this._imageIdentifier = value;
					this.UpdateMaterialBrush();
				}
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0003253A File Offset: 0x0003073A
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00032542 File Offset: 0x00030742
		public bool PlayerHasPiece
		{
			get
			{
				return this._playerHasPiece;
			}
			set
			{
				if (this._playerHasPiece != value)
				{
					this._playerHasPiece = value;
					this.UpdateSelfBrush();
					this.UpdateMaterialBrush();
				}
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00032560 File Offset: 0x00030760
		// (set) Token: 0x06001251 RID: 4689 RVA: 0x00032568 File Offset: 0x00030768
		public Brush HasPieceBrush
		{
			get
			{
				return this._hasPieceBrush;
			}
			set
			{
				if (this._hasPieceBrush != value)
				{
					this._hasPieceBrush = value;
					this.UpdateSelfBrush();
				}
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00032580 File Offset: 0x00030780
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00032588 File Offset: 0x00030788
		public Brush DontHavePieceBrush
		{
			get
			{
				return this._dontHavePieceBrush;
			}
			set
			{
				if (this._dontHavePieceBrush != value)
				{
					this._dontHavePieceBrush = value;
					this.UpdateSelfBrush();
				}
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x000325A0 File Offset: 0x000307A0
		// (set) Token: 0x06001255 RID: 4693 RVA: 0x000325A8 File Offset: 0x000307A8
		public Brush HasPieceMaterialBrush
		{
			get
			{
				return this._hasPieceMaterialBrush;
			}
			set
			{
				if (this._hasPieceMaterialBrush != value)
				{
					this._hasPieceMaterialBrush = value;
					this.UpdateMaterialBrush();
				}
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x000325C0 File Offset: 0x000307C0
		// (set) Token: 0x06001257 RID: 4695 RVA: 0x000325C8 File Offset: 0x000307C8
		public Brush DontHavePieceMaterialBrush
		{
			get
			{
				return this._dontHavePieceMaterialBrush;
			}
			set
			{
				if (this._dontHavePieceMaterialBrush != value)
				{
					this._dontHavePieceMaterialBrush = value;
					this.UpdateMaterialBrush();
				}
			}
		}

		// Token: 0x04000858 RID: 2136
		private ImageIdentifierWidget _imageIdentifier;

		// Token: 0x04000859 RID: 2137
		private bool _playerHasPiece;

		// Token: 0x0400085A RID: 2138
		private Brush _hasPieceBrush;

		// Token: 0x0400085B RID: 2139
		private Brush _dontHavePieceBrush;

		// Token: 0x0400085C RID: 2140
		private Brush _hasPieceMaterialBrush;

		// Token: 0x0400085D RID: 2141
		private Brush _dontHavePieceMaterialBrush;
	}
}
