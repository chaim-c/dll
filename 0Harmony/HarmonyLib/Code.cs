using System;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000048 RID: 72
	public static class Code
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000FBDE File Offset: 0x0000DDDE
		public static Code.Operand_ Operand
		{
			get
			{
				return new Code.Operand_();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000FBE5 File Offset: 0x0000DDE5
		public static Code.Nop_ Nop
		{
			get
			{
				return new Code.Nop_
				{
					opcode = OpCodes.Nop
				};
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000FBF7 File Offset: 0x0000DDF7
		public static Code.Break_ Break
		{
			get
			{
				return new Code.Break_
				{
					opcode = OpCodes.Break
				};
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000FC09 File Offset: 0x0000DE09
		public static Code.Ldarg_0_ Ldarg_0
		{
			get
			{
				return new Code.Ldarg_0_
				{
					opcode = OpCodes.Ldarg_0
				};
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000FC1B File Offset: 0x0000DE1B
		public static Code.Ldarg_1_ Ldarg_1
		{
			get
			{
				return new Code.Ldarg_1_
				{
					opcode = OpCodes.Ldarg_1
				};
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000FC2D File Offset: 0x0000DE2D
		public static Code.Ldarg_2_ Ldarg_2
		{
			get
			{
				return new Code.Ldarg_2_
				{
					opcode = OpCodes.Ldarg_2
				};
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000FC3F File Offset: 0x0000DE3F
		public static Code.Ldarg_3_ Ldarg_3
		{
			get
			{
				return new Code.Ldarg_3_
				{
					opcode = OpCodes.Ldarg_3
				};
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000FC51 File Offset: 0x0000DE51
		public static Code.Ldloc_0_ Ldloc_0
		{
			get
			{
				return new Code.Ldloc_0_
				{
					opcode = OpCodes.Ldloc_0
				};
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000FC63 File Offset: 0x0000DE63
		public static Code.Ldloc_1_ Ldloc_1
		{
			get
			{
				return new Code.Ldloc_1_
				{
					opcode = OpCodes.Ldloc_1
				};
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000FC75 File Offset: 0x0000DE75
		public static Code.Ldloc_2_ Ldloc_2
		{
			get
			{
				return new Code.Ldloc_2_
				{
					opcode = OpCodes.Ldloc_2
				};
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000FC87 File Offset: 0x0000DE87
		public static Code.Ldloc_3_ Ldloc_3
		{
			get
			{
				return new Code.Ldloc_3_
				{
					opcode = OpCodes.Ldloc_3
				};
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000FC99 File Offset: 0x0000DE99
		public static Code.Stloc_0_ Stloc_0
		{
			get
			{
				return new Code.Stloc_0_
				{
					opcode = OpCodes.Stloc_0
				};
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000FCAB File Offset: 0x0000DEAB
		public static Code.Stloc_1_ Stloc_1
		{
			get
			{
				return new Code.Stloc_1_
				{
					opcode = OpCodes.Stloc_1
				};
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000FCBD File Offset: 0x0000DEBD
		public static Code.Stloc_2_ Stloc_2
		{
			get
			{
				return new Code.Stloc_2_
				{
					opcode = OpCodes.Stloc_2
				};
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000FCCF File Offset: 0x0000DECF
		public static Code.Stloc_3_ Stloc_3
		{
			get
			{
				return new Code.Stloc_3_
				{
					opcode = OpCodes.Stloc_3
				};
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000FCE1 File Offset: 0x0000DEE1
		public static Code.Ldarg_S_ Ldarg_S
		{
			get
			{
				return new Code.Ldarg_S_
				{
					opcode = OpCodes.Ldarg_S
				};
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000FCF3 File Offset: 0x0000DEF3
		public static Code.Ldarga_S_ Ldarga_S
		{
			get
			{
				return new Code.Ldarga_S_
				{
					opcode = OpCodes.Ldarga_S
				};
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000FD05 File Offset: 0x0000DF05
		public static Code.Starg_S_ Starg_S
		{
			get
			{
				return new Code.Starg_S_
				{
					opcode = OpCodes.Starg_S
				};
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000FD17 File Offset: 0x0000DF17
		public static Code.Ldloc_S_ Ldloc_S
		{
			get
			{
				return new Code.Ldloc_S_
				{
					opcode = OpCodes.Ldloc_S
				};
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000FD29 File Offset: 0x0000DF29
		public static Code.Ldloca_S_ Ldloca_S
		{
			get
			{
				return new Code.Ldloca_S_
				{
					opcode = OpCodes.Ldloca_S
				};
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000FD3B File Offset: 0x0000DF3B
		public static Code.Stloc_S_ Stloc_S
		{
			get
			{
				return new Code.Stloc_S_
				{
					opcode = OpCodes.Stloc_S
				};
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000FD4D File Offset: 0x0000DF4D
		public static Code.Ldnull_ Ldnull
		{
			get
			{
				return new Code.Ldnull_
				{
					opcode = OpCodes.Ldnull
				};
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000FD5F File Offset: 0x0000DF5F
		public static Code.Ldc_I4_M1_ Ldc_I4_M1
		{
			get
			{
				return new Code.Ldc_I4_M1_
				{
					opcode = OpCodes.Ldc_I4_M1
				};
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000FD71 File Offset: 0x0000DF71
		public static Code.Ldc_I4_0_ Ldc_I4_0
		{
			get
			{
				return new Code.Ldc_I4_0_
				{
					opcode = OpCodes.Ldc_I4_0
				};
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000FD83 File Offset: 0x0000DF83
		public static Code.Ldc_I4_1_ Ldc_I4_1
		{
			get
			{
				return new Code.Ldc_I4_1_
				{
					opcode = OpCodes.Ldc_I4_1
				};
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000FD95 File Offset: 0x0000DF95
		public static Code.Ldc_I4_2_ Ldc_I4_2
		{
			get
			{
				return new Code.Ldc_I4_2_
				{
					opcode = OpCodes.Ldc_I4_2
				};
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000FDA7 File Offset: 0x0000DFA7
		public static Code.Ldc_I4_3_ Ldc_I4_3
		{
			get
			{
				return new Code.Ldc_I4_3_
				{
					opcode = OpCodes.Ldc_I4_3
				};
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000FDB9 File Offset: 0x0000DFB9
		public static Code.Ldc_I4_4_ Ldc_I4_4
		{
			get
			{
				return new Code.Ldc_I4_4_
				{
					opcode = OpCodes.Ldc_I4_4
				};
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000FDCB File Offset: 0x0000DFCB
		public static Code.Ldc_I4_5_ Ldc_I4_5
		{
			get
			{
				return new Code.Ldc_I4_5_
				{
					opcode = OpCodes.Ldc_I4_5
				};
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000FDDD File Offset: 0x0000DFDD
		public static Code.Ldc_I4_6_ Ldc_I4_6
		{
			get
			{
				return new Code.Ldc_I4_6_
				{
					opcode = OpCodes.Ldc_I4_6
				};
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000FDEF File Offset: 0x0000DFEF
		public static Code.Ldc_I4_7_ Ldc_I4_7
		{
			get
			{
				return new Code.Ldc_I4_7_
				{
					opcode = OpCodes.Ldc_I4_7
				};
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000FE01 File Offset: 0x0000E001
		public static Code.Ldc_I4_8_ Ldc_I4_8
		{
			get
			{
				return new Code.Ldc_I4_8_
				{
					opcode = OpCodes.Ldc_I4_8
				};
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000FE13 File Offset: 0x0000E013
		public static Code.Ldc_I4_S_ Ldc_I4_S
		{
			get
			{
				return new Code.Ldc_I4_S_
				{
					opcode = OpCodes.Ldc_I4_S
				};
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000FE25 File Offset: 0x0000E025
		public static Code.Ldc_I4_ Ldc_I4
		{
			get
			{
				return new Code.Ldc_I4_
				{
					opcode = OpCodes.Ldc_I4
				};
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000FE37 File Offset: 0x0000E037
		public static Code.Ldc_I8_ Ldc_I8
		{
			get
			{
				return new Code.Ldc_I8_
				{
					opcode = OpCodes.Ldc_I8
				};
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000FE49 File Offset: 0x0000E049
		public static Code.Ldc_R4_ Ldc_R4
		{
			get
			{
				return new Code.Ldc_R4_
				{
					opcode = OpCodes.Ldc_R4
				};
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000FE5B File Offset: 0x0000E05B
		public static Code.Ldc_R8_ Ldc_R8
		{
			get
			{
				return new Code.Ldc_R8_
				{
					opcode = OpCodes.Ldc_R8
				};
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000FE6D File Offset: 0x0000E06D
		public static Code.Dup_ Dup
		{
			get
			{
				return new Code.Dup_
				{
					opcode = OpCodes.Dup
				};
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000FE7F File Offset: 0x0000E07F
		public static Code.Pop_ Pop
		{
			get
			{
				return new Code.Pop_
				{
					opcode = OpCodes.Pop
				};
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000FE91 File Offset: 0x0000E091
		public static Code.Jmp_ Jmp
		{
			get
			{
				return new Code.Jmp_
				{
					opcode = OpCodes.Jmp
				};
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000FEA3 File Offset: 0x0000E0A3
		public static Code.Call_ Call
		{
			get
			{
				return new Code.Call_
				{
					opcode = OpCodes.Call
				};
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000FEB5 File Offset: 0x0000E0B5
		public static Code.Calli_ Calli
		{
			get
			{
				return new Code.Calli_
				{
					opcode = OpCodes.Calli
				};
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000FEC7 File Offset: 0x0000E0C7
		public static Code.Ret_ Ret
		{
			get
			{
				return new Code.Ret_
				{
					opcode = OpCodes.Ret
				};
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000FED9 File Offset: 0x0000E0D9
		public static Code.Br_S_ Br_S
		{
			get
			{
				return new Code.Br_S_
				{
					opcode = OpCodes.Br_S
				};
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000FEEB File Offset: 0x0000E0EB
		public static Code.Brfalse_S_ Brfalse_S
		{
			get
			{
				return new Code.Brfalse_S_
				{
					opcode = OpCodes.Brfalse_S
				};
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000FEFD File Offset: 0x0000E0FD
		public static Code.Brtrue_S_ Brtrue_S
		{
			get
			{
				return new Code.Brtrue_S_
				{
					opcode = OpCodes.Brtrue_S
				};
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000FF0F File Offset: 0x0000E10F
		public static Code.Beq_S_ Beq_S
		{
			get
			{
				return new Code.Beq_S_
				{
					opcode = OpCodes.Beq_S
				};
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000FF21 File Offset: 0x0000E121
		public static Code.Bge_S_ Bge_S
		{
			get
			{
				return new Code.Bge_S_
				{
					opcode = OpCodes.Bge_S
				};
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000FF33 File Offset: 0x0000E133
		public static Code.Bgt_S_ Bgt_S
		{
			get
			{
				return new Code.Bgt_S_
				{
					opcode = OpCodes.Bgt_S
				};
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000FF45 File Offset: 0x0000E145
		public static Code.Ble_S_ Ble_S
		{
			get
			{
				return new Code.Ble_S_
				{
					opcode = OpCodes.Ble_S
				};
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000FF57 File Offset: 0x0000E157
		public static Code.Blt_S_ Blt_S
		{
			get
			{
				return new Code.Blt_S_
				{
					opcode = OpCodes.Blt_S
				};
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000FF69 File Offset: 0x0000E169
		public static Code.Bne_Un_S_ Bne_Un_S
		{
			get
			{
				return new Code.Bne_Un_S_
				{
					opcode = OpCodes.Bne_Un_S
				};
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000FF7B File Offset: 0x0000E17B
		public static Code.Bge_Un_S_ Bge_Un_S
		{
			get
			{
				return new Code.Bge_Un_S_
				{
					opcode = OpCodes.Bge_Un_S
				};
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000FF8D File Offset: 0x0000E18D
		public static Code.Bgt_Un_S_ Bgt_Un_S
		{
			get
			{
				return new Code.Bgt_Un_S_
				{
					opcode = OpCodes.Bgt_Un_S
				};
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000FF9F File Offset: 0x0000E19F
		public static Code.Ble_Un_S_ Ble_Un_S
		{
			get
			{
				return new Code.Ble_Un_S_
				{
					opcode = OpCodes.Ble_Un_S
				};
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		public static Code.Blt_Un_S_ Blt_Un_S
		{
			get
			{
				return new Code.Blt_Un_S_
				{
					opcode = OpCodes.Blt_Un_S
				};
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000FFC3 File Offset: 0x0000E1C3
		public static Code.Br_ Br
		{
			get
			{
				return new Code.Br_
				{
					opcode = OpCodes.Br
				};
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000FFD5 File Offset: 0x0000E1D5
		public static Code.Brfalse_ Brfalse
		{
			get
			{
				return new Code.Brfalse_
				{
					opcode = OpCodes.Brfalse
				};
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000FFE7 File Offset: 0x0000E1E7
		public static Code.Brtrue_ Brtrue
		{
			get
			{
				return new Code.Brtrue_
				{
					opcode = OpCodes.Brtrue
				};
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000FFF9 File Offset: 0x0000E1F9
		public static Code.Beq_ Beq
		{
			get
			{
				return new Code.Beq_
				{
					opcode = OpCodes.Beq
				};
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001000B File Offset: 0x0000E20B
		public static Code.Bge_ Bge
		{
			get
			{
				return new Code.Bge_
				{
					opcode = OpCodes.Bge
				};
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0001001D File Offset: 0x0000E21D
		public static Code.Bgt_ Bgt
		{
			get
			{
				return new Code.Bgt_
				{
					opcode = OpCodes.Bgt
				};
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0001002F File Offset: 0x0000E22F
		public static Code.Ble_ Ble
		{
			get
			{
				return new Code.Ble_
				{
					opcode = OpCodes.Ble
				};
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00010041 File Offset: 0x0000E241
		public static Code.Blt_ Blt
		{
			get
			{
				return new Code.Blt_
				{
					opcode = OpCodes.Blt
				};
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00010053 File Offset: 0x0000E253
		public static Code.Bne_Un_ Bne_Un
		{
			get
			{
				return new Code.Bne_Un_
				{
					opcode = OpCodes.Bne_Un
				};
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00010065 File Offset: 0x0000E265
		public static Code.Bge_Un_ Bge_Un
		{
			get
			{
				return new Code.Bge_Un_
				{
					opcode = OpCodes.Bge_Un
				};
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00010077 File Offset: 0x0000E277
		public static Code.Bgt_Un_ Bgt_Un
		{
			get
			{
				return new Code.Bgt_Un_
				{
					opcode = OpCodes.Bgt_Un
				};
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00010089 File Offset: 0x0000E289
		public static Code.Ble_Un_ Ble_Un
		{
			get
			{
				return new Code.Ble_Un_
				{
					opcode = OpCodes.Ble_Un
				};
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0001009B File Offset: 0x0000E29B
		public static Code.Blt_Un_ Blt_Un
		{
			get
			{
				return new Code.Blt_Un_
				{
					opcode = OpCodes.Blt_Un
				};
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000100AD File Offset: 0x0000E2AD
		public static Code.Switch_ Switch
		{
			get
			{
				return new Code.Switch_
				{
					opcode = OpCodes.Switch
				};
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000100BF File Offset: 0x0000E2BF
		public static Code.Ldind_I1_ Ldind_I1
		{
			get
			{
				return new Code.Ldind_I1_
				{
					opcode = OpCodes.Ldind_I1
				};
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600028F RID: 655 RVA: 0x000100D1 File Offset: 0x0000E2D1
		public static Code.Ldind_U1_ Ldind_U1
		{
			get
			{
				return new Code.Ldind_U1_
				{
					opcode = OpCodes.Ldind_U1
				};
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000290 RID: 656 RVA: 0x000100E3 File Offset: 0x0000E2E3
		public static Code.Ldind_I2_ Ldind_I2
		{
			get
			{
				return new Code.Ldind_I2_
				{
					opcode = OpCodes.Ldind_I2
				};
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000100F5 File Offset: 0x0000E2F5
		public static Code.Ldind_U2_ Ldind_U2
		{
			get
			{
				return new Code.Ldind_U2_
				{
					opcode = OpCodes.Ldind_U2
				};
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00010107 File Offset: 0x0000E307
		public static Code.Ldind_I4_ Ldind_I4
		{
			get
			{
				return new Code.Ldind_I4_
				{
					opcode = OpCodes.Ldind_I4
				};
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00010119 File Offset: 0x0000E319
		public static Code.Ldind_U4_ Ldind_U4
		{
			get
			{
				return new Code.Ldind_U4_
				{
					opcode = OpCodes.Ldind_U4
				};
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0001012B File Offset: 0x0000E32B
		public static Code.Ldind_I8_ Ldind_I8
		{
			get
			{
				return new Code.Ldind_I8_
				{
					opcode = OpCodes.Ldind_I8
				};
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0001013D File Offset: 0x0000E33D
		public static Code.Ldind_I_ Ldind_I
		{
			get
			{
				return new Code.Ldind_I_
				{
					opcode = OpCodes.Ldind_I
				};
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0001014F File Offset: 0x0000E34F
		public static Code.Ldind_R4_ Ldind_R4
		{
			get
			{
				return new Code.Ldind_R4_
				{
					opcode = OpCodes.Ldind_R4
				};
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00010161 File Offset: 0x0000E361
		public static Code.Ldind_R8_ Ldind_R8
		{
			get
			{
				return new Code.Ldind_R8_
				{
					opcode = OpCodes.Ldind_R8
				};
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00010173 File Offset: 0x0000E373
		public static Code.Ldind_Ref_ Ldind_Ref
		{
			get
			{
				return new Code.Ldind_Ref_
				{
					opcode = OpCodes.Ldind_Ref
				};
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00010185 File Offset: 0x0000E385
		public static Code.Stind_Ref_ Stind_Ref
		{
			get
			{
				return new Code.Stind_Ref_
				{
					opcode = OpCodes.Stind_Ref
				};
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00010197 File Offset: 0x0000E397
		public static Code.Stind_I1_ Stind_I1
		{
			get
			{
				return new Code.Stind_I1_
				{
					opcode = OpCodes.Stind_I1
				};
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000101A9 File Offset: 0x0000E3A9
		public static Code.Stind_I2_ Stind_I2
		{
			get
			{
				return new Code.Stind_I2_
				{
					opcode = OpCodes.Stind_I2
				};
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600029C RID: 668 RVA: 0x000101BB File Offset: 0x0000E3BB
		public static Code.Stind_I4_ Stind_I4
		{
			get
			{
				return new Code.Stind_I4_
				{
					opcode = OpCodes.Stind_I4
				};
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000101CD File Offset: 0x0000E3CD
		public static Code.Stind_I8_ Stind_I8
		{
			get
			{
				return new Code.Stind_I8_
				{
					opcode = OpCodes.Stind_I8
				};
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000101DF File Offset: 0x0000E3DF
		public static Code.Stind_R4_ Stind_R4
		{
			get
			{
				return new Code.Stind_R4_
				{
					opcode = OpCodes.Stind_R4
				};
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029F RID: 671 RVA: 0x000101F1 File Offset: 0x0000E3F1
		public static Code.Stind_R8_ Stind_R8
		{
			get
			{
				return new Code.Stind_R8_
				{
					opcode = OpCodes.Stind_R8
				};
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010203 File Offset: 0x0000E403
		public static Code.Add_ Add
		{
			get
			{
				return new Code.Add_
				{
					opcode = OpCodes.Add
				};
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00010215 File Offset: 0x0000E415
		public static Code.Sub_ Sub
		{
			get
			{
				return new Code.Sub_
				{
					opcode = OpCodes.Sub
				};
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010227 File Offset: 0x0000E427
		public static Code.Mul_ Mul
		{
			get
			{
				return new Code.Mul_
				{
					opcode = OpCodes.Mul
				};
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00010239 File Offset: 0x0000E439
		public static Code.Div_ Div
		{
			get
			{
				return new Code.Div_
				{
					opcode = OpCodes.Div
				};
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0001024B File Offset: 0x0000E44B
		public static Code.Div_Un_ Div_Un
		{
			get
			{
				return new Code.Div_Un_
				{
					opcode = OpCodes.Div_Un
				};
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0001025D File Offset: 0x0000E45D
		public static Code.Rem_ Rem
		{
			get
			{
				return new Code.Rem_
				{
					opcode = OpCodes.Rem
				};
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0001026F File Offset: 0x0000E46F
		public static Code.Rem_Un_ Rem_Un
		{
			get
			{
				return new Code.Rem_Un_
				{
					opcode = OpCodes.Rem_Un
				};
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00010281 File Offset: 0x0000E481
		public static Code.And_ And
		{
			get
			{
				return new Code.And_
				{
					opcode = OpCodes.And
				};
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00010293 File Offset: 0x0000E493
		public static Code.Or_ Or
		{
			get
			{
				return new Code.Or_
				{
					opcode = OpCodes.Or
				};
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000102A5 File Offset: 0x0000E4A5
		public static Code.Xor_ Xor
		{
			get
			{
				return new Code.Xor_
				{
					opcode = OpCodes.Xor
				};
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000102B7 File Offset: 0x0000E4B7
		public static Code.Shl_ Shl
		{
			get
			{
				return new Code.Shl_
				{
					opcode = OpCodes.Shl
				};
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000102C9 File Offset: 0x0000E4C9
		public static Code.Shr_ Shr
		{
			get
			{
				return new Code.Shr_
				{
					opcode = OpCodes.Shr
				};
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002AC RID: 684 RVA: 0x000102DB File Offset: 0x0000E4DB
		public static Code.Shr_Un_ Shr_Un
		{
			get
			{
				return new Code.Shr_Un_
				{
					opcode = OpCodes.Shr_Un
				};
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000102ED File Offset: 0x0000E4ED
		public static Code.Neg_ Neg
		{
			get
			{
				return new Code.Neg_
				{
					opcode = OpCodes.Neg
				};
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002AE RID: 686 RVA: 0x000102FF File Offset: 0x0000E4FF
		public static Code.Not_ Not
		{
			get
			{
				return new Code.Not_
				{
					opcode = OpCodes.Not
				};
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00010311 File Offset: 0x0000E511
		public static Code.Conv_I1_ Conv_I1
		{
			get
			{
				return new Code.Conv_I1_
				{
					opcode = OpCodes.Conv_I1
				};
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00010323 File Offset: 0x0000E523
		public static Code.Conv_I2_ Conv_I2
		{
			get
			{
				return new Code.Conv_I2_
				{
					opcode = OpCodes.Conv_I2
				};
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00010335 File Offset: 0x0000E535
		public static Code.Conv_I4_ Conv_I4
		{
			get
			{
				return new Code.Conv_I4_
				{
					opcode = OpCodes.Conv_I4
				};
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00010347 File Offset: 0x0000E547
		public static Code.Conv_I8_ Conv_I8
		{
			get
			{
				return new Code.Conv_I8_
				{
					opcode = OpCodes.Conv_I8
				};
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010359 File Offset: 0x0000E559
		public static Code.Conv_R4_ Conv_R4
		{
			get
			{
				return new Code.Conv_R4_
				{
					opcode = OpCodes.Conv_R4
				};
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0001036B File Offset: 0x0000E56B
		public static Code.Conv_R8_ Conv_R8
		{
			get
			{
				return new Code.Conv_R8_
				{
					opcode = OpCodes.Conv_R8
				};
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0001037D File Offset: 0x0000E57D
		public static Code.Conv_U4_ Conv_U4
		{
			get
			{
				return new Code.Conv_U4_
				{
					opcode = OpCodes.Conv_U4
				};
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001038F File Offset: 0x0000E58F
		public static Code.Conv_U8_ Conv_U8
		{
			get
			{
				return new Code.Conv_U8_
				{
					opcode = OpCodes.Conv_U8
				};
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000103A1 File Offset: 0x0000E5A1
		public static Code.Callvirt_ Callvirt
		{
			get
			{
				return new Code.Callvirt_
				{
					opcode = OpCodes.Callvirt
				};
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000103B3 File Offset: 0x0000E5B3
		public static Code.Cpobj_ Cpobj
		{
			get
			{
				return new Code.Cpobj_
				{
					opcode = OpCodes.Cpobj
				};
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000103C5 File Offset: 0x0000E5C5
		public static Code.Ldobj_ Ldobj
		{
			get
			{
				return new Code.Ldobj_
				{
					opcode = OpCodes.Ldobj
				};
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002BA RID: 698 RVA: 0x000103D7 File Offset: 0x0000E5D7
		public static Code.Ldstr_ Ldstr
		{
			get
			{
				return new Code.Ldstr_
				{
					opcode = OpCodes.Ldstr
				};
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000103E9 File Offset: 0x0000E5E9
		public static Code.Newobj_ Newobj
		{
			get
			{
				return new Code.Newobj_
				{
					opcode = OpCodes.Newobj
				};
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000103FB File Offset: 0x0000E5FB
		public static Code.Castclass_ Castclass
		{
			get
			{
				return new Code.Castclass_
				{
					opcode = OpCodes.Castclass
				};
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0001040D File Offset: 0x0000E60D
		public static Code.Isinst_ Isinst
		{
			get
			{
				return new Code.Isinst_
				{
					opcode = OpCodes.Isinst
				};
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0001041F File Offset: 0x0000E61F
		public static Code.Conv_R_Un_ Conv_R_Un
		{
			get
			{
				return new Code.Conv_R_Un_
				{
					opcode = OpCodes.Conv_R_Un
				};
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00010431 File Offset: 0x0000E631
		public static Code.Unbox_ Unbox
		{
			get
			{
				return new Code.Unbox_
				{
					opcode = OpCodes.Unbox
				};
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00010443 File Offset: 0x0000E643
		public static Code.Throw_ Throw
		{
			get
			{
				return new Code.Throw_
				{
					opcode = OpCodes.Throw
				};
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00010455 File Offset: 0x0000E655
		public static Code.Ldfld_ Ldfld
		{
			get
			{
				return new Code.Ldfld_
				{
					opcode = OpCodes.Ldfld
				};
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00010467 File Offset: 0x0000E667
		public static Code.Ldflda_ Ldflda
		{
			get
			{
				return new Code.Ldflda_
				{
					opcode = OpCodes.Ldflda
				};
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00010479 File Offset: 0x0000E679
		public static Code.Stfld_ Stfld
		{
			get
			{
				return new Code.Stfld_
				{
					opcode = OpCodes.Stfld
				};
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0001048B File Offset: 0x0000E68B
		public static Code.Ldsfld_ Ldsfld
		{
			get
			{
				return new Code.Ldsfld_
				{
					opcode = OpCodes.Ldsfld
				};
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0001049D File Offset: 0x0000E69D
		public static Code.Ldsflda_ Ldsflda
		{
			get
			{
				return new Code.Ldsflda_
				{
					opcode = OpCodes.Ldsflda
				};
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000104AF File Offset: 0x0000E6AF
		public static Code.Stsfld_ Stsfld
		{
			get
			{
				return new Code.Stsfld_
				{
					opcode = OpCodes.Stsfld
				};
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000104C1 File Offset: 0x0000E6C1
		public static Code.Stobj_ Stobj
		{
			get
			{
				return new Code.Stobj_
				{
					opcode = OpCodes.Stobj
				};
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000104D3 File Offset: 0x0000E6D3
		public static Code.Conv_Ovf_I1_Un_ Conv_Ovf_I1_Un
		{
			get
			{
				return new Code.Conv_Ovf_I1_Un_
				{
					opcode = OpCodes.Conv_Ovf_I1_Un
				};
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000104E5 File Offset: 0x0000E6E5
		public static Code.Conv_Ovf_I2_Un_ Conv_Ovf_I2_Un
		{
			get
			{
				return new Code.Conv_Ovf_I2_Un_
				{
					opcode = OpCodes.Conv_Ovf_I2_Un
				};
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public static Code.Conv_Ovf_I4_Un_ Conv_Ovf_I4_Un
		{
			get
			{
				return new Code.Conv_Ovf_I4_Un_
				{
					opcode = OpCodes.Conv_Ovf_I4_Un
				};
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00010509 File Offset: 0x0000E709
		public static Code.Conv_Ovf_I8_Un_ Conv_Ovf_I8_Un
		{
			get
			{
				return new Code.Conv_Ovf_I8_Un_
				{
					opcode = OpCodes.Conv_Ovf_I8_Un
				};
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0001051B File Offset: 0x0000E71B
		public static Code.Conv_Ovf_U1_Un_ Conv_Ovf_U1_Un
		{
			get
			{
				return new Code.Conv_Ovf_U1_Un_
				{
					opcode = OpCodes.Conv_Ovf_U1_Un
				};
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0001052D File Offset: 0x0000E72D
		public static Code.Conv_Ovf_U2_Un_ Conv_Ovf_U2_Un
		{
			get
			{
				return new Code.Conv_Ovf_U2_Un_
				{
					opcode = OpCodes.Conv_Ovf_U2_Un
				};
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0001053F File Offset: 0x0000E73F
		public static Code.Conv_Ovf_U4_Un_ Conv_Ovf_U4_Un
		{
			get
			{
				return new Code.Conv_Ovf_U4_Un_
				{
					opcode = OpCodes.Conv_Ovf_U4_Un
				};
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00010551 File Offset: 0x0000E751
		public static Code.Conv_Ovf_U8_Un_ Conv_Ovf_U8_Un
		{
			get
			{
				return new Code.Conv_Ovf_U8_Un_
				{
					opcode = OpCodes.Conv_Ovf_U8_Un
				};
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00010563 File Offset: 0x0000E763
		public static Code.Conv_Ovf_I_Un_ Conv_Ovf_I_Un
		{
			get
			{
				return new Code.Conv_Ovf_I_Un_
				{
					opcode = OpCodes.Conv_Ovf_I_Un
				};
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00010575 File Offset: 0x0000E775
		public static Code.Conv_Ovf_U_Un_ Conv_Ovf_U_Un
		{
			get
			{
				return new Code.Conv_Ovf_U_Un_
				{
					opcode = OpCodes.Conv_Ovf_U_Un
				};
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00010587 File Offset: 0x0000E787
		public static Code.Box_ Box
		{
			get
			{
				return new Code.Box_
				{
					opcode = OpCodes.Box
				};
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00010599 File Offset: 0x0000E799
		public static Code.Newarr_ Newarr
		{
			get
			{
				return new Code.Newarr_
				{
					opcode = OpCodes.Newarr
				};
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000105AB File Offset: 0x0000E7AB
		public static Code.Ldlen_ Ldlen
		{
			get
			{
				return new Code.Ldlen_
				{
					opcode = OpCodes.Ldlen
				};
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000105BD File Offset: 0x0000E7BD
		public static Code.Ldelema_ Ldelema
		{
			get
			{
				return new Code.Ldelema_
				{
					opcode = OpCodes.Ldelema
				};
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000105CF File Offset: 0x0000E7CF
		public static Code.Ldelem_I1_ Ldelem_I1
		{
			get
			{
				return new Code.Ldelem_I1_
				{
					opcode = OpCodes.Ldelem_I1
				};
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000105E1 File Offset: 0x0000E7E1
		public static Code.Ldelem_U1_ Ldelem_U1
		{
			get
			{
				return new Code.Ldelem_U1_
				{
					opcode = OpCodes.Ldelem_U1
				};
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x000105F3 File Offset: 0x0000E7F3
		public static Code.Ldelem_I2_ Ldelem_I2
		{
			get
			{
				return new Code.Ldelem_I2_
				{
					opcode = OpCodes.Ldelem_I2
				};
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00010605 File Offset: 0x0000E805
		public static Code.Ldelem_U2_ Ldelem_U2
		{
			get
			{
				return new Code.Ldelem_U2_
				{
					opcode = OpCodes.Ldelem_U2
				};
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00010617 File Offset: 0x0000E817
		public static Code.Ldelem_I4_ Ldelem_I4
		{
			get
			{
				return new Code.Ldelem_I4_
				{
					opcode = OpCodes.Ldelem_I4
				};
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00010629 File Offset: 0x0000E829
		public static Code.Ldelem_U4_ Ldelem_U4
		{
			get
			{
				return new Code.Ldelem_U4_
				{
					opcode = OpCodes.Ldelem_U4
				};
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0001063B File Offset: 0x0000E83B
		public static Code.Ldelem_I8_ Ldelem_I8
		{
			get
			{
				return new Code.Ldelem_I8_
				{
					opcode = OpCodes.Ldelem_I8
				};
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001064D File Offset: 0x0000E84D
		public static Code.Ldelem_I_ Ldelem_I
		{
			get
			{
				return new Code.Ldelem_I_
				{
					opcode = OpCodes.Ldelem_I
				};
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0001065F File Offset: 0x0000E85F
		public static Code.Ldelem_R4_ Ldelem_R4
		{
			get
			{
				return new Code.Ldelem_R4_
				{
					opcode = OpCodes.Ldelem_R4
				};
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00010671 File Offset: 0x0000E871
		public static Code.Ldelem_R8_ Ldelem_R8
		{
			get
			{
				return new Code.Ldelem_R8_
				{
					opcode = OpCodes.Ldelem_R8
				};
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00010683 File Offset: 0x0000E883
		public static Code.Ldelem_Ref_ Ldelem_Ref
		{
			get
			{
				return new Code.Ldelem_Ref_
				{
					opcode = OpCodes.Ldelem_Ref
				};
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00010695 File Offset: 0x0000E895
		public static Code.Stelem_I_ Stelem_I
		{
			get
			{
				return new Code.Stelem_I_
				{
					opcode = OpCodes.Stelem_I
				};
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000106A7 File Offset: 0x0000E8A7
		public static Code.Stelem_I1_ Stelem_I1
		{
			get
			{
				return new Code.Stelem_I1_
				{
					opcode = OpCodes.Stelem_I1
				};
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000106B9 File Offset: 0x0000E8B9
		public static Code.Stelem_I2_ Stelem_I2
		{
			get
			{
				return new Code.Stelem_I2_
				{
					opcode = OpCodes.Stelem_I2
				};
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000106CB File Offset: 0x0000E8CB
		public static Code.Stelem_I4_ Stelem_I4
		{
			get
			{
				return new Code.Stelem_I4_
				{
					opcode = OpCodes.Stelem_I4
				};
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000106DD File Offset: 0x0000E8DD
		public static Code.Stelem_I8_ Stelem_I8
		{
			get
			{
				return new Code.Stelem_I8_
				{
					opcode = OpCodes.Stelem_I8
				};
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000106EF File Offset: 0x0000E8EF
		public static Code.Stelem_R4_ Stelem_R4
		{
			get
			{
				return new Code.Stelem_R4_
				{
					opcode = OpCodes.Stelem_R4
				};
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00010701 File Offset: 0x0000E901
		public static Code.Stelem_R8_ Stelem_R8
		{
			get
			{
				return new Code.Stelem_R8_
				{
					opcode = OpCodes.Stelem_R8
				};
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00010713 File Offset: 0x0000E913
		public static Code.Stelem_Ref_ Stelem_Ref
		{
			get
			{
				return new Code.Stelem_Ref_
				{
					opcode = OpCodes.Stelem_Ref
				};
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00010725 File Offset: 0x0000E925
		public static Code.Ldelem_ Ldelem
		{
			get
			{
				return new Code.Ldelem_
				{
					opcode = OpCodes.Ldelem
				};
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00010737 File Offset: 0x0000E937
		public static Code.Stelem_ Stelem
		{
			get
			{
				return new Code.Stelem_
				{
					opcode = OpCodes.Stelem
				};
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00010749 File Offset: 0x0000E949
		public static Code.Unbox_Any_ Unbox_Any
		{
			get
			{
				return new Code.Unbox_Any_
				{
					opcode = OpCodes.Unbox_Any
				};
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0001075B File Offset: 0x0000E95B
		public static Code.Conv_Ovf_I1_ Conv_Ovf_I1
		{
			get
			{
				return new Code.Conv_Ovf_I1_
				{
					opcode = OpCodes.Conv_Ovf_I1
				};
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001076D File Offset: 0x0000E96D
		public static Code.Conv_Ovf_U1_ Conv_Ovf_U1
		{
			get
			{
				return new Code.Conv_Ovf_U1_
				{
					opcode = OpCodes.Conv_Ovf_U1
				};
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0001077F File Offset: 0x0000E97F
		public static Code.Conv_Ovf_I2_ Conv_Ovf_I2
		{
			get
			{
				return new Code.Conv_Ovf_I2_
				{
					opcode = OpCodes.Conv_Ovf_I2
				};
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00010791 File Offset: 0x0000E991
		public static Code.Conv_Ovf_U2_ Conv_Ovf_U2
		{
			get
			{
				return new Code.Conv_Ovf_U2_
				{
					opcode = OpCodes.Conv_Ovf_U2
				};
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000107A3 File Offset: 0x0000E9A3
		public static Code.Conv_Ovf_I4_ Conv_Ovf_I4
		{
			get
			{
				return new Code.Conv_Ovf_I4_
				{
					opcode = OpCodes.Conv_Ovf_I4
				};
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000107B5 File Offset: 0x0000E9B5
		public static Code.Conv_Ovf_U4_ Conv_Ovf_U4
		{
			get
			{
				return new Code.Conv_Ovf_U4_
				{
					opcode = OpCodes.Conv_Ovf_U4
				};
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000107C7 File Offset: 0x0000E9C7
		public static Code.Conv_Ovf_I8_ Conv_Ovf_I8
		{
			get
			{
				return new Code.Conv_Ovf_I8_
				{
					opcode = OpCodes.Conv_Ovf_I8
				};
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000107D9 File Offset: 0x0000E9D9
		public static Code.Conv_Ovf_U8_ Conv_Ovf_U8
		{
			get
			{
				return new Code.Conv_Ovf_U8_
				{
					opcode = OpCodes.Conv_Ovf_U8
				};
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000107EB File Offset: 0x0000E9EB
		public static Code.Refanyval_ Refanyval
		{
			get
			{
				return new Code.Refanyval_
				{
					opcode = OpCodes.Refanyval
				};
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000107FD File Offset: 0x0000E9FD
		public static Code.Ckfinite_ Ckfinite
		{
			get
			{
				return new Code.Ckfinite_
				{
					opcode = OpCodes.Ckfinite
				};
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0001080F File Offset: 0x0000EA0F
		public static Code.Mkrefany_ Mkrefany
		{
			get
			{
				return new Code.Mkrefany_
				{
					opcode = OpCodes.Mkrefany
				};
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00010821 File Offset: 0x0000EA21
		public static Code.Ldtoken_ Ldtoken
		{
			get
			{
				return new Code.Ldtoken_
				{
					opcode = OpCodes.Ldtoken
				};
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00010833 File Offset: 0x0000EA33
		public static Code.Conv_U2_ Conv_U2
		{
			get
			{
				return new Code.Conv_U2_
				{
					opcode = OpCodes.Conv_U2
				};
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00010845 File Offset: 0x0000EA45
		public static Code.Conv_U1_ Conv_U1
		{
			get
			{
				return new Code.Conv_U1_
				{
					opcode = OpCodes.Conv_U1
				};
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00010857 File Offset: 0x0000EA57
		public static Code.Conv_I_ Conv_I
		{
			get
			{
				return new Code.Conv_I_
				{
					opcode = OpCodes.Conv_I
				};
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00010869 File Offset: 0x0000EA69
		public static Code.Conv_Ovf_I_ Conv_Ovf_I
		{
			get
			{
				return new Code.Conv_Ovf_I_
				{
					opcode = OpCodes.Conv_Ovf_I
				};
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001087B File Offset: 0x0000EA7B
		public static Code.Conv_Ovf_U_ Conv_Ovf_U
		{
			get
			{
				return new Code.Conv_Ovf_U_
				{
					opcode = OpCodes.Conv_Ovf_U
				};
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0001088D File Offset: 0x0000EA8D
		public static Code.Add_Ovf_ Add_Ovf
		{
			get
			{
				return new Code.Add_Ovf_
				{
					opcode = OpCodes.Add_Ovf
				};
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0001089F File Offset: 0x0000EA9F
		public static Code.Add_Ovf_Un_ Add_Ovf_Un
		{
			get
			{
				return new Code.Add_Ovf_Un_
				{
					opcode = OpCodes.Add_Ovf_Un
				};
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000108B1 File Offset: 0x0000EAB1
		public static Code.Mul_Ovf_ Mul_Ovf
		{
			get
			{
				return new Code.Mul_Ovf_
				{
					opcode = OpCodes.Mul_Ovf
				};
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000300 RID: 768 RVA: 0x000108C3 File Offset: 0x0000EAC3
		public static Code.Mul_Ovf_Un_ Mul_Ovf_Un
		{
			get
			{
				return new Code.Mul_Ovf_Un_
				{
					opcode = OpCodes.Mul_Ovf_Un
				};
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000108D5 File Offset: 0x0000EAD5
		public static Code.Sub_Ovf_ Sub_Ovf
		{
			get
			{
				return new Code.Sub_Ovf_
				{
					opcode = OpCodes.Sub_Ovf
				};
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000108E7 File Offset: 0x0000EAE7
		public static Code.Sub_Ovf_Un_ Sub_Ovf_Un
		{
			get
			{
				return new Code.Sub_Ovf_Un_
				{
					opcode = OpCodes.Sub_Ovf_Un
				};
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000303 RID: 771 RVA: 0x000108F9 File Offset: 0x0000EAF9
		public static Code.Endfinally_ Endfinally
		{
			get
			{
				return new Code.Endfinally_
				{
					opcode = OpCodes.Endfinally
				};
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001090B File Offset: 0x0000EB0B
		public static Code.Leave_ Leave
		{
			get
			{
				return new Code.Leave_
				{
					opcode = OpCodes.Leave
				};
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0001091D File Offset: 0x0000EB1D
		public static Code.Leave_S_ Leave_S
		{
			get
			{
				return new Code.Leave_S_
				{
					opcode = OpCodes.Leave_S
				};
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001092F File Offset: 0x0000EB2F
		public static Code.Stind_I_ Stind_I
		{
			get
			{
				return new Code.Stind_I_
				{
					opcode = OpCodes.Stind_I
				};
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00010941 File Offset: 0x0000EB41
		public static Code.Conv_U_ Conv_U
		{
			get
			{
				return new Code.Conv_U_
				{
					opcode = OpCodes.Conv_U
				};
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00010953 File Offset: 0x0000EB53
		public static Code.Prefix7_ Prefix7
		{
			get
			{
				return new Code.Prefix7_
				{
					opcode = OpCodes.Prefix7
				};
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00010965 File Offset: 0x0000EB65
		public static Code.Prefix6_ Prefix6
		{
			get
			{
				return new Code.Prefix6_
				{
					opcode = OpCodes.Prefix6
				};
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00010977 File Offset: 0x0000EB77
		public static Code.Prefix5_ Prefix5
		{
			get
			{
				return new Code.Prefix5_
				{
					opcode = OpCodes.Prefix5
				};
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00010989 File Offset: 0x0000EB89
		public static Code.Prefix4_ Prefix4
		{
			get
			{
				return new Code.Prefix4_
				{
					opcode = OpCodes.Prefix4
				};
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0001099B File Offset: 0x0000EB9B
		public static Code.Prefix3_ Prefix3
		{
			get
			{
				return new Code.Prefix3_
				{
					opcode = OpCodes.Prefix3
				};
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000109AD File Offset: 0x0000EBAD
		public static Code.Prefix2_ Prefix2
		{
			get
			{
				return new Code.Prefix2_
				{
					opcode = OpCodes.Prefix2
				};
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600030E RID: 782 RVA: 0x000109BF File Offset: 0x0000EBBF
		public static Code.Prefix1_ Prefix1
		{
			get
			{
				return new Code.Prefix1_
				{
					opcode = OpCodes.Prefix1
				};
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600030F RID: 783 RVA: 0x000109D1 File Offset: 0x0000EBD1
		public static Code.Prefixref_ Prefixref
		{
			get
			{
				return new Code.Prefixref_
				{
					opcode = OpCodes.Prefixref
				};
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000310 RID: 784 RVA: 0x000109E3 File Offset: 0x0000EBE3
		public static Code.Arglist_ Arglist
		{
			get
			{
				return new Code.Arglist_
				{
					opcode = OpCodes.Arglist
				};
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000311 RID: 785 RVA: 0x000109F5 File Offset: 0x0000EBF5
		public static Code.Ceq_ Ceq
		{
			get
			{
				return new Code.Ceq_
				{
					opcode = OpCodes.Ceq
				};
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00010A07 File Offset: 0x0000EC07
		public static Code.Cgt_ Cgt
		{
			get
			{
				return new Code.Cgt_
				{
					opcode = OpCodes.Cgt
				};
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00010A19 File Offset: 0x0000EC19
		public static Code.Cgt_Un_ Cgt_Un
		{
			get
			{
				return new Code.Cgt_Un_
				{
					opcode = OpCodes.Cgt_Un
				};
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00010A2B File Offset: 0x0000EC2B
		public static Code.Clt_ Clt
		{
			get
			{
				return new Code.Clt_
				{
					opcode = OpCodes.Clt
				};
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00010A3D File Offset: 0x0000EC3D
		public static Code.Clt_Un_ Clt_Un
		{
			get
			{
				return new Code.Clt_Un_
				{
					opcode = OpCodes.Clt_Un
				};
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00010A4F File Offset: 0x0000EC4F
		public static Code.Ldftn_ Ldftn
		{
			get
			{
				return new Code.Ldftn_
				{
					opcode = OpCodes.Ldftn
				};
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00010A61 File Offset: 0x0000EC61
		public static Code.Ldvirtftn_ Ldvirtftn
		{
			get
			{
				return new Code.Ldvirtftn_
				{
					opcode = OpCodes.Ldvirtftn
				};
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00010A73 File Offset: 0x0000EC73
		public static Code.Ldarg_ Ldarg
		{
			get
			{
				return new Code.Ldarg_
				{
					opcode = OpCodes.Ldarg
				};
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00010A85 File Offset: 0x0000EC85
		public static Code.Ldarga_ Ldarga
		{
			get
			{
				return new Code.Ldarga_
				{
					opcode = OpCodes.Ldarga
				};
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00010A97 File Offset: 0x0000EC97
		public static Code.Starg_ Starg
		{
			get
			{
				return new Code.Starg_
				{
					opcode = OpCodes.Starg
				};
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		public static Code.Ldloc_ Ldloc
		{
			get
			{
				return new Code.Ldloc_
				{
					opcode = OpCodes.Ldloc
				};
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00010ABB File Offset: 0x0000ECBB
		public static Code.Ldloca_ Ldloca
		{
			get
			{
				return new Code.Ldloca_
				{
					opcode = OpCodes.Ldloca
				};
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00010ACD File Offset: 0x0000ECCD
		public static Code.Stloc_ Stloc
		{
			get
			{
				return new Code.Stloc_
				{
					opcode = OpCodes.Stloc
				};
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00010ADF File Offset: 0x0000ECDF
		public static Code.Localloc_ Localloc
		{
			get
			{
				return new Code.Localloc_
				{
					opcode = OpCodes.Localloc
				};
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00010AF1 File Offset: 0x0000ECF1
		public static Code.Endfilter_ Endfilter
		{
			get
			{
				return new Code.Endfilter_
				{
					opcode = OpCodes.Endfilter
				};
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00010B03 File Offset: 0x0000ED03
		public static Code.Unaligned_ Unaligned
		{
			get
			{
				return new Code.Unaligned_
				{
					opcode = OpCodes.Unaligned
				};
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00010B15 File Offset: 0x0000ED15
		public static Code.Volatile_ Volatile
		{
			get
			{
				return new Code.Volatile_
				{
					opcode = OpCodes.Volatile
				};
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00010B27 File Offset: 0x0000ED27
		public static Code.Tailcall_ Tailcall
		{
			get
			{
				return new Code.Tailcall_
				{
					opcode = OpCodes.Tailcall
				};
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00010B39 File Offset: 0x0000ED39
		public static Code.Initobj_ Initobj
		{
			get
			{
				return new Code.Initobj_
				{
					opcode = OpCodes.Initobj
				};
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00010B4B File Offset: 0x0000ED4B
		public static Code.Constrained_ Constrained
		{
			get
			{
				return new Code.Constrained_
				{
					opcode = OpCodes.Constrained
				};
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00010B5D File Offset: 0x0000ED5D
		public static Code.Cpblk_ Cpblk
		{
			get
			{
				return new Code.Cpblk_
				{
					opcode = OpCodes.Cpblk
				};
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010B6F File Offset: 0x0000ED6F
		public static Code.Initblk_ Initblk
		{
			get
			{
				return new Code.Initblk_
				{
					opcode = OpCodes.Initblk
				};
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00010B81 File Offset: 0x0000ED81
		public static Code.Rethrow_ Rethrow
		{
			get
			{
				return new Code.Rethrow_
				{
					opcode = OpCodes.Rethrow
				};
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010B93 File Offset: 0x0000ED93
		public static Code.Sizeof_ Sizeof
		{
			get
			{
				return new Code.Sizeof_
				{
					opcode = OpCodes.Sizeof
				};
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00010BA5 File Offset: 0x0000EDA5
		public static Code.Refanytype_ Refanytype
		{
			get
			{
				return new Code.Refanytype_
				{
					opcode = OpCodes.Refanytype
				};
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010BB7 File Offset: 0x0000EDB7
		public static Code.Readonly_ Readonly
		{
			get
			{
				return new Code.Readonly_
				{
					opcode = OpCodes.Readonly
				};
			}
		}

		// Token: 0x020000B7 RID: 183
		public class Operand_ : CodeMatch
		{
			// Token: 0x17000108 RID: 264
			public Code.Operand_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Operand_)base.Set(operand, name);
				}
			}

			// Token: 0x06000560 RID: 1376 RVA: 0x00017AE0 File Offset: 0x00015CE0
			public Operand_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000B8 RID: 184
		public class Nop_ : CodeMatch
		{
			// Token: 0x17000109 RID: 265
			public Code.Nop_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Nop_)base.Set(OpCodes.Nop, operand, name);
				}
			}

			// Token: 0x06000562 RID: 1378 RVA: 0x00017B14 File Offset: 0x00015D14
			public Nop_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000B9 RID: 185
		public class Break_ : CodeMatch
		{
			// Token: 0x1700010A RID: 266
			public Code.Break_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Break_)base.Set(OpCodes.Break, operand, name);
				}
			}

			// Token: 0x06000564 RID: 1380 RVA: 0x00017B48 File Offset: 0x00015D48
			public Break_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BA RID: 186
		public class Ldarg_0_ : CodeMatch
		{
			// Token: 0x1700010B RID: 267
			public Code.Ldarg_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_0_)base.Set(OpCodes.Ldarg_0, operand, name);
				}
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x00017B7C File Offset: 0x00015D7C
			public Ldarg_0_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BB RID: 187
		public class Ldarg_1_ : CodeMatch
		{
			// Token: 0x1700010C RID: 268
			public Code.Ldarg_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_1_)base.Set(OpCodes.Ldarg_1, operand, name);
				}
			}

			// Token: 0x06000568 RID: 1384 RVA: 0x00017BB0 File Offset: 0x00015DB0
			public Ldarg_1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BC RID: 188
		public class Ldarg_2_ : CodeMatch
		{
			// Token: 0x1700010D RID: 269
			public Code.Ldarg_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_2_)base.Set(OpCodes.Ldarg_2, operand, name);
				}
			}

			// Token: 0x0600056A RID: 1386 RVA: 0x00017BE4 File Offset: 0x00015DE4
			public Ldarg_2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BD RID: 189
		public class Ldarg_3_ : CodeMatch
		{
			// Token: 0x1700010E RID: 270
			public Code.Ldarg_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_3_)base.Set(OpCodes.Ldarg_3, operand, name);
				}
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x00017C18 File Offset: 0x00015E18
			public Ldarg_3_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BE RID: 190
		public class Ldloc_0_ : CodeMatch
		{
			// Token: 0x1700010F RID: 271
			public Code.Ldloc_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_0_)base.Set(OpCodes.Ldloc_0, operand, name);
				}
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x00017C4C File Offset: 0x00015E4C
			public Ldloc_0_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000BF RID: 191
		public class Ldloc_1_ : CodeMatch
		{
			// Token: 0x17000110 RID: 272
			public Code.Ldloc_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_1_)base.Set(OpCodes.Ldloc_1, operand, name);
				}
			}

			// Token: 0x06000570 RID: 1392 RVA: 0x00017C80 File Offset: 0x00015E80
			public Ldloc_1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C0 RID: 192
		public class Ldloc_2_ : CodeMatch
		{
			// Token: 0x17000111 RID: 273
			public Code.Ldloc_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_2_)base.Set(OpCodes.Ldloc_2, operand, name);
				}
			}

			// Token: 0x06000572 RID: 1394 RVA: 0x00017CB4 File Offset: 0x00015EB4
			public Ldloc_2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C1 RID: 193
		public class Ldloc_3_ : CodeMatch
		{
			// Token: 0x17000112 RID: 274
			public Code.Ldloc_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_3_)base.Set(OpCodes.Ldloc_3, operand, name);
				}
			}

			// Token: 0x06000574 RID: 1396 RVA: 0x00017CE8 File Offset: 0x00015EE8
			public Ldloc_3_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C2 RID: 194
		public class Stloc_0_ : CodeMatch
		{
			// Token: 0x17000113 RID: 275
			public Code.Stloc_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_0_)base.Set(OpCodes.Stloc_0, operand, name);
				}
			}

			// Token: 0x06000576 RID: 1398 RVA: 0x00017D1C File Offset: 0x00015F1C
			public Stloc_0_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C3 RID: 195
		public class Stloc_1_ : CodeMatch
		{
			// Token: 0x17000114 RID: 276
			public Code.Stloc_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_1_)base.Set(OpCodes.Stloc_1, operand, name);
				}
			}

			// Token: 0x06000578 RID: 1400 RVA: 0x00017D50 File Offset: 0x00015F50
			public Stloc_1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C4 RID: 196
		public class Stloc_2_ : CodeMatch
		{
			// Token: 0x17000115 RID: 277
			public Code.Stloc_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_2_)base.Set(OpCodes.Stloc_2, operand, name);
				}
			}

			// Token: 0x0600057A RID: 1402 RVA: 0x00017D84 File Offset: 0x00015F84
			public Stloc_2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C5 RID: 197
		public class Stloc_3_ : CodeMatch
		{
			// Token: 0x17000116 RID: 278
			public Code.Stloc_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_3_)base.Set(OpCodes.Stloc_3, operand, name);
				}
			}

			// Token: 0x0600057C RID: 1404 RVA: 0x00017DB8 File Offset: 0x00015FB8
			public Stloc_3_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C6 RID: 198
		public class Ldarg_S_ : CodeMatch
		{
			// Token: 0x17000117 RID: 279
			public Code.Ldarg_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_S_)base.Set(OpCodes.Ldarg_S, operand, name);
				}
			}

			// Token: 0x0600057E RID: 1406 RVA: 0x00017DEC File Offset: 0x00015FEC
			public Ldarg_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C7 RID: 199
		public class Ldarga_S_ : CodeMatch
		{
			// Token: 0x17000118 RID: 280
			public Code.Ldarga_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarga_S_)base.Set(OpCodes.Ldarga_S, operand, name);
				}
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x00017E20 File Offset: 0x00016020
			public Ldarga_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C8 RID: 200
		public class Starg_S_ : CodeMatch
		{
			// Token: 0x17000119 RID: 281
			public Code.Starg_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Starg_S_)base.Set(OpCodes.Starg_S, operand, name);
				}
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00017E54 File Offset: 0x00016054
			public Starg_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000C9 RID: 201
		public class Ldloc_S_ : CodeMatch
		{
			// Token: 0x1700011A RID: 282
			public Code.Ldloc_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_S_)base.Set(OpCodes.Ldloc_S, operand, name);
				}
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x00017E88 File Offset: 0x00016088
			public Ldloc_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CA RID: 202
		public class Ldloca_S_ : CodeMatch
		{
			// Token: 0x1700011B RID: 283
			public Code.Ldloca_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloca_S_)base.Set(OpCodes.Ldloca_S, operand, name);
				}
			}

			// Token: 0x06000586 RID: 1414 RVA: 0x00017EBC File Offset: 0x000160BC
			public Ldloca_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CB RID: 203
		public class Stloc_S_ : CodeMatch
		{
			// Token: 0x1700011C RID: 284
			public Code.Stloc_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_S_)base.Set(OpCodes.Stloc_S, operand, name);
				}
			}

			// Token: 0x06000588 RID: 1416 RVA: 0x00017EF0 File Offset: 0x000160F0
			public Stloc_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CC RID: 204
		public class Ldnull_ : CodeMatch
		{
			// Token: 0x1700011D RID: 285
			public Code.Ldnull_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldnull_)base.Set(OpCodes.Ldnull, operand, name);
				}
			}

			// Token: 0x0600058A RID: 1418 RVA: 0x00017F24 File Offset: 0x00016124
			public Ldnull_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CD RID: 205
		public class Ldc_I4_M1_ : CodeMatch
		{
			// Token: 0x1700011E RID: 286
			public Code.Ldc_I4_M1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_M1_)base.Set(OpCodes.Ldc_I4_M1, operand, name);
				}
			}

			// Token: 0x0600058C RID: 1420 RVA: 0x00017F58 File Offset: 0x00016158
			public Ldc_I4_M1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CE RID: 206
		public class Ldc_I4_0_ : CodeMatch
		{
			// Token: 0x1700011F RID: 287
			public Code.Ldc_I4_0_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_0_)base.Set(OpCodes.Ldc_I4_0, operand, name);
				}
			}

			// Token: 0x0600058E RID: 1422 RVA: 0x00017F8C File Offset: 0x0001618C
			public Ldc_I4_0_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000CF RID: 207
		public class Ldc_I4_1_ : CodeMatch
		{
			// Token: 0x17000120 RID: 288
			public Code.Ldc_I4_1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_1_)base.Set(OpCodes.Ldc_I4_1, operand, name);
				}
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x00017FC0 File Offset: 0x000161C0
			public Ldc_I4_1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D0 RID: 208
		public class Ldc_I4_2_ : CodeMatch
		{
			// Token: 0x17000121 RID: 289
			public Code.Ldc_I4_2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_2_)base.Set(OpCodes.Ldc_I4_2, operand, name);
				}
			}

			// Token: 0x06000592 RID: 1426 RVA: 0x00017FF4 File Offset: 0x000161F4
			public Ldc_I4_2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D1 RID: 209
		public class Ldc_I4_3_ : CodeMatch
		{
			// Token: 0x17000122 RID: 290
			public Code.Ldc_I4_3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_3_)base.Set(OpCodes.Ldc_I4_3, operand, name);
				}
			}

			// Token: 0x06000594 RID: 1428 RVA: 0x00018028 File Offset: 0x00016228
			public Ldc_I4_3_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D2 RID: 210
		public class Ldc_I4_4_ : CodeMatch
		{
			// Token: 0x17000123 RID: 291
			public Code.Ldc_I4_4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_4_)base.Set(OpCodes.Ldc_I4_4, operand, name);
				}
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0001805C File Offset: 0x0001625C
			public Ldc_I4_4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D3 RID: 211
		public class Ldc_I4_5_ : CodeMatch
		{
			// Token: 0x17000124 RID: 292
			public Code.Ldc_I4_5_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_5_)base.Set(OpCodes.Ldc_I4_5, operand, name);
				}
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x00018090 File Offset: 0x00016290
			public Ldc_I4_5_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D4 RID: 212
		public class Ldc_I4_6_ : CodeMatch
		{
			// Token: 0x17000125 RID: 293
			public Code.Ldc_I4_6_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_6_)base.Set(OpCodes.Ldc_I4_6, operand, name);
				}
			}

			// Token: 0x0600059A RID: 1434 RVA: 0x000180C4 File Offset: 0x000162C4
			public Ldc_I4_6_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D5 RID: 213
		public class Ldc_I4_7_ : CodeMatch
		{
			// Token: 0x17000126 RID: 294
			public Code.Ldc_I4_7_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_7_)base.Set(OpCodes.Ldc_I4_7, operand, name);
				}
			}

			// Token: 0x0600059C RID: 1436 RVA: 0x000180F8 File Offset: 0x000162F8
			public Ldc_I4_7_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D6 RID: 214
		public class Ldc_I4_8_ : CodeMatch
		{
			// Token: 0x17000127 RID: 295
			public Code.Ldc_I4_8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_8_)base.Set(OpCodes.Ldc_I4_8, operand, name);
				}
			}

			// Token: 0x0600059E RID: 1438 RVA: 0x0001812C File Offset: 0x0001632C
			public Ldc_I4_8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D7 RID: 215
		public class Ldc_I4_S_ : CodeMatch
		{
			// Token: 0x17000128 RID: 296
			public Code.Ldc_I4_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_S_)base.Set(OpCodes.Ldc_I4_S, operand, name);
				}
			}

			// Token: 0x060005A0 RID: 1440 RVA: 0x00018160 File Offset: 0x00016360
			public Ldc_I4_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D8 RID: 216
		public class Ldc_I4_ : CodeMatch
		{
			// Token: 0x17000129 RID: 297
			public Code.Ldc_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I4_)base.Set(OpCodes.Ldc_I4, operand, name);
				}
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x00018194 File Offset: 0x00016394
			public Ldc_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000D9 RID: 217
		public class Ldc_I8_ : CodeMatch
		{
			// Token: 0x1700012A RID: 298
			public Code.Ldc_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_I8_)base.Set(OpCodes.Ldc_I8, operand, name);
				}
			}

			// Token: 0x060005A4 RID: 1444 RVA: 0x000181C8 File Offset: 0x000163C8
			public Ldc_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DA RID: 218
		public class Ldc_R4_ : CodeMatch
		{
			// Token: 0x1700012B RID: 299
			public Code.Ldc_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_R4_)base.Set(OpCodes.Ldc_R4, operand, name);
				}
			}

			// Token: 0x060005A6 RID: 1446 RVA: 0x000181FC File Offset: 0x000163FC
			public Ldc_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DB RID: 219
		public class Ldc_R8_ : CodeMatch
		{
			// Token: 0x1700012C RID: 300
			public Code.Ldc_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldc_R8_)base.Set(OpCodes.Ldc_R8, operand, name);
				}
			}

			// Token: 0x060005A8 RID: 1448 RVA: 0x00018230 File Offset: 0x00016430
			public Ldc_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DC RID: 220
		public class Dup_ : CodeMatch
		{
			// Token: 0x1700012D RID: 301
			public Code.Dup_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Dup_)base.Set(OpCodes.Dup, operand, name);
				}
			}

			// Token: 0x060005AA RID: 1450 RVA: 0x00018264 File Offset: 0x00016464
			public Dup_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DD RID: 221
		public class Pop_ : CodeMatch
		{
			// Token: 0x1700012E RID: 302
			public Code.Pop_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Pop_)base.Set(OpCodes.Pop, operand, name);
				}
			}

			// Token: 0x060005AC RID: 1452 RVA: 0x00018298 File Offset: 0x00016498
			public Pop_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DE RID: 222
		public class Jmp_ : CodeMatch
		{
			// Token: 0x1700012F RID: 303
			public Code.Jmp_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Jmp_)base.Set(OpCodes.Jmp, operand, name);
				}
			}

			// Token: 0x060005AE RID: 1454 RVA: 0x000182CC File Offset: 0x000164CC
			public Jmp_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000DF RID: 223
		public class Call_ : CodeMatch
		{
			// Token: 0x17000130 RID: 304
			public Code.Call_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Call_)base.Set(OpCodes.Call, operand, name);
				}
			}

			// Token: 0x060005B0 RID: 1456 RVA: 0x00018300 File Offset: 0x00016500
			public Call_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E0 RID: 224
		public class Calli_ : CodeMatch
		{
			// Token: 0x17000131 RID: 305
			public Code.Calli_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Calli_)base.Set(OpCodes.Calli, operand, name);
				}
			}

			// Token: 0x060005B2 RID: 1458 RVA: 0x00018334 File Offset: 0x00016534
			public Calli_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E1 RID: 225
		public class Ret_ : CodeMatch
		{
			// Token: 0x17000132 RID: 306
			public Code.Ret_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ret_)base.Set(OpCodes.Ret, operand, name);
				}
			}

			// Token: 0x060005B4 RID: 1460 RVA: 0x00018368 File Offset: 0x00016568
			public Ret_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E2 RID: 226
		public class Br_S_ : CodeMatch
		{
			// Token: 0x17000133 RID: 307
			public Code.Br_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Br_S_)base.Set(OpCodes.Br_S, operand, name);
				}
			}

			// Token: 0x060005B6 RID: 1462 RVA: 0x0001839C File Offset: 0x0001659C
			public Br_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E3 RID: 227
		public class Brfalse_S_ : CodeMatch
		{
			// Token: 0x17000134 RID: 308
			public Code.Brfalse_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brfalse_S_)base.Set(OpCodes.Brfalse_S, operand, name);
				}
			}

			// Token: 0x060005B8 RID: 1464 RVA: 0x000183D0 File Offset: 0x000165D0
			public Brfalse_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E4 RID: 228
		public class Brtrue_S_ : CodeMatch
		{
			// Token: 0x17000135 RID: 309
			public Code.Brtrue_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brtrue_S_)base.Set(OpCodes.Brtrue_S, operand, name);
				}
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x00018404 File Offset: 0x00016604
			public Brtrue_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E5 RID: 229
		public class Beq_S_ : CodeMatch
		{
			// Token: 0x17000136 RID: 310
			public Code.Beq_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Beq_S_)base.Set(OpCodes.Beq_S, operand, name);
				}
			}

			// Token: 0x060005BC RID: 1468 RVA: 0x00018438 File Offset: 0x00016638
			public Beq_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E6 RID: 230
		public class Bge_S_ : CodeMatch
		{
			// Token: 0x17000137 RID: 311
			public Code.Bge_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_S_)base.Set(OpCodes.Bge_S, operand, name);
				}
			}

			// Token: 0x060005BE RID: 1470 RVA: 0x0001846C File Offset: 0x0001666C
			public Bge_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E7 RID: 231
		public class Bgt_S_ : CodeMatch
		{
			// Token: 0x17000138 RID: 312
			public Code.Bgt_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_S_)base.Set(OpCodes.Bgt_S, operand, name);
				}
			}

			// Token: 0x060005C0 RID: 1472 RVA: 0x000184A0 File Offset: 0x000166A0
			public Bgt_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E8 RID: 232
		public class Ble_S_ : CodeMatch
		{
			// Token: 0x17000139 RID: 313
			public Code.Ble_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_S_)base.Set(OpCodes.Ble_S, operand, name);
				}
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x000184D4 File Offset: 0x000166D4
			public Ble_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000E9 RID: 233
		public class Blt_S_ : CodeMatch
		{
			// Token: 0x1700013A RID: 314
			public Code.Blt_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_S_)base.Set(OpCodes.Blt_S, operand, name);
				}
			}

			// Token: 0x060005C4 RID: 1476 RVA: 0x00018508 File Offset: 0x00016708
			public Blt_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000EA RID: 234
		public class Bne_Un_S_ : CodeMatch
		{
			// Token: 0x1700013B RID: 315
			public Code.Bne_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bne_Un_S_)base.Set(OpCodes.Bne_Un_S, operand, name);
				}
			}

			// Token: 0x060005C6 RID: 1478 RVA: 0x0001853C File Offset: 0x0001673C
			public Bne_Un_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000EB RID: 235
		public class Bge_Un_S_ : CodeMatch
		{
			// Token: 0x1700013C RID: 316
			public Code.Bge_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_Un_S_)base.Set(OpCodes.Bge_Un_S, operand, name);
				}
			}

			// Token: 0x060005C8 RID: 1480 RVA: 0x00018570 File Offset: 0x00016770
			public Bge_Un_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000EC RID: 236
		public class Bgt_Un_S_ : CodeMatch
		{
			// Token: 0x1700013D RID: 317
			public Code.Bgt_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_Un_S_)base.Set(OpCodes.Bgt_Un_S, operand, name);
				}
			}

			// Token: 0x060005CA RID: 1482 RVA: 0x000185A4 File Offset: 0x000167A4
			public Bgt_Un_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000ED RID: 237
		public class Ble_Un_S_ : CodeMatch
		{
			// Token: 0x1700013E RID: 318
			public Code.Ble_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_Un_S_)base.Set(OpCodes.Ble_Un_S, operand, name);
				}
			}

			// Token: 0x060005CC RID: 1484 RVA: 0x000185D8 File Offset: 0x000167D8
			public Ble_Un_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000EE RID: 238
		public class Blt_Un_S_ : CodeMatch
		{
			// Token: 0x1700013F RID: 319
			public Code.Blt_Un_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_Un_S_)base.Set(OpCodes.Blt_Un_S, operand, name);
				}
			}

			// Token: 0x060005CE RID: 1486 RVA: 0x0001860C File Offset: 0x0001680C
			public Blt_Un_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000EF RID: 239
		public class Br_ : CodeMatch
		{
			// Token: 0x17000140 RID: 320
			public Code.Br_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Br_)base.Set(OpCodes.Br, operand, name);
				}
			}

			// Token: 0x060005D0 RID: 1488 RVA: 0x00018640 File Offset: 0x00016840
			public Br_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F0 RID: 240
		public class Brfalse_ : CodeMatch
		{
			// Token: 0x17000141 RID: 321
			public Code.Brfalse_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brfalse_)base.Set(OpCodes.Brfalse, operand, name);
				}
			}

			// Token: 0x060005D2 RID: 1490 RVA: 0x00018674 File Offset: 0x00016874
			public Brfalse_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F1 RID: 241
		public class Brtrue_ : CodeMatch
		{
			// Token: 0x17000142 RID: 322
			public Code.Brtrue_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Brtrue_)base.Set(OpCodes.Brtrue, operand, name);
				}
			}

			// Token: 0x060005D4 RID: 1492 RVA: 0x000186A8 File Offset: 0x000168A8
			public Brtrue_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F2 RID: 242
		public class Beq_ : CodeMatch
		{
			// Token: 0x17000143 RID: 323
			public Code.Beq_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Beq_)base.Set(OpCodes.Beq, operand, name);
				}
			}

			// Token: 0x060005D6 RID: 1494 RVA: 0x000186DC File Offset: 0x000168DC
			public Beq_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F3 RID: 243
		public class Bge_ : CodeMatch
		{
			// Token: 0x17000144 RID: 324
			public Code.Bge_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_)base.Set(OpCodes.Bge, operand, name);
				}
			}

			// Token: 0x060005D8 RID: 1496 RVA: 0x00018710 File Offset: 0x00016910
			public Bge_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F4 RID: 244
		public class Bgt_ : CodeMatch
		{
			// Token: 0x17000145 RID: 325
			public Code.Bgt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_)base.Set(OpCodes.Bgt, operand, name);
				}
			}

			// Token: 0x060005DA RID: 1498 RVA: 0x00018744 File Offset: 0x00016944
			public Bgt_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F5 RID: 245
		public class Ble_ : CodeMatch
		{
			// Token: 0x17000146 RID: 326
			public Code.Ble_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_)base.Set(OpCodes.Ble, operand, name);
				}
			}

			// Token: 0x060005DC RID: 1500 RVA: 0x00018778 File Offset: 0x00016978
			public Ble_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F6 RID: 246
		public class Blt_ : CodeMatch
		{
			// Token: 0x17000147 RID: 327
			public Code.Blt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_)base.Set(OpCodes.Blt, operand, name);
				}
			}

			// Token: 0x060005DE RID: 1502 RVA: 0x000187AC File Offset: 0x000169AC
			public Blt_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F7 RID: 247
		public class Bne_Un_ : CodeMatch
		{
			// Token: 0x17000148 RID: 328
			public Code.Bne_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bne_Un_)base.Set(OpCodes.Bne_Un, operand, name);
				}
			}

			// Token: 0x060005E0 RID: 1504 RVA: 0x000187E0 File Offset: 0x000169E0
			public Bne_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F8 RID: 248
		public class Bge_Un_ : CodeMatch
		{
			// Token: 0x17000149 RID: 329
			public Code.Bge_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bge_Un_)base.Set(OpCodes.Bge_Un, operand, name);
				}
			}

			// Token: 0x060005E2 RID: 1506 RVA: 0x00018814 File Offset: 0x00016A14
			public Bge_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000F9 RID: 249
		public class Bgt_Un_ : CodeMatch
		{
			// Token: 0x1700014A RID: 330
			public Code.Bgt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Bgt_Un_)base.Set(OpCodes.Bgt_Un, operand, name);
				}
			}

			// Token: 0x060005E4 RID: 1508 RVA: 0x00018848 File Offset: 0x00016A48
			public Bgt_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FA RID: 250
		public class Ble_Un_ : CodeMatch
		{
			// Token: 0x1700014B RID: 331
			public Code.Ble_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ble_Un_)base.Set(OpCodes.Ble_Un, operand, name);
				}
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x0001887C File Offset: 0x00016A7C
			public Ble_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FB RID: 251
		public class Blt_Un_ : CodeMatch
		{
			// Token: 0x1700014C RID: 332
			public Code.Blt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Blt_Un_)base.Set(OpCodes.Blt_Un, operand, name);
				}
			}

			// Token: 0x060005E8 RID: 1512 RVA: 0x000188B0 File Offset: 0x00016AB0
			public Blt_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FC RID: 252
		public class Switch_ : CodeMatch
		{
			// Token: 0x1700014D RID: 333
			public Code.Switch_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Switch_)base.Set(OpCodes.Switch, operand, name);
				}
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x000188E4 File Offset: 0x00016AE4
			public Switch_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FD RID: 253
		public class Ldind_I1_ : CodeMatch
		{
			// Token: 0x1700014E RID: 334
			public Code.Ldind_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I1_)base.Set(OpCodes.Ldind_I1, operand, name);
				}
			}

			// Token: 0x060005EC RID: 1516 RVA: 0x00018918 File Offset: 0x00016B18
			public Ldind_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FE RID: 254
		public class Ldind_U1_ : CodeMatch
		{
			// Token: 0x1700014F RID: 335
			public Code.Ldind_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U1_)base.Set(OpCodes.Ldind_U1, operand, name);
				}
			}

			// Token: 0x060005EE RID: 1518 RVA: 0x0001894C File Offset: 0x00016B4C
			public Ldind_U1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x020000FF RID: 255
		public class Ldind_I2_ : CodeMatch
		{
			// Token: 0x17000150 RID: 336
			public Code.Ldind_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I2_)base.Set(OpCodes.Ldind_I2, operand, name);
				}
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x00018980 File Offset: 0x00016B80
			public Ldind_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000100 RID: 256
		public class Ldind_U2_ : CodeMatch
		{
			// Token: 0x17000151 RID: 337
			public Code.Ldind_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U2_)base.Set(OpCodes.Ldind_U2, operand, name);
				}
			}

			// Token: 0x060005F2 RID: 1522 RVA: 0x000189B4 File Offset: 0x00016BB4
			public Ldind_U2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000101 RID: 257
		public class Ldind_I4_ : CodeMatch
		{
			// Token: 0x17000152 RID: 338
			public Code.Ldind_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I4_)base.Set(OpCodes.Ldind_I4, operand, name);
				}
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x000189E8 File Offset: 0x00016BE8
			public Ldind_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000102 RID: 258
		public class Ldind_U4_ : CodeMatch
		{
			// Token: 0x17000153 RID: 339
			public Code.Ldind_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_U4_)base.Set(OpCodes.Ldind_U4, operand, name);
				}
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x00018A1C File Offset: 0x00016C1C
			public Ldind_U4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000103 RID: 259
		public class Ldind_I8_ : CodeMatch
		{
			// Token: 0x17000154 RID: 340
			public Code.Ldind_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I8_)base.Set(OpCodes.Ldind_I8, operand, name);
				}
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x00018A50 File Offset: 0x00016C50
			public Ldind_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000104 RID: 260
		public class Ldind_I_ : CodeMatch
		{
			// Token: 0x17000155 RID: 341
			public Code.Ldind_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_I_)base.Set(OpCodes.Ldind_I, operand, name);
				}
			}

			// Token: 0x060005FA RID: 1530 RVA: 0x00018A84 File Offset: 0x00016C84
			public Ldind_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000105 RID: 261
		public class Ldind_R4_ : CodeMatch
		{
			// Token: 0x17000156 RID: 342
			public Code.Ldind_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_R4_)base.Set(OpCodes.Ldind_R4, operand, name);
				}
			}

			// Token: 0x060005FC RID: 1532 RVA: 0x00018AB8 File Offset: 0x00016CB8
			public Ldind_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000106 RID: 262
		public class Ldind_R8_ : CodeMatch
		{
			// Token: 0x17000157 RID: 343
			public Code.Ldind_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_R8_)base.Set(OpCodes.Ldind_R8, operand, name);
				}
			}

			// Token: 0x060005FE RID: 1534 RVA: 0x00018AEC File Offset: 0x00016CEC
			public Ldind_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000107 RID: 263
		public class Ldind_Ref_ : CodeMatch
		{
			// Token: 0x17000158 RID: 344
			public Code.Ldind_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldind_Ref_)base.Set(OpCodes.Ldind_Ref, operand, name);
				}
			}

			// Token: 0x06000600 RID: 1536 RVA: 0x00018B20 File Offset: 0x00016D20
			public Ldind_Ref_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000108 RID: 264
		public class Stind_Ref_ : CodeMatch
		{
			// Token: 0x17000159 RID: 345
			public Code.Stind_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_Ref_)base.Set(OpCodes.Stind_Ref, operand, name);
				}
			}

			// Token: 0x06000602 RID: 1538 RVA: 0x00018B54 File Offset: 0x00016D54
			public Stind_Ref_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000109 RID: 265
		public class Stind_I1_ : CodeMatch
		{
			// Token: 0x1700015A RID: 346
			public Code.Stind_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I1_)base.Set(OpCodes.Stind_I1, operand, name);
				}
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x00018B88 File Offset: 0x00016D88
			public Stind_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010A RID: 266
		public class Stind_I2_ : CodeMatch
		{
			// Token: 0x1700015B RID: 347
			public Code.Stind_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I2_)base.Set(OpCodes.Stind_I2, operand, name);
				}
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x00018BBC File Offset: 0x00016DBC
			public Stind_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010B RID: 267
		public class Stind_I4_ : CodeMatch
		{
			// Token: 0x1700015C RID: 348
			public Code.Stind_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I4_)base.Set(OpCodes.Stind_I4, operand, name);
				}
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x00018BF0 File Offset: 0x00016DF0
			public Stind_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010C RID: 268
		public class Stind_I8_ : CodeMatch
		{
			// Token: 0x1700015D RID: 349
			public Code.Stind_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I8_)base.Set(OpCodes.Stind_I8, operand, name);
				}
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x00018C24 File Offset: 0x00016E24
			public Stind_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010D RID: 269
		public class Stind_R4_ : CodeMatch
		{
			// Token: 0x1700015E RID: 350
			public Code.Stind_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_R4_)base.Set(OpCodes.Stind_R4, operand, name);
				}
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x00018C58 File Offset: 0x00016E58
			public Stind_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010E RID: 270
		public class Stind_R8_ : CodeMatch
		{
			// Token: 0x1700015F RID: 351
			public Code.Stind_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_R8_)base.Set(OpCodes.Stind_R8, operand, name);
				}
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x00018C8C File Offset: 0x00016E8C
			public Stind_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200010F RID: 271
		public class Add_ : CodeMatch
		{
			// Token: 0x17000160 RID: 352
			public Code.Add_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_)base.Set(OpCodes.Add, operand, name);
				}
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x00018CC0 File Offset: 0x00016EC0
			public Add_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000110 RID: 272
		public class Sub_ : CodeMatch
		{
			// Token: 0x17000161 RID: 353
			public Code.Sub_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_)base.Set(OpCodes.Sub, operand, name);
				}
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x00018CF4 File Offset: 0x00016EF4
			public Sub_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000111 RID: 273
		public class Mul_ : CodeMatch
		{
			// Token: 0x17000162 RID: 354
			public Code.Mul_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_)base.Set(OpCodes.Mul, operand, name);
				}
			}

			// Token: 0x06000614 RID: 1556 RVA: 0x00018D28 File Offset: 0x00016F28
			public Mul_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000112 RID: 274
		public class Div_ : CodeMatch
		{
			// Token: 0x17000163 RID: 355
			public Code.Div_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Div_)base.Set(OpCodes.Div, operand, name);
				}
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x00018D5C File Offset: 0x00016F5C
			public Div_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000113 RID: 275
		public class Div_Un_ : CodeMatch
		{
			// Token: 0x17000164 RID: 356
			public Code.Div_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Div_Un_)base.Set(OpCodes.Div_Un, operand, name);
				}
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00018D90 File Offset: 0x00016F90
			public Div_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000114 RID: 276
		public class Rem_ : CodeMatch
		{
			// Token: 0x17000165 RID: 357
			public Code.Rem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rem_)base.Set(OpCodes.Rem, operand, name);
				}
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x00018DC4 File Offset: 0x00016FC4
			public Rem_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000115 RID: 277
		public class Rem_Un_ : CodeMatch
		{
			// Token: 0x17000166 RID: 358
			public Code.Rem_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rem_Un_)base.Set(OpCodes.Rem_Un, operand, name);
				}
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00018DF8 File Offset: 0x00016FF8
			public Rem_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000116 RID: 278
		public class And_ : CodeMatch
		{
			// Token: 0x17000167 RID: 359
			public Code.And_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.And_)base.Set(OpCodes.And, operand, name);
				}
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x00018E2C File Offset: 0x0001702C
			public And_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000117 RID: 279
		public class Or_ : CodeMatch
		{
			// Token: 0x17000168 RID: 360
			public Code.Or_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Or_)base.Set(OpCodes.Or, operand, name);
				}
			}

			// Token: 0x06000620 RID: 1568 RVA: 0x00018E60 File Offset: 0x00017060
			public Or_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000118 RID: 280
		public class Xor_ : CodeMatch
		{
			// Token: 0x17000169 RID: 361
			public Code.Xor_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Xor_)base.Set(OpCodes.Xor, operand, name);
				}
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x00018E94 File Offset: 0x00017094
			public Xor_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000119 RID: 281
		public class Shl_ : CodeMatch
		{
			// Token: 0x1700016A RID: 362
			public Code.Shl_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shl_)base.Set(OpCodes.Shl, operand, name);
				}
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x00018EC8 File Offset: 0x000170C8
			public Shl_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011A RID: 282
		public class Shr_ : CodeMatch
		{
			// Token: 0x1700016B RID: 363
			public Code.Shr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shr_)base.Set(OpCodes.Shr, operand, name);
				}
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x00018EFC File Offset: 0x000170FC
			public Shr_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011B RID: 283
		public class Shr_Un_ : CodeMatch
		{
			// Token: 0x1700016C RID: 364
			public Code.Shr_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Shr_Un_)base.Set(OpCodes.Shr_Un, operand, name);
				}
			}

			// Token: 0x06000628 RID: 1576 RVA: 0x00018F30 File Offset: 0x00017130
			public Shr_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011C RID: 284
		public class Neg_ : CodeMatch
		{
			// Token: 0x1700016D RID: 365
			public Code.Neg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Neg_)base.Set(OpCodes.Neg, operand, name);
				}
			}

			// Token: 0x0600062A RID: 1578 RVA: 0x00018F64 File Offset: 0x00017164
			public Neg_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011D RID: 285
		public class Not_ : CodeMatch
		{
			// Token: 0x1700016E RID: 366
			public Code.Not_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Not_)base.Set(OpCodes.Not, operand, name);
				}
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00018F98 File Offset: 0x00017198
			public Not_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011E RID: 286
		public class Conv_I1_ : CodeMatch
		{
			// Token: 0x1700016F RID: 367
			public Code.Conv_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I1_)base.Set(OpCodes.Conv_I1, operand, name);
				}
			}

			// Token: 0x0600062E RID: 1582 RVA: 0x00018FCC File Offset: 0x000171CC
			public Conv_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200011F RID: 287
		public class Conv_I2_ : CodeMatch
		{
			// Token: 0x17000170 RID: 368
			public Code.Conv_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I2_)base.Set(OpCodes.Conv_I2, operand, name);
				}
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x00019000 File Offset: 0x00017200
			public Conv_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000120 RID: 288
		public class Conv_I4_ : CodeMatch
		{
			// Token: 0x17000171 RID: 369
			public Code.Conv_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I4_)base.Set(OpCodes.Conv_I4, operand, name);
				}
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x00019034 File Offset: 0x00017234
			public Conv_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000121 RID: 289
		public class Conv_I8_ : CodeMatch
		{
			// Token: 0x17000172 RID: 370
			public Code.Conv_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I8_)base.Set(OpCodes.Conv_I8, operand, name);
				}
			}

			// Token: 0x06000634 RID: 1588 RVA: 0x00019068 File Offset: 0x00017268
			public Conv_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000122 RID: 290
		public class Conv_R4_ : CodeMatch
		{
			// Token: 0x17000173 RID: 371
			public Code.Conv_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R4_)base.Set(OpCodes.Conv_R4, operand, name);
				}
			}

			// Token: 0x06000636 RID: 1590 RVA: 0x0001909C File Offset: 0x0001729C
			public Conv_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000123 RID: 291
		public class Conv_R8_ : CodeMatch
		{
			// Token: 0x17000174 RID: 372
			public Code.Conv_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R8_)base.Set(OpCodes.Conv_R8, operand, name);
				}
			}

			// Token: 0x06000638 RID: 1592 RVA: 0x000190D0 File Offset: 0x000172D0
			public Conv_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000124 RID: 292
		public class Conv_U4_ : CodeMatch
		{
			// Token: 0x17000175 RID: 373
			public Code.Conv_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U4_)base.Set(OpCodes.Conv_U4, operand, name);
				}
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x00019104 File Offset: 0x00017304
			public Conv_U4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000125 RID: 293
		public class Conv_U8_ : CodeMatch
		{
			// Token: 0x17000176 RID: 374
			public Code.Conv_U8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U8_)base.Set(OpCodes.Conv_U8, operand, name);
				}
			}

			// Token: 0x0600063C RID: 1596 RVA: 0x00019138 File Offset: 0x00017338
			public Conv_U8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000126 RID: 294
		public class Callvirt_ : CodeMatch
		{
			// Token: 0x17000177 RID: 375
			public Code.Callvirt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Callvirt_)base.Set(OpCodes.Callvirt, operand, name);
				}
			}

			// Token: 0x0600063E RID: 1598 RVA: 0x0001916C File Offset: 0x0001736C
			public Callvirt_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000127 RID: 295
		public class Cpobj_ : CodeMatch
		{
			// Token: 0x17000178 RID: 376
			public Code.Cpobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cpobj_)base.Set(OpCodes.Cpobj, operand, name);
				}
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x000191A0 File Offset: 0x000173A0
			public Cpobj_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000128 RID: 296
		public class Ldobj_ : CodeMatch
		{
			// Token: 0x17000179 RID: 377
			public Code.Ldobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldobj_)base.Set(OpCodes.Ldobj, operand, name);
				}
			}

			// Token: 0x06000642 RID: 1602 RVA: 0x000191D4 File Offset: 0x000173D4
			public Ldobj_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000129 RID: 297
		public class Ldstr_ : CodeMatch
		{
			// Token: 0x1700017A RID: 378
			public Code.Ldstr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldstr_)base.Set(OpCodes.Ldstr, operand, name);
				}
			}

			// Token: 0x06000644 RID: 1604 RVA: 0x00019208 File Offset: 0x00017408
			public Ldstr_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012A RID: 298
		public class Newobj_ : CodeMatch
		{
			// Token: 0x1700017B RID: 379
			public Code.Newobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Newobj_)base.Set(OpCodes.Newobj, operand, name);
				}
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x0001923C File Offset: 0x0001743C
			public Newobj_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012B RID: 299
		public class Castclass_ : CodeMatch
		{
			// Token: 0x1700017C RID: 380
			public Code.Castclass_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Castclass_)base.Set(OpCodes.Castclass, operand, name);
				}
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00019270 File Offset: 0x00017470
			public Castclass_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012C RID: 300
		public class Isinst_ : CodeMatch
		{
			// Token: 0x1700017D RID: 381
			public Code.Isinst_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Isinst_)base.Set(OpCodes.Isinst, operand, name);
				}
			}

			// Token: 0x0600064A RID: 1610 RVA: 0x000192A4 File Offset: 0x000174A4
			public Isinst_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012D RID: 301
		public class Conv_R_Un_ : CodeMatch
		{
			// Token: 0x1700017E RID: 382
			public Code.Conv_R_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_R_Un_)base.Set(OpCodes.Conv_R_Un, operand, name);
				}
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x000192D8 File Offset: 0x000174D8
			public Conv_R_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012E RID: 302
		public class Unbox_ : CodeMatch
		{
			// Token: 0x1700017F RID: 383
			public Code.Unbox_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unbox_)base.Set(OpCodes.Unbox, operand, name);
				}
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x0001930C File Offset: 0x0001750C
			public Unbox_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200012F RID: 303
		public class Throw_ : CodeMatch
		{
			// Token: 0x17000180 RID: 384
			public Code.Throw_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Throw_)base.Set(OpCodes.Throw, operand, name);
				}
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x00019340 File Offset: 0x00017540
			public Throw_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000130 RID: 304
		public class Ldfld_ : CodeMatch
		{
			// Token: 0x17000181 RID: 385
			public Code.Ldfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldfld_)base.Set(OpCodes.Ldfld, operand, name);
				}
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x00019374 File Offset: 0x00017574
			public Ldfld_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000131 RID: 305
		public class Ldflda_ : CodeMatch
		{
			// Token: 0x17000182 RID: 386
			public Code.Ldflda_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldflda_)base.Set(OpCodes.Ldflda, operand, name);
				}
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x000193A8 File Offset: 0x000175A8
			public Ldflda_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000132 RID: 306
		public class Stfld_ : CodeMatch
		{
			// Token: 0x17000183 RID: 387
			public Code.Stfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stfld_)base.Set(OpCodes.Stfld, operand, name);
				}
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x000193DC File Offset: 0x000175DC
			public Stfld_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000133 RID: 307
		public class Ldsfld_ : CodeMatch
		{
			// Token: 0x17000184 RID: 388
			public Code.Ldsfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldsfld_)base.Set(OpCodes.Ldsfld, operand, name);
				}
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x00019410 File Offset: 0x00017610
			public Ldsfld_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000134 RID: 308
		public class Ldsflda_ : CodeMatch
		{
			// Token: 0x17000185 RID: 389
			public Code.Ldsflda_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldsflda_)base.Set(OpCodes.Ldsflda, operand, name);
				}
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x00019444 File Offset: 0x00017644
			public Ldsflda_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000135 RID: 309
		public class Stsfld_ : CodeMatch
		{
			// Token: 0x17000186 RID: 390
			public Code.Stsfld_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stsfld_)base.Set(OpCodes.Stsfld, operand, name);
				}
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x00019478 File Offset: 0x00017678
			public Stsfld_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000136 RID: 310
		public class Stobj_ : CodeMatch
		{
			// Token: 0x17000187 RID: 391
			public Code.Stobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stobj_)base.Set(OpCodes.Stobj, operand, name);
				}
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x000194AC File Offset: 0x000176AC
			public Stobj_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000137 RID: 311
		public class Conv_Ovf_I1_Un_ : CodeMatch
		{
			// Token: 0x17000188 RID: 392
			public Code.Conv_Ovf_I1_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I1_Un_)base.Set(OpCodes.Conv_Ovf_I1_Un, operand, name);
				}
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x000194E0 File Offset: 0x000176E0
			public Conv_Ovf_I1_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000138 RID: 312
		public class Conv_Ovf_I2_Un_ : CodeMatch
		{
			// Token: 0x17000189 RID: 393
			public Code.Conv_Ovf_I2_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I2_Un_)base.Set(OpCodes.Conv_Ovf_I2_Un, operand, name);
				}
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00019514 File Offset: 0x00017714
			public Conv_Ovf_I2_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000139 RID: 313
		public class Conv_Ovf_I4_Un_ : CodeMatch
		{
			// Token: 0x1700018A RID: 394
			public Code.Conv_Ovf_I4_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I4_Un_)base.Set(OpCodes.Conv_Ovf_I4_Un, operand, name);
				}
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00019548 File Offset: 0x00017748
			public Conv_Ovf_I4_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013A RID: 314
		public class Conv_Ovf_I8_Un_ : CodeMatch
		{
			// Token: 0x1700018B RID: 395
			public Code.Conv_Ovf_I8_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I8_Un_)base.Set(OpCodes.Conv_Ovf_I8_Un, operand, name);
				}
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x0001957C File Offset: 0x0001777C
			public Conv_Ovf_I8_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013B RID: 315
		public class Conv_Ovf_U1_Un_ : CodeMatch
		{
			// Token: 0x1700018C RID: 396
			public Code.Conv_Ovf_U1_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U1_Un_)base.Set(OpCodes.Conv_Ovf_U1_Un, operand, name);
				}
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x000195B0 File Offset: 0x000177B0
			public Conv_Ovf_U1_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013C RID: 316
		public class Conv_Ovf_U2_Un_ : CodeMatch
		{
			// Token: 0x1700018D RID: 397
			public Code.Conv_Ovf_U2_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U2_Un_)base.Set(OpCodes.Conv_Ovf_U2_Un, operand, name);
				}
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x000195E4 File Offset: 0x000177E4
			public Conv_Ovf_U2_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013D RID: 317
		public class Conv_Ovf_U4_Un_ : CodeMatch
		{
			// Token: 0x1700018E RID: 398
			public Code.Conv_Ovf_U4_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U4_Un_)base.Set(OpCodes.Conv_Ovf_U4_Un, operand, name);
				}
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x00019618 File Offset: 0x00017818
			public Conv_Ovf_U4_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013E RID: 318
		public class Conv_Ovf_U8_Un_ : CodeMatch
		{
			// Token: 0x1700018F RID: 399
			public Code.Conv_Ovf_U8_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U8_Un_)base.Set(OpCodes.Conv_Ovf_U8_Un, operand, name);
				}
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x0001964C File Offset: 0x0001784C
			public Conv_Ovf_U8_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200013F RID: 319
		public class Conv_Ovf_I_Un_ : CodeMatch
		{
			// Token: 0x17000190 RID: 400
			public Code.Conv_Ovf_I_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I_Un_)base.Set(OpCodes.Conv_Ovf_I_Un, operand, name);
				}
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x00019680 File Offset: 0x00017880
			public Conv_Ovf_I_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000140 RID: 320
		public class Conv_Ovf_U_Un_ : CodeMatch
		{
			// Token: 0x17000191 RID: 401
			public Code.Conv_Ovf_U_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U_Un_)base.Set(OpCodes.Conv_Ovf_U_Un, operand, name);
				}
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x000196B4 File Offset: 0x000178B4
			public Conv_Ovf_U_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000141 RID: 321
		public class Box_ : CodeMatch
		{
			// Token: 0x17000192 RID: 402
			public Code.Box_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Box_)base.Set(OpCodes.Box, operand, name);
				}
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000196E8 File Offset: 0x000178E8
			public Box_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000142 RID: 322
		public class Newarr_ : CodeMatch
		{
			// Token: 0x17000193 RID: 403
			public Code.Newarr_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Newarr_)base.Set(OpCodes.Newarr, operand, name);
				}
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0001971C File Offset: 0x0001791C
			public Newarr_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000143 RID: 323
		public class Ldlen_ : CodeMatch
		{
			// Token: 0x17000194 RID: 404
			public Code.Ldlen_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldlen_)base.Set(OpCodes.Ldlen, operand, name);
				}
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00019750 File Offset: 0x00017950
			public Ldlen_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000144 RID: 324
		public class Ldelema_ : CodeMatch
		{
			// Token: 0x17000195 RID: 405
			public Code.Ldelema_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelema_)base.Set(OpCodes.Ldelema, operand, name);
				}
			}

			// Token: 0x0600067A RID: 1658 RVA: 0x00019784 File Offset: 0x00017984
			public Ldelema_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000145 RID: 325
		public class Ldelem_I1_ : CodeMatch
		{
			// Token: 0x17000196 RID: 406
			public Code.Ldelem_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I1_)base.Set(OpCodes.Ldelem_I1, operand, name);
				}
			}

			// Token: 0x0600067C RID: 1660 RVA: 0x000197B8 File Offset: 0x000179B8
			public Ldelem_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000146 RID: 326
		public class Ldelem_U1_ : CodeMatch
		{
			// Token: 0x17000197 RID: 407
			public Code.Ldelem_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U1_)base.Set(OpCodes.Ldelem_U1, operand, name);
				}
			}

			// Token: 0x0600067E RID: 1662 RVA: 0x000197EC File Offset: 0x000179EC
			public Ldelem_U1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000147 RID: 327
		public class Ldelem_I2_ : CodeMatch
		{
			// Token: 0x17000198 RID: 408
			public Code.Ldelem_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I2_)base.Set(OpCodes.Ldelem_I2, operand, name);
				}
			}

			// Token: 0x06000680 RID: 1664 RVA: 0x00019820 File Offset: 0x00017A20
			public Ldelem_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000148 RID: 328
		public class Ldelem_U2_ : CodeMatch
		{
			// Token: 0x17000199 RID: 409
			public Code.Ldelem_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U2_)base.Set(OpCodes.Ldelem_U2, operand, name);
				}
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00019854 File Offset: 0x00017A54
			public Ldelem_U2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000149 RID: 329
		public class Ldelem_I4_ : CodeMatch
		{
			// Token: 0x1700019A RID: 410
			public Code.Ldelem_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I4_)base.Set(OpCodes.Ldelem_I4, operand, name);
				}
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x00019888 File Offset: 0x00017A88
			public Ldelem_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014A RID: 330
		public class Ldelem_U4_ : CodeMatch
		{
			// Token: 0x1700019B RID: 411
			public Code.Ldelem_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_U4_)base.Set(OpCodes.Ldelem_U4, operand, name);
				}
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x000198BC File Offset: 0x00017ABC
			public Ldelem_U4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014B RID: 331
		public class Ldelem_I8_ : CodeMatch
		{
			// Token: 0x1700019C RID: 412
			public Code.Ldelem_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I8_)base.Set(OpCodes.Ldelem_I8, operand, name);
				}
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x000198F0 File Offset: 0x00017AF0
			public Ldelem_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014C RID: 332
		public class Ldelem_I_ : CodeMatch
		{
			// Token: 0x1700019D RID: 413
			public Code.Ldelem_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_I_)base.Set(OpCodes.Ldelem_I, operand, name);
				}
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x00019924 File Offset: 0x00017B24
			public Ldelem_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014D RID: 333
		public class Ldelem_R4_ : CodeMatch
		{
			// Token: 0x1700019E RID: 414
			public Code.Ldelem_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_R4_)base.Set(OpCodes.Ldelem_R4, operand, name);
				}
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x00019958 File Offset: 0x00017B58
			public Ldelem_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014E RID: 334
		public class Ldelem_R8_ : CodeMatch
		{
			// Token: 0x1700019F RID: 415
			public Code.Ldelem_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_R8_)base.Set(OpCodes.Ldelem_R8, operand, name);
				}
			}

			// Token: 0x0600068E RID: 1678 RVA: 0x0001998C File Offset: 0x00017B8C
			public Ldelem_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200014F RID: 335
		public class Ldelem_Ref_ : CodeMatch
		{
			// Token: 0x170001A0 RID: 416
			public Code.Ldelem_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_Ref_)base.Set(OpCodes.Ldelem_Ref, operand, name);
				}
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x000199C0 File Offset: 0x00017BC0
			public Ldelem_Ref_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000150 RID: 336
		public class Stelem_I_ : CodeMatch
		{
			// Token: 0x170001A1 RID: 417
			public Code.Stelem_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I_)base.Set(OpCodes.Stelem_I, operand, name);
				}
			}

			// Token: 0x06000692 RID: 1682 RVA: 0x000199F4 File Offset: 0x00017BF4
			public Stelem_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000151 RID: 337
		public class Stelem_I1_ : CodeMatch
		{
			// Token: 0x170001A2 RID: 418
			public Code.Stelem_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I1_)base.Set(OpCodes.Stelem_I1, operand, name);
				}
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x00019A28 File Offset: 0x00017C28
			public Stelem_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000152 RID: 338
		public class Stelem_I2_ : CodeMatch
		{
			// Token: 0x170001A3 RID: 419
			public Code.Stelem_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I2_)base.Set(OpCodes.Stelem_I2, operand, name);
				}
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x00019A5C File Offset: 0x00017C5C
			public Stelem_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000153 RID: 339
		public class Stelem_I4_ : CodeMatch
		{
			// Token: 0x170001A4 RID: 420
			public Code.Stelem_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I4_)base.Set(OpCodes.Stelem_I4, operand, name);
				}
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x00019A90 File Offset: 0x00017C90
			public Stelem_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000154 RID: 340
		public class Stelem_I8_ : CodeMatch
		{
			// Token: 0x170001A5 RID: 421
			public Code.Stelem_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_I8_)base.Set(OpCodes.Stelem_I8, operand, name);
				}
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x00019AC4 File Offset: 0x00017CC4
			public Stelem_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000155 RID: 341
		public class Stelem_R4_ : CodeMatch
		{
			// Token: 0x170001A6 RID: 422
			public Code.Stelem_R4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_R4_)base.Set(OpCodes.Stelem_R4, operand, name);
				}
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x00019AF8 File Offset: 0x00017CF8
			public Stelem_R4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000156 RID: 342
		public class Stelem_R8_ : CodeMatch
		{
			// Token: 0x170001A7 RID: 423
			public Code.Stelem_R8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_R8_)base.Set(OpCodes.Stelem_R8, operand, name);
				}
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x00019B2C File Offset: 0x00017D2C
			public Stelem_R8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000157 RID: 343
		public class Stelem_Ref_ : CodeMatch
		{
			// Token: 0x170001A8 RID: 424
			public Code.Stelem_Ref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_Ref_)base.Set(OpCodes.Stelem_Ref, operand, name);
				}
			}

			// Token: 0x060006A0 RID: 1696 RVA: 0x00019B60 File Offset: 0x00017D60
			public Stelem_Ref_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000158 RID: 344
		public class Ldelem_ : CodeMatch
		{
			// Token: 0x170001A9 RID: 425
			public Code.Ldelem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldelem_)base.Set(OpCodes.Ldelem, operand, name);
				}
			}

			// Token: 0x060006A2 RID: 1698 RVA: 0x00019B94 File Offset: 0x00017D94
			public Ldelem_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000159 RID: 345
		public class Stelem_ : CodeMatch
		{
			// Token: 0x170001AA RID: 426
			public Code.Stelem_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stelem_)base.Set(OpCodes.Stelem, operand, name);
				}
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x00019BC8 File Offset: 0x00017DC8
			public Stelem_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015A RID: 346
		public class Unbox_Any_ : CodeMatch
		{
			// Token: 0x170001AB RID: 427
			public Code.Unbox_Any_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unbox_Any_)base.Set(OpCodes.Unbox_Any, operand, name);
				}
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x00019BFC File Offset: 0x00017DFC
			public Unbox_Any_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015B RID: 347
		public class Conv_Ovf_I1_ : CodeMatch
		{
			// Token: 0x170001AC RID: 428
			public Code.Conv_Ovf_I1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I1_)base.Set(OpCodes.Conv_Ovf_I1, operand, name);
				}
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x00019C30 File Offset: 0x00017E30
			public Conv_Ovf_I1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015C RID: 348
		public class Conv_Ovf_U1_ : CodeMatch
		{
			// Token: 0x170001AD RID: 429
			public Code.Conv_Ovf_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U1_)base.Set(OpCodes.Conv_Ovf_U1, operand, name);
				}
			}

			// Token: 0x060006AA RID: 1706 RVA: 0x00019C64 File Offset: 0x00017E64
			public Conv_Ovf_U1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015D RID: 349
		public class Conv_Ovf_I2_ : CodeMatch
		{
			// Token: 0x170001AE RID: 430
			public Code.Conv_Ovf_I2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I2_)base.Set(OpCodes.Conv_Ovf_I2, operand, name);
				}
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x00019C98 File Offset: 0x00017E98
			public Conv_Ovf_I2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015E RID: 350
		public class Conv_Ovf_U2_ : CodeMatch
		{
			// Token: 0x170001AF RID: 431
			public Code.Conv_Ovf_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U2_)base.Set(OpCodes.Conv_Ovf_U2, operand, name);
				}
			}

			// Token: 0x060006AE RID: 1710 RVA: 0x00019CCC File Offset: 0x00017ECC
			public Conv_Ovf_U2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200015F RID: 351
		public class Conv_Ovf_I4_ : CodeMatch
		{
			// Token: 0x170001B0 RID: 432
			public Code.Conv_Ovf_I4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I4_)base.Set(OpCodes.Conv_Ovf_I4, operand, name);
				}
			}

			// Token: 0x060006B0 RID: 1712 RVA: 0x00019D00 File Offset: 0x00017F00
			public Conv_Ovf_I4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000160 RID: 352
		public class Conv_Ovf_U4_ : CodeMatch
		{
			// Token: 0x170001B1 RID: 433
			public Code.Conv_Ovf_U4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U4_)base.Set(OpCodes.Conv_Ovf_U4, operand, name);
				}
			}

			// Token: 0x060006B2 RID: 1714 RVA: 0x00019D34 File Offset: 0x00017F34
			public Conv_Ovf_U4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000161 RID: 353
		public class Conv_Ovf_I8_ : CodeMatch
		{
			// Token: 0x170001B2 RID: 434
			public Code.Conv_Ovf_I8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I8_)base.Set(OpCodes.Conv_Ovf_I8, operand, name);
				}
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x00019D68 File Offset: 0x00017F68
			public Conv_Ovf_I8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000162 RID: 354
		public class Conv_Ovf_U8_ : CodeMatch
		{
			// Token: 0x170001B3 RID: 435
			public Code.Conv_Ovf_U8_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U8_)base.Set(OpCodes.Conv_Ovf_U8, operand, name);
				}
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00019D9C File Offset: 0x00017F9C
			public Conv_Ovf_U8_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000163 RID: 355
		public class Refanyval_ : CodeMatch
		{
			// Token: 0x170001B4 RID: 436
			public Code.Refanyval_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Refanyval_)base.Set(OpCodes.Refanyval, operand, name);
				}
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x00019DD0 File Offset: 0x00017FD0
			public Refanyval_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000164 RID: 356
		public class Ckfinite_ : CodeMatch
		{
			// Token: 0x170001B5 RID: 437
			public Code.Ckfinite_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ckfinite_)base.Set(OpCodes.Ckfinite, operand, name);
				}
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x00019E04 File Offset: 0x00018004
			public Ckfinite_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000165 RID: 357
		public class Mkrefany_ : CodeMatch
		{
			// Token: 0x170001B6 RID: 438
			public Code.Mkrefany_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mkrefany_)base.Set(OpCodes.Mkrefany, operand, name);
				}
			}

			// Token: 0x060006BC RID: 1724 RVA: 0x00019E38 File Offset: 0x00018038
			public Mkrefany_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000166 RID: 358
		public class Ldtoken_ : CodeMatch
		{
			// Token: 0x170001B7 RID: 439
			public Code.Ldtoken_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldtoken_)base.Set(OpCodes.Ldtoken, operand, name);
				}
			}

			// Token: 0x060006BE RID: 1726 RVA: 0x00019E6C File Offset: 0x0001806C
			public Ldtoken_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000167 RID: 359
		public class Conv_U2_ : CodeMatch
		{
			// Token: 0x170001B8 RID: 440
			public Code.Conv_U2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U2_)base.Set(OpCodes.Conv_U2, operand, name);
				}
			}

			// Token: 0x060006C0 RID: 1728 RVA: 0x00019EA0 File Offset: 0x000180A0
			public Conv_U2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000168 RID: 360
		public class Conv_U1_ : CodeMatch
		{
			// Token: 0x170001B9 RID: 441
			public Code.Conv_U1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U1_)base.Set(OpCodes.Conv_U1, operand, name);
				}
			}

			// Token: 0x060006C2 RID: 1730 RVA: 0x00019ED4 File Offset: 0x000180D4
			public Conv_U1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000169 RID: 361
		public class Conv_I_ : CodeMatch
		{
			// Token: 0x170001BA RID: 442
			public Code.Conv_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_I_)base.Set(OpCodes.Conv_I, operand, name);
				}
			}

			// Token: 0x060006C4 RID: 1732 RVA: 0x00019F08 File Offset: 0x00018108
			public Conv_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016A RID: 362
		public class Conv_Ovf_I_ : CodeMatch
		{
			// Token: 0x170001BB RID: 443
			public Code.Conv_Ovf_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_I_)base.Set(OpCodes.Conv_Ovf_I, operand, name);
				}
			}

			// Token: 0x060006C6 RID: 1734 RVA: 0x00019F3C File Offset: 0x0001813C
			public Conv_Ovf_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016B RID: 363
		public class Conv_Ovf_U_ : CodeMatch
		{
			// Token: 0x170001BC RID: 444
			public Code.Conv_Ovf_U_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_Ovf_U_)base.Set(OpCodes.Conv_Ovf_U, operand, name);
				}
			}

			// Token: 0x060006C8 RID: 1736 RVA: 0x00019F70 File Offset: 0x00018170
			public Conv_Ovf_U_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016C RID: 364
		public class Add_Ovf_ : CodeMatch
		{
			// Token: 0x170001BD RID: 445
			public Code.Add_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_Ovf_)base.Set(OpCodes.Add_Ovf, operand, name);
				}
			}

			// Token: 0x060006CA RID: 1738 RVA: 0x00019FA4 File Offset: 0x000181A4
			public Add_Ovf_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016D RID: 365
		public class Add_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001BE RID: 446
			public Code.Add_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Add_Ovf_Un_)base.Set(OpCodes.Add_Ovf_Un, operand, name);
				}
			}

			// Token: 0x060006CC RID: 1740 RVA: 0x00019FD8 File Offset: 0x000181D8
			public Add_Ovf_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016E RID: 366
		public class Mul_Ovf_ : CodeMatch
		{
			// Token: 0x170001BF RID: 447
			public Code.Mul_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_Ovf_)base.Set(OpCodes.Mul_Ovf, operand, name);
				}
			}

			// Token: 0x060006CE RID: 1742 RVA: 0x0001A00C File Offset: 0x0001820C
			public Mul_Ovf_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200016F RID: 367
		public class Mul_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001C0 RID: 448
			public Code.Mul_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Mul_Ovf_Un_)base.Set(OpCodes.Mul_Ovf_Un, operand, name);
				}
			}

			// Token: 0x060006D0 RID: 1744 RVA: 0x0001A040 File Offset: 0x00018240
			public Mul_Ovf_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000170 RID: 368
		public class Sub_Ovf_ : CodeMatch
		{
			// Token: 0x170001C1 RID: 449
			public Code.Sub_Ovf_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_Ovf_)base.Set(OpCodes.Sub_Ovf, operand, name);
				}
			}

			// Token: 0x060006D2 RID: 1746 RVA: 0x0001A074 File Offset: 0x00018274
			public Sub_Ovf_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000171 RID: 369
		public class Sub_Ovf_Un_ : CodeMatch
		{
			// Token: 0x170001C2 RID: 450
			public Code.Sub_Ovf_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sub_Ovf_Un_)base.Set(OpCodes.Sub_Ovf_Un, operand, name);
				}
			}

			// Token: 0x060006D4 RID: 1748 RVA: 0x0001A0A8 File Offset: 0x000182A8
			public Sub_Ovf_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000172 RID: 370
		public class Endfinally_ : CodeMatch
		{
			// Token: 0x170001C3 RID: 451
			public Code.Endfinally_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Endfinally_)base.Set(OpCodes.Endfinally, operand, name);
				}
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x0001A0DC File Offset: 0x000182DC
			public Endfinally_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000173 RID: 371
		public class Leave_ : CodeMatch
		{
			// Token: 0x170001C4 RID: 452
			public Code.Leave_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Leave_)base.Set(OpCodes.Leave, operand, name);
				}
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x0001A110 File Offset: 0x00018310
			public Leave_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000174 RID: 372
		public class Leave_S_ : CodeMatch
		{
			// Token: 0x170001C5 RID: 453
			public Code.Leave_S_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Leave_S_)base.Set(OpCodes.Leave_S, operand, name);
				}
			}

			// Token: 0x060006DA RID: 1754 RVA: 0x0001A144 File Offset: 0x00018344
			public Leave_S_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000175 RID: 373
		public class Stind_I_ : CodeMatch
		{
			// Token: 0x170001C6 RID: 454
			public Code.Stind_I_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stind_I_)base.Set(OpCodes.Stind_I, operand, name);
				}
			}

			// Token: 0x060006DC RID: 1756 RVA: 0x0001A178 File Offset: 0x00018378
			public Stind_I_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000176 RID: 374
		public class Conv_U_ : CodeMatch
		{
			// Token: 0x170001C7 RID: 455
			public Code.Conv_U_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Conv_U_)base.Set(OpCodes.Conv_U, operand, name);
				}
			}

			// Token: 0x060006DE RID: 1758 RVA: 0x0001A1AC File Offset: 0x000183AC
			public Conv_U_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000177 RID: 375
		public class Prefix7_ : CodeMatch
		{
			// Token: 0x170001C8 RID: 456
			public Code.Prefix7_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix7_)base.Set(OpCodes.Prefix7, operand, name);
				}
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x0001A1E0 File Offset: 0x000183E0
			public Prefix7_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000178 RID: 376
		public class Prefix6_ : CodeMatch
		{
			// Token: 0x170001C9 RID: 457
			public Code.Prefix6_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix6_)base.Set(OpCodes.Prefix6, operand, name);
				}
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x0001A214 File Offset: 0x00018414
			public Prefix6_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000179 RID: 377
		public class Prefix5_ : CodeMatch
		{
			// Token: 0x170001CA RID: 458
			public Code.Prefix5_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix5_)base.Set(OpCodes.Prefix5, operand, name);
				}
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x0001A248 File Offset: 0x00018448
			public Prefix5_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017A RID: 378
		public class Prefix4_ : CodeMatch
		{
			// Token: 0x170001CB RID: 459
			public Code.Prefix4_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix4_)base.Set(OpCodes.Prefix4, operand, name);
				}
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x0001A27C File Offset: 0x0001847C
			public Prefix4_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017B RID: 379
		public class Prefix3_ : CodeMatch
		{
			// Token: 0x170001CC RID: 460
			public Code.Prefix3_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix3_)base.Set(OpCodes.Prefix3, operand, name);
				}
			}

			// Token: 0x060006E8 RID: 1768 RVA: 0x0001A2B0 File Offset: 0x000184B0
			public Prefix3_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017C RID: 380
		public class Prefix2_ : CodeMatch
		{
			// Token: 0x170001CD RID: 461
			public Code.Prefix2_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix2_)base.Set(OpCodes.Prefix2, operand, name);
				}
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x0001A2E4 File Offset: 0x000184E4
			public Prefix2_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017D RID: 381
		public class Prefix1_ : CodeMatch
		{
			// Token: 0x170001CE RID: 462
			public Code.Prefix1_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefix1_)base.Set(OpCodes.Prefix1, operand, name);
				}
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x0001A318 File Offset: 0x00018518
			public Prefix1_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017E RID: 382
		public class Prefixref_ : CodeMatch
		{
			// Token: 0x170001CF RID: 463
			public Code.Prefixref_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Prefixref_)base.Set(OpCodes.Prefixref, operand, name);
				}
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x0001A34C File Offset: 0x0001854C
			public Prefixref_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200017F RID: 383
		public class Arglist_ : CodeMatch
		{
			// Token: 0x170001D0 RID: 464
			public Code.Arglist_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Arglist_)base.Set(OpCodes.Arglist, operand, name);
				}
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x0001A380 File Offset: 0x00018580
			public Arglist_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000180 RID: 384
		public class Ceq_ : CodeMatch
		{
			// Token: 0x170001D1 RID: 465
			public Code.Ceq_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ceq_)base.Set(OpCodes.Ceq, operand, name);
				}
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x0001A3B4 File Offset: 0x000185B4
			public Ceq_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000181 RID: 385
		public class Cgt_ : CodeMatch
		{
			// Token: 0x170001D2 RID: 466
			public Code.Cgt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cgt_)base.Set(OpCodes.Cgt, operand, name);
				}
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x0001A3E8 File Offset: 0x000185E8
			public Cgt_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000182 RID: 386
		public class Cgt_Un_ : CodeMatch
		{
			// Token: 0x170001D3 RID: 467
			public Code.Cgt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cgt_Un_)base.Set(OpCodes.Cgt_Un, operand, name);
				}
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x0001A41C File Offset: 0x0001861C
			public Cgt_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000183 RID: 387
		public class Clt_ : CodeMatch
		{
			// Token: 0x170001D4 RID: 468
			public Code.Clt_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Clt_)base.Set(OpCodes.Clt, operand, name);
				}
			}

			// Token: 0x060006F8 RID: 1784 RVA: 0x0001A450 File Offset: 0x00018650
			public Clt_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000184 RID: 388
		public class Clt_Un_ : CodeMatch
		{
			// Token: 0x170001D5 RID: 469
			public Code.Clt_Un_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Clt_Un_)base.Set(OpCodes.Clt_Un, operand, name);
				}
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x0001A484 File Offset: 0x00018684
			public Clt_Un_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000185 RID: 389
		public class Ldftn_ : CodeMatch
		{
			// Token: 0x170001D6 RID: 470
			public Code.Ldftn_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldftn_)base.Set(OpCodes.Ldftn, operand, name);
				}
			}

			// Token: 0x060006FC RID: 1788 RVA: 0x0001A4B8 File Offset: 0x000186B8
			public Ldftn_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000186 RID: 390
		public class Ldvirtftn_ : CodeMatch
		{
			// Token: 0x170001D7 RID: 471
			public Code.Ldvirtftn_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldvirtftn_)base.Set(OpCodes.Ldvirtftn, operand, name);
				}
			}

			// Token: 0x060006FE RID: 1790 RVA: 0x0001A4EC File Offset: 0x000186EC
			public Ldvirtftn_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000187 RID: 391
		public class Ldarg_ : CodeMatch
		{
			// Token: 0x170001D8 RID: 472
			public Code.Ldarg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarg_)base.Set(OpCodes.Ldarg, operand, name);
				}
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x0001A520 File Offset: 0x00018720
			public Ldarg_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000188 RID: 392
		public class Ldarga_ : CodeMatch
		{
			// Token: 0x170001D9 RID: 473
			public Code.Ldarga_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldarga_)base.Set(OpCodes.Ldarga, operand, name);
				}
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x0001A554 File Offset: 0x00018754
			public Ldarga_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000189 RID: 393
		public class Starg_ : CodeMatch
		{
			// Token: 0x170001DA RID: 474
			public Code.Starg_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Starg_)base.Set(OpCodes.Starg, operand, name);
				}
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x0001A588 File Offset: 0x00018788
			public Starg_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018A RID: 394
		public class Ldloc_ : CodeMatch
		{
			// Token: 0x170001DB RID: 475
			public Code.Ldloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloc_)base.Set(OpCodes.Ldloc, operand, name);
				}
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x0001A5BC File Offset: 0x000187BC
			public Ldloc_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018B RID: 395
		public class Ldloca_ : CodeMatch
		{
			// Token: 0x170001DC RID: 476
			public Code.Ldloca_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Ldloca_)base.Set(OpCodes.Ldloca, operand, name);
				}
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0001A5F0 File Offset: 0x000187F0
			public Ldloca_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018C RID: 396
		public class Stloc_ : CodeMatch
		{
			// Token: 0x170001DD RID: 477
			public Code.Stloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Stloc_)base.Set(OpCodes.Stloc, operand, name);
				}
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x0001A624 File Offset: 0x00018824
			public Stloc_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018D RID: 397
		public class Localloc_ : CodeMatch
		{
			// Token: 0x170001DE RID: 478
			public Code.Localloc_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Localloc_)base.Set(OpCodes.Localloc, operand, name);
				}
			}

			// Token: 0x0600070C RID: 1804 RVA: 0x0001A658 File Offset: 0x00018858
			public Localloc_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018E RID: 398
		public class Endfilter_ : CodeMatch
		{
			// Token: 0x170001DF RID: 479
			public Code.Endfilter_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Endfilter_)base.Set(OpCodes.Endfilter, operand, name);
				}
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x0001A68C File Offset: 0x0001888C
			public Endfilter_() : base(null, null, null)
			{
			}
		}

		// Token: 0x0200018F RID: 399
		public class Unaligned_ : CodeMatch
		{
			// Token: 0x170001E0 RID: 480
			public Code.Unaligned_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Unaligned_)base.Set(OpCodes.Unaligned, operand, name);
				}
			}

			// Token: 0x06000710 RID: 1808 RVA: 0x0001A6C0 File Offset: 0x000188C0
			public Unaligned_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000190 RID: 400
		public class Volatile_ : CodeMatch
		{
			// Token: 0x170001E1 RID: 481
			public Code.Volatile_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Volatile_)base.Set(OpCodes.Volatile, operand, name);
				}
			}

			// Token: 0x06000712 RID: 1810 RVA: 0x0001A6F4 File Offset: 0x000188F4
			public Volatile_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000191 RID: 401
		public class Tailcall_ : CodeMatch
		{
			// Token: 0x170001E2 RID: 482
			public Code.Tailcall_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Tailcall_)base.Set(OpCodes.Tailcall, operand, name);
				}
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x0001A728 File Offset: 0x00018928
			public Tailcall_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000192 RID: 402
		public class Initobj_ : CodeMatch
		{
			// Token: 0x170001E3 RID: 483
			public Code.Initobj_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Initobj_)base.Set(OpCodes.Initobj, operand, name);
				}
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0001A75C File Offset: 0x0001895C
			public Initobj_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000193 RID: 403
		public class Constrained_ : CodeMatch
		{
			// Token: 0x170001E4 RID: 484
			public Code.Constrained_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Constrained_)base.Set(OpCodes.Constrained, operand, name);
				}
			}

			// Token: 0x06000718 RID: 1816 RVA: 0x0001A790 File Offset: 0x00018990
			public Constrained_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000194 RID: 404
		public class Cpblk_ : CodeMatch
		{
			// Token: 0x170001E5 RID: 485
			public Code.Cpblk_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Cpblk_)base.Set(OpCodes.Cpblk, operand, name);
				}
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0001A7C4 File Offset: 0x000189C4
			public Cpblk_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000195 RID: 405
		public class Initblk_ : CodeMatch
		{
			// Token: 0x170001E6 RID: 486
			public Code.Initblk_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Initblk_)base.Set(OpCodes.Initblk, operand, name);
				}
			}

			// Token: 0x0600071C RID: 1820 RVA: 0x0001A7F8 File Offset: 0x000189F8
			public Initblk_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000196 RID: 406
		public class Rethrow_ : CodeMatch
		{
			// Token: 0x170001E7 RID: 487
			public Code.Rethrow_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Rethrow_)base.Set(OpCodes.Rethrow, operand, name);
				}
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x0001A82C File Offset: 0x00018A2C
			public Rethrow_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000197 RID: 407
		public class Sizeof_ : CodeMatch
		{
			// Token: 0x170001E8 RID: 488
			public Code.Sizeof_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Sizeof_)base.Set(OpCodes.Sizeof, operand, name);
				}
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0001A860 File Offset: 0x00018A60
			public Sizeof_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000198 RID: 408
		public class Refanytype_ : CodeMatch
		{
			// Token: 0x170001E9 RID: 489
			public Code.Refanytype_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Refanytype_)base.Set(OpCodes.Refanytype, operand, name);
				}
			}

			// Token: 0x06000722 RID: 1826 RVA: 0x0001A894 File Offset: 0x00018A94
			public Refanytype_() : base(null, null, null)
			{
			}
		}

		// Token: 0x02000199 RID: 409
		public class Readonly_ : CodeMatch
		{
			// Token: 0x170001EA RID: 490
			public Code.Readonly_ this[object operand = null, string name = null]
			{
				get
				{
					return (Code.Readonly_)base.Set(OpCodes.Readonly, operand, name);
				}
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x0001A8C8 File Offset: 0x00018AC8
			public Readonly_() : base(null, null, null)
			{
			}
		}
	}
}
