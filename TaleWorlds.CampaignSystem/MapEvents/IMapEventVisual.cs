using System;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.MapEvents
{
	// Token: 0x020002C1 RID: 705
	public interface IMapEventVisual
	{
		// Token: 0x060029B8 RID: 10680
		void Initialize(Vec2 position, int battleSizeValue, bool hasSound, bool isVisible);

		// Token: 0x060029B9 RID: 10681
		void OnMapEventEnd();

		// Token: 0x060029BA RID: 10682
		void SetVisibility(bool isVisible);
	}
}
