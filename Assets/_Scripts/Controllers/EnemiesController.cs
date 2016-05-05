using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Responsável por todo o controle dos objetos do tipo Enimes (General, Hunter e Bombardier)

public class EnemiesController : MonoBehaviour
{
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;
	//Faz o link com uma lista de possíveis prefabs de naves inimigas
	public Transform[] nave;
	//Controla o número de inimigos a serem criados
	private int numberOfEnemies;
	//Controla o número de inimigos aniquilados
	private int numberOfDeadEnemies;
	//Guarda uma lista das naves inimigas ativas no jogo
	private List <GameObject> enemies;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Inicializa a lista que guardará as naves inimigas
		enemies = new List<GameObject>();
		//Inicializa a variável numberOfDeadEnemies com o valor zero
		numberOfDeadEnemies = 0;
		//Chama o método Intantiate()
		Instantiate ();
	}
	
	//--->Função chamada uma vez a cada frame
	/*void Update ()
	{
		//Se o tempo corrido desde o início do jogo ultrapassa 5 minutos, 
		//considera-se vitória do jogador, neste caso...
		if (Time.timeSinceLevelLoad >= 60 * 5)
			//A tela final do jogo é chamda (isto é uma cena no projeto)
			//http://docs.unity3d.com/Documentation/ScriptReference/Application.LoadLevel.html
			Application.LoadLevel("GameWin");
	}*/
	
	//--->Função que controla a instanciação das naves inimigas do tipo
	void Instantiate ()
	{	
		//A variável numberOfEnemies recebe um valor randômico entre 1 e 5
		numberOfEnemies = (int) Random.Range(1f, 5f);
		
		//Cria-se uma variável do tipo Vector3(float, float, float) que receberá
		//a posição na qual cada nave será criada
		Vector3 pos = new Vector3 (0,0,0);
		//Variável delta, que receberá o valor do espaçamento horizontal entre cada nave
		float delta = 0f;
		//Variável init recebe o valor do tamanho da tela sobre a quantidade de inimigos a ser criados + 1
		float init = (Screen.width / (numberOfEnemies+1));
		//Variável que guardará a posição central da tela, se necessário
		float meio = 0f;
		
		//Se a variável numberOfEnimies recebeu o valor 1, então só uma nave será criada, logo
		//esta será instanciada na posição centra da tela
		if(numberOfEnemies == 1)
		{
		    init = (Screen.width * 0.5f);
		}
		//Se numberOfEnemies é um número ímpar...
 		else if (numberOfEnemies % 2 != 0)
		{
			//Então calcula-se o meio da tela
			meio = Screen.width * 0.5f;
			//E a distância entre as naves, que será dada pelo cálculo abaixo
		    delta = (meio-init)/((numberOfEnemies-1)/2);
		}
		//Se numberOfEnemies é um número par...
		else if (numberOfEnemies % 2 == 0)
		{
			//Então calcula-se a distância entre as naves, que será dada pelo cálculo abaixo
			delta = (Screen.width-2*init)/(numberOfEnemies-1);
		}
		
		//Com os cálculos necessários realizados, faz-se uma transformação da coordenada de tela para 
		//a coordenada de mundo
		//pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		
		//E para cada inimigo...
		for (int i = 0; i < numberOfEnemies; i++) {
			//Com os cálculos necessários realizados, calcula-se agora a posição no qual a nave será criada
			pos.x = (i * delta) + init;
			pos.y = Screen.height + 30f;
			pos.z = 15f;
			//Faz-se uma transformação da coordenada de tela para a coordenada de mundo
			pos = Camera.mainCamera.ScreenToWorldPoint(pos);
			pos.y = 0f;
			
			//A variável objectIndex recebe um valor randômico entre 0 e o número de
			//opções de naves existentes na lista nave
			int objectIndex = Random.Range(0, nave.Length);
			//Então a nave[objectIndex] é criada na posição calculada
			Transform novaNave = (Transform) Instantiate (nave[objectIndex], pos, Quaternion.identity);
			//E esta nave é rotacionada em 180 graus no eixo y para que fique virada de frente para o jogarodr
			novaNave.Rotate(novaNave.up, 180.0f);
			//Esta nave é também adionada na lista que guarda as naves imigas ativas no jogo
			enemies.Add(novaNave.gameObject);
			
			//Por fim, se a nave criada era de índice diferente de zero ou um na lista
			//(naves do tipo Hunter e General), ela recebe uma aceleração de -400
			if (objectIndex != 0 || objectIndex != 1)
			{
				novaNave.rigidbody.AddForce (Vector3.forward * -400);
			}
			//Caso ela seja do tipo Bombardier, recebe uma aceleração de -450
			if (objectIndex == 1)
			{
				novaNave.rigidbody.AddForce (Vector3.forward * -450);
			}
		}
	}
	
	//--->Função responsável por controlar o número de naves inimigas aniquiladas
	//e quando novos inimigos devem ser criados
	//--->Parâmetros: bool kill (true se o jogador aniquilou o inimigo, falso se o inimigo acertou 
	//o jogador - em ambos os casos o inimigo é destruído, por isso a existência desse parâmetro),
	//GameObject enemie (referência ao inimigo destruído)
	public void dead (bool kill, GameObject enemie)
	{	
		//Como, de qualquer forma, o inimigo foi destruído, ele é removido da lista
		//de inimigos ativos
		enemies.Remove(enemie);
		
		//Se o inimigo foi morto pelo tiro do jogador, e não por um choque entre ambos...
		if (kill)
			//A variável responsável por armazenar o número de inimigos aniquilados pelo
			//usuário é incrementada
			numberOfDeadEnemies++;
		
		//Se a lista de inimigos ativos está vazia, o método Instantiate() é novamente
		//chamado
		if (enemies.Count == 0)
			Instantiate ();
	}
	
	//--->Função responsável retornar o número de inimigos aniquilados pelo jogador
	public int getDeadEnemies ()
	{
		return numberOfDeadEnemies;
	}
	
	//--->Função responsável por destruir a referência, bem como a instância 
	//de todos os inimigos ativos (utilizada no fim do jogo)
	public void killAll () 
	{
		while (enemies.Count != 0)	
		{
			Destroy(enemies[0]);
			enemies.RemoveAt(0);
		}
	}
	
	//--->Função responsável por simular a destruição do inimigo
	private IEnumerator DestroyCountdown ()
	{
		//Obtém-se a posição do inimigo aniquilado
		float x = transform.position.x;
		float y = transform.position.y;
		float z = transform.position.z;
		//Destrói-se a instância deste inimigo
		Destroy (gameObject);
		//Cria uma exeplosão a partir do prefab de um detonador existente na variável detonator
		Transform det = (Transform) Instantiate (detonator, new Vector3 (x, y, z), Quaternion.identity);
		yield return new WaitForSeconds( 2 );
		//Destrói-se o detonador
		Destroy (det);	
	}
	
}
