using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000037 RID: 55
	[DataContract]
	[Serializable]
	public abstract class RestData
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003E81 File Offset: 0x00002081
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00003E89 File Offset: 0x00002089
		[DataMember]
		public string TypeName { get; set; }

		// Token: 0x06000121 RID: 289 RVA: 0x00003E92 File Offset: 0x00002092
		protected RestData()
		{
			this.TypeName = base.GetType().FullName;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003EAB File Offset: 0x000020AB
		public string SerializeAsJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
