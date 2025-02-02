using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000020 RID: 32
	public class GamepadCursorParentWidget : Widget
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00006672 File Offset: 0x00004872
		public GamepadCursorParentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000667C File Offset: 0x0000487C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			this.CenterWidget.SetGlobalAlphaRecursively(MathF.Lerp(this.CenterWidget.AlphaFactor, this.HasTarget ? 0.67f : 1f, 0.16f, 1E-05f));
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000066C9 File Offset: 0x000048C9
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000066D1 File Offset: 0x000048D1
		public float XOffset
		{
			get
			{
				return this._xOffset;
			}
			set
			{
				if (value != this._xOffset)
				{
					this._xOffset = value;
					base.OnPropertyChanged(value, "XOffset");
					this.CenterWidget.ScaledPositionXOffset = value;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000066FB File Offset: 0x000048FB
		// (set) Token: 0x06000195 RID: 405 RVA: 0x00006703 File Offset: 0x00004903
		public float YOffset
		{
			get
			{
				return this._yOffset;
			}
			set
			{
				if (value != this._yOffset)
				{
					this._yOffset = value;
					base.OnPropertyChanged(value, "YOffset");
					this.CenterWidget.ScaledPositionYOffset = value;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000672D File Offset: 0x0000492D
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00006735 File Offset: 0x00004935
		public bool HasTarget
		{
			get
			{
				return this._hasTarget;
			}
			set
			{
				if (value != this._hasTarget)
				{
					this._hasTarget = value;
					base.OnPropertyChanged(value, "HasTarget");
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006753 File Offset: 0x00004953
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000675B File Offset: 0x0000495B
		public BrushWidget CenterWidget
		{
			get
			{
				return this._centerWidget;
			}
			set
			{
				if (value != this._centerWidget)
				{
					this._centerWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "CenterWidget");
				}
			}
		}

		// Token: 0x040000BC RID: 188
		private float _xOffset;

		// Token: 0x040000BD RID: 189
		private float _yOffset;

		// Token: 0x040000BE RID: 190
		private bool _hasTarget;

		// Token: 0x040000BF RID: 191
		private BrushWidget _centerWidget;
	}
}
