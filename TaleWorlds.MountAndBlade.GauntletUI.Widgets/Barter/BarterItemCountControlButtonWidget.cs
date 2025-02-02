using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Barter
{
	// Token: 0x02000182 RID: 386
	public class BarterItemCountControlButtonWidget : ButtonWidget
	{
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x00036500 File Offset: 0x00034700
		// (set) Token: 0x060013DD RID: 5085 RVA: 0x00036508 File Offset: 0x00034708
		public float IncreaseToHoldDelay { get; set; } = 1f;

		// Token: 0x060013DE RID: 5086 RVA: 0x00036511 File Offset: 0x00034711
		public BarterItemCountControlButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00036528 File Offset: 0x00034728
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this._totalTime += dt;
			if (base.IsPressed && this._clickStartTime + this.IncreaseToHoldDelay < this._totalTime)
			{
				base.EventFired("MoveOne", Array.Empty<object>());
			}
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00036577 File Offset: 0x00034777
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			this._clickStartTime = this._totalTime;
			base.EventFired("MoveOne", Array.Empty<object>());
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003659B File Offset: 0x0003479B
		protected override void OnMouseReleased()
		{
			base.OnMouseReleased();
			this._clickStartTime = 0f;
		}

		// Token: 0x0400090D RID: 2317
		private float _clickStartTime;

		// Token: 0x0400090E RID: 2318
		private float _totalTime;
	}
}
