using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000039 RID: 57
	public class RestDataJsonConverter : JsonConverter<RestData>
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003EF2 File Offset: 0x000020F2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003EF5 File Offset: 0x000020F5
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00003EF8 File Offset: 0x000020F8
		private RestData Create(Type objectType, JObject jObject)
		{
			if (jObject == null)
			{
				throw new ArgumentNullException("jObject");
			}
			string text = null;
			if (jObject["TypeName"] != null)
			{
				text = jObject["TypeName"].Value<string>();
			}
			else if (jObject["typeName"] != null)
			{
				text = jObject["typeName"].Value<string>();
			}
			if (text != null)
			{
				return Activator.CreateInstance(Type.GetType(text)) as RestData;
			}
			return null;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00003F68 File Offset: 0x00002168
		public T ReadJson<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00003F70 File Offset: 0x00002170
		public override RestData ReadJson(JsonReader reader, Type objectType, RestData existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (serializer == null)
			{
				throw new ArgumentNullException("serializer");
			}
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			JObject jobject = JObject.Load(reader);
			RestData restData = this.Create(objectType, jobject);
			serializer.Populate(jobject.CreateReader(), restData);
			return restData;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00003FC5 File Offset: 0x000021C5
		public override void WriteJson(JsonWriter writer, RestData value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
