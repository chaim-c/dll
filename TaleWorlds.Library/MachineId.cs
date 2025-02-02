using System;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace TaleWorlds.Library
{
	// Token: 0x0200005C RID: 92
	public static class MachineId
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00007FAA File Offset: 0x000061AA
		public static void Initialize()
		{
			if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				MachineId.MachineIdString = "nonwindows";
				return;
			}
			MachineId.MachineIdString = MachineId.ProcessId();
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00007FCD File Offset: 0x000061CD
		public static int AsInteger()
		{
			if (!string.IsNullOrEmpty(MachineId.MachineIdString))
			{
				return BitConverter.ToInt32(Encoding.ASCII.GetBytes(MachineId.MachineIdString), 0);
			}
			return 0;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007FF2 File Offset: 0x000061F2
		private static string ProcessId()
		{
			return "" + MachineId.GetMotherboardIdentifier() + MachineId.GetCpuIdentifier() + MachineId.GetDiskIdentifier();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00008018 File Offset: 0x00006218
		private static string GetMotherboardIdentifier()
		{
			string text = "";
			try
			{
				using (ManagementObjectCollection instances = new ManagementClass("win32_baseboard").GetInstances())
				{
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						string text2 = (((ManagementObject)managementBaseObject)["SerialNumber"] as string).Trim(new char[]
						{
							' '
						});
						text += text2.Replace("-", "");
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return text;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000080E0 File Offset: 0x000062E0
		private static string GetCpuIdentifier()
		{
			string text = "";
			try
			{
				using (ManagementObjectCollection instances = new ManagementClass("win32_processor").GetInstances())
				{
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						string text2 = ((ManagementObject)managementBaseObject)["ProcessorId"] as string;
						string text3 = ((text2 != null) ? text2.Trim(new char[]
						{
							' '
						}) : null) ?? "";
						text += text3.Replace("-", "");
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return text;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000081B8 File Offset: 0x000063B8
		private static string GetDiskIdentifier()
		{
			string text = "";
			try
			{
				using (ManagementObjectCollection instances = new ManagementClass("win32_diskdrive").GetInstances())
				{
					foreach (ManagementBaseObject managementBaseObject in instances)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (string.Compare(managementObject["InterfaceType"] as string, "IDE", StringComparison.InvariantCultureIgnoreCase) == 0)
						{
							string text2 = (managementObject["SerialNumber"] as string).Trim(new char[]
							{
								' '
							});
							text += text2.Replace("-", "");
						}
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return text;
		}

		// Token: 0x040000F6 RID: 246
		private static string MachineIdString;
	}
}
