using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000049 RID: 73
	public class PropertyChangedWithFloatValueEventArgs
	{
		// Token: 0x0600025C RID: 604 RVA: 0x00007821 File Offset: 0x00005A21
		public PropertyChangedWithFloatValueEventArgs(string propertyName, float value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00007837 File Offset: 0x00005A37
		public string PropertyName { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000783F File Offset: 0x00005A3F
		public float Value { get; }
	}
}
