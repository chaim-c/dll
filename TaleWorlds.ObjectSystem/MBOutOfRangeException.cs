using System;

namespace TaleWorlds.ObjectSystem
{
	// Token: 0x0200000B RID: 11
	public class MBOutOfRangeException : ObjectSystemException
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000043C7 File Offset: 0x000025C7
		internal MBOutOfRangeException(string parameterName) : base("The given value is out of range : " + parameterName)
		{
		}
	}
}
