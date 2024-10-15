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
    void Start()
    {
        
    }

   
}
