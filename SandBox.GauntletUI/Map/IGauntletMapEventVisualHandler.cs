using System;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002B RID: 43
	public interface IGauntletMapEventVisualHandler
	{
		// Token: 0x06000198 RID: 408
		void OnNewEventStarted(GauntletMapEventVisual newEvent);

		// Token: 0x06000199 RID: 409
		void OnInitialized(GauntletMapEventVisual newEvent);

		// Token: 0x0600019A RID: 410
		void OnEventEnded(GauntletMapEventVisual newEvent);

		// Token: 0x0600019B RID: 411
		void OnEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent);
	}
}
