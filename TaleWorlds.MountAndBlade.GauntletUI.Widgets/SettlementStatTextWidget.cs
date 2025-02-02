using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x0200003C RID: 60
	public class SettlementStatTextWidget : TextWidget
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000AB09 File Offset: 0x00008D09
		public SettlementStatTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000AB14 File Offset: 0x00008D14
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			switch (this._state)
			{
			case SettlementStatTextWidget.State.Idle:
				if (this.IsWarning)
				{
					this.SetState("Warning");
				}
				else
				{
					this.SetState("Default");
				}
				this._state = SettlementStatTextWidget.State.Start;
				return;
			case SettlementStatTextWidget.State.Start:
				this._state = ((base.BrushRenderer.Brush != null) ? SettlementStatTextWidget.State.Playing : SettlementStatTextWidget.State.Start);
				return;
			case SettlementStatTextWidget.State.Playing:
				base.BrushRenderer.RestartAnimation();
				this._state = SettlementStatTextWidget.State.End;
				break;
			case SettlementStatTextWidget.State.End:
				break;
			default:
				return;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000AB98 File Offset: 0x00008D98
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		[Editor(false)]
		public bool IsWarning
		{
			get
			{
				return this._isWarning;
			}
			set
			{
				if (value != this._isWarning)
				{
					this._isWarning = value;
					base.OnPropertyChanged(value, "IsWarning");
					this.SetState(this._isWarning ? "Warning" : "Default");
				}
			}
		}

		// Token: 0x04000165 RID: 357
		private SettlementStatTextWidget.State _state;

		// Token: 0x04000166 RID: 358
		private bool _isWarning;

		// Token: 0x02000193 RID: 403
		public enum State
		{
			// Token: 0x04000962 RID: 2402
			Idle,
			// Token: 0x04000963 RID: 2403
			Start,
			// Token: 0x04000964 RID: 2404
			Playing,
			// Token: 0x04000965 RID: 2405
			End
		}
	}
}
