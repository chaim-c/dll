using System;
using System.Collections.Generic;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200000F RID: 15
	public class RichTextTagParser
	{
		// Token: 0x06000088 RID: 136 RVA: 0x0000481C File Offset: 0x00002A1C
		public static RichTextTag Parse(string text2, int tagBeginIndex, int tagEndIndex)
		{
			RichTextTagType richTextTagType = RichTextTagType.Open;
			int i = tagBeginIndex;
			RichTextTag richTextTag = null;
			int num = -1;
			int num2 = 0;
			int num3 = 0;
			while (i < tagEndIndex)
			{
				char c = text2[i];
				char c2 = ' ';
				bool flag = i + 1 < tagEndIndex;
				if (flag)
				{
					c2 = text2[i + 1];
				}
				if (num3 == 0)
				{
					if (c == '<')
					{
						num3 = 1;
					}
				}
				else if (num3 == 1)
				{
					if (c == '/')
					{
						if (richTextTagType == RichTextTagType.Close)
						{
							throw new RichTextException("Unexpected beginning in rich text tag");
						}
						richTextTagType = RichTextTagType.Close;
					}
					else if (c != ' ')
					{
						if (num == -1)
						{
							num = i;
						}
						num2++;
						num3 = 2;
					}
				}
				else if (num3 == 2)
				{
					if (c == ' ')
					{
						richTextTag = new RichTextTag(text2.Substring(num, num2));
						num3 = 3;
					}
					else if (c == '>')
					{
						richTextTag = new RichTextTag(text2.Substring(num, num2));
						i--;
						num3 = 3;
					}
					else
					{
						num2++;
					}
				}
				else if (num3 == 3 && c != ' ')
				{
					if (c == '>')
					{
						num3 = 4;
					}
					else if (c == '/')
					{
						if (!flag)
						{
							throw new RichTextException("Unexpected ending in rich text tag");
						}
						if (c2 != '>')
						{
							throw new RichTextException("Unexpected ending in rich text tag");
						}
						if (richTextTagType == RichTextTagType.Close)
						{
							throw new RichTextException("Unexpected ending in rich text tag");
						}
						richTextTagType = RichTextTagType.SelfClose;
						num3 = 4;
					}
					else
					{
						int num4 = 0;
						bool flag2 = false;
						int startIndex = i;
						int num5 = 0;
						while (i < tagEndIndex)
						{
							char c3 = text2[i];
							num5++;
							if (!flag2)
							{
								if (c3 == '=')
								{
									flag2 = true;
								}
							}
							else if (c3 == '"' || c3 == '\'')
							{
								num4++;
								if (num4 == 2)
								{
									break;
								}
							}
							i++;
						}
						if (num4 != 2)
						{
							throw new RichTextException("Could not parse attribute of tag in rich text");
						}
						KeyValuePair<string, string> keyValuePair = RichTextTagParser.ParseAttribute(text2.Substring(startIndex, num5));
						if (richTextTag == null)
						{
							throw new RichTextException("Rich text tag name could not be parsed");
						}
						richTextTag.AddAtrribute(keyValuePair.Key, keyValuePair.Value);
					}
				}
				i++;
			}
			if (num3 != 4)
			{
				richTextTag = new RichTextTag(text2);
				richTextTagType = RichTextTagType.TextAfterError;
			}
			if (richTextTag == null)
			{
				throw new RichTextException("Unexpected behavior in rich text tag parser");
			}
			richTextTag.Type = richTextTagType;
			return richTextTag;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004A1C File Offset: 0x00002C1C
		private static KeyValuePair<string, string> ParseAttribute(string attributeText)
		{
			string[] array = attributeText.Split(new char[]
			{
				'='
			}, StringSplitOptions.RemoveEmptyEntries);
			string keyText = array[0];
			string valueText = array[1];
			string key = RichTextTagParser.ParseAttributeKey(keyText);
			string value = RichTextTagParser.ParseAttributeValue(valueText);
			return new KeyValuePair<string, string>(key, value);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004A58 File Offset: 0x00002C58
		private static string ParseAttributeKey(string keyText)
		{
			string text = "";
			bool flag = false;
			foreach (char c in keyText)
			{
				if (c == ' ')
				{
					flag = true;
				}
				else
				{
					if (flag)
					{
						throw new RichTextException("Unexpected character on attribute key in tag");
					}
					text += c.ToString();
				}
			}
			if (text.Length == 0)
			{
				throw new RichTextException("Unexpected attribute key length");
			}
			return text;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004AC4 File Offset: 0x00002CC4
		private static string ParseAttributeValue(string valueText)
		{
			if (valueText[0] != '"' && valueText[valueText.Length - 1] != '"' && valueText[0] != '\'' && valueText[valueText.Length - 1] != '\'')
			{
				throw new RichTextException("Unexpected quotes on attribute value in tag");
			}
			return valueText.Substring(1, valueText.Length - 2);
		}
	}
}
