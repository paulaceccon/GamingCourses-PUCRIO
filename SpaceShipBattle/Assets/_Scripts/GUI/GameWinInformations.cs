using UnityEngine;
using System.Collections;

//Classe responsável pela interface da tela Game Win

public class GameWinInformations : MonoBehaviour {

	//--->Variáveis
	//Variável do tipo GameController (script)
	private GameController controller;
	//Responsável por exibir o score do jogador
	public GUIText score;
	
	//--->Função utilizada para inicialização
	void Start () 
	{
		//A variável controller recebe o componente GameController do GameObject GameController
		//http://docs.unity3d.com/Documentation/ScriptReference/GameObject.GetComponent.html
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();
		//Atribui o score do jogador à variável que o exibe na tela
		score.text = "Score: "+controller.getScore().ToString();
	}

}
