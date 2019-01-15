using UnityEngine;
using System.Collections;

//Classe responsável por controlar a criação de objetos do tipo asteróides

public class AsteroidsController : MonoBehaviour {
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um asteróide
	public Transform asteroid;
	//Controlarão o tempo no qual novos asteróides serão criados
	private float timer;
	private float timer_counter;
	//Controlará a quantidade de asteróides criados
	private int number;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Inicializa a variável timer_counter com o valor zero
		timer_counter = 0;
	}
	
	//--->Função chamada uma vez a cada frame
	void Update () 
	{
		//Preenche a variável timer com um valor randômico entre 30.0 e 150.0
		timer = Random.Range(30f, 30f * 5f);
		//A variável timer_counter recebe o seu valor anterior somado com o tempo que se passou
		//para completar o último frame 
		//(http://docs.unity3d.com/Documentation/ScriptReference/Time-deltaTime.html)
		timer_counter += Time.deltaTime;
		//Se o valor de timer_counter for maior ou igual ao valor da variável timer...
		if (timer_counter >= timer) 
		{
			//A variável number recebe um valor randômico entre 1 e 3
			//Este valor refere-se a quantidade de asteróides a serem criados
			number = Random.Range(1,3);
			//Enquanto number for maior que zero...
			while (number > 0)
			{
				//Cria-se uma variável do tipo Vector3(float, float, float)
				//Esta variável receberá a posição na qual o asteróide será criado no mundo
				Vector3 pos = new Vector3();
				pos.x = Random.Range(10f, Screen.width-10f);
				pos.y = -10f;
				pos.z = 15;
				//Coordenadas de tela para coordenadas de mundo
				pos = Camera.mainCamera.ScreenToWorldPoint(pos);
				pos.y = 0f;
				//Instanciação do modelo asteróide
				Transform prefab = (Transform) Instantiate (asteroid, pos, Quaternion.identity);
				//Adiciona-se uma força de modo que o objeto se mova, tenha uma aceleração
				prefab.rigidbody.AddForce(Vector3.forward * 200);
				prefab.name = "Asteroid";
				//Decrementa-se o valor da variável number, para controlar o número de asteróides criados
				number--;
			}
			//Quando a variável number chega ao valor zero, o valor da variável timer_counter é zerado,
			//e todo o processo se repete
			timer_counter = 0;
		}
	}
}
