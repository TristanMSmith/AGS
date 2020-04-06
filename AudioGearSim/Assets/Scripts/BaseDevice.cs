using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDevice : MonoBehaviour, ISimState
{
    public string fullName => GetFullName();
    protected bool hasPower = false;
    public abstract void HandleBaseDeviceComponentMessage(BaseDeviceComponent baseDeviceComponent);
    public abstract void HandlePortConnectionMessage();

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

}
