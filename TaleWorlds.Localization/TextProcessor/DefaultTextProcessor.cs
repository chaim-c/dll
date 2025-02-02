using System;
using System.Globalization;
using System.Text;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x02000026 RID: 38
	public class DefaultTextProcessor : LanguageSpecificTextProcessor
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x0000506B File Offset: 0x0000326B
		public override void ProcessToken(string sourceText, ref int cursorPos, string token, StringBuilder outputString)
		{
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000506D File Offset: 0x0000326D
		public override CultureInfo CultureInfoForLanguage
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005074 File Offset: 0x00003274
		public override void ClearTemporaryData()
		{
		}
	}
}
