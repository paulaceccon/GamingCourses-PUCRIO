using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Classe responsável por todo o controle da nave principal (controlada pelo jogador)

//Requisita a existência de um componente do tipo CharacterController para o GameObject da nave principal
//http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.html
[RequireComponent( typeof( CharacterController ) )]

public class SpaceShipController : MonoBehaviour
{
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;
	//Guarda o número de vidas do usuário
	public int lives;
	//Faz o link com um prefab (um moldelo) de uma bala
	public Transform bullets;
	//Guarda o número de vezes em que o jogador foi atingido
	private int count_hit;
	//Variável do tipo CharacterController (componente)
	private CharacterController controller;

	//--->Função utilizada para inicialização
	void Start ()
	{
		//Variável preenchida com o CharacterController do GameObject ao qual o script
		//em questão está atado (nave controlada pelo jogador)
		controller = GetComponent<CharacterController> ();
	}
	
	//--->Função chamada uma vez a cada frame
	void Update ()
	{
		//Conforme a movimentação do teclado feita pelo jogador, a nave por ele controlada se move na horizontal
		//vertical e diagonal
		controller.Move (Vector3.forward * Input.GetAxis ("Vertical") * Time.deltaTime * 40);
		controller.Move (Vector3.right * Input.GetAxis ("Horizontal") * Time.deltaTime * 40);
		
		
		Vector3 pos = transform.position;
		pos.y = 0f;
		transform.position = pos;
		
		//Se o usuário clica na tecla ˝alt˝
		if (Input.GetButtonDown ("Fire2")) 
		{
			//Armazena-se a posição do jogador
			float x = transform.position.x;
			float y = transform.position.y;
			float z = transform.position.z;
			//Instância uma bala na posição do jogador, somente um pouco mais à frente no eixo z
			Transform bullet = (Transform) Instantiate (bullets, new Vector3 (x, y, z + 3), Quaternion.identity);
			//Adiciona uma força à bala, de forma que esta possua uma aceleração
			bullet.rigidbody.AddForce (Vector3.forward * 700);
		}
		
		//Se a quantiade de vidas do usuário é igual a zero...
		if (lives == 0)
		{
			//Então a corrotina DestroyCountdown é chamada
			StartCoroutine (DestroyCountdown ());
		}
	}
	
	//--->Função chamada quando um objeto do tipo corpo rígido colide com outro corpo rígido
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnCollisionEnter.html
	void OnCollisionEnter (Collision collision)
	{
		//Se o objeto com o qual a nave do jogador colidiu é uma bala inimiga...
		if (collision.gameObject.name == "EnemyBullet") 
		{
			//Então a varíavel count_hit é incrementada
			count_hit++;
			//Se o valor de count_hit é igual a dez...
			if(count_hit == 10)
			{
				//Então o valor de lifes é decrementado, ou seja, o jogador perde uma vida
				lives--;
				//Então count_hit é novamente zerado
				count_hit = 0;
			}
		}			
	}
	
	//--->Função chamada quando o Collider do objeto entra em um Trigger (não se trata de um corpo rígido, ou
	//seja, com este objeto não há colisão)
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html
	void OnTriggerEnter (Collider collision)
	{
		//Se o objeto com o qual a nave do jogador colidiu é uma vida...
		if (collision.gameObject.name == "Life") 
		{
			//Então lifes é incrementado, ou seja, o jogador ganha uma vida
			lives++;
		}
	}
	
	//--->Função responsável por retornar a quantidade de vidas do jogador
	public int getLives ()
	{
		return lives;
	}
	
	//--->Função responsável por setar a quantidade de vidas do jogador
	public void decreaseLives ()
	{
		lives--;
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
		yield return new WaitForSeconds( 3 );
		//Destrói-se o detonador
		Destroy (det);	
	}
}
