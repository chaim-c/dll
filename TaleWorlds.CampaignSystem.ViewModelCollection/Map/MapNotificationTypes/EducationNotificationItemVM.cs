using System;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200003C RID: 60
	public class EducationNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x0001B828 File Offset: 0x00019A28
		public EducationNotificationItemVM(EducationMapNotification data) : base(data)
		{
			base.NotificationIdentifier = "education";
			base.ForceInspection = true;
			this._child = data.Child;
			this._age = data.Age;
			this._onInspect = new Action(this.OnInspect);
			CampaignEvents.ChildEducationCompletedEvent.AddNonSerializedListener(this, new Action<Hero, int>(this.OnEducationCompletedForChild));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001B890 File Offset: 0x00019A90
		private void OnInspect()
		{
			EducationMapNotification educationMapNotification = (EducationMapNotification)base.Data;
			if (educationMapNotification != null && !educationMapNotification.IsValid())
			{
				InformationManager.ShowInquiry(new InquiryData("", new TextObject("{=wGWYNYYX}This education stage is no longer relevant.", null).ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", null, null, "", 0f, null, null, null), false, false);
				base.ExecuteRemove();
				return;
			}
			Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<EducationState>(new object[]
			{
				this._child
			}), 0);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001B930 File Offset: 0x00019B30
		private void OnEducationCompletedForChild(Hero child, int age)
		{
			if (child == this._child && age >= this._age)
			{
				base.ExecuteRemove();
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001B94A File Offset: 0x00019B4A
		public override void OnFinalize()
		{
			base.OnFinalize();
			CampaignEvents.ChildEducationCompletedEvent.ClearListeners(this);
		}

		// Token: 0x04000248 RID: 584
		private readonly Hero _child;

		// Token: 0x04000249 RID: 585
		private readonly int _age;
	}
}
