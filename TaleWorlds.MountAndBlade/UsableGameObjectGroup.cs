using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000369 RID: 873
	public class UsableGameObjectGroup : ScriptComponentBehavior, IVisible
	{
		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000C4BC1 File Offset: 0x000C2DC1
		// (set) Token: 0x06002F90 RID: 12176 RVA: 0x000C4BCE File Offset: 0x000C2DCE
		public bool IsVisible
		{
			get
			{
				return base.GameEntity.IsVisibleIncludeParents();
			}
			set
			{
				base.GameEntity.SetVisibilityExcludeParents(value);
			}
		}
	}
}
