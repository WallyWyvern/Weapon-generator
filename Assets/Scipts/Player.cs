using UnityEngine;

public class Player : BaseActor
{
    public Vector3 aimDirection;

    public Player(GameObject go, float speed, Vector3 itemPos, float health, Vector3 initPos) : base(go, speed, itemPos, health, initPos) { }

    public override void Move(Vector3 moveVector)
    {
        gameObject.transform.position += moveVector.normalized * moveSpeed;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        aimDirection = mousePosition - gameObject.transform.position;
        aimDirection.Normalize();
        gameObject.transform.up = aimDirection;
        EventManager.instance.PlayerMoved(gameObject.transform.position);
    }

    public override void SetHeldObject(IUsable item)
    {
        heldItem = item;
        heldItem.gameObject.transform.localPosition = heldItemPos;
    }

    public override void SetupUI()
    {
       // does nothing because I dont want player health right now
    }
}
