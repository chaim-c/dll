using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x02000006 RID: 6
	public class Font
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000027CA File Offset: 0x000009CA
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000027D2 File Offset: 0x000009D2
		public string Name { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000027DB File Offset: 0x000009DB
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000027E3 File Offset: 0x000009E3
		public int Size { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000027EC File Offset: 0x000009EC
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000027F4 File Offset: 0x000009F4
		public int LineHeight { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000027FD File Offset: 0x000009FD
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002805 File Offset: 0x00000A05
		public int Base { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000280E File Offset: 0x00000A0E
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002816 File Offset: 0x00000A16
		public int CharacterCount { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000281F File Offset: 0x00000A1F
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002827 File Offset: 0x00000A27
		public float SmoothingConstant { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002830 File Offset: 0x00000A30
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002838 File Offset: 0x00000A38
		public float CustomScale { get; private set; } = 1f;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002841 File Offset: 0x00000A41
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002849 File Offset: 0x00000A49
		public bool Smooth { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002852 File Offset: 0x00000A52
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000285A File Offset: 0x00000A5A
		public SpritePart FontSprite { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002863 File Offset: 0x00000A63
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000286B File Offset: 0x00000A6B
		public Dictionary<int, BitmapFontCharacter> Characters { get; private set; }

		// Token: 0x06000033 RID: 51 RVA: 0x00002874 File Offset: 0x00000A74
		public Font(string name)
		{
			this.Name = name;
			this.Characters = new Dictionary<int, BitmapFontCharacter>();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000289C File Offset: 0x00000A9C
		public bool TryLoadFontFromPath(string path, SpriteData spriteData)
		{
			Debug.Print("Loading " + this.Name + " font, at: " + path, 0, Debug.DebugColor.White, 17592186044416UL);
			bool result;
			try
			{
				this.LoadFromPathAux(path, spriteData);
				result = true;
			}
			catch (Exception arg)
			{
				Debug.FailedAssert("Failed to load font:" + this.Name + " at path: " + path, "C:\\Develop\\MB3\\TaleWorlds.Shared\\Source\\GauntletUI\\TaleWorlds.TwoDimension\\BitmapFont\\Font.cs", "TryLoadFontFromPath", 54);
				Debug.Print(string.Format("Failed to load font:{0} at path: {1}. Error:{2}", this.Name, path, arg), 0, Debug.DebugColor.White, 17592186044416UL);
				result = false;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000293C File Offset: 0x00000B3C
		private void LoadFromPathAux(string path, SpriteData spriteData)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(path);
			XmlElement xmlElement = xmlDocument["font"];
			XmlElement xmlElement2 = xmlElement["info"];
			this._realSize = Math.Abs(Convert.ToInt32(xmlElement2.Attributes["size"].Value));
			this.Smooth = true;
			if (xmlElement2.Attributes["smooth"] != null)
			{
				this.Smooth = Convert.ToBoolean(Convert.ToInt32(xmlElement2.Attributes["smooth"].Value));
			}
			this.SmoothingConstant = 0.47f;
			if (xmlElement2.Attributes["smoothingConstant"] != null)
			{
				this.SmoothingConstant = Convert.ToSingle(xmlElement2.Attributes["smoothingConstant"].Value, CultureInfo.InvariantCulture);
			}
			if (xmlElement2.Attributes["customScale"] != null)
			{
				this.CustomScale = Convert.ToSingle(xmlElement2.Attributes["customScale"].Value, CultureInfo.InvariantCulture);
			}
			XmlElement xmlElement3 = xmlElement["common"];
			this.LineHeight = Convert.ToInt32(xmlElement3.Attributes["lineHeight"].Value);
			this.Base = Convert.ToInt32(xmlElement3.Attributes["base"].Value);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(xmlElement["pages"].ChildNodes[0].Attributes["file"].Value);
			XmlElement xmlElement4 = xmlElement["chars"];
			this.CharacterCount = Convert.ToInt32(xmlElement4.Attributes["count"].Value);
			string path2 = Path.ChangeExtension(path, ".bfnt");
			if (File.Exists(path2))
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(path2, FileMode.Open, FileAccess.Read)))
				{
					for (int i = 0; i < this.CharacterCount; i++)
					{
						GCHandle gchandle = GCHandle.Alloc(binaryReader.ReadBytes(Marshal.SizeOf(typeof(BitmapFontCharacter))), GCHandleType.Pinned);
						BitmapFontCharacter bitmapFontCharacter = (BitmapFontCharacter)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(BitmapFontCharacter));
						this.Characters.Add(bitmapFontCharacter.ID, bitmapFontCharacter);
						gchandle.Free();
					}
					goto IL_398;
				}
			}
			for (int j = 0; j < this.CharacterCount; j++)
			{
				XmlNode xmlNode = xmlElement4.ChildNodes[j];
				BitmapFontCharacter bitmapFontCharacter2;
				bitmapFontCharacter2.ID = Convert.ToInt32(xmlNode.Attributes["id"].Value);
				bitmapFontCharacter2.X = Convert.ToInt32(xmlNode.Attributes["x"].Value);
				bitmapFontCharacter2.Y = Convert.ToInt32(xmlNode.Attributes["y"].Value);
				bitmapFontCharacter2.Width = Convert.ToInt32(xmlNode.Attributes["width"].Value);
				bitmapFontCharacter2.Height = Convert.ToInt32(xmlNode.Attributes["height"].Value);
				bitmapFontCharacter2.XOffset = Convert.ToInt32(xmlNode.Attributes["xoffset"].Value);
				bitmapFontCharacter2.YOffset = Convert.ToInt32(xmlNode.Attributes["yoffset"].Value);
				bitmapFontCharacter2.XAdvance = Convert.ToInt32(xmlNode.Attributes["xadvance"].Value);
				this.Characters.Add(bitmapFontCharacter2.ID, bitmapFontCharacter2);
			}
			IL_398:
			SpriteGeneric spriteGeneric = spriteData.GetSprite(fileNameWithoutExtension) as SpriteGeneric;
			SpritePart fontSprite = (spriteGeneric != null) ? spriteGeneric.SpritePart : null;
			this.FontSprite = fontSprite;
			this.Size = (int)((float)this._realSize / this.CustomScale);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D2C File Offset: 0x00000F2C
		public float GetWordWidth(string word, float extraPadding)
		{
			float num = 0f;
			for (int i = 0; i < word.Length; i++)
			{
				num += this.GetCharacterWidth(word[i], extraPadding);
			}
			return num;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D64 File Offset: 0x00000F64
		public float GetCharacterWidth(char character, float extraPadding)
		{
			float num = 0f;
			int key = (int)character;
			if (!this.Characters.ContainsKey(key))
			{
				key = 0;
			}
			BitmapFontCharacter bitmapFontCharacter = this.Characters[key];
			return num + ((float)bitmapFontCharacter.XAdvance + extraPadding);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002D9F File Offset: 0x00000F9F
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.Name))
			{
				return base.ToString();
			}
			return this.Name;
		}

		// Token: 0x0400001F RID: 31
		private int _realSize;
	}
}
