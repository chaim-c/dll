using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020000FD RID: 253
	public class AgentDrivenProperties
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x00015BA5 File Offset: 0x00013DA5
		internal float[] Values
		{
			get
			{
				return this._statValues;
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00015BAD File Offset: 0x00013DAD
		public AgentDrivenProperties()
		{
			this._statValues = new float[84];
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00015BC2 File Offset: 0x00013DC2
		public float GetStat(DrivenProperty propertyEnum)
		{
			return this._statValues[(int)propertyEnum];
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00015BCC File Offset: 0x00013DCC
		public void SetStat(DrivenProperty propertyEnum, float value)
		{
			this._statValues[(int)propertyEnum] = value;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00015BD7 File Offset: 0x00013DD7
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00015BE1 File Offset: 0x00013DE1
		public float SwingSpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.SwingSpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.SwingSpeedMultiplier, value);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00015BEC File Offset: 0x00013DEC
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00015BF6 File Offset: 0x00013DF6
		public float ThrustOrRangedReadySpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.ThrustOrRangedReadySpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.ThrustOrRangedReadySpeedMultiplier, value);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00015C01 File Offset: 0x00013E01
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00015C0B File Offset: 0x00013E0B
		public float HandlingMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.HandlingMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.HandlingMultiplier, value);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00015C16 File Offset: 0x00013E16
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00015C20 File Offset: 0x00013E20
		public float ReloadSpeed
		{
			get
			{
				return this.GetStat(DrivenProperty.ReloadSpeed);
			}
			set
			{
				this.SetStat(DrivenProperty.ReloadSpeed, value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00015C2B File Offset: 0x00013E2B
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00015C35 File Offset: 0x00013E35
		public float MissileSpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.MissileSpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.MissileSpeedMultiplier, value);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00015C40 File Offset: 0x00013E40
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00015C4A File Offset: 0x00013E4A
		public float WeaponInaccuracy
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponInaccuracy);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponInaccuracy, value);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00015C55 File Offset: 0x00013E55
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00015C5F File Offset: 0x00013E5F
		public float WeaponMaxMovementAccuracyPenalty
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponWorstMobileAccuracyPenalty);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponWorstMobileAccuracyPenalty, value);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00015C6A File Offset: 0x00013E6A
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00015C74 File Offset: 0x00013E74
		public float WeaponMaxUnsteadyAccuracyPenalty
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponWorstUnsteadyAccuracyPenalty);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponWorstUnsteadyAccuracyPenalty, value);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00015C7F File Offset: 0x00013E7F
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00015C89 File Offset: 0x00013E89
		public float WeaponBestAccuracyWaitTime
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponBestAccuracyWaitTime);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponBestAccuracyWaitTime, value);
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00015C94 File Offset: 0x00013E94
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00015C9E File Offset: 0x00013E9E
		public float WeaponUnsteadyBeginTime
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponUnsteadyBeginTime);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponUnsteadyBeginTime, value);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00015CA9 File Offset: 0x00013EA9
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x00015CB3 File Offset: 0x00013EB3
		public float WeaponUnsteadyEndTime
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponUnsteadyEndTime);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponUnsteadyEndTime, value);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00015CBE File Offset: 0x00013EBE
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public float WeaponRotationalAccuracyPenaltyInRadians
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponRotationalAccuracyPenaltyInRadians);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponRotationalAccuracyPenaltyInRadians, value);
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00015CD3 File Offset: 0x00013ED3
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00015CDD File Offset: 0x00013EDD
		public float ArmorEncumbrance
		{
			get
			{
				return this.GetStat(DrivenProperty.ArmorEncumbrance);
			}
			set
			{
				this.SetStat(DrivenProperty.ArmorEncumbrance, value);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x00015CE8 File Offset: 0x00013EE8
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00015CF2 File Offset: 0x00013EF2
		public float WeaponsEncumbrance
		{
			get
			{
				return this.GetStat(DrivenProperty.WeaponsEncumbrance);
			}
			set
			{
				this.SetStat(DrivenProperty.WeaponsEncumbrance, value);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x00015CFD File Offset: 0x00013EFD
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00015D07 File Offset: 0x00013F07
		public float ArmorHead
		{
			get
			{
				return this.GetStat(DrivenProperty.ArmorHead);
			}
			set
			{
				this.SetStat(DrivenProperty.ArmorHead, value);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00015D12 File Offset: 0x00013F12
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00015D1C File Offset: 0x00013F1C
		public float ArmorTorso
		{
			get
			{
				return this.GetStat(DrivenProperty.ArmorTorso);
			}
			set
			{
				this.SetStat(DrivenProperty.ArmorTorso, value);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x00015D27 File Offset: 0x00013F27
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x00015D31 File Offset: 0x00013F31
		public float ArmorLegs
		{
			get
			{
				return this.GetStat(DrivenProperty.ArmorLegs);
			}
			set
			{
				this.SetStat(DrivenProperty.ArmorLegs, value);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x00015D3C File Offset: 0x00013F3C
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x00015D46 File Offset: 0x00013F46
		public float ArmorArms
		{
			get
			{
				return this.GetStat(DrivenProperty.ArmorArms);
			}
			set
			{
				this.SetStat(DrivenProperty.ArmorArms, value);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x00015D51 File Offset: 0x00013F51
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x00015D5B File Offset: 0x00013F5B
		public float AttributeRiding
		{
			get
			{
				return this.GetStat(DrivenProperty.AttributeRiding);
			}
			set
			{
				this.SetStat(DrivenProperty.AttributeRiding, value);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x00015D66 File Offset: 0x00013F66
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x00015D70 File Offset: 0x00013F70
		public float AttributeShield
		{
			get
			{
				return this.GetStat(DrivenProperty.AttributeShield);
			}
			set
			{
				this.SetStat(DrivenProperty.AttributeShield, value);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00015D7B File Offset: 0x00013F7B
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x00015D85 File Offset: 0x00013F85
		public float AttributeShieldMissileCollisionBodySizeAdder
		{
			get
			{
				return this.GetStat(DrivenProperty.AttributeShieldMissileCollisionBodySizeAdder);
			}
			set
			{
				this.SetStat(DrivenProperty.AttributeShieldMissileCollisionBodySizeAdder, value);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00015D90 File Offset: 0x00013F90
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00015D9A File Offset: 0x00013F9A
		public float ShieldBashStunDurationMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.ShieldBashStunDurationMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.ShieldBashStunDurationMultiplier, value);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00015DA5 File Offset: 0x00013FA5
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00015DAF File Offset: 0x00013FAF
		public float KickStunDurationMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.KickStunDurationMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.KickStunDurationMultiplier, value);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00015DBA File Offset: 0x00013FBA
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public float ReloadMovementPenaltyFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.ReloadMovementPenaltyFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.ReloadMovementPenaltyFactor, value);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x00015DCF File Offset: 0x00013FCF
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x00015DD9 File Offset: 0x00013FD9
		public float TopSpeedReachDuration
		{
			get
			{
				return this.GetStat(DrivenProperty.TopSpeedReachDuration);
			}
			set
			{
				this.SetStat(DrivenProperty.TopSpeedReachDuration, value);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00015DE4 File Offset: 0x00013FE4
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x00015DEE File Offset: 0x00013FEE
		public float MaxSpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.MaxSpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.MaxSpeedMultiplier, value);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00015DF9 File Offset: 0x00013FF9
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x00015E03 File Offset: 0x00014003
		public float CombatMaxSpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.CombatMaxSpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.CombatMaxSpeedMultiplier, value);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00015E0E File Offset: 0x0001400E
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x00015E18 File Offset: 0x00014018
		public float AttributeHorseArchery
		{
			get
			{
				return this.GetStat(DrivenProperty.AttributeHorseArchery);
			}
			set
			{
				this.SetStat(DrivenProperty.AttributeHorseArchery, value);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00015E23 File Offset: 0x00014023
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x00015E2D File Offset: 0x0001402D
		public float AttributeCourage
		{
			get
			{
				return this.GetStat(DrivenProperty.AttributeCourage);
			}
			set
			{
				this.SetStat(DrivenProperty.AttributeCourage, value);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00015E38 File Offset: 0x00014038
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x00015E42 File Offset: 0x00014042
		public float MountManeuver
		{
			get
			{
				return this.GetStat(DrivenProperty.MountManeuver);
			}
			set
			{
				this.SetStat(DrivenProperty.MountManeuver, value);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00015E4D File Offset: 0x0001404D
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x00015E57 File Offset: 0x00014057
		public float MountSpeed
		{
			get
			{
				return this.GetStat(DrivenProperty.MountSpeed);
			}
			set
			{
				this.SetStat(DrivenProperty.MountSpeed, value);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00015E62 File Offset: 0x00014062
		// (set) Token: 0x06000C05 RID: 3077 RVA: 0x00015E6C File Offset: 0x0001406C
		public float MountDashAccelerationMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.MountDashAccelerationMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.MountDashAccelerationMultiplier, value);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000C06 RID: 3078 RVA: 0x00015E77 File Offset: 0x00014077
		// (set) Token: 0x06000C07 RID: 3079 RVA: 0x00015E81 File Offset: 0x00014081
		public float MountChargeDamage
		{
			get
			{
				return this.GetStat(DrivenProperty.MountChargeDamage);
			}
			set
			{
				this.SetStat(DrivenProperty.MountChargeDamage, value);
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x00015E8C File Offset: 0x0001408C
		// (set) Token: 0x06000C09 RID: 3081 RVA: 0x00015E96 File Offset: 0x00014096
		public float MountDifficulty
		{
			get
			{
				return this.GetStat(DrivenProperty.MountDifficulty);
			}
			set
			{
				this.SetStat(DrivenProperty.MountDifficulty, value);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000C0A RID: 3082 RVA: 0x00015EA1 File Offset: 0x000140A1
		// (set) Token: 0x06000C0B RID: 3083 RVA: 0x00015EAB File Offset: 0x000140AB
		public float BipedalRangedReadySpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.BipedalRangedReadySpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.BipedalRangedReadySpeedMultiplier, value);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00015EB6 File Offset: 0x000140B6
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00015EC0 File Offset: 0x000140C0
		public float BipedalRangedReloadSpeedMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.BipedalRangedReloadSpeedMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.BipedalRangedReloadSpeedMultiplier, value);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00015ECB File Offset: 0x000140CB
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00015ED4 File Offset: 0x000140D4
		public float AiRangedHorsebackMissileRange
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRangedHorsebackMissileRange);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRangedHorsebackMissileRange, value);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00015EDE File Offset: 0x000140DE
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00015EE7 File Offset: 0x000140E7
		public float AiFacingMissileWatch
		{
			get
			{
				return this.GetStat(DrivenProperty.AiFacingMissileWatch);
			}
			set
			{
				this.SetStat(DrivenProperty.AiFacingMissileWatch, value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00015EF1 File Offset: 0x000140F1
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00015EFA File Offset: 0x000140FA
		public float AiFlyingMissileCheckRadius
		{
			get
			{
				return this.GetStat(DrivenProperty.AiFlyingMissileCheckRadius);
			}
			set
			{
				this.SetStat(DrivenProperty.AiFlyingMissileCheckRadius, value);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x00015F04 File Offset: 0x00014104
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00015F0D File Offset: 0x0001410D
		public float AiShootFreq
		{
			get
			{
				return this.GetStat(DrivenProperty.AiShootFreq);
			}
			set
			{
				this.SetStat(DrivenProperty.AiShootFreq, value);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x00015F17 File Offset: 0x00014117
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x00015F20 File Offset: 0x00014120
		public float AiWaitBeforeShootFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiWaitBeforeShootFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiWaitBeforeShootFactor, value);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00015F2A File Offset: 0x0001412A
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x00015F33 File Offset: 0x00014133
		public float AIBlockOnDecideAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIBlockOnDecideAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIBlockOnDecideAbility, value);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00015F3D File Offset: 0x0001413D
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x00015F46 File Offset: 0x00014146
		public float AIParryOnDecideAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIParryOnDecideAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIParryOnDecideAbility, value);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00015F50 File Offset: 0x00014150
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x00015F59 File Offset: 0x00014159
		public float AiTryChamberAttackOnDecide
		{
			get
			{
				return this.GetStat(DrivenProperty.AiTryChamberAttackOnDecide);
			}
			set
			{
				this.SetStat(DrivenProperty.AiTryChamberAttackOnDecide, value);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x00015F63 File Offset: 0x00014163
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x00015F6C File Offset: 0x0001416C
		public float AIAttackOnParryChance
		{
			get
			{
				return this.GetStat(DrivenProperty.AIAttackOnParryChance);
			}
			set
			{
				this.SetStat(DrivenProperty.AIAttackOnParryChance, value);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x00015F76 File Offset: 0x00014176
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x00015F80 File Offset: 0x00014180
		public float AiAttackOnParryTiming
		{
			get
			{
				return this.GetStat(DrivenProperty.AiAttackOnParryTiming);
			}
			set
			{
				this.SetStat(DrivenProperty.AiAttackOnParryTiming, value);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00015F8B File Offset: 0x0001418B
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x00015F95 File Offset: 0x00014195
		public float AIDecideOnAttackChance
		{
			get
			{
				return this.GetStat(DrivenProperty.AIDecideOnAttackChance);
			}
			set
			{
				this.SetStat(DrivenProperty.AIDecideOnAttackChance, value);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00015FA0 File Offset: 0x000141A0
		// (set) Token: 0x06000C25 RID: 3109 RVA: 0x00015FAA File Offset: 0x000141AA
		public float AIParryOnAttackAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIParryOnAttackAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIParryOnAttackAbility, value);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x00015FB5 File Offset: 0x000141B5
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x00015FBF File Offset: 0x000141BF
		public float AiKick
		{
			get
			{
				return this.GetStat(DrivenProperty.AiKick);
			}
			set
			{
				this.SetStat(DrivenProperty.AiKick, value);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x00015FCA File Offset: 0x000141CA
		// (set) Token: 0x06000C29 RID: 3113 RVA: 0x00015FD4 File Offset: 0x000141D4
		public float AiAttackCalculationMaxTimeFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiAttackCalculationMaxTimeFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiAttackCalculationMaxTimeFactor, value);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00015FDF File Offset: 0x000141DF
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x00015FE9 File Offset: 0x000141E9
		public float AiDecideOnAttackWhenReceiveHitTiming
		{
			get
			{
				return this.GetStat(DrivenProperty.AiDecideOnAttackWhenReceiveHitTiming);
			}
			set
			{
				this.SetStat(DrivenProperty.AiDecideOnAttackWhenReceiveHitTiming, value);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00015FF4 File Offset: 0x000141F4
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00015FFE File Offset: 0x000141FE
		public float AiDecideOnAttackContinueAction
		{
			get
			{
				return this.GetStat(DrivenProperty.AiDecideOnAttackContinueAction);
			}
			set
			{
				this.SetStat(DrivenProperty.AiDecideOnAttackContinueAction, value);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x00016009 File Offset: 0x00014209
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00016013 File Offset: 0x00014213
		public float AiDecideOnAttackingContinue
		{
			get
			{
				return this.GetStat(DrivenProperty.AiDecideOnAttackingContinue);
			}
			set
			{
				this.SetStat(DrivenProperty.AiDecideOnAttackingContinue, value);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001601E File Offset: 0x0001421E
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00016028 File Offset: 0x00014228
		public float AIParryOnAttackingContinueAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIParryOnAttackingContinueAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIParryOnAttackingContinueAbility, value);
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00016033 File Offset: 0x00014233
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0001603D File Offset: 0x0001423D
		public float AIDecideOnRealizeEnemyBlockingAttackAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIDecideOnRealizeEnemyBlockingAttackAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIDecideOnRealizeEnemyBlockingAttackAbility, value);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00016048 File Offset: 0x00014248
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x00016052 File Offset: 0x00014252
		public float AIRealizeBlockingFromIncorrectSideAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AIRealizeBlockingFromIncorrectSideAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AIRealizeBlockingFromIncorrectSideAbility, value);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001605D File Offset: 0x0001425D
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00016067 File Offset: 0x00014267
		public float AiAttackingShieldDefenseChance
		{
			get
			{
				return this.GetStat(DrivenProperty.AiAttackingShieldDefenseChance);
			}
			set
			{
				this.SetStat(DrivenProperty.AiAttackingShieldDefenseChance, value);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00016072 File Offset: 0x00014272
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0001607C File Offset: 0x0001427C
		public float AiAttackingShieldDefenseTimer
		{
			get
			{
				return this.GetStat(DrivenProperty.AiAttackingShieldDefenseTimer);
			}
			set
			{
				this.SetStat(DrivenProperty.AiAttackingShieldDefenseTimer, value);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00016087 File Offset: 0x00014287
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x00016091 File Offset: 0x00014291
		public float AiCheckMovementIntervalFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiCheckMovementIntervalFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiCheckMovementIntervalFactor, value);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001609C File Offset: 0x0001429C
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x000160A6 File Offset: 0x000142A6
		public float AiMovementDelayFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiMovementDelayFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiMovementDelayFactor, value);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000160B1 File Offset: 0x000142B1
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x000160BB File Offset: 0x000142BB
		public float AiParryDecisionChangeValue
		{
			get
			{
				return this.GetStat(DrivenProperty.AiParryDecisionChangeValue);
			}
			set
			{
				this.SetStat(DrivenProperty.AiParryDecisionChangeValue, value);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000160C6 File Offset: 0x000142C6
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x000160D0 File Offset: 0x000142D0
		public float AiDefendWithShieldDecisionChanceValue
		{
			get
			{
				return this.GetStat(DrivenProperty.AiDefendWithShieldDecisionChanceValue);
			}
			set
			{
				this.SetStat(DrivenProperty.AiDefendWithShieldDecisionChanceValue, value);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000160DB File Offset: 0x000142DB
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x000160E5 File Offset: 0x000142E5
		public float AiMoveEnemySideTimeValue
		{
			get
			{
				return this.GetStat(DrivenProperty.AiMoveEnemySideTimeValue);
			}
			set
			{
				this.SetStat(DrivenProperty.AiMoveEnemySideTimeValue, value);
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x000160F0 File Offset: 0x000142F0
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x000160FA File Offset: 0x000142FA
		public float AiMinimumDistanceToContinueFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiMinimumDistanceToContinueFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiMinimumDistanceToContinueFactor, value);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x00016105 File Offset: 0x00014305
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0001610F File Offset: 0x0001430F
		public float AiHearingDistanceFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiHearingDistanceFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiHearingDistanceFactor, value);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001611A File Offset: 0x0001431A
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x00016124 File Offset: 0x00014324
		public float AiChargeHorsebackTargetDistFactor
		{
			get
			{
				return this.GetStat(DrivenProperty.AiChargeHorsebackTargetDistFactor);
			}
			set
			{
				this.SetStat(DrivenProperty.AiChargeHorsebackTargetDistFactor, value);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0001612F File Offset: 0x0001432F
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x00016139 File Offset: 0x00014339
		public float AiRangerLeadErrorMin
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRangerLeadErrorMin);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRangerLeadErrorMin, value);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00016144 File Offset: 0x00014344
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0001614E File Offset: 0x0001434E
		public float AiRangerLeadErrorMax
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRangerLeadErrorMax);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRangerLeadErrorMax, value);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00016159 File Offset: 0x00014359
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x00016163 File Offset: 0x00014363
		public float AiRangerVerticalErrorMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRangerVerticalErrorMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRangerVerticalErrorMultiplier, value);
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001616E File Offset: 0x0001436E
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x00016178 File Offset: 0x00014378
		public float AiRangerHorizontalErrorMultiplier
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRangerHorizontalErrorMultiplier);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRangerHorizontalErrorMultiplier, value);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00016183 File Offset: 0x00014383
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0001618D File Offset: 0x0001438D
		public float AIAttackOnDecideChance
		{
			get
			{
				return this.GetStat(DrivenProperty.AIAttackOnDecideChance);
			}
			set
			{
				this.SetStat(DrivenProperty.AIAttackOnDecideChance, value);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00016198 File Offset: 0x00014398
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x000161A2 File Offset: 0x000143A2
		public float AiRaiseShieldDelayTimeBase
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRaiseShieldDelayTimeBase);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRaiseShieldDelayTimeBase, value);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000161AD File Offset: 0x000143AD
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x000161B7 File Offset: 0x000143B7
		public float AiUseShieldAgainstEnemyMissileProbability
		{
			get
			{
				return this.GetStat(DrivenProperty.AiUseShieldAgainstEnemyMissileProbability);
			}
			set
			{
				this.SetStat(DrivenProperty.AiUseShieldAgainstEnemyMissileProbability, value);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000161C2 File Offset: 0x000143C2
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000161D1 File Offset: 0x000143D1
		public int AiSpeciesIndex
		{
			get
			{
				return MathF.Round(this.GetStat(DrivenProperty.AiSpeciesIndex));
			}
			set
			{
				this.SetStat(DrivenProperty.AiSpeciesIndex, (float)value);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x000161DD File Offset: 0x000143DD
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x000161E7 File Offset: 0x000143E7
		public float AiRandomizedDefendDirectionChance
		{
			get
			{
				return this.GetStat(DrivenProperty.AiRandomizedDefendDirectionChance);
			}
			set
			{
				this.SetStat(DrivenProperty.AiRandomizedDefendDirectionChance, value);
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x000161F2 File Offset: 0x000143F2
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x000161FC File Offset: 0x000143FC
		public float AiShooterError
		{
			get
			{
				return this.GetStat(DrivenProperty.AiShooterError);
			}
			set
			{
				this.SetStat(DrivenProperty.AiShooterError, value);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00016207 File Offset: 0x00014407
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x00016211 File Offset: 0x00014411
		public float AISetNoAttackTimerAfterBeingHitAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AISetNoAttackTimerAfterBeingHitAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AISetNoAttackTimerAfterBeingHitAbility, value);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0001621C File Offset: 0x0001441C
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x00016226 File Offset: 0x00014426
		public float AISetNoAttackTimerAfterBeingParriedAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AISetNoAttackTimerAfterBeingParriedAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AISetNoAttackTimerAfterBeingParriedAbility, value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00016231 File Offset: 0x00014431
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0001623B File Offset: 0x0001443B
		public float AISetNoDefendTimerAfterHittingAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AISetNoDefendTimerAfterHittingAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AISetNoDefendTimerAfterHittingAbility, value);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x00016246 File Offset: 0x00014446
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x00016250 File Offset: 0x00014450
		public float AISetNoDefendTimerAfterParryingAbility
		{
			get
			{
				return this.GetStat(DrivenProperty.AISetNoDefendTimerAfterParryingAbility);
			}
			set
			{
				this.SetStat(DrivenProperty.AISetNoDefendTimerAfterParryingAbility, value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001625B File Offset: 0x0001445B
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x00016265 File Offset: 0x00014465
		public float AIEstimateStunDurationPrecision
		{
			get
			{
				return this.GetStat(DrivenProperty.AIEstimateStunDurationPrecision);
			}
			set
			{
				this.SetStat(DrivenProperty.AIEstimateStunDurationPrecision, value);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x00016270 File Offset: 0x00014470
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0001627A File Offset: 0x0001447A
		public float AIHoldingReadyMaxDuration
		{
			get
			{
				return this.GetStat(DrivenProperty.AIHoldingReadyMaxDuration);
			}
			set
			{
				this.SetStat(DrivenProperty.AIHoldingReadyMaxDuration, value);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00016285 File Offset: 0x00014485
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0001628F File Offset: 0x0001448F
		public float AIHoldingReadyVariationPercentage
		{
			get
			{
				return this.GetStat(DrivenProperty.AIHoldingReadyVariationPercentage);
			}
			set
			{
				this.SetStat(DrivenProperty.AIHoldingReadyVariationPercentage, value);
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0001629A File Offset: 0x0001449A
		internal float[] InitializeDrivenProperties(Agent agent, Equipment spawnEquipment, AgentBuildData agentBuildData)
		{
			MissionGameModels.Current.AgentStatCalculateModel.InitializeAgentStats(agent, spawnEquipment, this, agentBuildData);
			return this._statValues;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000162B5 File Offset: 0x000144B5
		internal float[] UpdateDrivenProperties(Agent agent)
		{
			MissionGameModels.Current.AgentStatCalculateModel.UpdateAgentStats(agent, this);
			return this._statValues;
		}

		// Token: 0x040002AB RID: 683
		private readonly float[] _statValues;
	}
}
