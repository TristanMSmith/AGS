/**MessageHandler**********************************************************************
    Date Created:   4/6/20
    Created By:     Tristan Smith

    Description:
        Handles messages sent from a  


    Updates:
        Version             Date            Name                Comments
        0                   4/6/2020        Tristan Smith       Creation

 ********************************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using System.IO;
using AGS;

public static class MessageHandler
{
    private static ConcurrentQueue<TransmissionProtocol> _lockFreeSendMessageQueue = new ConcurrentQueue<TransmissionProtocol>();
    public static Dictionary<string, BaseDevice> BaseDeviceLookup { get; private set; } = new Dictionary<string, BaseDevice>();
    public static Dictionary<string, string> PortConnectionLookup { get; private set; } = new Dictionary<string, string>();


    public static void PopulatePortConnectionLookup()
    {
        string filePath = Application.streamingAssetsPath + "/PortConnections/connections.portconnections";//EditorUtility.OpenFilePanel("Select Port Connections File", Application.streamingAssetsPath + "/PortConnections", "portconnections");
        try
        {
            string dataAsJson = File.ReadAllText(filePath);
            SerializablePortConnections spc = JsonUtility.FromJson<SerializablePortConnections>(dataAsJson);
            PopulatePortConnectionLookup(spc);
        }
        catch
        {
            UnityEngine.Debug.LogError("File not found: PortConnections/connections.portconnections");
        }
    }

    public static void PopulatePortConnectionLookup(SerializablePortConnections SerializablePortConnections)
    {
        PortConnectionLookup = SerializablePortConnections.ToDictionary();
    }

    public static void Update()
    {
        while (_lockFreeSendMessageQueue.Count > 0)
        {
            TransmissionProtocol data;
            if (_lockFreeSendMessageQueue.TryDequeue(out data))
            {
                UnityEngine.Debug.Log("SendData");

                string Recipient;

                if (PortConnectionLookup.TryGetValue(data.SenderBaseDevice + ":" + data.SenderPort, out Recipient))
                {
                    string RecipientBaseDevice = Recipient.Split(':')[0];
                    string RecipientPort = Recipient.Split(':')[1];
                    data.RecipientBaseDevice = RecipientBaseDevice;
                    data.RecipientPort = RecipientPort;
                    BaseDevice recipientDevice = BaseDeviceLookup[RecipientBaseDevice];
                    if (recipientDevice != null)
                    {
                        recipientDevice.TransmissionReceivedEvent(data);
                    }
                }
                else
                {
                    UnityEngine.Debug.LogError("No connection found for " + data.SenderBaseDevice + ":" + data.SenderPort);
                }
            }

        }
    }

    /// <summary>
    /// Data has been received this method needs to be overridden by the device to retrieve
    /// </summary>
    /// <param name="Data">The Protocol data</param>
    public static void SendData(TransmissionProtocol Data)
    {
        //Need to do this because of the main thread.
        _lockFreeSendMessageQueue.Enqueue(Data);
    }

    public static Dictionary<string, string> PortConnectionsFromJSON(string json)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        string[][] data = JsonUtility.FromJson<string[][]>(json);
        foreach (string[] lineItem in data)
        {
            result.Add(lineItem[0], lineItem[1]);
        }
        return result;
    }

    public static void ClearPortConnectionLookup()
    {
        PortConnectionLookup = new Dictionary<string, string>();
    }
}