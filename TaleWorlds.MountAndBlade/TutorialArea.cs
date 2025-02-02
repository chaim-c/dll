using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000358 RID: 856
	public class TutorialArea : MissionObject
	{
		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06002EED RID: 12013 RVA: 0x000BFECC File Offset: 0x000BE0CC
		public MBReadOnlyList<TrainingIcon> TrainingIconsReadOnly
		{
			get
			{
				return this._trainingIcons;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000BFED4 File Offset: 0x000BE0D4
		// (set) Token: 0x06002EEF RID: 12015 RVA: 0x000BFEDC File Offset: 0x000BE0DC
		public TutorialArea.TrainingType TypeOfTraining
		{
			get
			{
				return this._typeOfTraining;
			}
			private set
			{
				this._typeOfTraining = value;
			}
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000BFEE5 File Offset: 0x000BE0E5
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this.GatherWeapons();
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000BFEF4 File Offset: 0x000BE0F4
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (MBEditor.IsEntitySelected(base.GameEntity))
			{
				uint value = 4294901760U;
				using (List<TutorialArea.TutorialEntity>.Enumerator enumerator = this._tagWeapon.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TutorialArea.TutorialEntity tutorialEntity = enumerator.Current;
						foreach (Tuple<GameEntity, MatrixFrame> tuple in tutorialEntity.EntityList)
						{
							tuple.Item1.SetContourColor(new uint?(value), true);
							this._highlightedEntities.Add(tuple.Item1);
						}
					}
					return;
				}
			}
			foreach (GameEntity gameEntity in this._highlightedEntities)
			{
				gameEntity.SetContourColor(null, true);
			}
			this._highlightedEntities.Clear();
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000C0010 File Offset: 0x000BE210
		protected internal override void OnInit()
		{
			base.OnInit();
			List<GameEntity> list = new List<GameEntity>();
			base.GameEntity.Scene.GetEntities(ref list);
			foreach (GameEntity gameEntity in list)
			{
				string[] tags = gameEntity.Tags;
				for (int i = 0; i < tags.Length; i++)
				{
					if (tags[i].StartsWith(this._tagPrefix) && gameEntity.HasScriptOfType<WeaponSpawner>())
					{
						gameEntity.GetFirstScriptOfType<WeaponSpawner>().SpawnWeapon();
						break;
					}
				}
			}
			this.GatherWeapons();
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000C00BC File Offset: 0x000BE2BC
		public override void AfterMissionStart()
		{
			this.DeactivateAllWeapons(true);
			this.MarkTrainingIcons(false);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000C00CC File Offset: 0x000BE2CC
		private void GatherWeapons()
		{
			List<GameEntity> list = new List<GameEntity>();
			base.GameEntity.Scene.GetEntities(ref list);
			foreach (GameEntity gameEntity in list)
			{
				foreach (string text in gameEntity.Tags)
				{
					TrainingIcon firstScriptOfType = gameEntity.GetFirstScriptOfType<TrainingIcon>();
					if (firstScriptOfType != null)
					{
						if (firstScriptOfType.GetTrainingSubTypeTag().StartsWith(this._tagPrefix))
						{
							this._trainingIcons.Add(firstScriptOfType);
						}
					}
					else if (text == this._tagPrefix + "boundary")
					{
						this.AddBoundary(gameEntity);
					}
					else if (text.StartsWith(this._tagPrefix))
					{
						this.AddTaggedWeapon(gameEntity, text);
					}
				}
			}
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000C01BC File Offset: 0x000BE3BC
		public void MarkTrainingIcons(bool mark)
		{
			foreach (TrainingIcon trainingIcon in this._trainingIcons)
			{
				trainingIcon.SetMarked(mark);
			}
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000C0210 File Offset: 0x000BE410
		public TrainingIcon GetActiveTrainingIcon()
		{
			foreach (TrainingIcon trainingIcon in this._trainingIcons)
			{
				if (trainingIcon.GetIsActivated())
				{
					return trainingIcon;
				}
			}
			return null;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000C026C File Offset: 0x000BE46C
		private void AddBoundary(GameEntity boundary)
		{
			this._boundaries.Add(boundary);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000C027C File Offset: 0x000BE47C
		private void AddTaggedWeapon(GameEntity weapon, string tag)
		{
			if (weapon.HasScriptOfType<VolumeBox>())
			{
				this._volumeBoxes.Add(weapon.GetFirstScriptOfType<VolumeBox>());
				return;
			}
			bool flag = false;
			foreach (TutorialArea.TutorialEntity tutorialEntity in this._tagWeapon)
			{
				if (tutorialEntity.Tag == tag)
				{
					tutorialEntity.EntityList.Add(Tuple.Create<GameEntity, MatrixFrame>(weapon, weapon.GetGlobalFrame()));
					if (weapon.HasScriptOfType<DestructableComponent>())
					{
						tutorialEntity.DestructableComponents.Add(weapon.GetFirstScriptOfType<DestructableComponent>());
					}
					else if (weapon.HasScriptOfType<SpawnedItemEntity>())
					{
						tutorialEntity.WeaponList.Add(weapon);
						tutorialEntity.WeaponNames.Add(MBObjectManager.Instance.GetObject<ItemObject>(weapon.GetFirstScriptOfType<SpawnedItemEntity>().WeaponCopy.Item.StringId));
					}
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				this._tagWeapon.Add(new TutorialArea.TutorialEntity(tag, new List<Tuple<GameEntity, MatrixFrame>>
				{
					Tuple.Create<GameEntity, MatrixFrame>(weapon, weapon.GetGlobalFrame())
				}, new List<DestructableComponent>(), new List<GameEntity>(), new List<ItemObject>()));
				if (weapon.HasScriptOfType<DestructableComponent>())
				{
					this._tagWeapon[this._tagWeapon.Count - 1].DestructableComponents.Add(weapon.GetFirstScriptOfType<DestructableComponent>());
					return;
				}
				if (weapon.HasScriptOfType<SpawnedItemEntity>())
				{
					this._tagWeapon[this._tagWeapon.Count - 1].WeaponList.Add(weapon);
					this._tagWeapon[this._tagWeapon.Count - 1].WeaponNames.Add(MBObjectManager.Instance.GetObject<ItemObject>(weapon.GetFirstScriptOfType<SpawnedItemEntity>().WeaponCopy.Item.StringId));
				}
			}
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000C044C File Offset: 0x000BE64C
		public int GetIndexFromTag(string tag)
		{
			for (int i = 0; i < this._tagWeapon.Count; i++)
			{
				if (this._tagWeapon[i].Tag == tag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000C048C File Offset: 0x000BE68C
		public List<string> GetSubTrainingTags()
		{
			List<string> list = new List<string>();
			foreach (TutorialArea.TutorialEntity tutorialEntity in this._tagWeapon)
			{
				list.Add(tutorialEntity.Tag);
			}
			return list;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000C04EC File Offset: 0x000BE6EC
		public void ActivateTaggedWeapons(int index)
		{
			if (index >= this._tagWeapon.Count)
			{
				return;
			}
			this.DeactivateAllWeapons(false);
			foreach (Tuple<GameEntity, MatrixFrame> tuple in this._tagWeapon[index].EntityList)
			{
				tuple.Item1.SetVisibilityExcludeParents(true);
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000C0564 File Offset: 0x000BE764
		public void EquipWeaponsToPlayer(int index)
		{
			foreach (GameEntity gameEntity in this._tagWeapon[index].WeaponList)
			{
				bool flag;
				Agent.Main.OnItemPickup(gameEntity.GetFirstScriptOfType<SpawnedItemEntity>(), EquipmentIndex.None, out flag);
			}
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000C05D0 File Offset: 0x000BE7D0
		public void DeactivateAllWeapons(bool resetDestructibles)
		{
			foreach (TutorialArea.TutorialEntity tutorialEntity in this._tagWeapon)
			{
				if (resetDestructibles)
				{
					foreach (DestructableComponent destructableComponent in tutorialEntity.DestructableComponents)
					{
						destructableComponent.Reset();
						destructableComponent.HitPoint = 1000000f;
						Markable firstScriptOfType = destructableComponent.GameEntity.GetFirstScriptOfType<Markable>();
						if (firstScriptOfType != null)
						{
							firstScriptOfType.DisableMarkerActivation();
						}
					}
				}
				foreach (Tuple<GameEntity, MatrixFrame> tuple in tutorialEntity.EntityList)
				{
					if (!tuple.Item1.HasScriptOfType<DestructableComponent>())
					{
						if (tuple.Item1.HasScriptOfType<SpawnedItemEntity>())
						{
							tuple.Item1.GetFirstScriptOfType<SpawnedItemEntity>().StopPhysicsAndSetFrameForClient(tuple.Item2, null);
							tuple.Item1.GetFirstScriptOfType<SpawnedItemEntity>().HasLifeTime = false;
						}
						GameEntity item = tuple.Item1;
						MatrixFrame item2 = tuple.Item2;
						item.SetGlobalFrame(item2);
					}
					tuple.Item1.SetVisibilityExcludeParents(false);
				}
			}
			this.HideBoundaries();
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000C0758 File Offset: 0x000BE958
		public void ActivateBoundaries()
		{
			if (this._boundariesHidden)
			{
				foreach (GameEntity gameEntity in this._boundaries)
				{
					gameEntity.SetVisibilityExcludeParents(true);
				}
				this._boundariesHidden = false;
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000C07B8 File Offset: 0x000BE9B8
		public void HideBoundaries()
		{
			if (!this._boundariesHidden)
			{
				foreach (GameEntity gameEntity in this._boundaries)
				{
					gameEntity.SetVisibilityExcludeParents(false);
				}
				this._boundariesHidden = true;
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000C0818 File Offset: 0x000BEA18
		public int GetBreakablesCount(int index)
		{
			return this._tagWeapon[index].DestructableComponents.Count;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000C0830 File Offset: 0x000BEA30
		public void MakeDestructible(int index)
		{
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				this._tagWeapon[index].DestructableComponents[i].HitPoint = this._tagWeapon[index].DestructableComponents[i].MaxHitPoint;
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000C0898 File Offset: 0x000BEA98
		public void MarkAllTargets(int index, bool mark)
		{
			foreach (DestructableComponent destructableComponent in this._tagWeapon[index].DestructableComponents)
			{
				if (mark)
				{
					Markable firstScriptOfType = destructableComponent.GameEntity.GetFirstScriptOfType<Markable>();
					if (firstScriptOfType != null)
					{
						firstScriptOfType.ActivateMarkerFor(3f, 10f);
					}
				}
				else
				{
					Markable firstScriptOfType2 = destructableComponent.GameEntity.GetFirstScriptOfType<Markable>();
					if (firstScriptOfType2 != null)
					{
						firstScriptOfType2.DisableMarkerActivation();
					}
				}
			}
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x000C092C File Offset: 0x000BEB2C
		public void ResetMarkingTargetTimers(int index)
		{
			foreach (DestructableComponent destructableComponent in this._tagWeapon[index].DestructableComponents)
			{
				Markable firstScriptOfType = destructableComponent.GameEntity.GetFirstScriptOfType<Markable>();
				if (firstScriptOfType != null)
				{
					firstScriptOfType.ResetPassiveDurationTimer();
				}
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000C0998 File Offset: 0x000BEB98
		public void MakeInDestructible(int index)
		{
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				this._tagWeapon[index].DestructableComponents[i].HitPoint = 1000000f;
			}
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000C09E8 File Offset: 0x000BEBE8
		public bool AllBreakablesAreBroken(int index)
		{
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				if (!this._tagWeapon[index].DestructableComponents[i].IsDestroyed)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x000C0A38 File Offset: 0x000BEC38
		public int GetBrokenBreakableCount(int index)
		{
			int num = 0;
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				if (this._tagWeapon[index].DestructableComponents[i].IsDestroyed)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x000C0A8C File Offset: 0x000BEC8C
		public int GetUnbrokenBreakableCount(int index)
		{
			int num = 0;
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				if (!this._tagWeapon[index].DestructableComponents[i].IsDestroyed)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x000C0AE0 File Offset: 0x000BECE0
		public void ResetBreakables(int index, bool makeIndestructible = true)
		{
			for (int i = 0; i < this._tagWeapon[index].DestructableComponents.Count; i++)
			{
				if (makeIndestructible)
				{
					this._tagWeapon[index].DestructableComponents[i].HitPoint = 1000000f;
				}
				this._tagWeapon[index].DestructableComponents[i].Reset();
			}
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x000C0B50 File Offset: 0x000BED50
		public bool HasMainAgentPickedAll(int index)
		{
			using (List<GameEntity>.Enumerator enumerator = this._tagWeapon[index].WeaponList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasScriptOfType<SpawnedItemEntity>())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x000C0BB4 File Offset: 0x000BEDB4
		public void CheckMainAgentEquipment(int index)
		{
			this._allowedWeaponsHelper.Clear();
			this._allowedWeaponsHelper.AddRange(this._tagWeapon[index].WeaponNames);
			EquipmentIndex i;
			EquipmentIndex j;
			for (i = EquipmentIndex.WeaponItemBeginSlot; i <= EquipmentIndex.Weapon3; i = j + 1)
			{
				if (!Mission.Current.MainAgent.Equipment[i].IsEmpty)
				{
					if (this._allowedWeaponsHelper.Exists((ItemObject x) => x == Mission.Current.MainAgent.Equipment[i].Item))
					{
						this._allowedWeaponsHelper.Remove(Mission.Current.MainAgent.Equipment[i].Item);
					}
					else
					{
						Mission.Current.MainAgent.DropItem(i, WeaponClass.Undefined);
						MBInformationManager.AddQuickInformation(new TextObject("{=3PP01vFv}Keep away from that weapon.", null), 0, null, "");
					}
				}
				j = i;
			}
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000C0CB0 File Offset: 0x000BEEB0
		public void CheckWeapons(int index)
		{
			foreach (GameEntity gameEntity in this._tagWeapon[index].WeaponList)
			{
				if (gameEntity.HasScriptOfType<SpawnedItemEntity>())
				{
					gameEntity.GetFirstScriptOfType<SpawnedItemEntity>().HasLifeTime = false;
				}
			}
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x000C0D1C File Offset: 0x000BEF1C
		public bool IsPositionInsideTutorialArea(Vec3 position, out string[] volumeBoxTags)
		{
			foreach (VolumeBox volumeBox in this._volumeBoxes)
			{
				if (volumeBox.IsPointIn(position))
				{
					volumeBoxTags = volumeBox.GameEntity.Tags;
					return true;
				}
			}
			volumeBoxTags = null;
			return false;
		}

		// Token: 0x040013D9 RID: 5081
		[EditableScriptComponentVariable(true)]
		private TutorialArea.TrainingType _typeOfTraining;

		// Token: 0x040013DA RID: 5082
		[EditableScriptComponentVariable(true)]
		private string _tagPrefix = "A_";

		// Token: 0x040013DB RID: 5083
		private readonly List<TutorialArea.TutorialEntity> _tagWeapon = new List<TutorialArea.TutorialEntity>();

		// Token: 0x040013DC RID: 5084
		private readonly List<VolumeBox> _volumeBoxes = new List<VolumeBox>();

		// Token: 0x040013DD RID: 5085
		private readonly List<GameEntity> _boundaries = new List<GameEntity>();

		// Token: 0x040013DE RID: 5086
		private bool _boundariesHidden;

		// Token: 0x040013DF RID: 5087
		private readonly List<GameEntity> _highlightedEntities = new List<GameEntity>();

		// Token: 0x040013E0 RID: 5088
		private readonly List<ItemObject> _allowedWeaponsHelper = new List<ItemObject>();

		// Token: 0x040013E1 RID: 5089
		private readonly MBList<TrainingIcon> _trainingIcons = new MBList<TrainingIcon>();

		// Token: 0x02000612 RID: 1554
		public enum TrainingType
		{
			// Token: 0x04001F93 RID: 8083
			Bow,
			// Token: 0x04001F94 RID: 8084
			Melee,
			// Token: 0x04001F95 RID: 8085
			Mounted,
			// Token: 0x04001F96 RID: 8086
			AdvancedMelee
		}

		// Token: 0x02000613 RID: 1555
		private struct TutorialEntity
		{
			// Token: 0x06003C1D RID: 15389 RVA: 0x000E9C46 File Offset: 0x000E7E46
			public TutorialEntity(string tag, List<Tuple<GameEntity, MatrixFrame>> entityList, List<DestructableComponent> destructableComponents, List<GameEntity> weapon, List<ItemObject> weaponNames)
			{
				this.Tag = tag;
				this.EntityList = entityList;
				this.DestructableComponents = destructableComponents;
				this.WeaponList = weapon;
				this.WeaponNames = weaponNames;
			}

			// Token: 0x04001F97 RID: 8087
			public string Tag;

			// Token: 0x04001F98 RID: 8088
			public List<Tuple<GameEntity, MatrixFrame>> EntityList;

			// Token: 0x04001F99 RID: 8089
			public List<DestructableComponent> DestructableComponents;

			// Token: 0x04001F9A RID: 8090
			public List<GameEntity> WeaponList;

			// Token: 0x04001F9B RID: 8091
			public List<ItemObject> WeaponNames;
		}
	}
}
