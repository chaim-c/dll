using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000FD RID: 253
	public class DevelopmentQueueVisualIconWidget : Widget
	{
		// Token: 0x06000D63 RID: 3427 RVA: 0x0002544B File Offset: 0x0002364B
		public DevelopmentQueueVisualIconWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002545C File Offset: 0x0002365C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._animState == DevelopmentQueueVisualIconWidget.AnimState.Start)
			{
				this._tickCount += 1f;
				if (this._tickCount > 20f)
				{
					this._animState = DevelopmentQueueVisualIconWidget.AnimState.Starting;
					return;
				}
			}
			else if (this._animState == DevelopmentQueueVisualIconWidget.AnimState.Starting)
			{
				BrushWidget inProgressIconWidget = this.InProgressIconWidget;
				if (inProgressIconWidget != null)
				{
					inProgressIconWidget.BrushRenderer.RestartAnimation();
				}
				this._animState = DevelopmentQueueVisualIconWidget.AnimState.Playing;
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x000254C8 File Offset: 0x000236C8
		private void UpdateVisual(int index)
		{
			if (this.InProgressIconWidget != null && this.QueueIconWidget != null)
			{
				base.IsVisible = (index >= 0);
				this.InProgressIconWidget.IsVisible = (index == 0);
				this._animState = (this.InProgressIconWidget.IsVisible ? DevelopmentQueueVisualIconWidget.AnimState.Start : DevelopmentQueueVisualIconWidget.AnimState.Idle);
				this._tickCount = 0f;
				this.QueueIconWidget.IsVisible = (index > 0);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x00025532 File Offset: 0x00023732
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0002553A File Offset: 0x0002373A
		[Editor(false)]
		public int QueueIndex
		{
			get
			{
				return this._queueIndex;
			}
			set
			{
				if (this._queueIndex != value)
				{
					this._queueIndex = value;
					base.OnPropertyChanged(value, "QueueIndex");
					this.UpdateVisual(value);
				}
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002555F File Offset: 0x0002375F
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x00025567 File Offset: 0x00023767
		[Editor(false)]
		public Widget QueueIconWidget
		{
			get
			{
				return this._queueIconWidget;
			}
			set
			{
				if (this._queueIconWidget != value)
				{
					this._queueIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "QueueIconWidget");
					this.UpdateVisual(this.QueueIndex);
				}
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x00025591 File Offset: 0x00023791
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x00025599 File Offset: 0x00023799
		[Editor(false)]
		public BrushWidget InProgressIconWidget
		{
			get
			{
				return this._inProgressIconWidget;
			}
			set
			{
				if (this._inProgressIconWidget != value)
				{
					this._inProgressIconWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "InProgressIconWidget");
					this.UpdateVisual(this.QueueIndex);
				}
			}
		}

		// Token: 0x04000628 RID: 1576
		private DevelopmentQueueVisualIconWidget.AnimState _animState;

		// Token: 0x04000629 RID: 1577
		private float _tickCount;

		// Token: 0x0400062A RID: 1578
		private int _queueIndex = -1;

		// Token: 0x0400062B RID: 1579
		private Widget _queueIconWidget;

		// Token: 0x0400062C RID: 1580
		private BrushWidget _inProgressIconWidget;

		// Token: 0x020001AE RID: 430
		public enum AnimState
		{
			// Token: 0x040009B9 RID: 2489
			Idle,
			// Token: 0x040009BA RID: 2490
			Start,
			// Token: 0x040009BB RID: 2491
			Starting,
			// Token: 0x040009BC RID: 2492
			Playing
		}
	}
}
