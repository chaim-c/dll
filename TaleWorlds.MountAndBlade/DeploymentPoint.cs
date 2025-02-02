using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions;
using TaleWorlds.MountAndBlade.Objects.Siege;
using TaleWorlds.MountAndBlade.Objects.Usables;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000331 RID: 817
	public class DeploymentPoint : SynchedMissionObject
	{
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06002BF1 RID: 11249 RVA: 0x000AB9CC File Offset: 0x000A9BCC
		// (remove) Token: 0x06002BF2 RID: 11250 RVA: 0x000ABA04 File Offset: 0x000A9C04
		public event Action<DeploymentPoint, SynchedMissionObject> OnDeploymentStateChanged;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06002BF3 RID: 11251 RVA: 0x000ABA3C File Offset: 0x000A9C3C
		// (remove) Token: 0x06002BF4 RID: 11252 RVA: 0x000ABA74 File Offset: 0x000A9C74
		public event Action<DeploymentPoint> OnDeploymentPointTypeDetermined;

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000ABAA9 File Offset: 0x000A9CA9
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x000ABAB1 File Offset: 0x000A9CB1
		public Vec3 DeploymentTargetPosition { get; private set; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x000ABABA File Offset: 0x000A9CBA
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x000ABAC2 File Offset: 0x000A9CC2
		public WallSegment AssociatedWallSegment { get; private set; }

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000ABACB File Offset: 0x000A9CCB
		public IEnumerable<SynchedMissionObject> DeployableWeapons
		{
			get
			{
				return from w in this._weapons
				where !w.IsDisabled
				select w;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x000ABAF7 File Offset: 0x000A9CF7
		public bool IsDeployed
		{
			get
			{
				return this.DeployedWeapon != null;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000ABB02 File Offset: 0x000A9D02
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000ABB0A File Offset: 0x000A9D0A
		public SynchedMissionObject DeployedWeapon { get; private set; }

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000ABB13 File Offset: 0x000A9D13
		// (set) Token: 0x06002BFE RID: 11262 RVA: 0x000ABB1B File Offset: 0x000A9D1B
		public SynchedMissionObject DisbandedWeapon { get; private set; }

		// Token: 0x06002BFF RID: 11263 RVA: 0x000ABB24 File Offset: 0x000A9D24
		protected internal override void OnInit()
		{
			this._weapons = new MBList<SynchedMissionObject>();
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000ABB34 File Offset: 0x000A9D34
		public override void AfterMissionStart()
		{
			base.OnInit();
			if (!GameNetwork.IsClientOrReplay)
			{
				this._weapons = this.GetWeaponsUnder();
				this._associatedSiegeLadders = new List<SiegeLadder>();
				if (this.DeployableWeapons.IsEmpty<SynchedMissionObject>())
				{
					this.SetVisibleSynched(false, false);
					this.SetBreachSideDeploymentPoint();
				}
				base.AfterMissionStart();
				if (!GameNetwork.IsClientOrReplay)
				{
					this.DetermineDeploymentPointType();
				}
				this.HideAllWeapons();
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000ABB9C File Offset: 0x000A9D9C
		private void SetBreachSideDeploymentPoint()
		{
			Debug.Print("Deployment point " + ((base.GameEntity != null) ? ("upgrade level mask " + base.GameEntity.GetUpgradeLevelMask().ToString()) : "no game entity.") + "\n", 0, Debug.DebugColor.White, 17592186044416UL);
			this._isBreachSideDeploymentPoint = true;
			this._deploymentPointType = DeploymentPoint.DeploymentPointType.Breach;
			FormationAI.BehaviorSide deploymentPointSide = (this._weapons.FirstOrDefault((SynchedMissionObject w) => w is SiegeTower) as IPrimarySiegeWeapon).WeaponSide;
			this.AssociatedWallSegment = Mission.Current.ActiveMissionObjects.FindAllWithType<WallSegment>().FirstOrDefault((WallSegment ws) => ws.DefenseSide == deploymentPointSide);
			this.DeploymentTargetPosition = this.AssociatedWallSegment.GameEntity.GlobalPosition;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000ABC8B File Offset: 0x000A9E8B
		public Vec3 GetDeploymentOrigin()
		{
			return base.GameEntity.GlobalPosition;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000ABC98 File Offset: 0x000A9E98
		public DeploymentPoint.DeploymentPointState GetDeploymentPointState()
		{
			switch (this._deploymentPointType)
			{
			case DeploymentPoint.DeploymentPointType.BatteringRam:
				if (!this.IsDeployed)
				{
					return DeploymentPoint.DeploymentPointState.NotDeployed;
				}
				return DeploymentPoint.DeploymentPointState.BatteringRam;
			case DeploymentPoint.DeploymentPointType.TowerLadder:
				if (!this.IsDeployed)
				{
					return DeploymentPoint.DeploymentPointState.SiegeLadder;
				}
				return DeploymentPoint.DeploymentPointState.SiegeTower;
			case DeploymentPoint.DeploymentPointType.Breach:
				return DeploymentPoint.DeploymentPointState.Breach;
			case DeploymentPoint.DeploymentPointType.Ranged:
				if (!this.IsDeployed)
				{
					return DeploymentPoint.DeploymentPointState.NotDeployed;
				}
				return DeploymentPoint.DeploymentPointState.Ranged;
			default:
				MBDebug.ShowWarning("Undefined deployment point type fetched.");
				return DeploymentPoint.DeploymentPointState.NotDeployed;
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000ABCF5 File Offset: 0x000A9EF5
		public DeploymentPoint.DeploymentPointType GetDeploymentPointType()
		{
			return this._deploymentPointType;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000ABCFD File Offset: 0x000A9EFD
		public List<SiegeLadder> GetAssociatedSiegeLadders()
		{
			return this._associatedSiegeLadders;
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000ABD08 File Offset: 0x000A9F08
		private void DetermineDeploymentPointType()
		{
			if (this._isBreachSideDeploymentPoint)
			{
				this._deploymentPointType = DeploymentPoint.DeploymentPointType.Breach;
			}
			else if (this._weapons.Any((SynchedMissionObject w) => w is BatteringRam))
			{
				this._deploymentPointType = DeploymentPoint.DeploymentPointType.BatteringRam;
				this.DeploymentTargetPosition = (this._weapons.First((SynchedMissionObject w) => w is BatteringRam) as IPrimarySiegeWeapon).TargetCastlePosition.GameEntity.GlobalPosition;
			}
			else if (this._weapons.Any((SynchedMissionObject w) => w is SiegeTower))
			{
				SiegeTower tower = this._weapons.FirstOrDefault((SynchedMissionObject w) => w is SiegeTower) as SiegeTower;
				this._deploymentPointType = DeploymentPoint.DeploymentPointType.TowerLadder;
				this.DeploymentTargetPosition = tower.TargetCastlePosition.GameEntity.GlobalPosition;
				this._associatedSiegeLadders = (from sl in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeLadder>()
				where sl.WeaponSide == tower.WeaponSide
				select sl).ToList<SiegeLadder>();
			}
			else
			{
				this._deploymentPointType = DeploymentPoint.DeploymentPointType.Ranged;
				this.DeploymentTargetPosition = Vec3.Invalid;
			}
			Action<DeploymentPoint> onDeploymentPointTypeDetermined = this.OnDeploymentPointTypeDetermined;
			if (onDeploymentPointTypeDetermined == null)
			{
				return;
			}
			onDeploymentPointTypeDetermined(this);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000ABE84 File Offset: 0x000AA084
		public MBList<SynchedMissionObject> GetWeaponsUnder()
		{
			TeamAISiegeComponent teamAISiegeComponent;
			List<SiegeWeapon> list;
			if ((teamAISiegeComponent = (Mission.Current.Teams[0].TeamAI as TeamAISiegeComponent)) != null)
			{
				list = teamAISiegeComponent.SceneSiegeWeapons;
			}
			else
			{
				List<GameEntity> source = new List<GameEntity>();
				base.GameEntity.Scene.GetEntities(ref source);
				list = (from se in source
				where se.HasScriptOfType<SiegeWeapon>()
				select se.GetScriptComponents<SiegeWeapon>().FirstOrDefault<SiegeWeapon>()).ToList<SiegeWeapon>();
			}
			MBList<SynchedMissionObject> mblist = new MBList<SynchedMissionObject>();
			float num = this.Radius * this.Radius;
			foreach (SiegeWeapon siegeWeapon in list)
			{
				if (siegeWeapon.GameEntity.HasTag(this.SiegeWeaponTag) || (siegeWeapon.GameEntity.Parent != null && siegeWeapon.GameEntity.Parent.HasTag(this.SiegeWeaponTag)) || (siegeWeapon.GameEntity != base.GameEntity && siegeWeapon.GameEntity.GlobalPosition.DistanceSquared(base.GameEntity.GlobalPosition) < num))
				{
					mblist.Add(siegeWeapon);
				}
			}
			return mblist;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x000ABFF8 File Offset: 0x000AA1F8
		public IEnumerable<SpawnerBase> GetSpawnersForEditor()
		{
			List<GameEntity> source = new List<GameEntity>();
			base.GameEntity.Scene.GetEntities(ref source);
			IEnumerable<SpawnerBase> source2 = from se in source
			where se.HasScriptOfType<SpawnerBase>()
			select se.GetScriptComponents<SpawnerBase>().FirstOrDefault<SpawnerBase>();
			IEnumerable<SpawnerBase> first = from ssw in source2
			where ssw.GameEntity.HasTag(this.SiegeWeaponTag)
			select ssw;
			Vec3 globalPosition = base.GameEntity.GlobalPosition;
			float radiusSquared = this.Radius * this.Radius;
			IEnumerable<SpawnerBase> second = from ssw in source2
			where ssw.GameEntity != this.GameEntity && ssw.GameEntity.GlobalPosition.DistanceSquared(globalPosition) < radiusSquared
			select ssw;
			return first.Concat(second).Distinct<SpawnerBase>();
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x000AC110 File Offset: 0x000AA310
		protected internal override void OnEditorInit()
		{
			base.OnEditorInit();
			this._weapons = null;
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x000AC120 File Offset: 0x000AA320
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			foreach (GameEntity gameEntity in this._highlightedEntites)
			{
				gameEntity.SetContourColor(null, true);
			}
			this._highlightedEntites.Clear();
			if (MBEditor.IsEntitySelected(base.GameEntity))
			{
				uint num = 4294901760U;
				if (this.Radius > 0f)
				{
					DebugExtensions.RenderDebugCircleOnTerrain(base.Scene, base.GameEntity.GetGlobalFrame(), this.Radius, num, true, false);
				}
				foreach (SpawnerBase spawnerBase in this.GetSpawnersForEditor())
				{
					spawnerBase.GameEntity.SetContourColor(new uint?(num), true);
					this._highlightedEntites.Add(spawnerBase.GameEntity);
				}
			}
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x000AC22C File Offset: 0x000AA42C
		private void OnDeploymentStateChangedAux(SynchedMissionObject targetObject)
		{
			if (this.IsDeployed)
			{
				targetObject.SetVisibleSynched(true, false);
				targetObject.SetPhysicsStateSynched(true, true);
			}
			else
			{
				targetObject.SetVisibleSynched(false, false);
				targetObject.SetPhysicsStateSynched(false, true);
			}
			Action<DeploymentPoint, SynchedMissionObject> onDeploymentStateChanged = this.OnDeploymentStateChanged;
			if (onDeploymentStateChanged != null)
			{
				onDeploymentStateChanged(this, targetObject);
			}
			SiegeWeapon siegeWeapon;
			if ((siegeWeapon = (targetObject as SiegeWeapon)) != null)
			{
				siegeWeapon.OnDeploymentStateChanged(this.IsDeployed);
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000AC28C File Offset: 0x000AA48C
		public SiegeMachineStonePile GetStonePileOfWeapon(SynchedMissionObject weapon)
		{
			if (weapon is SiegeWeapon)
			{
				foreach (GameEntity gameEntity in weapon.GameEntity.Parent.GetChildren())
				{
					SiegeMachineStonePile firstScriptOfType = gameEntity.GetFirstScriptOfType<SiegeMachineStonePile>();
					if (firstScriptOfType != null)
					{
						return firstScriptOfType;
					}
				}
			}
			return null;
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000AC2F4 File Offset: 0x000AA4F4
		public void Deploy(Type t)
		{
			this.DeployedWeapon = this._weapons.First((SynchedMissionObject w) => MissionSiegeWeaponsController.GetWeaponType(w) == t);
			this.OnDeploymentStateChangedAux(this.DeployedWeapon);
			this.ToggleDeploymentPointVisibility(false);
			this.ToggleDeployedWeaponVisibility(true);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x000AC345 File Offset: 0x000AA545
		public void Deploy(SiegeWeapon s)
		{
			this.DeployedWeapon = s;
			this.DisbandedWeapon = null;
			this.OnDeploymentStateChangedAux(s);
			this.ToggleDeploymentPointVisibility(false);
			this.ToggleDeployedWeaponVisibility(true);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x000AC36A File Offset: 0x000AA56A
		public ScriptComponentBehavior Disband()
		{
			this.ToggleDeploymentPointVisibility(true);
			this.ToggleDeployedWeaponVisibility(false);
			this.DisbandedWeapon = this.DeployedWeapon;
			this.DeployedWeapon = null;
			this.OnDeploymentStateChangedAux(this.DisbandedWeapon);
			return this.DisbandedWeapon;
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000AC39F File Offset: 0x000AA59F
		public IEnumerable<Type> DeployableWeaponTypes
		{
			get
			{
				return this.DeployableWeapons.Select(new Func<SynchedMissionObject, Type>(MissionSiegeWeaponsController.GetWeaponType));
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x000AC3B8 File Offset: 0x000AA5B8
		public void Hide()
		{
			this.ToggleDeploymentPointVisibility(false);
			foreach (SynchedMissionObject synchedMissionObject in this.GetWeaponsUnder())
			{
				if (synchedMissionObject != null)
				{
					synchedMissionObject.SetVisibleSynched(false, false);
					synchedMissionObject.SetPhysicsStateSynched(false, true);
				}
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x000AC420 File Offset: 0x000AA620
		public void Show()
		{
			this.ToggleDeploymentPointVisibility(!this.IsDeployed);
			if (this.IsDeployed)
			{
				this.ToggleDeployedWeaponVisibility(true);
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000AC440 File Offset: 0x000AA640
		private void ToggleDeploymentPointVisibility(bool visible)
		{
			this.SetVisibleSynched(visible, false);
			this.SetPhysicsStateSynched(visible, true);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x000AC452 File Offset: 0x000AA652
		private void ToggleDeployedWeaponVisibility(bool visible)
		{
			this.ToggleWeaponVisibility(visible, this.DeployedWeapon);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x000AC464 File Offset: 0x000AA664
		public void ToggleWeaponVisibility(bool visible, SynchedMissionObject weapon)
		{
			SynchedMissionObject synchedMissionObject;
			if (weapon == null)
			{
				synchedMissionObject = null;
			}
			else
			{
				GameEntity parent = weapon.GameEntity.Parent;
				synchedMissionObject = ((parent != null) ? parent.GetFirstScriptOfType<SynchedMissionObject>() : null);
			}
			SynchedMissionObject synchedMissionObject2 = synchedMissionObject;
			if (synchedMissionObject2 != null)
			{
				synchedMissionObject2.SetVisibleSynched(visible, false);
				synchedMissionObject2.SetPhysicsStateSynched(visible, true);
			}
			else
			{
				if (weapon != null)
				{
					weapon.SetVisibleSynched(visible, false);
				}
				if (weapon != null)
				{
					weapon.SetPhysicsStateSynched(visible, true);
				}
			}
			if (weapon is SiegeWeapon && weapon.GameEntity.Parent != null)
			{
				foreach (GameEntity gameEntity in weapon.GameEntity.Parent.GetChildren())
				{
					SiegeMachineStonePile firstScriptOfType = gameEntity.GetFirstScriptOfType<SiegeMachineStonePile>();
					if (firstScriptOfType != null)
					{
						firstScriptOfType.SetPhysicsStateSynched(visible, true);
						break;
					}
				}
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x000AC52C File Offset: 0x000AA72C
		public void HideAllWeapons()
		{
			foreach (SynchedMissionObject weapon in this.DeployableWeapons)
			{
				this.ToggleWeaponVisibility(false, weapon);
			}
		}

		// Token: 0x04001177 RID: 4471
		public BattleSideEnum Side = BattleSideEnum.Attacker;

		// Token: 0x04001178 RID: 4472
		public float Radius = 3f;

		// Token: 0x04001179 RID: 4473
		public string SiegeWeaponTag = "dpWeapon";

		// Token: 0x0400117A RID: 4474
		private readonly List<GameEntity> _highlightedEntites = new List<GameEntity>();

		// Token: 0x0400117B RID: 4475
		private DeploymentPoint.DeploymentPointType _deploymentPointType;

		// Token: 0x0400117C RID: 4476
		private List<SiegeLadder> _associatedSiegeLadders;

		// Token: 0x0400117D RID: 4477
		private bool _isBreachSideDeploymentPoint;

		// Token: 0x0400117E RID: 4478
		private MBList<SynchedMissionObject> _weapons;

		// Token: 0x020005E1 RID: 1505
		public enum DeploymentPointType
		{
			// Token: 0x04001ECB RID: 7883
			BatteringRam,
			// Token: 0x04001ECC RID: 7884
			TowerLadder,
			// Token: 0x04001ECD RID: 7885
			Breach,
			// Token: 0x04001ECE RID: 7886
			Ranged
		}

		// Token: 0x020005E2 RID: 1506
		public enum DeploymentPointState
		{
			// Token: 0x04001ED0 RID: 7888
			NotDeployed,
			// Token: 0x04001ED1 RID: 7889
			BatteringRam,
			// Token: 0x04001ED2 RID: 7890
			SiegeLadder,
			// Token: 0x04001ED3 RID: 7891
			SiegeTower,
			// Token: 0x04001ED4 RID: 7892
			Breach,
			// Token: 0x04001ED5 RID: 7893
			Ranged
		}
	}
}
