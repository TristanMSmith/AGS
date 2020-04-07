/**MessageHandlerHelper**********************************************************************
    Date Created:   4/6/20
    Created By:     Tristan Smith

    Description:
        A MonoBehaviour wrapper for backing static class MessageHandler. 


    Updates:
        Version             Date            Name                Comments
        0                   4/6/2020        Tristan Smith       Creation

 ********************************************************************************************/
using UnityEngine;

public class MessageHandlerHelper : MonoBehaviour
{
    public void Start()
    {
        MessageHandler.PopulatePortConnectionLookup();
    }

    public void Update()
    {
        MessageHandler.Update();
    }
}