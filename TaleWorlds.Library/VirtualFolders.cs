using System;
using System.IO;
using System.Reflection;

namespace TaleWorlds.Library
{
	// Token: 0x020000A1 RID: 161
	public class VirtualFolders
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00013A62 File Offset: 0x00011C62
		public static string GetFileContent(string filePath)
		{
			if (VirtualFolders._useVirtualFolders)
			{
				return VirtualFolders.GetVirtualFileContent(filePath);
			}
			if (!File.Exists(filePath))
			{
				return "";
			}
			return File.ReadAllText(filePath);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00013A88 File Offset: 0x00011C88
		private static string GetVirtualFileContent(string filePath)
		{
			string fileName = Path.GetFileName(filePath);
			string[] array = Path.GetDirectoryName(filePath).Split(new char[]
			{
				Path.DirectorySeparatorChar
			});
			Type type = typeof(VirtualFolders);
			int num = 0;
			while (type != null && num != array.Length)
			{
				if (!string.IsNullOrEmpty(array[num]))
				{
					type = VirtualFolders.GetNestedDirectory(array[num], type);
				}
				num++;
			}
			if (type != null)
			{
				FieldInfo[] fields = type.GetFields();
				for (int i = 0; i < fields.Length; i++)
				{
					VirtualFileAttribute[] array2 = (VirtualFileAttribute[])fields[i].GetCustomAttributesSafe(typeof(VirtualFileAttribute), false);
					if (array2[0].Name == fileName)
					{
						return array2[0].Content;
					}
				}
			}
			return "";
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00013B4C File Offset: 0x00011D4C
		private static Type GetNestedDirectory(string name, Type type)
		{
			foreach (Type type2 in type.GetNestedTypes())
			{
				if (((VirtualDirectoryAttribute[])type2.GetCustomAttributesSafe(typeof(VirtualDirectoryAttribute), false))[0].Name == name)
				{
					return type2;
				}
			}
			return null;
		}

		// Token: 0x040001B4 RID: 436
		private static readonly bool _useVirtualFolders = true;

		// Token: 0x020000E8 RID: 232
		[VirtualDirectory("..")]
		public class Win64_Shipping_Client
		{
			// Token: 0x020000F3 RID: 243
			[VirtualDirectory("..")]
			public class bin
			{
				// Token: 0x020000F4 RID: 244
				[VirtualDirectory("Parameters")]
				public class Parameters
				{
					// Token: 0x0400030A RID: 778
					[VirtualFile("Environment", "eIEUjZV1fWgjwmWKMx8bXYrQ8NiCt336MENgiNk6v98nV4vDh9rQN3ZVfbI.e4k9q_0EWFXoROmuya9_C9tNELxxJX2jFjsUPAMdo15cpfOFTDOCIIGvOhE_wrSHHz_RlzfkNquCNe2N4SYyFGoaeSWP41e1wr3RDLO6Dlq9Y0I-")]
					public string Environment;

					// Token: 0x0400030B RID: 779
					[VirtualFile("Version.xml", "<Version>\t<Singleplayer Value=\"v1.2.11\" /></Version>")]
					public string Version;

					// Token: 0x0400030C RID: 780
					[VirtualFile("ClientProfile.xml", "<ClientProfile Value=\"DigitalOcean.Discovery\"/>")]
					public string ClientProfile;

					// Token: 0x020000F5 RID: 245
					[VirtualDirectory("ClientProfiles")]
					public class ClientProfiles
					{
						// Token: 0x020000F6 RID: 246
						[VirtualDirectory("DigitalOcean.Discovery")]
						public class DigitalOceanDiscovery
						{
							// Token: 0x0400030D RID: 781
							[VirtualFile("LobbyClient.xml", "<Configuration>\t<SessionProvider Type=\"ThreadedRest\" />\t<Clients>\t\t<Client Type=\"LobbyClient\" />\t</Clients>\t<Parameters>\t\t<Parameter Name=\"LobbyClient.ServiceDiscovery.Address\" Value=\"https://bannerlord-service-discovery.bannerlord-services-3.net/\" />\t\t\t\t<Parameter Name=\"LobbyClient.Address\" Value=\"service://bannerlord.lobby/\" />\t\t<Parameter Name=\"LobbyClient.Port\" Value=\"443\" />\t\t<Parameter Name=\"LobbyClient.IsSecure\" Value=\"true\" />\t</Parameters></Configuration>")]
							public string LobbyClient;
						}
					}
				}
			}
		}
	}
}
