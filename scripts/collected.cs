using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collected : MonoBehaviour
{
    public delegate void metodoDelegado();
    public event metodoDelegado collectedEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider) {
      Destroy(gameObject, 0.0f);
      collectedEvent();
    }
}
