using UnityEngine;

public class Player : IGameObject
{
    public GameObject gameObject { get; set; }
    
    private float speed;

    public Player(GameObject gameObject, float speed)
    {
        this.gameObject = gameObject;
        this.speed = speed;
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 moveVector)
    {
        gameObject.transform.position += moveVector * speed; 
    }
}
