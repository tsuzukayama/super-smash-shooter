using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public Text endText;

    static public GameManager singleton;

    private void Awake()    
    {
        singleton = this;
    }

    public void SetEndText(string text)
    {
        endText.text = text;
    }
}
