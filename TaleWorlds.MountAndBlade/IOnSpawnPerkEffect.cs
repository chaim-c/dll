using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FF RID: 767
	public interface IOnSpawnPerkEffect
	{
		// Token: 0x060029C8 RID: 10696
		int GetExtraTroopCount();

		// Token: 0x060029C9 RID: 10697
		List<ValueTuple<EquipmentIndex, EquipmentElement>> GetAlternativeEquipments(bool isPlayer, List<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments, bool getAll = false);

		// Token: 0x060029CA RID: 10698
		float GetDrivenPropertyBonusOnSpawn(bool isPlayer, DrivenProperty drivenProperty, float baseValue);

		// Token: 0x060029CB RID: 10699
		float GetHitpoints(bool isPlayer);
	}
}
