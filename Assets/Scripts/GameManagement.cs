using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public Text endText;

    private static GameManagement _instance;
    public static GameManagement Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetEndText(string text)
    {
        endText.text = text;
    }
}
