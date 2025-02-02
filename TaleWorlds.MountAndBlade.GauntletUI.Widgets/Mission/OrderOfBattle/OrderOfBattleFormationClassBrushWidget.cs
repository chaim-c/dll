using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000DE RID: 222
	public class OrderOfBattleFormationClassBrushWidget : BrushWidget
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x0001FFA5 File Offset: 0x0001E1A5
		public OrderOfBattleFormationClassBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
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

		// Token: 0x06000B7B RID: 2939 RVA: 0x00020063 File Offset: 0x0001E263
		private void SetColor()
		{
			if (this.IsErrored)
			{
				base.Brush.Color = this.ErroredColor;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0002007E File Offset: 0x0001E27E
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x00020086 File Offset: 0x0001E286
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

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x000200B2 File Offset: 0x0001E2B2
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x000200BA File Offset: 0x0001E2BA
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

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x000200DD File Offset: 0x0001E2DD
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x000200E5 File Offset: 0x0001E2E5
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

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00020109 File Offset: 0x0001E309
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x00020111 File Offset: 0x0001E311
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

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00020135 File Offset: 0x0001E335
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0002013D File Offset: 0x0001E33D
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

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x00020161 File Offset: 0x0001E361
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x00020169 File Offset: 0x0001E369
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

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0002018D File Offset: 0x0001E38D
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00020195 File Offset: 0x0001E395
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

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x000201B9 File Offset: 0x0001E3B9
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x000201C1 File Offset: 0x0001E3C1
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

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000201E5 File Offset: 0x0001E3E5
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x000201ED File Offset: 0x0001E3ED
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

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x00020211 File Offset: 0x0001E411
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x00020219 File Offset: 0x0001E419
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

		// Token: 0x04000537 RID: 1335
		private bool _hasBaseBrushSet;

		// Token: 0x04000538 RID: 1336
		private int _formationClass;

		// Token: 0x04000539 RID: 1337
		private Color _erroredColor;

		// Token: 0x0400053A RID: 1338
		private bool _isErrored;

		// Token: 0x0400053B RID: 1339
		private Brush _unsetBrush;

		// Token: 0x0400053C RID: 1340
		private Brush _infantryBrush;

		// Token: 0x0400053D RID: 1341
		private Brush _rangedBrush;

		// Token: 0x0400053E RID: 1342
		private Brush _cavalryBrush;

		// Token: 0x0400053F RID: 1343
		private Brush _horseArcherBrush;

		// Token: 0x04000540 RID: 1344
		private Brush _infantryAndRangedBrush;

		// Token: 0x04000541 RID: 1345
		private Brush _cavalryAndHorseArcherBrush;
	}
}
