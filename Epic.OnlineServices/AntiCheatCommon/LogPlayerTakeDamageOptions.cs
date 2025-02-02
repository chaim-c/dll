using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F4 RID: 1524
	public struct LogPlayerTakeDamageOptions
	{
		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x00039D77 File Offset: 0x00037F77
		// (set) Token: 0x060026CC RID: 9932 RVA: 0x00039D7F File Offset: 0x00037F7F
		public IntPtr VictimPlayerHandle { get; set; }

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x00039D88 File Offset: 0x00037F88
		// (set) Token: 0x060026CE RID: 9934 RVA: 0x00039D90 File Offset: 0x00037F90
		public Vec3f? VictimPlayerPosition { get; set; }

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x00039D99 File Offset: 0x00037F99
		// (set) Token: 0x060026D0 RID: 9936 RVA: 0x00039DA1 File Offset: 0x00037FA1
		public Quat? VictimPlayerViewRotation { get; set; }

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060026D1 RID: 9937 RVA: 0x00039DAA File Offset: 0x00037FAA
		// (set) Token: 0x060026D2 RID: 9938 RVA: 0x00039DB2 File Offset: 0x00037FB2
		public IntPtr AttackerPlayerHandle { get; set; }

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x00039DBB File Offset: 0x00037FBB
		// (set) Token: 0x060026D4 RID: 9940 RVA: 0x00039DC3 File Offset: 0x00037FC3
		public Vec3f? AttackerPlayerPosition { get; set; }

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00039DCC File Offset: 0x00037FCC
		// (set) Token: 0x060026D6 RID: 9942 RVA: 0x00039DD4 File Offset: 0x00037FD4
		public Quat? AttackerPlayerViewRotation { get; set; }

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00039DDD File Offset: 0x00037FDD
		// (set) Token: 0x060026D8 RID: 9944 RVA: 0x00039DE5 File Offset: 0x00037FE5
		public bool IsHitscanAttack { get; set; }

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x00039DEE File Offset: 0x00037FEE
		// (set) Token: 0x060026DA RID: 9946 RVA: 0x00039DF6 File Offset: 0x00037FF6
		public bool HasLineOfSight { get; set; }

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060026DB RID: 9947 RVA: 0x00039DFF File Offset: 0x00037FFF
		// (set) Token: 0x060026DC RID: 9948 RVA: 0x00039E07 File Offset: 0x00038007
		public bool IsCriticalHit { get; set; }

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00039E10 File Offset: 0x00038010
		// (set) Token: 0x060026DE RID: 9950 RVA: 0x00039E18 File Offset: 0x00038018
		public uint HitBoneId_DEPRECATED { get; set; }

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x00039E21 File Offset: 0x00038021
		// (set) Token: 0x060026E0 RID: 9952 RVA: 0x00039E29 File Offset: 0x00038029
		public float DamageTaken { get; set; }

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060026E1 RID: 9953 RVA: 0x00039E32 File Offset: 0x00038032
		// (set) Token: 0x060026E2 RID: 9954 RVA: 0x00039E3A File Offset: 0x0003803A
		public float HealthRemaining { get; set; }

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x00039E43 File Offset: 0x00038043
		// (set) Token: 0x060026E4 RID: 9956 RVA: 0x00039E4B File Offset: 0x0003804B
		public AntiCheatCommonPlayerTakeDamageSource DamageSource { get; set; }

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00039E54 File Offset: 0x00038054
		// (set) Token: 0x060026E6 RID: 9958 RVA: 0x00039E5C File Offset: 0x0003805C
		public AntiCheatCommonPlayerTakeDamageType DamageType { get; set; }

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x00039E65 File Offset: 0x00038065
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x00039E6D File Offset: 0x0003806D
		public AntiCheatCommonPlayerTakeDamageResult DamageResult { get; set; }

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x00039E76 File Offset: 0x00038076
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x00039E7E File Offset: 0x0003807E
		public LogPlayerUseWeaponData? PlayerUseWeaponData { get; set; }

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x00039E87 File Offset: 0x00038087
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x00039E8F File Offset: 0x0003808F
		public uint TimeSincePlayerUseWeaponMs { get; set; }

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x00039E98 File Offset: 0x00038098
		// (set) Token: 0x060026EE RID: 9966 RVA: 0x00039EA0 File Offset: 0x000380A0
		public Vec3f? DamagePosition { get; set; }
	}
}
