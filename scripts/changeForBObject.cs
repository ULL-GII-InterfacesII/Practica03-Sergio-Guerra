using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeForBObject : MonoBehaviour
{
    public playerController scriptInstance = null;
    public GameObject tempObj;
    // Start is called before the first frame update
    void Start()
    {
      tempObj = GameObject.Find("Player");
      scriptInstance = tempObj.GetComponent<playerController>();
      scriptInstance.collisionWithBObject += change;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void change() {
      Renderer myrd = GetComponent<Renderer>();
      Color randomColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f)
      );
      myrd.material.color = randomColor;
      Debug.Log("El jugador ha perdido vida. Ahora tiene: " + --scriptInstance.health + " hp");

    }
}
