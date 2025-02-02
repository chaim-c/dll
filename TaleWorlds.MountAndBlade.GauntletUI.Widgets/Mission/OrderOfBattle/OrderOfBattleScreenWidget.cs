using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E8 RID: 232
	public class OrderOfBattleScreenWidget : Widget
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00020FC5 File Offset: 0x0001F1C5
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00020FCD File Offset: 0x0001F1CD
		public float AlphaChangeDuration { get; set; } = 0.15f;

		// Token: 0x06000BFE RID: 3070 RVA: 0x00020FD6 File Offset: 0x0001F1D6
		public OrderOfBattleScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002100C File Offset: 0x0001F20C
		protected override void OnLateUpdate(float dt)
		{
			if (this._isTransitioning)
			{
				if (this._alphaChangeTimeElapsed < this.AlphaChangeDuration)
				{
					this._currentAlpha = MathF.Lerp(this._initialAlpha, this._targetAlpha, this._alphaChangeTimeElapsed / this.AlphaChangeDuration, 1E-05f);
					ListPanel leftSideFormations = this.LeftSideFormations;
					if (leftSideFormations != null)
					{
						leftSideFormations.SetGlobalAlphaRecursively(this._currentAlpha);
					}
					ListPanel rightSideFormations = this.RightSideFormations;
					if (rightSideFormations != null)
					{
						rightSideFormations.SetGlobalAlphaRecursively(this._currentAlpha);
					}
					ListPanel commanderPool = this.CommanderPool;
					if (commanderPool != null)
					{
						commanderPool.SetGlobalAlphaRecursively(this._currentAlpha);
					}
					Widget markers = this.Markers;
					if (markers != null)
					{
						markers.SetGlobalAlphaRecursively(this._currentAlpha);
					}
					this._alphaChangeTimeElapsed += dt;
					return;
				}
				this._isTransitioning = false;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000210D0 File Offset: 0x0001F2D0
		protected void OnCameraControlsEnabledChanged()
		{
			this._alphaChangeTimeElapsed = 0f;
			this._targetAlpha = (this.AreCameraControlsEnabled ? this.CameraEnabledAlpha : 1f);
			this._initialAlpha = this._currentAlpha;
			this._isTransitioning = true;
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0002110B File Offset: 0x0001F30B
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00021113 File Offset: 0x0001F313
		[Editor(false)]
		public bool AreCameraControlsEnabled
		{
			get
			{
				return this._areCameraControlsEnabled;
			}
			set
			{
				if (value != this._areCameraControlsEnabled)
				{
					this._areCameraControlsEnabled = value;
					base.OnPropertyChanged(value, "AreCameraControlsEnabled");
					this.OnCameraControlsEnabledChanged();
				}
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00021137 File Offset: 0x0001F337
		// (set) Token: 0x06000C04 RID: 3076 RVA: 0x0002113F File Offset: 0x0001F33F
		[Editor(false)]
		public float CameraEnabledAlpha
		{
			get
			{
				return this._cameraEnabledAlpha;
			}
			set
			{
				if (value != this._cameraEnabledAlpha)
				{
					this._cameraEnabledAlpha = value;
					base.OnPropertyChanged(value, "CameraEnabledAlpha");
				}
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002115D File Offset: 0x0001F35D
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x00021165 File Offset: 0x0001F365
		[Editor(false)]
		public ListPanel LeftSideFormations
		{
			get
			{
				return this._leftSideFormations;
			}
			set
			{
				if (value != this._leftSideFormations)
				{
					this._leftSideFormations = value;
					base.OnPropertyChanged<ListPanel>(value, "LeftSideFormations");
				}
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x00021183 File Offset: 0x0001F383
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0002118B File Offset: 0x0001F38B
		[Editor(false)]
		public ListPanel RightSideFormations
		{
			get
			{
				return this._rightSideFormations;
			}
			set
			{
				if (value != this._rightSideFormations)
				{
					this._rightSideFormations = value;
					base.OnPropertyChanged<ListPanel>(value, "RightSideFormations");
				}
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000211A9 File Offset: 0x0001F3A9
		// (set) Token: 0x06000C0A RID: 3082 RVA: 0x000211B1 File Offset: 0x0001F3B1
		[Editor(false)]
		public ListPanel CommanderPool
		{
			get
			{
				return this._commanderPool;
			}
			set
			{
				if (value != this._commanderPool)
				{
					this._commanderPool = value;
					base.OnPropertyChanged<ListPanel>(value, "CommanderPool");
				}
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x000211CF File Offset: 0x0001F3CF
		// (set) Token: 0x06000C0C RID: 3084 RVA: 0x000211D7 File Offset: 0x0001F3D7
		[Editor(false)]
		public Widget Markers
		{
			get
			{
				return this._markers;
			}
			set
			{
				if (value != this._markers)
				{
					this._markers = value;
					base.OnPropertyChanged<Widget>(value, "Markers");
				}
			}
		}

		// Token: 0x04000571 RID: 1393
		private float _alphaChangeTimeElapsed;

		// Token: 0x04000572 RID: 1394
		private float _initialAlpha = 1f;

		// Token: 0x04000573 RID: 1395
		private float _targetAlpha;

		// Token: 0x04000574 RID: 1396
		private float _currentAlpha = 1f;

		// Token: 0x04000575 RID: 1397
		private bool _isTransitioning;

		// Token: 0x04000576 RID: 1398
		private bool _areCameraControlsEnabled;

		// Token: 0x04000577 RID: 1399
		private float _cameraEnabledAlpha = 0.2f;

		// Token: 0x04000578 RID: 1400
		private ListPanel _leftSideFormations;

		// Token: 0x04000579 RID: 1401
		private ListPanel _rightSideFormations;

		// Token: 0x0400057A RID: 1402
		private ListPanel _commanderPool;

		// Token: 0x0400057B RID: 1403
		private Widget _markers;
	}
}
