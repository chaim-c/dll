﻿using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.PlatformService
{
	// Token: 0x0200000D RID: 13
	// (Invoke) Token: 0x06000050 RID: 80
	public delegate void PermissionChanged(PlayerId TargetPlayerId, Permission permission, bool HasPermission);
}
