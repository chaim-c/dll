using System;
using System.Numerics;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000015 RID: 21
	public class BrushListPanel : ListPanel
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000070D8 File Offset: 0x000052D8
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00007162 File Offset: 0x00005362
		[Editor(false)]
		public Brush Brush
		{
			get
			{
				if (this._originalBrush == null)
				{
					this._originalBrush = base.Context.DefaultBrush;
					this._clonedBrush = this._originalBrush.Clone();
					if (this.BrushRenderer != null)
					{
						this.BrushRenderer.Brush = this.ReadOnlyBrush;
					}
				}
				else if (this._clonedBrush == null)
				{
					this._clonedBrush = this._originalBrush.Clone();
					if (this.BrushRenderer != null)
					{
						this.BrushRenderer.Brush = this.ReadOnlyBrush;
					}
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000718D File Offset: 0x0000538D
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000071BD File Offset: 0x000053BD
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000071D9 File Offset: 0x000053D9
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

		// Token: 0x06000154 RID: 340 RVA: 0x000071F6 File Offset: 0x000053F6
		public void ForceUseBrush(Brush brush)
		{
			this._clonedBrush = brush;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000071FF File Offset: 0x000053FF
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00007207 File Offset: 0x00005407
		public BrushRenderer BrushRenderer { get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x00007210 File Offset: 0x00005410
		public BrushListPanel(UIContext context) : base(context)
		{
			this.BrushRenderer = new BrushRenderer();
			base.EventFire += this.BrushWidget_EventFire;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007238 File Offset: 0x00005438
		private void BrushWidget_EventFire(Widget arg1, string eventName, object[] arg3)
		{
			if (this.ReadOnlyBrush != null)
			{
				AudioProperty eventAudioProperty = this.Brush.SoundProperties.GetEventAudioProperty(eventName);
				if (eventAudioProperty != null && eventAudioProperty.AudioName != null && !eventAudioProperty.AudioName.Equals(""))
				{
					base.EventManager.Context.TwoDimensionContext.PlaySound(eventAudioProperty.AudioName);
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007298 File Offset: 0x00005498
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

		// Token: 0x0600015A RID: 346 RVA: 0x00007360 File Offset: 0x00005560
		private void UpdateBrushRendererInternal(float dt)
		{
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

		// Token: 0x0600015B RID: 347 RVA: 0x00007408 File Offset: 0x00005608
		public override void SetState(string stateName)
		{
			if (base.CurrentState != stateName)
			{
				if (base.EventManager != null && this.ReadOnlyBrush != null)
				{
					AudioProperty stateAudioProperty = this.Brush.SoundProperties.GetStateAudioProperty(stateName);
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
							}), "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.GauntletUI\\Brush\\BrushListPanel.cs", "SetState", 180);
						}
					}
				}
				this.RegisterUpdateBrushes();
			}
			base.SetState(stateName);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000074D9 File Offset: 0x000056D9
		protected override void RefreshState()
		{
			base.RefreshState();
			this.RegisterUpdateBrushes();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000074E8 File Offset: 0x000056E8
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (!this._isInsideCache || this.BrushRenderer.IsUpdateNeeded())
			{
				this.HandleUpdateNeededOnRender();
			}
			this.BrushRenderer.Render(drawContext, this._cachedGlobalPosition, base.Size, base._scaleToUse, base.Context.ContextAlpha, default(Vector2));
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007542 File Offset: 0x00005742
		protected void HandleUpdateNeededOnRender()
		{
			this.UpdateBrushRendererInternal(base.EventManager.CachedDt);
			if (this.BrushRenderer.IsUpdateNeeded())
			{
				this.RegisterUpdateBrushes();
			}
			this._isInsideCache = true;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000756F File Offset: 0x0000576F
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			this.BrushRenderer.SetSeed(this._seed);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007588 File Offset: 0x00005788
		public override void UpdateAnimationPropertiesSubTask(float alphaFactor)
		{
			this.Brush.GlobalAlphaFactor = alphaFactor;
			foreach (Widget widget in base.Children)
			{
				widget.UpdateAnimationPropertiesSubTask(alphaFactor);
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000075E8 File Offset: 0x000057E8
		public virtual void OnBrushChanged()
		{
			this.RegisterUpdateBrushes();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000075F0 File Offset: 0x000057F0
		private void RegisterUpdateBrushes()
		{
			base.EventManager.RegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, this);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000075FF File Offset: 0x000057FF
		private void UnRegisterUpdateBrushes()
		{
			base.EventManager.UnRegisterWidgetForEvent(WidgetContainer.ContainerType.UpdateBrushes, this);
		}

		// Token: 0x04000075 RID: 117
		private Brush _originalBrush;

		// Token: 0x04000076 RID: 118
		private Brush _clonedBrush;

		// Token: 0x04000078 RID: 120
		private bool _animRestarted;

		// Token: 0x04000079 RID: 121
		private bool _isInsideCache;
	}
}
