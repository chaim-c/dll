using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000362 RID: 866
	public interface IOrderableWithInteractionArea : IOrderable
	{
		// Token: 0x06002F38 RID: 12088
		bool IsPointInsideInteractionArea(Vec3 point);
	}
}
