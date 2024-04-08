using Dino.Scripts;
using UnityEngine;

public class GroundController : BasicController
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    
    [SerializeField] private GameObject ground;
    private Transform groundEndPoint;

    private void Start()
    {
        ReplaceGround();
    }

    public override void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                // Выполняем необходимые действия при начале игры
                break;
            case GameState.Running:
                ground.transform.Translate(Vector2.left * (Constants.MoveSpeed * Time.deltaTime));
                CheckGroundExpire();
                break;
            case GameState.GameOver:
                // Выполняем необходимые действия при завершении игры
                break;
            case GameState.Restart:
                ReplaceGround();
                break;
        }
    }

    void CheckGroundExpire()
    {
        if (groundEndPoint.position.x <= endPoint.position.x) ReplaceGround();
    }

    void ReplaceGround()
    {
        Destroy(ground);
        CreateGround();
    }

    void CreateGround()
    {
        ground = Instantiate(ground, startPoint.position + new Vector3(10f, 0f, 0f), Quaternion.identity);
        groundEndPoint = ground.transform.GetChild(0);
    }
}