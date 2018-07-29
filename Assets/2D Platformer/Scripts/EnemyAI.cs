using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
    {
        #region Поля
        /// <summary>
        /// Стартовая позиция
        /// </summary>
        private Vector3 StartPos;
        /// <summary>
        /// Направление движения (право-лево)
        /// </summary>
        private Vector3 MovementDirection;
        /// <summary>
        /// Игрок (цель)
        /// </summary>
        [SerializeField] private GameObject Player;
        /// <summary>
        /// Пламя для огнемета
        /// </summary>
      //  [SerializeField] private GameObject Flame;

    [SerializeField] private Rigidbody2D Fireball;			//bullet object
    [SerializeField] private GameObject Bazooka;
    [SerializeField] private float Fireball_Speed;




    /// <summary>
    /// Дистанция, с которой босс будет пытаться стрелять.
    /// </summary>
    [SerializeField] private int FireRange;
        /// <summary>
        /// Максимальная скорость противника
        /// </summary>
        [SerializeField] private int Speed;
        /// <summary>
        /// Акутальная дистанция до игрока
        /// </summary>
        private float Distance;
        /// <summary>
        /// Радиус поиска (насколько далеко должен находиться игрок, чтобы босс им заинтересовался)
        /// </summary>
        [SerializeField] private float SearchRadius;
        /// <summary>
        /// Направление на игрока на данный момент
        /// </summary>
        private Vector2 PlayerDirection;
        /// <summary>
        /// RayCast, следющий за игроком
        /// </summary>
        private RaycastHit2D FindPlayer;
        /// <summary>
        /// Маска слоев для FindPlayer
        /// </summary>
        [SerializeField] private LayerMask mask;
        /// <summary>
        /// Направление, куда травлятеся игрок при ударе в ближнем бою. 
        /// </summary>
        private Vector3 StrikeVector = new Vector3(0, 1, 0);
        /// <summary>
        /// Возможные модели поведения босса. Больше нигде не используются, поэтому описаны в классе босса
        /// </summary>
        private enum Mode { attack, search };
        /// <summary>
        /// Текущая модель поведения
        /// </summary>
        [SerializeField] private Mode CurrentMode;
    bool target = false;
    #endregion

    #region Методы MonoBehaviour

    void Start()
    {
        CurrentMode = Mode.search; //опредлеяет базовый режим (поиск). 
        StartPos = transform.position; // Фиксация места спауна для возврата в режиме Ожиждания
    }

    void Update()
    {
        if (CurrentMode != Mode.attack) Search();
        if (CurrentMode == Mode.attack) Attack();
    }

    #endregion

    #region Прочие методы

    private void Attack()
    {
        Debug.Log("Стреляем!");
        target = false;
        Shoot();

    }

    private void Shoot()
    {
        Rigidbody2D RB_fireball = Instantiate(Fireball, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        RB_fireball.velocity = new Vector2(transform.position.x < Player.transform.position.x ? Fireball_Speed : -Fireball_Speed, 0);
        CurrentMode = Mode.search;
    }


    private void Search()
    {
        PlayerDirection = Player.transform.position - transform.position;

        RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, 2, mask);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 2, mask);
        Debug.DrawRay(transform.position, Vector2.right * 2, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 2, Color.red);

        if (hit_left || hit_right)
        {
            Debug.Log("Игрок найден");
            target = true;
            CurrentMode = Mode.attack;
        }
        else
        {
            CurrentMode = Mode.search;
            target = false;
        }
    }
    /*
    /// <summary>
    /// Поведение в режиме поиска (движение в сторону игрока)
    /// </summary>
    private void Engage()
    {
        Debug.Log("Преследую");
        MovementDirection.x = (Player.transform.position.x - transform.position.x);
        if ((StartPos.x - transform.position.x) >= 3 || (StartPos.x - transform.position.x) <= -3)
            CurrentMode = Mode.wait;
        else
        {
            transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
            Distance = (Player.transform.position - transform.position).magnitude;
        }
        
        if (Distance <= FireRange)
        {
            CurrentMode = Mode.attack;
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// Поведение в режиме покоя: вовзарещние к стартвой позиции и ожидание на ней
    /// </summary>
    private void Wait()
    {
        // Debug.Log("Жду");
        if (transform.position.x != StartPos.x)
            MovementDirection.x = (StartPos.x - transform.position.x);
        transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
    }*/

    #endregion
}


/*
 
    public class EnemyAI : MonoBehaviour
    {
        #region Поля
        /// <summary>
        /// Стартовая позиция
        /// </summary>
        private Vector3 StartPos;
        /// <summary>
        /// Направление движения (право-лево)
        /// </summary>
        private Vector3 MovementDirection;
        /// <summary>
        /// Игрок (цель)
        /// </summary>
        [SerializeField] private GameObject Player;
        /// <summary>
        /// Пламя для огнемета
        /// </summary>
      //  [SerializeField] private GameObject Flame;

    [SerializeField] private Rigidbody2D Fireball;			//bullet object
    [SerializeField] private GameObject Bazooka;
    [SerializeField] private float Fireball_Speed;




    /// <summary>
    /// Дистанция, с которой босс будет пытаться стрелять.
    /// </summary>
    [SerializeField] private int FireRange;
        /// <summary>
        /// Максимальная скорость противника
        /// </summary>
        [SerializeField] private int Speed;
        /// <summary>
        /// Акутальная дистанция до игрока
        /// </summary>
        private float Distance;
        /// <summary>
        /// Радиус поиска (насколько далеко должен находиться игрок, чтобы босс им заинтересовался)
        /// </summary>
        [SerializeField] private float SearchRadius;
        /// <summary>
        /// Направление на игрока на данный момент
        /// </summary>
        private Vector2 PlayerDirection;
        /// <summary>
        /// RayCast, следющий за игроком
        /// </summary>
        private RaycastHit2D FindPlayer;
        /// <summary>
        /// Маска слоев для FindPlayer
        /// </summary>
        [SerializeField] private LayerMask mask;
        /// <summary>
        /// Направление, куда травлятеся игрок при ударе в ближнем бою. 
        /// </summary>
        private Vector3 StrikeVector = new Vector3(0, 1, 0);
        /// <summary>
        /// Возможные модели поведения босса. Больше нигде не используются, поэтому описаны в классе босса
        /// </summary>
        private enum Mode { attack, engage, wait };
        /// <summary>
        /// Текущая модель поведения
        /// </summary>
        [SerializeField] private Mode CurrentMode;
    #endregion

    #region Методы MonoBehaviour

    void Start()
    {

        CurrentMode = Mode.engage; //опредлеяет базовый режим (поиск). 
        StartPos = transform.position; // Фиксация места спауна для возврата в режиме Ожиждания

    }

    void Update()
    {
        if (CurrentMode != Mode.attack) Scan();
        if (CurrentMode == Mode.engage) Engage();
        if (CurrentMode == Mode.wait) Scan();
    }

    #endregion

    #region Прочие методы


    /// <summary>
    /// Процедура стрельбы из огнемета по игроку, если он подобрался на достаточное расстояние
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack()
    {
        Debug.Log("Стреляем!");
        yield return new WaitForSeconds(0.5f);

        Rigidbody2D RB_fireball = Instantiate(Fireball, Bazooka.transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        RB_fireball.velocity = new Vector2(transform.position.y < Player.transform.position.y ? Fireball_Speed : -Fireball_Speed, 0);

        yield return new WaitForSeconds(2);
        CurrentMode = Mode.engage;

        yield break;
    }

    /// <summary>
    /// Проверка, насколько игрок далеко от босса
    /// </summary>
    private void Scan()
    {
        // Debug.Log("Сканирую");
        PlayerDirection = Player.transform.position - transform.position;

        RaycastHit2D hit_right = Physics2D.Raycast(transform.position, Vector2.right, 2, mask);
        RaycastHit2D hit_left = Physics2D.Raycast(transform.position, Vector2.left, 2, mask);
        Debug.DrawRay(transform.position, Vector2.right * 2, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * 2, Color.red);

        if (hit_left || hit_right)
        {
            Debug.Log("Игрок найден");
            CurrentMode = Mode.engage;
        }
        else CurrentMode = Mode.wait;
    }

    /// <summary>
    /// Поведение в режиме поиска (движение в сторону игрока)
    /// </summary>
    private void Engage()
    {
        Debug.Log("Преследую");
        MovementDirection.x = (Player.transform.position.x - transform.position.x);
        if ((StartPos.x - transform.position.x) >= 3 || (StartPos.x - transform.position.x) <= -3)
            CurrentMode = Mode.wait;
        else
        {
            transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
            Distance = (Player.transform.position - transform.position).magnitude;
        }
        
        if (Distance <= FireRange)
        {
            CurrentMode = Mode.attack;
            StartCoroutine(Attack());
        }
    }

    /// <summary>
    /// Поведение в режиме покоя: вовзарещние к стартвой позиции и ожидание на ней
    /// </summary>
    private void Wait()
    {
        // Debug.Log("Жду");
        if (transform.position.x != StartPos.x)
            MovementDirection.x = (StartPos.x - transform.position.x);
        transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
    }

    #endregion
}

 */
