using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003B RID: 59
	[DataContract]
	[Serializable]
	public abstract class RestFunctionResult : RestData
	{
		// Token: 0x06000134 RID: 308
		public abstract FunctionResult GetFunctionResult();
	}
}
