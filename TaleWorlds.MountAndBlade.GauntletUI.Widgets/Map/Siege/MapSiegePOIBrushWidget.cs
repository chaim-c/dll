using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Siege
{
	// Token: 0x0200010D RID: 269
	public class MapSiegePOIBrushWidget : BrushWidget
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00027220 File Offset: 0x00025420
		private Color _fullColor
		{
			get
			{
				return new Color(0.2784314f, 0.9882353f, 0.44313726f, 1f);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0002723B File Offset: 0x0002543B
		private Color _emptyColor
		{
			get
			{
				return new Color(0.9882353f, 0.2784314f, 0.2784314f, 1f);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00027256 File Offset: 0x00025456
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0002725E File Offset: 0x0002545E
		public SliderWidget Slider { get; set; }

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00027267 File Offset: 0x00025467
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0002726F File Offset: 0x0002546F
		public Brush ConstructionBrush { get; set; }

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00027278 File Offset: 0x00025478
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x00027280 File Offset: 0x00025480
		public Brush NormalBrush { get; set; }

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00027289 File Offset: 0x00025489
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x00027291 File Offset: 0x00025491
		public Vec2 ScreenPosition { get; set; }

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002729A File Offset: 0x0002549A
		public MapSiegePOIBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000272B4 File Offset: 0x000254B4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			base.ScaledPositionXOffset = this.ScreenPosition.x - base.Size.X / 2f;
			base.ScaledPositionYOffset = this.ScreenPosition.y;
			float valueTo = (float)(this.IsInVisibleRange ? 1 : 0);
			float alphaFactor = MathF.Lerp(base.ReadOnlyBrush.GlobalAlphaFactor, valueTo, dt * 10f, 1E-05f);
			this.SetGlobalAlphaRecursively(alphaFactor);
			base.IsEnabled = false;
			if (this._animState == MapSiegePOIBrushWidget.AnimState.Start)
			{
				this._tickCount++;
				if (this._tickCount > 5)
				{
					this._animState = MapSiegePOIBrushWidget.AnimState.Starting;
				}
			}
			else if (this._animState == MapSiegePOIBrushWidget.AnimState.Starting)
			{
				(this.Slider.Filler as BrushWidget).BrushRenderer.RestartAnimation();
				if (this.QueueIndex == 0)
				{
					this.HammerAnimWidget.BrushRenderer.RestartAnimation();
				}
				this._animState = MapSiegePOIBrushWidget.AnimState.Playing;
			}
			if (!this._isBrushChanged)
			{
				(this.Slider.Filler as BrushWidget).Brush = (this.IsConstructing ? this.ConstructionBrush : this.NormalBrush);
				this._animState = MapSiegePOIBrushWidget.AnimState.Start;
				this._isBrushChanged = true;
			}
			if (!this.IsConstructing)
			{
				this.UpdateColorOfSlider();
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000273F0 File Offset: 0x000255F0
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			this.IsPOISelected = true;
			base.EventFired("OnSelection", Array.Empty<object>());
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002740F File Offset: 0x0002560F
		protected override void OnHoverBegin()
		{
			base.OnHoverBegin();
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00027417 File Offset: 0x00025617
		protected override void OnHoverEnd()
		{
			base.OnHoverEnd();
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00027420 File Offset: 0x00025620
		private void SetMachineTypeIcon(int machineType)
		{
			string text = "SPGeneral\\MapSiege\\";
			switch (machineType)
			{
			case 0:
				text += "wall";
				break;
			case 1:
				text += "broken_wall";
				break;
			case 2:
				text += "ballista";
				break;
			case 3:
				text += "trebuchet";
				break;
			case 4:
				text += "ladder";
				break;
			case 5:
				text += "ram";
				break;
			case 6:
				text += "tower";
				break;
			case 7:
				text += "mangonel";
				break;
			default:
				text += "fallback";
				break;
			}
			this.MachineTypeIconWidget.Sprite = base.Context.SpriteData.GetSprite(text);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x000274F4 File Offset: 0x000256F4
		private void UpdateColorOfSlider()
		{
			(this.Slider.Filler as BrushWidget).Brush.Color = Color.Lerp(this._emptyColor, this._fullColor, this.Slider.ValueFloat / this.Slider.MaxValueFloat);
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00027543 File Offset: 0x00025743
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0002754B File Offset: 0x0002574B
		public MapSiegeConstructionControllerWidget ConstructionControllerWidget
		{
			get
			{
				return this._constructionControllerWidget;
			}
			set
			{
				if (this._constructionControllerWidget != value)
				{
					this._constructionControllerWidget = value;
				}
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0002755D File Offset: 0x0002575D
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x00027565 File Offset: 0x00025765
		public bool IsPlayerSidePOI
		{
			get
			{
				return this._isPlayerSidePOI;
			}
			set
			{
				if (this._isPlayerSidePOI != value)
				{
					this._isPlayerSidePOI = value;
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x00027577 File Offset: 0x00025777
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0002757F File Offset: 0x0002577F
		public bool IsInVisibleRange
		{
			get
			{
				return this._isInVisibleRange;
			}
			set
			{
				if (this._isInVisibleRange != value)
				{
					this._isInVisibleRange = value;
				}
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00027591 File Offset: 0x00025791
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x00027599 File Offset: 0x00025799
		public bool IsPOISelected
		{
			get
			{
				return this._isPOISelected;
			}
			set
			{
				if (this._isPOISelected != value)
				{
					this._isPOISelected = value;
					this.ConstructionControllerWidget.SetCurrentPOIWidget(value ? this : null);
				}
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x000275BD File Offset: 0x000257BD
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x000275C5 File Offset: 0x000257C5
		public bool IsConstructing
		{
			get
			{
				return this._isConstructing;
			}
			set
			{
				if (this._isConstructing != value)
				{
					this._isConstructing = value;
					this._isBrushChanged = false;
					this._animState = MapSiegePOIBrushWidget.AnimState.Idle;
				}
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x000275E5 File Offset: 0x000257E5
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x000275ED File Offset: 0x000257ED
		public int MachineType
		{
			get
			{
				return this._machineType;
			}
			set
			{
				if (this._machineType != value)
				{
					this._machineType = value;
					this.SetMachineTypeIcon(value);
				}
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x00027606 File Offset: 0x00025806
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0002760E File Offset: 0x0002580E
		public int QueueIndex
		{
			get
			{
				return this._queueIndex;
			}
			set
			{
				if (this._queueIndex != value)
				{
					this._queueIndex = value;
					this._animState = MapSiegePOIBrushWidget.AnimState.Start;
					this._tickCount = 0;
				}
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0002762E File Offset: 0x0002582E
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x00027636 File Offset: 0x00025836
		public Widget MachineTypeIconWidget
		{
			get
			{
				return this._machineTypeIconWidget;
			}
			set
			{
				if (this._machineTypeIconWidget != value)
				{
					this._machineTypeIconWidget = value;
				}
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00027648 File Offset: 0x00025848
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x00027650 File Offset: 0x00025850
		public BrushWidget HammerAnimWidget
		{
			get
			{
				return this._hammerAnimWidget;
			}
			set
			{
				if (this._hammerAnimWidget != value)
				{
					this._hammerAnimWidget = value;
				}
			}
		}

		// Token: 0x04000681 RID: 1665
		private MapSiegePOIBrushWidget.AnimState _animState;

		// Token: 0x04000686 RID: 1670
		private bool _isBrushChanged;

		// Token: 0x04000687 RID: 1671
		private int _tickCount;

		// Token: 0x04000688 RID: 1672
		private bool _isConstructing;

		// Token: 0x04000689 RID: 1673
		private bool _isPlayerSidePOI;

		// Token: 0x0400068A RID: 1674
		private bool _isInVisibleRange;

		// Token: 0x0400068B RID: 1675
		private bool _isPOISelected;

		// Token: 0x0400068C RID: 1676
		private BrushWidget _hammerAnimWidget;

		// Token: 0x0400068D RID: 1677
		private Widget _machineTypeIconWidget;

		// Token: 0x0400068E RID: 1678
		private int _machineType = -1;

		// Token: 0x0400068F RID: 1679
		private int _queueIndex = -1;

		// Token: 0x04000690 RID: 1680
		private MapSiegeConstructionControllerWidget _constructionControllerWidget;

		// Token: 0x020001B0 RID: 432
		public enum AnimState
		{
			// Token: 0x040009C0 RID: 2496
			Idle,
			// Token: 0x040009C1 RID: 2497
			Start,
			// Token: 0x040009C2 RID: 2498
			Starting,
			// Token: 0x040009C3 RID: 2499
			Playing
		}
	}
}
