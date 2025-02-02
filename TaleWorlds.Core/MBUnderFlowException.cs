using System;

namespace TaleWorlds.Core
{
	// Token: 0x0200009A RID: 154
	public class MBUnderFlowException : MBException
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x0001BBDB File Offset: 0x00019DDB
		public MBUnderFlowException() : base("The given value is less than the expected value.")
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001BBE8 File Offset: 0x00019DE8
		public MBUnderFlowException(string parameterName) : base("The given value is less than the expected value : " + parameterName)
		{
		}
	}
}
