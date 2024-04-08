using System.Collections.Generic;
using Dino.Scripts;
using UnityEngine;

public enum GameState
{
    Start,
    Running,
    GameOver,
    Restart
}

public class StateManager : MonoBehaviour
{
    [SerializeField] private List<BasicController> controllers;
    private GameState currentState;
    
    private float startSpeed;

    void Start()
    {
        currentState = GameState.Start;
        ScoreCounter.SetAllTimeScore(PlayerPrefs.GetFloat("allTimeScore"));
        startSpeed = Constants.MoveSpeed;
    }

    void Update()
    {
        foreach (var controller in controllers) controller.HandleState(currentState);

        if (currentState == GameState.Restart)
        {
            Constants.MoveSpeed = startSpeed;
            Constants.accelerationFrequency = 100;
            Constants.accelerationCount = 0;
            
            ChangeState(GameState.Running);
        }
        else if (currentState == GameState.GameOver)
        {
            PlayerPrefs.SetFloat("allTimeScore", ScoreCounter.GetAllTimeScore());
            PlayerPrefs.Save();
        }
        
        AccelerateGame();
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
    }

    public GameState GetState()
    {
        return currentState;
    }

    public void AccelerateGame()
    {
        float ratio = ScoreCounter.GetCurrentScore() / Constants.accelerationFrequency;
        if (ratio > 1 && ratio > Constants.accelerationCount)
        {
            if (Constants.accelerationFrequency == 100) Constants.accelerationFrequency = 500;
            ++Constants.accelerationCount;
            Constants.MoveSpeed += 0.5f;
            Debug.Log(Constants.MoveSpeed);
        }
    }
    
    
    private void ResetSpeed()
    {
        Constants.MoveSpeed = startSpeed;
        Constants.accelerationCount = 0;
        Constants.accelerationFrequency = 100;
    }
}