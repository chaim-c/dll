using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008E RID: 142
	[EngineClass("rglTexture_view")]
	public sealed class TextureView : View
	{
		// Token: 0x06000ADE RID: 2782 RVA: 0x0000BED2 File Offset: 0x0000A0D2
		internal TextureView(UIntPtr meshPointer) : base(meshPointer)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000BEDB File Offset: 0x0000A0DB
		public static TextureView CreateTextureView()
		{
			return EngineApplicationInterface.ITextureView.CreateTextureView();
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000BEE7 File Offset: 0x0000A0E7
		public void SetTexture(Texture texture)
		{
			EngineApplicationInterface.ITextureView.SetTexture(base.Pointer, texture.Pointer);
		}
	}
}
