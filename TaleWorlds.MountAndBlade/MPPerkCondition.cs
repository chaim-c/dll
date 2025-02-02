using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000301 RID: 769
	public abstract class MPPerkCondition
	{
		// Token: 0x060029D3 RID: 10707 RVA: 0x000A1960 File Offset: 0x0009FB60
		static MPPerkCondition()
		{
			foreach (Type type in from t in PerkAssemblyCollection.GetPerkAssemblyTypes()
			where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MPPerkCondition))
			select t)
			{
				FieldInfo field = type.GetField("StringType", BindingFlags.Static | BindingFlags.NonPublic);
				string key = (string)((field != null) ? field.GetValue(null) : null);
				MPPerkCondition.Registered.Add(key, type);
			}
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000A19F0 File Offset: 0x0009FBF0
		public virtual MPPerkCondition.PerkEventFlags EventFlags
		{
			get
			{
				return MPPerkCondition.PerkEventFlags.None;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000A19F3 File Offset: 0x0009FBF3
		public virtual bool IsPeerCondition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060029D6 RID: 10710
		public abstract bool Check(MissionPeer peer);

		// Token: 0x060029D7 RID: 10711
		public abstract bool Check(Agent agent);

		// Token: 0x060029D8 RID: 10712 RVA: 0x000A19F6 File Offset: 0x0009FBF6
		protected virtual bool IsGameModesValid(List<string> gameModes)
		{
			return true;
		}

		// Token: 0x060029D9 RID: 10713
		protected abstract void Deserialize(XmlNode node);

		// Token: 0x060029DA RID: 10714 RVA: 0x000A19FC File Offset: 0x0009FBFC
		public static MPPerkCondition CreateFrom(List<string> gameModes, XmlNode node)
		{
			string text;
			if (node == null)
			{
				text = null;
			}
			else
			{
				XmlAttributeCollection attributes = node.Attributes;
				if (attributes == null)
				{
					text = null;
				}
				else
				{
					XmlAttribute xmlAttribute = attributes["type"];
					text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
				}
			}
			string key = text;
			MPPerkCondition mpperkCondition = (MPPerkCondition)Activator.CreateInstance(MPPerkCondition.Registered[key], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
			mpperkCondition.Deserialize(node);
			return mpperkCondition;
		}

		// Token: 0x0400102E RID: 4142
		protected static Dictionary<string, Type> Registered = new Dictionary<string, Type>();

		// Token: 0x020005C6 RID: 1478
		[Flags]
		public enum PerkEventFlags
		{
			// Token: 0x04001E7E RID: 7806
			None = 0,
			// Token: 0x04001E7F RID: 7807
			MoraleChange = 1,
			// Token: 0x04001E80 RID: 7808
			FlagCapture = 2,
			// Token: 0x04001E81 RID: 7809
			FlagRemoval = 4,
			// Token: 0x04001E82 RID: 7810
			HealthChange = 8,
			// Token: 0x04001E83 RID: 7811
			AliveBotCountChange = 16,
			// Token: 0x04001E84 RID: 7812
			PeerControlledAgentChange = 32,
			// Token: 0x04001E85 RID: 7813
			BannerPickUp = 64,
			// Token: 0x04001E86 RID: 7814
			BannerDrop = 128,
			// Token: 0x04001E87 RID: 7815
			SpawnEnd = 256,
			// Token: 0x04001E88 RID: 7816
			MountHealthChange = 512,
			// Token: 0x04001E89 RID: 7817
			MountChange = 1024,
			// Token: 0x04001E8A RID: 7818
			AgentEventsMask = 1576
		}
	}
}
