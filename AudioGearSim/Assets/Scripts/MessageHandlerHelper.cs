//﻿using NSSMS;
//using NSSMSUnityProtocol;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Runtime.Serialization;
//using UnityEngine;

//public class MessageHandlerHelper : MonoBehaviour
//{
//    public void Start()
//    {
//        MessageHandler.PopulatePortConnectionLookup();
//    }

//    public void Update()
//    {
//        MessageHandler.Update();
//    }


//}

//public static class MessageHandler
//{
//    private static ConcurrentQueue<UProtocol> _lockFreeSendMessageQueue = new ConcurrentQueue<UProtocol>();
//    public static Dictionary<string, BaseDevice> BaseDeviceLookup { get; private set; } = new Dictionary<string, BaseDevice>();
//    public static Dictionary<string, string> PortConnectionLookup { get; private set; } = new Dictionary<string, string>();


//    public static void PopulatePortConnectionLookup()
//    {
//        string filePath = Application.streamingAssetsPath + "/PortConnections/connections.portconnections";//EditorUtility.OpenFilePanel("Select Port Connections File", Application.streamingAssetsPath + "/PortConnections", "portconnections");
//        try
//        {
//            string dataAsJson = File.ReadAllText(filePath);
//            SerializablePortConnections spc = JsonUtility.FromJson<SerializablePortConnections>(dataAsJson);
//            PopulatePortConnectionLookup(spc);
//        }
//        catch
//        {
//            Debug.LogError("File not found: PortConnections/connections.portconnections");
//        }
//    }

//    public static void PopulatePortConnectionLookup(SerializablePortConnections SerializablePortConnections)
//    {
//        PortConnectionLookup = SerializablePortConnections.ToDictionary();
//    }

//    public static void Update()
//    {
//        while (_lockFreeSendMessageQueue.Count > 0)
//        {
//            UProtocol data;
//            if (_lockFreeSendMessageQueue.TryDequeue(out data))
//            {
//                Logger.Info("SendData");

//                string Recipient;

//                if (PortConnectionLookup.TryGetValue(data.DeviceNameSender + ":" + data.PortNumberSender, out Recipient))
//                {
//                    string DeviceNameRecipient = Recipient.Split(':')[0];
//                    string PortNumberRecipient = Recipient.Split(':')[1];
//                    data.DeviceNameRecipient = DeviceNameRecipient;
//                    data.PortNumberRecipient = PortNumberRecipient;
//                    BaseDevice recipientDevice = BaseDeviceLookup[DeviceNameRecipient];
//                    if (recipientDevice != null)
//                    {
//                        recipientDevice.DataReceivedEvent(data);
//                    }
//                }
//                else
//                {
//                    Debug.LogError("No connection found for " + data.DeviceNameSender + ":" + data.PortNumberSender);
//                }
//            }

//        }
//    }

//    /// <summary>
//    /// Data has been received this method needs to be overridden by the device to retrieve
//    /// </summary>
//    /// <param name="Data">The Protocol data</param>
//    public static void SendData(UProtocol Data)
//    {
//        //Need to do this because of the main thread.
//        _lockFreeSendMessageQueue.Enqueue(Data);
//    }

//    public static Dictionary<string, string> PortConnectionsFromJSON(string json)
//    {
//        Dictionary<string, string> result = new Dictionary<string, string>();
//        string[][] data = JsonUtility.FromJson<string[][]>(json);
//        foreach (string[] lineItem in data)
//        {
//            result.Add(lineItem[0], lineItem[1]);
//        }
//        return result;
//    }

//    public static void ClearPortConnectionLookup()
//    {
//        PortConnectionLookup = new Dictionary<string, string>();
//    }
//}