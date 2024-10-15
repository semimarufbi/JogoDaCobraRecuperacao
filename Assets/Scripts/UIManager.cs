using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao; // Refer�ncia ao componente TextMeshPro
    private int pontuacao; // Vari�vel para armazenar a pontua��o

    private void Start()
    {
        pontuacao = 0; // Inicializa a pontua��o
        AtualizarTextoPontuacao(); // Atualiza o texto na tela
    }

    public void AdicionarPonto()
    {
        pontuacao++; // Incrementa a pontua��o
        AtualizarTextoPontuacao(); // Atualiza o texto na tela
    }

    private void AtualizarTextoPontuacao()
    {
        textoPontuacao.text = "PONTOS: " + pontuacao; // Atualiza o texto exibido
    }
}
