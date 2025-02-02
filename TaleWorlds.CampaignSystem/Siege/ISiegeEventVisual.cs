using System;

namespace TaleWorlds.CampaignSystem.Siege
{
	// Token: 0x02000286 RID: 646
	public interface ISiegeEventVisual
	{
		// Token: 0x060022BD RID: 8893
		void Initialize();

		// Token: 0x060022BE RID: 8894
		void OnSiegeEventEnd();

		// Token: 0x060022BF RID: 8895
		void Tick();
	}
}
