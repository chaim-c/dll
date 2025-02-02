using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Loading
{
	// Token: 0x0200011F RID: 287
	public class LoadingWindowWidget : Widget
	{
		// Token: 0x06000EDD RID: 3805 RVA: 0x00029559 File Offset: 0x00027759
		public LoadingWindowWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00029564 File Offset: 0x00027764
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this._edgeOffsetPadding = base.EventManager.PageSize.X / 2.72f;
			if (this.AnimWidget != null && base.IsVisible && this.AnimWidget.IsVisible)
			{
				this.AnimWidget.ScaledPositionXOffset = MathF.PingPong(this._edgeOffsetPadding, base.EventManager.PageSize.X - this.AnimWidget.Size.X - this._edgeOffsetPadding, this._totalDt);
				this._totalDt += dt * 500f;
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x00029609 File Offset: 0x00027809
		private void UpdateStates()
		{
			base.IsVisible = this.IsActive;
			base.IsEnabled = this.IsActive;
			base.ParentWidget.IsVisible = this.IsActive;
			base.ParentWidget.IsEnabled = this.IsActive;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00029648 File Offset: 0x00027848
		private void UpdateImage(string imageName)
		{
			Sprite sprite = base.Context.SpriteData.GetSprite(imageName);
			if (sprite == null)
			{
				base.Sprite = base.Context.SpriteData.GetSprite("background_1");
				return;
			}
			base.Sprite = sprite;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0002968D File Offset: 0x0002788D
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00029695 File Offset: 0x00027895
		[Editor(false)]
		public Widget AnimWidget
		{
			get
			{
				return this._animWidget;
			}
			set
			{
				if (this._animWidget != value)
				{
					this._animWidget = value;
					base.OnPropertyChanged<Widget>(value, "AnimWidget");
				}
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x000296B3 File Offset: 0x000278B3
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x000296BB File Offset: 0x000278BB
		[Editor(false)]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (this._isActive != value)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
					this.UpdateStates();
				}
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000296DF File Offset: 0x000278DF
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x000296E7 File Offset: 0x000278E7
		[Editor(false)]
		public string ImageName
		{
			get
			{
				return this._imageName;
			}
			set
			{
				if (this._imageName != value)
				{
					this._imageName = value;
					base.OnPropertyChanged<string>(value, "ImageName");
					this.UpdateImage(value);
				}
			}
		}

		// Token: 0x040006D1 RID: 1745
		private const string _defaultBackgroundSpriteData = "background_1";

		// Token: 0x040006D2 RID: 1746
		private float _edgeOffsetPadding;

		// Token: 0x040006D3 RID: 1747
		private float _totalDt;

		// Token: 0x040006D4 RID: 1748
		private Widget _animWidget;

		// Token: 0x040006D5 RID: 1749
		private bool _isActive;

		// Token: 0x040006D6 RID: 1750
		private string _imageName;
	}
}
