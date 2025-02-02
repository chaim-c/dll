using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200004A RID: 74
	public class PropertyChangedWithUIntValueEventArgs
	{
		// Token: 0x0600025F RID: 607 RVA: 0x00007847 File Offset: 0x00005A47
		public PropertyChangedWithUIntValueEventArgs(string propertyName, uint value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000785D File Offset: 0x00005A5D
		public string PropertyName { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00007865 File Offset: 0x00005A65
		public uint Value { get; }
	}
}
