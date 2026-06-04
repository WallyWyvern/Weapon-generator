using UnityEngine;
using UnityEngine.InputSystem;

public class Main : MonoBehaviour
{
    [Header("Input references")]
    [SerializeField] InputActionReference moveAction;

    [Header("References")]
    [SerializeField] GameObject playerObject;

    [Header("Game settings")]
    [SerializeField] float playerSpeed = 0.1f;


    // Variables
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // input handling
        moveAction.action.Enable();

        DebugTesting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
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
