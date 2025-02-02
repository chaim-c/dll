﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000B6 RID: 182
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x0002725C File Offset: 0x0002545C
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			JArray.<WriteToAsync>d__0 <WriteToAsync>d__;
			<WriteToAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAsync>d__.<>4__this = this;
			<WriteToAsync>d__.writer = writer;
			<WriteToAsync>d__.cancellationToken = cancellationToken;
			<WriteToAsync>d__.converters = converters;
			<WriteToAsync>d__.<>1__state = -1;
			<WriteToAsync>d__.<>t__builder.Start<JArray.<WriteToAsync>d__0>(ref <WriteToAsync>d__);
			return <WriteToAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000272B7 File Offset: 0x000254B7
		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return JArray.LoadAsync(reader, null, cancellationToken);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x000272C4 File Offset: 0x000254C4
		public new static Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			JArray.<LoadAsync>d__2 <LoadAsync>d__;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JArray>.Create();
			<LoadAsync>d__.reader = reader;
			<LoadAsync>d__.settings = settings;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<JArray.<LoadAsync>d__2>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00027317 File Offset: 0x00025517
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0002731F File Offset: 0x0002551F
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00027322 File Offset: 0x00025522
		public JArray()
		{
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00027335 File Offset: 0x00025535
		public JArray(JArray other) : base(other)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00027349 File Offset: 0x00025549
		public JArray(params object[] content) : this(content)
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00027352 File Offset: 0x00025552
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002736C File Offset: 0x0002556C
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002738C File Offset: 0x0002558C
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00027394 File Offset: 0x00025594
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000273A0 File Offset: 0x000255A0
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00027414 File Offset: 0x00025614
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00027420 File Offset: 0x00025620
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jarray;
			}
			return result;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00027468 File Offset: 0x00025668
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00027478 File Offset: 0x00025678
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000274BC File Offset: 0x000256BC
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170001BC RID: 444
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170001BD RID: 445
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002758A File Offset: 0x0002578A
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000275A0 File Offset: 0x000257A0
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = (base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null;
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000275D4 File Offset: 0x000257D4
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000275DD File Offset: 0x000257DD
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000275E9 File Offset: 0x000257E9
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000275F4 File Offset: 0x000257F4
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0002760F File Offset: 0x0002580F
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00027618 File Offset: 0x00025818
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00027620 File Offset: 0x00025820
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00027629 File Offset: 0x00025829
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00027633 File Offset: 0x00025833
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00027636 File Offset: 0x00025836
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002763F File Offset: 0x0002583F
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x0400035A RID: 858
		private readonly List<JToken> _values = new List<JToken>();
	}
}
