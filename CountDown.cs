using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CountDown : MonoBehaviour
{
    public float Timer = 99.9f;

    public TMP_Text TimeText;

    public InGameGUIManager GUI;

    private void Start()
    {
        Timer *= 2;        
    }

    private void Update()
    {
        if(GUI.InMenu == true)
        {
            return;
        }

        Timer -= Time.deltaTime * 1.8f;

        TimeText.text = $"time : {Timer.ToString()}";

        if(Timer < 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
