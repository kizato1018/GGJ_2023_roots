using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ObjectSkill
{
    public void UseSkill(GameObject owner);
}

public interface BattleAction
{
    public void UnderAttack(int damage);
}

public class Object : MonoBehaviour, ObjectSkill
{
    protected GameObject _owner;
    protected SpriteRenderer sprite_renderer;
    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void SetDirection(Vector3 v3)
    {
        setObjectFlip(v3);
    }

    public void WasTaken()
    {

    }

    public void SetOwner(GameObject owner, Vector3 ownerLastMoveVector)
    {
        _owner = owner;
        if (sprite_renderer == null) return;
        if(owner == null)
        {
            sprite_renderer.flipX = false;
            sprite_renderer.flipY = false;
            return;
        }
        setObjectFlip(ownerLastMoveVector);
    }

    private void setObjectFlip(Vector3 inputVector)
    {
        switch (Vector3DirectionCheck.Check(inputVector))
        {
            case Direction.TopRight:
                sprite_renderer.flipX = true;
                sprite_renderer.flipY = false;
                break;
            case Direction.BottomRight:
                sprite_renderer.flipX = true;
                sprite_renderer.flipY = false;
                break;
            case Direction.BottomLeft:
                sprite_renderer.flipX = false;
                sprite_renderer.flipY = false;
                break;
            case Direction.TopLeft:
                sprite_renderer.flipX = false;
                sprite_renderer.flipY = false;
                break;
            case Direction.Right:
                sprite_renderer.flipX = true;
                sprite_renderer.flipY = false;
                break;
            case Direction.Left:
                sprite_renderer.flipX = false;
                sprite_renderer.flipY = false;
                break;
            case Direction.Top:
                sprite_renderer.flipX = false;
                sprite_renderer.flipY = false;
                break;
            case Direction.Bottom:
                sprite_renderer.flipX = false;
                sprite_renderer.flipY = false;
                break;
            case Direction.Center:
                break;
        }
    }

    public virtual void UseSkill(GameObject owner)
    {
    }
}
