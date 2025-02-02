using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000219 RID: 537
	public struct FactoredNumber
	{
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00067467 File Offset: 0x00065667
		public float ResultNumber
		{
			get
			{
				return MathF.Clamp(this.BaseNumber + this.BaseNumber * this._sumOfFactors, this.LimitMinValue, this.LimitMaxValue);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0006748E File Offset: 0x0006568E
		// (set) Token: 0x06001D70 RID: 7536 RVA: 0x00067496 File Offset: 0x00065696
		public float BaseNumber { get; private set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0006749F File Offset: 0x0006569F
		public float LimitMinValue
		{
			get
			{
				return this._limitMinValue;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000674A8 File Offset: 0x000656A8
		public float LimitMaxValue
		{
			get
			{
				return this._limitMaxValue;
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000674B1 File Offset: 0x000656B1
		public FactoredNumber(float baseNumber = 0f)
		{
			this.BaseNumber = baseNumber;
			this._sumOfFactors = 0f;
			this._limitMinValue = float.MinValue;
			this._limitMaxValue = float.MaxValue;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000674DB File Offset: 0x000656DB
		public void Add(float value)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this.BaseNumber += value;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000674FE File Offset: 0x000656FE
		public void AddFactor(float value)
		{
			if (value.ApproximatelyEqualsTo(0f, 1E-05f))
			{
				return;
			}
			this._sumOfFactors += value;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00067521 File Offset: 0x00065721
		public void LimitMin(float minValue)
		{
			this._limitMinValue = minValue;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0006752A File Offset: 0x0006572A
		public void LimitMax(float maxValue)
		{
			this._limitMaxValue = maxValue;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00067533 File Offset: 0x00065733
		public void Clamp(float minValue, float maxValue)
		{
			this.LimitMin(minValue);
			this.LimitMax(maxValue);
		}

		// Token: 0x040009A7 RID: 2471
		private float _limitMinValue;

		// Token: 0x040009A8 RID: 2472
		private float _limitMaxValue;

		// Token: 0x040009A9 RID: 2473
		private float _sumOfFactors;
	}
}
