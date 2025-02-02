using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000303 RID: 771
	public abstract class MPPerkEffect : MPPerkEffectBase
	{
		// Token: 0x060029DF RID: 10719 RVA: 0x000A1C38 File Offset: 0x0009FE38
		static MPPerkEffect()
		{
			foreach (Type type in from t in PerkAssemblyCollection.GetPerkAssemblyTypes()
			where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MPPerkEffect))
			select t)
			{
				FieldInfo field = type.GetField("StringType", BindingFlags.Static | BindingFlags.NonPublic);
				string key = (string)((field != null) ? field.GetValue(null) : null);
				MPPerkEffect.Registered.Add(key, type);
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x000A1CC8 File Offset: 0x0009FEC8
		public static MPPerkEffect CreateFrom(XmlNode node)
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
			MPPerkEffect mpperkEffect = (MPPerkEffect)Activator.CreateInstance(MPPerkEffect.Registered[key], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
			mpperkEffect.Deserialize(node);
			return mpperkEffect;
		}

		// Token: 0x0400102F RID: 4143
		protected static Dictionary<string, Type> Registered = new Dictionary<string, Type>();
	}
}
