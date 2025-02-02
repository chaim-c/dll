﻿using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000094 RID: 148
	[NullableContext(1)]
	[Nullable(0)]
	internal class JsonSerializerProxy : JsonSerializer
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060007A2 RID: 1954 RVA: 0x00022409 File Offset: 0x00020609
		// (remove) Token: 0x060007A3 RID: 1955 RVA: 0x00022417 File Offset: 0x00020617
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public override event EventHandler<ErrorEventArgs> Error
		{
			add
			{
				this._serializer.Error += value;
			}
			remove
			{
				this._serializer.Error -= value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00022425 File Offset: 0x00020625
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00022432 File Offset: 0x00020632
		[Nullable(2)]
		public override IReferenceResolver ReferenceResolver
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.ReferenceResolver;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.ReferenceResolver = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00022440 File Offset: 0x00020640
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0002244D File Offset: 0x0002064D
		[Nullable(2)]
		public override ITraceWriter TraceWriter
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.TraceWriter;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.TraceWriter = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002245B File Offset: 0x0002065B
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x00022468 File Offset: 0x00020668
		[Nullable(2)]
		public override IEqualityComparer EqualityComparer
		{
			[NullableContext(2)]
			get
			{
				return this._serializer.EqualityComparer;
			}
			[NullableContext(2)]
			set
			{
				this._serializer.EqualityComparer = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x00022476 File Offset: 0x00020676
		public override JsonConverterCollection Converters
		{
			get
			{
				return this._serializer.Converters;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00022483 File Offset: 0x00020683
		// (set) Token: 0x060007AC RID: 1964 RVA: 0x00022490 File Offset: 0x00020690
		public override DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._serializer.DefaultValueHandling;
			}
			set
			{
				this._serializer.DefaultValueHandling = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0002249E File Offset: 0x0002069E
		// (set) Token: 0x060007AE RID: 1966 RVA: 0x000224AB File Offset: 0x000206AB
		public override IContractResolver ContractResolver
		{
			get
			{
				return this._serializer.ContractResolver;
			}
			set
			{
				this._serializer.ContractResolver = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x000224B9 File Offset: 0x000206B9
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x000224C6 File Offset: 0x000206C6
		public override MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._serializer.MissingMemberHandling;
			}
			set
			{
				this._serializer.MissingMemberHandling = value;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000224D4 File Offset: 0x000206D4
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x000224E1 File Offset: 0x000206E1
		public override NullValueHandling NullValueHandling
		{
			get
			{
				return this._serializer.NullValueHandling;
			}
			set
			{
				this._serializer.NullValueHandling = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000224EF File Offset: 0x000206EF
		// (set) Token: 0x060007B4 RID: 1972 RVA: 0x000224FC File Offset: 0x000206FC
		public override ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._serializer.ObjectCreationHandling;
			}
			set
			{
				this._serializer.ObjectCreationHandling = value;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0002250A File Offset: 0x0002070A
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x00022517 File Offset: 0x00020717
		public override ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._serializer.ReferenceLoopHandling;
			}
			set
			{
				this._serializer.ReferenceLoopHandling = value;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00022525 File Offset: 0x00020725
		// (set) Token: 0x060007B8 RID: 1976 RVA: 0x00022532 File Offset: 0x00020732
		public override PreserveReferencesHandling PreserveReferencesHandling
		{
			get
			{
				return this._serializer.PreserveReferencesHandling;
			}
			set
			{
				this._serializer.PreserveReferencesHandling = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00022540 File Offset: 0x00020740
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0002254D File Offset: 0x0002074D
		public override TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._serializer.TypeNameHandling;
			}
			set
			{
				this._serializer.TypeNameHandling = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0002255B File Offset: 0x0002075B
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x00022568 File Offset: 0x00020768
		public override MetadataPropertyHandling MetadataPropertyHandling
		{
			get
			{
				return this._serializer.MetadataPropertyHandling;
			}
			set
			{
				this._serializer.MetadataPropertyHandling = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00022576 File Offset: 0x00020776
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x00022583 File Offset: 0x00020783
		[Obsolete("TypeNameAssemblyFormat is obsolete. Use TypeNameAssemblyFormatHandling instead.")]
		public override FormatterAssemblyStyle TypeNameAssemblyFormat
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormat;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormat = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00022591 File Offset: 0x00020791
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0002259E File Offset: 0x0002079E
		public override TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling
		{
			get
			{
				return this._serializer.TypeNameAssemblyFormatHandling;
			}
			set
			{
				this._serializer.TypeNameAssemblyFormatHandling = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x000225AC File Offset: 0x000207AC
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x000225B9 File Offset: 0x000207B9
		public override ConstructorHandling ConstructorHandling
		{
			get
			{
				return this._serializer.ConstructorHandling;
			}
			set
			{
				this._serializer.ConstructorHandling = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x000225C7 File Offset: 0x000207C7
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x000225D4 File Offset: 0x000207D4
		[Obsolete("Binder is obsolete. Use SerializationBinder instead.")]
		public override SerializationBinder Binder
		{
			get
			{
				return this._serializer.Binder;
			}
			set
			{
				this._serializer.Binder = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x000225E2 File Offset: 0x000207E2
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x000225EF File Offset: 0x000207EF
		public override ISerializationBinder SerializationBinder
		{
			get
			{
				return this._serializer.SerializationBinder;
			}
			set
			{
				this._serializer.SerializationBinder = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000225FD File Offset: 0x000207FD
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0002260A File Offset: 0x0002080A
		public override StreamingContext Context
		{
			get
			{
				return this._serializer.Context;
			}
			set
			{
				this._serializer.Context = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00022618 File Offset: 0x00020818
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00022625 File Offset: 0x00020825
		public override Formatting Formatting
		{
			get
			{
				return this._serializer.Formatting;
			}
			set
			{
				this._serializer.Formatting = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00022633 File Offset: 0x00020833
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x00022640 File Offset: 0x00020840
		public override DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._serializer.DateFormatHandling;
			}
			set
			{
				this._serializer.DateFormatHandling = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0002264E File Offset: 0x0002084E
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0002265B File Offset: 0x0002085B
		public override DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._serializer.DateTimeZoneHandling;
			}
			set
			{
				this._serializer.DateTimeZoneHandling = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00022669 File Offset: 0x00020869
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00022676 File Offset: 0x00020876
		public override DateParseHandling DateParseHandling
		{
			get
			{
				return this._serializer.DateParseHandling;
			}
			set
			{
				this._serializer.DateParseHandling = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00022684 File Offset: 0x00020884
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00022691 File Offset: 0x00020891
		public override FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._serializer.FloatFormatHandling;
			}
			set
			{
				this._serializer.FloatFormatHandling = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0002269F File Offset: 0x0002089F
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x000226AC File Offset: 0x000208AC
		public override FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._serializer.FloatParseHandling;
			}
			set
			{
				this._serializer.FloatParseHandling = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x000226BA File Offset: 0x000208BA
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x000226C7 File Offset: 0x000208C7
		public override StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._serializer.StringEscapeHandling;
			}
			set
			{
				this._serializer.StringEscapeHandling = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x000226D5 File Offset: 0x000208D5
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x000226E2 File Offset: 0x000208E2
		public override string DateFormatString
		{
			get
			{
				return this._serializer.DateFormatString;
			}
			set
			{
				this._serializer.DateFormatString = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x000226F0 File Offset: 0x000208F0
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x000226FD File Offset: 0x000208FD
		public override CultureInfo Culture
		{
			get
			{
				return this._serializer.Culture;
			}
			set
			{
				this._serializer.Culture = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0002270B File Offset: 0x0002090B
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x00022718 File Offset: 0x00020918
		public override int? MaxDepth
		{
			get
			{
				return this._serializer.MaxDepth;
			}
			set
			{
				this._serializer.MaxDepth = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00022726 File Offset: 0x00020926
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00022733 File Offset: 0x00020933
		public override bool CheckAdditionalContent
		{
			get
			{
				return this._serializer.CheckAdditionalContent;
			}
			set
			{
				this._serializer.CheckAdditionalContent = value;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00022741 File Offset: 0x00020941
		internal JsonSerializerInternalBase GetInternalSerializer()
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader;
			}
			return this._serializerWriter;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00022758 File Offset: 0x00020958
		public JsonSerializerProxy(JsonSerializerInternalReader serializerReader)
		{
			ValidationUtils.ArgumentNotNull(serializerReader, "serializerReader");
			this._serializerReader = serializerReader;
			this._serializer = serializerReader.Serializer;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002277E File Offset: 0x0002097E
		public JsonSerializerProxy(JsonSerializerInternalWriter serializerWriter)
		{
			ValidationUtils.ArgumentNotNull(serializerWriter, "serializerWriter");
			this._serializerWriter = serializerWriter;
			this._serializer = serializerWriter.Serializer;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000227A4 File Offset: 0x000209A4
		[NullableContext(2)]
		internal override object DeserializeInternal([Nullable(1)] JsonReader reader, Type objectType)
		{
			if (this._serializerReader != null)
			{
				return this._serializerReader.Deserialize(reader, objectType, false);
			}
			return this._serializer.Deserialize(reader, objectType);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000227CA File Offset: 0x000209CA
		internal override void PopulateInternal(JsonReader reader, object target)
		{
			if (this._serializerReader != null)
			{
				this._serializerReader.Populate(reader, target);
				return;
			}
			this._serializer.Populate(reader, target);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000227EF File Offset: 0x000209EF
		[NullableContext(2)]
		internal override void SerializeInternal([Nullable(1)] JsonWriter jsonWriter, object value, Type rootType)
		{
			if (this._serializerWriter != null)
			{
				this._serializerWriter.Serialize(jsonWriter, value, rootType);
				return;
			}
			this._serializer.Serialize(jsonWriter, value);
		}

		// Token: 0x040002AE RID: 686
		[Nullable(2)]
		private readonly JsonSerializerInternalReader _serializerReader;

		// Token: 0x040002AF RID: 687
		[Nullable(2)]
		private readonly JsonSerializerInternalWriter _serializerWriter;

		// Token: 0x040002B0 RID: 688
		private readonly JsonSerializer _serializer;
	}
}
