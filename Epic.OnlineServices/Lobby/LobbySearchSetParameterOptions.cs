using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AB RID: 939
	public struct LobbySearchSetParameterOptions
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000257D5 File Offset: 0x000239D5
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x000257DD File Offset: 0x000239DD
		public AttributeData? Parameter { get; set; }

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000257E6 File Offset: 0x000239E6
		// (set) Token: 0x060018B5 RID: 6325 RVA: 0x000257EE File Offset: 0x000239EE
		public ComparisonOp ComparisonOp { get; set; }
	}
}
