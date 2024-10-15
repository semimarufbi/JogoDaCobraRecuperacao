using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int diametroDoCampo;
    public int[,] grade;
    public GameObject foodPrefab;
    private GameObject spawnedFood;
    public Camera mainCamera;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
   
    public void GerarGrade() // gera a grade inicial do jogo
    {
        grade = new int[diametroDoCampo,diametroDoCampo];
        CameraSeguidora();
    }
    public void CameraSeguidora()
    {
        // Centraliza a câmera no campo de jogo
        mainCamera.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);
        mainCamera.orthographicSize = 2f;
    }
    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value);
    }
    public void DefinirVelocidade(string value)
    {
        float novaVelocidade;
        if(float.TryParse(value,out novaVelocidade) && novaVelocidade > 0)
        {
            // Chama o método para definir a velocidade da cobra
            FindObjectOfType<Snake>().DefinirVelocidade(novaVelocidade); 
        }
       

    }
    public void SpawnFood()
    {    // Se já houver uma comida gerada, destrói antes de criar uma nova
        if (spawnedFood != null)
        {
            Destroy(spawnedFood);
        }

        // Gera uma posição aleatória dentro dos limites do campo de jogo
        int randomX = Random.Range(0,diametroDoCampo);
        int randomY = Random.Range(0, diametroDoCampo);

        // Instancia a comida na nova posição dentro do grid
        Vector2 spawnPosition = new Vector2(randomX,randomY);
        spawnedFood = Instantiate(foodPrefab,spawnPosition,Quaternion.identity);
    }
      public void OnFoodCollected()
    {
        // QUando a comida for coletada, gera uma nova
        SpawnFood();
    }
    public void GameOver()
    {
        // Ativa o painel de Game Over
        gameOverPanel.SetActive(true);
        // Congela o jogo
        Time.timeScale = 0f;
    }
    public void ExitGame()
    {      // Fecha a aplicação
        Application.Quit();
    }



}
