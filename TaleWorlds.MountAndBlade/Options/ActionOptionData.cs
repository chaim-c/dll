using System;
using TaleWorlds.Engine.Options;

namespace TaleWorlds.MountAndBlade.Options
{
	// Token: 0x0200037E RID: 894
	public class ActionOptionData : IOptionData
	{
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000CBBC7 File Offset: 0x000C9DC7
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x000CBBCF File Offset: 0x000C9DCF
		public Action OnAction { get; private set; }

		// Token: 0x06003130 RID: 12592 RVA: 0x000CBBD8 File Offset: 0x000C9DD8
		public ActionOptionData(ManagedOptions.ManagedOptionsType managedType, Action onAction)
		{
			this._managedType = managedType;
			this.OnAction = onAction;
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000CBBEE File Offset: 0x000C9DEE
		public ActionOptionData(NativeOptions.NativeOptionsType nativeType, Action onAction)
		{
			this._nativeType = nativeType;
			this.OnAction = onAction;
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000CBC04 File Offset: 0x000C9E04
		public ActionOptionData(string optionTypeId, Action onAction)
		{
			this._actionOptionTypeId = optionTypeId;
			this._nativeType = NativeOptions.NativeOptionsType.None;
			this.OnAction = onAction;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000CBC21 File Offset: 0x000C9E21
		public void Commit()
		{
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000CBC23 File Offset: 0x000C9E23
		public float GetDefaultValue()
		{
			return 0f;
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000CBC2A File Offset: 0x000C9E2A
		public object GetOptionType()
		{
			if (this._nativeType != NativeOptions.NativeOptionsType.None)
			{
				return this._nativeType;
			}
			if (this._managedType != ManagedOptions.ManagedOptionsType.Language)
			{
				return this._managedType;
			}
			return this._actionOptionTypeId;
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000CBC5B File Offset: 0x000C9E5B
		public float GetValue(bool forceRefresh)
		{
			return 0f;
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000CBC62 File Offset: 0x000C9E62
		public bool IsNative()
		{
			return this._nativeType != NativeOptions.NativeOptionsType.None;
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000CBC70 File Offset: 0x000C9E70
		public void SetValue(float value)
		{
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000CBC72 File Offset: 0x000C9E72
		public bool IsAction()
		{
			return this._nativeType == NativeOptions.NativeOptionsType.None && this._managedType == ManagedOptions.ManagedOptionsType.Language;
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000CBC88 File Offset: 0x000C9E88
		public ValueTuple<string, bool> GetIsDisabledAndReasonID()
		{
			return new ValueTuple<string, bool>(string.Empty, false);
		}

		// Token: 0x04001519 RID: 5401
		private ManagedOptions.ManagedOptionsType _managedType;

		// Token: 0x0400151A RID: 5402
		private NativeOptions.NativeOptionsType _nativeType;

		// Token: 0x0400151B RID: 5403
		private string _actionOptionTypeId;
	}
}
