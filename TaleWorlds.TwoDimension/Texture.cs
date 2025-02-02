using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000034 RID: 52
	public class Texture
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009422 File Offset: 0x00007622
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000942A File Offset: 0x0000762A
		public ITexture PlatformTexture { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600024B RID: 587 RVA: 0x00009433 File Offset: 0x00007633
		public int Width
		{
			get
			{
				return this.PlatformTexture.Width;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00009440 File Offset: 0x00007640
		public int Height
		{
			get
			{
				return this.PlatformTexture.Height;
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000944D File Offset: 0x0000764D
		public Texture(ITexture platformTexture)
		{
			this.PlatformTexture = platformTexture;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000945C File Offset: 0x0000765C
		public bool IsLoaded()
		{
			return this.PlatformTexture.IsLoaded();
		}
	}
}
