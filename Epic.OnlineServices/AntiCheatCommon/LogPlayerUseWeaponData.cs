using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005FA RID: 1530
	public struct LogPlayerUseWeaponData
	{
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x0003A5BC File Offset: 0x000387BC
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x0003A5C4 File Offset: 0x000387C4
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x0003A5CD File Offset: 0x000387CD
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x0003A5D5 File Offset: 0x000387D5
		public Vec3f? PlayerPosition { get; set; }

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0003A5DE File Offset: 0x000387DE
		// (set) Token: 0x0600272D RID: 10029 RVA: 0x0003A5E6 File Offset: 0x000387E6
		public Quat? PlayerViewRotation { get; set; }

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x0003A5EF File Offset: 0x000387EF
		// (set) Token: 0x0600272F RID: 10031 RVA: 0x0003A5F7 File Offset: 0x000387F7
		public bool IsPlayerViewZoomed { get; set; }

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x0003A600 File Offset: 0x00038800
		// (set) Token: 0x06002731 RID: 10033 RVA: 0x0003A608 File Offset: 0x00038808
		public bool IsMeleeAttack { get; set; }

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x0003A611 File Offset: 0x00038811
		// (set) Token: 0x06002733 RID: 10035 RVA: 0x0003A619 File Offset: 0x00038819
		public Utf8String WeaponName { get; set; }

		// Token: 0x06002734 RID: 10036 RVA: 0x0003A624 File Offset: 0x00038824
		internal void Set(ref LogPlayerUseWeaponDataInternal other)
		{
			this.PlayerHandle = other.PlayerHandle;
			this.PlayerPosition = other.PlayerPosition;
			this.PlayerViewRotation = other.PlayerViewRotation;
			this.IsPlayerViewZoomed = other.IsPlayerViewZoomed;
			this.IsMeleeAttack = other.IsMeleeAttack;
			this.WeaponName = other.WeaponName;
		}
	}
}
