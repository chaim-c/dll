using System;

namespace SandBox.ViewModelCollection.GameOver
{
	// Token: 0x0200003C RID: 60
	public class StatItem
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00013060 File Offset: 0x00011260
		public StatItem(string id, string value, StatItem.StatType type = StatItem.StatType.None)
		{
			this.ID = id;
			this.Value = value;
			this.Type = type;
		}

		// Token: 0x04000236 RID: 566
		public readonly string ID;

		// Token: 0x04000237 RID: 567
		public readonly string Value;

		// Token: 0x04000238 RID: 568
		public readonly StatItem.StatType Type;

		// Token: 0x020000A6 RID: 166
		public enum StatType
		{
			// Token: 0x0400037E RID: 894
			None,
			// Token: 0x0400037F RID: 895
			Influence,
			// Token: 0x04000380 RID: 896
			Issue,
			// Token: 0x04000381 RID: 897
			Tournament,
			// Token: 0x04000382 RID: 898
			Gold,
			// Token: 0x04000383 RID: 899
			Crime,
			// Token: 0x04000384 RID: 900
			Kill
		}
	}
}
