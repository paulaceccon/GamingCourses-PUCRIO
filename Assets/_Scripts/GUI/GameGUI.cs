using UnityEngine;
using System.Collections;

//Classe responsável pela interface do jogo propriamente dito

public class GameGUI : MonoBehaviour
{
	
	//--->Variáveis
	//Responsáveis por exibir a quantidade de vidas e de inimgos aniquiliados pelo jogador,
	//respectivamente
	public GUIText lifes, dead_enemies;
	//Variável do tipo SpaceShipController (script)
	private SpaceShipController controllerSS;
	//Variável do tipo EnemiesController (script)
	private EnemiesController controllerE;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Seta a cor do texto que mostra as vidas do jogados
		lifes.guiText.font.material.color = new Color (0, 0, 0, 255);
		//Seta a cor do texto que mostra a quantiade de inimgos aniquilados pelo jogador
		dead_enemies.guiText.font.material.color = new Color (0, 0, 0, 255);
		//A variável controllerSS recebe o componente PauseGameController do GameObject RustyFighter
		//O mesmo se repete para controllerE, mundando o componente recuperado
		//http://docs.unity3d.com/Documentation/ScriptReference/GameObject.GetComponent.html
		controllerSS = GameObject.Find ("RustyFighter").GetComponent<SpaceShipController> ();
		controllerE = GameObject.Find ("EnemiesController").GetComponent<EnemiesController> ();
	}
	
	//--->Função chamada uma vez a cada frame
	void Update ()
	{
		//Obtém a quantidade de vidas do jogador e a atribui ao GUIText a ser mostrado na tela
		lifes.guiText.text = controllerSS.getLives ().ToString ();
		//Obtém a quantidade de inimigos mortos pelo jogador e a atribui ao GUIText a ser mostrado na tela
		dead_enemies.guiText.text = controllerE.getDeadEnemies ().ToString ();
	}
}
