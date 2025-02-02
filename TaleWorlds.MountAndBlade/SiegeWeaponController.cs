using System;
using System.Collections.Generic;
using System.Linq;
using NetworkMessages.FromClient;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000161 RID: 353
	public class SiegeWeaponController
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000383E0 File Offset: 0x000365E0
		public MBReadOnlyList<SiegeWeapon> SelectedWeapons
		{
			get
			{
				return this._selectedWeapons;
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06001193 RID: 4499 RVA: 0x000383E8 File Offset: 0x000365E8
		// (remove) Token: 0x06001194 RID: 4500 RVA: 0x00038420 File Offset: 0x00036620
		public event Action<SiegeWeaponOrderType, IEnumerable<SiegeWeapon>> OnOrderIssued;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06001195 RID: 4501 RVA: 0x00038458 File Offset: 0x00036658
		// (remove) Token: 0x06001196 RID: 4502 RVA: 0x00038490 File Offset: 0x00036690
		public event Action OnSelectedSiegeWeaponsChanged;

		// Token: 0x06001197 RID: 4503 RVA: 0x000384C5 File Offset: 0x000366C5
		public SiegeWeaponController(Mission mission, Team team)
		{
			this._mission = mission;
			this._team = team;
			this._selectedWeapons = new MBList<SiegeWeapon>();
			this.InitializeWeaponsForDeployment();
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000384EC File Offset: 0x000366EC
		private void InitializeWeaponsForDeployment()
		{
			IEnumerable<SiegeWeapon> source = from w in (from dp in this._mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>()
			where dp.Side == this._team.Side
			select dp).SelectMany((DeploymentPoint dp) => dp.DeployableWeapons)
			select w as SiegeWeapon;
			this._availableWeapons = source.ToList<SiegeWeapon>();
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00038570 File Offset: 0x00036770
		private void InitializeWeapons()
		{
			this._availableWeapons = new List<SiegeWeapon>();
			this._availableWeapons.AddRange(from w in this._mission.ActiveMissionObjects.FindAllWithType<RangedSiegeWeapon>()
			where w.Side == this._team.Side
			select w);
			if (this._team.Side == BattleSideEnum.Attacker)
			{
				this._availableWeapons.AddRange(from w in this._mission.ActiveMissionObjects.FindAllWithType<SiegeWeapon>()
				where w is IPrimarySiegeWeapon && !(w is RangedSiegeWeapon)
				select w);
			}
			this._availableWeapons.Sort((SiegeWeapon w1, SiegeWeapon w2) => this.GetShortcutIndexOf(w1).CompareTo(this.GetShortcutIndexOf(w2)));
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00038618 File Offset: 0x00036818
		public void Select(SiegeWeapon weapon)
		{
			if (this.SelectedWeapons.Contains(weapon) || !SiegeWeaponController.IsWeaponSelectable(weapon))
			{
				Debug.FailedAssert("Weapon already selected or is not selectable", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "Select", 82);
				return;
			}
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new SelectSiegeWeapon(weapon.Id));
				GameNetwork.EndModuleEventAsClient();
			}
			this._selectedWeapons.Add(weapon);
			Action onSelectedSiegeWeaponsChanged = this.OnSelectedSiegeWeaponsChanged;
			if (onSelectedSiegeWeaponsChanged == null)
			{
				return;
			}
			onSelectedSiegeWeaponsChanged();
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0003868F File Offset: 0x0003688F
		public void ClearSelectedWeapons()
		{
			bool isClient = GameNetwork.IsClient;
			this._selectedWeapons.Clear();
			Action onSelectedSiegeWeaponsChanged = this.OnSelectedSiegeWeaponsChanged;
			if (onSelectedSiegeWeaponsChanged == null)
			{
				return;
			}
			onSelectedSiegeWeaponsChanged();
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000386B4 File Offset: 0x000368B4
		public void Deselect(SiegeWeapon weapon)
		{
			if (!this.SelectedWeapons.Contains(weapon))
			{
				Debug.FailedAssert("Trying to deselect an unselected weapon", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "Deselect", 113);
				return;
			}
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new UnselectSiegeWeapon(weapon.Id));
				GameNetwork.EndModuleEventAsClient();
			}
			this._selectedWeapons.Remove(weapon);
			Action onSelectedSiegeWeaponsChanged = this.OnSelectedSiegeWeaponsChanged;
			if (onSelectedSiegeWeaponsChanged == null)
			{
				return;
			}
			onSelectedSiegeWeaponsChanged();
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00038724 File Offset: 0x00036924
		public void SelectAll()
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new SelectAllSiegeWeapons());
				GameNetwork.EndModuleEventAsClient();
			}
			this._selectedWeapons.Clear();
			foreach (SiegeWeapon item in this._availableWeapons)
			{
				this._selectedWeapons.Add(item);
			}
			Action onSelectedSiegeWeaponsChanged = this.OnSelectedSiegeWeaponsChanged;
			if (onSelectedSiegeWeaponsChanged == null)
			{
				return;
			}
			onSelectedSiegeWeaponsChanged();
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000387B4 File Offset: 0x000369B4
		public static bool IsWeaponSelectable(SiegeWeapon weapon)
		{
			return !weapon.IsDeactivated;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000387C0 File Offset: 0x000369C0
		public static SiegeWeaponOrderType GetActiveOrderOf(SiegeWeapon weapon)
		{
			if (!weapon.ForcedUse)
			{
				return SiegeWeaponOrderType.Stop;
			}
			if (!(weapon is RangedSiegeWeapon))
			{
				return SiegeWeaponOrderType.Attack;
			}
			switch (((RangedSiegeWeapon)weapon).Focus)
			{
			case RangedSiegeWeapon.FiringFocus.Troops:
				return SiegeWeaponOrderType.FireAtTroops;
			case RangedSiegeWeapon.FiringFocus.Walls:
				return SiegeWeaponOrderType.FireAtWalls;
			case RangedSiegeWeapon.FiringFocus.RangedSiegeWeapons:
				return SiegeWeaponOrderType.FireAtRangedSiegeWeapons;
			case RangedSiegeWeapon.FiringFocus.PrimarySiegeWeapons:
				return SiegeWeaponOrderType.FireAtPrimarySiegeWeapons;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "GetActiveOrderOf", 166);
				return SiegeWeaponOrderType.FireAtTroops;
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00038827 File Offset: 0x00036A27
		public static SiegeWeaponOrderType GetActiveMovementOrderOf(SiegeWeapon weapon)
		{
			if (!weapon.ForcedUse)
			{
				return SiegeWeaponOrderType.Stop;
			}
			return SiegeWeaponOrderType.Attack;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00038834 File Offset: 0x00036A34
		public static SiegeWeaponOrderType GetActiveFacingOrderOf(SiegeWeapon weapon)
		{
			if (!(weapon is RangedSiegeWeapon))
			{
				return SiegeWeaponOrderType.FireAtWalls;
			}
			switch (((RangedSiegeWeapon)weapon).Focus)
			{
			case RangedSiegeWeapon.FiringFocus.Troops:
				return SiegeWeaponOrderType.FireAtTroops;
			case RangedSiegeWeapon.FiringFocus.Walls:
				return SiegeWeaponOrderType.FireAtWalls;
			case RangedSiegeWeapon.FiringFocus.RangedSiegeWeapons:
				return SiegeWeaponOrderType.FireAtRangedSiegeWeapons;
			case RangedSiegeWeapon.FiringFocus.PrimarySiegeWeapons:
				return SiegeWeaponOrderType.FireAtPrimarySiegeWeapons;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "GetActiveFacingOrderOf", 204);
				return SiegeWeaponOrderType.FireAtTroops;
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00038891 File Offset: 0x00036A91
		public static SiegeWeaponOrderType GetActiveFiringOrderOf(SiegeWeapon weapon)
		{
			if (!weapon.ForcedUse)
			{
				return SiegeWeaponOrderType.Stop;
			}
			return SiegeWeaponOrderType.Attack;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0003889E File Offset: 0x00036A9E
		public static SiegeWeaponOrderType GetActiveAIControlOrderOf(SiegeWeapon weapon)
		{
			if (weapon.ForcedUse)
			{
				return SiegeWeaponOrderType.AIControlOn;
			}
			return SiegeWeaponOrderType.AIControlOff;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000388AC File Offset: 0x00036AAC
		private void SetOrderAux(SiegeWeaponOrderType order, SiegeWeapon weapon)
		{
			switch (order)
			{
			case SiegeWeaponOrderType.Stop:
			case SiegeWeaponOrderType.AIControlOff:
				weapon.SetForcedUse(false);
				return;
			case SiegeWeaponOrderType.Attack:
			case SiegeWeaponOrderType.AIControlOn:
				weapon.SetForcedUse(true);
				return;
			case SiegeWeaponOrderType.FireAtWalls:
			{
				weapon.SetForcedUse(true);
				RangedSiegeWeapon rangedSiegeWeapon = weapon as RangedSiegeWeapon;
				if (rangedSiegeWeapon != null)
				{
					rangedSiegeWeapon.Focus = RangedSiegeWeapon.FiringFocus.Walls;
					return;
				}
				break;
			}
			case SiegeWeaponOrderType.FireAtTroops:
			{
				weapon.SetForcedUse(true);
				RangedSiegeWeapon rangedSiegeWeapon2 = weapon as RangedSiegeWeapon;
				if (rangedSiegeWeapon2 != null)
				{
					rangedSiegeWeapon2.Focus = RangedSiegeWeapon.FiringFocus.Troops;
					return;
				}
				break;
			}
			case SiegeWeaponOrderType.FireAtRangedSiegeWeapons:
			{
				weapon.SetForcedUse(true);
				RangedSiegeWeapon rangedSiegeWeapon3 = weapon as RangedSiegeWeapon;
				if (rangedSiegeWeapon3 != null)
				{
					rangedSiegeWeapon3.Focus = RangedSiegeWeapon.FiringFocus.RangedSiegeWeapons;
					return;
				}
				break;
			}
			case SiegeWeaponOrderType.FireAtPrimarySiegeWeapons:
			{
				weapon.SetForcedUse(true);
				RangedSiegeWeapon rangedSiegeWeapon4 = weapon as RangedSiegeWeapon;
				if (rangedSiegeWeapon4 != null)
				{
					rangedSiegeWeapon4.Focus = RangedSiegeWeapon.FiringFocus.PrimarySiegeWeapons;
					return;
				}
				break;
			}
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "SetOrderAux", 294);
				break;
			}
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00038970 File Offset: 0x00036B70
		public void SetOrder(SiegeWeaponOrderType order)
		{
			if (GameNetwork.IsClient)
			{
				GameNetwork.BeginModuleEventAsClient();
				GameNetwork.WriteMessage(new ApplySiegeWeaponOrder(order));
				GameNetwork.EndModuleEventAsClient();
			}
			foreach (SiegeWeapon weapon in this.SelectedWeapons)
			{
				this.SetOrderAux(order, weapon);
			}
			Action<SiegeWeaponOrderType, IEnumerable<SiegeWeapon>> onOrderIssued = this.OnOrderIssued;
			if (onOrderIssued == null)
			{
				return;
			}
			onOrderIssued(order, this.SelectedWeapons);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000389F8 File Offset: 0x00036BF8
		public int GetShortcutIndexOf(SiegeWeapon weapon)
		{
			FormationAI.BehaviorSide sideOf = SiegeWeaponController.GetSideOf(weapon);
			int num = (sideOf == FormationAI.BehaviorSide.Left) ? 1 : ((sideOf == FormationAI.BehaviorSide.Right) ? 2 : 0);
			if (!(weapon is IPrimarySiegeWeapon))
			{
				num += 3;
			}
			return num;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00038A28 File Offset: 0x00036C28
		private static FormationAI.BehaviorSide GetSideOf(SiegeWeapon weapon)
		{
			IPrimarySiegeWeapon primarySiegeWeapon = weapon as IPrimarySiegeWeapon;
			if (primarySiegeWeapon != null)
			{
				return primarySiegeWeapon.WeaponSide;
			}
			if (weapon is RangedSiegeWeapon)
			{
				return FormationAI.BehaviorSide.Middle;
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\SiegeWeaponController.cs", "GetSideOf", 346);
			return FormationAI.BehaviorSide.Middle;
		}

		// Token: 0x04000475 RID: 1141
		private readonly Mission _mission;

		// Token: 0x04000476 RID: 1142
		private readonly Team _team;

		// Token: 0x04000477 RID: 1143
		private List<SiegeWeapon> _availableWeapons;

		// Token: 0x04000478 RID: 1144
		private MBList<SiegeWeapon> _selectedWeapons;
	}
}
