using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Siege
{
	// Token: 0x0200010C RID: 268
	public class MapSiegeMachineButtonWidget : ButtonWidget
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0002712F File Offset: 0x0002532F
		public MapSiegeMachineButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002714D File Offset: 0x0002534D
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.ColoredImageWidget != null && !this._machineSpritesUpdated)
			{
				this.SetStylesSprite(this.ColoredImageWidget, "SPGeneral\\Siege\\" + this.MachineID);
				this._machineSpritesUpdated = true;
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x00027189 File Offset: 0x00025389
		private void SetStylesSprite(Widget widget, string spriteName)
		{
			widget.Sprite = base.Context.SpriteData.GetSprite(spriteName);
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000271A2 File Offset: 0x000253A2
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x000271AA File Offset: 0x000253AA
		[Editor(false)]
		public Widget ColoredImageWidget
		{
			get
			{
				return this._coloredImageWidget;
			}
			set
			{
				if (value != this._coloredImageWidget)
				{
					this._coloredImageWidget = value;
					base.OnPropertyChanged<Widget>(value, "ColoredImageWidget");
				}
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x000271C8 File Offset: 0x000253C8
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x000271D0 File Offset: 0x000253D0
		[Editor(false)]
		public bool IsDeploymentTarget
		{
			get
			{
				return this._isDeploymentTarget;
			}
			set
			{
				if (value != this._isDeploymentTarget)
				{
					this._isDeploymentTarget = value;
					base.OnPropertyChanged(value, "IsDeploymentTarget");
				}
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x000271EE File Offset: 0x000253EE
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x000271F6 File Offset: 0x000253F6
		[Editor(false)]
		public string MachineID
		{
			get
			{
				return this._machineID;
			}
			set
			{
				if (value != this._machineID)
				{
					this._machineID = value;
					base.OnPropertyChanged<string>(value, "MachineID");
					this._machineSpritesUpdated = false;
				}
			}
		}

		// Token: 0x0400067C RID: 1660
		private Vec2 _orgClipSize = new Vec2(-1f, -1f);

		// Token: 0x0400067D RID: 1661
		private bool _machineSpritesUpdated;

		// Token: 0x0400067E RID: 1662
		private Widget _coloredImageWidget;

		// Token: 0x0400067F RID: 1663
		private bool _isDeploymentTarget;

		// Token: 0x04000680 RID: 1664
		private string _machineID;
	}
}
