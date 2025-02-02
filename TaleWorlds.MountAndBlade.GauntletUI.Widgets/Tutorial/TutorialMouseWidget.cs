using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x02000046 RID: 70
	public class TutorialMouseWidget : Widget
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000BB12 File Offset: 0x00009D12
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000BB1A File Offset: 0x00009D1A
		public Widget GhostMouseVisualWidget { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000BB23 File Offset: 0x00009D23
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000BB2B File Offset: 0x00009D2B
		public Widget LeftMouseClickVisualWidget { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000BB34 File Offset: 0x00009D34
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000BB3C File Offset: 0x00009D3C
		public Widget RightMouseClickVisualWidget { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000BB45 File Offset: 0x00009D45
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000BB4D File Offset: 0x00009D4D
		public Widget MiddleMouseClickVisualWidget { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000BB56 File Offset: 0x00009D56
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000BB5E File Offset: 0x00009D5E
		public Widget HorizontalArrowWidget { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000BB67 File Offset: 0x00009D67
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000BB6F File Offset: 0x00009D6F
		public Widget VerticalArrowWidget { get; set; }

		// Token: 0x060003B9 RID: 953 RVA: 0x0000BB78 File Offset: 0x00009D78
		public TutorialMouseWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000BB8C File Offset: 0x00009D8C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._animQueue.Count > 0)
			{
				base.IsVisible = true;
				base.ParentWidget.ParentWidget.AlphaFactor = 0.5f;
				this._animQueue.Peek().ForEach(delegate(MouseAnimStage a)
				{
					a.Tick(dt);
				});
				if (this._animQueue.Peek().All((MouseAnimStage a) => a.IsCompleted))
				{
					this._animQueue.Dequeue();
					return;
				}
			}
			else
			{
				this.UpdateAnimQueue();
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000BC3C File Offset: 0x00009E3C
		private void ResetAll()
		{
			this._animQueue.Clear();
			this.ResetAnim();
			this.GhostMouseVisualWidget.SetGlobalAlphaRecursively(0f);
			this.HorizontalArrowWidget.SetGlobalAlphaRecursively(0f);
			this.VerticalArrowWidget.SetGlobalAlphaRecursively(0f);
			base.ParentWidget.ParentWidget.AlphaFactor = 0f;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		private void ResetAnim()
		{
			base.PositionXOffset = 0f;
			base.PositionYOffset = 0f;
			this.SetGlobalAlphaRecursively(0f);
			this.RightMouseClickVisualWidget.SetGlobalAlphaRecursively(0f);
			this.LeftMouseClickVisualWidget.SetGlobalAlphaRecursively(0f);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private void UpdateAnimQueue()
		{
			this.ResetAnim();
			switch (this.CurrentObjectiveType)
			{
			case 1:
				this.HorizontalArrowWidget.HorizontalFlip = true;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.HorizontalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(-20f, 0f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.LeftMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(1f)
				});
				return;
			case 2:
				this.HorizontalArrowWidget.HorizontalFlip = false;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.HorizontalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(20f, 0f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.LeftMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(1f)
				});
				return;
			case 3:
				this.VerticalArrowWidget.VerticalFlip = true;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.VerticalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(0f, -20f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.LeftMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(1f)
				});
				return;
			case 4:
				this.VerticalArrowWidget.VerticalFlip = false;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.VerticalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(0f, 20f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.LeftMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(1f)
				});
				return;
			case 5:
				this.HorizontalArrowWidget.HorizontalFlip = true;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.HorizontalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(-20f, 0f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.RightMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(2f)
				});
				return;
			case 6:
				this.HorizontalArrowWidget.HorizontalFlip = false;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.HorizontalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(20f, 0f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.RightMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(2f)
				});
				return;
			case 7:
				this.VerticalArrowWidget.VerticalFlip = true;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.VerticalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(0f, -20f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.RightMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(2f)
				});
				return;
			case 8:
				this.VerticalArrowWidget.VerticalFlip = false;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.GhostMouseVisualWidget, false),
					MouseAnimStage.CreateFadeInStage(0.15f, this.VerticalArrowWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateMovementStage(0.15f, new Vec2(0f, 20f), this),
					MouseAnimStage.CreateFadeInStage(0.15f, this.RightMouseClickVisualWidget, false)
				});
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateStayStage(2f)
				});
				return;
			default:
				return;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C352 File Offset: 0x0000A552
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000C35A File Offset: 0x0000A55A
		[Editor(false)]
		public int CurrentObjectiveType
		{
			get
			{
				return this._currentObjectiveType;
			}
			set
			{
				if (this._currentObjectiveType != value)
				{
					this._currentObjectiveType = value;
					base.OnPropertyChanged(value, "CurrentObjectiveType");
					this.ResetAll();
					this.UpdateAnimQueue();
				}
			}
		}

		// Token: 0x0400018A RID: 394
		private const float LongStayTime = 1f;

		// Token: 0x0400018B RID: 395
		private const float ShortStayTime = 0.1f;

		// Token: 0x0400018C RID: 396
		private const float FadeInTime = 0.15f;

		// Token: 0x0400018D RID: 397
		private const float FadeOutTime = 0.15f;

		// Token: 0x0400018E RID: 398
		private const float SingleMovementDirection = 20f;

		// Token: 0x0400018F RID: 399
		private const float MovementTime = 0.15f;

		// Token: 0x04000190 RID: 400
		private const float ParentActiveAlpha = 0.5f;

		// Token: 0x04000197 RID: 407
		private Queue<List<MouseAnimStage>> _animQueue = new Queue<List<MouseAnimStage>>();

		// Token: 0x04000198 RID: 408
		private int _currentObjectiveType;
	}
}
