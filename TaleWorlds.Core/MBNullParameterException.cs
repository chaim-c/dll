using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200009E RID: 158
	public class MBNullParameterException : MBException
	{
		// Token: 0x06000827 RID: 2087 RVA: 0x0001BC34 File Offset: 0x00019E34
		public MBNullParameterException(string parameterName) : base("The parameter cannot be null : " + parameterName)
		{
		}
	}
}
