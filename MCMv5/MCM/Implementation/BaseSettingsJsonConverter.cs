using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Abstractions.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCM.Implementation
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BaseSettingsJsonConverter : JsonConverter
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00004A04 File Offset: 0x00002C04
		public BaseSettingsJsonConverter(IBUTRLogger logger, [Nullable(new byte[]
		{
			1,
			1,
			2
		})] Action<string, object> addSerializationProperty, Action clearSerializationProperties)
		{
			this._logger = logger;
			this._addSerializationProperty = addSerializationProperty;
			this._clearSerializationProperties = clearSerializationProperties;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004A23 File Offset: 0x00002C23
		public override bool CanConvert(Type objectType)
		{
			return objectType.IsSubclassOf(typeof(BaseSettings));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004A38 File Offset: 0x00002C38
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			BaseSettings settings = value as BaseSettings;
			bool flag = settings == null;
			if (!flag)
			{
				JObject jo = new JObject();
				foreach (ISettingsPropertyDefinition definition in settings.GetAllSettingPropertyDefinitions())
				{
					bool flag2 = definition.SettingType == SettingType.Button;
					if (!flag2)
					{
						jo.Add(definition.Id, (definition.PropertyReference.Value == null) ? null : JToken.FromObject(definition.PropertyReference.Value, serializer));
					}
				}
				jo.WriteTo(writer, Array.Empty<JsonConverter>());
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004AF4 File Offset: 0x00002CF4
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			BaseSettings settings = existingValue as BaseSettings;
			bool flag = settings == null;
			object result;
			if (flag)
			{
				result = existingValue;
			}
			else
			{
				try
				{
					JObject jo = JObject.Load(reader);
					this._clearSerializationProperties();
					foreach (ISettingsPropertyDefinition definition in settings.GetAllSettingPropertyDefinitions())
					{
						bool flag2 = definition.SettingType == SettingType.Button;
						if (!flag2)
						{
							JToken value;
							bool flag3 = jo.TryGetValue(definition.Id, out value);
							if (flag3)
							{
								this._addSerializationProperty(value.CreateReader().Path, definition.PropertyReference.Value);
								definition.PropertyReference.Value = value.ToObject(definition.PropertyReference.Type, serializer);
							}
						}
					}
					this._clearSerializationProperties();
				}
				catch (Exception e)
				{
					this._logger.LogError(e, "Error while deserializing Settings", Array.Empty<object>());
				}
				result = existingValue;
			}
			return result;
		}

		// Token: 0x0400003D RID: 61
		private readonly IBUTRLogger _logger;

		// Token: 0x0400003E RID: 62
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private readonly Action<string, object> _addSerializationProperty;

		// Token: 0x0400003F RID: 63
		private readonly Action _clearSerializationProperties;
	}
}
