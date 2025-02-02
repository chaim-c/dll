using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000323 RID: 803
	public class LightCycle : ScriptComponentBehavior
	{
		// Token: 0x06002B21 RID: 11041 RVA: 0x000A6F6C File Offset: 0x000A516C
		private void SetVisibility()
		{
			Light light = base.GameEntity.GetLight();
			float timeOfDay = base.Scene.TimeOfDay;
			this.visibility = (timeOfDay < 6f || timeOfDay > 20f || base.Scene.IsAtmosphereIndoor || this.alwaysBurn);
			if (light != null)
			{
				light.SetVisibility(this.visibility);
			}
			foreach (GameEntity gameEntity in base.GameEntity.GetChildren())
			{
				gameEntity.SetVisibilityExcludeParents(this.visibility);
			}
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000A701C File Offset: 0x000A521C
		protected internal override void OnInit()
		{
			base.OnInit();
			this.SetVisibility();
			if (!this.visibility)
			{
				List<GameEntity> list = new List<GameEntity>();
				base.GameEntity.GetChildrenRecursive(ref list);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					base.Scene.RemoveEntity(list[i], 0);
				}
				base.GameEntity.RemoveScriptComponent(base.ScriptComponent.Pointer, 0);
			}
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000A708D File Offset: 0x000A528D
		protected internal override void OnEditorTick(float dt)
		{
			this.SetVisibility();
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000A7095 File Offset: 0x000A5295
		protected internal override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x040010AF RID: 4271
		public bool alwaysBurn;

		// Token: 0x040010B0 RID: 4272
		private bool visibility;
	}
}
