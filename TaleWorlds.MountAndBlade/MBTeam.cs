using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DB RID: 475
	public struct MBTeam
	{
		// Token: 0x06001AC3 RID: 6851 RVA: 0x0005D1CF File Offset: 0x0005B3CF
		internal MBTeam(Mission mission, int index)
		{
			this._mission = mission;
			this.Index = index;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x0005D1DF File Offset: 0x0005B3DF
		public static MBTeam InvalidTeam
		{
			get
			{
				return new MBTeam(null, -1);
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0005D1E8 File Offset: 0x0005B3E8
		public override int GetHashCode()
		{
			return this.Index;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0005D1F0 File Offset: 0x0005B3F0
		public override bool Equals(object obj)
		{
			return ((MBTeam)obj).Index == this.Index;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x0005D205 File Offset: 0x0005B405
		public static bool operator ==(MBTeam team1, MBTeam team2)
		{
			return team1.Index == team2.Index;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005D215 File Offset: 0x0005B415
		public static bool operator !=(MBTeam team1, MBTeam team2)
		{
			return team1.Index != team2.Index;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0005D228 File Offset: 0x0005B428
		public bool IsValid
		{
			get
			{
				return this.Index >= 0;
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x0005D236 File Offset: 0x0005B436
		public bool IsEnemyOf(MBTeam otherTeam)
		{
			return MBAPI.IMBTeam.IsEnemy(this._mission.Pointer, this.Index, otherTeam.Index);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0005D259 File Offset: 0x0005B459
		public void SetIsEnemyOf(MBTeam otherTeam, bool isEnemyOf)
		{
			MBAPI.IMBTeam.SetIsEnemy(this._mission.Pointer, this.Index, otherTeam.Index, isEnemyOf);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x0005D27D File Offset: 0x0005B47D
		public override string ToString()
		{
			return "Mission Team: " + this.Index;
		}

		// Token: 0x0400085E RID: 2142
		public readonly int Index;

		// Token: 0x0400085F RID: 2143
		private readonly Mission _mission;
	}
}
