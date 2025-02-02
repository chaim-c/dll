using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.FormationMarker
{
	// Token: 0x02000054 RID: 84
	public class MissionSiegeEngineMarkerTargetVM : ViewModel
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001AE9F File Offset: 0x0001909F
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0001AEA7 File Offset: 0x000190A7
		public SiegeWeapon Engine { get; private set; }

		// Token: 0x060006AD RID: 1709 RVA: 0x0001AEB0 File Offset: 0x000190B0
		public MissionSiegeEngineMarkerTargetVM(SiegeWeapon engine, bool isEnemy)
		{
			this.Engine = engine;
			this.EngineType = this.Engine.GetSiegeEngineType().StringId;
			this.IsEnemy = isEnemy;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001AEDC File Offset: 0x000190DC
		public void Refresh()
		{
			this.HitPoints = MathF.Ceiling(this.Engine.DestructionComponent.HitPoint / this.Engine.DestructionComponent.MaxHitPoint * 100f);
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001AF10 File Offset: 0x00019110
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0001AF18 File Offset: 0x00019118
		[DataSourceProperty]
		public bool IsEnabled
		{
			get
			{
				return this._isEnabled;
			}
			set
			{
				if (this._isEnabled != value)
				{
					this._isEnabled = value;
					base.OnPropertyChangedWithValue(value, "IsEnabled");
				}
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001AF36 File Offset: 0x00019136
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001AF3E File Offset: 0x0001913E
		[DataSourceProperty]
		public bool IsEnemy
		{
			get
			{
				return this._isEnemy;
			}
			set
			{
				if (this._isEnemy != value)
				{
					this._isEnemy = value;
					base.OnPropertyChangedWithValue(value, "IsEnemy");
				}
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001AF5C File Offset: 0x0001915C
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0001AF64 File Offset: 0x00019164
		[DataSourceProperty]
		public string EngineType
		{
			get
			{
				return this._engineType;
			}
			set
			{
				if (this._engineType != value)
				{
					this._engineType = value;
					base.OnPropertyChangedWithValue<string>(value, "EngineType");
				}
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001AF87 File Offset: 0x00019187
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0001AF8F File Offset: 0x0001918F
		[DataSourceProperty]
		public bool IsBehind
		{
			get
			{
				return this._isBehind;
			}
			set
			{
				if (this._isBehind != value)
				{
					this._isBehind = value;
					base.OnPropertyChangedWithValue(value, "IsBehind");
				}
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001AFAD File Offset: 0x000191AD
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0001AFB5 File Offset: 0x000191B5
		[DataSourceProperty]
		public Vec2 ScreenPosition
		{
			get
			{
				return this._screenPosition;
			}
			set
			{
				if (value.x != this._screenPosition.x || value.y != this._screenPosition.y)
				{
					this._screenPosition = value;
					base.OnPropertyChangedWithValue(value, "ScreenPosition");
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0001AFF0 File Offset: 0x000191F0
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0001AFF8 File Offset: 0x000191F8
		[DataSourceProperty]
		public float Distance
		{
			get
			{
				return this._distance;
			}
			set
			{
				if (this._distance != value && !float.IsNaN(value))
				{
					this._distance = value;
					base.OnPropertyChangedWithValue(value, "Distance");
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001B01E File Offset: 0x0001921E
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0001B026 File Offset: 0x00019226
		[DataSourceProperty]
		public int HitPoints
		{
			get
			{
				return this._hitPoints;
			}
			set
			{
				if (this._hitPoints != value)
				{
					this._hitPoints = value;
					base.OnPropertyChangedWithValue(value, "HitPoints");
				}
			}
		}

		// Token: 0x04000330 RID: 816
		private Vec2 _screenPosition;

		// Token: 0x04000331 RID: 817
		private float _distance;

		// Token: 0x04000332 RID: 818
		private bool _isEnabled;

		// Token: 0x04000333 RID: 819
		private bool _isBehind;

		// Token: 0x04000334 RID: 820
		private bool _isEnemy;

		// Token: 0x04000335 RID: 821
		private string _engineType;

		// Token: 0x04000336 RID: 822
		private int _hitPoints;
	}
}
