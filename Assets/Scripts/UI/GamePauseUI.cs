using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButtom;
    [SerializeField] private Button mainMenuButtom;
    [SerializeField] private Button optionsButton;

    private void Awake() {
        resumeButtom.onClick.AddListener(() => {
            KitchenGameManager.Instance.TogglePauseGame();
        });
        mainMenuButtom.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() => {
            Hide();
            OptionsUI.Instance.Show(Show);//Pass the gamepause show function to the options script so that it can run it for the game pause whenever the options close
        });


    }
    private void Start() {
        KitchenGameManager.Instance.onGamePaused += KitchenGameManager_onGamePaused;
        KitchenGameManager.Instance.onGameUnpaused += KitchenGameManager_onGameUnpaused;
        Hide();
    }

    private void KitchenGameManager_onGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void KitchenGameManager_onGamePaused(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);

        resumeButtom.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
