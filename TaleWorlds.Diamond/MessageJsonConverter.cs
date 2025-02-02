using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TaleWorlds.Diamond
{
	// Token: 0x0200001E RID: 30
	public class MessageJsonConverter : JsonConverter
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002C8C File Offset: 0x00000E8C
		public override bool CanConvert(Type objectType)
		{
			return typeof(Message).IsAssignableFrom(objectType);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			string text = (string)jobject["_type"];
			Type type;
			if (text.StartsWith("Messages.") && MessageJsonConverter._knownTypes.TryGetValue(text, out type))
			{
				Message message = (Message)Activator.CreateInstance(type);
				serializer.Populate(jobject.CreateReader(), message);
				return message;
			}
			return null;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002CFE File Offset: 0x00000EFE
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002D04 File Offset: 0x00000F04
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			JProperty content = new JProperty("_type", value.GetType().FullName);
			JObject jobject = new JObject();
			jobject.Add(content);
			foreach (PropertyInfo propertyInfo in value.GetType().GetProperties())
			{
				if (propertyInfo.CanRead)
				{
					object value2 = propertyInfo.GetValue(value);
					if (value2 != null)
					{
						jobject.Add(propertyInfo.Name, JToken.FromObject(value2, serializer));
					}
				}
			}
			jobject.WriteTo(writer, Array.Empty<JsonConverter>());
		}

		// Token: 0x04000027 RID: 39
		private static readonly Dictionary<string, Type> _knownTypes = (from t in (from a in AppDomain.CurrentDomain.GetAssemblies()
		where !a.GlobalAssemblyCache
		select a).SelectMany((Assembly a) => a.GetTypes())
		where t.IsSubclassOf(typeof(Message))
		select t).ToDictionary((Type item) => item.FullName, (Type item) => item);
	}
}
