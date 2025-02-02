using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000047 RID: 71
	public class PropertyChangedWithBoolValueEventArgs
	{
		// Token: 0x06000256 RID: 598 RVA: 0x000077D5 File Offset: 0x000059D5
		public PropertyChangedWithBoolValueEventArgs(string propertyName, bool value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000257 RID: 599 RVA: 0x000077EB File Offset: 0x000059EB
		public string PropertyName { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000077F3 File Offset: 0x000059F3
		public bool Value { get; }
	}
}
