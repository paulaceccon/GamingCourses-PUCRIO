using UnityEngine;
using System.Collections;

//Classe responsável por setar a plano de fundo do jogo

public class BackgroundController : MonoBehaviour
{
	//--->Variáveis
	[SerializeField]
	//Variável que guarda a textura a ser utilizada
	private GUITexture _backgroundGUITexture;

	//--->Função utilizada para inicialização
	private void Start()
	{
		//Se _backgroundGUITexture é diferente de nulo...
		if( _backgroundGUITexture == null )
		{
			//Desativa-se o script
			this.enabled = false;
		}
		
		//Ativa-se o plano de fundo
		_backgroundGUITexture.enabled = true;
	}

	//--->Função chamada uma vez a cada frame
	private void Update()
	{
		//Seta-se o plano de fundo
		Rect r = new Rect( 0, 0, Screen.width, Screen.height );
		_backgroundGUITexture.pixelInset = r;
	}
}
