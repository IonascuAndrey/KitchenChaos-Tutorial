using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    public event EventHandler onStateChanged;
    public event EventHandler onGamePaused;
    public event EventHandler onGameUnpaused;
    public static KitchenGameManager Instance { get; private set; }
    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 100f;
    private bool isGamePaused = false;

    private void Awake() {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) {
        if (state == State.WaitingToStart) {
            state = State.CountdownToStart;
            onStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        TogglePauseGame();
    }

    public void TogglePauseGame() {
        isGamePaused = !isGamePaused;
        if(isGamePaused) {
            Time.timeScale = 0f;//the timeScale is a multiplyer deltaTime is calculated with, and considering the whole game uses deltaTime
            //To do stuff, changing it to 0 pauses everything
            onGamePaused?.Invoke(this, EventArgs.Empty);
        } else {
            Time.timeScale = 1f;
            onGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
                            
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    onStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0) {
                    state = State.GameOver;
                    onStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:

                break;
        }
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive() {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer() {
        return countdownToStartTimer;
    }

    public bool IsGameOver() {
        return state==State.GameOver;
    }

    public float GetGamePlayingTimerNormalized() {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }

}
