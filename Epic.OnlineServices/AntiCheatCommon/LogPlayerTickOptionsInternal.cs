using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F7 RID: 1527
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerTickOptionsInternal : ISettable<LogPlayerTickOptions>, IDisposable
	{
		// Token: 0x17000BA1 RID: 2977
		// (set) Token: 0x06002710 RID: 10000 RVA: 0x0003A310 File Offset: 0x00038510
		public IntPtr PlayerHandle
		{
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (set) Token: 0x06002711 RID: 10001 RVA: 0x0003A31A File Offset: 0x0003851A
		public Vec3f? PlayerPosition
		{
			set
			{
				Helper.Set<Vec3f, Vec3fInternal>(ref value, ref this.m_PlayerPosition);
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (set) Token: 0x06002712 RID: 10002 RVA: 0x0003A32B File Offset: 0x0003852B
		public Quat? PlayerViewRotation
		{
			set
			{
				Helper.Set<Quat, QuatInternal>(ref value, ref this.m_PlayerViewRotation);
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (set) Token: 0x06002713 RID: 10003 RVA: 0x0003A33C File Offset: 0x0003853C
		public bool IsPlayerViewZoomed
		{
			set
			{
				Helper.Set(value, ref this.m_IsPlayerViewZoomed);
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (set) Token: 0x06002714 RID: 10004 RVA: 0x0003A34C File Offset: 0x0003854C
		public float PlayerHealth
		{
			set
			{
				this.m_PlayerHealth = value;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (set) Token: 0x06002715 RID: 10005 RVA: 0x0003A356 File Offset: 0x00038556
		public AntiCheatCommonPlayerMovementState PlayerMovementState
		{
			set
			{
				this.m_PlayerMovementState = value;
			}
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x0003A360 File Offset: 0x00038560
		public void Set(ref LogPlayerTickOptions other)
		{
			this.m_ApiVersion = 2;
			this.PlayerHandle = other.PlayerHandle;
			this.PlayerPosition = other.PlayerPosition;
			this.PlayerViewRotation = other.PlayerViewRotation;
			this.IsPlayerViewZoomed = other.IsPlayerViewZoomed;
			this.PlayerHealth = other.PlayerHealth;
			this.PlayerMovementState = other.PlayerMovementState;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0003A3C4 File Offset: 0x000385C4
		public void Set(ref LogPlayerTickOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.PlayerHandle = other.Value.PlayerHandle;
				this.PlayerPosition = other.Value.PlayerPosition;
				this.PlayerViewRotation = other.Value.PlayerViewRotation;
				this.IsPlayerViewZoomed = other.Value.IsPlayerViewZoomed;
				this.PlayerHealth = other.Value.PlayerHealth;
				this.PlayerMovementState = other.Value.PlayerMovementState;
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0003A466 File Offset: 0x00038666
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlayerHandle);
			Helper.Dispose(ref this.m_PlayerPosition);
			Helper.Dispose(ref this.m_PlayerViewRotation);
		}

		// Token: 0x04001190 RID: 4496
		private int m_ApiVersion;

		// Token: 0x04001191 RID: 4497
		private IntPtr m_PlayerHandle;

		// Token: 0x04001192 RID: 4498
		private IntPtr m_PlayerPosition;

		// Token: 0x04001193 RID: 4499
		private IntPtr m_PlayerViewRotation;

		// Token: 0x04001194 RID: 4500
		private int m_IsPlayerViewZoomed;

		// Token: 0x04001195 RID: 4501
		private float m_PlayerHealth;

		// Token: 0x04001196 RID: 4502
		private AntiCheatCommonPlayerMovementState m_PlayerMovementState;
	}
}
