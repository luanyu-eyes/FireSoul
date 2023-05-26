using System;
using System.IO;
using UnityEngine;

namespace Script.Character.Entity
{
    public class EntityController : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        public bool CanControl { get; set; }  //启用实体控制                                                        

        private EntityControllerJ _entityControllerJ;
        private Animator _animator;                    
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");                                                                                                                                                                                                                                                                               
            
        private void Start()
        {
            CanControl = false;
            
            string filePath = Application.dataPath + "/JSON/Character/Entity/EntityControllerJ.json";
            string dataAsJson = File.ReadAllText(filePath);
            _entityControllerJ = JsonUtility.FromJson<EntityControllerJ>(dataAsJson);
            
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }
    
        private void Update()
        {
            if(CanControl)
            {
                var moveX = Input.GetAxis("Horizontal");
                if (Math.Abs(moveX) < 0.000001)
                {
                    _animator.SetBool(IsMoving,false);
                }                       
                else
                {
                    _animator.SetBool(IsMoving,true);
                }

                var v2 = new Vector3(moveX, 0,0) * _entityControllerJ.moveSpeed;

                transform.position += v2 * Time.deltaTime;

                if(_entityControllerJ.canJump){
                    Jump();
                }
            }
        }

        private void Jump()
        {
            if (_entityControllerJ.isOnGround && Input.GetKeyDown(KeyCode.Space))
            {
                _rigidbody2D.gravityScale = _entityControllerJ.graveScale;
                _rigidbody2D.velocity += new Vector2(0,_entityControllerJ.jumpInitialSpeed);
            }

            if (_entityControllerJ.canJumpHigher && Input.GetKey(KeyCode.Space))
            {
                _rigidbody2D.AddForce(new Vector2(0,_entityControllerJ.force));
            }

            if (!_entityControllerJ.isOnGround && _rigidbody2D.velocity.y < 0.01F)
            {
                _rigidbody2D.gravityScale = _entityControllerJ.graveScale * 2;
                _entityControllerJ.canJumpHigher = false;
            }
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Ground"))
            {
                _entityControllerJ.isOnGround = true;
                _entityControllerJ.canJumpHigher = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                _entityControllerJ.isOnGround = false;
            }
        }
        
        [Serializable]
        private class EntityControllerJ
        {
            public float moveSpeed;  //实体移动速度
            public bool isOnGround;  //实体是否在地面上
            public float jumpInitialSpeed;  //初始跳跃速度
            public bool canJumpHigher;  //能否跳的更高
            public float force;  //更高的跳跃所需要的力的大小
            public float graveScale;  //重力
            public bool canJump;  //可以进行跳跃
        }
    }
}

