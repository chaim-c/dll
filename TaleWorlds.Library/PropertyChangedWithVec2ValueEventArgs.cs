using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200004D RID: 77
	public class PropertyChangedWithVec2ValueEventArgs
	{
		// Token: 0x06000268 RID: 616 RVA: 0x000078B9 File Offset: 0x00005AB9
		public PropertyChangedWithVec2ValueEventArgs(string propertyName, Vec2 value)
		{
			this.PropertyName = propertyName;
			this.Value = value;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000078CF File Offset: 0x00005ACF
		public string PropertyName { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000078D7 File Offset: 0x00005AD7
		public Vec2 Value { get; }
	}
}
