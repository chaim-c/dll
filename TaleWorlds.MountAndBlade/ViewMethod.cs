using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C5 RID: 709
	public class ViewMethod : Attribute
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x00094EAF File Offset: 0x000930AF
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x00094EB7 File Offset: 0x000930B7
		public string Name { get; private set; }

		// Token: 0x060026F5 RID: 9973 RVA: 0x00094EC0 File Offset: 0x000930C0
		public ViewMethod(string name)
		{
			this.Name = name;
		}
	}
}
