using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200001F RID: 31
	public class GamepadCursorMarkerWidget : BrushWidget
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000663E File Offset: 0x0000483E
		public GamepadCursorMarkerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00006647 File Offset: 0x00004847
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0000664F File Offset: 0x0000484F
		public bool FlipVisual
		{
			get
			{
				return this._flipVisual;
			}
			set
			{
				if (value != this._flipVisual)
				{
					this._flipVisual = value;
					base.Brush.DefaultLayer.HorizontalFlip = value;
				}
			}
		}

		// Token: 0x040000BB RID: 187
		private bool _flipVisual;
	}
}
