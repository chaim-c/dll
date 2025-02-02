using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000DF RID: 223
	internal class OrderOfBattleFormationClassDropdownListButtonWidget : ButtonWidget
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0002023D File Offset: 0x0001E43D
		public OrderOfBattleFormationClassDropdownListButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00020248 File Offset: 0x0001E448
		private void SetBaseBrush()
		{
			switch (this.FormationClass)
			{
			case 0:
				base.Brush = this.UnsetBrush;
				break;
			case 1:
				base.Brush = this.InfantryBrush;
				break;
			case 2:
				base.Brush = this.RangedBrush;
				break;
			case 3:
				base.Brush = this.CavalryBrush;
				break;
			case 4:
				base.Brush = this.HorseArcherBrush;
				break;
			case 5:
				base.Brush = this.InfantryAndRangedBrush;
				break;
			case 6:
				base.Brush = this.CavalryAndHorseArcherBrush;
				break;
			default:
				base.Brush = this.UnsetBrush;
				break;
			}
			this._hasBaseBrushSet = true;
			this.SetColor();
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000202FB File Offset: 0x0001E4FB
		private void SetColor()
		{
			if (this.IsErrored)
			{
				base.Brush.Color = this.ErroredColor;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00020316 File Offset: 0x0001E516
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0002031E File Offset: 0x0001E51E
		[Editor(false)]
		public int FormationClass
		{
			get
			{
				return this._formationClass;
			}
			set
			{
				if (value != this._formationClass || !this._hasBaseBrushSet)
				{
					this._formationClass = value;
					base.OnPropertyChanged(value, "FormationClass");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002034A File Offset: 0x0001E54A
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x00020352 File Offset: 0x0001E552
		[Editor(false)]
		public Color ErroredColor
		{
			get
			{
				return this._erroredColor;
			}
			set
			{
				if (value != this._erroredColor)
				{
					this._erroredColor = value;
					base.OnPropertyChanged(value, "ErroredColor");
				}
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00020375 File Offset: 0x0001E575
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0002037D File Offset: 0x0001E57D
		[Editor(false)]
		public bool IsErrored
		{
			get
			{
				return this._isErrored;
			}
			set
			{
				if (value != this._isErrored)
				{
					this._isErrored = value;
					base.OnPropertyChanged(value, "IsErrored");
					this.SetColor();
				}
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x000203A1 File Offset: 0x0001E5A1
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x000203A9 File Offset: 0x0001E5A9
		[Editor(false)]
		public Brush UnsetBrush
		{
			get
			{
				return this._unsetBrush;
			}
			set
			{
				if (value != this._unsetBrush)
				{
					this._unsetBrush = value;
					base.OnPropertyChanged<Brush>(value, "UnsetBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x000203CD File Offset: 0x0001E5CD
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x000203D5 File Offset: 0x0001E5D5
		[Editor(false)]
		public Brush InfantryBrush
		{
			get
			{
				return this._infantryBrush;
			}
			set
			{
				if (value != this._infantryBrush)
				{
					this._infantryBrush = value;
					base.OnPropertyChanged<Brush>(value, "InfantryBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000203F9 File Offset: 0x0001E5F9
		// (set) Token: 0x06000B9E RID: 2974 RVA: 0x00020401 File Offset: 0x0001E601
		[Editor(false)]
		public Brush RangedBrush
		{
			get
			{
				return this._rangedBrush;
			}
			set
			{
				if (value != this._rangedBrush)
				{
					this._rangedBrush = value;
					base.OnPropertyChanged<Brush>(value, "RangedBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x00020425 File Offset: 0x0001E625
		// (set) Token: 0x06000BA0 RID: 2976 RVA: 0x0002042D File Offset: 0x0001E62D
		[Editor(false)]
		public Brush CavalryBrush
		{
			get
			{
				return this._cavalryBrush;
			}
			set
			{
				if (value != this._cavalryBrush)
				{
					this._cavalryBrush = value;
					base.OnPropertyChanged<Brush>(value, "CavalryBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x00020451 File Offset: 0x0001E651
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x00020459 File Offset: 0x0001E659
		[Editor(false)]
		public Brush HorseArcherBrush
		{
			get
			{
				return this._horseArcherBrush;
			}
			set
			{
				if (value != this._horseArcherBrush)
				{
					this._horseArcherBrush = value;
					base.OnPropertyChanged<Brush>(value, "HorseArcherBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002047D File Offset: 0x0001E67D
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00020485 File Offset: 0x0001E685
		[Editor(false)]
		public Brush InfantryAndRangedBrush
		{
			get
			{
				return this._infantryAndRangedBrush;
			}
			set
			{
				if (value != this._infantryAndRangedBrush)
				{
					this._infantryAndRangedBrush = value;
					base.OnPropertyChanged<Brush>(value, "InfantryAndRangedBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000204A9 File Offset: 0x0001E6A9
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x000204B1 File Offset: 0x0001E6B1
		[Editor(false)]
		public Brush CavalryAndHorseArcherBrush
		{
			get
			{
				return this._cavalryAndHorseArcherBrush;
			}
			set
			{
				if (value != this._cavalryAndHorseArcherBrush)
				{
					this._cavalryAndHorseArcherBrush = value;
					base.OnPropertyChanged<Brush>(value, "CavalryAndHorseArcherBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x04000542 RID: 1346
		private bool _hasBaseBrushSet;

		// Token: 0x04000543 RID: 1347
		private int _formationClass;

		// Token: 0x04000544 RID: 1348
		private Color _erroredColor;

		// Token: 0x04000545 RID: 1349
		private bool _isErrored;

		// Token: 0x04000546 RID: 1350
		private Brush _unsetBrush;

		// Token: 0x04000547 RID: 1351
		private Brush _infantryBrush;

		// Token: 0x04000548 RID: 1352
		private Brush _rangedBrush;

		// Token: 0x04000549 RID: 1353
		private Brush _cavalryBrush;

		// Token: 0x0400054A RID: 1354
		private Brush _horseArcherBrush;

		// Token: 0x0400054B RID: 1355
		private Brush _infantryAndRangedBrush;

		// Token: 0x0400054C RID: 1356
		private Brush _cavalryAndHorseArcherBrush;
	}
}
