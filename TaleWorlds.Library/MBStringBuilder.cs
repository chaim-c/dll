using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x0200001D RID: 29
	public struct MBStringBuilder
	{
		// Token: 0x0600009C RID: 156 RVA: 0x00003D07 File Offset: 0x00001F07
		public void Initialize(int capacity = 16, [CallerMemberName] string callerMemberName = "")
		{
			this._cachedStringBuilder = MBStringBuilder.CachedStringBuilder.Acquire(capacity);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D15 File Offset: 0x00001F15
		public string ToStringAndRelease()
		{
			string result = this._cachedStringBuilder.ToString();
			this.Release();
			return result;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D28 File Offset: 0x00001F28
		public void Release()
		{
			MBStringBuilder.CachedStringBuilder.Release(this._cachedStringBuilder);
			this._cachedStringBuilder = null;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D3C File Offset: 0x00001F3C
		public MBStringBuilder Append(char value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003D51 File Offset: 0x00001F51
		public MBStringBuilder Append(int value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D66 File Offset: 0x00001F66
		public MBStringBuilder Append(uint value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003D7B File Offset: 0x00001F7B
		public MBStringBuilder Append(float value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003D90 File Offset: 0x00001F90
		public MBStringBuilder Append(double value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public MBStringBuilder Append<T>(T value)
		{
			this._cachedStringBuilder.Append(value);
			return this;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003DBF File Offset: 0x00001FBF
		public MBStringBuilder AppendLine()
		{
			this._cachedStringBuilder.AppendLine();
			return this;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003DD3 File Offset: 0x00001FD3
		public MBStringBuilder AppendLine<T>(T value)
		{
			this.Append<T>(value);
			this.AppendLine();
			return this;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003DEA File Offset: 0x00001FEA
		public int Length
		{
			get
			{
				return this._cachedStringBuilder.Length;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003DF7 File Offset: 0x00001FF7
		public override string ToString()
		{
			Debug.FailedAssert("Don't use this. Use ToStringAndRelease instead!", "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\Base\\TaleWorlds.Library\\CachedStringBuilder.cs", "ToString", 190);
			return null;
		}

		// Token: 0x04000058 RID: 88
		private StringBuilder _cachedStringBuilder;

		// Token: 0x020000C2 RID: 194
		private static class CachedStringBuilder
		{
			// Token: 0x060006DF RID: 1759 RVA: 0x00015977 File Offset: 0x00013B77
			public static StringBuilder Acquire(int capacity = 16)
			{
				if (capacity <= 4096 && MBStringBuilder.CachedStringBuilder._cachedStringBuilder != null)
				{
					StringBuilder cachedStringBuilder = MBStringBuilder.CachedStringBuilder._cachedStringBuilder;
					MBStringBuilder.CachedStringBuilder._cachedStringBuilder = null;
					cachedStringBuilder.EnsureCapacity(capacity);
					return cachedStringBuilder;
				}
				return new StringBuilder(capacity);
			}

			// Token: 0x060006E0 RID: 1760 RVA: 0x000159A2 File Offset: 0x00013BA2
			public static void Release(StringBuilder sb)
			{
				if (sb.Capacity <= 4096)
				{
					MBStringBuilder.CachedStringBuilder._cachedStringBuilder = sb;
					MBStringBuilder.CachedStringBuilder._cachedStringBuilder.Clear();
				}
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x000159C2 File Offset: 0x00013BC2
			public static string GetStringAndReleaseBuilder(StringBuilder sb)
			{
				string result = sb.ToString();
				MBStringBuilder.CachedStringBuilder.Release(sb);
				return result;
			}

			// Token: 0x04000225 RID: 549
			private const int MaxBuilderSize = 4096;

			// Token: 0x04000226 RID: 550
			[ThreadStatic]
			private static StringBuilder _cachedStringBuilder;
		}
	}
}
