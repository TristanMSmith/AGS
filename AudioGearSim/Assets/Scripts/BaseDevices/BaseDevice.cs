using System;
using System.Collections.Generic;
using UnityEngine;
using AGS;


public abstract class BaseDevice : MonoBehaviour, ISimState
{
    public string fullName => GetFullName();
    public abstract string[] _ports { get; }
    public Dictionary<string, string> ports { get; } = new Dictionary<string, string>();
    public event EventHandler<TransmissionArgs> TransmissionsReceived;
    public abstract void HandleBaseDeviceComponentMessage(BaseDeviceComponent BaseDeviceComponent);
    public abstract void HandlePortConnectionMessage();

    public void Start()
    {
        PopulateDictionary();
        TransmissionsReceived += TransmissionReceived;
    }
    void PopulateDictionary()
    {
        MessageHandler.BaseDeviceLookup.Add(GetFullName(), this);
        foreach (string port in _ports)
        {
            ports.Add(port, port);
        }
    }
    protected abstract void TransmissionReceived(object SenderBaseDevice, TransmissionArgs TransmissionArgs);
    public string GetFullName()
    {
        if (gameObject == null)
        {
            return null;
        }
        string result = "";
        List<string> gos = new List<string>();
        GameObject go = gameObject;
        bool hasParent = true;

        do
        {
            gos.Add(go.name);
            try
            {
                go = go.transform.parent.gameObject;
            }
            catch
            {
                hasParent = false;
            }
        }
        while (hasParent);

        for (int i = gos.Count - 1; i >= 0; i--)
        {
            result += gos[i];
            if (i != 0)
            {
                result += "/";
            }
        }
        return result;
    }
    public void TransmissionReceivedEvent(TransmissionProtocol Data)
    {
        TransmissionsReceived?.Invoke(this, new TransmissionArgs(Data));
    }
}