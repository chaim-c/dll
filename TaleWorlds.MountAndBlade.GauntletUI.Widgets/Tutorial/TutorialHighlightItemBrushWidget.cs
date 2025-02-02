using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000045 RID: 69
	public class TutorialHighlightItemBrushWidget : BrushWidget
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0000B900 File Offset: 0x00009B00
		public Widget CustomSizeSyncTarget { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000B909 File Offset: 0x00009B09
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0000B911 File Offset: 0x00009B11
		public bool DoNotOverrideWidth { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000B91A File Offset: 0x00009B1A
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x0000B922 File Offset: 0x00009B22
		public bool DoNotOverrideHeight { get; set; }

		// Token: 0x060003A8 RID: 936 RVA: 0x0000B92B File Offset: 0x00009B2B
		public TutorialHighlightItemBrushWidget(UIContext context) : base(context)
		{
			base.UseGlobalTimeForAnimation = true;
			base.DoNotAcceptEvents = true;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000B944 File Offset: 0x00009B44
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._animState == TutorialHighlightItemBrushWidget.AnimState.Start)
			{
				this._animState = TutorialHighlightItemBrushWidget.AnimState.FirstFrame;
			}
			else if (this._animState == TutorialHighlightItemBrushWidget.AnimState.FirstFrame)
			{
				if (base.BrushRenderer.Brush == null)
				{
					this._animState = TutorialHighlightItemBrushWidget.AnimState.Start;
				}
				else
				{
					this._animState = TutorialHighlightItemBrushWidget.AnimState.Playing;
					base.BrushRenderer.RestartAnimation();
				}
			}
			if (this.IsHighlightEnabled && this._isDisabled)
			{
				this._isDisabled = false;
				this.SetState("Default");
			}
			else if (!this.IsHighlightEnabled && !this._isDisabled)
			{
				this.SetState("Disabled");
				this._isDisabled = true;
			}
			this.UpdateTargetSize();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		private void UpdateTargetSize()
		{
			Widget widget = this.CustomSizeSyncTarget ?? base.ParentWidget;
			if (widget == null)
			{
				return;
			}
			bool flag;
			if (widget.HeightSizePolicy == SizePolicy.CoverChildren || widget.WidthSizePolicy == SizePolicy.CoverChildren)
			{
				if (!this.DoNotOverrideWidth)
				{
					base.WidthSizePolicy = SizePolicy.Fixed;
				}
				if (!this.DoNotOverrideHeight)
				{
					base.HeightSizePolicy = SizePolicy.Fixed;
				}
				flag = true;
			}
			else
			{
				base.WidthSizePolicy = SizePolicy.StretchToParent;
				base.HeightSizePolicy = SizePolicy.StretchToParent;
				flag = false;
			}
			if (flag && widget.Size.X > 1f && widget.Size.Y > 1f)
			{
				if (!this.DoNotOverrideWidth)
				{
					base.ScaledSuggestedWidth = widget.Size.X - 1f;
				}
				if (!this.DoNotOverrideHeight)
				{
					base.ScaledSuggestedHeight = widget.Size.Y - 1f;
				}
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000BAB3 File Offset: 0x00009CB3
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000BABC File Offset: 0x00009CBC
		[Editor(false)]
		public bool IsHighlightEnabled
		{
			get
			{
				return this._isHighlightEnabled;
			}
			set
			{
				if (this._isHighlightEnabled != value)
				{
					this._isHighlightEnabled = value;
					base.OnPropertyChanged(value, "IsHighlightEnabled");
					if (this.IsHighlightEnabled)
					{
						this._animState = TutorialHighlightItemBrushWidget.AnimState.Start;
					}
					base.IsVisible = value;
					TaleWorlds.GauntletUI.EventManager.UIEventManager.TriggerEvent<TutorialHighlightItemBrushWidget.HighlightElementToggledEvent>(new TutorialHighlightItemBrushWidget.HighlightElementToggledEvent(value, value ? this : null));
				}
			}
		}

		// Token: 0x04000187 RID: 391
		private TutorialHighlightItemBrushWidget.AnimState _animState;

		// Token: 0x04000188 RID: 392
		private bool _isDisabled;

		// Token: 0x04000189 RID: 393
		private bool _isHighlightEnabled;

		// Token: 0x02000194 RID: 404
		public enum AnimState
		{
			// Token: 0x04000967 RID: 2407
			Idle,
			// Token: 0x04000968 RID: 2408
			Start,
			// Token: 0x04000969 RID: 2409
			FirstFrame,
			// Token: 0x0400096A RID: 2410
			Playing
		}

		// Token: 0x02000195 RID: 405
		public class HighlightElementToggledEvent : EventBase
		{
			// Token: 0x17000719 RID: 1817
			// (get) Token: 0x06001447 RID: 5191 RVA: 0x00037CA0 File Offset: 0x00035EA0
			// (set) Token: 0x06001448 RID: 5192 RVA: 0x00037CA8 File Offset: 0x00035EA8
			public bool IsEnabled { get; private set; }

			// Token: 0x1700071A RID: 1818
			// (get) Token: 0x06001449 RID: 5193 RVA: 0x00037CB1 File Offset: 0x00035EB1
			// (set) Token: 0x0600144A RID: 5194 RVA: 0x00037CB9 File Offset: 0x00035EB9
			public TutorialHighlightItemBrushWidget HighlightFrameWidget { get; private set; }

			// Token: 0x0600144B RID: 5195 RVA: 0x00037CC2 File Offset: 0x00035EC2
			public HighlightElementToggledEvent(bool isEnabled, TutorialHighlightItemBrushWidget highlightFrameWidget)
			{
				this.IsEnabled = isEnabled;
				this.HighlightFrameWidget = highlightFrameWidget;
			}
		}
	}
}
