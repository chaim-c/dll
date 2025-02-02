using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial
{
	// Token: 0x0200004A RID: 74
	public class TutorialStickWidget : Widget
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000CBA6 File Offset: 0x0000ADA6
		public TutorialStickWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000CBBA File Offset: 0x0000ADBA
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		public Widget GhostMouseVisualWidget { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000CBD3 File Offset: 0x0000ADD3
		public Widget LeftMouseClickVisualWidget { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000CBDC File Offset: 0x0000ADDC
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		public Widget RightMouseClickVisualWidget { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000CBED File Offset: 0x0000ADED
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		public Widget HorizontalArrowWidget { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000CBFE File Offset: 0x0000ADFE
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000CC06 File Offset: 0x0000AE06
		public Widget VerticalArrowWidget { get; set; }

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CC10 File Offset: 0x0000AE10
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

		// Token: 0x060003FF RID: 1023 RVA: 0x0000CCC0 File Offset: 0x0000AEC0
		private void ResetAll()
		{
			this._animQueue.Clear();
			this.ResetAnim();
			this.GhostMouseVisualWidget.SetGlobalAlphaRecursively(0f);
			this.HorizontalArrowWidget.SetGlobalAlphaRecursively(0f);
			this.VerticalArrowWidget.SetGlobalAlphaRecursively(0f);
			base.ParentWidget.ParentWidget.AlphaFactor = 0f;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000CD24 File Offset: 0x0000AF24
		private void ResetAnim()
		{
			base.PositionXOffset = 0f;
			base.PositionYOffset = 0f;
			this.SetGlobalAlphaRecursively(0f);
			this.RightMouseClickVisualWidget.SetGlobalAlphaRecursively(0f);
			this.LeftMouseClickVisualWidget.SetGlobalAlphaRecursively(0f);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000CD74 File Offset: 0x0000AF74
		private void UpdateAnimQueue()
		{
			this.ResetAnim();
			switch (this.CurrentObjectiveType)
			{
			case 1:
				this.HorizontalArrowWidget.HorizontalFlip = true;
				this._animQueue.Enqueue(new List<MouseAnimStage>
				{
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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
					MouseAnimStage.CreateFadeInStage(0.15f, this, true),
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000D3D6 File Offset: 0x0000B5D6
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000D3DE File Offset: 0x0000B5DE
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

		// Token: 0x040001AF RID: 431
		private const float LongStayTime = 1f;

		// Token: 0x040001B0 RID: 432
		private const float ShortStayTime = 0.1f;

		// Token: 0x040001B1 RID: 433
		private const float FadeInTime = 0.15f;

		// Token: 0x040001B2 RID: 434
		private const float FadeOutTime = 0.15f;

		// Token: 0x040001B3 RID: 435
		private const float SingleMovementDirection = 20f;

		// Token: 0x040001B4 RID: 436
		private const float MovementTime = 0.15f;

		// Token: 0x040001B5 RID: 437
		private const float ParentActiveAlpha = 0.5f;

		// Token: 0x040001BB RID: 443
		private Queue<List<MouseAnimStage>> _animQueue = new Queue<List<MouseAnimStage>>();

		// Token: 0x040001BC RID: 444
		private int _currentObjectiveType;
	}
}
