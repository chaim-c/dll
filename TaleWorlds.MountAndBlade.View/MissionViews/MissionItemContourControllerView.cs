using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x0200004F RID: 79
	public class MissionItemContourControllerView : MissionView
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0001DF2C File Offset: 0x0001C12C
		private bool _isAllowedByOption
		{
			get
			{
				return !BannerlordConfig.HideBattleUI || GameNetwork.IsMultiplayer;
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public MissionItemContourControllerView()
		{
			this._contourItems = new List<GameEntity>();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001E048 File Offset: 0x0001C248
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this._isAllowedByOption)
			{
				if (Agent.Main != null && base.MissionScreen.InputManager.IsGameKeyDown(5))
				{
					this.RemoveContourFromAllItems();
					this.PopulateContourListWithNearbyItems();
					this.ApplyContourToAllItems();
					this._lastItemQueryTime = base.Mission.CurrentTime;
				}
				else
				{
					this.RemoveContourFromAllItems();
					this._contourItems.Clear();
				}
				if (this._isContourAppliedToAllItems)
				{
					float currentTime = base.Mission.CurrentTime;
					if (currentTime - this._lastItemQueryTime > this._sceneItemQueryFreq)
					{
						this.RemoveContourFromAllItems();
						this.PopulateContourListWithNearbyItems();
						this._lastItemQueryTime = currentTime;
					}
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001E0F0 File Offset: 0x0001C2F0
		public override void OnFocusGained(Agent agent, IFocusable focusableObject, bool isInteractable)
		{
			base.OnFocusGained(agent, focusableObject, isInteractable);
			if (this._isAllowedByOption && focusableObject != this._currentFocusedObject && isInteractable)
			{
				this._currentFocusedObject = focusableObject;
				UsableMissionObject usableMissionObject;
				if ((usableMissionObject = (focusableObject as UsableMissionObject)) != null)
				{
					SpawnedItemEntity spawnedItemEntity;
					if ((spawnedItemEntity = (usableMissionObject as SpawnedItemEntity)) != null)
					{
						this._focusedGameEntity = spawnedItemEntity.GameEntity;
					}
					else if (!string.IsNullOrEmpty(usableMissionObject.ActionMessage.ToString()) && !string.IsNullOrEmpty(usableMissionObject.DescriptionMessage.ToString()))
					{
						this._focusedGameEntity = usableMissionObject.GameEntity;
					}
					else
					{
						UsableMachine usableMachineFromPoint = this.GetUsableMachineFromPoint(usableMissionObject);
						if (usableMachineFromPoint != null)
						{
							this._focusedGameEntity = usableMachineFromPoint.GameEntity;
						}
					}
				}
				this.AddContourToFocusedItem();
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001E199 File Offset: 0x0001C399
		public override void OnFocusLost(Agent agent, IFocusable focusableObject)
		{
			base.OnFocusLost(agent, focusableObject);
			if (this._isAllowedByOption)
			{
				this.RemoveContourFromFocusedItem();
				this._currentFocusedObject = null;
				this._focusedGameEntity = null;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		private void PopulateContourListWithNearbyItems()
		{
			this._contourItems.Clear();
			float num = GameNetwork.IsSessionActive ? 1f : 3f;
			float num2 = Agent.Main.MaximumForwardUnlimitedSpeed * num;
			Vec3 vec = Agent.Main.Position - new Vec3(num2, num2, 1f, -1f);
			Vec3 vec2 = Agent.Main.Position + new Vec3(num2, num2, 1.8f, -1f);
			int num3 = base.Mission.Scene.SelectEntitiesInBoxWithScriptComponent<SpawnedItemEntity>(ref vec, ref vec2, this._tempPickableEntities, this._pickableItemsId);
			for (int i = 0; i < num3; i++)
			{
				SpawnedItemEntity firstScriptOfType = this._tempPickableEntities[i].GetFirstScriptOfType<SpawnedItemEntity>();
				if (firstScriptOfType != null)
				{
					if (firstScriptOfType.IsBanner())
					{
						if (MissionGameModels.Current.BattleBannerBearersModel.IsInteractableFormationBanner(firstScriptOfType, Agent.Main))
						{
							this._contourItems.Add(firstScriptOfType.GameEntity);
						}
					}
					else
					{
						this._contourItems.Add(firstScriptOfType.GameEntity);
					}
				}
			}
			int num4 = base.Mission.Scene.SelectEntitiesInBoxWithScriptComponent<UsableMachine>(ref vec, ref vec2, this._tempPickableEntities, this._pickableItemsId);
			for (int j = 0; j < num4; j++)
			{
				UsableMachine firstScriptOfType2 = this._tempPickableEntities[j].GetFirstScriptOfType<UsableMachine>();
				if (firstScriptOfType2 != null && !firstScriptOfType2.IsDisabled)
				{
					GameEntity validStandingPointForAgentWithoutDistanceCheck = firstScriptOfType2.GetValidStandingPointForAgentWithoutDistanceCheck(Agent.Main);
					if (validStandingPointForAgentWithoutDistanceCheck != null && !(validStandingPointForAgentWithoutDistanceCheck.GetFirstScriptOfType<UsableMissionObject>() is SpawnedItemEntity))
					{
						IFocusable focusable;
						if ((focusable = (validStandingPointForAgentWithoutDistanceCheck.GetScriptComponents().FirstOrDefault((ScriptComponentBehavior sc) => sc is IFocusable) as IFocusable)) != null && focusable is UsableMissionObject)
						{
							this._contourItems.Add(firstScriptOfType2.GameEntity);
						}
					}
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001E398 File Offset: 0x0001C598
		private void ApplyContourToAllItems()
		{
			if (!this._isContourAppliedToAllItems)
			{
				foreach (GameEntity gameEntity in this._contourItems)
				{
					uint nonFocusedColor = this.GetNonFocusedColor(gameEntity);
					uint value = (gameEntity == this._focusedGameEntity) ? this._focusedContourColor : nonFocusedColor;
					gameEntity.SetContourColor(new uint?(value), true);
				}
				this._isContourAppliedToAllItems = true;
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001E420 File Offset: 0x0001C620
		private uint GetNonFocusedColor(GameEntity entity)
		{
			SpawnedItemEntity firstScriptOfType = entity.GetFirstScriptOfType<SpawnedItemEntity>();
			ItemObject itemObject = (firstScriptOfType != null) ? firstScriptOfType.WeaponCopy.Item : null;
			WeaponComponentData weaponComponentData = (itemObject != null) ? itemObject.PrimaryWeapon : null;
			ItemObject.ItemTypeEnum? itemTypeEnum = (itemObject != null) ? new ItemObject.ItemTypeEnum?(itemObject.ItemType) : null;
			if (itemObject != null && itemObject.HasBannerComponent)
			{
				return this._nonFocusedBannerContourColor;
			}
			if (weaponComponentData == null || !weaponComponentData.IsAmmo)
			{
				ItemObject.ItemTypeEnum? itemTypeEnum2 = itemTypeEnum;
				ItemObject.ItemTypeEnum itemTypeEnum3 = ItemObject.ItemTypeEnum.Arrows;
				if (!(itemTypeEnum2.GetValueOrDefault() == itemTypeEnum3 & itemTypeEnum2 != null))
				{
					itemTypeEnum2 = itemTypeEnum;
					itemTypeEnum3 = ItemObject.ItemTypeEnum.Bolts;
					if (!(itemTypeEnum2.GetValueOrDefault() == itemTypeEnum3 & itemTypeEnum2 != null))
					{
						itemTypeEnum2 = itemTypeEnum;
						itemTypeEnum3 = ItemObject.ItemTypeEnum.Bullets;
						if (!(itemTypeEnum2.GetValueOrDefault() == itemTypeEnum3 & itemTypeEnum2 != null))
						{
							itemTypeEnum2 = itemTypeEnum;
							itemTypeEnum3 = ItemObject.ItemTypeEnum.Thrown;
							if (itemTypeEnum2.GetValueOrDefault() == itemTypeEnum3 & itemTypeEnum2 != null)
							{
								return this._nonFocusedThrowableContourColor;
							}
							return this._nonFocusedDefaultContourColor;
						}
					}
				}
			}
			return this._nonFocusedAmmoContourColor;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001E510 File Offset: 0x0001C710
		private void RemoveContourFromAllItems()
		{
			if (this._isContourAppliedToAllItems)
			{
				foreach (GameEntity gameEntity in this._contourItems)
				{
					if (this._focusedGameEntity == null || gameEntity != this._focusedGameEntity)
					{
						gameEntity.SetContourColor(null, true);
					}
				}
				this._isContourAppliedToAllItems = false;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001E598 File Offset: 0x0001C798
		private void AddContourToFocusedItem()
		{
			if (this._focusedGameEntity != null && !this._isContourAppliedToFocusedItem)
			{
				this._focusedGameEntity.SetContourColor(new uint?(this._focusedContourColor), true);
				this._isContourAppliedToFocusedItem = true;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001E5D0 File Offset: 0x0001C7D0
		private void RemoveContourFromFocusedItem()
		{
			if (this._focusedGameEntity != null && this._isContourAppliedToFocusedItem)
			{
				if (this._contourItems.Contains(this._focusedGameEntity))
				{
					this._focusedGameEntity.SetContourColor(new uint?(this._nonFocusedDefaultContourColor), true);
				}
				else
				{
					this._focusedGameEntity.SetContourColor(null, true);
				}
				this._isContourAppliedToFocusedItem = false;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001E63C File Offset: 0x0001C83C
		private UsableMachine GetUsableMachineFromPoint(UsableMissionObject standingPoint)
		{
			GameEntity gameEntity = standingPoint.GameEntity;
			while (gameEntity != null && !gameEntity.HasScriptOfType<UsableMachine>())
			{
				gameEntity = gameEntity.Parent;
			}
			if (gameEntity != null)
			{
				UsableMachine firstScriptOfType = gameEntity.GetFirstScriptOfType<UsableMachine>();
				if (firstScriptOfType != null)
				{
					return firstScriptOfType;
				}
			}
			return null;
		}

		// Token: 0x04000255 RID: 597
		private GameEntity[] _tempPickableEntities = new GameEntity[128];

		// Token: 0x04000256 RID: 598
		private UIntPtr[] _pickableItemsId = new UIntPtr[128];

		// Token: 0x04000257 RID: 599
		private List<GameEntity> _contourItems;

		// Token: 0x04000258 RID: 600
		private GameEntity _focusedGameEntity;

		// Token: 0x04000259 RID: 601
		private IFocusable _currentFocusedObject;

		// Token: 0x0400025A RID: 602
		private bool _isContourAppliedToAllItems;

		// Token: 0x0400025B RID: 603
		private bool _isContourAppliedToFocusedItem;

		// Token: 0x0400025C RID: 604
		private uint _nonFocusedDefaultContourColor = new Color(0.85f, 0.85f, 0.85f, 1f).ToUnsignedInteger();

		// Token: 0x0400025D RID: 605
		private uint _nonFocusedAmmoContourColor = new Color(0f, 0.73f, 1f, 1f).ToUnsignedInteger();

		// Token: 0x0400025E RID: 606
		private uint _nonFocusedThrowableContourColor = new Color(0.051f, 0.988f, 0.18f, 1f).ToUnsignedInteger();

		// Token: 0x0400025F RID: 607
		private uint _nonFocusedBannerContourColor = new Color(0.521f, 0.988f, 0.521f, 1f).ToUnsignedInteger();

		// Token: 0x04000260 RID: 608
		private uint _focusedContourColor = new Color(1f, 0.84f, 0.35f, 1f).ToUnsignedInteger();

		// Token: 0x04000261 RID: 609
		private float _lastItemQueryTime;

		// Token: 0x04000262 RID: 610
		private float _sceneItemQueryFreq = 1f;
	}
}
