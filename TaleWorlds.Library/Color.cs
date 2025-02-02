using System;
using System.Globalization;
using System.Numerics;

namespace TaleWorlds.Library
{
	// Token: 0x0200001E RID: 30
	public struct Color
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003E13 File Offset: 0x00002013
		public Color(float red, float green, float blue, float alpha = 1f)
		{
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
			this.Alpha = alpha;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003E32 File Offset: 0x00002032
		public Vector3 ToVector3()
		{
			return new Vector3(this.Red, this.Green, this.Blue);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E4B File Offset: 0x0000204B
		public Vec3 ToVec3()
		{
			return new Vec3(this.Red, this.Green, this.Blue, this.Alpha);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E6C File Offset: 0x0000206C
		public static Color operator *(Color c, float f)
		{
			float red = c.Red * f;
			float green = c.Green * f;
			float blue = c.Blue * f;
			float alpha = c.Alpha * f;
			return new Color(red, green, blue, alpha);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003EA4 File Offset: 0x000020A4
		public static Color operator *(Color c1, Color c2)
		{
			return new Color(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue, c1.Alpha * c2.Alpha);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003EDF File Offset: 0x000020DF
		public static Color operator +(Color c1, Color c2)
		{
			return new Color(c1.Red + c2.Red, c1.Green + c2.Green, c1.Blue + c2.Blue, c1.Alpha + c2.Alpha);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003F1A File Offset: 0x0000211A
		public static Color operator -(Color c1, Color c2)
		{
			return new Color(c1.Red - c2.Red, c1.Green - c2.Green, c1.Blue - c2.Blue, c1.Alpha - c2.Alpha);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003F55 File Offset: 0x00002155
		public static Color Black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003F70 File Offset: 0x00002170
		public static Color White
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003F8B File Offset: 0x0000218B
		public static bool operator ==(Color a, Color b)
		{
			return a.Red == b.Red && a.Green == b.Green && a.Blue == b.Blue && a.Alpha == b.Alpha;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003FC7 File Offset: 0x000021C7
		public static bool operator !=(Color a, Color b)
		{
			return !(a == b);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003FD3 File Offset: 0x000021D3
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003FE8 File Offset: 0x000021E8
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is Color))
			{
				return false;
			}
			Color b = (Color)obj;
			return this == b;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004017 File Offset: 0x00002217
		public static Color FromVector3(Vector3 vector3)
		{
			return new Color(vector3.X, vector3.Y, vector3.Z, 1f);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004035 File Offset: 0x00002235
		public static Color FromVector3(Vec3 vector3)
		{
			return new Color(vector3.x, vector3.y, vector3.z, 1f);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004053 File Offset: 0x00002253
		public float Length()
		{
			return MathF.Sqrt(this.Red * this.Red + this.Green * this.Green + this.Blue * this.Blue + this.Alpha * this.Alpha);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004094 File Offset: 0x00002294
		public uint ToUnsignedInteger()
		{
			byte b = (byte)(this.Red * 255f);
			byte b2 = (byte)(this.Green * 255f);
			byte b3 = (byte)(this.Blue * 255f);
			return (uint)(((int)((byte)(this.Alpha * 255f)) << 24) + ((int)b << 16) + ((int)b2 << 8) + (int)b3);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000040E8 File Offset: 0x000022E8
		public static Color FromUint(uint color)
		{
			float num = (float)((byte)(color >> 24));
			byte b = (byte)(color >> 16);
			byte b2 = (byte)(color >> 8);
			byte b3 = (byte)color;
			float alpha = num * 0.003921569f;
			float red = (float)b * 0.003921569f;
			float green = (float)b2 * 0.003921569f;
			float blue = (float)b3 * 0.003921569f;
			return new Color(red, green, blue, alpha);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004138 File Offset: 0x00002338
		public static Color ConvertStringToColor(string color)
		{
			string s = color.Substring(1, 2);
			string s2 = color.Substring(3, 2);
			string s3 = color.Substring(5, 2);
			string s4 = color.Substring(7, 2);
			int num = int.Parse(s, NumberStyles.HexNumber);
			int num2 = int.Parse(s2, NumberStyles.HexNumber);
			int num3 = int.Parse(s3, NumberStyles.HexNumber);
			int num4 = int.Parse(s4, NumberStyles.HexNumber);
			return new Color((float)num * 0.003921569f, (float)num2 * 0.003921569f, (float)num3 * 0.003921569f, (float)num4 * 0.003921569f);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000041C4 File Offset: 0x000023C4
		public static Color Lerp(Color start, Color end, float ratio)
		{
			float red = start.Red * (1f - ratio) + end.Red * ratio;
			float green = start.Green * (1f - ratio) + end.Green * ratio;
			float blue = start.Blue * (1f - ratio) + end.Blue * ratio;
			float alpha = start.Alpha * (1f - ratio) + end.Alpha * ratio;
			return new Color(red, green, blue, alpha);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004238 File Offset: 0x00002438
		public override string ToString()
		{
			byte b = (byte)(this.Red * 255f);
			byte b2 = (byte)(this.Green * 255f);
			byte b3 = (byte)(this.Blue * 255f);
			byte b4 = (byte)(this.Alpha * 255f);
			return string.Concat(new string[]
			{
				"#",
				b.ToString("X2"),
				b2.ToString("X2"),
				b3.ToString("X2"),
				b4.ToString("X2")
			});
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000042CC File Offset: 0x000024CC
		public static string UIntToColorString(uint color)
		{
			string text = (color >> 24).ToString("X");
			if (text.Length == 1)
			{
				text = text.Insert(0, "0");
			}
			string text2 = (color >> 16).ToString("X");
			if (text2.Length == 1)
			{
				text2 = text2.Insert(0, "0");
			}
			text2 = text2.Substring(MathF.Max(0, text2.Length - 2));
			string text3 = (color >> 8).ToString("X");
			if (text3.Length == 1)
			{
				text3 = text3.Insert(0, "0");
			}
			text3 = text3.Substring(MathF.Max(0, text3.Length - 2));
			uint num = color;
			string text4 = num.ToString("X");
			if (text4.Length == 1)
			{
				text4 = text4.Insert(0, "0");
			}
			text4 = text4.Substring(MathF.Max(0, text4.Length - 2));
			return text2 + text3 + text4 + text;
		}

		// Token: 0x04000059 RID: 89
		public float Red;

		// Token: 0x0400005A RID: 90
		public float Green;

		// Token: 0x0400005B RID: 91
		public float Blue;

		// Token: 0x0400005C RID: 92
		public float Alpha;
	}
}
