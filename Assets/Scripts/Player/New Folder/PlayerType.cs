using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerType : MonoBehaviour
{
    public PlayerController player;

    // =========== ��� (������) ============

    public virtual void DodgeUpdate()
    {
        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.35f)
        {
            if (player.CheckGround())
            {
                player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
                return;
            }
            else
            {
                player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
                return;
            }
        }
    }

    public virtual void DodgeFixedUpdate()
    {
        player.Dodge();
    }

    public virtual void DodgeOnEnter()
    {
        player.animator.Play("Dodge");

        player.moveSpeed *= 5f;
        player.maxSpeed *= 2f;
    }

    public virtual void DodgeOnExit()
    {
        player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);

        player.moveSpeed /= 5f;
        player.maxSpeed /= 2f;
    }

    // ============ ��� ============
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


    // ============ �ϰ� ============
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


    // ============ ���� ============
    public abstract void AttackUpdate();
    public abstract void AttackFixedUpdate();
    public abstract void AttackOnEnter();
    public abstract void AttackOnExit();

    // ============ Ư�� ��ų ============
    public abstract void SkillUpdate();
    public abstract void SkillFixedUpdate();
    public abstract void SkillOnEnter();
    public abstract void SkillOnExit();
}