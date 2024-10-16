using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D Rigid;
    public SpriteRenderer PlayerXY;
    public float Movement_Speed = 10f;
    private float Movement = 0;
    private float JumpForce = 20;
    private int Direction = 1;
    private Vector3 Player_LocalScale;

    public Sprite[] Spr_Player = new Sprite[2];
    private Coroutine currentCoroutine;
    public bool JumpBool = false;
    private bool PCPlayPlatform;

    // Use this for initialization
    void Start()
    {
        PlayerXY = GetComponent<SpriteRenderer>();
        Rigid = GetComponent<Rigidbody2D>();
        Player_LocalScale = transform.localScale;


        if (Application.platform == RuntimePlatform.WindowsPlayer) PCPlayPlatform = true;
        else if (Application.platform == RuntimePlatform.Android ||
                 Application.platform == RuntimePlatform.IPhonePlayer) PCPlayPlatform = false;
    }

    public bool canJump = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !canJump)
        {
            // Проверяем нажатие кнопки мыши (клик по экрану)
            canJump = true;
        }


            Movement = Input.acceleration.x * Movement_Speed; // Movement logic


        if (Movement > 0)
        {
            transform.localScale = new Vector3(-1, Player_LocalScale.y, Player_LocalScale.z);
        }

        else if (Movement < 0)
        {
            PlayerXY.flipX = true;
            transform.localScale = new Vector3(1, Player_LocalScale.y, Player_LocalScale.z);
        }
    }

    void FixedUpdate()
    {
        if (canJump)
        {
            Vector2 Velocity = Rigid.velocity;
            Velocity.x = Movement;
            Rigid.velocity = Velocity;

            if (Velocity.y < 0)
            {
                JumpBool = true; // Assumed player is falling
                GetComponent<BoxCollider2D>().enabled = true;
                Propeller_Fall();

                if (currentCoroutine == null) // Start sprite animation coroutine if not already running
                    currentCoroutine = StartCoroutine(AnimateSprites(Spr_Player, 0.03f, false));
            }
            else
            {
                JumpBool = false; // Assumed player is rising or on flat ground
                GetComponent<BoxCollider2D>().enabled = false;

                if (currentCoroutine == null) // Start sprite animation coroutine if not already running
                    currentCoroutine = StartCoroutine(AnimateSprites(Spr_Player, 0.03f, true));
            }

            // Screen wrapping logic
            Vector3 Top_Left = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            float Offset = 0.5f;

            if (transform.position.x > -Top_Left.x + Offset)
                transform.position = new Vector3(Top_Left.x - Offset, transform.position.y, transform.position.z);
            else if (transform.position.x < Top_Left.x - Offset)
                transform.position = new Vector3(-Top_Left.x + Offset, transform.position.y, transform.position.z);
        }
        else
        {
            if (currentCoroutine == null) // Start sprite animation coroutine if not already running
                currentCoroutine = StartCoroutine(AnimateSprites(Spr_Player, 0.03f, true));
        }
    }

    IEnumerator AnimateSprites(Sprite[] sprites, float delay, bool reverse)
    {
        bool isAtBoundary = false; // Флаг для контроля, находится ли индекс на границе
        int index = 0;
        while (!canJump)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index];

            yield return new WaitForSeconds(0.3f);

            // Увеличиваем индекс
            index++;

            // Если достигли конца массива, переходим к началу
            if (index >= 2)
            {
                index = 0;
            }
        }

        index = reverse ? sprites.Length - 1 : 0;

        while (true)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[index];

            yield return new WaitForSeconds(delay);

            if (reverse) // Если движение вверх
            {
                if (index > 0 && !isAtBoundary)
                    index--;
                else
                {
                    isAtBoundary = true;
                    index = sprites.Length - 1; // Переходим к последнему спрайту, начиная зацикливание
                }
            }
            else // Если движение вниз
            {
                if (index < sprites.Length - 1 && !isAtBoundary)
                    index++;
                else
                {
                    isAtBoundary = true;
                    index = 0; // Переходим к первому спрайту, начиная зацикливание
                }
            }

            // Если мы находимся на границе
            if (isAtBoundary)
            {
                if (reverse)
                {
                    index = (index == sprites.Length - 1) ? sprites.Length - 2 : sprites.Length - 1;
                }
                else
                {
                    index = (index == 0) ? 1 : 0;
                }
            }

            // Проверяем, меняется ли JumpBool
            if (JumpBool != reverse)
            {
                reverse = JumpBool;
                isAtBoundary = false; // Сбрасываем флаг границы
                index = reverse ? sprites.Length - 1 : 0; // Сбрасываем индекс
            }
        }
    }


    public void Anim()
    {
        if (currentCoroutine == null) // Start sprite animation coroutine if not already running
            currentCoroutine = StartCoroutine(AnimateSprites(Spr_Player, 0.03f, false));
    }

    void Propeller_Fall()
    {
        if (transform.childCount > 0)
        {
            if (currentCoroutine == null) // Start sprite animation coroutine if not already running
                currentCoroutine = StartCoroutine(AnimateSprites(Spr_Player, 0.03f, false));
            transform.GetChild(0).GetComponent<Animator>().SetBool("Active", false);
            transform.GetChild(0).GetComponent<Propeller>().Set_Fall(gameObject);
            transform.GetChild(0).parent = null;
        }
    }
}