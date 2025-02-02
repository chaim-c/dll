﻿using System;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000C6 RID: 198
	[NullableContext(2)]
	[Nullable(0)]
	public class JTokenWriter : JsonWriter
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x0002CC39 File Offset: 0x0002AE39
		[NullableContext(1)]
		internal override Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			if (reader is JTokenReader)
			{
				this.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return AsyncUtils.CompletedTask;
			}
			return base.WriteTokenSyncReadingAsync(reader, cancellationToken);
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0002CC5D File Offset: 0x0002AE5D
		public JToken CurrentToken
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x0002CC65 File Offset: 0x0002AE65
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0002CC7C File Offset: 0x0002AE7C
		[NullableContext(1)]
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0002CC9D File Offset: 0x0002AE9D
		public JTokenWriter()
		{
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0002CCA5 File Offset: 0x0002AEA5
		public override void Flush()
		{
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0002CCA7 File Offset: 0x0002AEA7
		public override void Close()
		{
			base.Close();
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002CCAF File Offset: 0x0002AEAF
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002CCC2 File Offset: 0x0002AEC2
		[NullableContext(1)]
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
			this._current = container;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002CCF0 File Offset: 0x0002AEF0
		private void RemoveParent()
		{
			this._current = this._parent;
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0002CD41 File Offset: 0x0002AF41
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002CD54 File Offset: 0x0002AF54
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002CD69 File Offset: 0x0002AF69
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0002CD71 File Offset: 0x0002AF71
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			JObject jobject = this._parent as JObject;
			if (jobject != null)
			{
				jobject.Remove(name);
			}
			this.AddParent(new JProperty(name));
			base.WritePropertyName(name);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0002CD9E File Offset: 0x0002AF9E
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent.TryAdd(value))
				{
					this._current = this._parent.Last;
					if (this._parent.Type == JTokenType.Property)
					{
						this._parent = this._parent.Parent;
						return;
					}
				}
			}
			else
			{
				this._value = (value ?? JValue.CreateNull());
				this._current = this._value;
			}
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0002CE20 File Offset: 0x0002B020
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0002CE41 File Offset: 0x0002B041
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0002CE52 File Offset: 0x0002B052
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0002CE63 File Offset: 0x0002B063
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0002CE79 File Offset: 0x0002B079
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0002CE8F File Offset: 0x0002B08F
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0002CEA1 File Offset: 0x0002B0A1
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0002CEB7 File Offset: 0x0002B0B7
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0002CECD File Offset: 0x0002B0CD
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0002CEE3 File Offset: 0x0002B0E3
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002CEF9 File Offset: 0x0002B0F9
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0002CF0F File Offset: 0x0002B10F
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0002CF25 File Offset: 0x0002B125
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002CF3C File Offset: 0x0002B13C
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CF52 File Offset: 0x0002B152
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CF68 File Offset: 0x0002B168
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString(CultureInfo.InvariantCulture);
			this.AddValue(value2, JsonToken.String);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002CF92 File Offset: 0x0002B192
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002CFA8 File Offset: 0x0002B1A8
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002CFBE File Offset: 0x0002B1BE
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002CFF9 File Offset: 0x0002B1F9
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002D010 File Offset: 0x0002B210
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D022 File Offset: 0x0002B222
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D039 File Offset: 0x0002B239
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D050 File Offset: 0x0002B250
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002D064 File Offset: 0x0002B264
		[NullableContext(1)]
		internal override void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			JTokenReader jtokenReader = reader as JTokenReader;
			if (jtokenReader == null || !writeChildren || !writeDateConstructorAsDate || !writeComments)
			{
				base.WriteToken(reader, writeChildren, writeDateConstructorAsDate, writeComments);
				return;
			}
			if (jtokenReader.TokenType == JsonToken.None && !jtokenReader.Read())
			{
				return;
			}
			JToken jtoken = jtokenReader.CurrentToken.CloneToken();
			if (this._parent != null)
			{
				this._parent.Add(jtoken);
				this._current = this._parent.Last;
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					base.InternalWriteValue(JsonToken.Null);
				}
			}
			else
			{
				this._current = jtoken;
				if (this._token == null && this._value == null)
				{
					this._token = (jtoken as JContainer);
					this._value = (jtoken as JValue);
				}
			}
			jtokenReader.Skip();
		}

		// Token: 0x04000399 RID: 921
		private JContainer _token;

		// Token: 0x0400039A RID: 922
		private JContainer _parent;

		// Token: 0x0400039B RID: 923
		private JValue _value;

		// Token: 0x0400039C RID: 924
		private JToken _current;
	}
}
