using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005FB RID: 1531
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseWeaponDataInternal : IGettable<LogPlayerUseWeaponData>, ISettable<LogPlayerUseWeaponData>, IDisposable
	{
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x0003A680 File Offset: 0x00038880
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x0003A698 File Offset: 0x00038898
		public IntPtr PlayerHandle
		{
			get
			{
				return this.m_PlayerHandle;
			}
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0003A6A4 File Offset: 0x000388A4
		// (set) Token: 0x06002738 RID: 10040 RVA: 0x0003A6C5 File Offset: 0x000388C5
		public Vec3f? PlayerPosition
		{
			get
			{
				Vec3f? result;
				Helper.Get<Vec3fInternal, Vec3f>(this.m_PlayerPosition, out result);
				return result;
			}
			set
			{
				Helper.Set<Vec3f, Vec3fInternal>(ref value, ref this.m_PlayerPosition);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0003A6D8 File Offset: 0x000388D8
		// (set) Token: 0x0600273A RID: 10042 RVA: 0x0003A6F9 File Offset: 0x000388F9
		public Quat? PlayerViewRotation
		{
			get
			{
				Quat? result;
				Helper.Get<QuatInternal, Quat>(this.m_PlayerViewRotation, out result);
				return result;
			}
			set
			{
				Helper.Set<Quat, QuatInternal>(ref value, ref this.m_PlayerViewRotation);
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0003A70C File Offset: 0x0003890C
		// (set) Token: 0x0600273C RID: 10044 RVA: 0x0003A72D File Offset: 0x0003892D
		public bool IsPlayerViewZoomed
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsPlayerViewZoomed, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsPlayerViewZoomed);
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x0003A740 File Offset: 0x00038940
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x0003A761 File Offset: 0x00038961
		public bool IsMeleeAttack
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsMeleeAttack, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsMeleeAttack);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0003A774 File Offset: 0x00038974
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x0003A795 File Offset: 0x00038995
		public Utf8String WeaponName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_WeaponName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_WeaponName);
			}
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0003A7A8 File Offset: 0x000389A8
		public void Set(ref LogPlayerUseWeaponData other)
		{
			this.PlayerHandle = other.PlayerHandle;
			this.PlayerPosition = other.PlayerPosition;
			this.PlayerViewRotation = other.PlayerViewRotation;
			this.IsPlayerViewZoomed = other.IsPlayerViewZoomed;
			this.IsMeleeAttack = other.IsMeleeAttack;
			this.WeaponName = other.WeaponName;
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0003A804 File Offset: 0x00038A04
		public void Set(ref LogPlayerUseWeaponData? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.PlayerHandle = other.Value.PlayerHandle;
				this.PlayerPosition = other.Value.PlayerPosition;
				this.PlayerViewRotation = other.Value.PlayerViewRotation;
				this.IsPlayerViewZoomed = other.Value.IsPlayerViewZoomed;
				this.IsMeleeAttack = other.Value.IsMeleeAttack;
				this.WeaponName = other.Value.WeaponName;
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0003A89F File Offset: 0x00038A9F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlayerHandle);
			Helper.Dispose(ref this.m_PlayerPosition);
			Helper.Dispose(ref this.m_PlayerViewRotation);
			Helper.Dispose(ref this.m_WeaponName);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0003A8D2 File Offset: 0x00038AD2
		public void Get(out LogPlayerUseWeaponData output)
		{
			output = default(LogPlayerUseWeaponData);
			output.Set(ref this);
		}

		// Token: 0x040011A6 RID: 4518
		private IntPtr m_PlayerHandle;

		// Token: 0x040011A7 RID: 4519
		private IntPtr m_PlayerPosition;

		// Token: 0x040011A8 RID: 4520
		private IntPtr m_PlayerViewRotation;

		// Token: 0x040011A9 RID: 4521
		private int m_IsPlayerViewZoomed;

		// Token: 0x040011AA RID: 4522
		private int m_IsMeleeAttack;

		// Token: 0x040011AB RID: 4523
		private IntPtr m_WeaponName;
	}
}
