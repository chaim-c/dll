using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200001C RID: 28
	public class FiefStatTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00005AF9 File Offset: 0x00003CF9
		public FiefStatTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005B09 File Offset: 0x00003D09
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

		// Token: 0x06000150 RID: 336 RVA: 0x00005B34 File Offset: 0x00003D34
		private void UpdateVisual(int type)
		{
			switch (type)
			{
			case 0:
				this.SetState("None");
				return;
			case 1:
				this.SetState("Wall");
				return;
			case 2:
				this.SetState("Garrison");
				return;
			case 3:
				this.SetState("Militia");
				return;
			case 4:
				this.SetState("Prosperity");
				return;
			case 5:
				this.SetState("Food");
				return;
			case 6:
				this.SetState("Loyalty");
				return;
			case 7:
				this.SetState("Security");
				return;
			default:
				return;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005BC7 File Offset: 0x00003DC7
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00005BCF File Offset: 0x00003DCF
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

		// Token: 0x0400009D RID: 157
		private bool _determinedVisual;

		// Token: 0x0400009E RID: 158
		private int _type = -1;
	}
}
