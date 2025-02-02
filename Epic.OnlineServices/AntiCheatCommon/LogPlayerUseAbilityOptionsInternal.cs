using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F9 RID: 1529
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerUseAbilityOptionsInternal : ISettable<LogPlayerUseAbilityOptions>, IDisposable
	{
		// Token: 0x17000BAB RID: 2987
		// (set) Token: 0x06002721 RID: 10017 RVA: 0x0003A4D1 File Offset: 0x000386D1
		public IntPtr PlayerHandle
		{
			set
			{
				this.m_PlayerHandle = value;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (set) Token: 0x06002722 RID: 10018 RVA: 0x0003A4DB File Offset: 0x000386DB
		public uint AbilityId
		{
			set
			{
				this.m_AbilityId = value;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (set) Token: 0x06002723 RID: 10019 RVA: 0x0003A4E5 File Offset: 0x000386E5
		public uint AbilityDurationMs
		{
			set
			{
				this.m_AbilityDurationMs = value;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (set) Token: 0x06002724 RID: 10020 RVA: 0x0003A4EF File Offset: 0x000386EF
		public uint AbilityCooldownMs
		{
			set
			{
				this.m_AbilityCooldownMs = value;
			}
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x0003A4F9 File Offset: 0x000386F9
		public void Set(ref LogPlayerUseAbilityOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlayerHandle = other.PlayerHandle;
			this.AbilityId = other.AbilityId;
			this.AbilityDurationMs = other.AbilityDurationMs;
			this.AbilityCooldownMs = other.AbilityCooldownMs;
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0003A538 File Offset: 0x00038738
		public void Set(ref LogPlayerUseAbilityOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlayerHandle = other.Value.PlayerHandle;
				this.AbilityId = other.Value.AbilityId;
				this.AbilityDurationMs = other.Value.AbilityDurationMs;
				this.AbilityCooldownMs = other.Value.AbilityCooldownMs;
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0003A5AD File Offset: 0x000387AD
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlayerHandle);
		}

		// Token: 0x0400119B RID: 4507
		private int m_ApiVersion;

		// Token: 0x0400119C RID: 4508
		private IntPtr m_PlayerHandle;

		// Token: 0x0400119D RID: 4509
		private uint m_AbilityId;

		// Token: 0x0400119E RID: 4510
		private uint m_AbilityDurationMs;

		// Token: 0x0400119F RID: 4511
		private uint m_AbilityCooldownMs;
	}
}
