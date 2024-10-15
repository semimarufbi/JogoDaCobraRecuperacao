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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
