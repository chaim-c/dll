using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000326 RID: 806
	public class MultiplayerSceneValidator : ScriptComponentBehavior
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x000A7636 File Offset: 0x000A5836
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			base.OnEditorVariableChanged(variableName);
			if (variableName == "SelectFaultyEntities")
			{
				this.SelectInvalidEntities();
			}
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x000A7654 File Offset: 0x000A5854
		protected internal override void OnSceneSave(string saveFolder)
		{
			base.OnSceneSave(saveFolder);
			foreach (GameEntity gameEntity in this.GetInvalidEntities())
			{
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000A76A8 File Offset: 0x000A58A8
		private List<GameEntity> GetInvalidEntities()
		{
			List<GameEntity> list = new List<GameEntity>();
			List<GameEntity> list2 = new List<GameEntity>();
			base.Scene.GetEntities(ref list2);
			foreach (GameEntity gameEntity in list2)
			{
				foreach (ScriptComponentBehavior scriptComponentBehavior in gameEntity.GetScriptComponents())
				{
					if (scriptComponentBehavior != null && (scriptComponentBehavior.GetType().IsSubclassOf(typeof(MissionObject)) || (scriptComponentBehavior.GetType() == typeof(MissionObject) && scriptComponentBehavior.IsOnlyVisual())))
					{
						list.Add(gameEntity);
						break;
					}
				}
			}
			return list;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x000A7790 File Offset: 0x000A5990
		private void SelectInvalidEntities()
		{
			base.GameEntity.DeselectEntityOnEditor();
			foreach (GameEntity gameEntity in this.GetInvalidEntities())
			{
				gameEntity.SelectEntityOnEditor();
			}
		}

		// Token: 0x040010C1 RID: 4289
		public SimpleButton SelectFaultyEntities;
	}
}
