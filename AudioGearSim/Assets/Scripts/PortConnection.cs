using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class SerializablePortConnection
{
    public string fromDevicePort, toDevicePort;
    public SerializablePortConnection(string FromDevicePort, string ToDevicePort)
    {
        fromDevicePort = FromDevicePort;
        toDevicePort = ToDevicePort;
    }
}

[Serializable]
public class SerializablePortConnections
{
    public List<SerializablePortConnection> serializablePortConnections = new List<SerializablePortConnection>();
    public SerializablePortConnections(Dictionary<string, string> dictionary)
    {
        foreach (KeyValuePair<string, string> entry in dictionary)
        {
            serializablePortConnections.Add(new SerializablePortConnection(entry.Key, entry.Value));
        }
    }

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (SerializablePortConnection serializablePortConnection in serializablePortConnections)
        {
            result.Add(serializablePortConnection.fromDevicePort, serializablePortConnection.toDevicePort);
        }
        return result;
    }

}