using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerType : MonoBehaviour
{
    public PlayerController player;

    
    // ���
    public virtual void RiseUpdate()
    {
        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }
    }
    public virtual void RiseFixedUpdate()
    {
        player.Move();
        player.SetFacingDirection();
    }
    public virtual void RiseOnEnter()
    {
        player.animator.Play("Jump");
    }
    public virtual void RiseOnExit()
    {

    }

    
    // �ϰ�
    public virtual void FallUpdate()
    {
        if (player.CheckGround())
        {
            player.stateMachine.ChangeStateAny(PlayerMovementEnums.LAND);
            return;
        }
    }
    public virtual void FallFixedUpdate()
    {
        player.Move();
        player.SetFacingDirection();
    }

    public virtual void FallOnEnter()
    {
        player.animator.Play("Fall");
    }

    public virtual void FallOnExit()
    {

    }


    // ����
    public abstract void AttackUpdate();
    public abstract void AttackFixedUpdate();
    public abstract void AttackOnEnter();
    public abstract void AttackOnExit();

    // Ư�� ��ų
    public abstract void SkillUpdate();
    public abstract void SkillFixedUpdate();
    public abstract void SkillOnEnter();
    public abstract void SkillOnExit();
}