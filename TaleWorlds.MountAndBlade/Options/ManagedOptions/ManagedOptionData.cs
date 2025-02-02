using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options.ManagedOptions
{
	// Token: 0x02000384 RID: 900
	public abstract class ManagedOptionData : IOptionData
	{
		// Token: 0x06003161 RID: 12641 RVA: 0x000CC09C File Offset: 0x000CA29C
		protected ManagedOptionData(ManagedOptions.ManagedOptionsType type)
		{
			this.Type = type;
			this._value = ManagedOptions.GetConfig(type);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000CC0B7 File Offset: 0x000CA2B7
		public virtual float GetDefaultValue()
		{
			return ManagedOptions.GetDefaultConfig(this.Type);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000CC0C4 File Offset: 0x000CA2C4
		public void Commit()
		{
			if (this._value != ManagedOptions.GetConfig(this.Type))
			{
				ManagedOptions.SetConfig(this.Type, this._value);
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000CC0EA File Offset: 0x000CA2EA
		public float GetValue(bool forceRefresh)
		{
			if (forceRefresh)
			{
				this._value = ManagedOptions.GetConfig(this.Type);
			}
			return this._value;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000CC106 File Offset: 0x000CA306
		public void SetValue(float value)
		{
			this._value = value;
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000CC10F File Offset: 0x000CA30F
		public object GetOptionType()
		{
			return this.Type;
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000CC11C File Offset: 0x000CA31C
		public bool IsNative()
		{
			return false;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000CC11F File Offset: 0x000CA31F
		public bool IsAction()
		{
			return false;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000CC124 File Offset: 0x000CA324
		public ValueTuple<string, bool> GetIsDisabledAndReasonID()
		{
			ManagedOptions.ManagedOptionsType type = this.Type;
			if (type - ManagedOptions.ManagedOptionsType.ControlBlockDirection <= 1 && BannerlordConfig.GyroOverrideForAttackDefend)
			{
				return new ValueTuple<string, bool>("str_gyro_overrides_attack_block_direction", true);
			}
			return new ValueTuple<string, bool>(string.Empty, false);
		}

		// Token: 0x04001526 RID: 5414
		public readonly ManagedOptions.ManagedOptionsType Type;

		// Token: 0x04001527 RID: 5415
		private float _value;
	}
}
