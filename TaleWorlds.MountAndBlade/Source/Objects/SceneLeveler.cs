using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade.Source.Objects
{
	// Token: 0x020003AB RID: 939
	public class SceneLeveler : ScriptComponentBehavior
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x000D172C File Offset: 0x000CF92C
		protected internal override void OnEditorVariableChanged(string variableName)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(variableName);
			if (num <= 90459893U)
			{
				if (num != 40127036U)
				{
					if (num != 73682274U)
					{
						if (num != 90459893U)
						{
							return;
						}
						if (!(variableName == "CreateLevel2"))
						{
							return;
						}
						this.OnLevelizeButtonPressed(2);
						return;
					}
					else
					{
						if (!(variableName == "CreateLevel3"))
						{
							return;
						}
						this.OnLevelizeButtonPressed(3);
						return;
					}
				}
				else
				{
					if (!(variableName == "CreateLevel1"))
					{
						return;
					}
					this.OnLevelizeButtonPressed(1);
					return;
				}
			}
			else if (num <= 1310461563U)
			{
				if (num != 804927328U)
				{
					if (num != 1310461563U)
					{
						return;
					}
					if (!(variableName == "DeleteLevel1"))
					{
						return;
					}
					this.OnDeleteButtonPressed(1);
					return;
				}
				else
				{
					if (!(variableName == "SelectEntitiesWithoutLevel"))
					{
						return;
					}
					this.OnSelectEntitiesWithoutLevelButtonPressed();
					return;
				}
			}
			else if (num != 1327239182U)
			{
				if (num != 1344016801U)
				{
					return;
				}
				if (!(variableName == "DeleteLevel3"))
				{
					return;
				}
				this.OnDeleteButtonPressed(3);
				return;
			}
			else
			{
				if (!(variableName == "DeleteLevel2"))
				{
					return;
				}
				this.OnDeleteButtonPressed(2);
				return;
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000D1824 File Offset: 0x000CFA24
		private void OnLevelizeButtonPressed(int level)
		{
			if (this.SourceSelectionSetName.IsEmpty<char>())
			{
				MessageManager.DisplayMessage("ApplyToSelectionSet is empty!");
				return;
			}
			if (this.TargetSelectionSetName.IsEmpty<char>())
			{
				MessageManager.DisplayMessage("NewSelectionSetName is empty!");
				return;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			GameEntity.UpgradeLevelMask levelMask = this.GetLevelMask(level);
			List<GameEntity> list = this.CollectEntitiesWithLevel();
			List<GameEntity> list2 = new List<GameEntity>();
			foreach (GameEntity gameEntity in list)
			{
				string text = this.FindPossiblePrefabName(gameEntity);
				if (text.IsEmpty<char>())
				{
					num++;
				}
				else
				{
					GameEntity.UpgradeLevelMask upgradeLevelMask = gameEntity.GetUpgradeLevelMask();
					if ((upgradeLevelMask & levelMask) != GameEntity.UpgradeLevelMask.None)
					{
						num2++;
						list2.Add(gameEntity);
					}
					else
					{
						string prefabName = this.ConvertPrefabName(text, levelMask);
						GameEntity gameEntity2 = GameEntity.Instantiate(base.Scene, prefabName, gameEntity.GetGlobalFrame());
						if (gameEntity2 == null)
						{
							num3++;
						}
						else
						{
							num4++;
							GameEntity.UpgradeLevelMask upgradeLevelMask2 = upgradeLevelMask & ~GameEntity.UpgradeLevelMask.Level1 & ~GameEntity.UpgradeLevelMask.Level2 & ~GameEntity.UpgradeLevelMask.Level3;
							upgradeLevelMask2 |= levelMask;
							gameEntity2.SetUpgradeLevelMask(upgradeLevelMask2);
							this.CopyScriptParameters(gameEntity2, gameEntity);
							list2.Add(gameEntity2);
						}
					}
				}
			}
			Debug.Print(string.Concat(new object[]
			{
				"Created Entities : ",
				num4,
				"\nAlready Visible In Desired Level : ",
				num2,
				"\nWithout Prefab For Level : ",
				num3,
				"\nWithout Prefab Info : ",
				num
			}), 0, Debug.DebugColor.Magenta, 17592186044416UL);
			Utilities.CreateSelectionInEditor(list2, this.TargetSelectionSetName);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000D19D0 File Offset: 0x000CFBD0
		private void CopyScriptParameters(GameEntity entity, GameEntity copyFromEntity)
		{
			if (copyFromEntity.HasScriptComponent("WallSegment") && !entity.HasScriptComponent("WallSegment"))
			{
				entity.CopyScriptComponentFromAnotherEntity(copyFromEntity, "WallSegment");
			}
			if (copyFromEntity.HasScriptComponent("mesh_bender") && !entity.HasScriptComponent("mesh_bender"))
			{
				entity.CopyScriptComponentFromAnotherEntity(copyFromEntity, "mesh_bender");
			}
			int num = 0;
			while (num < entity.ChildCount && num < copyFromEntity.ChildCount)
			{
				this.CopyScriptParameters(entity.GetChild(num), copyFromEntity.GetChild(num));
				num++;
			}
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000D1A57 File Offset: 0x000CFC57
		private GameEntity.UpgradeLevelMask GetLevelMask(int level)
		{
			if (level == 1)
			{
				return GameEntity.UpgradeLevelMask.Level1;
			}
			if (level != 2)
			{
				return GameEntity.UpgradeLevelMask.Level3;
			}
			return GameEntity.UpgradeLevelMask.Level2;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000D1A66 File Offset: 0x000CFC66
		private string GetLevelSubString(GameEntity.UpgradeLevelMask levelMask)
		{
			if (levelMask == GameEntity.UpgradeLevelMask.Level1)
			{
				return "_l1";
			}
			if (levelMask == GameEntity.UpgradeLevelMask.Level2)
			{
				return "_l2";
			}
			if (levelMask != GameEntity.UpgradeLevelMask.Level3)
			{
				return "";
			}
			return "_l3";
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000D1A90 File Offset: 0x000CFC90
		private string ConvertPrefabName(string prefabName, GameEntity.UpgradeLevelMask newLevelMask)
		{
			string text = prefabName;
			string levelSubString = this.GetLevelSubString(newLevelMask);
			if (newLevelMask != GameEntity.UpgradeLevelMask.Level1)
			{
				text = text.Replace(this.GetLevelSubString(GameEntity.UpgradeLevelMask.Level1), levelSubString);
			}
			if (newLevelMask != GameEntity.UpgradeLevelMask.Level2)
			{
				text = text.Replace(this.GetLevelSubString(GameEntity.UpgradeLevelMask.Level2), levelSubString);
			}
			if (newLevelMask != GameEntity.UpgradeLevelMask.Level3)
			{
				text = text.Replace(this.GetLevelSubString(GameEntity.UpgradeLevelMask.Level3), levelSubString);
			}
			if (text.Equals(prefabName))
			{
				return "";
			}
			return text;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000D1AF0 File Offset: 0x000CFCF0
		private string FindPossiblePrefabName(GameEntity gameEntity)
		{
			string prefabName = gameEntity.GetPrefabName();
			if (prefabName.IsEmpty<char>())
			{
				return gameEntity.GetOldPrefabName();
			}
			return prefabName;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000D1B14 File Offset: 0x000CFD14
		private void OnDeleteButtonPressed(int level)
		{
			if (this.SourceSelectionSetName.IsEmpty<char>())
			{
				MessageManager.DisplayMessage("ApplyToSelectionSet is empty!");
				return;
			}
			List<GameEntity> list = this.CollectEntitiesWithLevel();
			GameEntity.UpgradeLevelMask levelMask = this.GetLevelMask(level);
			List<GameEntity> list2 = new List<GameEntity>();
			int num = 0;
			int num2 = 0;
			foreach (GameEntity gameEntity in list)
			{
				GameEntity.UpgradeLevelMask upgradeLevelMask = gameEntity.GetUpgradeLevelMask();
				if (upgradeLevelMask == levelMask)
				{
					list2.Add(gameEntity);
					num++;
				}
				else if ((upgradeLevelMask & levelMask) != GameEntity.UpgradeLevelMask.None)
				{
					gameEntity.SetUpgradeLevelMask(upgradeLevelMask & ~levelMask);
					num2++;
				}
			}
			Utilities.DeleteEntitiesInEditorScene(list2);
			TextObject textObject = new TextObject("{=!}Deleted entity count : {DELETED_ENTRY_COUNT}", null);
			TextObject textObject2 = new TextObject("{=!}Removed level mask count : {REMOVED_LEVEL_MASK}", null);
			textObject.SetTextVariable("DELETED_ENTRY_COUNT", num);
			textObject2.SetTextVariable("REMOVED_LEVEL_MASK", num2);
			MessageManager.DisplayMessage(textObject.ToString());
			MessageManager.DisplayMessage(textObject2.ToString());
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000D1C10 File Offset: 0x000CFE10
		private void OnSelectEntitiesWithoutLevelButtonPressed()
		{
			List<GameEntity> list = new List<GameEntity>();
			base.Scene.GetEntities(ref list);
			List<GameEntity> list2 = list.FindAll((GameEntity x) => x.GetUpgradeLevelMask() == GameEntity.UpgradeLevelMask.None);
			TextObject textObject = new TextObject("{=!}Selected entity count : {SELECTED_ENTITIES}", null);
			textObject.SetTextVariable("SELECTED_ENTITIES", list2.Count);
			MessageManager.DisplayMessage(textObject.ToString());
			if (list2.Count > 0)
			{
				Utilities.SelectEntities(list2);
			}
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x000D1C8C File Offset: 0x000CFE8C
		private List<GameEntity> CollectEntitiesWithLevel()
		{
			List<GameEntity> list = new List<GameEntity>();
			Utilities.GetEntitiesOfSelectionSet(this.SourceSelectionSetName, ref list);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if ((list[i].GetUpgradeLevelMask() & (GameEntity.UpgradeLevelMask.Level1 | GameEntity.UpgradeLevelMask.Level2 | GameEntity.UpgradeLevelMask.Level3)) == GameEntity.UpgradeLevelMask.None)
				{
					list.RemoveAt(i);
				}
			}
			return list;
		}

		// Token: 0x040015DC RID: 5596
		public string SourceSelectionSetName = "";

		// Token: 0x040015DD RID: 5597
		public string TargetSelectionSetName = "";

		// Token: 0x040015DE RID: 5598
		public SimpleButton CreateLevel1;

		// Token: 0x040015DF RID: 5599
		public SimpleButton CreateLevel2;

		// Token: 0x040015E0 RID: 5600
		public SimpleButton CreateLevel3;

		// Token: 0x040015E1 RID: 5601
		public SimpleButton DeleteLevel1;

		// Token: 0x040015E2 RID: 5602
		public SimpleButton DeleteLevel2;

		// Token: 0x040015E3 RID: 5603
		public SimpleButton DeleteLevel3;

		// Token: 0x040015E4 RID: 5604
		public SimpleButton SelectEntitiesWithoutLevel;
	}
}
