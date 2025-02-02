using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200004A RID: 74
	[EngineClass("rglEntity_component")]
	public abstract class GameEntityComponent : NativeObject
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00004AB1 File Offset: 0x00002CB1
		internal GameEntityComponent(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public GameEntity GetEntity()
		{
			return EngineApplicationInterface.IGameEntityComponent.GetEntity(this);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00004ACD File Offset: 0x00002CCD
		public virtual MetaMesh GetFirstMetaMesh()
		{
			return EngineApplicationInterface.IGameEntityComponent.GetFirstMetaMesh(this);
		}
	}
}
