using System;
using TaleWorlds.Library;
using TaleWorlds.Localization.Expressions;

namespace TaleWorlds.Localization.TextProcessor
{
	// Token: 0x02000027 RID: 39
	public class MBTextModel
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000507E File Offset: 0x0000327E
		internal MBReadOnlyList<TextExpression> RootExpressions
		{
			get
			{
				return this._rootExpressions;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00005099 File Offset: 0x00003299
		internal void AddRootExpression(TextExpression newExp)
		{
			this._rootExpressions.Add(newExp);
		}

		// Token: 0x04000059 RID: 89
		internal MBList<TextExpression> _rootExpressions = new MBList<TextExpression>();
	}
}
