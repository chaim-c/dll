using System;

namespace TaleWorlds.Library.Information
{
	// Token: 0x020000A7 RID: 167
	public class TooltipTriggerVM : ViewModel
	{
		// Token: 0x0600061E RID: 1566 RVA: 0x0001408C File Offset: 0x0001228C
		public TooltipTriggerVM(Type linkedTooltipType, params object[] args)
		{
			this._linkedTooltipType = linkedTooltipType;
			this._args = args;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000140A2 File Offset: 0x000122A2
		public void ExecuteBeginHint()
		{
			InformationManager.ShowTooltip(this._linkedTooltipType, this._args);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000140B5 File Offset: 0x000122B5
		public void ExecuteEndHint()
		{
			InformationManager.HideTooltip();
		}

		// Token: 0x040001C7 RID: 455
		private Type _linkedTooltipType;

		// Token: 0x040001C8 RID: 456
		private object[] _args;
	}
}
