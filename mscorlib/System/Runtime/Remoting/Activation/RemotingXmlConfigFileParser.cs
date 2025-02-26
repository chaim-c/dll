﻿using System;
using System.Collections;
using System.Globalization;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089E RID: 2206
	internal static class RemotingXmlConfigFileParser
	{
		// Token: 0x06005D3B RID: 23867 RVA: 0x00146DD7 File Offset: 0x00144FD7
		private static Hashtable CreateSyncCaseInsensitiveHashtable()
		{
			return Hashtable.Synchronized(RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable());
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x00146DE3 File Offset: 0x00144FE3
		private static Hashtable CreateCaseInsensitiveHashtable()
		{
			return new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x00146DF0 File Offset: 0x00144FF0
		public static RemotingXmlConfigFileData ParseDefaultConfiguration()
		{
			ConfigNode configNode = new ConfigNode("system.runtime.remoting", null);
			ConfigNode configNode2 = new ConfigNode("application", configNode);
			configNode.Children.Add(configNode2);
			ConfigNode configNode3 = new ConfigNode("channels", configNode2);
			configNode2.Children.Add(configNode3);
			ConfigNode configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "http client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "http client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "tcp client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "tcp client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode2);
			configNode4.Attributes.Add(new DictionaryEntry("ref", "ipc client"));
			configNode4.Attributes.Add(new DictionaryEntry("displayName", "ipc client (delay loaded)"));
			configNode4.Attributes.Add(new DictionaryEntry("delayLoadAsClientChannel", "true"));
			configNode3.Children.Add(configNode4);
			configNode3 = new ConfigNode("channels", configNode);
			configNode.Children.Add(configNode3);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "http server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Http.HttpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "tcp server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Tcp.TcpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc client"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			configNode4 = new ConfigNode("channel", configNode3);
			configNode4.Attributes.Add(new DictionaryEntry("id", "ipc server"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.Ipc.IpcServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode3.Children.Add(configNode4);
			ConfigNode configNode5 = new ConfigNode("channelSinkProviders", configNode);
			configNode.Children.Add(configNode5);
			ConfigNode configNode6 = new ConfigNode("clientProviders", configNode5);
			configNode5.Children.Add(configNode6);
			configNode4 = new ConfigNode("formatter", configNode6);
			configNode4.Attributes.Add(new DictionaryEntry("id", "soap"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.SoapClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode6.Children.Add(configNode4);
			configNode4 = new ConfigNode("formatter", configNode6);
			configNode4.Attributes.Add(new DictionaryEntry("id", "binary"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode6.Children.Add(configNode4);
			ConfigNode configNode7 = new ConfigNode("serverProviders", configNode5);
			configNode5.Children.Add(configNode7);
			configNode4 = new ConfigNode("formatter", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "soap"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.SoapServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			configNode4 = new ConfigNode("formatter", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "binary"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			configNode4 = new ConfigNode("provider", configNode7);
			configNode4.Attributes.Add(new DictionaryEntry("id", "wsdl"));
			configNode4.Attributes.Add(new DictionaryEntry("type", "System.Runtime.Remoting.MetadataServices.SdlChannelSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"));
			configNode7.Children.Add(configNode4);
			return RemotingXmlConfigFileParser.ParseConfigNode(configNode);
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x00147410 File Offset: 0x00145610
		public static RemotingXmlConfigFileData ParseConfigFile(string filename)
		{
			ConfigTreeParser configTreeParser = new ConfigTreeParser();
			ConfigNode rootNode = configTreeParser.Parse(filename, "/configuration/system.runtime.remoting");
			return RemotingXmlConfigFileParser.ParseConfigNode(rootNode);
		}

		// Token: 0x06005D3F RID: 23871 RVA: 0x00147438 File Offset: 0x00145638
		private static RemotingXmlConfigFileData ParseConfigNode(ConfigNode rootNode)
		{
			RemotingXmlConfigFileData remotingXmlConfigFileData = new RemotingXmlConfigFileData();
			if (rootNode == null)
			{
				return null;
			}
			foreach (DictionaryEntry dictionaryEntry in rootNode.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				a == "version";
			}
			ConfigNode configNode = null;
			ConfigNode configNode2 = null;
			ConfigNode configNode3 = null;
			ConfigNode configNode4 = null;
			ConfigNode configNode5 = null;
			foreach (ConfigNode configNode6 in rootNode.Children)
			{
				string name = configNode6.Name;
				if (!(name == "application"))
				{
					if (!(name == "channels"))
					{
						if (!(name == "channelSinkProviders"))
						{
							if (!(name == "debug"))
							{
								if (name == "customErrors")
								{
									if (configNode5 != null)
									{
										RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode5, remotingXmlConfigFileData);
									}
									configNode5 = configNode6;
								}
							}
							else
							{
								if (configNode4 != null)
								{
									RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode4, remotingXmlConfigFileData);
								}
								configNode4 = configNode6;
							}
						}
						else
						{
							if (configNode3 != null)
							{
								RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode3, remotingXmlConfigFileData);
							}
							configNode3 = configNode6;
						}
					}
					else
					{
						if (configNode2 != null)
						{
							RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode2, remotingXmlConfigFileData);
						}
						configNode2 = configNode6;
					}
				}
				else
				{
					if (configNode != null)
					{
						RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode, remotingXmlConfigFileData);
					}
					configNode = configNode6;
				}
			}
			if (configNode4 != null)
			{
				RemotingXmlConfigFileParser.ProcessDebugNode(configNode4, remotingXmlConfigFileData);
			}
			if (configNode3 != null)
			{
				RemotingXmlConfigFileParser.ProcessChannelSinkProviderTemplates(configNode3, remotingXmlConfigFileData);
			}
			if (configNode2 != null)
			{
				RemotingXmlConfigFileParser.ProcessChannelTemplates(configNode2, remotingXmlConfigFileData);
			}
			if (configNode != null)
			{
				RemotingXmlConfigFileParser.ProcessApplicationNode(configNode, remotingXmlConfigFileData);
			}
			if (configNode5 != null)
			{
				RemotingXmlConfigFileParser.ProcessCustomErrorsNode(configNode5, remotingXmlConfigFileData);
			}
			return remotingXmlConfigFileData;
		}

		// Token: 0x06005D40 RID: 23872 RVA: 0x001475D8 File Offset: 0x001457D8
		private static void ReportError(string errorStr, RemotingXmlConfigFileData configData)
		{
			throw new RemotingException(errorStr);
		}

		// Token: 0x06005D41 RID: 23873 RVA: 0x001475E0 File Offset: 0x001457E0
		private static void ReportUniqueSectionError(ConfigNode parent, ConfigNode child, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NodeMustBeUnique"), child.Name, parent.Name), configData);
		}

		// Token: 0x06005D42 RID: 23874 RVA: 0x00147608 File Offset: 0x00145808
		private static void ReportUnknownValueError(ConfigNode node, string value, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnknownValue"), node.Name, value), configData);
		}

		// Token: 0x06005D43 RID: 23875 RVA: 0x0014762B File Offset: 0x0014582B
		private static void ReportMissingAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportMissingAttributeError(node.Name, attributeName, configData);
		}

		// Token: 0x06005D44 RID: 23876 RVA: 0x0014763A File Offset: 0x0014583A
		private static void ReportMissingAttributeError(string nodeDescription, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_RequiredXmlAttribute"), nodeDescription, attributeName), configData);
		}

		// Token: 0x06005D45 RID: 23877 RVA: 0x00147658 File Offset: 0x00145858
		private static void ReportMissingTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingTypeAttribute"), node.Name, attributeName), configData);
		}

		// Token: 0x06005D46 RID: 23878 RVA: 0x0014767B File Offset: 0x0014587B
		private static void ReportMissingXmlTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingXmlTypeAttribute"), node.Name, attributeName), configData);
		}

		// Token: 0x06005D47 RID: 23879 RVA: 0x0014769E File Offset: 0x0014589E
		private static void ReportInvalidTimeFormatError(string time, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidTimeFormat"), time), configData);
		}

		// Token: 0x06005D48 RID: 23880 RVA: 0x001476BB File Offset: 0x001458BB
		private static void ReportNonTemplateIdAttributeError(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NonTemplateIdAttribute"), node.Name), configData);
		}

		// Token: 0x06005D49 RID: 23881 RVA: 0x001476DD File Offset: 0x001458DD
		private static void ReportTemplateCannotReferenceTemplateError(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TemplateCannotReferenceTemplate"), node.Name), configData);
		}

		// Token: 0x06005D4A RID: 23882 RVA: 0x001476FF File Offset: 0x001458FF
		private static void ReportUnableToResolveTemplateReferenceError(ConfigNode node, string referenceName, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnableToResolveTemplate"), node.Name, referenceName), configData);
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00147722 File Offset: 0x00145922
		private static void ReportAssemblyVersionInfoPresent(string assemName, string entryDescription, RemotingXmlConfigFileData configData)
		{
			RemotingXmlConfigFileParser.ReportError(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_VersionPresent"), assemName, entryDescription), configData);
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x00147740 File Offset: 0x00145940
		private static void ProcessDebugNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (a == "loadTypes")
				{
					RemotingXmlConfigFileData.LoadTypes = Convert.ToBoolean((string)dictionaryEntry.Value, CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x06005D4D RID: 23885 RVA: 0x001477C4 File Offset: 0x001459C4
		private static void ProcessApplicationNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text.Equals("name"))
				{
					configData.ApplicationName = (string)dictionaryEntry.Value;
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "channels"))
				{
					if (!(name == "client"))
					{
						if (!(name == "lifetime"))
						{
							if (!(name == "service"))
							{
								if (name == "soapInterop")
								{
									RemotingXmlConfigFileParser.ProcessSoapInteropNode(configNode, configData);
								}
							}
							else
							{
								RemotingXmlConfigFileParser.ProcessServiceNode(configNode, configData);
							}
						}
						else
						{
							RemotingXmlConfigFileParser.ProcessLifetimeNode(node, configNode, configData);
						}
					}
					else
					{
						RemotingXmlConfigFileParser.ProcessClientNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessChannelsNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D4E RID: 23886 RVA: 0x00147900 File Offset: 0x00145B00
		private static void ProcessCustomErrorsNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text = dictionaryEntry.Key.ToString();
				if (text.Equals("mode"))
				{
					string text2 = (string)dictionaryEntry.Value;
					CustomErrorsModes mode = CustomErrorsModes.On;
					if (string.Compare(text2, "on", StringComparison.OrdinalIgnoreCase) == 0)
					{
						mode = CustomErrorsModes.On;
					}
					else if (string.Compare(text2, "off", StringComparison.OrdinalIgnoreCase) == 0)
					{
						mode = CustomErrorsModes.Off;
					}
					else if (string.Compare(text2, "remoteonly", StringComparison.OrdinalIgnoreCase) == 0)
					{
						mode = CustomErrorsModes.RemoteOnly;
					}
					else
					{
						RemotingXmlConfigFileParser.ReportUnknownValueError(node, text2, configData);
					}
					configData.CustomErrors = new RemotingXmlConfigFileData.CustomErrorsEntry(mode);
				}
			}
		}

		// Token: 0x06005D4F RID: 23887 RVA: 0x001479CC File Offset: 0x00145BCC
		private static void ProcessLifetimeNode(ConfigNode parentNode, ConfigNode node, RemotingXmlConfigFileData configData)
		{
			if (configData.Lifetime != null)
			{
				RemotingXmlConfigFileParser.ReportUniqueSectionError(node, parentNode, configData);
			}
			configData.Lifetime = new RemotingXmlConfigFileData.LifetimeEntry();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "leaseTime"))
				{
					if (!(a == "sponsorshipTimeout"))
					{
						if (!(a == "renewOnCallTime"))
						{
							if (a == "leaseManagerPollTime")
							{
								configData.Lifetime.LeaseManagerPollTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
							}
						}
						else
						{
							configData.Lifetime.RenewOnCallTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
						}
					}
					else
					{
						configData.Lifetime.SponsorshipTimeout = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
					}
				}
				else
				{
					configData.Lifetime.LeaseTime = RemotingXmlConfigFileParser.ParseTime((string)dictionaryEntry.Value, configData);
				}
			}
		}

		// Token: 0x06005D50 RID: 23888 RVA: 0x00147AF8 File Offset: 0x00145CF8
		private static void ProcessServiceNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "wellknown"))
				{
					if (name == "activated")
					{
						RemotingXmlConfigFileParser.ProcessServiceActivatedNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessServiceWellKnownNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D51 RID: 23889 RVA: 0x00147B78 File Offset: 0x00145D78
		private static void ProcessClientNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "url"))
				{
					if (!(a == "displayName"))
					{
					}
				}
				else
				{
					text = (string)dictionaryEntry.Value;
				}
			}
			RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = configData.AddRemoteAppEntry(text);
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "wellknown"))
				{
					if (name == "activated")
					{
						RemotingXmlConfigFileParser.ProcessClientActivatedNode(configNode, configData, remoteAppEntry);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessClientWellKnownNode(configNode, configData, remoteAppEntry);
				}
			}
			if (remoteAppEntry.ActivatedObjects.Count > 0 && text == null)
			{
				RemotingXmlConfigFileParser.ReportMissingAttributeError(node, "url", configData);
			}
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x00147C9C File Offset: 0x00145E9C
		private static void ProcessSoapInteropNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (a == "urlObjRef")
				{
					configData.UrlObjRefMode = Convert.ToBoolean(dictionaryEntry.Value, CultureInfo.InvariantCulture);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "preLoad"))
				{
					if (!(name == "interopXmlElement"))
					{
						if (name == "interopXmlType")
						{
							RemotingXmlConfigFileParser.ProcessInteropXmlTypeNode(configNode, configData);
						}
					}
					else
					{
						RemotingXmlConfigFileParser.ProcessInteropXmlElementNode(configNode, configData);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessPreLoadNode(configNode, configData);
				}
			}
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00147DA8 File Offset: 0x00145FA8
		private static void ProcessChannelsNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				if (configNode.Name.Equals("channel"))
				{
					RemotingXmlConfigFileData.ChannelEntry value = RemotingXmlConfigFileParser.ProcessChannelsChannelNode(configNode, configData, false);
					configData.ChannelEntries.Add(value);
				}
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x00147E1C File Offset: 0x0014601C
		private static void ProcessServiceWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			string text3 = null;
			WellKnownObjectMode objMode = WellKnownObjectMode.Singleton;
			bool flag = false;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "displayName"))
				{
					if (!(a == "mode"))
					{
						if (!(a == "objectUri"))
						{
							if (a == "type")
							{
								RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
							}
						}
						else
						{
							text3 = (string)dictionaryEntry.Value;
						}
					}
					else
					{
						string strA = (string)dictionaryEntry.Value;
						flag = true;
						if (string.CompareOrdinal(strA, "Singleton") == 0)
						{
							objMode = WellKnownObjectMode.Singleton;
						}
						else if (string.CompareOrdinal(strA, "SingleCall") == 0)
						{
							objMode = WellKnownObjectMode.SingleCall;
						}
						else
						{
							flag = false;
						}
					}
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "contextAttribute"))
				{
					if (!(name == "lifetime"))
					{
					}
				}
				else
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (!flag)
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_MissingWellKnownModeAttribute"), configData);
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (text3 == null)
			{
				text3 = text + ".soap";
			}
			configData.AddServerWellKnownEntry(text, text2, arrayList, text3, objMode);
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00147FE4 File Offset: 0x001461E4
		private static void ProcessServiceActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (a == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "contextAttribute"))
				{
					if (!(name == "lifetime"))
					{
					}
				}
				else
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(text2))
			{
				RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(text2, "service activated", configData);
			}
			configData.AddServerActivatedEntry(text, text2, arrayList);
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x00148110 File Offset: 0x00146310
		private static void ProcessClientWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "displayName"))
				{
					if (!(a == "type"))
					{
						if (a == "url")
						{
							text3 = (string)dictionaryEntry.Value;
						}
					}
					else
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
					}
				}
			}
			if (text3 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingAttributeError("WellKnown client", "url", configData);
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(text2))
			{
				RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(text2, "client wellknown", configData);
			}
			remoteApp.AddWellKnownEntry(text, text2, text3);
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x00148204 File Offset: 0x00146404
		private static void ProcessClientActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
		{
			string text = null;
			string text2 = null;
			ArrayList arrayList = new ArrayList();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (a == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (name == "contextAttribute")
				{
					arrayList.Add(RemotingXmlConfigFileParser.ProcessContextAttributeNode(configNode, configData));
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			remoteApp.AddActivatedEntry(text, text2, arrayList);
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x0014830C File Offset: 0x0014650C
		private static void ProcessInteropXmlElementNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "xml"))
				{
					if (a == "clr")
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text3, out text4);
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
			}
			if (text3 == null || text4 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
			}
			configData.AddInteropXmlElementEntry(text, text2, text3, text4);
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x001483E8 File Offset: 0x001465E8
		private static void ProcessInteropXmlTypeNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "xml"))
				{
					if (a == "clr")
					{
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text3, out text4);
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
			}
			if (text3 == null || text4 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
			}
			configData.AddInteropXmlTypeEntry(text, text2, text3, text4);
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001484C4 File Offset: 0x001466C4
		private static void ProcessPreLoadNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string typeName = null;
			string text = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string a = dictionaryEntry.Key.ToString();
				if (!(a == "type"))
				{
					if (a == "assembly")
					{
						text = (string)dictionaryEntry.Value;
					}
				}
				else
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out typeName, out text);
				}
			}
			if (text == null)
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_PreloadRequiresTypeOrAssembly"), configData);
			}
			configData.AddPreLoadEntry(typeName, text);
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x00148580 File Offset: 0x00146780
		private static RemotingXmlConfigFileData.ContextAttributeEntry ProcessContextAttributeNode(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			string text = null;
			string text2 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = ((string)dictionaryEntry.Key).ToLower(CultureInfo.InvariantCulture);
				if (text3 == "type")
				{
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
				else
				{
					hashtable[text3] = dictionaryEntry.Value;
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			return new RemotingXmlConfigFileData.ContextAttributeEntry(text, text2, hashtable);
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x00148640 File Offset: 0x00146840
		private static RemotingXmlConfigFileData.ChannelEntry ProcessChannelsChannelNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate)
		{
			string key = null;
			string text = null;
			string text2 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			bool delayLoad = false;
			RemotingXmlConfigFileData.ChannelEntry channelEntry = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = (string)dictionaryEntry.Key;
				if (!(text3 == "displayName"))
				{
					if (!(text3 == "id"))
					{
						if (!(text3 == "ref"))
						{
							if (!(text3 == "type"))
							{
								if (!(text3 == "delayLoadAsClientChannel"))
								{
									hashtable[text3] = dictionaryEntry.Value;
									continue;
								}
								delayLoad = Convert.ToBoolean((string)dictionaryEntry.Value, CultureInfo.InvariantCulture);
								continue;
							}
						}
						else
						{
							if (isTemplate)
							{
								RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
								continue;
							}
							channelEntry = (RemotingXmlConfigFileData.ChannelEntry)RemotingXmlConfigFileParser._channelTemplates[dictionaryEntry.Value];
							if (channelEntry == null)
							{
								RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, dictionaryEntry.Value.ToString(), configData);
								continue;
							}
							text = channelEntry.TypeName;
							text2 = channelEntry.AssemblyName;
							using (IDictionaryEnumerator enumerator2 = channelEntry.Properties.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj = enumerator2.Current;
									DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj;
									hashtable[dictionaryEntry2.Key] = dictionaryEntry2.Value;
								}
								continue;
							}
						}
						RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
					}
					else if (!isTemplate)
					{
						RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
					}
					else
					{
						key = ((string)dictionaryEntry.Value).ToLower(CultureInfo.InvariantCulture);
					}
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			RemotingXmlConfigFileData.ChannelEntry channelEntry2 = new RemotingXmlConfigFileData.ChannelEntry(text, text2, hashtable);
			channelEntry2.DelayLoad = delayLoad;
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "clientProviders"))
				{
					if (name == "serverProviders")
					{
						RemotingXmlConfigFileParser.ProcessSinkProviderNodes(configNode, channelEntry2, configData, true);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessSinkProviderNodes(configNode, channelEntry2, configData, false);
				}
			}
			if (channelEntry != null)
			{
				if (channelEntry2.ClientSinkProviders.Count == 0)
				{
					channelEntry2.ClientSinkProviders = channelEntry.ClientSinkProviders;
				}
				if (channelEntry2.ServerSinkProviders.Count == 0)
				{
					channelEntry2.ServerSinkProviders = channelEntry.ServerSinkProviders;
				}
			}
			if (isTemplate)
			{
				RemotingXmlConfigFileParser._channelTemplates[key] = channelEntry2;
				return null;
			}
			return channelEntry2;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x0014893C File Offset: 0x00146B3C
		private static void ProcessSinkProviderNodes(ConfigNode node, RemotingXmlConfigFileData.ChannelEntry channelEntry, RemotingXmlConfigFileData configData, bool isServer)
		{
			foreach (ConfigNode node2 in node.Children)
			{
				RemotingXmlConfigFileData.SinkProviderEntry value = RemotingXmlConfigFileParser.ProcessSinkProviderNode(node2, configData, false, isServer);
				if (isServer)
				{
					channelEntry.ServerSinkProviders.Add(value);
				}
				else
				{
					channelEntry.ClientSinkProviders.Add(value);
				}
			}
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x001489B4 File Offset: 0x00146BB4
		private static RemotingXmlConfigFileData.SinkProviderEntry ProcessSinkProviderNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate, bool isServer)
		{
			bool isFormatter = false;
			string name = node.Name;
			if (name.Equals("formatter"))
			{
				isFormatter = true;
			}
			else if (name.Equals("provider"))
			{
				isFormatter = false;
			}
			else
			{
				RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_ProviderNeedsElementName"), configData);
			}
			string key = null;
			string text = null;
			string text2 = null;
			Hashtable hashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
			RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = null;
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				string text3 = (string)dictionaryEntry.Key;
				if (!(text3 == "id"))
				{
					if (!(text3 == "ref"))
					{
						if (!(text3 == "type"))
						{
							hashtable[text3] = dictionaryEntry.Value;
							continue;
						}
					}
					else
					{
						if (isTemplate)
						{
							RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
							continue;
						}
						if (isServer)
						{
							sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)RemotingXmlConfigFileParser._serverChannelSinkTemplates[dictionaryEntry.Value];
						}
						else
						{
							sinkProviderEntry = (RemotingXmlConfigFileData.SinkProviderEntry)RemotingXmlConfigFileParser._clientChannelSinkTemplates[dictionaryEntry.Value];
						}
						if (sinkProviderEntry == null)
						{
							RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, dictionaryEntry.Value.ToString(), configData);
							continue;
						}
						text = sinkProviderEntry.TypeName;
						text2 = sinkProviderEntry.AssemblyName;
						using (IDictionaryEnumerator enumerator2 = sinkProviderEntry.Properties.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj = enumerator2.Current;
								DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj;
								hashtable[dictionaryEntry2.Key] = dictionaryEntry2.Value;
							}
							continue;
						}
					}
					RemotingConfigHandler.ParseType((string)dictionaryEntry.Value, out text, out text2);
				}
				else if (!isTemplate)
				{
					RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
				}
				else
				{
					key = (string)dictionaryEntry.Value;
				}
			}
			if (text == null || text2 == null)
			{
				RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
			}
			RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry2 = new RemotingXmlConfigFileData.SinkProviderEntry(text, text2, hashtable, isFormatter);
			foreach (ConfigNode node2 in node.Children)
			{
				SinkProviderData value = RemotingXmlConfigFileParser.ProcessSinkProviderData(node2, configData);
				sinkProviderEntry2.ProviderData.Add(value);
			}
			if (sinkProviderEntry != null && sinkProviderEntry2.ProviderData.Count == 0)
			{
				sinkProviderEntry2.ProviderData = sinkProviderEntry.ProviderData;
			}
			if (isTemplate)
			{
				if (isServer)
				{
					RemotingXmlConfigFileParser._serverChannelSinkTemplates[key] = sinkProviderEntry2;
				}
				else
				{
					RemotingXmlConfigFileParser._clientChannelSinkTemplates[key] = sinkProviderEntry2;
				}
				return null;
			}
			return sinkProviderEntry2;
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x00148C8C File Offset: 0x00146E8C
		private static SinkProviderData ProcessSinkProviderData(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			SinkProviderData sinkProviderData = new SinkProviderData(node.Name);
			foreach (ConfigNode node2 in node.Children)
			{
				SinkProviderData value = RemotingXmlConfigFileParser.ProcessSinkProviderData(node2, configData);
				sinkProviderData.Children.Add(value);
			}
			foreach (DictionaryEntry dictionaryEntry in node.Attributes)
			{
				sinkProviderData.Properties[dictionaryEntry.Key] = dictionaryEntry.Value;
			}
			return sinkProviderData;
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x00148D50 File Offset: 0x00146F50
		private static void ProcessChannelTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (name == "channel")
				{
					RemotingXmlConfigFileParser.ProcessChannelsChannelNode(configNode, configData, true);
				}
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00148DBC File Offset: 0x00146FBC
		private static void ProcessChannelSinkProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
		{
			foreach (ConfigNode configNode in node.Children)
			{
				string name = configNode.Name;
				if (!(name == "clientProviders"))
				{
					if (name == "serverProviders")
					{
						RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(configNode, configData, true);
					}
				}
				else
				{
					RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(configNode, configData, false);
				}
			}
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x00148E40 File Offset: 0x00147040
		private static void ProcessChannelProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData, bool isServer)
		{
			foreach (ConfigNode node2 in node.Children)
			{
				RemotingXmlConfigFileParser.ProcessSinkProviderNode(node2, configData, true, isServer);
			}
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00148E98 File Offset: 0x00147098
		private static bool CheckAssemblyNameForVersionInfo(string assemName)
		{
			if (assemName == null)
			{
				return false;
			}
			int num = assemName.IndexOf(',');
			return num != -1;
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x00148EBC File Offset: 0x001470BC
		private static TimeSpan ParseTime(string time, RemotingXmlConfigFileData configData)
		{
			string time2 = time;
			string a = "s";
			int num = 0;
			char c = ' ';
			if (time.Length > 0)
			{
				c = time[time.Length - 1];
			}
			TimeSpan result = TimeSpan.FromSeconds(0.0);
			try
			{
				if (!char.IsDigit(c))
				{
					if (time.Length == 0)
					{
						RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time2, configData);
					}
					time = time.ToLower(CultureInfo.InvariantCulture);
					num = 1;
					if (time.EndsWith("ms", StringComparison.Ordinal))
					{
						num = 2;
					}
					a = time.Substring(time.Length - num, num);
				}
				int num2 = int.Parse(time.Substring(0, time.Length - num), CultureInfo.InvariantCulture);
				if (!(a == "d"))
				{
					if (!(a == "h"))
					{
						if (!(a == "m"))
						{
							if (!(a == "s"))
							{
								if (!(a == "ms"))
								{
									RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time2, configData);
								}
								else
								{
									result = TimeSpan.FromMilliseconds((double)num2);
								}
							}
							else
							{
								result = TimeSpan.FromSeconds((double)num2);
							}
						}
						else
						{
							result = TimeSpan.FromMinutes((double)num2);
						}
					}
					else
					{
						result = TimeSpan.FromHours((double)num2);
					}
				}
				else
				{
					result = TimeSpan.FromDays((double)num2);
				}
			}
			catch (Exception)
			{
				RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time2, configData);
			}
			return result;
		}

		// Token: 0x04002A08 RID: 10760
		private static Hashtable _channelTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();

		// Token: 0x04002A09 RID: 10761
		private static Hashtable _clientChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();

		// Token: 0x04002A0A RID: 10762
		private static Hashtable _serverChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();
	}
}
