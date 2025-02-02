using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000312 RID: 786
	[AttributeUsage(AttributeTargets.Field)]
	public class MultiplayerOptionsProperty : Attribute
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000A54CA File Offset: 0x000A36CA
		public bool HasBounds
		{
			get
			{
				return this.BoundsMax > this.BoundsMin;
			}
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x000A54DC File Offset: 0x000A36DC
		public MultiplayerOptionsProperty(MultiplayerOptions.OptionValueType optionValueType, MultiplayerOptionsProperty.ReplicationOccurrence replicationOccurrence, string description = null, int boundsMin = 0, int boundsMax = 0, string[] validGameModes = null, bool hasMultipleSelections = false, Type enumType = null)
		{
			this.OptionValueType = optionValueType;
			this.Replication = replicationOccurrence;
			this.Description = description;
			this.BoundsMin = boundsMin;
			this.BoundsMax = boundsMax;
			this.ValidGameModes = validGameModes;
			this.HasMultipleSelections = hasMultipleSelections;
			this.EnumType = enumType;
		}

		// Token: 0x04001067 RID: 4199
		public readonly MultiplayerOptions.OptionValueType OptionValueType;

		// Token: 0x04001068 RID: 4200
		public readonly MultiplayerOptionsProperty.ReplicationOccurrence Replication;

		// Token: 0x04001069 RID: 4201
		public readonly string Description;

		// Token: 0x0400106A RID: 4202
		public readonly int BoundsMin;

		// Token: 0x0400106B RID: 4203
		public readonly int BoundsMax;

		// Token: 0x0400106C RID: 4204
		public readonly string[] ValidGameModes;

		// Token: 0x0400106D RID: 4205
		public readonly bool HasMultipleSelections;

		// Token: 0x0400106E RID: 4206
		public readonly Type EnumType;

		// Token: 0x020005D2 RID: 1490
		public enum ReplicationOccurrence
		{
			// Token: 0x04001E9A RID: 7834
			Never,
			// Token: 0x04001E9B RID: 7835
			AtMapLoad,
			// Token: 0x04001E9C RID: 7836
			Immediately
		}
	}
}
