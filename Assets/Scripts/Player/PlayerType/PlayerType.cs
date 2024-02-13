using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerType : MonoBehaviour
{
    public PlayerController player;

    // =========== Dodge(Dash) State ============
    public virtual void DodgeUpdate()
    {
        if (!player.animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge")) return;

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.40f)
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
        player.Dash();
    }

    public virtual void DodgeOnEnter()
    {
        player.PlayAnimation("Dodge");

        player.moveSpeed *= 5f;
        player.maxSpeed *= 2f;

        player.ghost.makeGhost = true;
    }

    public virtual void DodgeOnExit()
    {
        player.rigid.velocity = new Vector2(0, player.rigid.velocity.y);

        player.moveSpeed /= 5f;
        player.maxSpeed /= 2f;

        player.ghost.makeGhost = false;
    }

    // ============ Rise State ============
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
        player.PlayAnimation("Jump");
    }

    public virtual void RiseOnExit()
    {

    }

    // ============ Fall State ============
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
        player.PlayAnimation("Fall");
    }

    public virtual void FallOnExit()
    {

    }


    // ============ Attack State ============
    public virtual void AttackUpdate()
    {
        if (player.CheckGround())
            player.InitializeJumpCount();

        if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.98f) return;
        if (player.isAttack) return;

        if (player.CheckGround())
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.IDLE);
            // player.InitializeJumpCount();
            return;
        }

        if (player.rigid.velocity.y < 0)
        {
            player.stateMachine.ChangeStateLogic(PlayerMovementEnums.FALL);
            return;
        }
        
        player.stateMachine.ChangeStateLogic(PlayerMovementEnums.RISE);
        
    }

    public virtual void AttackFixedUpdate()
    {
        player.SetFacingDirection();
    }

    public virtual void AttackOnEnter()
    {
        player.PlayAnimation("Attack");
    }

    public virtual void AttackOnExit()
    {

    }

    // ============ Skill State ============
    public abstract void SkillUpdate();
    public abstract void SkillFixedUpdate();
    public abstract void SkillOnEnter();
    public abstract void SkillOnExit();
}