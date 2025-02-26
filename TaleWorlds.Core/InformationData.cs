﻿using System;
using System.Collections.Generic;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.Core
{
	// Token: 0x0200003B RID: 59
	public abstract class InformationData
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x000111A8 File Offset: 0x0000F3A8
		protected virtual void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			collectedObjects.Add(this.DescriptionText);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000111B6 File Offset: 0x0000F3B6
		internal static object AutoGeneratedGetMemberValueDescriptionText(object o)
		{
			return ((InformationData)o).DescriptionText;
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004AF RID: 1199
		public abstract TextObject TitleText { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B0 RID: 1200
		public abstract string SoundEventPath { get; }

		// Token: 0x060004B1 RID: 1201 RVA: 0x000111C3 File Offset: 0x0000F3C3
		protected InformationData(TextObject description)
		{
			this.DescriptionText = description;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000111D2 File Offset: 0x0000F3D2
		public virtual bool IsValid()
		{
			return true;
		}

		// Token: 0x04000244 RID: 580
		[SaveableField(2)]
		public readonly TextObject DescriptionText;
	}
}
