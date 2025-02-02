using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000253 RID: 595
	public struct MissionObjectId
	{
		// Token: 0x06001FEE RID: 8174 RVA: 0x000716F2 File Offset: 0x0006F8F2
		public MissionObjectId(int id, bool createdAtRuntime = false)
		{
			this.Id = id;
			this.CreatedAtRuntime = createdAtRuntime;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x00071702 File Offset: 0x0006F902
		public static bool operator ==(MissionObjectId a, MissionObjectId b)
		{
			return a.Id == b.Id && a.CreatedAtRuntime == b.CreatedAtRuntime;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x00071722 File Offset: 0x0006F922
		public static bool operator !=(MissionObjectId a, MissionObjectId b)
		{
			return a.Id != b.Id || a.CreatedAtRuntime != b.CreatedAtRuntime;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x00071748 File Offset: 0x0006F948
		public override bool Equals(object obj)
		{
			if (!(obj is MissionObjectId))
			{
				return false;
			}
			MissionObjectId missionObjectId = (MissionObjectId)obj;
			return missionObjectId.Id == this.Id && missionObjectId.CreatedAtRuntime == this.CreatedAtRuntime;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x00071784 File Offset: 0x0006F984
		public override int GetHashCode()
		{
			int num = this.Id;
			if (this.CreatedAtRuntime)
			{
				num |= 1073741824;
			}
			return num.GetHashCode();
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000717B0 File Offset: 0x0006F9B0
		public override string ToString()
		{
			return this.Id + " - " + this.CreatedAtRuntime.ToString();
		}

		// Token: 0x04000BE8 RID: 3048
		public readonly int Id;

		// Token: 0x04000BE9 RID: 3049
		public readonly bool CreatedAtRuntime;

		// Token: 0x04000BEA RID: 3050
		public static readonly MissionObjectId Invalid = new MissionObjectId(-1, false);
	}
}
