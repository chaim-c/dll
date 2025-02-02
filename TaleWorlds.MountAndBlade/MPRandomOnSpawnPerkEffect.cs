using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;
using TaleWorlds.MountAndBlade.Network.Gameplay.Perks;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000308 RID: 776
	public abstract class MPRandomOnSpawnPerkEffect : MPOnSpawnPerkEffectBase
	{
		// Token: 0x06002A4B RID: 10827 RVA: 0x000A495C File Offset: 0x000A2B5C
		static MPRandomOnSpawnPerkEffect()
		{
			foreach (Type type in from t in PerkAssemblyCollection.GetPerkAssemblyTypes()
			where t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(MPRandomOnSpawnPerkEffect))
			select t)
			{
				FieldInfo field = type.GetField("StringType", BindingFlags.Static | BindingFlags.NonPublic);
				string key = (string)((field != null) ? field.GetValue(null) : null);
				MPRandomOnSpawnPerkEffect.Registered.Add(key, type);
			}
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000A49EC File Offset: 0x000A2BEC
		public static MPRandomOnSpawnPerkEffect CreateFrom(XmlNode node)
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
			MPRandomOnSpawnPerkEffect mprandomOnSpawnPerkEffect = (MPRandomOnSpawnPerkEffect)Activator.CreateInstance(MPRandomOnSpawnPerkEffect.Registered[key], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
			mprandomOnSpawnPerkEffect.Deserialize(node);
			return mprandomOnSpawnPerkEffect;
		}

		// Token: 0x04001045 RID: 4165
		protected static Dictionary<string, Type> Registered = new Dictionary<string, Type>();
	}
}
