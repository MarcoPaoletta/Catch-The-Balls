# Catch-The-Balls
* Juego en el cual debemos de recoger las pelotas en el aro de basquet

![Gameplay](Gameplay.gif)

---

# Script [ButtonsController](https://github.com/MarcoPaoletta/Catch-The-Balls/blob/main/Assets/Scripts/ButtonsController.cs)

### Botones
```c#
    public static Color backgroundColorSelected; 

    public void BlueButton()
    {
        backgroundColorSelected = new Color(255, 255, 255, 1);
        SceneManager.LoadScene(1);
    }

    public void RedButton()
    {
        backgroundColorSelected = new Color(255, 0, 0, 1);
        SceneManager.LoadScene(1);
    }

    public void OpenSourceCodeButton()
    {
        Application.OpenURL("https://github.com/MarcoPaoletta/Catch-The-Balls");
    }
```
* Creamos una variable publica y estatica para que pueda ser accedida desde cualquier script que va a almacenar el color de fondo seleccionado dependiendo de que boton se toque
* Si se toca el boton azul, el color seleccionado sera azul y cambiamos la escena a la del juego
* Si se toca el boton rojo, el color seleccionado sera rojo y cambiamos la escena a la del juego
* Si se toca el boton para abrir el codigo fuente, abrimos la URL del repositorio

---

# Script [Ring](https://github.com/MarcoPaoletta/Catch-The-Balls/blob/main/Assets/Scripts/Ring.cs)

### Variables
```c#
    public int speed;
    public GameObject ballPrefab;
    public Rigidbody2D rb;
    
    private float horizontal;
```
* speed = velocidad de movimiento
* ballPrefab = almacena el prefab de la ball que sera instanciado
* rb = referencia al RigidBody2D
* horizontal = para el movimiento

---

### Bordes de la pantalla
```c#
    void ScreenThreshold()
    {
        if(transform.position.x > 8f)
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }

        if(transform.position.x < -8f)
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
    }
```
* Si la posicion es mayor a 8, es decir, estamos en el borde derecho de la pantalla, disminuimos un poco la posicion para volver hacia atras
* Si la posicion es menor a -8, es decir, estamos en el borde izquierdo de la pantalla, aumentamos un poco la posicion para ir hacia delante

---

### Movimiento
```c#
    void HorizontalMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * speed; // returns 1 if we are pressing D or -1 if we are pressing A

        if (horizontal < 0.0f) // if we are moving left
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f) // if we are moving right
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
```
* A la variable horizontal creada al inicio, definimos que retorne 1 si se presiona la D o que retorne -1 si se presiona la A y que a ese valor se lo multiplique por la velocidad
* Si el valor de horizontal es menor a 0, es decir, el jugador se esta moviendo hacia la izquierda, la escala local en el eje X va a ser -1

---

# Script [GameManager](https://github.com/MarcoPaoletta/Catch-The-Balls/blob/main/Assets/Scripts/GameManager.cs)

### Variables
```c#
    [SerializeField] public static int score;
    [SerializeField] public static int lives = 3;

    public GameObject ballPrefab;

    private float currentTime = 2;
    private int spawnTime = 2;
    private SpriteRenderer backgroundColor;
```
* *Creamos dos variables que sean SerializeField para que no sean mostradas sin necesidad en el inspector y estaticas y publicas para que puedan ser accedidas desde cualquier script*
* ballPrefab = almacena el prefab de la ball que sera instanciado
* currentTime / spawnTime = nos van a permitir crear un sistema basado en tiempo para spawnear las balls
* backgroundColor = almacena el SpriteRenderer del background para que su color pueda ser modificado

---

### Cambiar el color del fondo
```c#
    void ChangeBackgroundColor()
    {
        backgroundColor = GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>();
        backgroundColor.color = ButtonsController.backgroundColorSelected;
    }
```
* Asignamos el SpriteRenderer del background a la variable
* El color del background va a ser igual al color que haya sido seleccionado en el menu del juego

---

### Spawnear balls
```c#
    public void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, new Vector3(Random.Range(-8, 8), 6, 0), Quaternion.identity);
        ball.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
    }
```
* Creamos un nuevo GameObject en el cual se va a instanciar el ballPrefab, en una posicion X aleatoria dentro de la pantalla y en una posicion Y elevada. No modificamos inicialmente la rotacion
* A esta ball instanciada, le vamos a cambiar la rotacion de manera aleatoria

```c#
    void Timer()
    {
        currentTime -= Time.deltaTime;

        if(currentTime < 0)
        {
            currentTime = spawnTime;
            SpawnBall();
        }
    }
```
* A nuestra variable de tiempo, por cada segundo que pase vamos a restarle 1 para crear asi un timer
* Si el tiempo actual es menor a 0, reiniciamos el tiempo con el tiempo de la variable spawnTime y spawneamos una ball

---

### Game Over
```c#
    void GameOver()
    {
        if (lives <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
```
* Si no hay mas vidas, cambiamos a la escena del menu principal

---

# Script [Counter](https://github.com/MarcoPaoletta/Catch-The-Balls/blob/main/Assets/Scripts/Counter.cs)

### Actualizar el texto del canvas
```c#
    public Text scoreText;
    public Text livesText;

    void Update()
    {
        scoreText.text = GameManager.score.ToString();
        livesText.text = "Lives: " + GameManager.lives.ToString();
    }
```
* Creamos dos variables publicas que van a almacenar los textos del canvas
* Todo el tiempo vamos a estar actualizando estos textos segun las variables almacenadas en el GameManager. Al ser valores int, las debemos de convertir a string

---

# Script [Ball](https://github.com/MarcoPaoletta/Catch-The-Balls/blob/main/Assets/Scripts/Ball.cs)

### Limite inferior
```c#
    void BottomLimit()
    {
        if(transform.position.y < -6f)
        {
            Destroy(gameObject);
            GameManager.lives -= 1;
        }
    }
```
* Si la posicion Y es menor a -6, es decir, sobre paso el limite de la pantalla en el eje Y, destruimos la ball y perdemos una vida

---

### Colision con el ring
```c#
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        GameManager.score += 1;
    }
```
* Si colisionamos con un cuerpo, en este caso solo se puede colisionar con el ring, destruimos la ball y aumentamos la puntuacion

---

# Descargar Unity, ejecutar el proyecto y utilizar Visual Studio

## Descargar Unity
* Dirigirnos al [sitio oficial de descarga](https://unity.com/download) de Unity y descargar el hub como cualquier otra aplicacion simplemente tocando siguiente, siguiente, siguiente
* Una vez instalado, nos dirigimos  a la parte de `Installs`, luego en `ADD` e instalamos la version de Unity utilizada en este proyecto que es la `2020.3.28f1`
* Lo siguiente es seleccionar los modulos. El unico que vamos a seleccionar es el que dice `Microsoft Visual Studio Community` seguido de un año que puede ir cambiando
* Esperamos a que se instale y ya estaria

---

## Ejecutar el proyecto
* Nos dirigimos a la parte de `Installs`, luego en `OPEN` y abrimos la carpeta del proyecto la cual deberia de tener una carpeta con el nombre del proyecto, por ejemplo `John And Grunt` y otra con el nombre `My project`
* Con esto, ya tendremos el proyecto importado

---

## Utilizar Visual Studio
* Con todos los pasos anteriores ya se puede ejecutar y probar el proyecto, no obstante, no podemos realizar cambio en ningun script ya que Unity no tiene ningun IDE o editor de texto incluido
* Entonces, descargamos [Visual Studio](https://visualstudio.microsoft.com/es/downloads/) como cualquier otra aplicacion simplemente tocando siguiente, siguiente, siguiente
* Lo siguiente es seleccionar los modulos. Los modulos que vamos a seleccionar son: `.NET desktop development`  y `Game development with Unity`
* Vamos a algun proyecto de Unity, tocamos en `Edit` -> `Preferences` -> `External Tools` -> `External Script Editor` y seleccionamos `Microsoft Visual Studio Community` seguido de un año que puede ir cambiando
* Ahora, podremos modificar los scripts de Unity que son escritos en C#