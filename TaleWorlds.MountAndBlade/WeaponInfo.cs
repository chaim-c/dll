using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037B RID: 891
	public struct WeaponInfo
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000CBABB File Offset: 0x000C9CBB
		// (set) Token: 0x06003126 RID: 12582 RVA: 0x000CBAC3 File Offset: 0x000C9CC3
		public bool IsValid { get; private set; }

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000CBACC File Offset: 0x000C9CCC
		// (set) Token: 0x06003128 RID: 12584 RVA: 0x000CBAD4 File Offset: 0x000C9CD4
		public bool IsMeleeWeapon { get; private set; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000CBADD File Offset: 0x000C9CDD
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000CBAE5 File Offset: 0x000C9CE5
		public bool IsRangedWeapon { get; private set; }

		// Token: 0x0600312B RID: 12587 RVA: 0x000CBAEE File Offset: 0x000C9CEE
		public WeaponInfo(bool isValid, bool isMeleeWeapon, bool isRangedWeapon)
		{
			this.IsValid = isValid;
			this.IsMeleeWeapon = isMeleeWeapon;
			this.IsRangedWeapon = isRangedWeapon;
		}
	}
}
