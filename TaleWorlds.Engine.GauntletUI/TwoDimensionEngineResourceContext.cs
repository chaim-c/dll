using System;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.Engine.GauntletUI
{
	// Token: 0x02000006 RID: 6
	public class TwoDimensionEngineResourceContext : ITwoDimensionResourceContext
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003044 File Offset: 0x00001244
		Texture ITwoDimensionResourceContext.LoadTexture(ResourceDepot resourceDepot, string name)
		{
			string[] array = name.Split(new char[]
			{
				'\\'
			});
			Texture fromResource = Texture.GetFromResource(array[array.Length - 1]);
			fromResource.SetTextureAsAlwaysValid();
			bool blocking = true;
			fromResource.PreloadTexture(blocking);
			return new Texture(new EngineTexture(fromResource));
		}
	}
}
