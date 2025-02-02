using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000263 RID: 611
	// (Invoke) Token: 0x06002083 RID: 8323
	public delegate void PlayerTurnToChooseFormationToLeadEvent(Dictionary<int, Agent> lockedFormationIndicesAndSergeants, List<int> remainingFormationIndices);
}
