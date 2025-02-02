using System;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x020000A2 RID: 162
	public class NativeNumericOptionData : NativeOptionData, INumericOptionData, IOptionData
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0000D360 File Offset: 0x0000B560
		public NativeNumericOptionData(NativeOptions.NativeOptionsType type) : base(type)
		{
			this._minValue = NativeNumericOptionData.GetLimitValue(this.Type, true);
			this._maxValue = NativeNumericOptionData.GetLimitValue(this.Type, false);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0000D38D File Offset: 0x0000B58D
		public float GetMinValue()
		{
			return this._minValue;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0000D395 File Offset: 0x0000B595
		public float GetMaxValue()
		{
			return this._maxValue;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		private static float GetLimitValue(NativeOptions.NativeOptionsType type, bool isMin)
		{
			if (type <= NativeOptions.NativeOptionsType.Brightness)
			{
				switch (type)
				{
				case NativeOptions.NativeOptionsType.MouseSensitivity:
					if (!isMin)
					{
						return 1f;
					}
					return 0.3f;
				case NativeOptions.NativeOptionsType.InvertMouseYAxis:
				case NativeOptions.NativeOptionsType.EnableVibration:
				case NativeOptions.NativeOptionsType.EnableGyroAssistedAim:
					break;
				case NativeOptions.NativeOptionsType.MouseYMovementScale:
					if (!isMin)
					{
						return 4f;
					}
					return 0.25f;
				case NativeOptions.NativeOptionsType.TrailAmount:
					if (!isMin)
					{
						return 1f;
					}
					return 0f;
				case NativeOptions.NativeOptionsType.GyroAimSensitivity:
					if (!isMin)
					{
						return 1f;
					}
					return 0f;
				default:
					switch (type)
					{
					case NativeOptions.NativeOptionsType.ResolutionScale:
						if (!isMin)
						{
							return 100f;
						}
						return 50f;
					case NativeOptions.NativeOptionsType.FrameLimiter:
						if (!isMin)
						{
							return 360f;
						}
						return 30f;
					case NativeOptions.NativeOptionsType.Brightness:
						if (!isMin)
						{
							return 100f;
						}
						return 0f;
					}
					break;
				}
			}
			else if (type != NativeOptions.NativeOptionsType.SharpenAmount)
			{
				switch (type)
				{
				case NativeOptions.NativeOptionsType.BrightnessMin:
					if (!isMin)
					{
						return 0.3f;
					}
					return 0f;
				case NativeOptions.NativeOptionsType.BrightnessMax:
					if (!isMin)
					{
						return 1f;
					}
					return 0.7f;
				case NativeOptions.NativeOptionsType.ExposureCompensation:
					if (!isMin)
					{
						return 2f;
					}
					return -2f;
				case NativeOptions.NativeOptionsType.DynamicResolutionTarget:
					if (!isMin)
					{
						return 240f;
					}
					return 30f;
				}
			}
			else
			{
				if (!isMin)
				{
					return 100f;
				}
				return 0f;
			}
			if (!isMin)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		public bool GetIsDiscrete()
		{
			NativeOptions.NativeOptionsType type = this.Type;
			if (type <= NativeOptions.NativeOptionsType.Brightness)
			{
				if (type - NativeOptions.NativeOptionsType.ResolutionScale > 1 && type != NativeOptions.NativeOptionsType.Brightness)
				{
					return false;
				}
			}
			else if (type != NativeOptions.NativeOptionsType.SharpenAmount)
			{
				switch (type)
				{
				case NativeOptions.NativeOptionsType.BrightnessMin:
				case NativeOptions.NativeOptionsType.BrightnessMax:
				case NativeOptions.NativeOptionsType.ExposureCompensation:
				case NativeOptions.NativeOptionsType.DynamicResolutionTarget:
					break;
				case NativeOptions.NativeOptionsType.BrightnessCalibrated:
				case NativeOptions.NativeOptionsType.DynamicResolution:
					return false;
				default:
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0000D53C File Offset: 0x0000B73C
		public int GetDiscreteIncrementInterval()
		{
			NativeOptions.NativeOptionsType type = this.Type;
			if (type == NativeOptions.NativeOptionsType.SharpenAmount)
			{
				return 5;
			}
			return 1;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0000D558 File Offset: 0x0000B758
		public bool GetShouldUpdateContinuously()
		{
			return true;
		}

		// Token: 0x04000200 RID: 512
		private readonly float _minValue;

		// Token: 0x04000201 RID: 513
		private readonly float _maxValue;
	}
}
