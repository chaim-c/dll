using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000020 RID: 32
	public class PeerIdJsonConverter : JsonConverter
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00003125 File Offset: 0x00001325
		public override bool CanConvert(Type objectType)
		{
			return typeof(PeerId).IsAssignableFrom(objectType);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003137 File Offset: 0x00001337
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return PeerId.FromString((string)JObject.Load(reader)["_peerId"]);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003158 File Offset: 0x00001358
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000315C File Offset: 0x0000135C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JProperty content = new JProperty("_peerId", ((PeerId)value).ToString());
			new JObject
			{
				content
			}.WriteTo(writer, Array.Empty<JsonConverter>());
		}
	}
}
