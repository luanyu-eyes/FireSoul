using UnityEngine;

namespace Script.Character.Entity.Player
{
    public class Basic : Entity  //基础型载体
    {
        
        public float maxHoverTime;  //跳跃是在空中最长的持续时间
        public float maxJumpSpeed;  //最大的跳跃速度
        public GameObject directionTarget;

        private float _time;  //用来记录时间的变量
        private int _jumpTimes;  //跳跃的次数
        private AttackCheck _attackCheck0;  //攻击检测
        private float _gravityScale;
        private Animator _animator;
        private bool _canJumpHigh;
        
        private BasicAnimationController _basicAnimationController;
        protected override void Start()
        {
            base.Start();
            
            _time = 0;
            _jumpTimes = 0;
            _attackCheck0 = transform.GetChild(0).gameObject.GetComponent<AttackCheck>();
            _gravityScale = Rigidbody2D.gravityScale;
            _animator = GetComponent<Animator>();
            _canJumpHigh = false;
            
            _basicAnimationController = new BasicAnimationController(_animator);
        }

        protected override void Update()
        {
            base.Update();
            WalkOnWall();
            JumpFromWall();
            SelfUpdate();
            State();
        }

        protected virtual void SelfUpdate()
        {
            _basicAnimationController.PlayAnimation(IsOnGround,IsOnWall);
        }

        protected virtual void WalkOnWall()  //墙上行走
        {
            if (IsOnWall && !IsOnGround)
            {
                float faceDir = Input.GetAxisRaw("Vertical");
                if (faceDir != 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x,faceDir,1);

                }
                
                var moveY = Input.GetAxis("Vertical");
                transform.position += new Vector3(0,moveY * moveSpeed * Time.deltaTime, 0);
                Rigidbody2D.gravityScale = 0;
            }
        }

        private void JumpFromWall()  //从墙上起跳
        {
            if (IsOnWall && !IsOnGround && Input.GetKeyDown(KeyCode.Space))
            {
                _time = Time.time;
                _canJumpHigh = true;
                transform.position += new Vector3(0.1f * transform.localScale.x * -1, 0, 0);
            }

            if (!IsOnGround && Input.GetKey(KeyCode.Space) && (Time.time - _time) < 3.0f && _canJumpHigh)
            {
                Rigidbody2D.velocity = new Vector2(0, 3);
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _canJumpHigh = false;
            }
        }

        protected override void Attack()  //重写父类的攻击方法，可实现多段攻击
        {
            Attack0();
        }

        private void Attack0()  //第一段攻击
        {
            if(Input.GetKeyDown(KeyCode.J)){
                var attackTarget = _attackCheck0.GetAttackTarget(); 
                if (attackTarget)
                {
                    attackTarget.GetComponent<Entity>().Damage(2.0f);  //调用目标对象Entity脚本的Damage方法
                }
            }
        }

        protected override void Jump()  //跳跃方法
        {
            if (IsOnGround && Input.GetKeyDown(KeyCode.Space))
            {
                _time = Time.time;  //按下跳跃键时记录跳跃开始的时间
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxJumpSpeed); //为跳跃提供初始的速度
                IsOnGround = false;  //将状态设置为不在地上
                Rigidbody2D.gravityScale = 0.5f * _gravityScale;  //将重力设置为初始值的一半
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _jumpTimes += 1;  //跳跃次数加1
                Rigidbody2D.gravityScale = _gravityScale;  //重力恢复为初始值
            }

            if (!IsOnGround && _jumpTimes < 1 && Input.GetKey(KeyCode.Space) && (Time.time - _time) < maxHoverTime)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, maxJumpSpeed);  //当跳跃键持续输入时，保持对象向上的速度直到跳跃时间达到最大跳跃持续时间
            }
        }

        protected override void Move()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                float faceDir = Input.GetAxisRaw("Horizontal");
                if (faceDir != 0)
                {
                    //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, (faceDir - 1) * 90, 0));
                    transform.localScale = new Vector3(faceDir,1, 1);
                }
                
                float moveX = Input.GetAxis("Horizontal");
                if(!IsOnWall){
                    transform.position += new Vector3(moveX * moveSpeed * Time.deltaTime, 0, 0);
                }
            }
        }

        public override void Damage(float damage)
        {
            base.Damage(damage);
        }

        private void State()
        {
            if (IsOnGround)
            {
                _jumpTimes = 0;
            }

            if (IsOnWall)
            {
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
                Rigidbody2D.gravityScale = 0;
            }
            else
            {
                Rigidbody2D.gravityScale = _gravityScale;
            }
        }
    }
}
