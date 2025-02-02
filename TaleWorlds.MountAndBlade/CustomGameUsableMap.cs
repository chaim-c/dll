using System;
using System.Collections.Generic;
using System.Linq;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002DB RID: 731
	public class CustomGameUsableMap
	{
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060027FA RID: 10234 RVA: 0x0009A2A9 File Offset: 0x000984A9
		// (set) Token: 0x060027FB RID: 10235 RVA: 0x0009A2B1 File Offset: 0x000984B1
		public string map { get; private set; }

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x0009A2BA File Offset: 0x000984BA
		// (set) Token: 0x060027FD RID: 10237 RVA: 0x0009A2C2 File Offset: 0x000984C2
		public bool isCompatibleWithAllGameTypes { get; private set; }

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060027FE RID: 10238 RVA: 0x0009A2CB File Offset: 0x000984CB
		// (set) Token: 0x060027FF RID: 10239 RVA: 0x0009A2D3 File Offset: 0x000984D3
		public List<string> compatibleGameTypes { get; private set; }

		// Token: 0x06002800 RID: 10240 RVA: 0x0009A2DC File Offset: 0x000984DC
		public CustomGameUsableMap(string map, bool isCompatibleWithAllGameTypes, List<string> compatibleGameTypes)
		{
			this.map = map;
			this.isCompatibleWithAllGameTypes = isCompatibleWithAllGameTypes;
			this.compatibleGameTypes = compatibleGameTypes;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0009A2FC File Offset: 0x000984FC
		public override bool Equals(object obj)
		{
			CustomGameUsableMap customGameUsableMap;
			if ((customGameUsableMap = (obj as CustomGameUsableMap)) != null)
			{
				return !(customGameUsableMap.map != this.map) && customGameUsableMap.isCompatibleWithAllGameTypes == this.isCompatibleWithAllGameTypes && (((this.compatibleGameTypes == null || this.compatibleGameTypes.Count == 0) && (customGameUsableMap.compatibleGameTypes == null || customGameUsableMap.compatibleGameTypes.Count == 0)) || (this.compatibleGameTypes != null && this.compatibleGameTypes.Count != 0 && customGameUsableMap.compatibleGameTypes != null && customGameUsableMap.compatibleGameTypes.Count != 0 && this.compatibleGameTypes.SequenceEqual(customGameUsableMap.compatibleGameTypes)));
			}
			return base.Equals(obj);
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0009A3AC File Offset: 0x000985AC
		public override int GetHashCode()
		{
			return (((this.map != null) ? this.map.GetHashCode() : 0) * 397 ^ this.isCompatibleWithAllGameTypes.GetHashCode()) * 397 ^ ((this.compatibleGameTypes != null) ? this.compatibleGameTypes.GetHashCode() : 0);
		}
	}
}
