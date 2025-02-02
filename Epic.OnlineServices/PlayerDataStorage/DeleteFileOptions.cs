using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000269 RID: 617
	public struct DeleteFileOptions
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00018FE6 File Offset: 0x000171E6
		// (set) Token: 0x060010DB RID: 4315 RVA: 0x00018FEE File Offset: 0x000171EE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00018FF7 File Offset: 0x000171F7
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x00018FFF File Offset: 0x000171FF
		public Utf8String Filename { get; set; }
	}
}
