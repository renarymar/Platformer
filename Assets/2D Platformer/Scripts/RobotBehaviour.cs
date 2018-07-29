using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour {
    #region Поля
    [SerializeField] private Vector3 StartPos; // Стартовая позиция
    [SerializeField] private Vector3 MovementDirection; //Направление движения (право-лево)
    [SerializeField] private GameObject Player; //Игрок (цель)
    [SerializeField] private GameObject Flame; //Пламя для огнемета
    [SerializeField] private int FireRange; //Дистанция, с которой противник будет пытаться стрелять.
    [SerializeField] private int Speed; //Максимальная скорость противника
    [SerializeField] private float Distance; //Акутальная дистанция до игрока
    [SerializeField] private float Searchradius; // Радиус поиска (насколько далеко должен находиться игрок, чтобы босс им заинтересовался)
    [SerializeField] private Vector2 PlayerDirection; // Направление на игрока на данный момент
    private RaycastHit2D FindPlayer; // RayCast, следющий за игроком
    [SerializeField] private LayerMask mask; //Маска слоев для FindPlayer
    private Vector3 StrikeVector = new Vector3(0, 1, 0); //Направление, куда травлятеся игрок при ударе в ближнем бою. 
    private enum Mode {search, destroy, idle}; //Возможные модели поведения босса
    [SerializeField] private Mode CurrentMode; //Текущая модель
    #endregion

    #region Методы MonoBehavioyr
    void Start () {
        CurrentMode = Mode.search; //Выставляем базовый режим: поиск
        StartPos = transform.position; // Запоминаем место спауна
    }

    void Update()
    {
        if (CurrentMode != Mode.destroy) Scan(); //Если не 
        if (CurrentMode == Mode.search) SearchMode();
        else if (CurrentMode == Mode.idle) IdleMode();
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") Strike(collision.gameObject);
    }
    #endregion

    #region Прочие методы
    /// <summary>
    /// Удар в ближнем бою
    /// </summary>
    /// <param name="target">Цель удара</param>
    private void Strike(GameObject target)
    {
        target.gameObject.GetComponent<Rigidbody2D>().AddForce(StrikeVector * 4500);
        target.GetComponent<BasicController>().Hurt(1);
    }

    /// <summary>
    /// Процедура стрельбы из огнемета по игроку, если он подобрался на достаточное расстояние
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyMode()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject Firebolt = Instantiate(Flame, transform.position, Quaternion.identity);
        float angle = Vector2.Angle(Vector2.right, Player.transform.position - transform.position);
        Firebolt.transform.eulerAngles = new Vector3(0f, 0f, transform.position.y < Player.transform.position.y ? angle : -angle);
        yield return new WaitForSeconds(2);
        CurrentMode = Mode.search;
        yield break;
    }

    /// <summary>
    /// Проверка, насколько игрок далеко от босса
    /// </summary>
    private void Scan()
    {
        PlayerDirection = Player.transform.position - transform.position;
        FindPlayer = Physics2D.Raycast(transform.position, PlayerDirection, Searchradius, mask);
        if (FindPlayer)
        {
            CurrentMode = Mode.search;
        }
        else CurrentMode = Mode.idle;
    }

    /// <summary>
    /// Поведение в режиме поиска (движение в сторону игрока)
    /// </summary>
    private void SearchMode()
    {
        MovementDirection.x = (Player.transform.position.x - transform.position.x);
        transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
        Distance = (Player.transform.position - transform.position).magnitude;
        if (Distance <= FireRange)
        {
            CurrentMode = Mode.destroy;
            StartCoroutine(DestroyMode());
        }
    }

    /// <summary>
    /// Поведение в режиме покоя: вовзарещние к стартвой позиции и ожидание на ней
    /// </summary>
    private void IdleMode()
    {
        if(transform.position.x != StartPos.x)
        MovementDirection.x = (StartPos.x - transform.position.x);
        transform.Translate(MovementDirection.normalized * Time.deltaTime * Speed);
    }
    #endregion



}
