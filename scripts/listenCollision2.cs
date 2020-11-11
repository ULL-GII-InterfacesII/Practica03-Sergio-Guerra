using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listenCollision2 : MonoBehaviour
{

    public Transform mytf;
    public playerController scriptInstance = null;
    public int counter = 0;

    void Start()
    {
      mytf = GetComponent<Transform>();
      GameObject tempObj = GameObject.Find("Player");
      scriptInstance = tempObj.GetComponent<playerController>();
      scriptInstance.EventoB += jump;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void jump() {
      Vector3 dummy = new Vector3(0.0f, 1.0f, 0.0f);
      mytf.position += dummy;
      counter++;
      Debug.Log("Contador en objeto B: " + counter);
    }
}
