using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000027 RID: 39
	public class ThumbnailCacheNode
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public ThumbnailCacheNode()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		public ThumbnailCacheNode(string key, Texture value, int frameNo)
		{
			this.Key = key;
			this.Value = value;
			this.FrameNo = frameNo;
			this.ReferenceCount = 0;
		}

		// Token: 0x04000125 RID: 293
		public string Key;

		// Token: 0x04000126 RID: 294
		public Texture Value;

		// Token: 0x04000127 RID: 295
		public int FrameNo;

		// Token: 0x04000128 RID: 296
		public int ReferenceCount;
	}
}
