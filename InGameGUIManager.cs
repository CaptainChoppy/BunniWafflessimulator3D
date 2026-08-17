using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class InGameGUIManager : MonoBehaviour
{
    public GameMenuState State = GameMenuState.InGame;

    public Player Player;
    public BeeManager BeeManager;

    public GameObject InGameObject;
    public GameObject CrossViewObject;
    public GameObject ParallelViewObject;
    public GameObject MobileModeButtonsObject;

    public GameObject MenuObject;
    public GameObject MainMenuObject;
    public GameObject OptionsMenuObject;

    private ViewMode ViewMode = ViewMode.None;

    public TMP_Text BeesCollectedText;

    public Toggle MobileModeToggle;

    public bool InMenu
    {
        get
        {
            return State != GameMenuState.InGame;
        }
    }

    private void Start()
    {
        SetMenuActive(false);
    }

    private void Update()
    {
        BeesCollectedText.text = $"bees collected : {BeeManager.BeesCollected}/{BeeManager.NumberOfBees}";

        if(MobileModeToggle.isOn == true)
        {
            MobileModeButtonsObject.SetActive(true);
            Player.MobileMode = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            MobileModeButtonsObject.SetActive(false);
            Player.MobileMode = false;

            if (InMenu == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    public void SetMenuActive(bool active)
    {
        if (!(Player is null))
        {
            Player.FreezeInputs = active;
        }

        if(!(BeeManager is null))
        {
            BeeManager.FreezeSimulation = active;
        }

        Cursor.visible = active;

        InGameObject.SetActive(!active);
        MenuObject.SetActive(active);

        if(active == true)
        {
            Cursor.lockState = CursorLockMode.None;

            OpenMainMenu();

            return;
        }

        State = new GameMenuState("ingame");

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OpenMainMenu()
    {
        MainMenuObject.SetActive(true);
        OptionsMenuObject.SetActive(false);

        State = new GameMenuState("mainmenu");
    }
    public void OpenOptionsMenu()
    {
        MainMenuObject.SetActive(false);
        OptionsMenuObject.SetActive(true);

        State = new GameMenuState("optionsmenu");
    }
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnViewModeDropDownValueChanged(TMP_Dropdown dropdown)
    {
        SetViewMode((ViewMode)(dropdown.value));
    }
    public void OnSensitivityValueChanged(SliderObject slider)
    {
        Options.MouseSensitivity = slider.Slider.value;

        slider.ValueText.text = $"{slider.Slider.value} °/ΔpxΔt";
    }
    public void OnVolumeValueChanged(SliderObject slider)
    {
        Options.Volume = slider.Slider.value / 100.0f;

        slider.ValueText.text = $"{slider.Slider.value}%";
    }

    private void SetViewMode(ViewMode mode)
    {
        ViewMode = mode;

        switch (ViewMode)
        {
            case ViewMode.None:
                CrossViewObject.SetActive(false);
                ParallelViewObject.SetActive(false);
                return;

            case ViewMode.Cross:
                CrossViewObject.SetActive(true);
                ParallelViewObject.SetActive(false);
                return;

            case ViewMode.Parallel:
                CrossViewObject.SetActive(false);
                ParallelViewObject.SetActive(true);
                return;
        }
    }
}

public class GameMenuState
{
    public string Name;

    public static readonly GameMenuState InGame = new GameMenuState("ingame");
    public static readonly GameMenuState MainMenu = new GameMenuState("mainmenu");
    public static readonly GameMenuState Options = new GameMenuState("options");

    public GameMenuState(string name)
    {
        Name = name;
    }

    public static bool operator ==(GameMenuState left, GameMenuState right)
    {
        return left.Name == right.Name;
    }
    public static bool operator !=(GameMenuState left, GameMenuState right)
    {
        return (left.Name == right.Name) == false;
    }
}

public enum ViewMode : int
{
    None = 0,
    Cross,
    Parallel
}