﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D1 RID: 209
	[NullableContext(2)]
	[Nullable(0)]
	internal abstract class PathFilter
	{
		// Token: 0x06000BEC RID: 3052
		[NullableContext(1)]
		public abstract IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings);

		// Token: 0x06000BED RID: 3053 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
		protected static JToken GetTokenIndex([Nullable(1)] JToken t, JsonSelectSettings settings, int index)
		{
			JArray jarray = t as JArray;
			if (jarray != null)
			{
				if (jarray.Count > index)
				{
					return jarray[index];
				}
				if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Index {0} outside the bounds of JArray.".FormatWith(CultureInfo.InvariantCulture, index));
				}
				return null;
			}
			else
			{
				JConstructor jconstructor = t as JConstructor;
				if (jconstructor != null)
				{
					if (jconstructor.Count > index)
					{
						return jconstructor[index];
					}
					if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new JsonException("Index {0} outside the bounds of JConstructor.".FormatWith(CultureInfo.InvariantCulture, index));
					}
					return null;
				}
				else
				{
					if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new JsonException("Index {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, index, t.GetType().Name));
					}
					return null;
				}
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		protected static JToken GetNextScanValue([Nullable(1)] JToken originalParent, JToken container, JToken value)
		{
			if (container != null && container.HasValues)
			{
				value = container.First;
			}
			else
			{
				while (value != null && value != originalParent && value == value.Parent.Last)
				{
					value = value.Parent;
				}
				if (value == null || value == originalParent)
				{
					return null;
				}
				value = value.Next;
			}
			return value;
		}
	}
}
