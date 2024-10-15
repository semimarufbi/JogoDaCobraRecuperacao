using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    public float gridMoveTimeMax;
    private Vector2Int gridMoveDirection;
    private int snakeBodySize;
    private List<Vector2Int> snakePositionList;
    private GameObject snakeBodyPrefab;
    private List<Transform> snakeBodyParts;
    [SerializeField]
    UIManager pontuacaoManager;

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimeMax = .5f;
        gridMoveTimer = gridMoveTimeMax;
        gridMoveDirection = new Vector2Int(1, 0);
        snakePositionList = new List<Vector2Int> { gridPosition };
        snakeBodyParts = new List<Transform>();
        snakeBodySize = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (pontuacaoManager == null)
        {
            pontuacaoManager = FindAnyObjectByType<UIManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandGridMoviment();
    }
    private void HandGridMoviment()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer > gridMoveTimeMax)
        {
            Vector2Int previusHeadPosition = gridMoveDirection;
            gridPosition += gridMoveDirection;

            snakePositionList.Insert(0, previusHeadPosition);

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            //UpdateSnakeBody();
            if (snakePositionList.Count > snakeBodySize)
            {
                snakePositionList.RemoveAt(snakePositionList.Count - 1);
            }
            gridMoveTimer -= gridMoveTimeMax;
        }
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && gridMoveDirection.y != -1)
        {
            gridMoveDirection = new Vector2Int(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && gridMoveDirection.y != 1)
        {
            gridMoveDirection = new Vector2Int(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && gridMoveDirection.x != 1)
        {
            gridMoveDirection = new Vector2Int(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && gridMoveDirection.x != -1)
        {
            gridMoveDirection = new Vector2Int(1, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            GameManager.Instance.OnFoodCollected();
            pontuacaoManager.AdicionarPonto();
            //GrowSnake();
            Destroy(collision.gameObject);
        }
    }
    private void CheckSelfCollision()
    {
        for (int i = 1; i < snakePositionList.Count; i++)
        {
            if (gridPosition == snakePositionList[i])
            {
                GameOver();
                break;
            }
        }
    }
    private void GameOver()
    {
        GameManager.Instance.GameOver();
        this.enabled = false;
    }
    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x)* Mathf.Rad2Deg;
        return n < 0 ? n + 360 : n;
    }
    private void GrowSnake()
    {
        snakeBodySize++;
        Vector2Int tailPosition = snakePositionList[snakePositionList.Count - 1];

       GameObject newBodyPart = Instantiate(snakeBodyPrefab,new Vector3(tailPosition.x,tailPosition.y,0),Quaternion.identity));
        snakeBodyParts.Add(newBodyPart.transform);

    }

}
