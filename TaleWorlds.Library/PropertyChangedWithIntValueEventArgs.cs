using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000048 RID: 72
	public class PropertyChangedWithIntValueEventArgs
	{
		// Token: 0x06000259 RID: 601 RVA: 0x000077FB File Offset: 0x000059FB
		public PropertyChangedWithIntValueEventArgs(string propertyName, int value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007811 File Offset: 0x00005A11
		public string PropertyName { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00007819 File Offset: 0x00005A19
		public int Value { get; }
	}
}
