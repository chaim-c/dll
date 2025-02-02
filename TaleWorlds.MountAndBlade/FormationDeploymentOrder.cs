using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020C RID: 524
	public struct FormationDeploymentOrder
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00065DA2 File Offset: 0x00063FA2
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x00065DAA File Offset: 0x00063FAA
		public int Key { get; private set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00065DB3 File Offset: 0x00063FB3
		// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x00065DBB File Offset: 0x00063FBB
		public int Offset { get; private set; }

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00065DC4 File Offset: 0x00063FC4
		private FormationDeploymentOrder(FormationClass formationClass, int offset = 0)
		{
			int formationClassPriority = FormationDeploymentOrder.GetFormationClassPriority(formationClass);
			this.Offset = MathF.Max(0, offset);
			this.Key = formationClassPriority + this.Offset * 11;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x00065DF6 File Offset: 0x00063FF6
		public static FormationDeploymentOrder GetDeploymentOrder(FormationClass fClass, int offset = 0)
		{
			return new FormationDeploymentOrder(fClass, offset);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x00065DFF File Offset: 0x00063FFF
		public static FormationDeploymentOrder.DeploymentOrderComparer GetComparer()
		{
			FormationDeploymentOrder.DeploymentOrderComparer result;
			if ((result = FormationDeploymentOrder._comparer) == null)
			{
				result = (FormationDeploymentOrder._comparer = new FormationDeploymentOrder.DeploymentOrderComparer());
			}
			return result;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x00065E18 File Offset: 0x00064018
		private static int GetFormationClassPriority(FormationClass fClass)
		{
			switch (fClass)
			{
			case FormationClass.Infantry:
				return 2;
			case FormationClass.Ranged:
				return 5;
			case FormationClass.Cavalry:
				return 4;
			case FormationClass.HorseArcher:
				return 6;
			case FormationClass.NumberOfDefaultFormations:
				return 0;
			case FormationClass.HeavyInfantry:
				return 1;
			case FormationClass.LightCavalry:
				return 7;
			case FormationClass.HeavyCavalry:
				return 3;
			case FormationClass.NumberOfRegularFormations:
				return 9;
			case FormationClass.Bodyguard:
				return 8;
			default:
				return 10;
			}
		}

		// Token: 0x0400095E RID: 2398
		private static FormationDeploymentOrder.DeploymentOrderComparer _comparer;

		// Token: 0x020004EC RID: 1260
		public class DeploymentOrderComparer : IComparer<FormationDeploymentOrder>
		{
			// Token: 0x060037B6 RID: 14262 RVA: 0x000E05D2 File Offset: 0x000DE7D2
			public int Compare(FormationDeploymentOrder a, FormationDeploymentOrder b)
			{
				return a.Key - b.Key;
			}
		}
	}
}
