using UnityEngine;
using System.Collections;

//Classe responsável pelo controle dos objetos do tipo Asteroid

public class Asteroids : MonoBehaviour {
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;	
	//Variável do tipo SpaceShipController (script)
	private SpaceShipController controllerSS;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Obtém o componente SpaceShipController do GameObject RustyFighter
		controllerSS = GameObject.Find ("RustyFighter").GetComponent<SpaceShipController> ();	
	}
	
	//--->Função chamada quando um objeto do tipo corpo rígido colide com outro corpo rígido
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnCollisionEnter.html
	void OnCollisionEnter (Collision collision)
	{
		//Se houve uma colisão com um objeto de nome Collider1 ou Collider2 (colisores presentes
		//na nave do jogador)...
		if(collision.gameObject.name == "Collider1" || collision.gameObject.name == "Collider2")
		{
			//Então decrementa-se a quantidade de vida do jogador
			controllerSS.decreaseLives ();
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		//Se houve uma colisão com alguma das barreiras do mundo...
		else if(collision.gameObject.name != "Top" && collision.gameObject.name != "Left" && collision.gameObject.name != "Right")
		{
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		//Em qualquer outro tipo de colisão, com qualquer outro objeto...
		else
		{
			//Destrói-se o asteróide em questão
			Destroy (gameObject);
		}
	}
	
	//--->Função responsável por simular a destruição do jogador
	private IEnumerator DestroyCountdown ()
	{
		//Obtém-se a posição do asteróide 
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		//Destrói-se a instância do asteróide
		Destroy (gameObject);
		//Cria uma exeplosão a partir do prefab de um detonador existente na variável detonator
		Transform det = (Transform) Instantiate (detonator, new Vector3 (x, y, z), Quaternion.identity);
		yield return new WaitForSeconds( 2 );
		//Destrói-se o detonador
		Destroy (det);	
	}
}
