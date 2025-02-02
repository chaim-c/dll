using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000017 RID: 23
	public sealed class EpicAccountId : Handle
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00004477 File Offset: 0x00002677
		public EpicAccountId()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00004481 File Offset: 0x00002681
		public EpicAccountId(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000448C File Offset: 0x0000268C
		public static EpicAccountId FromString(Utf8String accountIdString)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(accountIdString, ref zero);
			IntPtr from = Bindings.EOS_EpicAccountId_FromString(zero);
			Helper.Dispose(ref zero);
			EpicAccountId result;
			Helper.Get<EpicAccountId>(from, out result);
			return result;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000044C8 File Offset: 0x000026C8
		public static explicit operator EpicAccountId(Utf8String value)
		{
			return EpicAccountId.FromString(value);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000044E0 File Offset: 0x000026E0
		public bool IsValid()
		{
			int from = Bindings.EOS_EpicAccountId_IsValid(base.InnerHandle);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00004508 File Offset: 0x00002708
		public Result ToString(out Utf8String outBuffer)
		{
			int size = 33;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_EpicAccountId_ToString(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00004544 File Offset: 0x00002744
		public override string ToString()
		{
			Utf8String u8str;
			this.ToString(out u8str);
			return u8str;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00004568 File Offset: 0x00002768
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

		// Token: 0x060002D1 RID: 721 RVA: 0x00004598 File Offset: 0x00002798
		public static explicit operator Utf8String(EpicAccountId value)
		{
			Utf8String result = null;
			bool flag = value != null;
			if (flag)
			{
				value.ToString(out result);
			}
			return result;
		}

		// Token: 0x04000024 RID: 36
		public const int EpicaccountidMaxLength = 32;
	}
}
