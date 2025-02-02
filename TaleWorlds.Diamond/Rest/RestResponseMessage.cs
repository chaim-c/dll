using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003E RID: 62
	[DataContract]
	[Serializable]
	public abstract class RestResponseMessage : RestData
	{
		// Token: 0x0600013C RID: 316
		public abstract Message GetMessage();
	}
}
