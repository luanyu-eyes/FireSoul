using System;
using UnityEngine;

namespace Script.Character.Entity.Enemy
{
    public class Monster : Entity  //怪物类
    {
        public float patrolRange;  //巡逻的范围
        
        private Component _monsterView;
        private float _startPositionX;  //巡逻开始的位置
        private bool _canMoveTo1;  //能否向巡逻点1移动
        private Animator _animator;
        private bool isInAttackedRange;
        private bool isHurt;
        private float faceDirection;

        protected override void Start()
        {
            base.Start();
        
            _monsterView = transform.GetChild(0).GetComponent<MonsterView>();
            _animator = GetComponent<Animator>();
            _startPositionX = transform.position.x;
            _canMoveTo1 = true;
        }

        protected override void Update()
        {
            if (!isHurt)
            {
                base.Update();

                MoveToPlayer();

                Hurt();
            }
            else
            {
                HurtBack();
            }
        }

        private void MoveToPlayer()  //当玩家出现在视野中后，怪物向玩家移动
        {
            if (_monsterView.GetComponent<MonsterView>().PlayerInView && _monsterView.GetComponent<MonsterView>().Player)
            {
                var position = _monsterView.GetComponent<MonsterView>().Player.transform.position.x - transform.position.x;
                transform.Translate(new Vector3(position,0,0).normalized * moveSpeed * Time.deltaTime);
                _animator.SetBool("attack", true);
                if (position < 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }

        protected override void Move()  //怪物在空闲的时原地巡逻
        {
            if (!_monsterView.GetComponent<MonsterView>().PlayerInView)
            {
                _animator.SetBool("attack", false);
                if (_canMoveTo1){
                    if (Math.Abs(transform.position.x - _startPositionX - patrolRange) < 0.1f)  //如果对象到达了巡逻点1，则向反方向移动
                    {
                        _canMoveTo1 = false;
                        transform.localScale = new Vector3(1, 1, 1);
                    }
                    else
                    {
                        if (transform.position.x > (_startPositionX + patrolRange))  //确定对象的移动方向
                        {
                            faceDirection = -1;
                        }
                        else
                        {
                            faceDirection = 1;
                        }
                    
                        transform.Translate(new Vector3(faceDirection,0,0) * (moveSpeed * Time.deltaTime));
                    }
                }
                else
                {
                    if (Math.Abs(transform.position.x - _startPositionX + patrolRange) < 0.1f)
                    {
                        _canMoveTo1 = true;
                        transform.localScale = new Vector3(-1, 1, 1);
                    }
                    else
                    {
                        if (transform.position.x > (_startPositionX - patrolRange))
                        {
                            faceDirection = -1;
                        }
                        else
                        {
                            faceDirection = 1;
                        }
                        transform.Translate(new Vector3(faceDirection,0,0) * (moveSpeed * Time.deltaTime));
                    }
                }
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "PlayerAttack")
            {
                isInAttackedRange = true;
            }   
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "PlayerAttack")
            {
                isInAttackedRange = false;
            }
        }
        private void Hurt()
        {
            if (isInAttackedRange && Input.GetKeyDown(KeyCode.J))
            {
                _animator.SetTrigger("hurt");
            }
        }
        public void IsHurting()
        {
            isHurt = true;
        }
        public void UnHurting()
        {
            isHurt = false;
        }
        private void HurtBack()
        {
            Rigidbody2D.velocity = new Vector2(-faceDirection * moveSpeed * 2, Rigidbody2D.velocity.y);
        }        
    }
}
