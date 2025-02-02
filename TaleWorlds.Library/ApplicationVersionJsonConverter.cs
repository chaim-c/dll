using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.Library
{
	// Token: 0x0200000B RID: 11
	public class ApplicationVersionJsonConverter : JsonConverter
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002644 File Offset: 0x00000844
		public override bool CanConvert(Type objectType)
		{
			return typeof(ApplicationVersion).IsAssignableFrom(objectType);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002656 File Offset: 0x00000856
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ApplicationVersion.FromString((string)JObject.Load(reader)["_version"], 45697);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000267C File Offset: 0x0000087C
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002680 File Offset: 0x00000880
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JProperty content = new JProperty("_version", ((ApplicationVersion)value).ToString());
			new JObject
			{
				content
			}.WriteTo(writer, Array.Empty<JsonConverter>());
		}
	}
}
