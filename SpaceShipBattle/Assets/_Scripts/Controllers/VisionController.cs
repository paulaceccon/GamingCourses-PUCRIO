using UnityEngine;
using System.Collections;

//Classe responsável por colocar as ˝barreiras˝ no jogo, de modo que a nave do jogador não possa ultrapassa a área
//de vida visível

public class VisionController : MonoBehaviour
{
	
	//--->Variáveis
	//Guarda a barreira que ficará situada à esquerda da tela
	public Transform left;
	//Guarda a barreira que ficará situada à direita da tela
	public Transform right;
	//Guarda a barreira que ficará situada no topo da tela
	public Transform top;
	//Guarda a barreira que ficará situada abaixo na tela
	public Transform down;
	
	[SerializeField]
	private float _distance = 15;

	[SerializeField]
	private Vector2 left_screenPercent;
	
	[SerializeField]
	private Vector2 right_screenPercent;
	
	[SerializeField]
	private Vector2 down_screenPercent;
	
	[SerializeField]
	private Vector2 top_screenPercent;

	[SerializeField]
	private Vector2 _screenDisplacement;
	
	//--->Função chamada uma vez a cada frame
	void Update ()
	{	
		//Cria-se uma variável do tipo Vector3(float, float, float), que armazenará
		//a posição das barreiras na tela
		Vector3 pos = new Vector3();
		
		//Calcula-se a posição da barreira esquerda
		pos.x = ( Screen.width * left_screenPercent.x );
		pos.y = ( Screen.height * left_screenPercent.y );
		pos.z = _distance;
		//Coordenadas de tela para coordenadas de mundo
		pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		//Seta-se a posição da barreira esquerda
		left.position = pos;
		
		//Calcula-se a posição da barreira direita
		pos.x = ( Screen.width * right_screenPercent.x );
		pos.y = ( Screen.height * right_screenPercent.y );
		pos.z = _distance;
		//Coordenadas de tela para coordenadas de mundo
		pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		//Seta-se a posição da barreira direita
		right.position = pos;
		
		//Calcula-se a posição da barreira do topo
		pos.x = ( Screen.width * top_screenPercent.x );
		pos.y = ( Screen.height * top_screenPercent.y );
		pos.z = _distance;
		//Coordenadas de tela para coordenadas de mundo
		pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		//Seta-se a posição da barreira topo
		top.position = pos;
		
		//Calcula-se a posição da barreira de baixo
		pos.x = ( Screen.width * down_screenPercent.x );
		pos.y = ( Screen.height * down_screenPercent.y );
		pos.z = _distance;
		//Coordenadas de tela para coordenadas de mundo
		pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		//Seta-se a posição da barreira de baixo
		down.position = pos;

	}	
}
