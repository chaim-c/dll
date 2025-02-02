using System;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000065 RID: 101
	internal class SpriteFromTexture : Sprite
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001C2DA File Offset: 0x0001A4DA
		public override Texture Texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001C2E2 File Offset: 0x0001A4E2
		public SpriteFromTexture(Texture texture, int width, int height) : base("Sprite", width, height)
		{
			this._texture = texture;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0001C2F8 File Offset: 0x0001A4F8
		public override float GetScaleToUse(float width, float height, float scale)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001C2FF File Offset: 0x0001A4FF
		protected override DrawObject2D GetArrays(SpriteDrawData spriteDrawData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000307 RID: 775
		private Texture _texture;
	}
}
