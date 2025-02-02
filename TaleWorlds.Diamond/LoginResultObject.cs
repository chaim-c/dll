using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	[JsonConverter(typeof(LoginResultObjectJsonConverter))]
	[Serializable]
	public abstract class LoginResultObject
	{
	}
}
