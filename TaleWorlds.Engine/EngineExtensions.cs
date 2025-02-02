using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200009A RID: 154
	public static class EngineExtensions
	{
		// Token: 0x06000BBA RID: 3002 RVA: 0x0000D131 File Offset: 0x0000B331
		public static WorldPosition ToWorldPosition(this Vec3 vec3, Scene scene)
		{
			return new WorldPosition(scene, UIntPtr.Zero, vec3, false);
		}
	}
}
