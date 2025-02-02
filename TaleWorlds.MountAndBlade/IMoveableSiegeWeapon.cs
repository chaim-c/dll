using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000341 RID: 833
	public interface IMoveableSiegeWeapon
	{
		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002D9A RID: 11674
		SiegeWeaponMovementComponent MovementComponent { get; }

		// Token: 0x06002D9B RID: 11675
		void HighlightPath();

		// Token: 0x06002D9C RID: 11676
		void SwitchGhostEntityMovementMode(bool isGhostEnabled);

		// Token: 0x06002D9D RID: 11677
		MatrixFrame GetInitialFrame();
	}
}
