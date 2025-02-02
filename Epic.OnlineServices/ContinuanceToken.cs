using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000016 RID: 22
	public sealed class ContinuanceToken : Handle
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x000043A2 File Offset: 0x000025A2
		public ContinuanceToken()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000043AC File Offset: 0x000025AC
		public ContinuanceToken(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000043B8 File Offset: 0x000025B8
		public Result ToString(out Utf8String outBuffer)
		{
			int size = 1024;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_ContinuanceToken_ToString(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x000043F8 File Offset: 0x000025F8
		public override string ToString()
		{
			Utf8String u8str;
			this.ToString(out u8str);
			return u8str;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000441C File Offset: 0x0000261C
		public override string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = format != null;
			string result;
			if (flag)
			{
				result = string.Format(format, this.ToString());
			}
			else
			{
				result = this.ToString();
			}
			return result;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000444C File Offset: 0x0000264C
		public static explicit operator Utf8String(ContinuanceToken value)
		{
			Utf8String result = null;
			bool flag = value != null;
			if (flag)
			{
				value.ToString(out result);
			}
			return result;
		}
	}
}
