using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    [Header("Input references")]
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference fireAction;

    [Header("References")]
    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject weaponObject;

    [Header("Game settings")]
    [SerializeField] float playerSpeed = 0.1f;

    // debug variables
    private Gun testGun;

    // Variables
    private Player player;
    private EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = new EventManager();

        // input handling
        moveAction.action.Enable();
        fireAction.action.started += triggerFire;

        testGun = new Gun(weaponObject, bulletObject);

        DebugTesting();
    }

    private void triggerFire(InputAction.CallbackContext context)
    {
        testGun.Use();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Update game
        EventManager.instance.SendUpdateTick();
        EventManager.instance.TickTimers(Time.fixedDeltaTime);

        var moveDirection = moveAction.action.ReadValue<Vector3>();
        player.Move(moveDirection);
    }

    private void DebugTesting()
    {
        //IWeapon testWeapon = new TestWeapon();
        //IStatusEffectable temp = new TestEnemy();
        //var testDecorator = new FireDecorator();
        //testWeapon = testDecorator.Decorate(testWeapon, 10);
        //testWeapon.weaponEffects[0].Handle(testWeapon.weaponEffects, temp);

        player = new Player(playerObject, playerSpeed);

    }
}
