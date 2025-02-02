using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000176 RID: 374
	public class QueryData<T> : IQueryData
	{
		// Token: 0x060012F0 RID: 4848 RVA: 0x00047A94 File Offset: 0x00045C94
		public QueryData(Func<T> valueFunc, float lifetime)
		{
			this._cachedValue = default(T);
			this._expireTime = 0f;
			this._lifetime = lifetime;
			this._valueFunc = valueFunc;
			this._syncGroup = null;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00047AC8 File Offset: 0x00045CC8
		public QueryData(Func<T> valueFunc, float lifetime, T defaultCachedValue)
		{
			this._cachedValue = defaultCachedValue;
			this._expireTime = 0f;
			this._lifetime = lifetime;
			this._valueFunc = valueFunc;
			this._syncGroup = null;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00047AF7 File Offset: 0x00045CF7
		public void Evaluate(float currentTime)
		{
			this.SetValue(this._valueFunc(), currentTime);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x00047B0B File Offset: 0x00045D0B
		public void SetValue(T value, float currentTime)
		{
			this._cachedValue = value;
			this._expireTime = currentTime + this._lifetime;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00047B22 File Offset: 0x00045D22
		public T GetCachedValue()
		{
			return this._cachedValue;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00047B2A File Offset: 0x00045D2A
		public T GetCachedValueWithMaxAge(float age)
		{
			if (Mission.Current.CurrentTime > this._expireTime - this._lifetime + MathF.Min(this._lifetime, age))
			{
				this.Expire();
				return this.Value;
			}
			return this._cachedValue;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x00047B68 File Offset: 0x00045D68
		public T Value
		{
			get
			{
				float currentTime = Mission.Current.CurrentTime;
				if (currentTime >= this._expireTime)
				{
					if (this._syncGroup != null)
					{
						IQueryData[] syncGroup = this._syncGroup;
						for (int i = 0; i < syncGroup.Length; i++)
						{
							syncGroup[i].Evaluate(currentTime);
						}
					}
					this.Evaluate(currentTime);
				}
				return this._cachedValue;
			}
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00047BBC File Offset: 0x00045DBC
		public void Expire()
		{
			this._expireTime = 0f;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00047BCC File Offset: 0x00045DCC
		public static void SetupSyncGroup(params IQueryData[] groupItems)
		{
			for (int i = 0; i < groupItems.Length; i++)
			{
				groupItems[i].SetSyncGroup(groupItems);
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00047BF2 File Offset: 0x00045DF2
		public void SetSyncGroup(IQueryData[] syncGroup)
		{
			this._syncGroup = syncGroup;
		}

		// Token: 0x0400051C RID: 1308
		private T _cachedValue;

		// Token: 0x0400051D RID: 1309
		private float _expireTime;

		// Token: 0x0400051E RID: 1310
		private readonly float _lifetime;

		// Token: 0x0400051F RID: 1311
		private readonly Func<T> _valueFunc;

		// Token: 0x04000520 RID: 1312
		private IQueryData[] _syncGroup;
	}
}
