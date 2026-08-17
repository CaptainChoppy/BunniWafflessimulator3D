using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public TMP_Text BeesCollectedText;
    public TMP_Text GradeText;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        string grade = "";

        BeesCollectedText.text = $"bees collected : {BeeManager.BeesCollected}";

        if(BeeManager.BeesCollected == 0)
        {
            grade = "L*";
        }
        else if(BeeManager.BeesCollected < 50)
        {
            grade = "F";
        }
        else if (BeeManager.BeesCollected == 100)
        {
            grade = "A* well done";
        }
        else if (BeeManager.BeesCollected > 90)
        {
            grade = "A";
        }
        else if (BeeManager.BeesCollected > 80)
        {
            grade = "B";
        }
        else if (BeeManager.BeesCollected > 50)
        {
            grade = "C";
        }

        GradeText.text = $"your grade : {grade}";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
