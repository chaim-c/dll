using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200017D RID: 381
	public static class DefaultTacticalDecisionCodes
	{
		// Token: 0x04000575 RID: 1397
		public const byte FormationMoveToPoint = 0;

		// Token: 0x04000576 RID: 1398
		public const byte FormationDefendPoint = 1;

		// Token: 0x04000577 RID: 1399
		public const byte FormationMoveToObject = 10;

		// Token: 0x04000578 RID: 1400
		public const byte FormationAttackObject = 11;

		// Token: 0x04000579 RID: 1401
		public const byte FormationDefendObject = 12;

		// Token: 0x0400057A RID: 1402
		public const byte FormationDefendFormation = 20;

		// Token: 0x0400057B RID: 1403
		public const byte FormationAttackFormation = 21;

		// Token: 0x0400057C RID: 1404
		public const byte TeamCharge = 30;

		// Token: 0x0400057D RID: 1405
		public const byte TeamFallbackToKeep = 31;

		// Token: 0x0400057E RID: 1406
		public const byte TeamRetreat = 32;
	}
}
