using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.BarterSystem.Barterables;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.BarterBehaviors
{
	// Token: 0x020003FB RID: 1019
	public class ItemBarterBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003ECD RID: 16077 RVA: 0x00134609 File Offset: 0x00132809
		public override void RegisterEvents()
		{
			CampaignEvents.BarterablesRequested.AddNonSerializedListener(this, new Action<BarterData>(this.CheckForBarters));
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x00134622 File Offset: 0x00132822
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x00134624 File Offset: 0x00132824
		public void CheckForBarters(BarterData args)
		{
			Vec2 asVec;
			if (args.OffererHero != null)
			{
				asVec = args.OffererHero.GetPosition().AsVec2;
			}
			else if (args.OffererParty != null)
			{
				asVec = args.OffererParty.MobileParty.GetPosition().AsVec2;
			}
			else
			{
				asVec = args.OtherHero.GetPosition().AsVec2;
			}
			List<Settlement> closestSettlements = this._distanceCache.GetClosestSettlements(asVec);
			if (args.OffererParty != null && args.OtherParty != null)
			{
				for (int i = 0; i < args.OffererParty.ItemRoster.Count; i++)
				{
					ItemRosterElement elementCopyAtIndex = args.OffererParty.ItemRoster.GetElementCopyAtIndex(i);
					if (elementCopyAtIndex.Amount > 0 && elementCopyAtIndex.EquipmentElement.GetBaseValue() > 100)
					{
						int averageValueOfItemInNearbySettlements = this.CalculateAverageItemValueInNearbySettlements(elementCopyAtIndex.EquipmentElement, args.OffererParty, closestSettlements);
						Barterable barterable = new ItemBarterable(args.OffererHero, args.OtherHero, args.OffererParty, args.OtherParty, elementCopyAtIndex, averageValueOfItemInNearbySettlements);
						args.AddBarterable<ItemBarterGroup>(barterable, false);
					}
				}
				for (int j = 0; j < args.OtherParty.ItemRoster.Count; j++)
				{
					ItemRosterElement elementCopyAtIndex2 = args.OtherParty.ItemRoster.GetElementCopyAtIndex(j);
					if (elementCopyAtIndex2.Amount > 0 && elementCopyAtIndex2.EquipmentElement.GetBaseValue() > 100)
					{
						int averageValueOfItemInNearbySettlements2 = this.CalculateAverageItemValueInNearbySettlements(elementCopyAtIndex2.EquipmentElement, args.OtherParty, closestSettlements);
						Barterable barterable2 = new ItemBarterable(args.OtherHero, args.OffererHero, args.OtherParty, args.OffererParty, elementCopyAtIndex2, averageValueOfItemInNearbySettlements2);
						args.AddBarterable<ItemBarterGroup>(barterable2, false);
					}
				}
			}
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x001347D0 File Offset: 0x001329D0
		private int CalculateAverageItemValueInNearbySettlements(EquipmentElement itemRosterElement, PartyBase involvedParty, List<Settlement> nearbySettlements)
		{
			int num = 0;
			if (!nearbySettlements.IsEmpty<Settlement>())
			{
				foreach (Settlement settlement in nearbySettlements)
				{
					num += settlement.Town.GetItemPrice(itemRosterElement, involvedParty.MobileParty, true);
				}
				num /= nearbySettlements.Count;
			}
			return num;
		}

		// Token: 0x0400125D RID: 4701
		private const int ItemValueThreshold = 100;

		// Token: 0x0400125E RID: 4702
		private ItemBarterBehavior.SettlementDistanceCache _distanceCache = new ItemBarterBehavior.SettlementDistanceCache();

		// Token: 0x0200075B RID: 1883
		private class SettlementDistanceCache
		{
			// Token: 0x060059E8 RID: 23016 RVA: 0x00184948 File Offset: 0x00182B48
			public SettlementDistanceCache()
			{
				this._latestHeroPosition = new Vec2(-1f, -1f);
				this._sortedSettlements = new List<ItemBarterBehavior.SettlementDistanceCache.SettlementDistancePair>(64);
				this._closestSettlements = new List<Settlement>(3);
			}

			// Token: 0x060059E9 RID: 23017 RVA: 0x00184980 File Offset: 0x00182B80
			public List<Settlement> GetClosestSettlements(Vec2 position)
			{
				if (!position.NearlyEquals(this._latestHeroPosition, 1E-05f))
				{
					this._latestHeroPosition = position;
					MBReadOnlyList<Settlement> all = Settlement.All;
					int count = all.Count;
					for (int i = 0; i < count; i++)
					{
						Settlement settlement = all[i];
						if (settlement.IsTown)
						{
							this._sortedSettlements.Add(new ItemBarterBehavior.SettlementDistanceCache.SettlementDistancePair(position.DistanceSquared(settlement.GatePosition), settlement));
						}
					}
					this._sortedSettlements.Sort();
					this._closestSettlements.Clear();
					this._closestSettlements.Add(this._sortedSettlements[0].Settlement);
					this._closestSettlements.Add(this._sortedSettlements[1].Settlement);
					this._closestSettlements.Add(this._sortedSettlements[2].Settlement);
					this._sortedSettlements.Clear();
				}
				return this._closestSettlements;
			}

			// Token: 0x04001EDD RID: 7901
			private Vec2 _latestHeroPosition;

			// Token: 0x04001EDE RID: 7902
			private List<ItemBarterBehavior.SettlementDistanceCache.SettlementDistancePair> _sortedSettlements;

			// Token: 0x04001EDF RID: 7903
			private List<Settlement> _closestSettlements;

			// Token: 0x020007D5 RID: 2005
			private struct SettlementDistancePair : IComparable<ItemBarterBehavior.SettlementDistanceCache.SettlementDistancePair>
			{
				// Token: 0x06005B54 RID: 23380 RVA: 0x00186793 File Offset: 0x00184993
				public SettlementDistancePair(float distance, Settlement settlement)
				{
					this._distance = distance;
					this.Settlement = settlement;
				}

				// Token: 0x06005B55 RID: 23381 RVA: 0x001867A3 File Offset: 0x001849A3
				public int CompareTo(ItemBarterBehavior.SettlementDistanceCache.SettlementDistancePair other)
				{
					if (this._distance == other._distance)
					{
						return 0;
					}
					if (this._distance > other._distance)
					{
						return 1;
					}
					return -1;
				}

				// Token: 0x04002034 RID: 8244
				private float _distance;

				// Token: 0x04002035 RID: 8245
				public Settlement Settlement;
			}
		}
	}
}
