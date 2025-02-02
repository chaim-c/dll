using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015A RID: 346
	public class CraftingPieceItemButtonWidget : ButtonWidget
	{
		// Token: 0x0600123A RID: 4666 RVA: 0x0003236C File Offset: 0x0003056C
		public CraftingPieceItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00032375 File Offset: 0x00030575
		private void UpdateSelfBrush()
		{
			if (this.DontHavePieceBrush == null || this.HasPieceBrush == null)
			{
				return;
			}
			base.Brush = (this.PlayerHasPiece ? this.HasPieceBrush : this.DontHavePieceBrush);
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000323A4 File Offset: 0x000305A4
		private void UpdateMaterialBrush()
		{
			if (this.DontHavePieceMaterialBrush == null || this.HasPieceMaterialBrush == null || this.ImageIdentifier == null)
			{
				return;
			}
			this.ImageIdentifier.Brush = (this.PlayerHasPiece ? this.HasPieceMaterialBrush : this.DontHavePieceMaterialBrush);
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000323E0 File Offset: 0x000305E0
		// (set) Token: 0x0600123E RID: 4670 RVA: 0x000323E8 File Offset: 0x000305E8
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

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00032400 File Offset: 0x00030600
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x00032408 File Offset: 0x00030608
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

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00032426 File Offset: 0x00030626
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0003242E File Offset: 0x0003062E
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

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00032446 File Offset: 0x00030646
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x0003244E File Offset: 0x0003064E
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

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00032466 File Offset: 0x00030666
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0003246E File Offset: 0x0003066E
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

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00032486 File Offset: 0x00030686
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0003248E File Offset: 0x0003068E
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

		// Token: 0x04000852 RID: 2130
		private ImageIdentifierWidget _imageIdentifier;

		// Token: 0x04000853 RID: 2131
		private bool _playerHasPiece;

		// Token: 0x04000854 RID: 2132
		private Brush _hasPieceBrush;

		// Token: 0x04000855 RID: 2133
		private Brush _dontHavePieceBrush;

		// Token: 0x04000856 RID: 2134
		private Brush _hasPieceMaterialBrush;

		// Token: 0x04000857 RID: 2135
		private Brush _dontHavePieceMaterialBrush;
	}
}
