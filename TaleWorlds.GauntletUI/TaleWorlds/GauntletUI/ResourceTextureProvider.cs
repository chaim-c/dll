using System;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x0200002B RID: 43
	public class ResourceTextureProvider : TextureProvider
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000E4E9 File Offset: 0x0000C6E9
		public override Texture GetTexture(TwoDimensionContext twoDimensionContext, string name)
		{
			return twoDimensionContext.LoadTexture(name);
		}
	}
}
