using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000044 RID: 68
	[DataContract]
	[Serializable]
	public sealed class SessionlessRestResponse : RestData
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000164 RID: 356 RVA: 0x000046BD File Offset: 0x000028BD
		// (set) Token: 0x06000165 RID: 357 RVA: 0x000046C5 File Offset: 0x000028C5
		[DataMember]
		public bool Successful { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000166 RID: 358 RVA: 0x000046CE File Offset: 0x000028CE
		// (set) Token: 0x06000167 RID: 359 RVA: 0x000046D6 File Offset: 0x000028D6
		[DataMember]
		public string SuccessfulReason { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000046DF File Offset: 0x000028DF
		// (set) Token: 0x06000169 RID: 361 RVA: 0x000046E7 File Offset: 0x000028E7
		[DataMember]
		public RestFunctionResult FunctionResult { get; set; }

		// Token: 0x0600016B RID: 363 RVA: 0x000046F8 File Offset: 0x000028F8
		public void SetSuccessful(bool successful, string succressfulReason)
		{
			this.Successful = successful;
			this.SuccessfulReason = succressfulReason;
		}
	}
}
