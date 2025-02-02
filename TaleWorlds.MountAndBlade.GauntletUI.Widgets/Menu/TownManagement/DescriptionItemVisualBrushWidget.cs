using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000F9 RID: 249
	public class DescriptionItemVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x000244CA File Offset: 0x000226CA
		public DescriptionItemVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x000244DA File Offset: 0x000226DA
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._determinedVisual)
			{
				this.RegisterBrushStatesOfWidget();
				this.UpdateVisual(this.Type);
				this._determinedVisual = true;
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00024504 File Offset: 0x00022704
		private void UpdateVisual(int type)
		{
			switch (type)
			{
			case 0:
				this.SetState("Gold");
				return;
			case 1:
				this.SetState("Production");
				return;
			case 2:
				this.SetState("Militia");
				return;
			case 3:
				this.SetState("Prosperity");
				return;
			case 4:
				this.SetState("Food");
				return;
			case 5:
				this.SetState("Loyalty");
				return;
			case 6:
				this.SetState("Security");
				return;
			case 7:
				this.SetState("Garrison");
				return;
			default:
				return;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00024597 File Offset: 0x00022797
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0002459F File Offset: 0x0002279F
		[Editor(false)]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (this._type != value)
				{
					this._type = value;
					base.OnPropertyChanged(value, "Type");
				}
			}
		}

		// Token: 0x04000607 RID: 1543
		private bool _determinedVisual;

		// Token: 0x04000608 RID: 1544
		private int _type = -1;
	}
}
