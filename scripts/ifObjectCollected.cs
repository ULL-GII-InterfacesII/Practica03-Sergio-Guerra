using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ifObjectCollected : MonoBehaviour
{
    GameObject tempObj;
    collected scriptInstance;
    // Start is called before the first frame update
    void Start()
    {
      tempObj = GameObject.Find("obstacle");
      scriptInstance = tempObj.GetComponent<collected>();
      scriptInstance.collectedEvent += open;

    }

    // Update is called once per frame
    void Update() {}

    void open() {
      Transform mytf = GetComponent<Transform>();
      mytf.position -= new Vector3(1.0f, 0.0f, 0.0f);
    }
}
