using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DebugTesting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void DebugTesting()
    {
        IWeapon testWeapon = new TestWeapon();
        IStatusEffectable temp = new TestEnemy();
        var testDecorator = new FireDecorator();
        testWeapon = testDecorator.Decorate(testWeapon, 10);
        testWeapon.weaponEffects[0].Handle(testWeapon.weaponEffects, temp);
    }
}
