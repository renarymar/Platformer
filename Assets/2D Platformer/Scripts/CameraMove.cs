using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    [SerializeField] private GameObject Character;		//Игрок
	[SerializeField] private Vector3 Offset;        //Cоотношение положений игрока и камеры

    private int HP;

    private void Awake()
    {

    }

    void Start () 
    {
		Offset = transform.position - Character.transform.position;         //Присваиваем значение offset
    

    }

    void Update () 
	{
        if (Character != null)
		transform.position = Character.transform.position + Offset;			//Сдвигаем камеру так, чтобы она была на расстоянии offset от игрока

	}
   
}
