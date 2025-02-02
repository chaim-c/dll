using System;
using System.Runtime.Serialization;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000038 RID: 56
	[DataContract]
	[Serializable]
	public class RestDataFunctionResult : RestFunctionResult
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003EB3 File Offset: 0x000020B3
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00003EBB File Offset: 0x000020BB
		[DataMember]
		public byte[] FunctionResultData { get; private set; }

		// Token: 0x06000125 RID: 293 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override FunctionResult GetFunctionResult()
		{
			return (FunctionResult)Common.DeserializeObject(this.FunctionResultData);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003ED6 File Offset: 0x000020D6
		public RestDataFunctionResult()
		{
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003EDE File Offset: 0x000020DE
		public RestDataFunctionResult(FunctionResult functionResult)
		{
			this.FunctionResultData = Common.SerializeObject(functionResult);
		}
	}
}
