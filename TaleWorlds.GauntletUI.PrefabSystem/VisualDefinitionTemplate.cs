﻿using System;
using System.Collections.Generic;
using System.Xml;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.PrefabSystem
{
	// Token: 0x0200000B RID: 11
	public class VisualDefinitionTemplate
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002884 File Offset: 0x00000A84
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000288C File Offset: 0x00000A8C
		public string Name { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002895 File Offset: 0x00000A95
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000289D File Offset: 0x00000A9D
		public float TransitionDuration { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028A6 File Offset: 0x00000AA6
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000028AE File Offset: 0x00000AAE
		public float DelayOnBegin { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000028B7 File Offset: 0x00000AB7
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000028BF File Offset: 0x00000ABF
		public bool EaseIn { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000028C8 File Offset: 0x00000AC8
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000028D0 File Offset: 0x00000AD0
		public Dictionary<string, VisualStateTemplate> VisualStates { get; private set; }

		// Token: 0x0600004C RID: 76 RVA: 0x000028D9 File Offset: 0x00000AD9
		public VisualDefinitionTemplate()
		{
			this.VisualStates = new Dictionary<string, VisualStateTemplate>();
			this.TransitionDuration = 0.2f;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028F7 File Offset: 0x00000AF7
		public void AddVisualState(VisualStateTemplate visualState)
		{
			this.VisualStates.Add(visualState.State, visualState);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000290C File Offset: 0x00000B0C
		public VisualDefinition CreateVisualDefinition(BrushFactory brushFactory, SpriteData spriteData, Dictionary<string, VisualDefinitionTemplate> visualDefinitionTemplates, Dictionary<string, ConstantDefinition> constants, Dictionary<string, WidgetAttributeTemplate> parameters, Dictionary<string, string> defaultParameters)
		{
			VisualDefinition visualDefinition = new VisualDefinition(this.Name, this.TransitionDuration, this.DelayOnBegin, this.EaseIn);
			foreach (VisualStateTemplate visualStateTemplate in this.VisualStates.Values)
			{
				VisualState visualState = visualStateTemplate.CreateVisualState(brushFactory, spriteData, visualDefinitionTemplates, constants, parameters, defaultParameters);
				visualDefinition.AddVisualState(visualState);
			}
			return visualDefinition;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002994 File Offset: 0x00000B94
		internal void Save(XmlNode rootNode)
		{
			XmlDocument ownerDocument = rootNode.OwnerDocument;
			XmlNode xmlNode = ownerDocument.CreateElement("VisualDefinition");
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute("Name");
			xmlAttribute.InnerText = this.Name;
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = ownerDocument.CreateAttribute("TransitionDuration");
			xmlAttribute2.InnerText = this.TransitionDuration.ToString();
			xmlNode.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = ownerDocument.CreateAttribute("DelayOnBegin");
			xmlAttribute3.InnerText = this.DelayOnBegin.ToString();
			xmlNode.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = ownerDocument.CreateAttribute("EaseIn");
			xmlAttribute4.InnerText = this.EaseIn.ToString();
			xmlNode.Attributes.Append(xmlAttribute4);
			foreach (VisualStateTemplate visualStateTemplate in this.VisualStates.Values)
			{
				XmlNode xmlNode2 = ownerDocument.CreateElement("VisualState");
				XmlAttribute xmlAttribute5 = ownerDocument.CreateAttribute("State");
				xmlAttribute5.InnerText = visualStateTemplate.State;
				xmlNode2.Attributes.Append(xmlAttribute5);
				foreach (KeyValuePair<string, string> keyValuePair in visualStateTemplate.GetAttributes())
				{
					XmlAttribute xmlAttribute6 = ownerDocument.CreateAttribute(keyValuePair.Key);
					xmlAttribute6.InnerText = keyValuePair.Value;
					xmlNode2.Attributes.Append(xmlAttribute6);
				}
				xmlNode.AppendChild(xmlNode2);
			}
			rootNode.AppendChild(xmlNode);
		}
	}
}
