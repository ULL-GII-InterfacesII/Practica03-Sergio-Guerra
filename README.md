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
 
#### 2. Implementar un controlador de escena usando el patrón delegado que gestione las siguientes acciones:  
a. Si el jugador dispara, los objetos de tipo A que estén a una distancia media serán desplazados y los que estén a una distancia pequeña serán destruidos. Los umbrales que marcan los límites se deben configurar en el inspector.

