using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000005 RID: 5
	public abstract class Handle : IEquatable<Handle>, IFormattable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003D11 File Offset: 0x00001F11
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003D19 File Offset: 0x00001F19
		public IntPtr InnerHandle { get; internal set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003D22 File Offset: 0x00001F22
		public Handle()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003D2C File Offset: 0x00001F2C
		public Handle(IntPtr innerHandle)
		{
			this.InnerHandle = innerHandle;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003D40 File Offset: 0x00001F40
		public static bool operator ==(Handle left, Handle right)
		{
			bool flag = left == null;
			bool result;
			if (flag)
			{
				bool flag2 = right == null;
				result = flag2;
			}
			else
			{
				result = left.Equals(right);
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003D74 File Offset: 0x00001F74
		public static bool operator !=(Handle left, Handle right)
		{
			return !(left == right);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003D90 File Offset: 0x00001F90
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Handle);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public override int GetHashCode()
		{
			return (int)(65536L + this.InnerHandle.ToInt64());
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public bool Equals(Handle other)
		{
			bool flag = other == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == other;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = base.GetType() != other.GetType();
					result = (!flag3 && this.InnerHandle == other.InnerHandle);
				}
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E30 File Offset: 0x00002030
		public override string ToString()
		{
			return this.InnerHandle.ToString();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E50 File Offset: 0x00002050
		public virtual string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = format != null;
			string result;
			if (flag)
			{
				result = this.InnerHandle.ToString(format);
			}
			else
			{
				result = this.InnerHandle.ToString();
			}
			return result;
		}
	}
}
