using System;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x0200006B RID: 107
	public class SelectedStateBrushWidget : BrushWidget
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
		public SelectedStateBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001E7FF File Offset: 0x0001C9FF
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!this._isBrushStatesRegistered)
			{
				this.RegisterBrushStatesOfWidget();
				this._isBrushStatesRegistered = true;
			}
			if (this._isDirty)
			{
				this.SetState(this.SelectedState);
				this._isDirty = false;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001E838 File Offset: 0x0001CA38
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001E840 File Offset: 0x0001CA40
		[Editor(false)]
		public string SelectedState
		{
			get
			{
				return this._selectedState;
			}
			set
			{
				if (this._selectedState != value)
				{
					this._selectedState = value;
					base.OnPropertyChanged<string>(value, "SelectedState");
					this._isDirty = true;
				}
			}
		}

		// Token: 0x04000344 RID: 836
		private bool _isDirty = true;

		// Token: 0x04000345 RID: 837
		private bool _isBrushStatesRegistered;

		// Token: 0x04000346 RID: 838
		private string _selectedState = "Default";
	}
}
