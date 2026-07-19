using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    [Header("Input references")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference fireAction;
    [SerializeField] InputActionReference newGunAction;
    [SerializeField] InputActionReference startGame;

    [Header("References")]
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject enemyObject;

    [Header("UI References")]
    [SerializeField] TextMeshProUGUI fireStatsUI;
    [SerializeField] TextMeshProUGUI poisonStatsUI;
    [SerializeField] TextMeshProUGUI iceStatsUI;
    [SerializeField] TextMeshProUGUI weaponStrengthUI;
    [SerializeField] TextMeshProUGUI scoreUI;
    [SerializeField] TextMeshProUGUI gameOverScoreUI;

    [Header("Scene references")]
    [SerializeField] Canvas startScreen;
    [SerializeField] Canvas gameOverScreen;
    [SerializeField] Canvas gameUI;

    [Header("Game settings")]
    [SerializeField] float playerSpeed = 0.1f;

    // Variables
    private Player player;
    private IUsable playerItem;
    private Enemy targetDummy;
    private EventManager eventManager;
    private int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = new EventManager();
        Time.timeScale = 0;
        gameUI.enabled = false;
        gameOverScreen.enabled = false;
        startScreen.enabled = true;
        startGame.action.started += StartGamePressed;
    }



    // Update is called once per frame
    void Update()
    {
        if (fireAction.action.ReadValue<float>() == 1) 
        { 
            player.heldItem.Use(); 
        }
    }

    private void FixedUpdate()
    {
        // Update game
        EventManager.instance.SendUpdateTick();
        EventManager.instance.TickTimers(Time.fixedDeltaTime);

        var moveDirection = moveAction.action.ReadValue<Vector3>();
        player.Move(moveDirection);
    }

    private void StartGame()
    {
        Time.timeScale = 1;

        // input handling
        moveAction.action.Enable();
        fireAction.action.Enable();
        newGunAction.action.started += NewGunDebug;
        startGame.action.started -= StartGamePressed;

        // UI
        fireStatsUI.gameObject.SetActive(false);
        poisonStatsUI.gameObject.SetActive(false);
        iceStatsUI.gameObject.SetActive(false);
        weaponStrengthUI.gameObject.SetActive(false);
        EventManager.instance.onWeaponDecorated += UpdateUI;

        // gameloop events
        EventManager.instance.onEnemyDeath += EnemyDied;
        EventManager.instance.onPlayerDeath += PlayerDied;

        InitializeGame();
    }

    private void NewGunDebug(InputAction.CallbackContext context)
    {
        GenerateNewGun();
    }

    private void StartGamePressed(InputAction.CallbackContext context)
    {
        startScreen.enabled = false;
        gameUI.enabled = true;
        StartGame();
    }

    private void GenerateNewGun()
    {
        // reset UI
        fireStatsUI.gameObject.SetActive(false);
        poisonStatsUI.gameObject.SetActive(false);
        iceStatsUI.gameObject.SetActive(false);

        IRangedWeapon gun = new Gun(
            weaponObject,
            bulletObject,
            player);
        // decorate gun
        DecorateRangedWeapon(gun);
        player.SetHeldObject(gun);
    }

    private void InitializeGame()
    {
        player = new Player(
            playerObject, 
            playerSpeed, 
            new Vector3(0f, 0.5f, 0f), 
            100f, 
            new Vector3(0f,0f,0f));
        IRangedWeapon gun = new Gun(
            weaponObject, 
            bulletObject, 
            player);
        // decorate gun
        CreateEnemy();
        DecorateRangedWeapon(gun);
        player.SetHeldObject(gun);
        scoreUI.text = "Score: " + score.ToString();
    }

    private IRangedWeapon DecorateRangedWeapon(IRangedWeapon weapon)
    {
        int intensity = UnityEngine.Random.Range(25,100);
        weaponStrengthUI.text = "Weapon strength: " + intensity.ToString();
        weaponStrengthUI.gameObject.SetActive(true);
        var rangedWeaponDecorator = new RangedWeaponDecorator();
        rangedWeaponDecorator.DecorateRanged(weapon, intensity);

        if (UnityEngine.Random.Range(0, 10) >= 5)
        {
            var fire = new FireDecorator();
            fire.Decorate(weapon, intensity);
        }

        if (UnityEngine.Random.Range(0, 10) >= 5)
        {
            var poison = new PoisonDecorator();
            poison.Decorate(weapon, intensity);
        }

        if (UnityEngine.Random.Range(0, 10) >= 5)
        {
            var ice = new IceDecorator();
            ice.Decorate(weapon, intensity);
        }
        return weapon;
    }

    // Very bad solution, but I ran out of time to think of a proper one
    private void UpdateUI(EffectType type)
    {
        switch (type)
        {
            case EffectType.fire:
                fireStatsUI.gameObject.SetActive(true);
                break;
            case EffectType.poison:
                poisonStatsUI.gameObject.SetActive(true);
                break;
            case EffectType.ice:
                iceStatsUI.gameObject.SetActive(true);
                break;
        }
    }

    private void CreateEnemy()
    {
        var enemyHealth = UnityEngine.Random.Range(100, 200);
        var enemySpeed = UnityEngine.Random.Range(1f, 3f);
        var startPos = new Vector3(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f), 0);
        var newEnemy = new Enemy(
            enemyObject,
            enemySpeed,
            new Vector3(0f, 0f, 0f),
            enemyHealth,
            startPos,
            10);
    }

    private void EnemyDied()
    {
        IncreaseScore();
        CreateEnemy();
        if (UnityEngine.Random.Range(0f, 10f) <= 2.5f) CreateEnemy();
        GenerateNewGun();
    }

    private void PlayerDied()
    {
        EndGame();
    }

    private void EndGame()
    {
        moveAction.action.Disable();
        fireAction.action.Disable();
        newGunAction.action.started -= NewGunDebug;

        EventManager.instance.onWeaponDecorated -= UpdateUI;
        EventManager.instance.onEnemyDeath -= EnemyDied;
        EventManager.instance.onPlayerDeath -= PlayerDied;

        gameUI.enabled = false;
        gameOverScreen.enabled = true;
        gameOverScoreUI.text = "Game Over" + "<br>Score: " + score.ToString();
    }

    private void IncreaseScore()
    {
        score++;
        scoreUI.text = "Score: " + score.ToString();
    }
}
