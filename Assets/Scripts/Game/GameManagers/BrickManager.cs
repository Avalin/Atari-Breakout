using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickManager : MonoBehaviour
{
    [SerializeField]
    private Transform brickContainer;
    [SerializeField] 
    private GameObject brickPrefab;
    public List<GameObject> Bricks { get; private set; }

    private void Start()
    {
        LoadBricksIntoScene();
    }

    public void LoadBricksIntoScene() 
    {
        CleanBrickContainer();
        StartCoroutine(SpawnBrickLines());
        StartCoroutine(SpawnBricks());
    }

    public void CleanBrickContainer() 
    {
        foreach (Transform child in brickContainer.transform)
        {
            //Destroy all the children!
            Destroy(child.gameObject);
        }
    }

    private IEnumerator SpawnBricks() 
    {
        yield return new WaitUntil(() => CSVLevelReader.BricksAreLoaded);
        Bricks = new List<GameObject>();
        string[,] bricks = CSVLevelReader.Bricks;
        
        for (int x = 0; x < bricks.GetLength(0); x++)
        {
            for (int y = 0; y < bricks.GetLength(1); y++)
            {
                InstantiateBrick(x, bricks[x, y]);
            }
        }
        ScaleBricks(bricks.GetLength(1));
        AlignBricks();
        yield return null;
    }   

    private void ScaleBricks(int x) 
    {
        //This method should scale according to camera size, divided by x amount of bricks
        float height = Camera.main.orthographicSize * 2;
        float width = height * Screen.width / Screen.height;

        foreach (Transform brickLinePivot in brickContainer) 
        {
            foreach(Transform brick in brickLinePivot.GetChild(0)) 
            {
                SpriteRenderer brickSprite = brick.gameObject.GetComponent<SpriteRenderer>();
                brickSprite.drawMode = SpriteDrawMode.Tiled;
                brickSprite.size = new Vector2(width/x, brickSprite.size.y);
                brick.GetComponent<BoxCollider2D>().size = brickSprite.size;
            }
        }
    }

    private IEnumerator SpawnBrickLines()
    {
        yield return new WaitUntil(() => CSVLevelReader.BricksAreLoaded);
        string[,] bricks = CSVLevelReader.Bricks;

        for (int i = 0; i < bricks.GetLength(0); i++) 
        {
            InstantiateBrickLine();
        }
        yield return null;
    }

    void InstantiateBrickLine()
    {
        GameObject brickPivot = new GameObject("BrickPivot");
        brickPivot.transform.parent = brickContainer.transform;
        brickPivot.transform.localPosition = Vector2.zero;

        GameObject brickLine = new GameObject("BrickLine");
        brickLine.transform.parent = brickPivot.transform;
        brickLine.name = "BrickLine";
    }

    void InstantiateBrick(int brickLineNo, string brickValue)
    {
        GameObject brick = Instantiate(brickPrefab, new Vector2(0, 0), Quaternion.identity) as GameObject;
        brick.transform.parent = brickContainer.GetChild(brickLineNo).GetChild(0);
        BrickMechanics brickMechanics = brick.GetComponent<BrickMechanics>();
        brick.name = "Brick";

        if (brickValue.Equals("x")) brickMechanics.IsPowerUp = true;
        else if (!brickValue.Trim().Equals(""))
        {
            brickMechanics.HitsToBreak = int.Parse(brickValue);
        }
        else
        {
            brickMechanics.HitsToBreak = 0;
            brick.SetActive(false);
        }
        if(brickMechanics.HitsToBreak != 0) Bricks.Add(brick);
    }

    private void AlignBricks() 
    {
        for(int i = 0; brickContainer.transform.childCount > i; i++) 
        {
            Transform brickLine = brickContainer.GetChild(i).GetChild(0);
            SpriteRenderer spriteRendererOfFirstBrick = brickLine.GetChild(0).GetComponent<SpriteRenderer>();
            float y = -(spriteRendererOfFirstBrick.size.y * i);
            float brickLineY = -(spriteRendererOfFirstBrick.size.x);

            Transform lastBrick = brickLine.GetChild(0);
            for (int j = 0; brickLine.childCount > j; j++)
            {
                float x = lastBrick.GetComponent<SpriteRenderer>().size.x * j;
                brickLine.GetChild(j).transform.position = new Vector2(x, brickLine.GetChild(j).transform.position.y);
                lastBrick = brickLine.GetChild(j);
            }
            brickLine.transform.localPosition = new Vector2(-(brickLineY*0.5f), y);
        }
    }

    public void DoFunkyPowerUpThingy()
    {
        foreach (GameObject brick in Bricks)
        {
            Animator animator = brick.GetComponent<Animator>();
            if(animator.enabled) animator.Play("Raaaaaaainbow");
        }
    }

    public void CheckIfBricksAreCleared()
    {
        if (Bricks.Count == 0)
        {
            GameManagers.LevelManager.AdvanceToNextLevel();
        }
    }
}
