using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [SerializeField]
    private Transform ballContainer;
    private List<GameObject> pooledBalls;
    [SerializeField]
    private GameObject ballPrefab;
    [SerializeField]
    private int amountOfBallsToPool;
    [SerializeField]
    private Vector2 startPosition = new Vector2(0, -1.3f);

    void Start()
    {
        InitializeBallPool();
    }

    public void AddBallToGame() 
    {
        GameObject ball = GetBallFromPool();
        ball.transform.position = GetStartPosition() + new Vector2(Random.Range(-0.05f, 0.05f), 0);
        ball.SetActive(true);
    }

    public Vector2 GetStartPosition() 
    {
        return startPosition;
    }

    private void InitializeBallPool()
    {
        pooledBalls = new List<GameObject>();
        for (int i = 0; i < amountOfBallsToPool; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.transform.SetParent(ballContainer);
            ball.SetActive(false);
            ball.name = "Ball";
            pooledBalls.Add(ball);
        }
    }

    public int GetActiveBallAmount()
    {
        int ballAmount = 0;
        foreach(Transform ball in ballContainer) 
        {
            if (ball.gameObject.activeInHierarchy) ballAmount++;
        }
        return ballAmount;
    }

    public GameObject GetBallFromPool()
    {
        foreach(GameObject ball in pooledBalls) 
        {
            if (!ball.activeInHierarchy)
            {
                return ball;
            }
        }
        return null;
    }

    public void ResetBall() 
    {
        SetActivateAllBalls(false);
        RespawnBall();
    }

    private void SetActivateAllBalls(bool isActivated)
    {
        foreach (GameObject ball in pooledBalls)
        {
            ball.SetActive(isActivated);
        }
    }

    private void RespawnBall()
    {
        Transform ball = ballContainer.GetChild(0);
        ball.position = startPosition;
        ball.GetComponent<BallMechanics>().PushBallUpwards();
        ball.gameObject.SetActive(true);
    }
}
