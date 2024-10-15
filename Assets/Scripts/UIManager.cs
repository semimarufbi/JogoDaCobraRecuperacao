using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao; // referencia ao componente TextMeshPro
    private int pontuacao; // Variavel para armazenar a pontuação

    // Start is called before the first frame update
    void Start()
    {
        pontuacao = 0; //inicializa a pontuaçao
        AtualizarTextoPontuacao();

    }
    public void AdicionarPonto()
    {
        pontuacao++; // incrementa a pontuação
        AtualizarTextoPontuacao();
    }
    private void AtualizarTextoPontuacao()
    {
        textoPontuacao.text = "PONTOS: " + pontuacao;
    }
}
