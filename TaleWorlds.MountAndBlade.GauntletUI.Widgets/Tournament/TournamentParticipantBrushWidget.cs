using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tournament
{
	// Token: 0x0200004C RID: 76
	public class TournamentParticipantBrushWidget : BrushWidget
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000D488 File Offset: 0x0000B688
		public TournamentParticipantBrushWidget(UIContext context) : base(context)
		{
			base.AddState("Current");
			base.AddState("Over");
			base.AddState("Dead");
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000D4B2 File Offset: 0x0000B6B2
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			base.EventFired("ClickEvent", Array.Empty<object>());
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D4CA File Offset: 0x0000B6CA
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.AddState("Current");
			child.AddState("Over");
			child.AddState("Dead");
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000D4F4 File Offset: 0x0000B6F4
		protected override void OnConnectedToRoot()
		{
			base.OnConnectedToRoot();
			base.ParentWidget.AddState("Current");
			base.ParentWidget.AddState("Over");
			base.ParentWidget.AddState("Dead");
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D52C File Offset: 0x0000B72C
		private void SetWidgetState(string state)
		{
			base.ParentWidget.SetState(state);
			this.SetState(state);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D544 File Offset: 0x0000B744
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._brushApplied)
			{
				this.NameTextWidget.Brush = (this.IsMainHero ? this.MainHeroTextBrush : this.NormalTextBrush);
				this._brushApplied = true;
			}
			if (this._stateChanged && base.ReadOnlyBrush != null && base.BrushRenderer.Brush != null)
			{
				this._stateChanged = false;
				this.SetWidgetState("Default");
				foreach (BrushLayer brushLayer in base.Brush.Layers)
				{
					brushLayer.Color = base.Brush.Color;
				}
				if (this.OnMission)
				{
					base.Brush.GlobalAlphaFactor = 0.75f;
				}
				else
				{
					base.Brush.GlobalAlphaFactor = 1f;
				}
				if (this.MatchState == 0)
				{
					this.SetWidgetState("Default");
					return;
				}
				if (this.MatchState == 1)
				{
					this.SetWidgetState("Current");
					return;
				}
				if (this.MatchState == 2)
				{
					this.SetWidgetState("Over");
					return;
				}
				if (this.MatchState == 3)
				{
					if (this._isDead && this.OnMission)
					{
						this.SetWidgetState("Dead");
						return;
					}
					this.SetWidgetState("Default");
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000D6AC File Offset: 0x0000B8AC
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public TextWidget NameTextWidget
		{
			get
			{
				return this._nameTextWidget;
			}
			set
			{
				if (this._nameTextWidget != value)
				{
					this._nameTextWidget = value;
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000D6C6 File Offset: 0x0000B8C6
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000D6CE File Offset: 0x0000B8CE
		public int MatchState
		{
			get
			{
				return this._matchState;
			}
			set
			{
				if (this._matchState != value)
				{
					this._stateChanged = true;
					this._matchState = value;
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000D6E7 File Offset: 0x0000B8E7
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000D6EF File Offset: 0x0000B8EF
		public bool IsDead
		{
			get
			{
				return this._isDead;
			}
			set
			{
				if (this._isDead != value)
				{
					this._stateChanged = true;
					this._isDead = value;
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000D708 File Offset: 0x0000B908
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000D710 File Offset: 0x0000B910
		public bool IsMainHero
		{
			get
			{
				return this._isMainHero;
			}
			set
			{
				if (this._isMainHero != value)
				{
					this._isMainHero = value;
					this._brushApplied = false;
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000D729 File Offset: 0x0000B929
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000D731 File Offset: 0x0000B931
		public Brush MainHeroTextBrush
		{
			get
			{
				return this._mainHeroTextBrush;
			}
			set
			{
				if (this._mainHeroTextBrush != value)
				{
					this._mainHeroTextBrush = value;
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000D743 File Offset: 0x0000B943
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000D74B File Offset: 0x0000B94B
		public Brush NormalTextBrush
		{
			get
			{
				return this._normalTextBrush;
			}
			set
			{
				if (this._normalTextBrush != value)
				{
					this._normalTextBrush = value;
				}
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000D75D File Offset: 0x0000B95D
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000D765 File Offset: 0x0000B965
		public bool OnMission
		{
			get
			{
				return this._onMission;
			}
			set
			{
				if (this._onMission != value)
				{
					this._stateChanged = true;
					this._onMission = value;
				}
			}
		}

		// Token: 0x040001BE RID: 446
		private bool _stateChanged;

		// Token: 0x040001BF RID: 447
		private bool _brushApplied;

		// Token: 0x040001C0 RID: 448
		private int _matchState;

		// Token: 0x040001C1 RID: 449
		private bool _isDead;

		// Token: 0x040001C2 RID: 450
		private bool _onMission;

		// Token: 0x040001C3 RID: 451
		private bool _isMainHero;

		// Token: 0x040001C4 RID: 452
		private Brush _mainHeroTextBrush;

		// Token: 0x040001C5 RID: 453
		private Brush _normalTextBrush;

		// Token: 0x040001C6 RID: 454
		private TextWidget _nameTextWidget;
	}
}
