using System;
using TaleWorlds.Library;

namespace SandBox.ViewModelCollection.Nameplate
{
	// Token: 0x02000018 RID: 24
	public class SettlementNameplateEventItemVM : ViewModel
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000BB39 File Offset: 0x00009D39
		public SettlementNameplateEventItemVM(SettlementNameplateEventItemVM.SettlementEventType eventType)
		{
			this.EventType = eventType;
			this.Type = (int)eventType;
			this.AdditionalParameters = "";
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000BB5A File Offset: 0x00009D5A
		public SettlementNameplateEventItemVM(string productionIconId = "")
		{
			this.EventType = SettlementNameplateEventItemVM.SettlementEventType.Production;
			this.Type = (int)this.EventType;
			this.AdditionalParameters = productionIconId;
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000BB7C File Offset: 0x00009D7C
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000BB84 File Offset: 0x00009D84
		[DataSourceProperty]
		public int Type
		{
			get
			{
				return this._type;
			}
			set
			{
				if (value != this._type)
				{
					this._type = value;
					base.OnPropertyChangedWithValue(value, "Type");
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000BBA2 File Offset: 0x00009DA2
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000BBAA File Offset: 0x00009DAA
		[DataSourceProperty]
		public string AdditionalParameters
		{
			get
			{
				return this._additionalParameters;
			}
			set
			{
				if (value != this._additionalParameters)
				{
					this._additionalParameters = value;
					base.OnPropertyChangedWithValue<string>(value, "AdditionalParameters");
				}
			}
		}

		// Token: 0x0400011F RID: 287
		public readonly SettlementNameplateEventItemVM.SettlementEventType EventType;

		// Token: 0x04000120 RID: 288
		private int _type;

		// Token: 0x04000121 RID: 289
		private string _additionalParameters;

		// Token: 0x0200006A RID: 106
		public enum SettlementEventType
		{
			// Token: 0x040002EB RID: 747
			Tournament,
			// Token: 0x040002EC RID: 748
			AvailableIssue,
			// Token: 0x040002ED RID: 749
			ActiveQuest,
			// Token: 0x040002EE RID: 750
			ActiveStoryQuest,
			// Token: 0x040002EF RID: 751
			TrackedIssue,
			// Token: 0x040002F0 RID: 752
			TrackedStoryQuest,
			// Token: 0x040002F1 RID: 753
			Production
		}
	}
}
