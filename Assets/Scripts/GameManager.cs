using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public int diametroDoCampo;
    public int[,] grade;

    GameObject menu, gameover;
    public GameObject foodPrefab;
    private GameObject spawnedFood;
    public Camera mainCamera;
    public GameObject gameOverPanel; // Referência ao painel de Game Over

    public AudioSource gameMusic; // Referência ao AudioSource que toca a música

    void Start()
    {
        // Gera a grade no início do jogo
        GerarGrade();
    }

    public void GerarGrade()
    {
        grade = new int[diametroDoCampo, diametroDoCampo];
        CameraSeguidora();
    }

    public void CameraSeguidora()
    {
        // Centraliza a câmera no campo de jogo
        mainCamera.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);
        mainCamera.orthographicSize = diametroDoCampo / 2f;
    }

    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value);
    }

    public void DefinirVelocidade(string value)
    {
        float novaVelocidade;
        if (float.TryParse(value, out novaVelocidade) && novaVelocidade > 0)
        {
            // Chama o método para definir a velocidade da cobra
            FindObjectOfType<Snake>().DefinirVelocidade(novaVelocidade);
        }
        else
        {
            Debug.LogWarning("Velocidade inválida. Certifique-se de que é um número positivo.");
        }
    }

    public void SpawnFood()
    {
        // Se já houver uma comida gerada, destrói antes de criar uma nova
        if (spawnedFood != null)
        {
            Destroy(spawnedFood);
        }

        // Gera uma posição aleatória dentro dos limites do campo de jogo
        int randomX = Random.Range(0, diametroDoCampo);
        int randomY = Random.Range(0, diametroDoCampo);

        // Instancia a comida na nova posição dentro do grid
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        spawnedFood = Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
    }

    public void OnFoodCollected()
    {
        // Quando a comida for coletada, gera uma nova
        SpawnFood();
    }

    public void GameOver()
    {
        // Ativa o painel de Game Over
        gameOverPanel.SetActive(true);

        // Pausa a música
        if (gameMusic != null && gameMusic.isPlaying)
        {
            gameMusic.Pause();
        }

        // Congela o jogo
        Time.timeScale = 0f; // Congela o tempo
    }

    public void ExitGame()
    {
        // Fecha a aplicação
        Application.Quit();

        // Para o editor do Unity, use:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
