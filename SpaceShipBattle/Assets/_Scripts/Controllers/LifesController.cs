using UnityEngine;
using System.Collections;

//Classe responsável pela criação das vidas que podem ser coletadas pelo jogador

public class LifesController : MonoBehaviour {
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de uma vida
	public Transform life;
	//Controlarão o tempo no qual novos asteróides serão criados
	private float timer;
	private float timer_counter;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Inicializa a variável timer_counter com o valor zero
		timer_counter = 0;
	}
	
	//--->Função chamada uma vez a cada frame
	void Update () 
	{
		//Preenche a variável timer com um valor randômico entre 60.0 e 300.0
		timer = Random.Range(60f, 60f * 5f);
		//A variável timer_counter recebe o seu valor anterior somado com o tempo que se passou
		//para completar o último frame 
		//(http://docs.unity3d.com/Documentation/ScriptReference/Time-deltaTime.html)
		timer_counter += Time.deltaTime;
		//Se o valor de timer_counter for maior ou igual ao valor da variável timer...
		if (timer_counter >= timer) {
			//Cria-se uma variável do tipo Vector3(float, float, float)
			//Esta variável receberá a posição na qual a vida será criada no mundo
			Vector3 pos = new Vector3();
			pos.x = Random.Range(10f, Screen.width-10);
			pos.y = Random.Range(10f, Screen.height-10);;
			pos.z = 15;
			//Coordenadas de tela para coordenadas de mundo
			pos = Camera.mainCamera.ScreenToWorldPoint(pos);
			pos.y = 0;
			//Instanciação do modelo da vida
			Transform prefab = (Transform) Instantiate (life, pos, Quaternion.identity);
			prefab.name = "Life";
			//Quando a variável number chega ao valor zero, o valor da variável timer_counter é zerado,
			//e todo o processo se repete
			timer_counter = 0;
		}
	}
}
