using System;

namespace Mono.Cecil.Cil
{
	// Token: 0x02000002 RID: 2
	public enum Code
	{
		// Token: 0x04000002 RID: 2
		Nop,
		// Token: 0x04000003 RID: 3
		Break,
		// Token: 0x04000004 RID: 4
		Ldarg_0,
		// Token: 0x04000005 RID: 5
		Ldarg_1,
		// Token: 0x04000006 RID: 6
		Ldarg_2,
		// Token: 0x04000007 RID: 7
		Ldarg_3,
		// Token: 0x04000008 RID: 8
		Ldloc_0,
		// Token: 0x04000009 RID: 9
		Ldloc_1,
		// Token: 0x0400000A RID: 10
		Ldloc_2,
		// Token: 0x0400000B RID: 11
		Ldloc_3,
		// Token: 0x0400000C RID: 12
		Stloc_0,
		// Token: 0x0400000D RID: 13
		Stloc_1,
		// Token: 0x0400000E RID: 14
		Stloc_2,
		// Token: 0x0400000F RID: 15
		Stloc_3,
		// Token: 0x04000010 RID: 16
		Ldarg_S,
		// Token: 0x04000011 RID: 17
		Ldarga_S,
		// Token: 0x04000012 RID: 18
		Starg_S,
		// Token: 0x04000013 RID: 19
		Ldloc_S,
		// Token: 0x04000014 RID: 20
		Ldloca_S,
		// Token: 0x04000015 RID: 21
		Stloc_S,
		// Token: 0x04000016 RID: 22
		Ldnull,
		// Token: 0x04000017 RID: 23
		Ldc_I4_M1,
		// Token: 0x04000018 RID: 24
		Ldc_I4_0,
		// Token: 0x04000019 RID: 25
		Ldc_I4_1,
		// Token: 0x0400001A RID: 26
		Ldc_I4_2,
		// Token: 0x0400001B RID: 27
		Ldc_I4_3,
		// Token: 0x0400001C RID: 28
		Ldc_I4_4,
		// Token: 0x0400001D RID: 29
		Ldc_I4_5,
		// Token: 0x0400001E RID: 30
		Ldc_I4_6,
		// Token: 0x0400001F RID: 31
		Ldc_I4_7,
		// Token: 0x04000020 RID: 32
		Ldc_I4_8,
		// Token: 0x04000021 RID: 33
		Ldc_I4_S,
		// Token: 0x04000022 RID: 34
		Ldc_I4,
		// Token: 0x04000023 RID: 35
		Ldc_I8,
		// Token: 0x04000024 RID: 36
		Ldc_R4,
		// Token: 0x04000025 RID: 37
		Ldc_R8,
		// Token: 0x04000026 RID: 38
		Dup,
		// Token: 0x04000027 RID: 39
		Pop,
		// Token: 0x04000028 RID: 40
		Jmp,
		// Token: 0x04000029 RID: 41
		Call,
		// Token: 0x0400002A RID: 42
		Calli,
		// Token: 0x0400002B RID: 43
		Ret,
		// Token: 0x0400002C RID: 44
		Br_S,
		// Token: 0x0400002D RID: 45
		Brfalse_S,
		// Token: 0x0400002E RID: 46
		Brtrue_S,
		// Token: 0x0400002F RID: 47
		Beq_S,
		// Token: 0x04000030 RID: 48
		Bge_S,
		// Token: 0x04000031 RID: 49
		Bgt_S,
		// Token: 0x04000032 RID: 50
		Ble_S,
		// Token: 0x04000033 RID: 51
		Blt_S,
		// Token: 0x04000034 RID: 52
		Bne_Un_S,
		// Token: 0x04000035 RID: 53
		Bge_Un_S,
		// Token: 0x04000036 RID: 54
		Bgt_Un_S,
		// Token: 0x04000037 RID: 55
		Ble_Un_S,
		// Token: 0x04000038 RID: 56
		Blt_Un_S,
		// Token: 0x04000039 RID: 57
		Br,
		// Token: 0x0400003A RID: 58
		Brfalse,
		// Token: 0x0400003B RID: 59
		Brtrue,
		// Token: 0x0400003C RID: 60
		Beq,
		// Token: 0x0400003D RID: 61
		Bge,
		// Token: 0x0400003E RID: 62
		Bgt,
		// Token: 0x0400003F RID: 63
		Ble,
		// Token: 0x04000040 RID: 64
		Blt,
		// Token: 0x04000041 RID: 65
		Bne_Un,
		// Token: 0x04000042 RID: 66
		Bge_Un,
		// Token: 0x04000043 RID: 67
		Bgt_Un,
		// Token: 0x04000044 RID: 68
		Ble_Un,
		// Token: 0x04000045 RID: 69
		Blt_Un,
		// Token: 0x04000046 RID: 70
		Switch,
		// Token: 0x04000047 RID: 71
		Ldind_I1,
		// Token: 0x04000048 RID: 72
		Ldind_U1,
		// Token: 0x04000049 RID: 73
		Ldind_I2,
		// Token: 0x0400004A RID: 74
		Ldind_U2,
		// Token: 0x0400004B RID: 75
		Ldind_I4,
		// Token: 0x0400004C RID: 76
		Ldind_U4,
		// Token: 0x0400004D RID: 77
		Ldind_I8,
		// Token: 0x0400004E RID: 78
		Ldind_I,
		// Token: 0x0400004F RID: 79
		Ldind_R4,
		// Token: 0x04000050 RID: 80
		Ldind_R8,
		// Token: 0x04000051 RID: 81
		Ldind_Ref,
		// Token: 0x04000052 RID: 82
		Stind_Ref,
		// Token: 0x04000053 RID: 83
		Stind_I1,
		// Token: 0x04000054 RID: 84
		Stind_I2,
		// Token: 0x04000055 RID: 85
		Stind_I4,
		// Token: 0x04000056 RID: 86
		Stind_I8,
		// Token: 0x04000057 RID: 87
		Stind_R4,
		// Token: 0x04000058 RID: 88
		Stind_R8,
		// Token: 0x04000059 RID: 89
		Add,
		// Token: 0x0400005A RID: 90
		Sub,
		// Token: 0x0400005B RID: 91
		Mul,
		// Token: 0x0400005C RID: 92
		Div,
		// Token: 0x0400005D RID: 93
		Div_Un,
		// Token: 0x0400005E RID: 94
		Rem,
		// Token: 0x0400005F RID: 95
		Rem_Un,
		// Token: 0x04000060 RID: 96
		And,
		// Token: 0x04000061 RID: 97
		Or,
		// Token: 0x04000062 RID: 98
		Xor,
		// Token: 0x04000063 RID: 99
		Shl,
		// Token: 0x04000064 RID: 100
		Shr,
		// Token: 0x04000065 RID: 101
		Shr_Un,
		// Token: 0x04000066 RID: 102
		Neg,
		// Token: 0x04000067 RID: 103
		Not,
		// Token: 0x04000068 RID: 104
		Conv_I1,
		// Token: 0x04000069 RID: 105
		Conv_I2,
		// Token: 0x0400006A RID: 106
		Conv_I4,
		// Token: 0x0400006B RID: 107
		Conv_I8,
		// Token: 0x0400006C RID: 108
		Conv_R4,
		// Token: 0x0400006D RID: 109
		Conv_R8,
		// Token: 0x0400006E RID: 110
		Conv_U4,
		// Token: 0x0400006F RID: 111
		Conv_U8,
		// Token: 0x04000070 RID: 112
		Callvirt,
		// Token: 0x04000071 RID: 113
		Cpobj,
		// Token: 0x04000072 RID: 114
		Ldobj,
		// Token: 0x04000073 RID: 115
		Ldstr,
		// Token: 0x04000074 RID: 116
		Newobj,
		// Token: 0x04000075 RID: 117
		Castclass,
		// Token: 0x04000076 RID: 118
		Isinst,
		// Token: 0x04000077 RID: 119
		Conv_R_Un,
		// Token: 0x04000078 RID: 120
		Unbox,
		// Token: 0x04000079 RID: 121
		Throw,
		// Token: 0x0400007A RID: 122
		Ldfld,
		// Token: 0x0400007B RID: 123
		Ldflda,
		// Token: 0x0400007C RID: 124
		Stfld,
		// Token: 0x0400007D RID: 125
		Ldsfld,
		// Token: 0x0400007E RID: 126
		Ldsflda,
		// Token: 0x0400007F RID: 127
		Stsfld,
		// Token: 0x04000080 RID: 128
		Stobj,
		// Token: 0x04000081 RID: 129
		Conv_Ovf_I1_Un,
		// Token: 0x04000082 RID: 130
		Conv_Ovf_I2_Un,
		// Token: 0x04000083 RID: 131
		Conv_Ovf_I4_Un,
		// Token: 0x04000084 RID: 132
		Conv_Ovf_I8_Un,
		// Token: 0x04000085 RID: 133
		Conv_Ovf_U1_Un,
		// Token: 0x04000086 RID: 134
		Conv_Ovf_U2_Un,
		// Token: 0x04000087 RID: 135
		Conv_Ovf_U4_Un,
		// Token: 0x04000088 RID: 136
		Conv_Ovf_U8_Un,
		// Token: 0x04000089 RID: 137
		Conv_Ovf_I_Un,
		// Token: 0x0400008A RID: 138
		Conv_Ovf_U_Un,
		// Token: 0x0400008B RID: 139
		Box,
		// Token: 0x0400008C RID: 140
		Newarr,
		// Token: 0x0400008D RID: 141
		Ldlen,
		// Token: 0x0400008E RID: 142
		Ldelema,
		// Token: 0x0400008F RID: 143
		Ldelem_I1,
		// Token: 0x04000090 RID: 144
		Ldelem_U1,
		// Token: 0x04000091 RID: 145
		Ldelem_I2,
		// Token: 0x04000092 RID: 146
		Ldelem_U2,
		// Token: 0x04000093 RID: 147
		Ldelem_I4,
		// Token: 0x04000094 RID: 148
		Ldelem_U4,
		// Token: 0x04000095 RID: 149
		Ldelem_I8,
		// Token: 0x04000096 RID: 150
		Ldelem_I,
		// Token: 0x04000097 RID: 151
		Ldelem_R4,
		// Token: 0x04000098 RID: 152
		Ldelem_R8,
		// Token: 0x04000099 RID: 153
		Ldelem_Ref,
		// Token: 0x0400009A RID: 154
		Stelem_I,
		// Token: 0x0400009B RID: 155
		Stelem_I1,
		// Token: 0x0400009C RID: 156
		Stelem_I2,
		// Token: 0x0400009D RID: 157
		Stelem_I4,
		// Token: 0x0400009E RID: 158
		Stelem_I8,
		// Token: 0x0400009F RID: 159
		Stelem_R4,
		// Token: 0x040000A0 RID: 160
		Stelem_R8,
		// Token: 0x040000A1 RID: 161
		Stelem_Ref,
		// Token: 0x040000A2 RID: 162
		Ldelem_Any,
		// Token: 0x040000A3 RID: 163
		Stelem_Any,
		// Token: 0x040000A4 RID: 164
		Unbox_Any,
		// Token: 0x040000A5 RID: 165
		Conv_Ovf_I1,
		// Token: 0x040000A6 RID: 166
		Conv_Ovf_U1,
		// Token: 0x040000A7 RID: 167
		Conv_Ovf_I2,
		// Token: 0x040000A8 RID: 168
		Conv_Ovf_U2,
		// Token: 0x040000A9 RID: 169
		Conv_Ovf_I4,
		// Token: 0x040000AA RID: 170
		Conv_Ovf_U4,
		// Token: 0x040000AB RID: 171
		Conv_Ovf_I8,
		// Token: 0x040000AC RID: 172
		Conv_Ovf_U8,
		// Token: 0x040000AD RID: 173
		Refanyval,
		// Token: 0x040000AE RID: 174
		Ckfinite,
		// Token: 0x040000AF RID: 175
		Mkrefany,
		// Token: 0x040000B0 RID: 176
		Ldtoken,
		// Token: 0x040000B1 RID: 177
		Conv_U2,
		// Token: 0x040000B2 RID: 178
		Conv_U1,
		// Token: 0x040000B3 RID: 179
		Conv_I,
		// Token: 0x040000B4 RID: 180
		Conv_Ovf_I,
		// Token: 0x040000B5 RID: 181
		Conv_Ovf_U,
		// Token: 0x040000B6 RID: 182
		Add_Ovf,
		// Token: 0x040000B7 RID: 183
		Add_Ovf_Un,
		// Token: 0x040000B8 RID: 184
		Mul_Ovf,
		// Token: 0x040000B9 RID: 185
		Mul_Ovf_Un,
		// Token: 0x040000BA RID: 186
		Sub_Ovf,
		// Token: 0x040000BB RID: 187
		Sub_Ovf_Un,
		// Token: 0x040000BC RID: 188
		Endfinally,
		// Token: 0x040000BD RID: 189
		Leave,
		// Token: 0x040000BE RID: 190
		Leave_S,
		// Token: 0x040000BF RID: 191
		Stind_I,
		// Token: 0x040000C0 RID: 192
		Conv_U,
		// Token: 0x040000C1 RID: 193
		Arglist,
		// Token: 0x040000C2 RID: 194
		Ceq,
		// Token: 0x040000C3 RID: 195
		Cgt,
		// Token: 0x040000C4 RID: 196
		Cgt_Un,
		// Token: 0x040000C5 RID: 197
		Clt,
		// Token: 0x040000C6 RID: 198
		Clt_Un,
		// Token: 0x040000C7 RID: 199
		Ldftn,
		// Token: 0x040000C8 RID: 200
		Ldvirtftn,
		// Token: 0x040000C9 RID: 201
		Ldarg,
		// Token: 0x040000CA RID: 202
		Ldarga,
		// Token: 0x040000CB RID: 203
		Starg,
		// Token: 0x040000CC RID: 204
		Ldloc,
		// Token: 0x040000CD RID: 205
		Ldloca,
		// Token: 0x040000CE RID: 206
		Stloc,
		// Token: 0x040000CF RID: 207
		Localloc,
		// Token: 0x040000D0 RID: 208
		Endfilter,
		// Token: 0x040000D1 RID: 209
		Unaligned,
		// Token: 0x040000D2 RID: 210
		Volatile,
		// Token: 0x040000D3 RID: 211
		Tail,
		// Token: 0x040000D4 RID: 212
		Initobj,
		// Token: 0x040000D5 RID: 213
		Constrained,
		// Token: 0x040000D6 RID: 214
		Cpblk,
		// Token: 0x040000D7 RID: 215
		Initblk,
		// Token: 0x040000D8 RID: 216
		No,
		// Token: 0x040000D9 RID: 217
		Rethrow,
		// Token: 0x040000DA RID: 218
		Sizeof,
		// Token: 0x040000DB RID: 219
		Refanytype,
		// Token: 0x040000DC RID: 220
		Readonly
	}
}
