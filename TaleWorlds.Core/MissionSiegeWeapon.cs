using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000AF RID: 175
	public class MissionSiegeWeapon : IMissionSiegeWeapon
	{
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001D083 File Offset: 0x0001B283
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001D08B File Offset: 0x0001B28B
		public SiegeEngineType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001D093 File Offset: 0x0001B293
		public float Health
		{
			get
			{
				return this._health;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001D09B File Offset: 0x0001B29B
		public float InitialHealth
		{
			get
			{
				return this._initialHealth;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001D0A3 File Offset: 0x0001B2A3
		public float MaxHealth
		{
			get
			{
				return this._maxHealth;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001D0AB File Offset: 0x0001B2AB
		private MissionSiegeWeapon(int index, SiegeEngineType type, float health, float maxHealth)
		{
			this._index = index;
			this._type = type;
			this._initialHealth = health;
			this._health = this._initialHealth;
			this._maxHealth = maxHealth;
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001D0DC File Offset: 0x0001B2DC
		public static MissionSiegeWeapon CreateDefaultWeapon(SiegeEngineType type)
		{
			return new MissionSiegeWeapon(-1, type, (float)type.BaseHitPoints, (float)type.BaseHitPoints);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001D0F3 File Offset: 0x0001B2F3
		public static MissionSiegeWeapon CreateCampaignWeapon(SiegeEngineType type, int index, float health, float maxHealth)
		{
			return new MissionSiegeWeapon(index, type, health, maxHealth);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001D0FE File Offset: 0x0001B2FE
		public void SetHealth(float health)
		{
			this._health = health;
		}

		// Token: 0x040004E3 RID: 1251
		private float _health;

		// Token: 0x040004E4 RID: 1252
		private readonly int _index;

		// Token: 0x040004E5 RID: 1253
		private readonly SiegeEngineType _type;

		// Token: 0x040004E6 RID: 1254
		private readonly float _initialHealth;

		// Token: 0x040004E7 RID: 1255
		private readonly float _maxHealth;
	}
}
