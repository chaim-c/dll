using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000D8 RID: 216
	public class MissionSiegeEngineMarkerWidget : Widget
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001F949 File Offset: 0x0001DB49
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0001F951 File Offset: 0x0001DB51
		public SliderWidget Slider { get; set; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0001F95A File Offset: 0x0001DB5A
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0001F962 File Offset: 0x0001DB62
		public BrushWidget MachineIconParent { get; set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0001F96B File Offset: 0x0001DB6B
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0001F973 File Offset: 0x0001DB73
		public Brush EnemyBrush { get; set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0001F97C File Offset: 0x0001DB7C
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0001F984 File Offset: 0x0001DB84
		public Brush AllyBrush { get; set; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0001F98D File Offset: 0x0001DB8D
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0001F995 File Offset: 0x0001DB95
		public Vec2 ScreenPosition { get; set; }

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		public MissionSiegeEngineMarkerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001F9F4 File Offset: 0x0001DBF4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			base.ScaledPositionXOffset = this.ScreenPosition.x - base.Size.X / 2f;
			base.ScaledPositionYOffset = this.ScreenPosition.y;
			float valueTo = this.IsActive ? 0.65f : 0f;
			float alphaFactor = MathF.Lerp(base.AlphaFactor, valueTo, dt * 10f, 1E-05f);
			this.SetGlobalAlphaRecursively(alphaFactor);
			if (!this._isBrushChanged)
			{
				this.MachineIconParent.Brush = (this.IsEnemy ? this.EnemyBrush : this.AllyBrush);
				this._isBrushChanged = true;
			}
			this.UpdateColorOfSlider();
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001FAA8 File Offset: 0x0001DCA8
		private void SetMachineTypeIcon(string machineType)
		{
			string name = "SPGeneral\\MapSiege\\" + machineType;
			this.MachineTypeIconWidget.Sprite = base.Context.SpriteData.GetSprite(name);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001FAE0 File Offset: 0x0001DCE0
		private void UpdateColorOfSlider()
		{
			(this.Slider.Filler as BrushWidget).Brush.Color = Color.Lerp(this._emptyColor, this._fullColor, this.Slider.ValueFloat / this.Slider.MaxValueFloat);
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001FB2F File Offset: 0x0001DD2F
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x0001FB37 File Offset: 0x0001DD37
		public bool IsEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				if (this._isEnemy != value)
				{
					this._isEnemy = value;
				}
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0001FB49 File Offset: 0x0001DD49
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x0001FB51 File Offset: 0x0001DD51
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
				}
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0001FB63 File Offset: 0x0001DD63
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0001FB6B File Offset: 0x0001DD6B
		public string EngineType
		{
			get
			{
				return this._engineType;
			}
			set
			{
				if (this._engineType != value)
				{
					this._engineType = value;
					this.SetMachineTypeIcon(value);
				}
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0001FB89 File Offset: 0x0001DD89
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0001FB91 File Offset: 0x0001DD91
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

		// Token: 0x0400051C RID: 1308
		private Color _fullColor = new Color(0.2784314f, 0.9882353f, 0.44313726f, 1f);

		// Token: 0x0400051D RID: 1309
		private Color _emptyColor = new Color(0.9882353f, 0.2784314f, 0.2784314f, 1f);

		// Token: 0x04000523 RID: 1315
		private bool _isBrushChanged;

		// Token: 0x04000524 RID: 1316
		private bool _isEnemy;

		// Token: 0x04000525 RID: 1317
		private bool _isActive;

		// Token: 0x04000526 RID: 1318
		private Widget _machineTypeIconWidget;

		// Token: 0x04000527 RID: 1319
		private string _engineType;
	}
}
