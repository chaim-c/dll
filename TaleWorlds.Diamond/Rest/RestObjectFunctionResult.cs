using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003C RID: 60
	[DataContract]
	[Serializable]
	public class RestObjectFunctionResult : RestFunctionResult
	{
		// Token: 0x06000136 RID: 310 RVA: 0x0000401B File Offset: 0x0000221B
		public override FunctionResult GetFunctionResult()
		{
			return this._functionResult;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004023 File Offset: 0x00002223
		public RestObjectFunctionResult()
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000402B File Offset: 0x0000222B
		public RestObjectFunctionResult(FunctionResult functionResult)
		{
			this._functionResult = functionResult;
		}

		// Token: 0x04000054 RID: 84
		[DataMember]
		private FunctionResult _functionResult;
	}
}
