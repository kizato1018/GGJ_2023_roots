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
    public float amplitude = 0.05f;
    public float frequency = 0.1f;
    protected GameObject _owner;
    protected SpriteRenderer sprite_renderer;
    protected Vector3 tempPosition;

    public Vector3 offset;  //最大的偏移量
    private Vector3 originPosition; //记录物体的原始坐标
    private float tick;      // 用于计算当前时间量（可以理解成函数坐标轴x轴）

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        tempPosition = transform.position;

        //如果没有设置频率或者设置频率为0则自动记录成1

        if (Mathf.Approximately(frequency, 0))

            frequency = 1f;

        originPosition = transform.localPosition;

        tick = Random.Range(0f, 2f * Mathf.PI);

        //计算振幅

        //amplitude = 2 * Mathf.PI / frequency;
    }

    void FixedUpdate()
    {
        if (_owner != null) return;
        //计算下一个时间量

        tick = tick + Time.fixedDeltaTime * amplitude;

        //计算下一个偏移量

        var amp = new Vector3(Mathf.Cos(tick) * offset.x, Mathf.Sin(tick) * offset.y, 0);

        // 更新坐标

        transform.localPosition = originPosition + amp;
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
        if (owner == null)
        {
            sprite_renderer.flipX = false;
            sprite_renderer.flipY = false;
            originPosition = transform.position;
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
