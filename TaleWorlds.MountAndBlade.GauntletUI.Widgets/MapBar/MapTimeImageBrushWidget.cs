using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.MapBar
{
	// Token: 0x02000108 RID: 264
	public class MapTimeImageBrushWidget : BrushWidget
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x000269F7 File Offset: 0x00024BF7
		public MapTimeImageBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00026A00 File Offset: 0x00024C00
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			StyleLayer layer = base.Brush.DefaultStyle.GetLayer("Default");
			StyleLayer layer2 = base.Brush.DefaultStyle.GetLayer("Part2");
			if (!this._initialized)
			{
				this._offset = layer2.XOffset;
				this._initialized = true;
			}
			float overridenWidth = layer.OverridenWidth;
			float num = -overridenWidth * ((float)this.DayTime / 24f) + this._offset;
			float xoffset;
			if (this.DayTime > 12.0)
			{
				xoffset = num + overridenWidth;
			}
			else
			{
				xoffset = num - overridenWidth;
			}
			layer.XOffset = num;
			layer2.XOffset = xoffset;
			base.OnRender(twoDimensionContext, drawContext);
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00026AA8 File Offset: 0x00024CA8
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00026AB0 File Offset: 0x00024CB0
		[Editor(false)]
		public double DayTime
		{
			get
			{
				return this._dayTime;
			}
			set
			{
				if (this._dayTime != value)
				{
					this._dayTime = value;
					base.OnPropertyChanged(value, "DayTime");
				}
			}
		}

		// Token: 0x0400066C RID: 1644
		private float _offset;

		// Token: 0x0400066D RID: 1645
		private bool _initialized;

		// Token: 0x0400066E RID: 1646
		private double _dayTime;
	}
}
