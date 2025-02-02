﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000CD RID: 205
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0002E4B1 File Offset: 0x0002C6B1
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0002E4B9 File Offset: 0x0002C6B9
		public int? Start { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0002E4C2 File Offset: 0x0002C6C2
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0002E4CA File Offset: 0x0002C6CA
		public int? End { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0002E4D3 File Offset: 0x0002C6D3
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0002E4DB File Offset: 0x0002C6DB
		public int? Step { get; set; }

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			int? num = this.Step;
			int num2 = 0;
			if (num.GetValueOrDefault() == num2 & num != null)
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken jtoken in current)
			{
				JArray a = jtoken as JArray;
				if (a != null)
				{
					int stepCount = this.Step ?? 1;
					int num3 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : -1);
					num = this.Start;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						num3 = a.Count + num3;
					}
					num = this.End;
					num2 = 0;
					if (num.GetValueOrDefault() < num2 & num != null)
					{
						stopIndex = a.Count + stopIndex;
					}
					num3 = Math.Max(num3, (stepCount > 0) ? 0 : int.MinValue);
					num3 = Math.Min(num3, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num3, stopIndex, positiveStep))
					{
						int i = num3;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
				a = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002E502 File Offset: 0x0002C702
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
