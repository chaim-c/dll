﻿using System;
using System.Collections.Generic;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.LogEntries
{
	// Token: 0x020002C5 RID: 709
	public class LogEntryHistory
	{
		// Token: 0x060029D5 RID: 10709 RVA: 0x000B35C8 File Offset: 0x000B17C8
		internal static void AutoGeneratedStaticCollectObjectsLogEntryHistory(object o, List<object> collectedObjects)
		{
			((LogEntryHistory)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x000B35D6 File Offset: 0x000B17D6
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this._logs);
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x000B35E4 File Offset: 0x000B17E4
		internal static object AutoGeneratedGetMemberValueLastAddedIndex(object o)
		{
			return ((LogEntryHistory)o).LastAddedIndex;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x000B35F6 File Offset: 0x000B17F6
		internal static object AutoGeneratedGetMemberValue_logs(object o)
		{
			return ((LogEntryHistory)o)._logs;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x000B3604 File Offset: 0x000B1804
		internal void AddActionLog(LogEntry actionLog, bool checkSequenceAndInsert = false)
		{
			if (this._logs.Count > 0 && this._logs[this._logs.Count - 1].Id > actionLog.Id)
			{
				Debug.FailedAssert("Log ids should always get bigger", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.CampaignSystem\\LogEntries\\LogEntry.cs", "AddActionLog", 209);
				int num = this._logs.FindIndex((LogEntry l) => l.Id > actionLog.Id);
				if (num >= 0)
				{
					this._logs.Insert(num, actionLog);
				}
			}
			else
			{
				this._logs.Add(actionLog);
			}
			Campaign.Current.CampaignInformationManager.NewLogEntryAdded(actionLog);
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x000B36C5 File Offset: 0x000B18C5
		internal void DeleteLogAtIndex(int i)
		{
			this._logs.RemoveAt(i);
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x000B36D4 File Offset: 0x000B18D4
		public void DeleteOutdatedLogs()
		{
			int num = -1;
			for (int i = 0; i < this._logs.Count; i++)
			{
				LogEntry logEntry = this._logs[i];
				if (!(logEntry.KeepInHistoryTime + logEntry.GameTime).IsPast)
				{
					num++;
					if (i != num)
					{
						this._logs[num] = logEntry;
					}
				}
			}
			if (num < this._logs.Count)
			{
				this._logs.RemoveRange(num + 1, this._logs.Count - num - 1);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000B3761 File Offset: 0x000B1961
		public MBReadOnlyList<LogEntry> GameActionLogs
		{
			get
			{
				return this._logs;
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000B3769 File Offset: 0x000B1969
		public IEnumerable<T> GetGameActionLogs<T>(Func<T, bool> predicate) where T : LogEntry
		{
			using (List<LogEntry>.Enumerator enumerator = this._logs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					T t;
					if ((t = (enumerator.Current as T)) != null && predicate(t))
					{
						yield return t;
					}
				}
			}
			List<LogEntry>.Enumerator enumerator = default(List<LogEntry>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000B3780 File Offset: 0x000B1980
		public T FindLastGameActionLog<T>(Func<T, bool> predicate) where T : LogEntry
		{
			for (int i = this._logs.Count - 1; i >= 0; i--)
			{
				T t;
				if ((t = (this._logs[i] as T)) != null && predicate(t))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x000B37D8 File Offset: 0x000B19D8
		private int BinarySearchFirst(long target)
		{
			int i = 0;
			int num = this._logs.Count - 1;
			while (i < num)
			{
				int num2 = (i + num + 1) / 2;
				if (this._logs[num2].Id > target)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2;
				}
			}
			return num;
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000B3820 File Offset: 0x000B1A20
		private int BinarySearchFirst(CampaignTime target)
		{
			int i = 0;
			int num = this._logs.Count - 1;
			while (i < num)
			{
				int num2 = (i + num + 1) / 2;
				if (this._logs[num2].GameTime > target)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2;
				}
			}
			return num;
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x000B3870 File Offset: 0x000B1A70
		public int GetStartIndexForComments()
		{
			for (int i = MathF.Max(0, this._logs.Count - 10000); i < this._logs.Count; i++)
			{
				if (this._logs[i].Id > Hero.OneToOneConversationHero.LastExaminedLogEntryID)
				{
					return i;
				}
			}
			return this._logs.Count;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x000B38D4 File Offset: 0x000B1AD4
		public LogEntry GetRelevantComment(Hero conversationHero, out int bestScore, out string bestRelatedLogEntryTag)
		{
			int num = -1;
			bestRelatedLogEntryTag = "";
			int startIndexForComments = this.GetStartIndexForComments();
			bestScore = 0;
			if (startIndexForComments < this._logs.Count)
			{
				for (int i = startIndexForComments; i < this._logs.Count; i++)
				{
					string text;
					ImportanceEnum importanceEnum;
					this._logs[i].GetConversationScoreAndComment(conversationHero, false, out text, out importanceEnum);
					int num2 = (int)importanceEnum;
					if (num2 > bestScore)
					{
						bestScore = num2;
						num = i;
					}
				}
				Hero.OneToOneConversationHero.LastExaminedLogEntryID = this._logs[this._logs.Count - 1].Id;
			}
			if (num > -1)
			{
				string text2;
				ImportanceEnum importanceEnum2;
				this._logs[num].GetConversationScoreAndComment(conversationHero, true, out text2, out importanceEnum2);
				bestRelatedLogEntryTag = text2;
				return this._logs[num];
			}
			return null;
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x000B3994 File Offset: 0x000B1B94
		internal void OnAfterLoad()
		{
			for (int i = this._logs.Count - 1; i >= 0; i--)
			{
				if (this._logs[i] == null)
				{
					this._logs.RemoveAt(i);
				}
			}
		}

		// Token: 0x04000C96 RID: 3222
		[SaveableField(0)]
		internal long LastAddedIndex = -1L;

		// Token: 0x04000C97 RID: 3223
		[SaveableField(1)]
		private readonly MBList<LogEntry> _logs = new MBList<LogEntry>();
	}
}
