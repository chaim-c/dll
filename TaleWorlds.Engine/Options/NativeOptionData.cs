using System;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x020000A3 RID: 163
	public abstract class NativeOptionData : IOptionData
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0000D55B File Offset: 0x0000B75B
		protected NativeOptionData(NativeOptions.NativeOptionsType type)
		{
			this.Type = type;
			this._value = NativeOptions.GetConfig(type);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0000D576 File Offset: 0x0000B776
		public virtual float GetDefaultValue()
		{
			return NativeOptions.GetDefaultConfig(this.Type);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0000D583 File Offset: 0x0000B783
		public void Commit()
		{
			NativeOptions.SetConfig(this.Type, this._value);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0000D596 File Offset: 0x0000B796
		public float GetValue(bool forceRefresh)
		{
			if (forceRefresh)
			{
				this._value = NativeOptions.GetConfig(this.Type);
			}
			return this._value;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0000D5B2 File Offset: 0x0000B7B2
		public void SetValue(float value)
		{
			this._value = value;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0000D5BB File Offset: 0x0000B7BB
		public object GetOptionType()
		{
			return this.Type;
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0000D5C8 File Offset: 0x0000B7C8
		public bool IsNative()
		{
			return true;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0000D5CB File Offset: 0x0000B7CB
		public bool IsAction()
		{
			return false;
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
		public ValueTuple<string, bool> GetIsDisabledAndReasonID()
		{
			NativeOptions.NativeOptionsType type = this.Type;
			if (type <= NativeOptions.NativeOptionsType.ResolutionScale)
			{
				if (type != NativeOptions.NativeOptionsType.GyroAimSensitivity)
				{
					if (type == NativeOptions.NativeOptionsType.ResolutionScale)
					{
						if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DLSS) != 0f)
						{
							return new ValueTuple<string, bool>("str_dlss_enabled", true);
						}
						if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DynamicResolution) != 0f)
						{
							return new ValueTuple<string, bool>("str_dynamic_resolution_enabled", true);
						}
					}
				}
				else if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.EnableGyroAssistedAim) != 1f)
				{
					return new ValueTuple<string, bool>("str_gyro_disabled", true);
				}
			}
			else if (type != NativeOptions.NativeOptionsType.DLSS)
			{
				if (type != NativeOptions.NativeOptionsType.DynamicResolution)
				{
					if (type == NativeOptions.NativeOptionsType.DynamicResolutionTarget)
					{
						if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DynamicResolution) == 0f)
						{
							return new ValueTuple<string, bool>("str_dynamic_resolution_disabled", true);
						}
						if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DLSS) != 0f)
						{
							return new ValueTuple<string, bool>("str_dlss_enabled", true);
						}
					}
				}
				else if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.DLSS) != 0f)
				{
					return new ValueTuple<string, bool>("str_dlss_enabled", true);
				}
			}
			else if (!NativeOptions.GetIsDLSSAvailable())
			{
				return new ValueTuple<string, bool>("str_dlss_not_available", true);
			}
			return new ValueTuple<string, bool>(string.Empty, false);
		}

		// Token: 0x04000202 RID: 514
		public readonly NativeOptions.NativeOptionsType Type;

		// Token: 0x04000203 RID: 515
		private float _value;
	}
}
