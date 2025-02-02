using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F5 RID: 1525
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerTakeDamageOptionsInternal : ISettable<LogPlayerTakeDamageOptions>, IDisposable
	{
		// Token: 0x17000B89 RID: 2953
		// (set) Token: 0x060026EF RID: 9967 RVA: 0x00039EA9 File Offset: 0x000380A9
		public IntPtr VictimPlayerHandle
		{
			set
			{
				this.m_VictimPlayerHandle = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (set) Token: 0x060026F0 RID: 9968 RVA: 0x00039EB3 File Offset: 0x000380B3
		public Vec3f? VictimPlayerPosition
		{
			set
			{
				Helper.Set<Vec3f, Vec3fInternal>(ref value, ref this.m_VictimPlayerPosition);
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (set) Token: 0x060026F1 RID: 9969 RVA: 0x00039EC4 File Offset: 0x000380C4
		public Quat? VictimPlayerViewRotation
		{
			set
			{
				Helper.Set<Quat, QuatInternal>(ref value, ref this.m_VictimPlayerViewRotation);
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x00039ED5 File Offset: 0x000380D5
		public IntPtr AttackerPlayerHandle
		{
			set
			{
				this.m_AttackerPlayerHandle = value;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (set) Token: 0x060026F3 RID: 9971 RVA: 0x00039EDF File Offset: 0x000380DF
		public Vec3f? AttackerPlayerPosition
		{
			set
			{
				Helper.Set<Vec3f, Vec3fInternal>(ref value, ref this.m_AttackerPlayerPosition);
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x00039EF0 File Offset: 0x000380F0
		public Quat? AttackerPlayerViewRotation
		{
			set
			{
				Helper.Set<Quat, QuatInternal>(ref value, ref this.m_AttackerPlayerViewRotation);
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x00039F01 File Offset: 0x00038101
		public bool IsHitscanAttack
		{
			set
			{
				Helper.Set(value, ref this.m_IsHitscanAttack);
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x00039F11 File Offset: 0x00038111
		public bool HasLineOfSight
		{
			set
			{
				Helper.Set(value, ref this.m_HasLineOfSight);
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x00039F21 File Offset: 0x00038121
		public bool IsCriticalHit
		{
			set
			{
				Helper.Set(value, ref this.m_IsCriticalHit);
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x00039F31 File Offset: 0x00038131
		public uint HitBoneId_DEPRECATED
		{
			set
			{
				this.m_HitBoneId_DEPRECATED = value;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x00039F3B File Offset: 0x0003813B
		public float DamageTaken
		{
			set
			{
				this.m_DamageTaken = value;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x00039F45 File Offset: 0x00038145
		public float HealthRemaining
		{
			set
			{
				this.m_HealthRemaining = value;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (set) Token: 0x060026FB RID: 9979 RVA: 0x00039F4F File Offset: 0x0003814F
		public AntiCheatCommonPlayerTakeDamageSource DamageSource
		{
			set
			{
				this.m_DamageSource = value;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x00039F59 File Offset: 0x00038159
		public AntiCheatCommonPlayerTakeDamageType DamageType
		{
			set
			{
				this.m_DamageType = value;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x00039F63 File Offset: 0x00038163
		public AntiCheatCommonPlayerTakeDamageResult DamageResult
		{
			set
			{
				this.m_DamageResult = value;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x00039F6D File Offset: 0x0003816D
		public LogPlayerUseWeaponData? PlayerUseWeaponData
		{
			set
			{
				Helper.Set<LogPlayerUseWeaponData, LogPlayerUseWeaponDataInternal>(ref value, ref this.m_PlayerUseWeaponData);
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (set) Token: 0x060026FF RID: 9983 RVA: 0x00039F7E File Offset: 0x0003817E
		public uint TimeSincePlayerUseWeaponMs
		{
			set
			{
				this.m_TimeSincePlayerUseWeaponMs = value;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x00039F88 File Offset: 0x00038188
		public Vec3f? DamagePosition
		{
			set
			{
				Helper.Set<Vec3f, Vec3fInternal>(ref value, ref this.m_DamagePosition);
			}
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x00039F9C File Offset: 0x0003819C
		public void Set(ref LogPlayerTakeDamageOptions other)
		{
			this.m_ApiVersion = 3;
			this.VictimPlayerHandle = other.VictimPlayerHandle;
			this.VictimPlayerPosition = other.VictimPlayerPosition;
			this.VictimPlayerViewRotation = other.VictimPlayerViewRotation;
			this.AttackerPlayerHandle = other.AttackerPlayerHandle;
			this.AttackerPlayerPosition = other.AttackerPlayerPosition;
			this.AttackerPlayerViewRotation = other.AttackerPlayerViewRotation;
			this.IsHitscanAttack = other.IsHitscanAttack;
			this.HasLineOfSight = other.HasLineOfSight;
			this.IsCriticalHit = other.IsCriticalHit;
			this.HitBoneId_DEPRECATED = other.HitBoneId_DEPRECATED;
			this.DamageTaken = other.DamageTaken;
			this.HealthRemaining = other.HealthRemaining;
			this.DamageSource = other.DamageSource;
			this.DamageType = other.DamageType;
			this.DamageResult = other.DamageResult;
			this.PlayerUseWeaponData = other.PlayerUseWeaponData;
			this.TimeSincePlayerUseWeaponMs = other.TimeSincePlayerUseWeaponMs;
			this.DamagePosition = other.DamagePosition;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x0003A09C File Offset: 0x0003829C
		public void Set(ref LogPlayerTakeDamageOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.VictimPlayerHandle = other.Value.VictimPlayerHandle;
				this.VictimPlayerPosition = other.Value.VictimPlayerPosition;
				this.VictimPlayerViewRotation = other.Value.VictimPlayerViewRotation;
				this.AttackerPlayerHandle = other.Value.AttackerPlayerHandle;
				this.AttackerPlayerPosition = other.Value.AttackerPlayerPosition;
				this.AttackerPlayerViewRotation = other.Value.AttackerPlayerViewRotation;
				this.IsHitscanAttack = other.Value.IsHitscanAttack;
				this.HasLineOfSight = other.Value.HasLineOfSight;
				this.IsCriticalHit = other.Value.IsCriticalHit;
				this.HitBoneId_DEPRECATED = other.Value.HitBoneId_DEPRECATED;
				this.DamageTaken = other.Value.DamageTaken;
				this.HealthRemaining = other.Value.HealthRemaining;
				this.DamageSource = other.Value.DamageSource;
				this.DamageType = other.Value.DamageType;
				this.DamageResult = other.Value.DamageResult;
				this.PlayerUseWeaponData = other.Value.PlayerUseWeaponData;
				this.TimeSincePlayerUseWeaponMs = other.Value.TimeSincePlayerUseWeaponMs;
				this.DamagePosition = other.Value.DamagePosition;
			}
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x0003A23C File Offset: 0x0003843C
		public void Dispose()
		{
			Helper.Dispose(ref this.m_VictimPlayerHandle);
			Helper.Dispose(ref this.m_VictimPlayerPosition);
			Helper.Dispose(ref this.m_VictimPlayerViewRotation);
			Helper.Dispose(ref this.m_AttackerPlayerHandle);
			Helper.Dispose(ref this.m_AttackerPlayerPosition);
			Helper.Dispose(ref this.m_AttackerPlayerViewRotation);
			Helper.Dispose(ref this.m_PlayerUseWeaponData);
			Helper.Dispose(ref this.m_DamagePosition);
		}

		// Token: 0x04001177 RID: 4471
		private int m_ApiVersion;

		// Token: 0x04001178 RID: 4472
		private IntPtr m_VictimPlayerHandle;

		// Token: 0x04001179 RID: 4473
		private IntPtr m_VictimPlayerPosition;

		// Token: 0x0400117A RID: 4474
		private IntPtr m_VictimPlayerViewRotation;

		// Token: 0x0400117B RID: 4475
		private IntPtr m_AttackerPlayerHandle;

		// Token: 0x0400117C RID: 4476
		private IntPtr m_AttackerPlayerPosition;

		// Token: 0x0400117D RID: 4477
		private IntPtr m_AttackerPlayerViewRotation;

		// Token: 0x0400117E RID: 4478
		private int m_IsHitscanAttack;

		// Token: 0x0400117F RID: 4479
		private int m_HasLineOfSight;

		// Token: 0x04001180 RID: 4480
		private int m_IsCriticalHit;

		// Token: 0x04001181 RID: 4481
		private uint m_HitBoneId_DEPRECATED;

		// Token: 0x04001182 RID: 4482
		private float m_DamageTaken;

		// Token: 0x04001183 RID: 4483
		private float m_HealthRemaining;

		// Token: 0x04001184 RID: 4484
		private AntiCheatCommonPlayerTakeDamageSource m_DamageSource;

		// Token: 0x04001185 RID: 4485
		private AntiCheatCommonPlayerTakeDamageType m_DamageType;

		// Token: 0x04001186 RID: 4486
		private AntiCheatCommonPlayerTakeDamageResult m_DamageResult;

		// Token: 0x04001187 RID: 4487
		private IntPtr m_PlayerUseWeaponData;

		// Token: 0x04001188 RID: 4488
		private uint m_TimeSincePlayerUseWeaponMs;

		// Token: 0x04001189 RID: 4489
		private IntPtr m_DamagePosition;
	}
}
