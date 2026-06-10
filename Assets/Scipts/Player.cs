using UnityEngine;

public class Player : IGameObject
{
    public IWeapon weapon;
    public GameObject gameObject { get; set; }
    
    private float speed;
    private Vector3 weaponPos;

    public Player(GameObject go, float _speed, Vector3 _weaponPos)
    {
        gameObject = GameObject.Instantiate(go);
        speed = _speed;
        weaponPos = _weaponPos;
    }

    // make a child without monobaviour?

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 moveVector)
    {
        gameObject.transform.position += moveVector.normalized * speed; 
    }
}
