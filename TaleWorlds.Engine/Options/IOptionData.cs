using System;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x0200009E RID: 158
	public interface IOptionData
	{
		// Token: 0x06000BE0 RID: 3040
		float GetDefaultValue();

		// Token: 0x06000BE1 RID: 3041
		void Commit();

		// Token: 0x06000BE2 RID: 3042
		float GetValue(bool forceRefresh);

		// Token: 0x06000BE3 RID: 3043
		void SetValue(float value);

		// Token: 0x06000BE4 RID: 3044
		object GetOptionType();

		// Token: 0x06000BE5 RID: 3045
		bool IsNative();

		// Token: 0x06000BE6 RID: 3046
		bool IsAction();

		// Token: 0x06000BE7 RID: 3047
		ValueTuple<string, bool> GetIsDisabledAndReasonID();
	}
}
