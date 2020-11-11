using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNearButton : MonoBehaviour
{
    public Light myLight;
    public GameObject player;
    public Transform playertf;
    public Transform mytf;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.Find("Player");
      playertf = player.GetComponent<Transform>();
      mytf = GetComponent<Transform>();
      myLight = GameObject.Find("Lamp light").GetComponent<Light>();
      myLight.intensity = 0;
    }

    // Update is called once per frame
    void Update() {}

      void OnTriggerStay (Collider collider) {
        float distance = Vector3.Distance(playertf.position, mytf.position);
        if ((distance < 1.5) && (Input.GetKeyDown("l")))
            myLight.intensity = 5;
      }
}
