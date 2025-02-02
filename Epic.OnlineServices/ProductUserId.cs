using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000020 RID: 32
	public sealed class ProductUserId : Handle
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x00004832 File Offset: 0x00002A32
		public ProductUserId()
		{
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000483C File Offset: 0x00002A3C
		public ProductUserId(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00004848 File Offset: 0x00002A48
		public static ProductUserId FromString(Utf8String productUserIdString)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(productUserIdString, ref zero);
			IntPtr from = Bindings.EOS_ProductUserId_FromString(zero);
			Helper.Dispose(ref zero);
			ProductUserId result;
			Helper.Get<ProductUserId>(from, out result);
			return result;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00004884 File Offset: 0x00002A84
		public static explicit operator ProductUserId(Utf8String value)
		{
			return ProductUserId.FromString(value);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000489C File Offset: 0x00002A9C
		public bool IsValid()
		{
			int from = Bindings.EOS_ProductUserId_IsValid(base.InnerHandle);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000048C4 File Offset: 0x00002AC4
		public Result ToString(out Utf8String outBuffer)
		{
			int size = 33;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_ProductUserId_ToString(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00004900 File Offset: 0x00002B00
		public override string ToString()
		{
			Utf8String u8str;
			this.ToString(out u8str);
			return u8str;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00004924 File Offset: 0x00002B24
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

		// Token: 0x060002F9 RID: 761 RVA: 0x00004954 File Offset: 0x00002B54
		public static explicit operator Utf8String(ProductUserId value)
		{
			Utf8String result = null;
			bool flag = value != null;
			if (flag)
			{
				value.ToString(out result);
			}
			return result;
		}

		// Token: 0x04000057 RID: 87
		public const int ProductuseridMaxLength = 32;
	}
}
