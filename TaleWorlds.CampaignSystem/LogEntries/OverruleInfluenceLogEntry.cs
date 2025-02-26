﻿using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002DD RID: 733
	public class OverruleInfluenceLogEntry : LogEntry
	{
		// Token: 0x06002AFE RID: 11006 RVA: 0x000B692C File Offset: 0x000B4B2C
		internal static void AutoGeneratedStaticCollectObjectsOverruleInfluenceLogEntry(object o, List<object> collectedObjects)
		{
			((OverruleInfluenceLogEntry)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000B693A File Offset: 0x000B4B3A
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._liege);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000B694F File Offset: 0x000B4B4F
		internal static object AutoGeneratedGetMemberValue_liege(object o)
		{
			return ((OverruleInfluenceLogEntry)o)._liege;
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x000B695C File Offset: 0x000B4B5C
		public override CampaignTime KeepInHistoryTime
		{
			get
			{
				return CampaignTime.Weeks(240f);
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000B6968 File Offset: 0x000B4B68
		public OverruleInfluenceLogEntry(Hero liege, Hero wrongedLord)
		{
			this._liege = liege;
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000B6977 File Offset: 0x000B4B77
		public override TextObject GetHistoricComment(Hero talkTroop)
		{
			TextObject textObject = GameTexts.FindText("str_overrule_influence_historic_comment", null);
			textObject.SetTextVariable("HERO", this._liege.Name);
			textObject.SetTextVariable("FACTION", this._liege.Clan.Name);
			return textObject;
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000B69B7 File Offset: 0x000B4BB7
		public override int GetValueAsPoliticsAbuseOfPower(Hero referenceTroop, Hero liege)
		{
			if (liege == this._liege)
			{
				return 3;
			}
			return 0;
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000B69C5 File Offset: 0x000B4BC5
		public override string ToString()
		{
			return "OverruleInfluenceLogEntry";
		}

		// Token: 0x04000CE3 RID: 3299
		[SaveableField(260)]
		private readonly Hero _liege;
	}
}
