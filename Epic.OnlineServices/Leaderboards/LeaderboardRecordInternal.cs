using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200040D RID: 1037
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardRecordInternal : IGettable<LeaderboardRecord>, ISettable<LeaderboardRecord>, IDisposable
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x000277F8 File Offset: 0x000259F8
		// (set) Token: 0x06001AB5 RID: 6837 RVA: 0x00027819 File Offset: 0x00025A19
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

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0002782C File Offset: 0x00025A2C
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x00027844 File Offset: 0x00025A44
		public uint Rank
		{
			get
			{
				return this.m_Rank;
			}
			set
			{
				this.m_Rank = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x00027850 File Offset: 0x00025A50
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x00027868 File Offset: 0x00025A68
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

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x00027874 File Offset: 0x00025A74
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x00027895 File Offset: 0x00025A95
		public Utf8String UserDisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UserDisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserDisplayName);
			}
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000278A5 File Offset: 0x00025AA5
		public void Set(ref LeaderboardRecord other)
		{
			this.m_ApiVersion = 2;
			this.UserId = other.UserId;
			this.Rank = other.Rank;
			this.Score = other.Score;
			this.UserDisplayName = other.UserDisplayName;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000278E4 File Offset: 0x00025AE4
		public void Set(ref LeaderboardRecord? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UserId = other.Value.UserId;
				this.Rank = other.Value.Rank;
				this.Score = other.Value.Score;
				this.UserDisplayName = other.Value.UserDisplayName;
			}
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00027959 File Offset: 0x00025B59
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_UserDisplayName);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00027974 File Offset: 0x00025B74
		public void Get(out LeaderboardRecord output)
		{
			output = default(LeaderboardRecord);
			output.Set(ref this);
		}

		// Token: 0x04000BE4 RID: 3044
		private int m_ApiVersion;

		// Token: 0x04000BE5 RID: 3045
		private IntPtr m_UserId;

		// Token: 0x04000BE6 RID: 3046
		private uint m_Rank;

		// Token: 0x04000BE7 RID: 3047
		private int m_Score;

		// Token: 0x04000BE8 RID: 3048
		private IntPtr m_UserDisplayName;
	}
}
