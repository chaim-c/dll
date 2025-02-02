using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000141 RID: 321
	public static class ModuleInfoModelExtensions
	{
		// Token: 0x060008A8 RID: 2216 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		public static bool IsCompatibleWith(this IEnumerable<ModuleInfoModel> a, IEnumerable<ModuleInfoModel> b, bool allowOptionalModules)
		{
			bool flag = (from m in a
			where !m.IsOptional
			orderby m.Id
			select m).SequenceEqual(from m in b
			where !m.IsOptional
			orderby m.Id
			select m);
			bool flag2;
			if (!a.Any((ModuleInfoModel m) => m.IsOptional))
			{
				flag2 = b.Any((ModuleInfoModel m) => m.IsOptional);
			}
			else
			{
				flag2 = true;
			}
			bool flag3 = flag2;
			return flag && (allowOptionalModules || !flag3);
		}
	}
}
