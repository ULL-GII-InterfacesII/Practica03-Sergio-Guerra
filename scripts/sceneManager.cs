using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneManager : MonoBehaviour
{
    playerController scriptInstance = null;
    public delegate void metodoDelegado();
    public event metodoDelegado EventoDisparoRecibido;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.Space))
        EventoDisparoRecibido();
    }
}
