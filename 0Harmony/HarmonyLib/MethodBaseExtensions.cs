using System;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x0200004F RID: 79
	public static class MethodBaseExtensions
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00013088 File Offset: 0x00011288
		public static bool HasMethodBody(this MethodBase member)
		{
			MethodBody methodBody = member.GetMethodBody();
			int? num;
			if (methodBody == null)
			{
				num = null;
			}
			else
			{
				byte[] ilasByteArray = methodBody.GetILAsByteArray();
				num = ((ilasByteArray != null) ? new int?(ilasByteArray.Length) : null);
			}
			int? num2 = num;
			return num2.GetValueOrDefault() > 0;
		}
	}
}
