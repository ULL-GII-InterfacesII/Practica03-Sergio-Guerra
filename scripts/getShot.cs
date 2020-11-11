using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getShot : MonoBehaviour
{
    sceneManager scriptInstance = null;
    public Rigidbody myrb;
    public Transform mytf;
    public Transform tf;

    // Start is called before the first frame update
    void Start()
    {
      mytf = GetComponent<Transform>();
      myrb = GetComponent<Rigidbody>();
      GameObject tempObj = GameObject.Find("Player");
      tf = tempObj.GetComponent<Transform>();
      tempObj = GameObject.Find("Main Camera");
      scriptInstance = tempObj.GetComponent<sceneManager>();
      scriptInstance.EventoDisparoRecibido += gotShot;
    }

    void Update() {}

    void gotShot() {
      if (mytf != null) {
        Vector3 direction = mytf.position - tf.position;
        if (direction.magnitude < 1.0f)
          Destroy(gameObject, 0.0f);
        else if (direction.magnitude < 4.0f)
          myrb.AddForce(direction.x, direction.y, direction.z, ForceMode.Impulse);
      }
    }
}
