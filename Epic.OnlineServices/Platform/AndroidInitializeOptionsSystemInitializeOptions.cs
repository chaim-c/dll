using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000643 RID: 1603
	public struct AndroidInitializeOptionsSystemInitializeOptions
	{
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060028CD RID: 10445 RVA: 0x0003CBC6 File Offset: 0x0003ADC6
		// (set) Token: 0x060028CE RID: 10446 RVA: 0x0003CBCE File Offset: 0x0003ADCE
		public IntPtr Reserved { get; set; }

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060028CF RID: 10447 RVA: 0x0003CBD7 File Offset: 0x0003ADD7
		// (set) Token: 0x060028D0 RID: 10448 RVA: 0x0003CBDF File Offset: 0x0003ADDF
		public Utf8String OptionalInternalDirectory { get; set; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x0003CBE8 File Offset: 0x0003ADE8
		// (set) Token: 0x060028D2 RID: 10450 RVA: 0x0003CBF0 File Offset: 0x0003ADF0
		public Utf8String OptionalExternalDirectory { get; set; }

		// Token: 0x060028D3 RID: 10451 RVA: 0x0003CBF9 File Offset: 0x0003ADF9
		internal void Set(ref AndroidInitializeOptionsSystemInitializeOptionsInternal other)
		{
			this.Reserved = other.Reserved;
			this.OptionalInternalDirectory = other.OptionalInternalDirectory;
			this.OptionalExternalDirectory = other.OptionalExternalDirectory;
		}
	}
}
