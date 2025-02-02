using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using MCM.Abstractions;
using MCM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MCM.Implementation
{
	// Token: 0x02000024 RID: 36
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DropdownJsonConverter : JsonConverter
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00004C24 File Offset: 0x00002E24
		public DropdownJsonConverter(IBUTRLogger logger, [Nullable(new byte[]
		{
			1,
			1,
			2
		})] Func<string, object> getSerializationProperty)
		{
			this._logger = logger;
			this._getSerializationProperty = getSerializationProperty;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004C3C File Offset: 0x00002E3C
		public override bool CanConvert(Type objectType)
		{
			return SettingsUtils.IsForGenericDropdown(objectType);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004C44 File Offset: 0x00002E44
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			JToken token = JToken.FromObject(new SelectedIndexWrapper(value).SelectedIndex);
			token.WriteTo(writer, Array.Empty<JsonConverter>());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004C78 File Offset: 0x00002E78
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			try
			{
				if (existingValue == null)
				{
					existingValue = this._getSerializationProperty(reader.Path);
				}
				bool flag = existingValue == null;
				if (flag)
				{
					return null;
				}
				SelectedIndexWrapper wrapper = new SelectedIndexWrapper(existingValue);
				JToken token = JToken.Load(reader);
				int res;
				wrapper.SelectedIndex = (int.TryParse(token.ToString(), out res) ? res : wrapper.SelectedIndex);
			}
			catch (Exception e)
			{
				this._logger.LogError(e, "Error while deserializing Dropdown", Array.Empty<object>());
			}
			return existingValue;
		}

		// Token: 0x04000040 RID: 64
		private readonly IBUTRLogger _logger;

		// Token: 0x04000041 RID: 65
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private readonly Func<string, object> _getSerializationProperty;
	}
}
