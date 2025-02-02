using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000126 RID: 294
	public class KingdomDecisionPopupWidget : Widget
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0002A091 File Offset: 0x00028291
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0002A099 File Offset: 0x00028299
		public int DelayAfterKingsDecision { get; set; } = 5;

		// Token: 0x06000F34 RID: 3892 RVA: 0x0002A0A2 File Offset: 0x000282A2
		public KingdomDecisionPopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x0002A0BD File Offset: 0x000282BD
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._kingDecisionDoneTime != -1f && base.EventManager.Time - this._kingDecisionDoneTime > (float)this.DelayAfterKingsDecision)
			{
				this.ExecuteFinalDone();
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0002A0F4 File Offset: 0x000282F4
		private void ExecuteFinalDone()
		{
			base.EventFired("FinalDone", Array.Empty<object>());
			this._kingDecisionDoneTime = -1f;
			using (IEnumerator<Widget> enumerator = base.AllChildren.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KingdomDecisionOptionWidget kingdomDecisionOptionWidget;
					if ((kingdomDecisionOptionWidget = (enumerator.Current as KingdomDecisionOptionWidget)) != null)
					{
						kingdomDecisionOptionWidget.OnFinalDone();
					}
				}
			}
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0002A164 File Offset: 0x00028364
		private void OnKingsDecisionDone()
		{
			this._kingDecisionDoneTime = base.EventManager.Time;
			using (IEnumerator<Widget> enumerator = base.AllChildren.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KingdomDecisionOptionWidget kingdomDecisionOptionWidget;
					if ((kingdomDecisionOptionWidget = (enumerator.Current as KingdomDecisionOptionWidget)) != null)
					{
						kingdomDecisionOptionWidget.OnKingsDecisionDone();
					}
				}
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0002A1CC File Offset: 0x000283CC
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0002A1D4 File Offset: 0x000283D4
		[Editor(false)]
		public bool IsKingsDecisionDone
		{
			get
			{
				return this._isKingsDecisionDone;
			}
			set
			{
				if (this._isKingsDecisionDone != value)
				{
					this._isKingsDecisionDone = value;
					base.OnPropertyChanged(value, "IsKingsDecisionDone");
					if (value)
					{
						this.OnKingsDecisionDone();
					}
				}
			}
		}

		// Token: 0x040006F9 RID: 1785
		private float _kingDecisionDoneTime = -1f;

		// Token: 0x040006FA RID: 1786
		private bool _isKingsDecisionDone;
	}
}
