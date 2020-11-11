using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class listeningCollision : MonoBehaviour
{
    public Transform mytf;
    playerController scriptInstance = null;

    void Start() {
      mytf = GetComponent<Transform>();
      GameObject tempObj = GameObject.Find("Player");
      scriptInstance = tempObj.GetComponent<playerController>();
      scriptInstance.EventoA += jump;
    }

    void Update() {}


    void jump() {
      Vector3 dummy = new Vector3(0.0f, 1.0f, 0.0f);
      mytf.position += dummy;
      Debug.Log("¡Soy el objeto A!");
    }
}
