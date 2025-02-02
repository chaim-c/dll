using System;

namespace Mono.Cecil
{
	// Token: 0x020000BD RID: 189
	public enum NativeType
	{
		// Token: 0x04000459 RID: 1113
		None = 102,
		// Token: 0x0400045A RID: 1114
		Boolean = 2,
		// Token: 0x0400045B RID: 1115
		I1,
		// Token: 0x0400045C RID: 1116
		U1,
		// Token: 0x0400045D RID: 1117
		I2,
		// Token: 0x0400045E RID: 1118
		U2,
		// Token: 0x0400045F RID: 1119
		I4,
		// Token: 0x04000460 RID: 1120
		U4,
		// Token: 0x04000461 RID: 1121
		I8,
		// Token: 0x04000462 RID: 1122
		U8,
		// Token: 0x04000463 RID: 1123
		R4,
		// Token: 0x04000464 RID: 1124
		R8,
		// Token: 0x04000465 RID: 1125
		LPStr = 20,
		// Token: 0x04000466 RID: 1126
		Int = 31,
		// Token: 0x04000467 RID: 1127
		UInt,
		// Token: 0x04000468 RID: 1128
		Func = 38,
		// Token: 0x04000469 RID: 1129
		Array = 42,
		// Token: 0x0400046A RID: 1130
		Currency = 15,
		// Token: 0x0400046B RID: 1131
		BStr = 19,
		// Token: 0x0400046C RID: 1132
		LPWStr = 21,
		// Token: 0x0400046D RID: 1133
		LPTStr,
		// Token: 0x0400046E RID: 1134
		FixedSysString,
		// Token: 0x0400046F RID: 1135
		IUnknown = 25,
		// Token: 0x04000470 RID: 1136
		IDispatch,
		// Token: 0x04000471 RID: 1137
		Struct,
		// Token: 0x04000472 RID: 1138
		IntF,
		// Token: 0x04000473 RID: 1139
		SafeArray,
		// Token: 0x04000474 RID: 1140
		FixedArray,
		// Token: 0x04000475 RID: 1141
		ByValStr = 34,
		// Token: 0x04000476 RID: 1142
		ANSIBStr,
		// Token: 0x04000477 RID: 1143
		TBStr,
		// Token: 0x04000478 RID: 1144
		VariantBool,
		// Token: 0x04000479 RID: 1145
		ASAny = 40,
		// Token: 0x0400047A RID: 1146
		LPStruct = 43,
		// Token: 0x0400047B RID: 1147
		CustomMarshaler,
		// Token: 0x0400047C RID: 1148
		Error,
		// Token: 0x0400047D RID: 1149
		Max = 80
	}
}
