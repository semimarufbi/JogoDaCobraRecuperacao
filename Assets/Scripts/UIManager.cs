using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textoPontuacao; // Referência ao componente TextMeshPro
    private int pontuacao; // Variável para armazenar a pontuação

    private void Start()
    {
        pontuacao = 0; // Inicializa a pontuação
        AtualizarTextoPontuacao(); // Atualiza o texto na tela
    }

    public void AdicionarPonto()
    {
        pontuacao++; // Incrementa a pontuação
        AtualizarTextoPontuacao(); // Atualiza o texto na tela
    }

    private void AtualizarTextoPontuacao()
    {
        textoPontuacao.text = "PONTOS: " + pontuacao; // Atualiza o texto exibido
    }
}
