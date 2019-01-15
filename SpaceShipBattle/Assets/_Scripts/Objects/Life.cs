using UnityEngine;
using System.Collections;

//Classe responsável pelo controle dos objetos do tipo Life
public class Life : MonoBehaviour {
	
	//--->Função chamada quando o Collider do objeto entra em um Trigger (não se trata de um corpo rígido, ou
	//seja, com este objeto não há colisão)
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html
	void OnTriggerEnter (Collider collider)
	{
		//Destrói a vida
		Destroy(gameObject);
	}
}
