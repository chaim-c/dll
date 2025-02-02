using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200001B RID: 27
	public class FiefProfitTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00005A25 File Offset: 0x00003C25
		public FiefProfitTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005A35 File Offset: 0x00003C35
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

		// Token: 0x0600014B RID: 331 RVA: 0x00005A60 File Offset: 0x00003C60
		private void UpdateVisual(int type)
		{
			switch (type)
			{
			case 0:
				this.SetState("None");
				return;
			case 1:
				this.SetState("Tax");
				return;
			case 2:
				this.SetState("Tariff");
				return;
			case 3:
				this.SetState("Garrison");
				return;
			case 4:
				this.SetState("Village");
				return;
			case 5:
				this.SetState("Governor");
				return;
			default:
				return;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00005AD3 File Offset: 0x00003CD3
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00005ADB File Offset: 0x00003CDB
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

		// Token: 0x0400009B RID: 155
		private bool _determinedVisual;

		// Token: 0x0400009C RID: 156
		private int _type = -1;
	}
}
