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
        snakePositionList = new List<Vector2Int> {gridPosition };
        snakeBodyParts = new List<Transform>();
        snakeBodySize = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(pontuacaoManager == null) 
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
        if(gridMoveTimer > gridMoveTimeMax)
        {
            Vector2Int previusHeadPosition = gridMoveDirection;
            gridPosition += gridMoveDirection;
            
             snakePositionList.Insert(0, previusHeadPosition);

            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            //transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            //UpdateSnakeBody();
            if(snakePositionList.Count > snakeBodySize)
            {
                snakePositionList.RemoveAt(snakePositionList.Count - 1);
            }
            gridMoveTimer -= gridMoveTimeMax;
        }
    }
}
