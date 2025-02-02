using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003E RID: 62
	public class SortButtonWidget : ButtonWidget
	{
		// Token: 0x06000365 RID: 869 RVA: 0x0000AD6A File Offset: 0x00008F6A
		public SortButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000AD74 File Offset: 0x00008F74
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.SortVisualWidget != null)
			{
				if (base.IsSelected)
				{
					switch (this.SortState)
					{
					case 0:
						this.SortVisualWidget.SetState("Default");
						return;
					case 1:
						this.SortVisualWidget.SetState("Ascending");
						return;
					case 2:
						this.SortVisualWidget.SetState("Descending");
						return;
					default:
						return;
					}
				}
				else
				{
					this.SortVisualWidget.SetState("Default");
				}
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000ADF5 File Offset: 0x00008FF5
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000ADFD File Offset: 0x00008FFD
		[Editor(false)]
		public int SortState
		{
			get
			{
				return this._sortState;
			}
			set
			{
				if (this._sortState != value)
				{
					this._sortState = value;
					base.OnPropertyChanged(value, "SortState");
				}
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000AE1B File Offset: 0x0000901B
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000AE23 File Offset: 0x00009023
		[Editor(false)]
		public BrushWidget SortVisualWidget
		{
			get
			{
				return this._sortVisualWidget;
			}
			set
			{
				if (this._sortVisualWidget != value)
				{
					this._sortVisualWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "SortVisualWidget");
				}
			}
		}

		// Token: 0x0400016B RID: 363
		private int _sortState;

		// Token: 0x0400016C RID: 364
		private BrushWidget _sortVisualWidget;
	}
}
