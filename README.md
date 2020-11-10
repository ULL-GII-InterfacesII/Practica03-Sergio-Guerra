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
![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/Disparos.gif)
   
 b. Si el jugador choca con objetos de tipo B, todos los de ese tipo sufrirán alguna transformación o algún cambio en su apariencia y decrementarán el poder del jugador  
   
 El cambio de apariencia que he decidido implementar es un cambio de color. Es decir, cuando un jugador choca con un objeto tipo B,  todos los objetos de este tipo cambiarán de color. Además, cuando ocurra esta colisión cada objeto del tipo B le restará un punto de vida al jugador. Es decir, que si hay dos objetos tipo B, el jugador perderá dos puntos de vida.  
 Esto se ha implementado (evidentemente) mediante un método delegado. El jugador colisiona con un objeto y comprueba si es de tipo B. Si es así, dispará un evento que los objetos del tipo B estarán escuchando. Cuando estos reciban el evento, sufren el cambio programado. 
  
El código que dispara el evento en la colision es el siguiente (vemos cómo se llama al nuevo evento junto a los otros dos del apartado anterior):  
```c#
  void OnCollisionEnter(Collision collision) {
      if (collision.gameObject.name == "DroneA")
       EventoA();
      if (collision.gameObject.name == "DroneB")
        EventoB();
      if (collision.gameObject.tag == "B")
        collisionWithBObject();
    }
```  
Los objetos tipo B se suscriben al evento del delegado de la misma manera en que se ha explicado anteriormente. Se obtiene el gameObject del elemento
que disparará el vento (en este caso el jugador), se accede al script que contiene el delegado y se produce la suscripción.  
El método que produce los cambios en los objetos tipo B es un método simple de generación y cambio de color.  
  
```c#
  void changeColor() {
      Renderer myrd = GetComponent<Renderer>();
      Color randomColor = new Color(
        Random.Range(0f, 1f),
        Random.Range(0f, 1f),
        Random.Range(0f, 1f)
      );
      myrd.material.color = randomColor;
      Debug.Log("El jugador ha perdido vida. Ahora tiene: " + --scriptInstance.health + " hp");

    }
```  
![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/CambioTipoB.gif)

#### 3. En la escena habrá ubicados un tipo de objetos que al ser recolectados por el jugador harán que ciertos obstáculos se desplacen desbloqueando algún espacio.  
  
Se ha colocado una pequeña esfera con apariencia de holograma que cuando el jugador la recolecta (cuando se posiciona encima de ella) se desbloquea una segunda habitación.  
  
![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/recolectable.png)  
  
La recolección se ha implementado mediante un trigger y la comunicación mediante el evento de un delegado.  
La esfera tiene un collider con la opción isTrigger activada. Así, cuando este es detactado se comprueba si el causante es el jugador. Si es así, el objeto es destruido (se recolecta) y se dispara un evento.  
  
```c#
    void OnTriggerEnter(Collider collider) {
      Destroy(gameObject, 0.0f);
      collectedEvent();
    }
```  
El objeto que se desplazará para abrir el nuevo camino, en este caso, un armario, estará a la escucha de si este evento se produce. Cuando esto ocurra, su desplaza hacia un lado.  
La suscripción al evento se realiza como de costumbre:  
```c#
  GameObject tempObj;
  tempObj = GameObject.Find("obstacle");
  scriptInstance = tempObj.GetComponent<collected>();
  scriptInstance.collectedEvent += open;
```  
  
El método suscrito:  
```c#
    void open() {
      Transform mytf = GetComponent<Transform>();
      mytf.position -= new Vector3(1.0f, 0.0f, 0.0f);
    }
```  
En el siguiente gif podemos ver el resultado de la implementación
![alt_text](https://github.com/ULL-GII-InterfacesII/Practica03-Sergio-Guerra/blob/main/gifs/abrirHabitacion.gif)
