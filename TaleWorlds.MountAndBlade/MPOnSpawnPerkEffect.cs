using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002FE RID: 766
	public abstract class MPOnSpawnPerkEffect : MPOnSpawnPerkEffectBase
	{
		// Token: 0x060029C5 RID: 10693 RVA: 0x000A1794 File Offset: 0x0009F994
		static MPOnSpawnPerkEffect()
		{
			foreach (Type type in from t in PerkAssemblyCollection.GetPerkAssemblyTypes()
			where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MPOnSpawnPerkEffect))
			select t)
			{
				FieldInfo field = type.GetField("StringType", BindingFlags.Static | BindingFlags.NonPublic);
				string key = (string)((field != null) ? field.GetValue(null) : null);
				MPOnSpawnPerkEffect.Registered.Add(key, type);
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000A1824 File Offset: 0x0009FA24
		public static MPOnSpawnPerkEffect CreateFrom(XmlNode node)
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
			MPOnSpawnPerkEffect mponSpawnPerkEffect = (MPOnSpawnPerkEffect)Activator.CreateInstance(MPOnSpawnPerkEffect.Registered[key], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
			mponSpawnPerkEffect.Deserialize(node);
			return mponSpawnPerkEffect;
		}

		// Token: 0x0400102C RID: 4140
		protected static Dictionary<string, Type> Registered = new Dictionary<string, Type>();
	}
}
