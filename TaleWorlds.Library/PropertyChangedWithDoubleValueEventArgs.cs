using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200004C RID: 76
	public class PropertyChangedWithDoubleValueEventArgs
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00007893 File Offset: 0x00005A93
		public PropertyChangedWithDoubleValueEventArgs(string propertyName, double value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000078A9 File Offset: 0x00005AA9
		public string PropertyName { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000267 RID: 615 RVA: 0x000078B1 File Offset: 0x00005AB1
		public double Value { get; }
	}
}
