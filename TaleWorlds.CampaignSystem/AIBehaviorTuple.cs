﻿using System;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem
{
	// Token: 0x02000087 RID: 135
	public struct AIBehaviorTuple : IEquatable<AIBehaviorTuple>
	{
		// Token: 0x06001084 RID: 4228 RVA: 0x0004BE71 File Offset: 0x0004A071
		public AIBehaviorTuple(IMapPoint party, AiBehavior aiBehavior, bool willGatherArmy = false)
		{
			this.Party = party;
			this.AiBehavior = aiBehavior;
			this.WillGatherArmy = willGatherArmy;
		}

		// Token: 0x06001085 RID: 4229 RVA: 0x0004BE88 File Offset: 0x0004A088
		public override bool Equals(object obj)
		{
			if (!(obj is AIBehaviorTuple))
			{
				return false;
			}
			AIBehaviorTuple aibehaviorTuple = (AIBehaviorTuple)obj;
			return aibehaviorTuple.Party == this.Party && aibehaviorTuple.AiBehavior == this.AiBehavior && aibehaviorTuple.WillGatherArmy == this.WillGatherArmy;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x0004BED2 File Offset: 0x0004A0D2
		public bool Equals(AIBehaviorTuple other)
		{
			return other.Party == this.Party && other.AiBehavior == this.AiBehavior && other.WillGatherArmy == this.WillGatherArmy;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x0004BF00 File Offset: 0x0004A100
		public override int GetHashCode()
		{
			int aiBehavior = (int)this.AiBehavior;
			int num = aiBehavior.GetHashCode();
			num = ((this.Party != null) ? (num * 397 ^ this.Party.GetHashCode()) : num);
			return num * 397 ^ this.WillGatherArmy.GetHashCode();
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x0004BF50 File Offset: 0x0004A150
		public static bool operator ==(AIBehaviorTuple a, AIBehaviorTuple b)
		{
			return a.Party == b.Party && a.AiBehavior == b.AiBehavior && a.WillGatherArmy == b.WillGatherArmy;
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x0004BF7E File Offset: 0x0004A17E
		public static bool operator !=(AIBehaviorTuple a, AIBehaviorTuple b)
		{
			return !(a == b);
		}

		// Token: 0x040005C7 RID: 1479
		public IMapPoint Party;

		// Token: 0x040005C8 RID: 1480
		public AiBehavior AiBehavior;

		// Token: 0x040005C9 RID: 1481
		public bool WillGatherArmy;
	}
}
