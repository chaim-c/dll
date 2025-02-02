﻿using System;
using System.Collections.Generic;
using Helpers;
using TaleWorlds.Localization;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002D4 RID: 724
	public class ClanLeaderChangedLogEntry : LogEntry, IEncyclopediaLog
	{
		// Token: 0x06002A9F RID: 10911 RVA: 0x000B5BB5 File Offset: 0x000B3DB5
		internal static void AutoGeneratedStaticCollectObjectsClanLeaderChangedLogEntry(object o, List<object> collectedObjects)
		{
			((ClanLeaderChangedLogEntry)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000B5BC3 File Offset: 0x000B3DC3
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.OldLeader);
			collectedObjects.Add(this.NewLeader);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x000B5BE4 File Offset: 0x000B3DE4
		internal static object AutoGeneratedGetMemberValueOldLeader(object o)
		{
			return ((ClanLeaderChangedLogEntry)o).OldLeader;
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000B5BF1 File Offset: 0x000B3DF1
		internal static object AutoGeneratedGetMemberValueNewLeader(object o)
		{
			return ((ClanLeaderChangedLogEntry)o).NewLeader;
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x000B5BFE File Offset: 0x000B3DFE
		public ClanLeaderChangedLogEntry(Hero oldLeader, Hero newLeader)
		{
			this.OldLeader = oldLeader;
			this.NewLeader = newLeader;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000B5C14 File Offset: 0x000B3E14
		public override string ToString()
		{
			return this.GetEncyclopediaText().ToString();
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000B5C21 File Offset: 0x000B3E21
		public bool IsVisibleInEncyclopediaPageOf<T>(T obj) where T : MBObjectBase
		{
			return obj == this.OldLeader || obj == this.NewLeader || obj == this.NewLeader.Clan;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000B5C54 File Offset: 0x000B3E54
		public TextObject GetEncyclopediaText()
		{
			TextObject textObject = new TextObject("{=o69PZuce}{OLD_LEADER.LINK} is no longer the head of the {CLAN_NAME}. {NEW_LEADER.LINK} has taken over as ruler of the clan.", null);
			textObject.SetTextVariable("CLAN_NAME", this.NewLeader.Clan.EncyclopediaLinkWithName);
			StringHelpers.SetCharacterProperties("OLD_LEADER", this.OldLeader.CharacterObject, textObject, false);
			StringHelpers.SetCharacterProperties("NEW_LEADER", this.NewLeader.CharacterObject, textObject, false);
			return textObject;
		}

		// Token: 0x04000CC9 RID: 3273
		[SaveableField(160)]
		public readonly Hero OldLeader;

		// Token: 0x04000CCA RID: 3274
		[SaveableField(161)]
		public readonly Hero NewLeader;
	}
}
