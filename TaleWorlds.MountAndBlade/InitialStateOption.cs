using System;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023A RID: 570
	public class InitialStateOption
	{
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x0006E596 File Offset: 0x0006C796
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x0006E59E File Offset: 0x0006C79E
		public int OrderIndex { get; private set; }

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001F03 RID: 7939 RVA: 0x0006E5A7 File Offset: 0x0006C7A7
		// (set) Token: 0x06001F04 RID: 7940 RVA: 0x0006E5AF File Offset: 0x0006C7AF
		public TextObject Name { get; private set; }

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001F05 RID: 7941 RVA: 0x0006E5B8 File Offset: 0x0006C7B8
		// (set) Token: 0x06001F06 RID: 7942 RVA: 0x0006E5C0 File Offset: 0x0006C7C0
		public string Id { get; private set; }

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0006E5C9 File Offset: 0x0006C7C9
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0006E5D1 File Offset: 0x0006C7D1
		public Func<ValueTuple<bool, TextObject>> IsDisabledAndReason { get; private set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0006E5DA File Offset: 0x0006C7DA
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x0006E5E2 File Offset: 0x0006C7E2
		public TextObject EnabledHint { get; private set; }

		// Token: 0x06001F0B RID: 7947 RVA: 0x0006E5EC File Offset: 0x0006C7EC
		public InitialStateOption(string id, TextObject name, int orderIndex, Action action, Func<ValueTuple<bool, TextObject>> isDisabledAndReason, TextObject enabledHint = null)
		{
			this.Name = name;
			this.Id = id;
			this.OrderIndex = orderIndex;
			this._action = action;
			this.IsDisabledAndReason = isDisabledAndReason;
			this.EnabledHint = enabledHint;
			TextObject item = this.IsDisabledAndReason().Item2;
			string.IsNullOrEmpty((item != null) ? item.ToString() : null);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0006E64E File Offset: 0x0006C84E
		public void DoAction()
		{
			Action action = this._action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x04000B67 RID: 2919
		private Action _action;
	}
}
