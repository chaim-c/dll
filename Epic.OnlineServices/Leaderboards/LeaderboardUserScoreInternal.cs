using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000410 RID: 1040
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardUserScoreInternal : IGettable<LeaderboardUserScore>, ISettable<LeaderboardUserScore>, IDisposable
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00027E5C File Offset: 0x0002605C
		// (set) Token: 0x06001AD7 RID: 6871 RVA: 0x00027E7D File Offset: 0x0002607D
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00027E90 File Offset: 0x00026090
		// (set) Token: 0x06001AD9 RID: 6873 RVA: 0x00027EA8 File Offset: 0x000260A8
		public int Score
		{
			get
			{
				return this.m_Score;
			}
			set
			{
				this.m_Score = value;
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x00027EB2 File Offset: 0x000260B2
		public void Set(ref LeaderboardUserScore other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
			this.Score = other.Score;
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x00027ED8 File Offset: 0x000260D8
		public void Set(ref LeaderboardUserScore? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
				this.Score = other.Value.Score;
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x00027F23 File Offset: 0x00026123
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00027F32 File Offset: 0x00026132
		public void Get(out LeaderboardUserScore output)
		{
			output = default(LeaderboardUserScore);
			output.Set(ref this);
		}

		// Token: 0x04000BFC RID: 3068
		private int m_ApiVersion;

		// Token: 0x04000BFD RID: 3069
		private IntPtr m_UserId;

		// Token: 0x04000BFE RID: 3070
		private int m_Score;
	}
}
