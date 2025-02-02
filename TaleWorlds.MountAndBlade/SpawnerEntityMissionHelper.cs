using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000345 RID: 837
	public class SpawnerEntityMissionHelper
	{
		// Token: 0x06002DE7 RID: 11751 RVA: 0x000BB0BC File Offset: 0x000B92BC
		public SpawnerEntityMissionHelper(SpawnerBase spawner, bool fireVersion = false)
		{
			this._spawner = spawner;
			this._fireVersion = fireVersion;
			this._ownerEntity = this._spawner.GameEntity;
			this._gameEntityName = this._ownerEntity.Name;
			if (this.SpawnPrefab(this._ownerEntity, this.GetPrefabName()) != null)
			{
				this.SyncMatrixFrames();
			}
			else
			{
				Debug.FailedAssert("Spawner couldn't spawn a proper entity.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Objects\\Siege\\SpawnerEntityMissionHelper.cs", ".ctor", 34);
			}
			this._spawner.AssignParameters(this);
			this.CallSetSpawnedFromSpawnerOfScripts();
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x000BB14C File Offset: 0x000B934C
		private GameEntity SpawnPrefab(GameEntity parent, string entityName)
		{
			this.SpawnedEntity = GameEntity.Instantiate(parent.Scene, entityName, false);
			this.SpawnedEntity.SetMobility(GameEntity.Mobility.dynamic);
			this.SpawnedEntity.EntityFlags = (this.SpawnedEntity.EntityFlags | EntityFlags.DontSaveToScene);
			parent.AddChild(this.SpawnedEntity, false);
			MatrixFrame identity = MatrixFrame.Identity;
			this.SpawnedEntity.SetFrame(ref identity);
			foreach (string tag in this._ownerEntity.Tags)
			{
				this.SpawnedEntity.AddTag(tag);
			}
			return this.SpawnedEntity;
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x000BB1E4 File Offset: 0x000B93E4
		private void RemoveChildEntity(GameEntity child)
		{
			child.CallScriptCallbacks();
			child.Remove(85);
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000BB1F4 File Offset: 0x000B93F4
		private void SyncMatrixFrames()
		{
			List<GameEntity> list = new List<GameEntity>();
			this.SpawnedEntity.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				if (SpawnerEntityMissionHelper.HasField(this._spawner, gameEntity.Name))
				{
					MatrixFrame matrixFrame = (MatrixFrame)SpawnerEntityMissionHelper.GetFieldValue(this._spawner, gameEntity.Name);
					gameEntity.SetFrame(ref matrixFrame);
				}
				if (SpawnerEntityMissionHelper.HasField(this._spawner, gameEntity.Name + "_enabled") && !(bool)SpawnerEntityMissionHelper.GetFieldValue(this._spawner, gameEntity.Name + "_enabled"))
				{
					this.RemoveChildEntity(gameEntity);
				}
			}
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000BB2CC File Offset: 0x000B94CC
		private void CallSetSpawnedFromSpawnerOfScripts()
		{
			foreach (GameEntity gameEntity in this.SpawnedEntity.GetEntityAndChildren())
			{
				foreach (ScriptComponentBehavior scriptComponentBehavior in from x in gameEntity.GetScriptComponents()
				where x is ISpawnable
				select x)
				{
					(scriptComponentBehavior as ISpawnable).SetSpawnedFromSpawner();
				}
			}
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000BB378 File Offset: 0x000B9578
		private string GetPrefabName()
		{
			string text;
			if (this._spawner.ToBeSpawnedOverrideName != "")
			{
				text = this._spawner.ToBeSpawnedOverrideName;
			}
			else
			{
				text = this._gameEntityName;
				text = text.Remove(this._gameEntityName.Length - this._gameEntityName.Split(new char[]
				{
					'_'
				}).Last<string>().Length - 1);
			}
			if (this._fireVersion)
			{
				if (this._spawner.ToBeSpawnedOverrideNameForFireVersion != "")
				{
					text = this._spawner.ToBeSpawnedOverrideNameForFireVersion;
				}
				else
				{
					text += "_fire";
				}
			}
			return text;
		}

		// Token: 0x06002DED RID: 11757 RVA: 0x000BB420 File Offset: 0x000B9620
		private static object GetFieldValue(object src, string propName)
		{
			return src.GetType().GetField(propName).GetValue(src);
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x000BB434 File Offset: 0x000B9634
		private static bool HasField(object obj, string propertyName)
		{
			return obj.GetType().GetField(propertyName) != null;
		}

		// Token: 0x0400131F RID: 4895
		private const string EnabledSuffix = "_enabled";

		// Token: 0x04001320 RID: 4896
		public GameEntity SpawnedEntity;

		// Token: 0x04001321 RID: 4897
		private GameEntity _ownerEntity;

		// Token: 0x04001322 RID: 4898
		private SpawnerBase _spawner;

		// Token: 0x04001323 RID: 4899
		private string _gameEntityName;

		// Token: 0x04001324 RID: 4900
		private bool _fireVersion;
	}
}
