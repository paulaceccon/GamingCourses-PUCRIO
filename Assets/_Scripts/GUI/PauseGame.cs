using UnityEngine;
using System.Collections;

//Classe responsável pelo controle da funcionalidade de pause do jogo

public class PauseGame : MonoBehaviour {
	
	//--->Variáveis
	//Variável que guarda o skin do menu de pausa
	public GUISkin skin;
	//Variável que guarda o logo do jogo presente no menu
	public Texture2D logoTexture;
	
	//--->Função responsável pela construção do menu de pausa
	void thePauseMenu() 
	{
    	//Inicializa um grupo com as dimensões e na posição passadas por parâmetro
    	GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height/4, 300, 250));
    
		//Cria um box com a textura do logo do jogo
    	GUI.Box(new Rect(0, 0, 300, 250), "");
    	GUI.Label(new Rect(10, 10, 300, 75), logoTexture);
    
		//Cria o botão ˝Resume˝ e, caso o jogador clique nele...
    	if(GUI.Button(new Rect(55, 100, 180, 40), "Resume")) {
			//O timeScale do jogo é setado para um, finalizando o efeito de pausa
			//http://docs.unity3d.com/Documentation/ScriptReference/Time-timeScale.html
    		Time.timeScale = 1.0f;
			PauseGame controller = GameObject.Find ("PauseGame").GetComponent<PauseGame> ();
			//O controlador de pausa é desativado e retorna-se ao jogo
    		controller.enabled = false;
    	}
    
		//Cria o botão ˝Main Menu˝ e, caso o jogador clique nele...
    	if(GUI.Button(new Rect(55, 150, 180, 40), "Main Menu")) {
			PauseGame controller = GameObject.Find ("PauseGame").GetComponent<PauseGame> ();
			//O controlador de pausa é desativado e retorna-se ao jogo e retorna-se a menu principal
    		controller.enabled = false;
    		Application.LoadLevel("GameStart");
    	}
		
		//Cria o botão ˝Quit˝ e, caso o jogador clique nele...
    	if(GUI.Button(new Rect(55, 200, 180, 40), "Quit")) {
			PauseGame controller = GameObject.Find ("PauseGame").GetComponent<PauseGame> ();
			//O controlador de pausa é desativado e a aplicação é finalizada
    		controller.enabled = false;
    		Application.Quit();
    	}
    
    	GUI.EndGroup(); 
	}
	
	//--->Função chamada para renderizar e lidar com eventos de GUI
	//http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnGUI.html
	void OnGUI () 
	{ 		
		//A skin do menu é setada
    	GUI.skin = skin;
		//O cursor é habilitado como visível
    	Screen.showCursor = true;
		//A função thePauseMenu() é chamada
    	thePauseMenu();
	}
}
