using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200009F RID: 159
	public class MBNotNullParameterException : MBException
	{
		// Token: 0x06000828 RID: 2088 RVA: 0x0001BC47 File Offset: 0x00019E47
		public MBNotNullParameterException(string parameterName) : base("The parameter must be null : " + parameterName)
		{
		}
	}
}
