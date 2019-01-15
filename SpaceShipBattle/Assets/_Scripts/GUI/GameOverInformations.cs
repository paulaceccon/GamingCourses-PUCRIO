using UnityEngine;
using System.Collections;

//Classe responsável pela interface da tela de Game Over

public class GameOverInformations : MonoBehaviour {
	
	//--->Variáveis
	//Variável do tipo GameController (script)
	private GameController controller;
	//Responsáveis por exibir a quantidade o score e o tempo de jogo do jogador,
	//respectivamente
	public GUIText score, time;
	
	//--->Função utilizada para inicialização
	void Start () 
	{
		//A variável controller recebe o componente GameController do GameObject GameController
		//http://docs.unity3d.com/Documentation/ScriptReference/GameObject.GetComponent.html
		controller = GameObject.Find ("GameController").GetComponent<GameController> ();
		//Atribui o score do jogador à variável que o exibe na tela
		score.text = "Score: "+controller.getScore().ToString();
		//Atribui o tempo de jogo à variável que o exibe na tela
		float time_counter = controller.getTime()/60f;
		time.text  = "Time: "+time_counter.ToString("0.00");
	}

}
