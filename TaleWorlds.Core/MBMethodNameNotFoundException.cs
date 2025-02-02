using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200009C RID: 156
	public class MBMethodNameNotFoundException : MBException
	{
		// Token: 0x06000825 RID: 2085 RVA: 0x0001BC0E File Offset: 0x00019E0E
		public MBMethodNameNotFoundException(string methodName) : base("Unable to find method " + methodName)
		{
		}
	}
}
