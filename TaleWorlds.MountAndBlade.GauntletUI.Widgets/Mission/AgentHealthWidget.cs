using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000CD RID: 205
	public class AgentHealthWidget : Widget
	{
		// Token: 0x06000A8E RID: 2702 RVA: 0x0001DDAF File Offset: 0x0001BFAF
		public AgentHealthWidget(UIContext context) : base(context)
		{
			this._healtDrops = new List<AgentHealthWidget.HealthDropData>();
			this.CheckVisibility();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001DDE8 File Offset: 0x0001BFE8
		private void CreateHealthDrop(Widget container, int preHealth, int currentHealth)
		{
			float num = container.Size.X / base._scaleToUse;
			float suggestedWidth = Mathf.Ceil(num * ((float)(preHealth - currentHealth) / (float)this._maxHealth));
			float positionXOffset = Mathf.Floor(num * ((float)currentHealth / (float)this._maxHealth));
			BrushWidget brushWidget = new BrushWidget(base.Context);
			brushWidget.WidthSizePolicy = SizePolicy.Fixed;
			brushWidget.HeightSizePolicy = SizePolicy.Fixed;
			brushWidget.Brush = this.HealthDropBrush;
			brushWidget.SuggestedWidth = suggestedWidth;
			brushWidget.SuggestedHeight = (float)brushWidget.ReadOnlyBrush.Sprite.Height;
			brushWidget.HorizontalAlignment = HorizontalAlignment.Left;
			brushWidget.VerticalAlignment = VerticalAlignment.Center;
			brushWidget.PositionXOffset = positionXOffset;
			brushWidget.ParentWidget = container;
			this._healtDrops.Add(new AgentHealthWidget.HealthDropData(brushWidget, this.AnimationDelay + this.AnimationDuration));
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.HealthBar != null && this.HealthBar.IsVisible)
			{
				for (int i = this._healtDrops.Count - 1; i >= 0; i--)
				{
					AgentHealthWidget.HealthDropData healthDropData = this._healtDrops[i];
					healthDropData.LifeTime -= dt;
					if (healthDropData.LifeTime <= 0f)
					{
						this.HealthDropContainer.RemoveChild(healthDropData.Widget);
						this._healtDrops.RemoveAt(i);
					}
					else
					{
						float alphaFactor = Mathf.Min(1f, healthDropData.LifeTime / this.AnimationDuration);
						healthDropData.Widget.Brush.AlphaFactor = alphaFactor;
					}
				}
			}
			this.CheckVisibility();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001DF6C File Offset: 0x0001C16C
		private void HealthChanged(bool createDropVisual = true)
		{
			this.HealthBar.MaxAmount = this._maxHealth;
			this.HealthBar.InitialAmount = this.Health;
			if (this._prevHealth > this.Health)
			{
				this.CreateHealthDrop(this.HealthDropContainer, this._prevHealth, this.Health);
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		private void CheckVisibility()
		{
			bool flag = this.ShowHealthBar;
			if (flag)
			{
				flag = ((float)this._health > 0f || this._healtDrops.Count > 0);
			}
			base.IsVisible = flag;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x0001E002 File Offset: 0x0001C202
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0001E00A File Offset: 0x0001C20A
		[Editor(false)]
		public int Health
		{
			get
			{
				return this._health;
			}
			set
			{
				if (this._health != value)
				{
					this._prevHealth = this._health;
					this._health = value;
					this.HealthChanged(true);
					base.OnPropertyChanged(value, "Health");
				}
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001E03B File Offset: 0x0001C23B
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x0001E043 File Offset: 0x0001C243
		[Editor(false)]
		public int MaxHealth
		{
			get
			{
				return this._maxHealth;
			}
			set
			{
				if (this._maxHealth != value)
				{
					this._maxHealth = value;
					this.HealthChanged(false);
					base.OnPropertyChanged(value, "MaxHealth");
				}
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001E068 File Offset: 0x0001C268
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0001E070 File Offset: 0x0001C270
		[Editor(false)]
		public FillBarWidget HealthBar
		{
			get
			{
				return this._healthBar;
			}
			set
			{
				if (this._healthBar != value)
				{
					this._healthBar = value;
					base.OnPropertyChanged<FillBarWidget>(value, "HealthBar");
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x0001E08E File Offset: 0x0001C28E
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x0001E096 File Offset: 0x0001C296
		[Editor(false)]
		public Widget HealthDropContainer
		{
			get
			{
				return this._healthDropContainer;
			}
			set
			{
				if (this._healthDropContainer != value)
				{
					this._healthDropContainer = value;
					base.OnPropertyChanged<Widget>(value, "HealthDropContainer");
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0001E0B4 File Offset: 0x0001C2B4
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		[Editor(false)]
		public Brush HealthDropBrush
		{
			get
			{
				return this._healthDropBrush;
			}
			set
			{
				if (this._healthDropBrush != value)
				{
					this._healthDropBrush = value;
					base.OnPropertyChanged<Brush>(value, "HealthDropBrush");
				}
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0001E0DA File Offset: 0x0001C2DA
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0001E0E2 File Offset: 0x0001C2E2
		[Editor(false)]
		public bool ShowHealthBar
		{
			get
			{
				return this._showHealthBar;
			}
			set
			{
				if (this._showHealthBar != value)
				{
					this._showHealthBar = value;
					base.OnPropertyChanged(value, "ShowHealthBar");
				}
			}
		}

		// Token: 0x040004D1 RID: 1233
		private float AnimationDelay = 0.2f;

		// Token: 0x040004D2 RID: 1234
		private float AnimationDuration = 0.8f;

		// Token: 0x040004D3 RID: 1235
		private List<AgentHealthWidget.HealthDropData> _healtDrops;

		// Token: 0x040004D4 RID: 1236
		private int _health;

		// Token: 0x040004D5 RID: 1237
		private int _prevHealth = -1;

		// Token: 0x040004D6 RID: 1238
		private int _maxHealth;

		// Token: 0x040004D7 RID: 1239
		private bool _showHealthBar;

		// Token: 0x040004D8 RID: 1240
		private FillBarWidget _healthBar;

		// Token: 0x040004D9 RID: 1241
		private Widget _healthDropContainer;

		// Token: 0x040004DA RID: 1242
		private Brush _healthDropBrush;

		// Token: 0x020001AA RID: 426
		public class HealthDropData
		{
			// Token: 0x06001480 RID: 5248 RVA: 0x00037F7A File Offset: 0x0003617A
			public HealthDropData(BrushWidget widget, float lifeTime)
			{
				this.Widget = widget;
				this.LifeTime = lifeTime;
			}

			// Token: 0x040009A8 RID: 2472
			public BrushWidget Widget;

			// Token: 0x040009A9 RID: 2473
			public float LifeTime;
		}
	}
}
