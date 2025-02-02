using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.MapBar
{
	// Token: 0x0200011B RID: 283
	public class MapBarUnreadBrushWidget : BrushWidget
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00029028 File Offset: 0x00027228
		// (set) Token: 0x06000EBF RID: 3775 RVA: 0x00029030 File Offset: 0x00027230
		public bool IsBannerNotification { get; set; }

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00029039 File Offset: 0x00027239
		public MapBarUnreadBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00029044 File Offset: 0x00027244
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (base.IsVisible && this._animState == MapBarUnreadBrushWidget.AnimState.Idle)
			{
				this._animState = MapBarUnreadBrushWidget.AnimState.Start;
			}
			if (this._animState == MapBarUnreadBrushWidget.AnimState.Start)
			{
				this._animState = MapBarUnreadBrushWidget.AnimState.FirstFrame;
			}
			else if (this._animState == MapBarUnreadBrushWidget.AnimState.FirstFrame)
			{
				if (base.BrushRenderer.Brush == null)
				{
					this._animState = MapBarUnreadBrushWidget.AnimState.Start;
				}
				else
				{
					this._animState = MapBarUnreadBrushWidget.AnimState.Playing;
					base.BrushRenderer.RestartAnimation();
				}
			}
			if (this.IsBannerNotification && base.IsVisible && this._animState == MapBarUnreadBrushWidget.AnimState.Idle)
			{
				this._animState = MapBarUnreadBrushWidget.AnimState.Start;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000290D1 File Offset: 0x000272D1
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000290D9 File Offset: 0x000272D9
		[Editor(false)]
		public TextWidget UnreadTextWidget
		{
			get
			{
				return this._unreadTextWidget;
			}
			set
			{
				if (this._unreadTextWidget != value)
				{
					this._unreadTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "UnreadTextWidget");
					if (value != null)
					{
						value.boolPropertyChanged += this.UnreadTextWidgetOnPropertyChanged;
					}
				}
			}
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002910C File Offset: 0x0002730C
		private void UnreadTextWidgetOnPropertyChanged(PropertyOwnerObject widget, string propertyName, bool propertyValue)
		{
			if (propertyName == "IsVisible")
			{
				base.IsVisible = propertyValue;
				this._animState = (base.IsVisible ? MapBarUnreadBrushWidget.AnimState.Start : MapBarUnreadBrushWidget.AnimState.Idle);
			}
		}

		// Token: 0x040006C7 RID: 1735
		private MapBarUnreadBrushWidget.AnimState _animState;

		// Token: 0x040006C8 RID: 1736
		private TextWidget _unreadTextWidget;

		// Token: 0x020001B1 RID: 433
		public enum AnimState
		{
			// Token: 0x040009C5 RID: 2501
			Idle,
			// Token: 0x040009C6 RID: 2502
			Start,
			// Token: 0x040009C7 RID: 2503
			FirstFrame,
			// Token: 0x040009C8 RID: 2504
			Playing
		}
	}
}
