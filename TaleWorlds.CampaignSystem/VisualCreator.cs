using System;
using TaleWorlds.CampaignSystem.MapEvents;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x020000A2 RID: 162
	public class VisualCreator
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060011A9 RID: 4521 RVA: 0x00051D3C File Offset: 0x0004FF3C
		// (set) Token: 0x060011AA RID: 4522 RVA: 0x00051D44 File Offset: 0x0004FF44
		public IMapEventVisualCreator MapEventVisualCreator { get; set; }

		// Token: 0x060011AB RID: 4523 RVA: 0x00051D4D File Offset: 0x0004FF4D
		public IMapEventVisual CreateMapEventVisual(MapEvent mapEvent)
		{
			IMapEventVisualCreator mapEventVisualCreator = this.MapEventVisualCreator;
			if (mapEventVisualCreator == null)
			{
				return null;
			}
			return mapEventVisualCreator.CreateMapEventVisual(mapEvent);
		}
	}
}
