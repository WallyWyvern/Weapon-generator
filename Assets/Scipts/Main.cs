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

    [Header("Game settings")]
    [SerializeField] float playerSpeed = 0.1f;

    // Variables
    private Player player;
    private IUsable playerItem;
    private Enemy targetDummy;
    private EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = new EventManager();

        // input handling
        moveAction.action.Enable();
        fireAction.action.Enable();
        newGunAction.action.started += GenerateNewGun;

        // UI
        fireStatsUI.gameObject.SetActive(false);
        poisonStatsUI.gameObject.SetActive(false);
        iceStatsUI.gameObject.SetActive(false);
        weaponStrengthUI.gameObject.SetActive(false);
        EventManager.instance.onWeaponDecorated += UpdateUI;

        InitializeGame();
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

    private void GenerateNewGun(InputAction.CallbackContext context)
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
        targetDummy = new Enemy(
            enemyObject, 
            0f, 
            new Vector3(0f, 0f, 0f), 
            999f,
            new Vector3(0f, 3f, 0f));
        IRangedWeapon gun = new Gun(
            weaponObject, 
            bulletObject, 
            player);
        // decorate gun
        DecorateRangedWeapon(gun);
        player.SetHeldObject(gun);
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
}
