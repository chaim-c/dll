using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Objects.AreaMarkers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000082 RID: 130
	public class NotableSpawnPointHandler : MissionLogic
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x000227B4 File Offset: 0x000209B4
		public override void OnBehaviorInitialize()
		{
			List<GameEntity> list = Mission.Current.Scene.FindEntitiesWithTag("sp_notables_parent").ToList<GameEntity>();
			Settlement settlement = PlayerEncounter.LocationEncounter.Settlement;
			this._workshopAssignedHeroes = new List<Hero>();
			foreach (Hero hero in settlement.Notables)
			{
				if (hero.IsGangLeader)
				{
					this._gangLeaderNotableCount++;
				}
				else if (hero.IsPreacher)
				{
					this._preacherNotableCount++;
				}
				else if (hero.IsArtisan)
				{
					this._artisanNotableCount++;
				}
				else if (hero.IsRuralNotable || hero.IsHeadman)
				{
					this._ruralNotableCount++;
				}
				else if (hero.IsMerchant)
				{
					this._merchantNotableCount++;
				}
			}
			foreach (GameEntity gameEntity in list.ToList<GameEntity>())
			{
				foreach (GameEntity childGameEntity in gameEntity.GetChildren())
				{
					this.FindAndSetChild(childGameEntity);
				}
				foreach (WorkshopAreaMarker workshopAreaMarker in (from x in base.Mission.ActiveMissionObjects.FindAllWithType<WorkshopAreaMarker>().ToList<WorkshopAreaMarker>()
				orderby x.AreaIndex
				select x).ToList<WorkshopAreaMarker>())
				{
					if (workshopAreaMarker.IsPositionInRange(gameEntity.GlobalPosition))
					{
						if (workshopAreaMarker.GetWorkshop().Owner.OwnedWorkshops.First((Workshop x) => !x.WorkshopType.IsHidden).Tag == workshopAreaMarker.Tag)
						{
							this.ActivateParentSetInsideWorkshop(workshopAreaMarker);
							list.Remove(gameEntity);
							break;
						}
					}
				}
			}
			foreach (GameEntity gameEntity2 in list)
			{
				foreach (GameEntity childGameEntity2 in gameEntity2.GetChildren())
				{
					this.FindAndSetChild(childGameEntity2);
				}
				this.ActivateParentSetOutsideWorkshop();
			}
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00022AE0 File Offset: 0x00020CE0
		private void FindAndSetChild(GameEntity childGameEntity)
		{
			if (childGameEntity.HasTag("merchant_notary_talking_set"))
			{
				this._currentMerchantSetGameEntity = childGameEntity;
				return;
			}
			if (childGameEntity.HasTag("preacher_notary_talking_set"))
			{
				this._currentPreacherSetGameEntity = childGameEntity;
				return;
			}
			if (childGameEntity.HasTag("gangleader_sitting_and_talking_with_guards_set"))
			{
				this._currentGangLeaderSetGameEntity = childGameEntity;
				return;
			}
			if (childGameEntity.HasTag("sp_artisan_notary_talking_set"))
			{
				this._currentArtisanSetGameEntity = childGameEntity;
				return;
			}
			if (childGameEntity.HasTag("sp_ruralnotable_notary_talking_set"))
			{
				this._currentRuralNotableSetGameEntity = childGameEntity;
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00022B58 File Offset: 0x00020D58
		private void ActivateParentSetInsideWorkshop(WorkshopAreaMarker areaMarker)
		{
			Hero owner = areaMarker.GetWorkshop().Owner;
			if (!this._workshopAssignedHeroes.Contains(owner))
			{
				this._workshopAssignedHeroes.Add(owner);
				if (owner.IsMerchant)
				{
					this.DeactivateAllExcept(this._currentMerchantSetGameEntity);
					this._merchantNotableCount--;
					return;
				}
				if (owner.IsArtisan)
				{
					this.DeactivateAllExcept(this._currentArtisanSetGameEntity);
					this._artisanNotableCount--;
					return;
				}
				if (owner.IsGangLeader)
				{
					this.DeactivateAllExcept(this._currentGangLeaderSetGameEntity);
					this._gangLeaderNotableCount--;
					return;
				}
				if (owner.IsPreacher)
				{
					this.DeactivateAllExcept(this._currentPreacherSetGameEntity);
					this._preacherNotableCount--;
					return;
				}
				if (owner.IsRuralNotable)
				{
					this.DeactivateAllExcept(this._currentRuralNotableSetGameEntity);
					this._ruralNotableCount--;
					return;
				}
			}
			else
			{
				this.DeactivateAll();
			}
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00022C44 File Offset: 0x00020E44
		private void ActivateParentSetOutsideWorkshop()
		{
			if (this._gangLeaderNotableCount > 0)
			{
				this.DeactivateAllExcept(this._currentGangLeaderSetGameEntity);
				this._gangLeaderNotableCount--;
				return;
			}
			if (this._merchantNotableCount > 0)
			{
				this.DeactivateAllExcept(this._currentMerchantSetGameEntity);
				this._merchantNotableCount--;
				return;
			}
			if (this._preacherNotableCount > 0)
			{
				this.DeactivateAllExcept(this._currentPreacherSetGameEntity);
				this._preacherNotableCount--;
				return;
			}
			if (this._artisanNotableCount > 0)
			{
				this.DeactivateAllExcept(this._currentArtisanSetGameEntity);
				this._artisanNotableCount--;
				return;
			}
			if (this._ruralNotableCount > 0)
			{
				this.DeactivateAllExcept(this._currentRuralNotableSetGameEntity);
				this._ruralNotableCount--;
				return;
			}
			this.DeactivateAll();
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00022D0B File Offset: 0x00020F0B
		private void DeactivateAll()
		{
			this.MakeInvisibleAndDeactivate(this._currentGangLeaderSetGameEntity);
			this.MakeInvisibleAndDeactivate(this._currentMerchantSetGameEntity);
			this.MakeInvisibleAndDeactivate(this._currentPreacherSetGameEntity);
			this.MakeInvisibleAndDeactivate(this._currentArtisanSetGameEntity);
			this.MakeInvisibleAndDeactivate(this._currentRuralNotableSetGameEntity);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00022D4C File Offset: 0x00020F4C
		private void DeactivateAllExcept(GameEntity gameEntity)
		{
			if (gameEntity != this._currentMerchantSetGameEntity)
			{
				this.MakeInvisibleAndDeactivate(this._currentMerchantSetGameEntity);
			}
			if (gameEntity != this._currentGangLeaderSetGameEntity)
			{
				this.MakeInvisibleAndDeactivate(this._currentGangLeaderSetGameEntity);
			}
			if (gameEntity != this._currentPreacherSetGameEntity)
			{
				this.MakeInvisibleAndDeactivate(this._currentPreacherSetGameEntity);
			}
			if (gameEntity != this._currentArtisanSetGameEntity)
			{
				this.MakeInvisibleAndDeactivate(this._currentArtisanSetGameEntity);
			}
			if (gameEntity != this._currentRuralNotableSetGameEntity)
			{
				this.MakeInvisibleAndDeactivate(this._currentRuralNotableSetGameEntity);
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00022DDC File Offset: 0x00020FDC
		private void MakeInvisibleAndDeactivate(GameEntity gameEntity)
		{
			gameEntity.SetVisibilityExcludeParents(false);
			UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
			if (firstScriptOfType != null)
			{
				firstScriptOfType.Deactivate();
			}
			foreach (GameEntity gameEntity2 in gameEntity.GetChildren())
			{
				this.MakeInvisibleAndDeactivate(gameEntity2);
			}
		}

		// Token: 0x04000268 RID: 616
		private int _merchantNotableCount;

		// Token: 0x04000269 RID: 617
		private int _gangLeaderNotableCount;

		// Token: 0x0400026A RID: 618
		private int _preacherNotableCount;

		// Token: 0x0400026B RID: 619
		private int _artisanNotableCount;

		// Token: 0x0400026C RID: 620
		private int _ruralNotableCount;

		// Token: 0x0400026D RID: 621
		private GameEntity _currentMerchantSetGameEntity;

		// Token: 0x0400026E RID: 622
		private GameEntity _currentPreacherSetGameEntity;

		// Token: 0x0400026F RID: 623
		private GameEntity _currentGangLeaderSetGameEntity;

		// Token: 0x04000270 RID: 624
		private GameEntity _currentArtisanSetGameEntity;

		// Token: 0x04000271 RID: 625
		private GameEntity _currentRuralNotableSetGameEntity;

		// Token: 0x04000272 RID: 626
		private List<Hero> _workshopAssignedHeroes;
	}
}
