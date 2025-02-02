using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects.Siege;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000025 RID: 37
	public class OrderSiegeMachineVM : OrderSubjectVM
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public DeploymentPoint DeploymentPoint { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000D3C1 File Offset: 0x0000B5C1
		public SiegeWeapon SiegeWeapon
		{
			get
			{
				if (this.DeploymentPoint != null)
				{
					return this.DeploymentPoint.DeployedWeapon as SiegeWeapon;
				}
				return null;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000D3DD File Offset: 0x0000B5DD
		public bool IsPrimarySiegeMachine
		{
			get
			{
				return this.SiegeWeapon is IPrimarySiegeWeapon;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000D3ED File Offset: 0x0000B5ED
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000D3F5 File Offset: 0x0000B5F5
		[DataSourceProperty]
		public string MachineClass
		{
			get
			{
				return this._machineClass;
			}
			set
			{
				this._machineClass = value;
				base.OnPropertyChangedWithValue<string>(value, "MachineClass");
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000D40A File Offset: 0x0000B60A
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000D412 File Offset: 0x0000B612
		[DataSourceProperty]
		public double CurrentHP
		{
			get
			{
				return this._currentHP;
			}
			set
			{
				if (value != this._currentHP)
				{
					this._currentHP = value;
					base.OnPropertyChangedWithValue(value, "CurrentHP");
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000D430 File Offset: 0x0000B630
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000D438 File Offset: 0x0000B638
		public bool IsInside
		{
			get
			{
				return this._isInside;
			}
			set
			{
				if (value != this._isInside)
				{
					this._isInside = value;
					base.OnPropertyChangedWithValue(value, "IsInside");
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000D456 File Offset: 0x0000B656
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000D45E File Offset: 0x0000B65E
		public Vec2 Position
		{
			get
			{
				return this._position;
			}
			set
			{
				if (this._position != value)
				{
					this._position = value;
					base.OnPropertyChangedWithValue(value, "Position");
				}
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D481 File Offset: 0x0000B681
		private void ExecuteAction()
		{
			if (this.SiegeWeapon != null)
			{
				this.SetSelected(this);
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D497 File Offset: 0x0000B697
		public OrderSiegeMachineVM(DeploymentPoint deploymentPoint, Action<OrderSiegeMachineVM> setSelected, int keyIndex)
		{
			this.DeploymentPoint = deploymentPoint;
			this.SetSelected = setSelected;
			base.ShortcutText = keyIndex.ToString();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public void RefreshSiegeWeapon()
		{
			if (this.SiegeWeapon == null)
			{
				this.MachineType = null;
				this.MachineClass = "none";
				this.CurrentHP = 1.0;
				base.IsSelectable = false;
				base.IsSelected = false;
				return;
			}
			base.IsSelectable = (!this.SiegeWeapon.IsDestroyed && !this.SiegeWeapon.IsDeactivated);
			this.MachineType = this.SiegeWeapon.GetType();
			this.MachineClass = this.SiegeWeapon.GetSiegeEngineType().StringId;
			if (this.SiegeWeapon.DestructionComponent != null)
			{
				this.CurrentHP = (double)(this.SiegeWeapon.DestructionComponent.HitPoint / this.SiegeWeapon.DestructionComponent.MaxHitPoint);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D590 File Offset: 0x0000B790
		public static SiegeEngineType GetSiegeType(Type t, BattleSideEnum side)
		{
			if (t == typeof(SiegeLadder))
			{
				return DefaultSiegeEngineTypes.Ladder;
			}
			if (t == typeof(Ballista))
			{
				return DefaultSiegeEngineTypes.Ballista;
			}
			if (t == typeof(FireBallista))
			{
				return DefaultSiegeEngineTypes.FireBallista;
			}
			if (t == typeof(BatteringRam))
			{
				return DefaultSiegeEngineTypes.Ram;
			}
			if (t == typeof(SiegeTower))
			{
				return DefaultSiegeEngineTypes.SiegeTower;
			}
			if (t == typeof(Mangonel))
			{
				if (side != BattleSideEnum.Attacker)
				{
					return DefaultSiegeEngineTypes.Catapult;
				}
				return DefaultSiegeEngineTypes.Onager;
			}
			else if (t == typeof(FireMangonel))
			{
				if (side != BattleSideEnum.Attacker)
				{
					return DefaultSiegeEngineTypes.FireCatapult;
				}
				return DefaultSiegeEngineTypes.FireOnager;
			}
			else
			{
				if (t == typeof(Trebuchet))
				{
					return DefaultSiegeEngineTypes.Trebuchet;
				}
				if (t == typeof(FireTrebuchet))
				{
					return DefaultSiegeEngineTypes.FireTrebuchet;
				}
				Debug.FailedAssert("Invalid siege weapon", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\OrderSiegeMachineVM.cs", "GetSiegeType", 163);
				return DefaultSiegeEngineTypes.Ladder;
			}
		}

		// Token: 0x0400015C RID: 348
		public Type MachineType;

		// Token: 0x0400015D RID: 349
		public Action<OrderSiegeMachineVM> SetSelected;

		// Token: 0x0400015E RID: 350
		private string _machineClass = "";

		// Token: 0x0400015F RID: 351
		private double _currentHP;

		// Token: 0x04000160 RID: 352
		private bool _isInside;

		// Token: 0x04000161 RID: 353
		private Vec2 _position;
	}
}
