using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000300 RID: 768
	public abstract class MPOnSpawnPerkEffectBase : MPPerkEffectBase, IOnSpawnPerkEffect
	{
		// Token: 0x060029CC RID: 10700 RVA: 0x000A1888 File Offset: 0x0009FA88
		protected override void Deserialize(XmlNode node)
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
					XmlAttribute xmlAttribute = attributes["is_disabled_in_warmup"];
					text = ((xmlAttribute != null) ? xmlAttribute.Value : null);
				}
			}
			string text2 = text;
			base.IsDisabledInWarmup = (((text2 != null) ? text2.ToLower() : null) == "true");
			string text3;
			if (node == null)
			{
				text3 = null;
			}
			else
			{
				XmlAttributeCollection attributes2 = node.Attributes;
				if (attributes2 == null)
				{
					text3 = null;
				}
				else
				{
					XmlAttribute xmlAttribute2 = attributes2["target"];
					text3 = ((xmlAttribute2 != null) ? xmlAttribute2.Value : null);
				}
			}
			string text4 = text3;
			this.EffectTarget = MPOnSpawnPerkEffectBase.Target.Any;
			if (text4 != null && !Enum.TryParse<MPOnSpawnPerkEffectBase.Target>(text4, true, out this.EffectTarget))
			{
				this.EffectTarget = MPOnSpawnPerkEffectBase.Target.Any;
				Debug.FailedAssert("provided 'target' is invalid", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\Perks\\MPOnSpawnPerkEffectBase.cs", "Deserialize", 38);
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x000A193B File Offset: 0x0009FB3B
		public virtual float GetTroopCountMultiplier()
		{
			return 0f;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x000A1942 File Offset: 0x0009FB42
		public virtual int GetExtraTroopCount()
		{
			return 0;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x000A1945 File Offset: 0x0009FB45
		public virtual List<ValueTuple<EquipmentIndex, EquipmentElement>> GetAlternativeEquipments(bool isPlayer, List<ValueTuple<EquipmentIndex, EquipmentElement>> alternativeEquipments, bool getAll = false)
		{
			return alternativeEquipments;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x000A1948 File Offset: 0x0009FB48
		public virtual float GetDrivenPropertyBonusOnSpawn(bool isPlayer, DrivenProperty drivenProperty, float baseValue)
		{
			return 0f;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x000A194F File Offset: 0x0009FB4F
		public virtual float GetHitpoints(bool isPlayer)
		{
			return 0f;
		}

		// Token: 0x0400102D RID: 4141
		protected MPOnSpawnPerkEffectBase.Target EffectTarget;

		// Token: 0x020005C5 RID: 1477
		protected enum Target
		{
			// Token: 0x04001E7A RID: 7802
			Player,
			// Token: 0x04001E7B RID: 7803
			Troops,
			// Token: 0x04001E7C RID: 7804
			Any
		}
	}
}
