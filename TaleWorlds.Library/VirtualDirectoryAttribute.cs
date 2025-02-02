using System;

namespace TaleWorlds.Library
{
	// Token: 0x020000A2 RID: 162
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class VirtualDirectoryAttribute : Attribute
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00013BAA File Offset: 0x00011DAA
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00013BB2 File Offset: 0x00011DB2
		public string Name { get; private set; }

		// Token: 0x060005FA RID: 1530 RVA: 0x00013BBB File Offset: 0x00011DBB
		public VirtualDirectoryAttribute(string name)
		{
			this.Name = name;
		}
	}
}
