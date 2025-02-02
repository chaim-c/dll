using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E2 RID: 226
	public class OrderOfBattleFormationFilterVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public OrderOfBattleFormationFilterVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000205C4 File Offset: 0x0001E7C4
		private void SetBaseBrush()
		{
			switch (this.FormationFilter)
			{
			case 0:
				base.Brush = this.UnsetBrush;
				break;
			case 1:
				base.Brush = this.ShieldBrush;
				break;
			case 2:
				base.Brush = this.SpearBrush;
				break;
			case 3:
				base.Brush = this.ThrownBrush;
				break;
			case 4:
				base.Brush = this.HeavyBrush;
				break;
			case 5:
				base.Brush = this.HighTierBrush;
				break;
			case 6:
				base.Brush = this.LowTierBrush;
				break;
			default:
				base.Brush = this.UnsetBrush;
				break;
			}
			this._hasBaseBrushSet = true;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x00020671 File Offset: 0x0001E871
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x00020679 File Offset: 0x0001E879
		[Editor(false)]
		public int FormationFilter
		{
			get
			{
				return this._formationFilter;
			}
			set
			{
				if (value != this._formationFilter || !this._hasBaseBrushSet)
				{
					this._formationFilter = value;
					base.OnPropertyChanged(value, "FormationFilter");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000206A5 File Offset: 0x0001E8A5
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x000206AD File Offset: 0x0001E8AD
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x000206D1 File Offset: 0x0001E8D1
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x000206D9 File Offset: 0x0001E8D9
		[Editor(false)]
		public Brush SpearBrush
		{
			get
			{
				return this._spearBrush;
			}
			set
			{
				if (value != this._spearBrush)
				{
					this._spearBrush = value;
					base.OnPropertyChanged<Brush>(value, "SpearBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x000206FD File Offset: 0x0001E8FD
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x00020705 File Offset: 0x0001E905
		[Editor(false)]
		public Brush ShieldBrush
		{
			get
			{
				return this._shieldBrush;
			}
			set
			{
				if (value != this._shieldBrush)
				{
					this._shieldBrush = value;
					base.OnPropertyChanged<Brush>(value, "ShieldBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00020729 File Offset: 0x0001E929
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x00020731 File Offset: 0x0001E931
		[Editor(false)]
		public Brush ThrownBrush
		{
			get
			{
				return this._thrownBrush;
			}
			set
			{
				if (value != this._thrownBrush)
				{
					this._thrownBrush = value;
					base.OnPropertyChanged<Brush>(value, "ThrownBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00020755 File Offset: 0x0001E955
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0002075D File Offset: 0x0001E95D
		[Editor(false)]
		public Brush HeavyBrush
		{
			get
			{
				return this._heavyBrush;
			}
			set
			{
				if (value != this._heavyBrush)
				{
					this._heavyBrush = value;
					base.OnPropertyChanged<Brush>(value, "HeavyBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x00020781 File Offset: 0x0001E981
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x00020789 File Offset: 0x0001E989
		[Editor(false)]
		public Brush HighTierBrush
		{
			get
			{
				return this._highTierBrush;
			}
			set
			{
				if (value != this._highTierBrush)
				{
					this._highTierBrush = value;
					base.OnPropertyChanged<Brush>(value, "HighTierBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000207AD File Offset: 0x0001E9AD
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000207B5 File Offset: 0x0001E9B5
		[Editor(false)]
		public Brush LowTierBrush
		{
			get
			{
				return this._lowTierBrush;
			}
			set
			{
				if (value != this._lowTierBrush)
				{
					this._lowTierBrush = value;
					base.OnPropertyChanged<Brush>(value, "LowTierBrush");
					this.SetBaseBrush();
				}
			}
		}

		// Token: 0x04000552 RID: 1362
		private bool _hasBaseBrushSet;

		// Token: 0x04000553 RID: 1363
		private int _formationFilter;

		// Token: 0x04000554 RID: 1364
		private Brush _unsetBrush;

		// Token: 0x04000555 RID: 1365
		private Brush _spearBrush;

		// Token: 0x04000556 RID: 1366
		private Brush _shieldBrush;

		// Token: 0x04000557 RID: 1367
		private Brush _thrownBrush;

		// Token: 0x04000558 RID: 1368
		private Brush _heavyBrush;

		// Token: 0x04000559 RID: 1369
		private Brush _highTierBrush;

		// Token: 0x0400055A RID: 1370
		private Brush _lowTierBrush;
	}
}
