using UnityEngine;
using System.Collections;

//Classe responsável por controlar o jogo em si

public class GameController : MonoBehaviour {
	
	//--->Variáveis
	//Variável do tipo PauseGameController (script)
	private PauseGame visionPG;
	//Variável do tipo SpaceShipController (script)
	private SpaceShipController controllerSS;
	//Variável do tipo EnemiesController (script)
	private EnemiesController controllerEC;
	//Variável do tipo AsteroidsController (script)
	private AsteroidsController controllerAC;
	//Variável do tipo LifesController (script)
	private LifesController controllerLC;
	//Variável responsável por armazenar o tempo de jogo do jogador
	private float time;
	
	//--->Função chamada quando uma instância do script está sendo carregada
	//http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.Awake.html
	void Awake () 
	{
		//Faz com que o objeto em questão não seja destruído no carregamento de outra cena
		//Neste caso, o GameObject ˝pai˝ do script
    	DontDestroyOnLoad (gameObject.transform.parent);
	}
	
	//--->Função utilizada para inicialização
	void Start () 
	{
		//A variável controllerPG recebe o componente PauseGameController do GameObject PauseGameController
		//O mesmo se repete para as outras variáveis, mundando o componente recuperado
		//http://docs.unity3d.com/Documentation/ScriptReference/GameObject.GetComponent.html
		visionPG = GameObject.Find ("PauseGame").GetComponent<PauseGame> ();
		controllerSS = GameObject.Find ("RustyFighter").GetComponent<SpaceShipController> ();
		controllerEC = GameObject.Find ("EnemiesController").GetComponent<EnemiesController> ();
		controllerAC = GameObject.Find ("AsteroidsController").GetComponent<AsteroidsController> ();
		controllerLC = GameObject.Find ("LifesController").GetComponent<LifesController> ();
	}
	
	//--->Função chamada uma vez a cada frame
	void Update () 
	{
		//Se o usuário apertou a tecla ˝space˝
		if(Input.GetButtonDown("Jump")) 
		{
			//O timeScale do jogo é setado para zero, gerando um efeito de pausa
			//http://docs.unity3d.com/Documentation/ScriptReference/Time-timeScale.html
			Time.timeScale = 0;
			//O controlador controllerPG é ativado
			visionPG.enabled = true;
		}
		
		//Se o controllerSS é diferente de nulo e o número de vidas do jogador é igual a zero...
		if (controllerSS && controllerSS.getLives() == 0)
		{
			//A variável time recebe o tempo corrido desde o início do jogo
			time = Time.timeSinceLevelLoad;
			//Todos os controladores são desativados
			visionPG.enabled = false;
			controllerEC.enabled = false;
			controllerAC.enabled = false;
			controllerLC.enabled = false;
			//É chamada a corrotida LoadGameOver ()
			Application.LoadLevel("GameOver");
		}
		
		//Se o tempo desde o início do jogo é maior ou igual a 5 minutos...
		if (Time.timeSinceLevelLoad >= 5 * 60)
		{
			//Todos os controladores são desativados
			visionPG.enabled = false;
			controllerEC.enabled = false;
			controllerAC.enabled = false;
			controllerLC.enabled = false;
			//É chamada a corrotida LoadGameWin ()
			Application.LoadLevel("GameWin");
		}
		//Se a tecla Escape é pressionada, o jogo é abortado
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	//--->Função responsável por retornar o tempo de jogo corrido até o fim deste
	public float getTime () 
	{
		return time;
	}
	
	//--->Função responsável por retornar do número de inimigos aniquilados pelo jogador
	public int getScore () 
	{
		return controllerEC.getDeadEnemies();
	}
	
}
