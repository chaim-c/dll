using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission
{
	// Token: 0x020000CC RID: 204
	public class AgentAmmoTextWidget : TextWidget
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0001DD58 File Offset: 0x0001BF58
		public AgentAmmoTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0001DD61 File Offset: 0x0001BF61
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.IsAlertEnabled)
			{
				this.SetState("Alert");
				return;
			}
			this.SetState("Default");
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001DD89 File Offset: 0x0001BF89
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x0001DD91 File Offset: 0x0001BF91
		public bool IsAlertEnabled
		{
			get
			{
				return this._isAlertEnabled;
			}
			set
			{
				if (this._isAlertEnabled != value)
				{
					this._isAlertEnabled = value;
					base.OnPropertyChanged(value, "IsAlertEnabled");
				}
			}
		}

		// Token: 0x040004D0 RID: 1232
		private bool _isAlertEnabled;
	}
}
