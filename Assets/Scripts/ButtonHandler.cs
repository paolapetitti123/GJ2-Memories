using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonHandler : MonoBehaviour
{
    public Conversation conversation;

    public void OnButtonClick()
    {
        conversation.OnButtonClick();
    }
}