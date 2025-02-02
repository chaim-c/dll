﻿using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000BE RID: 190
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x00029F18 File Offset: 0x00028118
		public static Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw.<CreateAsync>d__0 <CreateAsync>d__;
			<CreateAsync>d__.<>t__builder = AsyncTaskMethodBuilder<JRaw>.Create();
			<CreateAsync>d__.reader = reader;
			<CreateAsync>d__.cancellationToken = cancellationToken;
			<CreateAsync>d__.<>1__state = -1;
			<CreateAsync>d__.<>t__builder.Start<JRaw.<CreateAsync>d__0>(ref <CreateAsync>d__);
			return <CreateAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00029F63 File Offset: 0x00028163
		public JRaw(JRaw other) : base(other)
		{
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00029F6C File Offset: 0x0002816C
		[NullableContext(2)]
		public JRaw(object rawJson) : base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00029F78 File Offset: 0x00028178
		public static JRaw Create(JsonReader reader)
		{
			JRaw result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					result = new JRaw(stringWriter.ToString());
				}
			}
			return result;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00029FE0 File Offset: 0x000281E0
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
