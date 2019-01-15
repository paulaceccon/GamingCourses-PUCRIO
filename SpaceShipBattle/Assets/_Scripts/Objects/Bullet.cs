using UnityEngine;
using System.Collections;

//Classe responsável pelo controle dos objetos do tipo Bullet

//Requisita a existência de um componente do tipo CharacterController para o GameObject da nave principal
//http://docs.unity3d.com/Documentation/ScriptReference/CharacterController.html
[RequireComponent (typeof(Rigidbody)) ]

public class Bullet : MonoBehaviour {
	
	//--->Variáveis
	//Faz o link com um prefab (um moldelo) de um detonador
	public Transform detonator;
	
	//--->Função utilizada para inicialização
	void Start ()
	{
		//Toca o áudio da bala (som de tiro)
		audio.Play();
	}
	
	//--->Função chamada quando um objeto do tipo corpo rígido colide com outro corpo rígido
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnCollisionEnter.html
	void OnCollisionEnter () 
	{
		//Destrói a bala
		Destroy(gameObject);		
	}
}
