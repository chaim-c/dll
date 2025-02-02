using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options.ManagedOptions
{
	// Token: 0x02000383 RID: 899
	public class ManagedNumericOptionData : ManagedOptionData, INumericOptionData, IOptionData
	{
		// Token: 0x0600315A RID: 12634 RVA: 0x000CBF7C File Offset: 0x000CA17C
		public ManagedNumericOptionData(ManagedOptions.ManagedOptionsType type) : base(type)
		{
			this._minValue = ManagedNumericOptionData.GetLimitValue(this.Type, true);
			this._maxValue = ManagedNumericOptionData.GetLimitValue(this.Type, false);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000CBFA9 File Offset: 0x000CA1A9
		public float GetMinValue()
		{
			return this._minValue;
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x000CBFB1 File Offset: 0x000CA1B1
		public float GetMaxValue()
		{
			return this._maxValue;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000CBFBC File Offset: 0x000CA1BC
		private static float GetLimitValue(ManagedOptions.ManagedOptionsType type, bool isMin)
		{
			if (type <= ManagedOptions.ManagedOptionsType.AutoSaveInterval)
			{
				if (type == ManagedOptions.ManagedOptionsType.BattleSize)
				{
					return (float)(isMin ? BannerlordConfig.MinBattleSize : BannerlordConfig.MaxBattleSize);
				}
				if (type == ManagedOptions.ManagedOptionsType.AutoSaveInterval)
				{
					if (!isMin)
					{
						return 60f;
					}
					return 4f;
				}
			}
			else if (type != ManagedOptions.ManagedOptionsType.FirstPersonFov)
			{
				if (type != ManagedOptions.ManagedOptionsType.CombatCameraDistance)
				{
					if (type == ManagedOptions.ManagedOptionsType.UIScale)
					{
						if (!isMin)
						{
							return 1f;
						}
						return 0.75f;
					}
				}
				else
				{
					if (!isMin)
					{
						return 2.4f;
					}
					return 0.7f;
				}
			}
			else
			{
				if (!isMin)
				{
					return 100f;
				}
				return 45f;
			}
			if (!isMin)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000CC048 File Offset: 0x000CA248
		public bool GetIsDiscrete()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			if (type <= ManagedOptions.ManagedOptionsType.AutoSaveInterval)
			{
				if (type != ManagedOptions.ManagedOptionsType.BattleSize && type != ManagedOptions.ManagedOptionsType.AutoSaveInterval)
				{
					return false;
				}
			}
			else if (type != ManagedOptions.ManagedOptionsType.FirstPersonFov)
			{
				if (type != ManagedOptions.ManagedOptionsType.UIScale)
				{
					return false;
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000CC07B File Offset: 0x000CA27B
		public int GetDiscreteIncrementInterval()
		{
			return 1;
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000CC080 File Offset: 0x000CA280
		public bool GetShouldUpdateContinuously()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			return type != ManagedOptions.ManagedOptionsType.UIScale;
		}

		// Token: 0x04001524 RID: 5412
		private readonly float _minValue;

		// Token: 0x04001525 RID: 5413
		private readonly float _maxValue;
	}
}
