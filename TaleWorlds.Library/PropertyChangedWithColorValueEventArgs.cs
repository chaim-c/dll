using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200004B RID: 75
	public class PropertyChangedWithColorValueEventArgs
	{
		// Token: 0x06000262 RID: 610 RVA: 0x0000786D File Offset: 0x00005A6D
		public PropertyChangedWithColorValueEventArgs(string propertyName, Color value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00007883 File Offset: 0x00005A83
		public string PropertyName { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000788B File Offset: 0x00005A8B
		public Color Value { get; }
	}
}
