using System;

namespace TaleWorlds.Library
{
	// Token: 0x020000A3 RID: 163
	public class VirtualFileAttribute : Attribute
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00013BCA File Offset: 0x00011DCA
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00013BD2 File Offset: 0x00011DD2
		public string Name { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x00013BDB File Offset: 0x00011DDB
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x00013BE3 File Offset: 0x00011DE3
		public string Content { get; private set; }

		// Token: 0x060005FF RID: 1535 RVA: 0x00013BEC File Offset: 0x00011DEC
		public VirtualFileAttribute(string name, string content)
		{
			this.Name = name;
			this.Content = content;
		}
	}
}
