using System;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000058 RID: 88
	public class BrushWidget : Widget
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x00017ADC File Offset: 0x00015CDC
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00017B56 File Offset: 0x00015D56
		[Editor(false)]
		public Brush Brush
		{
			get
			{
				if (this._originalBrush == null)
				{
					this._originalBrush = base.Context.DefaultBrush;
					this._clonedBrush = this._originalBrush.Clone();
					this.BrushRenderer.Brush = this.ReadOnlyBrush;
				}
				else if (this._clonedBrush == null)
				{
					this._clonedBrush = this._originalBrush.Clone();
					this.BrushRenderer.Brush = this.ReadOnlyBrush;
				}
				return this._clonedBrush;
			}
			set
			{
				if (this._originalBrush != value)
				{
					this._originalBrush = value;
					this._clonedBrush = null;
					this.OnBrushChanged();
					base.OnPropertyChanged<Brush>(value, "Brush");
				}
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x00017B81 File Offset: 0x00015D81
		public Brush ReadOnlyBrush
		{
			get
			{
				if (this._clonedBrush != null)
				{
					return this._clonedBrush;
				}
				if (this._originalBrush == null)
				{
					this._originalBrush = base.Context.DefaultBrush;
				}
				return this._originalBrush;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00017BB1 File Offset: 0x00015DB1
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00017BCD File Offset: 0x00015DCD
		[Editor(false)]
		public new Sprite Sprite
		{
			get
			{
				return this.ReadOnlyBrush.DefaultStyle.GetLayer("Default").Sprite;
			}
			set
			{
				this.Brush.DefaultStyle.GetLayer("Default").Sprite = value;
			}
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017BEA File Offset: 0x00015DEA
		public void ForceUseBrush(Brush brush)
		{
			this._clonedBrush = brush;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x00017BF3 File Offset: 0x00015DF3
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x00017BFB File Offset: 0x00015DFB
		public BrushRenderer BrushRenderer { get; private set; }

		// Token: 0x0600059D RID: 1437 RVA: 0x00017C04 File Offset: 0x00015E04
		public BrushWidget(UIContext context) : base(context)
		{
			this.BrushRenderer = new BrushRenderer();
			base.EventFire += this.BrushWidget_EventFire;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017C2C File Offset: 0x00015E2C
		private void BrushWidget_EventFire(Widget arg1, string eventName, object[] arg3)
		{
			if (this.ReadOnlyBrush != null)
			{
				AudioProperty eventAudioProperty = this.ReadOnlyBrush.SoundProperties.GetEventAudioProperty(eventName);
				if (eventAudioProperty != null && eventAudioProperty.AudioName != null && !eventAudioProperty.AudioName.Equals(""))
				{
					base.EventManager.Context.TwoDimensionContext.PlaySound(eventAudioProperty.AudioName);
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00017C8C File Offset: 0x00015E8C
		public override void UpdateBrushes(float dt)
		{
			if (base.IsVisible)
			{
				Rectangle rectangle = new Rectangle(this._cachedGlobalPosition.X, this._cachedGlobalPosition.Y, base.MeasuredSize.X, base.MeasuredSize.Y);
				Rectangle other = new Rectangle(base.EventManager.LeftUsableAreaStart, base.EventManager.TopUsableAreaStart, base.EventManager.PageSize.X, base.EventManager.PageSize.Y);
				this._isInsideCache = rectangle.IsCollide(other);
				if (this._isInsideCache)
				{
					this.UpdateBrushRendererInternal(dt);
				}
			}
			if (!base.IsVisible || !this._isInsideCache || !this.BrushRenderer.IsUpdateNeeded())
			{
				this.UnRegisterUpdateBrushes();
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00017D54 File Offset: 0x00015F54
		protected void UpdateBrushRendererInternal(float dt)
		{
			UIContext context = base.Context;
			bool flag;
			if (context == null)
			{
				flag = (null != null);
			}
			else
			{
				TwoDimensionContext twoDimensionContext = context.TwoDimensionContext;
				flag = (((twoDimensionContext != null) ? twoDimensionContext.Platform : null) != null);
			}
			if (!flag)
			{
				Debug.FailedAssert("Trying to update brush renderer after context or platform is finalized", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\BaseTypes\\BrushWidget.cs", "UpdateBrushRendererInternal", 141);
				return;
			}
			this.BrushRenderer.ForcePixelPerfectPlacement = base.ForcePixelPerfectRenderPlacement;
			this.BrushRenderer.UseLocalTimer = !base.UseGlobalTimeForAnimation;
			this.BrushRenderer.Brush = this.ReadOnlyBrush;
			this.BrushRenderer.CurrentState = base.CurrentState;
			this.BrushRenderer.Update(base.Context.TwoDimensionContext.Platform.ApplicationTime, dt);
			if (base.RestartAnimationFirstFrame && !this._animRestarted)
			{
				base.EventManager.AddLateUpdateAction(this, delegate(float _dt)
				{
					if (base.RestartAnimationFirstFrame)
					{
						this.BrushRenderer.RestartAnimation();
					}
				}, 5);
				this._animRestarted = true;
			}
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00017E34 File Offset: 0x00016034
		public override void SetState(string stateName)
		{
			if (base.CurrentState != stateName)
			{
				if (base.EventManager != null && this.ReadOnlyBrush != null)
				{
					AudioProperty stateAudioProperty = this.ReadOnlyBrush.SoundProperties.GetStateAudioProperty(stateName);
					if (stateAudioProperty != null)
					{
						if (stateAudioProperty.AudioName != null && !stateAudioProperty.AudioName.Equals(""))
						{
							base.EventManager.Context.TwoDimensionContext.PlaySound(stateAudioProperty.AudioName);
						}
						else
						{
							Debug.FailedAssert(string.Concat(new string[]
							{
								"Widget with id \"",
								base.Id,
								"\" has a sound having no audioName for event \"",
								stateName,
								"\"!"
							}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\BaseTypes\\BrushWidget.cs", "SetState", 181);
						}
					}
				}
				this.RegisterUpdateBrushes();
			}
			base.SetState(stateName);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00017F05 File Offset: 0x00016105
		protected override void RefreshState()
		{
			base.RefreshState();
			this.RegisterUpdateBrushes();
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00017F14 File Offset: 0x00016114
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (!this._isInsideCache || this.BrushRenderer.IsUpdateNeeded())
			{
				this.HandleUpdateNeededOnRender();
			}
			this.BrushRenderer.Render(drawContext, this._cachedGlobalPosition, base.Size, base._scaleToUse, base.Context.ContextAlpha, default(Vector2));
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00017F6E File Offset: 0x0001616E
		protected void HandleUpdateNeededOnRender()
		{
			this.UpdateBrushRendererInternal(base.EventManager.CachedDt);
			if (this.BrushRenderer.IsUpdateNeeded())
			{
				this.RegisterUpdateBrushes();
			}
			this._isInsideCache = true;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00017F9B File Offset: 0x0001619B
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.BrushRenderer.SetSeed(this._seed);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00017FB4 File Offset: 0x000161B4
		public override void UpdateAnimationPropertiesSubTask(float alphaFactor)
		{
			this.Brush.GlobalAlphaFactor = alphaFactor;
			foreach (Widget widget in base.Children)
			{
				widget.UpdateAnimationPropertiesSubTask(alphaFactor);
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00018014 File Offset: 0x00016214
		public virtual void OnBrushChanged()
		{
			this.RegisterUpdateBrushes();
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001801C File Offset: 0x0001621C
		protected void RegisterUpdateBrushes()
		{
			base.EventManager.RegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, this);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001802B File Offset: 0x0001622B
		protected void UnRegisterUpdateBrushes()
		{
			base.EventManager.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, this);
		}

		// Token: 0x040002A7 RID: 679
		private Brush _originalBrush;

		// Token: 0x040002A8 RID: 680
		private Brush _clonedBrush;

		// Token: 0x040002AA RID: 682
		private bool _animRestarted;

		// Token: 0x040002AB RID: 683
		protected bool _isInsideCache;
	}
}
