using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public float speed;
    private Vector2Int gridPosition;
    private float gridMoveTimer;
    public float gridMoveTimerMax;
    private Vector2Int gridMoveDirection;
    private int snakeBodySize;
    private List<Vector2Int> snakePositionList;  // Lista de posições da cobra
    public GameObject snakeBodyPrefab;  // Prefab do corpo da cobra
    private List<Transform> snakeBodyParts;  // Lista de objetos de segmentos do corpo da cobra

    [SerializeField] UIManager pontuacaoManager; // Referência ao PontuacaoManager

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
        gridMoveTimerMax = .5f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
        snakePositionList = new List<Vector2Int> { gridPosition }; // Inicializa com a posição da cabeça
        snakeBodyParts = new List<Transform>();  // Inicializa a lista de segmentos do corpo
        snakeBodySize = 1;  // Inicializa o tamanho da cobra
    }

    private void Start()
    {
        if (pontuacaoManager == null) // Verifica se a referência não foi atribuída
        {
            pontuacaoManager = FindObjectOfType<UIManager>(); // Busca a instância do PontuacaoManager
        }
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();
        CheckSelfCollision();
    }



    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;
        if (gridMoveTimer > gridMoveTimerMax)
        {
            Vector2Int previousHeadPosition = gridPosition;

            // Atualiza a posição da cabeça
            gridPosition += gridMoveDirection;

            // Aplica o teletransporte
            Teleportar();

            // Adiciona a posição anterior à lista
            snakePositionList.Insert(0, previousHeadPosition);

            // Atualiza a posição do transform
            transform.position = new Vector3(gridPosition.x, gridPosition.y, 0);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection) - 90);

            UpdateSnakeBody();

            // Remove a última posição se não houver corpo suficiente
            if (snakePositionList.Count > snakeBodySize)
            {
                snakePositionList.RemoveAt(snakePositionList.Count - 1);
            }

            gridMoveTimer -= gridMoveTimerMax;
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
            GameManager.instance.OnFoodCollected();
            pontuacaoManager.AdicionarPonto(); // Adiciona ponto ao coletar comida
            GrowSnake();
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
        GameManager.instance.GameOver();
        this.enabled = false; // Desativa este script
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return n < 0 ? n + 360 : n;
    }

    private void GrowSnake()
    {
        snakeBodySize++;  // Aumenta o tamanho da cobra
        Vector2Int tailPosition = snakePositionList[snakePositionList.Count - 1]; // Posição da cauda

        // Instancia um novo segmento do corpo
        GameObject newBodyPart = Instantiate(snakeBodyPrefab, new Vector3(tailPosition.x, tailPosition.y, 0), Quaternion.identity);
        snakeBodyParts.Add(newBodyPart.transform);  // Adiciona o novo segmento à lista de partes do corpo
    }

    private void UpdateSnakeBody()
    {
        for (int i = 0; i < snakeBodyParts.Count; i++)
        {
            if (i < snakePositionList.Count) // Verifica se a posição existe
            {
                snakeBodyParts[i].position = new Vector3(snakePositionList[i].x, snakePositionList[i].y, 0);
            }
        }
    }

    private void Teleportar()
    {
        // Log das posições antes do teletransporte
        Debug.Log($"Antes do teletransporte: X: {gridPosition.x}, Y: {gridPosition.y}");

        // Verifica se o objeto saiu dos limites da grade e teleporta para o lado oposto
        if (gridPosition.x < 0)
        {
            gridPosition.x = GameManager.instance.diametroDoCampo - 1; // Teleporta para o lado direito
        }
        else if (gridPosition.x >= GameManager.instance.diametroDoCampo)
        {
            gridPosition.x = 0; // Teleporta para o lado esquerdo
        }

        if (gridPosition.y < 0)
        {
            gridPosition.y = GameManager.instance.diametroDoCampo - 1; // Teleporta para o lado de cima
        }
        else if (gridPosition.y >= GameManager.instance.diametroDoCampo)
        {
            gridPosition.y = 0; // Teleporta para o lado de baixo
        }

        // Log das posições depois do teletransporte
        Debug.Log($"Depois do teletransporte: X: {gridPosition.x}, Y: {gridPosition.y}");
    }

    public void DefinirVelocidade(float novaVelocidade)
    {
        speed = novaVelocidade;

        // Ajuste o tempo de movimento da cobra com base na nova velocidade
        gridMoveTimerMax = 1f / speed;  // Inverte a velocidade para calcular o tempo de movimento
    }
}
