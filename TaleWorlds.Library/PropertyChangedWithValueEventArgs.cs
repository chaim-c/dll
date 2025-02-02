using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000046 RID: 70
	public class PropertyChangedWithValueEventArgs
	{
		// Token: 0x06000253 RID: 595 RVA: 0x000077AF File Offset: 0x000059AF
		public PropertyChangedWithValueEventArgs(string propertyName, object value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000077C5 File Offset: 0x000059C5
		public string PropertyName { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000077CD File Offset: 0x000059CD
		public object Value { get; }
	}
}
