using UnityEngine;

namespace Script.Character.Entity.Player
{
    public class BasicAnimationController
    {
        private Animator _animator;

        public BasicAnimationController( Animator animator)
        {
            _animator = animator;
        }

        public virtual void PlayAnimation(bool IsOnGround, bool IsOnWall)
        {
            _animator.SetBool("IsOnGround",IsOnGround);
            _animator.SetBool("IsOnWall",IsOnWall);
            
            Run(IsOnGround, IsOnWall);
            WalkOnWall(IsOnGround, IsOnWall);
            InAir(IsOnGround,IsOnWall);
            Jump(IsOnGround,IsOnWall);
            Attack(IsOnGround,IsOnWall);
            RangedAttack(IsOnGround,IsOnWall);
        }

        protected virtual void Run(bool IsOnGround, bool IsOnWall)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.001)
            {
                _animator.SetBool("IsRun", true);
            }
            else
            {
                _animator.SetBool("IsRun", false);
            }
        }

        protected virtual void WalkOnWall(bool IsOnGround, bool IsOnWall)
        {
            if (IsOnWall && !IsOnGround && Mathf.Abs(Input.GetAxis("Vertical")) > 0.001)
            {
                _animator.SetBool("IsWalkOnWall", true);
            }
            else
            {
                _animator.SetBool("IsWalkOnWall", false);
            }
        }

        protected virtual void InAir(bool IsOnGround, bool IsOnWall)
        {
            if (!IsOnWall && !IsOnGround)
            {
                _animator.SetBool("IsInAir",true);
            }
            else
            {
                _animator.SetBool("IsInAir",false);
            }
        }

        protected virtual void Jump(bool IsOnGround,bool IsOnWall)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _animator.SetBool("IsJump",true);
            }
            else
            {
                _animator.SetBool("IsJump",false);
            }
        }

        protected virtual void Attack(bool IsOnGround,bool IsOnWall)
        {
            if (Input.GetKey(KeyCode.J))
            {
                _animator.SetBool("IsAttack",true);
            }
            else
            {
                _animator.SetBool("IsAttack",false);
            }
        }

        protected virtual void RangedAttack(bool IsOnGround,bool IsOnWall)
        {
            if (Input.GetKey(KeyCode.K))
            {
                _animator.SetBool("IsRangedAttack",true);
            }
            else
            {
                _animator.SetBool("IsRangedAttack",false);
            }
        }
    }
}
