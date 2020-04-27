using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AGS
{
    public static class Debug
    {
        static bool isDev {get;} = true;

        public static bool AudioListenerCheck()
        {
            if (!isDev)
            {
                return true;
            }
            if (Camera.main.GetComponent<AudioListener>() == null)
            {
                UnityEngine.Debug.Log("Camera doesn't have an audio listener.");
                return false;
            }
            return true;
        }

        public static bool RaycasterCheck()
        {
            if (!isDev)
            {
                return true;
            }
            if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
            {
                UnityEngine.Debug.Log("Camera doesn't have a physics raycaster.");
                return false;
            }
            return true;
        }
    }
}

