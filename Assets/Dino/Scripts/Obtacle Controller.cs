using System.Collections.Generic;
using Dino.Scripts;
using UnityEngine;

public class ObtacleController : BasicController
{
    [SerializeField] private List<Transform> startPoints;
    private int nextDestroyPointIndex;
    
    [SerializeField] private List<Transform> birdSpawnPoints;
    [SerializeField] private List<GameObject> obtacleList;
    private GameObject currentObtacle;

    private void Start()
    {
        CreateObtacle(0);
    }

    public override void HandleState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Running:
                currentObtacle.transform.Translate(Vector2.left * (Constants.MoveSpeed * Time.deltaTime));
                SpawnObtacle();
                break;
            case GameState.GameOver:
                if(currentObtacle.name == "Bird(Clone)") currentObtacle.GetComponent<Animator>().enabled = false;
                break;
            case GameState.Restart:
                Destroy(currentObtacle);
                CreateObtacle(0);
                break;
        }
    }

    void SpawnObtacle()
    {
        if (currentObtacle.transform.position.x <= startPoints[nextDestroyPointIndex].position.x)
        {
            Destroy(currentObtacle);
            CreateObtacle();
        }
    }

    void CreateObtacle()
    {
        float rand = Random.Range(-1.02f, -0.9f);
        int obtacleIndex = Random.Range(0, obtacleList.Count);
        if (obtacleList[obtacleIndex].name == "Bird")
        {
            currentObtacle = Instantiate(obtacleList[obtacleIndex], birdSpawnPoints[Random.Range(0, birdSpawnPoints.Count)].position, Quaternion.identity);
        }
        else
        {
            currentObtacle = Instantiate(obtacleList[obtacleIndex], new Vector3(transform.position.x, rand, 0), Quaternion.identity);
        }

        nextDestroyPointIndex = Random.Range(0, startPoints.Count);
    }

    void CreateObtacle(int index)
    {
        float rand = Random.Range(-1.02f, -0.9f);
        currentObtacle = Instantiate(obtacleList[index], new Vector3(transform.position.x, rand, 0), Quaternion.identity);
    }
}