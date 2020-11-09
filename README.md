# Practica03-Sergio-Guerra
## Interfaces Inteligentes - ULL
#### Sergio guerra Arencibia


#### 1. Agregar dos objetos en la escena: A y B. Cuando el jugador colisiona con un objeto de tipo B, el objeto A volcará en consola un mensaje. Cuando toca el objeto A se incrementará un contador en el objeto B  
  
Para implementar esta primera funcionalidad, he usado los delegados. El gameObject que contiene y que activa estos delegados es el jugador, este, contiene un delegado
del cual descienden dos eventos (eventoA y eventoB). El eventoA se disparará (como su nombre indica) cuando colisione con un objeto del tipo A, el funcionamiento del eventoB es análogo.  
De tal manera, que el script asociado al jugador queda contiene lo siguiente:  

```c#
    public delegate void metodoDelegado();
    public event metodoDelegado EventoA;
    public event metodoDelegado EventoB;
```  
Las llamadas a estos eventos se efectúan al detectar una colisión, quedando el método OnCollisionEnter como sigue:  
```c# 
void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.name == "DroneA")
       EventoA();
      if (collision.gameObject.name == "DroneB")
        EventoB();
    }
```  
En este punto los eventos ya son disparados cuando se produce la colisión. Solo falta que el objeto A y B se suscriban al evento correspondiente.  
Para ello lo primero que tenemos que hacer desde los scripts asociados a estos objetos es acceder a este delegado. Este acceso se podría realizar desde
la interfaz gráfica, aunque yo he optado por hacerlo puramente mediante código:  
```c# 
      GameObject tempObj = GameObject.Find("Player");
      scriptInstance = tempObj.GetComponent<playerController>();
```   
Como se ve, accedemos al script llamado "playerController" del objeto player.  
A continuación nos sucribimos al evento correspondiente (suscribo un método jump que hace saltar al objeto y escribir un mensaje por pantalla):  
```c#
    scriptInstance.EventoA += jump;
```  
Este método jump es el siguiente:  
```c#  
     void jump() {
      Vector3 dummy = new Vector3(0.0f, 1.0f, 0.0f);
      mytf.position += dummy;
      Debug.Log("¡Soy el objeto A!");
    }
```  
El proceso con el objeto B y su evento correspondiente es análogo, a excepción de que en vez de sacar un mensaje por pantalla, se incrementa un contador.  
  
![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/1ObjetoA.gif)  

![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/1ObjetoB.gif)  
 
 --- 
 
#### 2. Implementar un controlador de escena usando el patrón delegado que gestione las siguientes acciones:  
Para los dos apartados de este punto se ha usado un controlado de escena asignado a la cámara principal.  
Así, el script asociado a la cámara principal contiene las siguientes líneas:  
```c#
    public class sceneManager : MonoBehaviour {
      public delegate void metodoDelegado();
      [...]
```  
Sabiendo dónde tenemos el delegado, proseguimos con los apartados de este punto.  
 
a. Si el jugador dispara, los objetos de tipo A que estén a una distancia media serán desplazados y los que estén a una distancia pequeña serán destruidos. Los umbrales que marcan los límites se deben configurar en el inspector.

Lo primero es crear un evento que dispararemos cuando el jugador dispare  

```c#
    public event metodoDelegado EventoDisparo;
```  

Lo segundo que tenemos que hacer es controlar cuándo dispara el jugador. Para esto he decidido usar la tecla espacio y lo controlamos desde el controlador de escena.  
El método de update que controla la pulsación de esta tecla y dispara el evento es el siguiente:  

```c#
  void Update()
    {
      if (Input.GetKey(KeyCode.Space))
        EventoDisparoRecibido();
    }
```  
Una vez disparado el evento debemos estar a la escucha desde los objetos que se vayan a ver afectados. Esto lo hacemos mediante un script que accederá al evento y suscribirá un método que producirá en el elemento los efectos del disparo.  
Al igual que en el punto anterior, accedemos al delegado mediante código y, posteriormente, suscribimos nuestro método.  
```c#
    tempObj = GameObject.Find("Main Camera");
    scriptInstance = tempObj.GetComponent<sceneManager>();
    scriptInstance.EventoDisparoRecibido += gotShot;  
```  
Este método (gotShot) comprobará la distancia entre el objeto y el jugador. 
Si está a una distancia media (entre 1 y 4 unidades), el objeto sufrirá un desplazamiento. En cambio, si está a una distancia corta (menor que uno) el objeto será destruido.  
```c#  
  void gotShot() {
     if (mytf != null) {
       Vector3 direction = mytf.position - tf.position;
       if (direction.magnitude < 1.0f)
         Destroy(gameObject, 0.0f);
       else if (direction.magnitude < 4.0f)
         myrb.AddForce(direction.x, direction.y, direction.z, ForceMode.Impulse);
     }
  }
```

