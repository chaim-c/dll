using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Kingdom
{
	// Token: 0x02000124 RID: 292
	public class KingdomDecisionFactionTypeVisualBrushWidget : BrushWidget
	{
		// Token: 0x06000F12 RID: 3858 RVA: 0x00029D8D File Offset: 0x00027F8D
		public KingdomDecisionFactionTypeVisualBrushWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00029DA1 File Offset: 0x00027FA1
		private void SetVisualState(string type)
		{
			this.RegisterBrushStatesOfWidget();
			this.SetState(type);
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00029DB0 File Offset: 0x00027FB0
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00029DB8 File Offset: 0x00027FB8
		[Editor(false)]
		public string FactionName
		{
			get
			{
				return this._factionName;
			}
			set
			{
				if (this._factionName != value)
				{
					this._factionName = value;
					base.OnPropertyChanged<string>(value, "FactionName");
					if (value != null)
					{
						this.SetVisualState(value);
					}
				}
			}
		}

		// Token: 0x040006EA RID: 1770
		private string _factionName = "";
	}
}
