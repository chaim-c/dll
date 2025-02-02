using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000CF RID: 207
	public class AgentWeaponPassiveUsageVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x0001E1E9 File Offset: 0x0001C3E9
		public AgentWeaponPassiveUsageVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0001E1FC File Offset: 0x0001C3FC
		private void UpdateVisualState()
		{
			if (this._firstUpdate)
			{
				this.RegisterBrushStatesOfWidget();
				this._firstUpdate = false;
			}
			switch (this.CouchLanceState)
			{
			case 0:
				base.IsVisible = false;
				return;
			case 1:
				base.IsVisible = true;
				this.SetState("ConditionsNotMet");
				return;
			case 2:
				base.IsVisible = true;
				this.SetState("Possible");
				return;
			case 3:
				this.SetState("Active");
				base.IsVisible = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001E27C File Offset: 0x0001C47C
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0001E284 File Offset: 0x0001C484
		[Editor(false)]
		public int CouchLanceState
		{
			get
			{
				return this._couchLanceState;
			}
			set
			{
				if (this._couchLanceState != value)
				{
					this._couchLanceState = value;
					base.OnPropertyChanged(value, "CouchLanceState");
					this.UpdateVisualState();
				}
			}
		}

		// Token: 0x040004DD RID: 1245
		private bool _firstUpdate;

		// Token: 0x040004DE RID: 1246
		private int _couchLanceState = -1;
	}
}
