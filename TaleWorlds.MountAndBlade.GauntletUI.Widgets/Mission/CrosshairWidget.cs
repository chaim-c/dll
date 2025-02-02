using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D3 RID: 211
	public class CrosshairWidget : Widget
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001E881 File Offset: 0x0001CA81
		public CrosshairWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001E88C File Offset: 0x0001CA8C
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.IsVisible)
			{
				base.SuggestedWidth = (float)((int)(74.0 + this.CrosshairAccuracy * 300.0));
				base.SuggestedHeight = (float)((int)(74.0 + this.CrosshairAccuracy * 300.0));
			}
			this.LeftArrow.Brush.AlphaFactor = (float)this.LeftArrowOpacity;
			this.RightArrow.Brush.AlphaFactor = (float)this.RightArrowOpacity;
			this.TopArrow.Brush.AlphaFactor = (float)this.TopArrowOpacity;
			this.BottomArrow.Brush.AlphaFactor = (float)this.BottomArrowOpacity;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001E948 File Offset: 0x0001CB48
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			child.AddState("Invalid");
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001E95C File Offset: 0x0001CB5C
		private void HitMarkerUpdated()
		{
			if (this.HitMarker != null)
			{
				this.HitMarker.AddState("Show");
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001E976 File Offset: 0x0001CB76
		private void HeadshotMarkerUpdated()
		{
			if (this.HeadshotMarker != null)
			{
				this.HitMarker.AddState("Show");
			}
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0001E990 File Offset: 0x0001CB90
		private void ShowHitMarkerChanged()
		{
			if (this.HitMarker == null)
			{
				return;
			}
			string text = this.IsVictimDead ? "ShowDeath" : "Show";
			if (this.HitMarker.CurrentState != text)
			{
				this.HitMarker.SetState(text);
				return;
			}
			this.HitMarker.BrushRenderer.RestartAnimation();
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		private void ShowHeadshotMarkerChanged()
		{
			if (this.HeadshotMarker == null)
			{
				return;
			}
			string text = this.IsHumanoidHeadshot ? "Show" : "Default";
			if (this.HeadshotMarker.CurrentState != text)
			{
				this.HeadshotMarker.SetState(text);
			}
			this.HeadshotMarker.BrushRenderer.RestartAnimation();
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x0001EA46 File Offset: 0x0001CC46
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x0001EA4E File Offset: 0x0001CC4E
		[Editor(false)]
		public double TopArrowOpacity
		{
			get
			{
				return this._topArrowOpacity;
			}
			set
			{
				if (value != this._topArrowOpacity)
				{
					this._topArrowOpacity = value;
					base.OnPropertyChanged(value, "TopArrowOpacity");
				}
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x0001EA6C File Offset: 0x0001CC6C
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x0001EA74 File Offset: 0x0001CC74
		[Editor(false)]
		public double BottomArrowOpacity
		{
			get
			{
				return this._bottomArrowOpacity;
			}
			set
			{
				if (value != this._bottomArrowOpacity)
				{
					this._bottomArrowOpacity = value;
					base.OnPropertyChanged(value, "BottomArrowOpacity");
				}
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001EA92 File Offset: 0x0001CC92
		// (set) Token: 0x06000AD3 RID: 2771 RVA: 0x0001EA9A File Offset: 0x0001CC9A
		[Editor(false)]
		public double RightArrowOpacity
		{
			get
			{
				return this._rightArrowOpacity;
			}
			set
			{
				if (value != this._rightArrowOpacity)
				{
					this._rightArrowOpacity = value;
					base.OnPropertyChanged(value, "RightArrowOpacity");
				}
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x0001EAB8 File Offset: 0x0001CCB8
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001EAC0 File Offset: 0x0001CCC0
		[Editor(false)]
		public double LeftArrowOpacity
		{
			get
			{
				return this._leftArrowOpacity;
			}
			set
			{
				if (value != this._leftArrowOpacity)
				{
					this._leftArrowOpacity = value;
					base.OnPropertyChanged(value, "LeftArrowOpacity");
				}
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x0001EADE File Offset: 0x0001CCDE
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
		[Editor(false)]
		public bool IsTargetInvalid
		{
			get
			{
				return this._isTargetInvalid;
			}
			set
			{
				if (value != this._isTargetInvalid)
				{
					this._isTargetInvalid = value;
					base.OnPropertyChanged(value, "IsTargetInvalid");
					foreach (Widget widget in base.AllChildren)
					{
						widget.SetState(value ? "Invalid" : "Default");
					}
				}
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x0001EB60 File Offset: 0x0001CD60
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0001EB68 File Offset: 0x0001CD68
		[Editor(false)]
		public double CrosshairAccuracy
		{
			get
			{
				return this._crosshairAccuracy;
			}
			set
			{
				if (value != this._crosshairAccuracy)
				{
					this._crosshairAccuracy = value;
					base.OnPropertyChanged(value, "CrosshairAccuracy");
				}
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0001EB86 File Offset: 0x0001CD86
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0001EB8E File Offset: 0x0001CD8E
		[Editor(false)]
		public double CrosshairScale
		{
			get
			{
				return this._crosshairScale;
			}
			set
			{
				if (value != this._crosshairScale)
				{
					this._crosshairScale = value;
					base.OnPropertyChanged(value, "CrosshairScale");
				}
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001EBAC File Offset: 0x0001CDAC
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x0001EBB4 File Offset: 0x0001CDB4
		[Editor(false)]
		public bool IsVictimDead
		{
			get
			{
				return this._isVictimDead;
			}
			set
			{
				if (value != this._isVictimDead)
				{
					this._isVictimDead = value;
					base.OnPropertyChanged(value, "IsVictimDead");
				}
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0001EBD2 File Offset: 0x0001CDD2
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0001EBDA File Offset: 0x0001CDDA
		[Editor(false)]
		public bool IsHumanoidHeadshot
		{
			get
			{
				return this._isHumanoidHeadshot;
			}
			set
			{
				if (value != this._isHumanoidHeadshot)
				{
					this._isHumanoidHeadshot = value;
					base.OnPropertyChanged(value, "IsHumanoidHeadshot");
					this.ShowHeadshotMarkerChanged();
				}
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0001EBFE File Offset: 0x0001CDFE
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0001EC06 File Offset: 0x0001CE06
		[Editor(false)]
		public bool ShowHitMarker
		{
			get
			{
				return this._showHitMarker;
			}
			set
			{
				if (value != this._showHitMarker)
				{
					this._showHitMarker = value;
					base.OnPropertyChanged(value, "ShowHitMarker");
					this.ShowHitMarkerChanged();
				}
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001EC2A File Offset: 0x0001CE2A
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0001EC32 File Offset: 0x0001CE32
		[Editor(false)]
		public BrushWidget LeftArrow
		{
			get
			{
				return this._leftArrow;
			}
			set
			{
				if (value != this._leftArrow)
				{
					this._leftArrow = value;
					base.OnPropertyChanged<BrushWidget>(value, "LeftArrow");
				}
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0001EC50 File Offset: 0x0001CE50
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0001EC58 File Offset: 0x0001CE58
		[Editor(false)]
		public BrushWidget RightArrow
		{
			get
			{
				return this._rightArrow;
			}
			set
			{
				if (value != this._rightArrow)
				{
					this._rightArrow = value;
					base.OnPropertyChanged<BrushWidget>(value, "RightArrow");
				}
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0001EC76 File Offset: 0x0001CE76
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0001EC7E File Offset: 0x0001CE7E
		[Editor(false)]
		public BrushWidget TopArrow
		{
			get
			{
				return this._topArrow;
			}
			set
			{
				if (value != this._topArrow)
				{
					this._topArrow = value;
					base.OnPropertyChanged<BrushWidget>(value, "TopArrow");
				}
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x0001ECA4 File Offset: 0x0001CEA4
		[Editor(false)]
		public BrushWidget BottomArrow
		{
			get
			{
				return this._bottomArrow;
			}
			set
			{
				if (value != this._bottomArrow)
				{
					this._bottomArrow = value;
					base.OnPropertyChanged<BrushWidget>(value, "BottomArrow");
				}
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0001ECC2 File Offset: 0x0001CEC2
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x0001ECCA File Offset: 0x0001CECA
		[Editor(false)]
		public BrushWidget HitMarker
		{
			get
			{
				return this._hitMarker;
			}
			set
			{
				if (value != this._hitMarker)
				{
					this._hitMarker = value;
					base.OnPropertyChanged<BrushWidget>(value, "HitMarker");
					this.HitMarkerUpdated();
				}
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x0001ECEE File Offset: 0x0001CEEE
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x0001ECF6 File Offset: 0x0001CEF6
		[Editor(false)]
		public BrushWidget HeadshotMarker
		{
			get
			{
				return this._headshotMarker;
			}
			set
			{
				if (value != this._headshotMarker)
				{
					this._headshotMarker = value;
					base.OnPropertyChanged<BrushWidget>(value, "HeadshotMarker");
					this.HeadshotMarkerUpdated();
				}
			}
		}

		// Token: 0x040004EA RID: 1258
		private double _crosshairAccuracy;

		// Token: 0x040004EB RID: 1259
		private double _crosshairScale;

		// Token: 0x040004EC RID: 1260
		private bool _isTargetInvalid;

		// Token: 0x040004ED RID: 1261
		private double _topArrowOpacity;

		// Token: 0x040004EE RID: 1262
		private double _bottomArrowOpacity;

		// Token: 0x040004EF RID: 1263
		private double _rightArrowOpacity;

		// Token: 0x040004F0 RID: 1264
		private double _leftArrowOpacity;

		// Token: 0x040004F1 RID: 1265
		private bool _isVictimDead;

		// Token: 0x040004F2 RID: 1266
		private bool _showHitMarker;

		// Token: 0x040004F3 RID: 1267
		private bool _isHumanoidHeadshot;

		// Token: 0x040004F4 RID: 1268
		private BrushWidget _leftArrow;

		// Token: 0x040004F5 RID: 1269
		private BrushWidget _rightArrow;

		// Token: 0x040004F6 RID: 1270
		private BrushWidget _topArrow;

		// Token: 0x040004F7 RID: 1271
		private BrushWidget _bottomArrow;

		// Token: 0x040004F8 RID: 1272
		private BrushWidget _hitMarker;

		// Token: 0x040004F9 RID: 1273
		private BrushWidget _headshotMarker;
	}
}
