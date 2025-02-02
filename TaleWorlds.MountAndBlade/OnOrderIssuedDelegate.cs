using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200014C RID: 332
	// (Invoke) Token: 0x06001089 RID: 4233
	public delegate void OnOrderIssuedDelegate(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController, params object[] delegateParams);
}
