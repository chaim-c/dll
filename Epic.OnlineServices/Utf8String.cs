using System;
using System.Diagnostics;
using System.Text;

namespace Epic.OnlineServices
{
	// Token: 0x02000010 RID: 16
	[DebuggerDisplay("{ToString()}")]
	public sealed class Utf8String
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003F24 File Offset: 0x00002124
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003F2C File Offset: 0x0000212C
		public int Length { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003F35 File Offset: 0x00002135
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003F3D File Offset: 0x0000213D
		public byte[] Bytes { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003F48 File Offset: 0x00002148
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003FD0 File Offset: 0x000021D0
		private string Utf16
		{
			get
			{
				bool flag = this.Length > 0;
				string result;
				if (flag)
				{
					result = Encoding.UTF8.GetString(this.Bytes, 0, this.Length);
				}
				else
				{
					bool flag2 = this.Bytes == null;
					if (flag2)
					{
						throw new Exception("Bytes array is null.");
					}
					bool flag3 = this.Bytes.Length == 0 || this.Bytes[this.Bytes.Length - 1] > 0;
					if (flag3)
					{
						throw new Exception("Bytes array is not null terminated.");
					}
					result = "";
				}
				return result;
			}
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.Bytes = new byte[Encoding.UTF8.GetMaxByteCount(value.Length) + 1];
					this.Length = Encoding.UTF8.GetBytes(value, 0, value.Length, this.Bytes, 0);
				}
				else
				{
					this.Length = 0;
				}
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004032 File Offset: 0x00002232
		public Utf8String()
		{
			this.Length = 0;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004044 File Offset: 0x00002244
		public Utf8String(byte[] bytes)
		{
			bool flag = bytes == null;
			if (flag)
			{
				throw new ArgumentNullException("bytes");
			}
			bool flag2 = bytes.Length == 0 || bytes[bytes.Length - 1] > 0;
			if (flag2)
			{
				throw new ArgumentException("Argument is not null terminated.", "bytes");
			}
			this.Bytes = bytes;
			this.Length = this.Bytes.Length - 1;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000040AC File Offset: 0x000022AC
		public Utf8String(string value)
		{
			this.Utf16 = value;
		}

		// Token: 0x17000007 RID: 7
		public byte this[int index]
		{
			get
			{
				return this.Bytes[index];
			}
			set
			{
				this.Bytes[index] = value;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040E8 File Offset: 0x000022E8
		public static explicit operator Utf8String(byte[] bytes)
		{
			return new Utf8String(bytes);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004100 File Offset: 0x00002300
		public static explicit operator byte[](Utf8String u8str)
		{
			return u8str.Bytes;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004118 File Offset: 0x00002318
		public static implicit operator Utf8String(string str)
		{
			return new Utf8String(str);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004130 File Offset: 0x00002330
		public static implicit operator string(Utf8String u8str)
		{
			bool flag = u8str != null;
			string result;
			if (flag)
			{
				result = u8str.ToString();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004158 File Offset: 0x00002358
		public static Utf8String operator +(Utf8String left, Utf8String right)
		{
			byte[] array = new byte[left.Length + right.Length + 1];
			Buffer.BlockCopy(left.Bytes, 0, array, 0, left.Length);
			Buffer.BlockCopy(right.Bytes, 0, array, left.Length, right.Length + 1);
			return new Utf8String(array);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000041B8 File Offset: 0x000023B8
		public static bool operator ==(Utf8String left, Utf8String right)
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

		// Token: 0x06000096 RID: 150 RVA: 0x000041EC File Offset: 0x000023EC
		public static bool operator !=(Utf8String left, Utf8String right)
		{
			return !(left == right);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004208 File Offset: 0x00002408
		public override bool Equals(object obj)
		{
			Utf8String utf8String = obj as Utf8String;
			bool flag = utf8String == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this == utf8String;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = this.Length != utf8String.Length;
					if (flag3)
					{
						result = false;
					}
					else
					{
						for (int i = 0; i < this.Length; i++)
						{
							bool flag4 = this[i] != utf8String[i];
							if (flag4)
							{
								return false;
							}
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004298 File Offset: 0x00002498
		public override string ToString()
		{
			return this.Utf16;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000042B0 File Offset: 0x000024B0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}
	}
}
