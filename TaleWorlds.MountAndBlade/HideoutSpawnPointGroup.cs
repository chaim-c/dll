using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000321 RID: 801
	public class HideoutSpawnPointGroup : SynchedMissionObject
	{
		// Token: 0x06002B1C RID: 11036 RVA: 0x000A6DE4 File Offset: 0x000A4FE4
		protected internal override void OnInit()
		{
			base.OnInit();
			this._spawnPoints = new GameEntity[4];
			string spawnPointTagAffix = this.Side.ToString().ToLower() + "_";
			string[] array = new string[4];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = spawnPointTagAffix + ((FormationClass)i).GetName().ToLower();
			}
			IEnumerable<GameEntity> children = base.GameEntity.GetChildren();
			Func<GameEntity, bool> <>9__0;
			Func<GameEntity, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				Func<string, bool> <>9__1;
				predicate = (<>9__0 = delegate(GameEntity ce)
				{
					IEnumerable<string> tags = ce.Tags;
					Func<string, bool> predicate2;
					if ((predicate2 = <>9__1) == null)
					{
						predicate2 = (<>9__1 = ((string t) => t.StartsWith(spawnPointTagAffix)));
					}
					return tags.Any(predicate2);
				});
			}
			foreach (GameEntity gameEntity in children.Where(predicate))
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (gameEntity.HasTag(array[j]))
					{
						this._spawnPoints[j] = gameEntity;
						break;
					}
				}
			}
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000A6EF0 File Offset: 0x000A50F0
		public MatrixFrame[] GetSpawnPointFrames()
		{
			MatrixFrame[] array = new MatrixFrame[this._spawnPoints.Length];
			for (int i = 0; i < this._spawnPoints.Length; i++)
			{
				array[i] = ((this._spawnPoints[i] != null) ? this._spawnPoints[i].GetGlobalFrame() : MatrixFrame.Identity);
			}
			return array;
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x000A6F4A File Offset: 0x000A514A
		public void RemoveWithAllChildren()
		{
			base.GameEntity.RemoveAllChildren();
			base.GameEntity.Remove(83);
		}

		// Token: 0x040010AB RID: 4267
		private const int NumberOfDefaultFormations = 4;

		// Token: 0x040010AC RID: 4268
		public BattleSideEnum Side;

		// Token: 0x040010AD RID: 4269
		public int PhaseNumber;

		// Token: 0x040010AE RID: 4270
		private GameEntity[] _spawnPoints;
	}
}
