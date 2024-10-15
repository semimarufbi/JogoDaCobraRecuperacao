using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int diametroDoCampo;
    public int[,] grade;
    public GameObject foodPrefab;
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

   
}
