using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Credits
{
	// Token: 0x02000154 RID: 340
	public class CreditsWidget : Widget
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x00031B32 File Offset: 0x0002FD32
		public CreditsWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00031B48 File Offset: 0x0002FD48
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.RootItemWidget != null)
			{
				this.RootItemWidget.PositionYOffset = this._currentOffset;
				this._currentOffset -= dt * 75f;
				if (this._currentOffset < -this.RootItemWidget.Size.Y * base._inverseScaleToUse)
				{
					this._currentOffset = 1080f;
				}
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00031BB4 File Offset: 0x0002FDB4
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x00031BBC File Offset: 0x0002FDBC
		[Editor(false)]
		public Widget RootItemWidget
		{
			get
			{
				return this._rootItemWidget;
			}
			set
			{
				if (this._rootItemWidget != value)
				{
					this._rootItemWidget = value;
					base.OnPropertyChanged<Widget>(value, "RootItemWidget");
				}
			}
		}

		// Token: 0x04000832 RID: 2098
		private float _currentOffset = 1080f;

		// Token: 0x04000833 RID: 2099
		private Widget _rootItemWidget;
	}
}
