using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // npc移动速度
    public float attackTime = 2f; // 攻击间隔时间
    public float attackRange = 1f; // 攻击距离

    private Animator anim; // npc动画控制器
    private Rigidbody2D rb; // npc刚体组件
    private Transform player; // 玩家对象
    private bool isMoving = false; // npc是否正在移动
    private bool isAttacking = false; // npc是否正在攻击
    private float timer = 0f; // 攻击计时器

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // 获取玩家对象
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < attackRange)
        { // 玩家进入攻击范围
            isMoving = false;
            if (!isAttacking)
            { // npc还未攻击
                timer += Time.deltaTime;
                if (timer >= attackTime)
                { // 攻击时间间隔已到
                    anim.SetTrigger("Attack"); // 触发攻击动画
                    isAttacking = true;
                    timer = 0;
                }
            }
        }
        else
        { // 玩家不在攻击范围内
            if (isAttacking)
            { // npc还在攻击状态
                anim.SetTrigger("StopAttack"); // 触发停止攻击动画
                isAttacking = false;
            }
            if (!isMoving)
            { // npc还未进行巡逻或追击
                anim.SetTrigger("Walk"); // 触发行走动画
                isMoving = true;
            }
            if (isMoving)
            { // 进行巡逻或追击
                Vector2 direction = player.position - transform.position;
                rb.velocity = direction.normalized * speed; // npc朝玩家移动
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f); // npc朝向玩家
            }
        }
    }

    // 用于动画事件中调用的函数，表示npc攻击结束
    public void AttackEnd()
    {
        isAttacking = false;
    }
}
