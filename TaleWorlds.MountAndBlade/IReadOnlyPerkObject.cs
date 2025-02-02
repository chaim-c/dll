using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000305 RID: 773
	public interface IReadOnlyPerkObject
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060029FE RID: 10750
		TextObject Name { get; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060029FF RID: 10751
		TextObject Description { get; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002A00 RID: 10752
		List<string> GameModes { get; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06002A01 RID: 10753
		int PerkListIndex { get; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002A02 RID: 10754
		string IconId { get; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06002A03 RID: 10755
		string HeroIdleAnimOverride { get; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002A04 RID: 10756
		string HeroMountIdleAnimOverride { get; }

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002A05 RID: 10757
		string TroopIdleAnimOverride { get; }

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002A06 RID: 10758
		string TroopMountIdleAnimOverride { get; }

		// Token: 0x06002A07 RID: 10759
		int GetExtraTroopCount(bool isWarmup);

		// Token: 0x06002A08 RID: 10760
		List<ValueTuple<EquipmentIndex, EquipmentElement>> GetAlternativeEquipments(bool isWarmup, bool isPlayer, List<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments, bool getAllEquipments = false);

		// Token: 0x06002A09 RID: 10761
		float GetDrivenPropertyBonusOnSpawn(bool isWarmup, bool isPlayer, DrivenProperty drivenProperty, float baseValue);

		// Token: 0x06002A0A RID: 10762
		float GetHitpoints(bool isWarmup, bool isPlayer);

		// Token: 0x06002A0B RID: 10763
		MPPerkObject Clone(MissionPeer peer);
	}
}
