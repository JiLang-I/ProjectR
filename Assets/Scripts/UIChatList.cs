using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChatList : MonoBehaviour
{
    public TMP_Text Name = null;

    public TMP_Text Message = null;

    public TMP_Text Time = null;

    private UInt64 Index = 0;

    private string Tag = string.Empty;

    public void SetData(UInt64 index, string avatar, string name, string message, string time, string tag, bool is_my = false)
    {
        Index = index;
        Tag = tag;

        if (is_my)
        {
            Name.text = name + " (You)";
        }
        else
        {
            Name.text = name;
        }

        Message.text = message;
        Time.text = time;
    }

    public bool IsEqual(UInt64 index, string tag)
    {
        return Index == index && Tag == tag;
    }

    public void SetMessage(string message)
    {
        Message.text = message;
    }
}
