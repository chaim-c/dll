﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaleWorlds.GauntletUI.PrefabSystem;
using TaleWorlds.Library;
using TaleWorlds.Library.CodeGeneration;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.GauntletUI.CodeGenerator
{
	// Token: 0x0200000E RID: 14
	public class UICodeGenerationContext
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004485 File Offset: 0x00002685
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000448D File Offset: 0x0000268D
		public ResourceDepot ResourceDepot { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004496 File Offset: 0x00002696
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x0000449E File Offset: 0x0000269E
		public WidgetFactory WidgetFactory { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000044A7 File Offset: 0x000026A7
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000044AF File Offset: 0x000026AF
		public FontFactory FontFactory { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000044B8 File Offset: 0x000026B8
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000044C0 File Offset: 0x000026C0
		public BrushFactory BrushFactory { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000044C9 File Offset: 0x000026C9
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000044D1 File Offset: 0x000026D1
		public SpriteData SpriteData { get; private set; }

		// Token: 0x060000BA RID: 186 RVA: 0x000044DA File Offset: 0x000026DA
		public UICodeGenerationContext(string nameSpace, string outputFolder)
		{
			this._nameSpace = nameSpace;
			this._outputFolder = outputFolder;
			this._widgetTemplateGenerateContexts = new List<WidgetTemplateGenerateContext>();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000044FC File Offset: 0x000026FC
		public void Prepare(IEnumerable<string> resourceLocations, IEnumerable<PrefabExtension> prefabExtensions)
		{
			this.ResourceDepot = new ResourceDepot();
			foreach (string location in resourceLocations)
			{
				this.ResourceDepot.AddLocation(BasePath.Name, location);
			}
			this.ResourceDepot.CollectResources();
			this.WidgetFactory = new WidgetFactory(this.ResourceDepot, "Prefabs");
			foreach (PrefabExtension prefabExtension in prefabExtensions)
			{
				this.WidgetFactory.PrefabExtensionContext.AddExtension(prefabExtension);
			}
			this.WidgetFactory.Initialize(null);
			this.SpriteData = new SpriteData("SpriteData");
			this.SpriteData.Load(this.ResourceDepot);
			this.FontFactory = new FontFactory(this.ResourceDepot);
			this.FontFactory.LoadAllFonts(this.SpriteData);
			this.BrushFactory = new BrushFactory(this.ResourceDepot, "Brushes", this.SpriteData, this.FontFactory);
			this.BrushFactory.Initialize();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004638 File Offset: 0x00002838
		public void AddPrefabVariant(string prefabName, string variantName, UICodeGenerationVariantExtension variantExtension, Dictionary<string, object> data)
		{
			WidgetTemplateGenerateContext item = WidgetTemplateGenerateContext.CreateAsRoot(this, prefabName, variantName, variantExtension, data);
			this._widgetTemplateGenerateContexts.Add(item);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004660 File Offset: 0x00002860
		private static void ClearFolder(string folderName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(folderName);
			FileInfo[] files = directoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				UICodeGenerationContext.ClearFolder(directoryInfo2.FullName);
				directoryInfo2.Delete();
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000046BC File Offset: 0x000028BC
		public void Generate()
		{
			Dictionary<string, CodeGenerationContext> dictionary = new Dictionary<string, CodeGenerationContext>();
			foreach (WidgetTemplateGenerateContext widgetTemplateGenerateContext in this._widgetTemplateGenerateContexts)
			{
				string key = widgetTemplateGenerateContext.PrefabName + ".gen.cs";
				if (!dictionary.ContainsKey(key))
				{
					dictionary.Add(key, new CodeGenerationContext());
				}
				NamespaceCode namespaceCode = dictionary[key].FindOrCreateNamespace(this._nameSpace);
				widgetTemplateGenerateContext.GenerateInto(namespaceCode);
			}
			string fullPath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\..\\..\\..\\Source\\" + this._outputFolder);
			UICodeGenerationContext.ClearFolder(fullPath);
			List<string> usingDefinitions = new List<string>
			{
				"System.Numerics",
				"TaleWorlds.Library"
			};
			foreach (KeyValuePair<string, CodeGenerationContext> keyValuePair in dictionary)
			{
				string key2 = keyValuePair.Key;
				CodeGenerationContext codeGenerationContext = dictionary[key2];
				CodeGenerationFile codeGenerationFile = new CodeGenerationFile(usingDefinitions);
				codeGenerationContext.GenerateInto(codeGenerationFile);
				string contents = codeGenerationFile.GenerateText();
				File.WriteAllText(fullPath + "\\" + key2, contents, Encoding.UTF8);
			}
			CodeGenerationContext codeGenerationContext2 = new CodeGenerationContext();
			NamespaceCode namespaceCode2 = codeGenerationContext2.FindOrCreateNamespace(this._nameSpace);
			ClassCode classCode = new ClassCode();
			classCode.Name = "GeneratedUIPrefabCreator";
			classCode.AccessModifier = ClassCodeAccessModifier.Public;
			classCode.InheritedInterfaces.Add("TaleWorlds.GauntletUI.PrefabSystem.IGeneratedUIPrefabCreator");
			MethodCode methodCode = new MethodCode();
			methodCode.Name = "CollectGeneratedPrefabDefinitions";
			methodCode.MethodSignature = "(TaleWorlds.GauntletUI.PrefabSystem.GeneratedPrefabContext generatedPrefabContext)";
			foreach (WidgetTemplateGenerateContext widgetTemplateGenerateContext2 in this._widgetTemplateGenerateContexts)
			{
				MethodCode methodCode2 = widgetTemplateGenerateContext2.GenerateCreatorMethod();
				classCode.AddMethod(methodCode2);
				methodCode.AddLine(string.Concat(new string[]
				{
					"generatedPrefabContext.AddGeneratedPrefab(\"",
					widgetTemplateGenerateContext2.PrefabName,
					"\", \"",
					widgetTemplateGenerateContext2.VariantName,
					"\", ",
					methodCode2.Name,
					");"
				}));
			}
			classCode.AddMethod(methodCode);
			namespaceCode2.AddClass(classCode);
			CodeGenerationFile codeGenerationFile2 = new CodeGenerationFile(null);
			codeGenerationContext2.GenerateInto(codeGenerationFile2);
			string contents2 = codeGenerationFile2.GenerateText();
			File.WriteAllText(fullPath + "\\PrefabCodes.gen.cs", contents2, Encoding.UTF8);
		}

		// Token: 0x0400004C RID: 76
		private List<WidgetTemplateGenerateContext> _widgetTemplateGenerateContexts;

		// Token: 0x0400004D RID: 77
		private readonly string _nameSpace;

		// Token: 0x0400004E RID: 78
		private readonly string _outputFolder;
	}
}
