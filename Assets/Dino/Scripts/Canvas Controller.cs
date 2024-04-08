using Dino.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : BasicController, IPointerDownHandler
{
    [SerializeField] private StateManager stateManager;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private Button upButton;
    [SerializeField] private Button downButton;
    [SerializeField] private Text allTimeScore;
    [SerializeField] private Text currentScore;

    public override void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                UpDateUI();
                break;
            case GameState.Running:
                UpDateUI();
                break;
            case GameState.GameOver:
                restartButton.SetActive(true);
                upButton.interactable = false;
                downButton.interactable = false;
                
                ScoreCounter.Stop();
                break;
            case GameState.Restart:
                restartButton.SetActive(false);
                upButton.interactable = true;
                downButton.interactable = true;
                
                ScoreCounter.Start();
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (stateManager.GetState() == GameState.Start)
        {
            stateManager.ChangeState(GameState.Running);
            upButton.interactable = true;
            downButton.interactable = true;
            
            ScoreCounter.Start();
        }
    }
    
    public void Restart()
    {
        stateManager.ChangeState(GameState.Restart);
    }

    private void UpDateUI()
    {
        ScoreCounter.Update();
        currentScore.text = ScoreCounter.GetCurrentScore().ToString("00000");
        allTimeScore.text = "HI " + ScoreCounter.GetAllTimeScore().ToString("00000");
    }
}