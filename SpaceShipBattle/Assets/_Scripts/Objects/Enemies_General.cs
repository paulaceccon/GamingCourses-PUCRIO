using UnityEngine;
using System.Collections;

//Classe responsável pelo controle dos objetos do tipo General

public class Enemies_General : MonoBehaviour
{
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de uma bala
	public Transform bullets;
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;
	//Variável do tipo EnemiesController (script)
	private EnemiesController controllerEC;
	//Variável do tipo DetonatorSound (script)
	private DetonatorSound controllerDS;
	//Variável do tipo SpaceShipController (script)
	private SpaceShipController controllerSS;
	public float timer;
	private float timer_counter;
	private float wait_timer = 0;
	private float wait_timer_base = 2;
	public int count_bullet = 0;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Seta a variável timer_counter com zero
		timer_counter = 0;
		//Obtém o componente EnemiesController do GameObject EnemiesController
		GameObject ec = GameObject.Find ("EnemiesController");
		if (ec)
			controllerEC = ec.GetComponent<EnemiesController> ();
		//Obtém o componente SpaceShipController do GameObject RustyFighter
		GameObject rf = GameObject.Find ("RustyFighter"); 
		if (rf)
			controllerSS = rf.GetComponent<SpaceShipController> ();	
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
		
		//Se houve uma colisão com uma bala atirada pelo jogador...
		else if (collision.gameObject.name == "Bullet(Clone)")// || collision.gameObject.name == "Collider1" || collision.gameObject.name == "Collider2") 
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool true,
			//uma vez que este inimigo foi morto por uma colisão com o jogador
			controllerEC.dead (true, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());		
		} 
		//Se houve uma colisão com um asteróide, com um outro inimigo do tipo comum (Enemy_General) 
		//ou com um camicase (Hunter)...
		else if (collision.gameObject.name == "Asteroid" || collision.gameObject.name == "Bombardier(Clone)" || collision.gameObject.name == "Hunter(Clone)")
		{
			//Chama-se a função que decrementa o número de inimigos existentes, passando o bool false,
			//uma vez que este inimigo não foi morto pelo jogador, mas por uma colisão com outro inimigo/objeto 
			controllerEC.dead (false, gameObject);
			//Chama-se a corrotina DestroyCountdown
			StartCoroutine (DestroyCountdown ());
		}
		//Se houve uma colisão com uma das barreiras do mundo
		else if (collision.gameObject.name == "Left" || collision.gameObject.name == "Right" || collision.gameObject.name == "Down") {
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
	
	//--->Função responsável por simular a destruição da nave General
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
	
	private IEnumerator wait ()
	{
		yield return new WaitForSeconds(3);
		count_bullet = 0;
	}
	
	//--->Função chamada uma vez a cada frame
	void Update ()
	{
		if (wait_timer > 0)
		{
			wait_timer -= Time.deltaTime;
			return;
		}
		//A variável timer_counter recebe o seu valor anterior somado com o tempo que se passou
		//para completar o último frame 
		//(http://docs.unity3d.com/Documentation/ScriptReference/Time-deltaTime.html)
		timer_counter += Time.deltaTime;
		//Se o valor de timer_counter for maior ou igual ao valor da variável timer...
		if (timer_counter >= timer && count_bullet < 5) 
		{
			//Debug.Log(count_bullet);
			//Armazena-se a posição da nave inimiga
			float x = transform.position.x;
			float y = transform.position.y;
			float z = transform.position.z;
			//Intancia-se uma bala nesta posição, variando somente a coordenada do eixo z
			Transform bullet = (Transform) Instantiate (bullets, new Vector3 (x, y, z - 7), Quaternion.identity);
			bullet.name = "EnemyBullet";
			//Chama-se a função SetLayerRecursively, passando como parâmetro a bala criada e o número da layer
			SetLayerRecursively(bullet.gameObject, 20);
			//Adiona-se uma força à bala, para que ela tenha uma aceleração
			bullet.rigidbody.AddForce (Vector3.forward * -700);
			//Zera-se o timer_counter e todo o processo recomeça
			timer_counter = 0;
			count_bullet++;
		}
		
		if (count_bullet == 5)
		{
			//StartCoroutine(wait());	
			wait_timer = wait_timer_base;
			count_bullet = 0;
		}
		
	}
	
	
	//--->Função responsável por setar a layer de um objeto e de seus filhos
	void SetLayerRecursively(GameObject obj, int newLayer)
    {
		//Se o objeto passado é nulo...
        if (null == obj)
        {
			//Não se faz nada, saindo da função
            return;
        }
		//Seta-se a layer do objeto
        obj.layer = newLayer;
		//Para cada filho deste objeto...
        foreach (Transform child in obj.transform)
        {
			//Se ele é null, nada se faz
            if (null == child)
            {
                continue;
            }
			//Se não chama-se este método também par os filhos dos filhos do objeto
            SetLayerRecursively(child.gameObject, newLayer);
        }
	}
    
}
