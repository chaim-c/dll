using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000067 RID: 103
	public class PartyUpgradesContainerWidget : Widget
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		public PartyUpgradesContainerWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		private void OnAnyUpgradeHasRequirementChanged(bool value)
		{
			base.ScaledPositionYOffset = (value ? 0f : 8f);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00010BE7 File Offset: 0x0000EDE7
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00010BEF File Offset: 0x0000EDEF
		[Editor(false)]
		public bool AnyUpgradeHasRequirement
		{
			get
			{
				return this._anyUpgradeHasRequirement;
			}
			set
			{
				if (this._anyUpgradeHasRequirement != value)
				{
					this._anyUpgradeHasRequirement = value;
					this.OnAnyUpgradeHasRequirementChanged(value);
					base.OnPropertyChanged(value, "AnyUpgradeHasRequirement");
				}
			}
		}

		// Token: 0x04000265 RID: 613
		private const float _noRequirementOffset = 8f;

		// Token: 0x04000266 RID: 614
		private bool _anyUpgradeHasRequirement = true;
	}
}
