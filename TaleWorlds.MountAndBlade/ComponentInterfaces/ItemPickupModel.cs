using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003C7 RID: 967
	public abstract class ItemPickupModel : GameModel
	{
		// Token: 0x06003362 RID: 13154
		public abstract float GetItemScoreForAgent(SpawnedItemEntity item, Agent agent);

		// Token: 0x06003363 RID: 13155
		public abstract bool IsItemAvailableForAgent(SpawnedItemEntity item, Agent agent, EquipmentIndex slotToPickUp);

		// Token: 0x06003364 RID: 13156
		public abstract bool IsAgentEquipmentSuitableForPickUpAvailability(Agent agent);
	}
}
