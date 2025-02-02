using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000031 RID: 49
	[ApplicationInterfaceBase]
	internal interface ITextureView
	{
		// Token: 0x0600044D RID: 1101
		[EngineMethod("create_texture_view", false)]
		TextureView CreateTextureView();

		// Token: 0x0600044E RID: 1102
		[EngineMethod("set_texture", false)]
		void SetTexture(UIntPtr pointer, UIntPtr texture_ptr);
	}
}
