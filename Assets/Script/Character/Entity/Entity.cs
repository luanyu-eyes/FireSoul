using Unity.Mathematics;
using UnityEngine;

namespace Script.Character.Entity
{
    public class Entity : MonoBehaviour  //实体类，是一切拥有实体的对象的父类，其子类包括怪物，玩家载体，npc载体等
    {
        public float health;  //生命值
        public float moveSpeed;  //移动速度

        protected Rigidbody2D Rigidbody2D;

        protected bool IsOnGround;  //是否在地面
        protected bool IsOnWall;  //是否在墙上
        
        protected virtual void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            IsOnGround = false;
            IsOnWall = false;
        }

        protected virtual void Update()
        {
            Attack();
            Jump();
            Move();
            SetInputVal();
        }

        private void SetInputVal()
        {

        }
        
        protected virtual void Attack()  //攻击方法，可被子类继承
        {
            
        }

        protected virtual void Jump()
        {
            
        }

        protected virtual void Move()  //移动方法，可被子类继承
        {
            
        }

        public virtual void Damage(float damage)  //对实体造成伤害，当生命值下降到0及以下时，销毁游戏对象
        {
            health = health - damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))  //当对象与“Ground”发生碰撞时，将IsOnGround设置为真
            {
                if (math.abs(collision.contacts[0].normal.x) > 0.1)
                {
                    IsOnWall = true;
                }

                if (collision.contacts[0].normal.y > 0.1)
                {
                    IsOnGround = true;
                }
            }
        }

        protected virtual void OnCollisionExit2D(Collision2D other)  //当对象离开“Ground”时，将IsOnGround设置为假
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                IsOnGround = false;
                IsOnWall = false;
            }
        }
    }
}