﻿using System;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200001C RID: 28
	[JsonConverter(typeof(MessageJsonConverter))]
	[Serializable]
	public abstract class Message
	{
	}
}
