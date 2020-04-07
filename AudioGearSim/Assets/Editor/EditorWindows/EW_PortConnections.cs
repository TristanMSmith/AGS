using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;


public class EW_PortConnections : EditorWindow
{

    #region Class Variables
    public Vector2 editorScrollPosition, narrativeScrollPosition;

    public BaseDevice fromDevice, toDevice, filterDevice;
    public static string[] fromPort = new string[] { };
    public static string[] toPort = new string[] { };
    public static string[] filterPort = new string[] { };
    static int fromPortSelected = -1;
    static int toPortSelected = -1;
    static int filterPortSelected = -1;


    string titleBar;
    bool isSaved = false;
    bool isEmpty = true;
    SerializedObject serializedObject;
    SerializedProperty serializedProperty;
    #endregion

    [MenuItem("Tools/Port Connections Editor")]
    static void Init()
    {
        GetWindow<EW_PortConnections>("Port Connections Editor");

        fromPort = new string[] { };
        toPort = new string[] { };
        filterPort = new string[] { };
        fromPortSelected = -1;
        toPortSelected = -1;
        filterPortSelected = -1;
        MessageHandler.ClearPortConnectionLookup();
    }

    private void Awake()
    {
        fromDevice = null;
        toDevice = null;
        filterDevice = null;
        MessageHandler.ClearPortConnectionLookup();
    }

    private void OnGUI()
    {

        serializedObject = new SerializedObject(this);
        int normalTextSize = GUI.skin.label.fontSize;
        titleBar = "Port Connections Editor";


        editorScrollPosition = EditorGUILayout.BeginScrollView(editorScrollPosition);
        EditorGUILayout.BeginVertical();
        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        GUI.skin.label.fontSize = 20;
        GUILayout.FlexibleSpace();
        GUILayout.Label(titleBar);
        GUILayout.FlexibleSpace();
        GUI.skin.label.fontSize = normalTextSize;
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        serializedProperty = serializedObject.FindProperty("fromDevice");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedProperty, GUILayout.MaxWidth(500), GUILayout.MinWidth(250));
        if (EditorGUI.EndChangeCheck())
        {
            try
            {
                fromDevice = serializedProperty.objectReferenceValue as BaseDevice;
                fromPort = GetAvailablePorts(fromDevice);
            }
            catch
            {
                fromDevice = null;
                fromPort = new string[] { };
            }
            fromPortSelected = -1;
        }
        fromPortSelected = EditorGUILayout.Popup(fromPortSelected, fromPort);
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();
        serializedProperty = serializedObject.FindProperty("toDevice");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(serializedProperty, GUILayout.MaxWidth(500), GUILayout.MinWidth(250));
        if (EditorGUI.EndChangeCheck())
        {
            try
            {
                toDevice = serializedProperty.objectReferenceValue as BaseDevice;
                toPort = GetAvailablePorts(toDevice);// fromDevice.ports;
            }
            catch
            {
                toDevice = null;
                toPort = new string[] { };
            }
            toPortSelected = -1;
        }
        toPortSelected = EditorGUILayout.Popup(toPortSelected, toPort);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Connection"))
        {
            if (fromDevice != null && toDevice != null && fromPortSelected >= 0 && toPortSelected >= 0)
            {
                //new PortConnection(fromDevice, fromPort[fromPortSelected], toDevice, toPort[toPortSelected]);
                try
                {
                    MessageHandler.PortConnectionLookup.Add(fromDevice.GetFullName() + ":" + fromPort[fromPortSelected], toDevice.GetFullName() + ":" + toPort[toPortSelected]);
                    MessageHandler.PortConnectionLookup.Add(toDevice.GetFullName() + ":" + toPort[toPortSelected], fromDevice.GetFullName() + ":" + fromPort[fromPortSelected]);
                }
                catch
                {
                    //entry already exists
                    Debug.LogError("Connection already exists: " + fromDevice.GetFullName() + ":" + fromPort[fromPortSelected] + "<>" + toDevice.GetFullName() + ":" + toPort[toPortSelected]);
                }

                ResetPortConnection();
            }
            else
            {
                EditorUtility.DisplayDialog(
                "Add Connection Error",
                "Error: Unable to Add Connection. Make sure that both the from device/port " +
                "and to device/port item values have been selected before attempting to add the connection.",
                "Ok"
            );
            }

        }
        if (GUILayout.Button("Save Port Connections"))
        {
            SavePortConnections();
        }
        if (GUILayout.Button("Load Port Connections"))
        {
            LoadPortConnections();
        }

        EditorGUILayout.EndHorizontal();
        GUILayout.Space(50);
        EditorGUILayout.HelpBox("PortConnection JSON Path: StreamingAssets/PortConnections/connections.portconnections", MessageType.Info);
        GUILayout.Space(15);

        if (MessageHandler.PortConnectionLookup.Count > 0)
        {
            EditorGUILayout.BeginHorizontal();
            serializedProperty = serializedObject.FindProperty("filterDevice");
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedProperty, GUILayout.MaxWidth(500), GUILayout.MinWidth(250));
            if (EditorGUI.EndChangeCheck())
            {
                try
                {
                    filterDevice = serializedProperty.objectReferenceValue as BaseDevice;
                    filterPort = GetAllPorts(filterDevice);
                }
                catch
                {
                    filterDevice = null;
                    filterPort = new string[] { };
                }
                filterPortSelected = 0;
            }
            filterPortSelected = EditorGUILayout.Popup(filterPortSelected, filterPort);
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Space(15);

        DrawDictionary();
        GUILayout.Space(10);
        /*
        narrativeScrollPosition = EditorGUILayout.BeginScrollView(narrativeScrollPosition);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndScrollView();
        */
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

    }

    void ResetPortConnection()
    {
        fromDevice = null;
        toDevice = null;
        filterDevice = null;
        fromPort = new string[] { };
        toPort = new string[] { };
        fromPortSelected = -1;
        toPortSelected = -1;
    }

    string[] GetAvailablePorts(BaseDevice bd)
    {
        List<string> result = new List<string>();
        foreach (string port in bd._ports)
        {
            string deviceName = bd.GetFullName();
            string devicePort = deviceName + ":" + port;
            bool connection = MessageHandler.PortConnectionLookup.ContainsKey(devicePort);

            if (!MessageHandler.PortConnectionLookup.ContainsKey(bd.GetFullName() + ":" + port))
            {
                result.Add(port);
            }
        }
        return result.ToArray();
    }

    string[] ValidateAvailablePorts()
    {
        List<string> result = new List<string>();

        return result.ToArray();
    }

    string[] GetAllPorts(BaseDevice bd)
    {
        List<string> result = new List<string>();
        result.Add("ALL");
        foreach (string port in bd._ports)
        {
            //string deviceName = bd.GetFullName();
            //string devicePort = deviceName + ":" + port;
            //bool connection = MessageHandler.PortConnectionLookup.ContainsKey(devicePort);
            result.Add(port);
        }
        return result.ToArray();
    }

    void DrawDictionary()
    {
        EditorGUILayout.BeginVertical();
        try
        {
            foreach (KeyValuePair<string, string> dictionaryItem in MessageHandler.PortConnectionLookup)
            {
                string filter = null;
                try
                {
                    filter = filterDevice.GetFullName();
                    if (filterPort[filterPortSelected] != "ALL" && filterPortSelected >= 0)
                    {
                        filter += ":" + filterPort[filterPortSelected];
                    }
                }
                catch { }

                if (filterDevice == null || dictionaryItem.Key.Contains(filter) || dictionaryItem.Value.Contains(filter))
                {

                    EditorGUILayout.BeginHorizontal();
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        RemovePortConnection(dictionaryItem.Key);
                        RemovePortConnection(dictionaryItem.Value);
                        fromPort = GetAvailablePorts(fromDevice);
                        toPort = GetAvailablePorts(toDevice);
                    }
                    GUILayout.Label(dictionaryItem.Key + "<>" + dictionaryItem.Value);
                    EditorGUILayout.EndHorizontal();

                }

            }
        }
        catch
        { }
        //EditorGUILayout.HelpBox(dictionary, MessageType.None);
        EditorGUILayout.EndVertical();
    }

    void RemovePortConnection(string key)
    {
        MessageHandler.PortConnectionLookup.Remove(key);
    }

    private void SavePortConnections()
    {
        if (MessageHandler.PortConnectionLookup.Count < 1)
        {
            EditorUtility.DisplayDialog(
                "Save Error",
                "Error: Unable to save. There are no entries in the Port Connections list.",
                "Ok"
            );
            return;
        }

        string filePath = EditorUtility.SaveFilePanel("Save Port Connections", Application.streamingAssetsPath + "/PortConnetions", "", "portconnections");
        if (!string.IsNullOrEmpty(filePath))
        {
            SerializablePortConnections serializablePortConnections = new SerializablePortConnections(MessageHandler.PortConnectionLookup);
            string dataAsJson = JsonUtility.ToJson(serializablePortConnections);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void LoadPortConnections()
    {
        try
        {
            string filePath = EditorUtility.OpenFilePanel("Select Port Connections File", Application.streamingAssetsPath + "/PortConnections", "portconnections");
            if (!string.IsNullOrEmpty(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                SerializablePortConnections spc = JsonUtility.FromJson<SerializablePortConnections>(dataAsJson);
                MessageHandler.PopulatePortConnectionLookup(spc);
            }
        }
        catch { }


    }
}


