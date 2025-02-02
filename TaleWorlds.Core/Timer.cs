using System;

namespace TaleWorlds.Core
{
	// Token: 0x020000C1 RID: 193
	public class Timer
	{
		// Token: 0x1700033D RID: 829
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001FF28 File Offset: 0x0001E128
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0001FF30 File Offset: 0x0001E130
		public float Duration { get; protected set; }

		// Token: 0x060009A5 RID: 2469 RVA: 0x0001FF39 File Offset: 0x0001E139
		public Timer(float gameTime, float duration, bool autoReset = true)
		{
			this._startTime = gameTime;
			this._latestGameTime = gameTime;
			this._autoReset = autoReset;
			this.Duration = duration;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001FF60 File Offset: 0x0001E160
		public virtual bool Check(float gameTime)
		{
			this._latestGameTime = gameTime;
			if (this.Duration <= 0f)
			{
				this.PreviousDeltaTime = this.ElapsedTime();
				this._startTime = gameTime;
				return true;
			}
			bool result = false;
			if (this.ElapsedTime() >= this.Duration)
			{
				this.PreviousDeltaTime = this.ElapsedTime();
				if (this._autoReset)
				{
					while (this.ElapsedTime() >= this.Duration)
					{
						this._startTime += this.Duration;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001FFE0 File Offset: 0x0001E1E0
		public float ElapsedTime()
		{
			return this._latestGameTime - this._startTime;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x0001FFEF File Offset: 0x0001E1EF
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0001FFF7 File Offset: 0x0001E1F7
		public float PreviousDeltaTime { get; private set; }

		// Token: 0x060009AA RID: 2474 RVA: 0x00020000 File Offset: 0x0001E200
		public void Reset(float gameTime)
		{
			this.Reset(gameTime, this.Duration);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0002000F File Offset: 0x0001E20F
		public void Reset(float gameTime, float newDuration)
		{
			this._startTime = gameTime;
			this._latestGameTime = gameTime;
			this.Duration = newDuration;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00020026 File Offset: 0x0001E226
		public void AdjustStartTime(float deltaTime)
		{
			this._startTime += deltaTime;
		}

		// Token: 0x040005A1 RID: 1441
		private float _startTime;

		// Token: 0x040005A2 RID: 1442
		private float _latestGameTime;

		// Token: 0x040005A3 RID: 1443
		private bool _autoReset;
	}
}
