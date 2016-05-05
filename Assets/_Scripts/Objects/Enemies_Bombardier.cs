using UnityEngine;
using System.Collections;

public class Enemies_Bombardier : MonoBehaviour {
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um míssil
	public Transform missiles;
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;
	//Variável do tipo EnemiesController (script)
	private EnemiesController controllerEC;
	//Variável do tipo SpaceShipController (script)
	private SpaceShipController controllerSS;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Obtém o componente EnemiesController do GameObject EnemiesController
		controllerEC = GameObject.Find ("EnemiesController").GetComponent<EnemiesController> ();
		//Obtém o componente SpaceShipController do GameObject RustyFighter
		controllerSS = GameObject.Find ("RustyFighter").GetComponent<SpaceShipController> ();
		
		//Guarda a posição do inimigo do tipo Bombardier
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		//Instancia um míssil nessa posição, variando somente no eixo z
		Transform missile = (Transform) Instantiate (missiles, new Vector3 (x, y, z - 7), Quaternion.identity);
		missile.name = "EnemyMissile";
		//Debug.Log(missile.collider.name);
	}
	
	//--->Função chamada quando um objeto do tipo corpo rígido colide com outro corpo rígido
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnCollisionEnter.html
	void OnCollisionEnter (Collision collision)
	{
		//Se houve uma colisão com um objeto de nome Collider1 ou Collider2 (colisores presentes
		//na nave do jogador)...
		if(collision.gameObject.name == "Collider1" || collision.gameObject.name == "Collider2")
		{
			Debug.Log("Collider1");
			//Então decrementa-se a quantidade de vida do jogador
			controllerSS.decreaseLives ();
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool true,
			//uma vez que este inimigo foi morto por uma colisão com o jogador
			controllerEC.dead (true, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		else if(collision.gameObject.name == "RustyFighter")
		{
			Debug.Log("RustyFighter");
			//Então decrementa-se a quantidade de vida do jogador
			controllerSS.decreaseLives ();
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool true,
			//uma vez que este inimigo foi morto por uma colisão com o jogador
			controllerEC.dead (true, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		//Se houve uma colisão com uma bala atirada pelo jogador
		else if (collision.gameObject.name == "Bullet(Clone)")// || collision.gameObject.name == "Collider1" || collision.gameObject.name == "Collider2") {
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool true,
			//uma vez que este inimigo foi morto por uma colisão com o jogador
			controllerEC.dead (true, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());		
		} 
		//Se houve uma colisão com um asteróide, com um outro inimigo do tipo comum (Enemy_General) ou com um camicase (Hunter)...
		else if (collision.gameObject.name == "Asteroid" || collision.gameObject.name == "Enemy(Clone)" || collision.gameObject.name == "Hunter(Clone)")
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool false,
			//uma vez que este inimigo não foi morto pelo jogador, mas por uma colisão com outro inimigo/objeto 
			controllerEC.dead (false, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		//Se houve uma colisão com uma das barreiras do mundo
		else if (collision.gameObject.name == "Left" || collision.gameObject.name == "Right" || collision.gameObject.name == "Down") 
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool false,
			//uma vez que este inimigo não foi morto pelo jogador, mas está fora do campo de visão da tela
			controllerEC.dead (false, gameObject);
			//Destrói-se este objeto
			Destroy (gameObject);	
		}
	}
	
	//--->Função chamada quando o Collider do objeto entra em um Trigger (não se trata de um corpo rígido, ou
	//seja, com este objeto não há colisão)
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html
	void OnTriggerEnter (Collider collision)
	{
		//Se o objeto com o qual a nave do jogador colidiu é um dos colisores da nave Bombardier...
	    if (collision.gameObject.name == "Hunter1" || collision.gameObject.name == "Hunter2")
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool false,
			//uma vez que este inimigo não foi morto pelo jogador, mas por uma colisão com outro inimigo 
			controllerEC.dead (false, gameObject);
			controllerEC.dead (false, collision.transform.parent.parent.gameObject);
			//Destrói-se este inimigo
			Destroy(collision.transform.parent.parent.gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
	}
	
	//--->Função responsável por simular a destruição do jogador
	private IEnumerator DestroyCountdown ()
	{
		//Obtém-se a posição do jogador 
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		//Destrói-se a instância do jogador
		Destroy (gameObject);
		//Cria uma exeplosão a partir do prefab de um detonador existente na variável detonator
		Transform det = (Transform) Instantiate (detonator, new Vector3 (x, y, z), Quaternion.identity);
		yield return new WaitForSeconds( 2 );
		//Destrói-se o detonador
		Destroy (det);	
	}
}
