using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014B RID: 331
	public enum OrderType
	{
		// Token: 0x040003DD RID: 989
		None,
		// Token: 0x040003DE RID: 990
		Move,
		// Token: 0x040003DF RID: 991
		MoveToLineSegment,
		// Token: 0x040003E0 RID: 992
		MoveToLineSegmentWithHorizontalLayout,
		// Token: 0x040003E1 RID: 993
		Charge,
		// Token: 0x040003E2 RID: 994
		ChargeWithTarget,
		// Token: 0x040003E3 RID: 995
		StandYourGround,
		// Token: 0x040003E4 RID: 996
		FollowMe,
		// Token: 0x040003E5 RID: 997
		FollowEntity,
		// Token: 0x040003E6 RID: 998
		GuardMe,
		// Token: 0x040003E7 RID: 999
		Retreat,
		// Token: 0x040003E8 RID: 1000
		AdvanceTenPaces,
		// Token: 0x040003E9 RID: 1001
		FallBackTenPaces,
		// Token: 0x040003EA RID: 1002
		Advance,
		// Token: 0x040003EB RID: 1003
		FallBack,
		// Token: 0x040003EC RID: 1004
		LookAtEnemy,
		// Token: 0x040003ED RID: 1005
		LookAtDirection,
		// Token: 0x040003EE RID: 1006
		ArrangementLine,
		// Token: 0x040003EF RID: 1007
		ArrangementCloseOrder,
		// Token: 0x040003F0 RID: 1008
		ArrangementLoose,
		// Token: 0x040003F1 RID: 1009
		ArrangementCircular,
		// Token: 0x040003F2 RID: 1010
		ArrangementSchiltron,
		// Token: 0x040003F3 RID: 1011
		ArrangementVee,
		// Token: 0x040003F4 RID: 1012
		ArrangementColumn,
		// Token: 0x040003F5 RID: 1013
		ArrangementScatter,
		// Token: 0x040003F6 RID: 1014
		FormCustom,
		// Token: 0x040003F7 RID: 1015
		FormDeep,
		// Token: 0x040003F8 RID: 1016
		FormWide,
		// Token: 0x040003F9 RID: 1017
		FormWider,
		// Token: 0x040003FA RID: 1018
		CohesionHigh,
		// Token: 0x040003FB RID: 1019
		CohesionMedium,
		// Token: 0x040003FC RID: 1020
		CohesionLow,
		// Token: 0x040003FD RID: 1021
		HoldFire,
		// Token: 0x040003FE RID: 1022
		FireAtWill,
		// Token: 0x040003FF RID: 1023
		RideFree,
		// Token: 0x04000400 RID: 1024
		Mount,
		// Token: 0x04000401 RID: 1025
		Dismount,
		// Token: 0x04000402 RID: 1026
		AIControlOn,
		// Token: 0x04000403 RID: 1027
		AIControlOff,
		// Token: 0x04000404 RID: 1028
		Transfer,
		// Token: 0x04000405 RID: 1029
		Use,
		// Token: 0x04000406 RID: 1030
		AttackEntity,
		// Token: 0x04000407 RID: 1031
		PointDefence,
		// Token: 0x04000408 RID: 1032
		Count
	}
}
