﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002CE RID: 718
	public class CharacterBornLogEntry : LogEntry, IEncyclopediaLog, IChatNotification
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x000B4B5A File Offset: 0x000B2D5A
		internal static void AutoGeneratedStaticCollectObjectsCharacterBornLogEntry(object o, List<object> collectedObjects)
		{
			((CharacterBornLogEntry)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000B4B68 File Offset: 0x000B2D68
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.BornCharacter);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000B4B7D File Offset: 0x000B2D7D
		internal static object AutoGeneratedGetMemberValueBornCharacter(object o)
		{
			return ((CharacterBornLogEntry)o).BornCharacter;
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002A49 RID: 10825 RVA: 0x000B4B8A File Offset: 0x000B2D8A
		public bool IsVisibleNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x000B4B8D File Offset: 0x000B2D8D
		public override CampaignTime KeepInHistoryTime
		{
			get
			{
				return CampaignTime.Years((float)(Campaign.Current.Models.AgeModel.BecomeOldAge / 2));
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000B4BAB File Offset: 0x000B2DAB
		public CharacterBornLogEntry(Hero bornCharacter)
		{
			this.BornCharacter = bornCharacter;
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000B4BBA File Offset: 0x000B2DBA
		public override ChatNotificationType NotificationType
		{
			get
			{
				return base.PoliticalNotification(this.BornCharacter.Clan);
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000B4BCD File Offset: 0x000B2DCD
		public override string ToString()
		{
			return this.GetEncyclopediaText().ToString();
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000B4BDA File Offset: 0x000B2DDA
		public TextObject GetNotificationText()
		{
			return this.GetEncyclopediaText();
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000B4BE2 File Offset: 0x000B2DE2
		public bool IsVisibleInEncyclopediaPageOf<T>(T obj) where T : MBObjectBase
		{
			return obj == this.BornCharacter;
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000B4BF4 File Offset: 0x000B2DF4
		public TextObject GetEncyclopediaText()
		{
			TextObject textObject = GameTexts.FindText("str_notification_character_born", null);
			StringHelpers.SetCharacterProperties("HERO", this.BornCharacter.CharacterObject, textObject, false);
			StringHelpers.SetCharacterProperties("MOTHER", this.BornCharacter.Mother.CharacterObject, textObject, false);
			StringHelpers.SetCharacterProperties("FATHER", this.BornCharacter.Father.CharacterObject, textObject, false);
			return textObject;
		}

		// Token: 0x04000CB5 RID: 3253
		[SaveableField(100)]
		public readonly Hero BornCharacter;
	}
}
