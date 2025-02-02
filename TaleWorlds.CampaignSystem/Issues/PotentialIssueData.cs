using System;

namespace TaleWorlds.CampaignSystem.Issues
{
	// Token: 0x02000321 RID: 801
	public struct PotentialIssueData
	{
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002E08 RID: 11784 RVA: 0x000C11F8 File Offset: 0x000BF3F8
		public PotentialIssueData.StartIssueDelegate OnStartIssue { get; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000C1200 File Offset: 0x000BF400
		public string IssueId { get; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x000C1208 File Offset: 0x000BF408
		public Type IssueType { get; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x000C1210 File Offset: 0x000BF410
		public IssueBase.IssueFrequency Frequency { get; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06002E0C RID: 11788 RVA: 0x000C1218 File Offset: 0x000BF418
		public object RelatedObject { get; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000C1220 File Offset: 0x000BF420
		public bool IsValid
		{
			get
			{
				return this.OnStartIssue != null;
			}
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000C122B File Offset: 0x000BF42B
		public PotentialIssueData(PotentialIssueData.StartIssueDelegate onStartIssue, Type issueType, IssueBase.IssueFrequency frequency, object relatedObject = null)
		{
			this.OnStartIssue = onStartIssue;
			this.IssueId = issueType.Name;
			this.IssueType = issueType;
			this.Frequency = frequency;
			this.RelatedObject = relatedObject;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000C1256 File Offset: 0x000BF456
		public PotentialIssueData(Type issueType, IssueBase.IssueFrequency frequency)
		{
			this.OnStartIssue = null;
			this.IssueId = issueType.Name;
			this.IssueType = issueType;
			this.Frequency = frequency;
			this.RelatedObject = null;
		}

		// Token: 0x02000682 RID: 1666
		// (Invoke) Token: 0x060054A7 RID: 21671
		public delegate IssueBase StartIssueDelegate(in PotentialIssueData pid, Hero issueOwner);
	}
}
