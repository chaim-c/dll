using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.Siege
{
	// Token: 0x02000285 RID: 645
	public interface ISiegeEventSide
	{
		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060022AD RID: 8877
		SiegeEvent SiegeEvent { get; }

		// Token: 0x060022AE RID: 8878
		IEnumerable<PartyBase> GetInvolvedPartiesForEventType(MapEvent.BattleTypes mapEventType = MapEvent.BattleTypes.Siege);

		// Token: 0x060022AF RID: 8879
		PartyBase GetNextInvolvedPartyForEventType(ref int partyIndex, MapEvent.BattleTypes mapEventType = MapEvent.BattleTypes.Siege);

		// Token: 0x060022B0 RID: 8880
		bool HasInvolvedPartyForEventType(PartyBase party, MapEvent.BattleTypes mapEventType = MapEvent.BattleTypes.Siege);

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060022B1 RID: 8881
		SiegeStrategy SiegeStrategy { get; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060022B2 RID: 8882
		BattleSideEnum BattleSide { get; }

		// Token: 0x060022B3 RID: 8883
		void OnTroopsKilledOnSide(int killCount);

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060022B4 RID: 8884
		int NumberOfTroopsKilledOnSide { get; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060022B5 RID: 8885
		SiegeEvent.SiegeEnginesContainer SiegeEngines { get; }

		// Token: 0x060022B6 RID: 8886
		void AddSiegeEngineMissile(SiegeEvent.SiegeEngineMissile missile);

		// Token: 0x060022B7 RID: 8887
		void RemoveDeprecatedMissiles();

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060022B8 RID: 8888
		MBReadOnlyList<SiegeEvent.SiegeEngineMissile> SiegeEngineMissiles { get; }

		// Token: 0x060022B9 RID: 8889
		void SetSiegeStrategy(SiegeStrategy strategy);

		// Token: 0x060022BA RID: 8890
		void InitializeSiegeEventSide();

		// Token: 0x060022BB RID: 8891
		void GetAttackTarget(ISiegeEventSide siegeEventSide, SiegeEngineType siegeEngine, int siegeEngineSlot, out SiegeBombardTargets targetType, out int targetIndex);

		// Token: 0x060022BC RID: 8892
		void FinalizeSiegeEvent();
	}
}
