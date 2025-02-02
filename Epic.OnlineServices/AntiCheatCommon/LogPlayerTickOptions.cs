using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F6 RID: 1526
	public struct LogPlayerTickOptions
	{
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x0003A2AA File Offset: 0x000384AA
		// (set) Token: 0x06002705 RID: 9989 RVA: 0x0003A2B2 File Offset: 0x000384B2
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002706 RID: 9990 RVA: 0x0003A2BB File Offset: 0x000384BB
		// (set) Token: 0x06002707 RID: 9991 RVA: 0x0003A2C3 File Offset: 0x000384C3
		public Vec3f? PlayerPosition { get; set; }

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002708 RID: 9992 RVA: 0x0003A2CC File Offset: 0x000384CC
		// (set) Token: 0x06002709 RID: 9993 RVA: 0x0003A2D4 File Offset: 0x000384D4
		public Quat? PlayerViewRotation { get; set; }

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x0003A2DD File Offset: 0x000384DD
		// (set) Token: 0x0600270B RID: 9995 RVA: 0x0003A2E5 File Offset: 0x000384E5
		public bool IsPlayerViewZoomed { get; set; }

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x0003A2EE File Offset: 0x000384EE
		// (set) Token: 0x0600270D RID: 9997 RVA: 0x0003A2F6 File Offset: 0x000384F6
		public float PlayerHealth { get; set; }

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x0003A2FF File Offset: 0x000384FF
		// (set) Token: 0x0600270F RID: 9999 RVA: 0x0003A307 File Offset: 0x00038507
		public AntiCheatCommonPlayerMovementState PlayerMovementState { get; set; }
	}
}
