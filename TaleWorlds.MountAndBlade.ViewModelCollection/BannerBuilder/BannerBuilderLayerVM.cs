using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.BannerBuilder
{
	// Token: 0x02000078 RID: 120
	public class BannerBuilderLayerVM : ViewModel
	{
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00025C51 File Offset: 0x00023E51
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00025C59 File Offset: 0x00023E59
		public BannerData Data { get; private set; }

		// Token: 0x060009B5 RID: 2485 RVA: 0x00025C64 File Offset: 0x00023E64
		public BannerBuilderLayerVM(BannerData data, int layerIndex)
		{
			this.Data = data;
			this.LayerIndex = layerIndex;
			this._rotationValue = this.Data.Rotation;
			this._positionValue = this.Data.Position;
			this._sizeValue = this.Data.Size;
			this._isDrawStrokeActive = this.Data.DrawStroke;
			this._isMirrorActive = this.Data.Mirror;
			this.Refresh();
			this.IsLayerPattern = (layerIndex == 0);
			this.CanDeleteLayer = !this.IsLayerPattern;
			this.TotalAreaSize = 1528;
			this.EditableAreaSize = 512;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00025D10 File Offset: 0x00023F10
		public void Refresh()
		{
			this.IconID = this.Data.MeshId;
			this.IconIDAsString = this.IconID.ToString();
			uint color = BannerManager.Instance.ReadOnlyColorPalette[this.Data.ColorId].Color;
			this.Color1 = Color.FromUint(color);
			uint color2 = BannerManager.Instance.ReadOnlyColorPalette[this.Data.ColorId2].Color;
			this.Color2 = Color.FromUint(color2);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00025DA0 File Offset: 0x00023FA0
		public void ExecuteDelete()
		{
			Action<BannerBuilderLayerVM> onDeletion = BannerBuilderLayerVM._onDeletion;
			if (onDeletion == null)
			{
				return;
			}
			onDeletion(this);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00025DB2 File Offset: 0x00023FB2
		public void ExecuteSelection()
		{
			Action<BannerBuilderLayerVM> onSelection = BannerBuilderLayerVM._onSelection;
			if (onSelection == null)
			{
				return;
			}
			onSelection(this);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00025DC4 File Offset: 0x00023FC4
		public void SetLayerIndex(int newIndex)
		{
			this.LayerIndex = newIndex;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00025DCD File Offset: 0x00023FCD
		public void ExecuteSelectColor1()
		{
			Action<int, Action<BannerBuilderColorItemVM>> onColorSelection = BannerBuilderLayerVM._onColorSelection;
			if (onColorSelection == null)
			{
				return;
			}
			onColorSelection(this.Data.ColorId, new Action<BannerBuilderColorItemVM>(this.OnSelectColor1));
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00025DF8 File Offset: 0x00023FF8
		private void OnSelectColor1(BannerBuilderColorItemVM selectedColor)
		{
			this.Data.ColorId = selectedColor.ColorID;
			this.Color1 = Color.FromUint(selectedColor.BannerColor.Color);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00025E35 File Offset: 0x00024035
		public void ExecuteSelectColor2()
		{
			Action<int, Action<BannerBuilderColorItemVM>> onColorSelection = BannerBuilderLayerVM._onColorSelection;
			if (onColorSelection == null)
			{
				return;
			}
			onColorSelection(this.Data.ColorId2, new Action<BannerBuilderColorItemVM>(this.OnSelectColor2));
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00025E60 File Offset: 0x00024060
		private void OnSelectColor2(BannerBuilderColorItemVM selectedColor)
		{
			this.Data.ColorId2 = selectedColor.ColorID;
			this.Color2 = Color.FromUint(selectedColor.BannerColor.Color);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00025EA0 File Offset: 0x000240A0
		public void ExecuteSwapColors()
		{
			int colorId = this.Data.ColorId2;
			this.Data.ColorId2 = this.Data.ColorId;
			this.Data.ColorId = colorId;
			Color color = this.Color2;
			Color color2 = this.Color1;
			this.Color1 = color;
			this.Color2 = color2;
			this.Refresh();
			this.ExecuteUpdateBanner();
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00025F07 File Offset: 0x00024107
		public void ExecuteCenterSigil()
		{
			this.PositionValue = new Vec2((float)this.TotalAreaSize / 2f, (float)this.TotalAreaSize / 2f);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00025F34 File Offset: 0x00024134
		public void ExecuteResetSize()
		{
			float num = (float)(this.IsLayerPattern ? this.TotalAreaSize : 483);
			this.SizeValue = new Vec2(num, num);
			this.ExecuteUpdateBanner();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00025F6B File Offset: 0x0002416B
		public void ExecuteUpdateBanner()
		{
			Action refresh = BannerBuilderLayerVM._refresh;
			if (refresh == null)
			{
				return;
			}
			refresh();
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00025F7C File Offset: 0x0002417C
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x00025F84 File Offset: 0x00024184
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

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00025FA2 File Offset: 0x000241A2
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x00025FAA File Offset: 0x000241AA
		[DataSourceProperty]
		public bool CanDeleteLayer
		{
			get
			{
				return this._canDeleteLayer;
			}
			set
			{
				if (value != this._canDeleteLayer)
				{
					this._canDeleteLayer = value;
					base.OnPropertyChangedWithValue(value, "CanDeleteLayer");
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00025FC8 File Offset: 0x000241C8
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x00025FD0 File Offset: 0x000241D0
		[DataSourceProperty]
		public bool IsLayerPattern
		{
			get
			{
				return this._isLayerPattern;
			}
			set
			{
				if (value != this._isLayerPattern)
				{
					this._isLayerPattern = value;
					base.OnPropertyChangedWithValue(value, "IsLayerPattern");
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x00025FEE File Offset: 0x000241EE
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x00025FF6 File Offset: 0x000241F6
		[DataSourceProperty]
		public bool IsDrawStrokeActive
		{
			get
			{
				return this._isDrawStrokeActive;
			}
			set
			{
				if (value != this._isDrawStrokeActive)
				{
					this._isDrawStrokeActive = value;
					base.OnPropertyChangedWithValue(value, "IsDrawStrokeActive");
					this.Data.DrawStroke = value;
					this.ExecuteUpdateBanner();
				}
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00026026 File Offset: 0x00024226
		// (set) Token: 0x060009CB RID: 2507 RVA: 0x0002602E File Offset: 0x0002422E
		[DataSourceProperty]
		public bool IsMirrorActive
		{
			get
			{
				return this._isMirrorActive;
			}
			set
			{
				if (value != this._isMirrorActive)
				{
					this._isMirrorActive = value;
					base.OnPropertyChangedWithValue(value, "IsMirrorActive");
					this.Data.Mirror = value;
					this.ExecuteUpdateBanner();
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002605E File Offset: 0x0002425E
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x00026066 File Offset: 0x00024266
		[DataSourceProperty]
		public float RotationValue
		{
			get
			{
				return this._rotationValue;
			}
			set
			{
				if (value != this._rotationValue)
				{
					this._rotationValue = value;
					this.Data.RotationValue = value;
					base.OnPropertyChangedWithValue(value, "RotationValue");
					base.OnPropertyChanged("RotationValue360");
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002609B File Offset: 0x0002429B
		// (set) Token: 0x060009CF RID: 2511 RVA: 0x000260AA File Offset: 0x000242AA
		[DataSourceProperty]
		public int RotationValue360
		{
			get
			{
				return (int)(this._rotationValue * 360f);
			}
			set
			{
				if (value != (int)(this._rotationValue * 360f))
				{
					this.RotationValue = (float)value / 360f;
					base.OnPropertyChangedWithValue(value, "RotationValue360");
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000260D6 File Offset: 0x000242D6
		// (set) Token: 0x060009D1 RID: 2513 RVA: 0x000260DE File Offset: 0x000242DE
		[DataSourceProperty]
		public int IconID
		{
			get
			{
				return this._iconID;
			}
			set
			{
				if (value != this._iconID)
				{
					this._iconID = value;
					base.OnPropertyChangedWithValue(value, "IconID");
				}
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000260FC File Offset: 0x000242FC
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x00026104 File Offset: 0x00024304
		[DataSourceProperty]
		public int LayerIndex
		{
			get
			{
				return this._layerIndex;
			}
			set
			{
				if (value != this._layerIndex)
				{
					this._layerIndex = value;
					base.OnPropertyChangedWithValue(value, "LayerIndex");
				}
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00026122 File Offset: 0x00024322
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0002612A File Offset: 0x0002432A
		[DataSourceProperty]
		public int EditableAreaSize
		{
			get
			{
				return this._editableAreaSize;
			}
			set
			{
				if (value != this._editableAreaSize)
				{
					this._editableAreaSize = value;
					base.OnPropertyChangedWithValue(value, "EditableAreaSize");
				}
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00026148 File Offset: 0x00024348
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x00026150 File Offset: 0x00024350
		[DataSourceProperty]
		public int TotalAreaSize
		{
			get
			{
				return this._totalAreaSize;
			}
			set
			{
				if (value != this._totalAreaSize)
				{
					this._totalAreaSize = value;
					base.OnPropertyChangedWithValue(value, "TotalAreaSize");
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0002616E File Offset: 0x0002436E
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x00026176 File Offset: 0x00024376
		[DataSourceProperty]
		public string IconIDAsString
		{
			get
			{
				return this._iconIDAsString;
			}
			set
			{
				if (value != this._iconIDAsString)
				{
					this._iconIDAsString = value;
					base.OnPropertyChangedWithValue<string>(value, "IconIDAsString");
				}
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00026199 File Offset: 0x00024399
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x000261A1 File Offset: 0x000243A1
		[DataSourceProperty]
		public Color Color1
		{
			get
			{
				return this._color1;
			}
			set
			{
				if (value != this._color1)
				{
					this._color1 = value;
					base.OnPropertyChangedWithValue(value, "Color1");
					this.Color1AsStr = value.ToString();
				}
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000261D7 File Offset: 0x000243D7
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000261DF File Offset: 0x000243DF
		[DataSourceProperty]
		public Color Color2
		{
			get
			{
				return this._color2;
			}
			set
			{
				if (value != this._color2)
				{
					this._color2 = value;
					base.OnPropertyChangedWithValue(value, "Color2");
					this.Color2AsStr = value.ToString();
				}
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00026215 File Offset: 0x00024415
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0002621D File Offset: 0x0002441D
		[DataSourceProperty]
		public string Color1AsStr
		{
			get
			{
				return this._color1AsStr;
			}
			set
			{
				if (value != this._color1AsStr)
				{
					this._color1AsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "Color1AsStr");
				}
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00026240 File Offset: 0x00024440
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00026248 File Offset: 0x00024448
		[DataSourceProperty]
		public string Color2AsStr
		{
			get
			{
				return this._color2AsStr;
			}
			set
			{
				if (value != this._color2AsStr)
				{
					this._color2AsStr = value;
					base.OnPropertyChangedWithValue<string>(value, "Color2AsStr");
				}
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002626B File Offset: 0x0002446B
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00026274 File Offset: 0x00024474
		[DataSourceProperty]
		public Vec2 PositionValue
		{
			get
			{
				return this._positionValue;
			}
			set
			{
				if (this._positionValue != value)
				{
					this._positionValue = value;
					base.OnPropertyChangedWithValue(value, "PositionValue");
					base.OnPropertyChanged("PositionValueX");
					base.OnPropertyChanged("PositionValueY");
					this.Data.Position = value;
				}
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x000262C4 File Offset: 0x000244C4
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x000262D8 File Offset: 0x000244D8
		[DataSourceProperty]
		public float PositionValueX
		{
			get
			{
				return (float)Math.Round((double)this._positionValue.X);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._positionValue.X)
				{
					this.PositionValue = new Vec2(value, this._positionValue.Y);
					this.Data.Position = this._positionValue;
					base.OnPropertyChangedWithValue(value, "PositionValueX");
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00026331 File Offset: 0x00024531
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00026348 File Offset: 0x00024548
		[DataSourceProperty]
		public float PositionValueY
		{
			get
			{
				return (float)Math.Round((double)this._positionValue.Y);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._positionValue.Y)
				{
					this.PositionValue = new Vec2(this._positionValue.X, value);
					this.Data.Position = this._positionValue;
					base.OnPropertyChangedWithValue(value, "PositionValueY");
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000263A1 File Offset: 0x000245A1
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x000263AC File Offset: 0x000245AC
		[DataSourceProperty]
		public Vec2 SizeValue
		{
			get
			{
				return this._sizeValue;
			}
			set
			{
				if (this._sizeValue != value)
				{
					this._sizeValue = value;
					base.OnPropertyChangedWithValue(value, "SizeValue");
					base.OnPropertyChanged("SizeValueX");
					base.OnPropertyChanged("SizeValueY");
					this.Data.Size = value;
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x000263FC File Offset: 0x000245FC
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00026410 File Offset: 0x00024610
		[DataSourceProperty]
		public float SizeValueX
		{
			get
			{
				return (float)Math.Round((double)this._sizeValue.X);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._sizeValue.X)
				{
					this.SizeValue = new Vec2(value, this._sizeValue.Y);
					this.Data.Size = this._sizeValue;
					base.OnPropertyChangedWithValue(value, "SizeValueX");
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00026469 File Offset: 0x00024669
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x00026480 File Offset: 0x00024680
		[DataSourceProperty]
		public float SizeValueY
		{
			get
			{
				return (float)Math.Round((double)this._sizeValue.Y);
			}
			set
			{
				value = (float)Math.Round((double)value);
				if (value != this._sizeValue.Y)
				{
					this.SizeValue = new Vec2(this._sizeValue.X, value);
					this.Data.Size = this._sizeValue;
					base.OnPropertyChangedWithValue(value, "SizeValueY");
				}
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000264D9 File Offset: 0x000246D9
		public static void SetLayerActions(Action refresh, Action<BannerBuilderLayerVM> onSelection, Action<BannerBuilderLayerVM> onDeletion, Action<int, Action<BannerBuilderColorItemVM>> onColorSelection)
		{
			BannerBuilderLayerVM._onSelection = onSelection;
			BannerBuilderLayerVM._onDeletion = onDeletion;
			BannerBuilderLayerVM._onColorSelection = onColorSelection;
			BannerBuilderLayerVM._refresh = refresh;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000264F3 File Offset: 0x000246F3
		public static void ResetLayerActions()
		{
			BannerBuilderLayerVM._onSelection = null;
			BannerBuilderLayerVM._onDeletion = null;
			BannerBuilderLayerVM._onColorSelection = null;
			BannerBuilderLayerVM._refresh = null;
		}

		// Token: 0x040004A1 RID: 1185
		private static Action<BannerBuilderLayerVM> _onSelection;

		// Token: 0x040004A2 RID: 1186
		private static Action<BannerBuilderLayerVM> _onDeletion;

		// Token: 0x040004A3 RID: 1187
		private static Action<int, Action<BannerBuilderColorItemVM>> _onColorSelection;

		// Token: 0x040004A4 RID: 1188
		private static Action _refresh;

		// Token: 0x040004A5 RID: 1189
		private int _iconID;

		// Token: 0x040004A6 RID: 1190
		private string _iconIDAsString;

		// Token: 0x040004A7 RID: 1191
		private Color _color1;

		// Token: 0x040004A8 RID: 1192
		private Color _color2;

		// Token: 0x040004A9 RID: 1193
		private string _color1AsStr;

		// Token: 0x040004AA RID: 1194
		private string _color2AsStr;

		// Token: 0x040004AB RID: 1195
		private bool _isSelected;

		// Token: 0x040004AC RID: 1196
		private bool _canDeleteLayer;

		// Token: 0x040004AD RID: 1197
		private bool _isLayerPattern;

		// Token: 0x040004AE RID: 1198
		private bool _isDrawStrokeActive;

		// Token: 0x040004AF RID: 1199
		private bool _isMirrorActive;

		// Token: 0x040004B0 RID: 1200
		private int _editableAreaSize;

		// Token: 0x040004B1 RID: 1201
		private int _totalAreaSize;

		// Token: 0x040004B2 RID: 1202
		private int _layerIndex;

		// Token: 0x040004B3 RID: 1203
		private float _rotationValue;

		// Token: 0x040004B4 RID: 1204
		private Vec2 _positionValue;

		// Token: 0x040004B5 RID: 1205
		private Vec2 _sizeValue;
	}
}
