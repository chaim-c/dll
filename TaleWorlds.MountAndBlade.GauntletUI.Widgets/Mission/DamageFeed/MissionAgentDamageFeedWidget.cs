using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.DamageFeed
{
	// Token: 0x020000F5 RID: 245
	public class MissionAgentDamageFeedWidget : Widget
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x0002414A File Offset: 0x0002234A
		public MissionAgentDamageFeedWidget(UIContext context) : base(context)
		{
			this._feedItemQueue = new Queue<MissionAgentDamageFeedItemWidget>();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00024168 File Offset: 0x00022368
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			MissionAgentDamageFeedItemWidget item = (MissionAgentDamageFeedItemWidget)child;
			this._feedItemQueue.Enqueue(item);
			this.UpdateSpeedModifiers();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00024195 File Offset: 0x00022395
		protected override void OnChildRemoved(Widget child)
		{
			this._activeFeedItem = null;
			base.OnChildRemoved(child);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x000241A8 File Offset: 0x000223A8
		protected override void OnLateUpdate(float dt)
		{
			if (this._activeFeedItem == null && this._feedItemQueue.Count > 0)
			{
				MissionAgentDamageFeedItemWidget activeFeedItem = this._feedItemQueue.Dequeue();
				this._activeFeedItem = activeFeedItem;
				this._activeFeedItem.ShowFeed();
			}
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000241EC File Offset: 0x000223EC
		private void UpdateSpeedModifiers()
		{
			if (base.ChildCount > this._speedUpWidgetLimit)
			{
				float speedModifier = (float)(base.ChildCount - this._speedUpWidgetLimit) / 3f + 1f;
				for (int i = 0; i < base.ChildCount - this._speedUpWidgetLimit; i++)
				{
					((MissionAgentDamageFeedItemWidget)base.GetChild(i)).SetSpeedModifier(speedModifier);
				}
			}
		}

		// Token: 0x040005FC RID: 1532
		private int _speedUpWidgetLimit = 1;

		// Token: 0x040005FD RID: 1533
		private readonly Queue<MissionAgentDamageFeedItemWidget> _feedItemQueue;

		// Token: 0x040005FE RID: 1534
		private MissionAgentDamageFeedItemWidget _activeFeedItem;
	}
}
