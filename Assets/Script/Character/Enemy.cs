using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f; // npc�ƶ��ٶ�
    public float attackTime = 2f; // �������ʱ��
    public float attackRange = 1f; // ��������

    private Animator anim; // npc����������
    private Rigidbody2D rb; // npc�������
    private Transform player; // ��Ҷ���
    private bool isMoving = false; // npc�Ƿ������ƶ�
    private bool isAttacking = false; // npc�Ƿ����ڹ���
    private float timer = 0f; // ������ʱ��

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform; // ��ȡ��Ҷ���
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < attackRange)
        { // ��ҽ��빥����Χ
            isMoving = false;
            if (!isAttacking)
            { // npc��δ����
                timer += Time.deltaTime;
                if (timer >= attackTime)
                { // ����ʱ�����ѵ�
                    anim.SetTrigger("Attack"); // ������������
                    isAttacking = true;
                    timer = 0;
                }
            }
        }
        else
        { // ��Ҳ��ڹ�����Χ��
            if (isAttacking)
            { // npc���ڹ���״̬
                anim.SetTrigger("StopAttack"); // ����ֹͣ��������
                isAttacking = false;
            }
            if (!isMoving)
            { // npc��δ����Ѳ�߻�׷��
                anim.SetTrigger("Walk"); // �������߶���
                isMoving = true;
            }
            if (isMoving)
            { // ����Ѳ�߻�׷��
                Vector2 direction = player.position - transform.position;
                rb.velocity = direction.normalized * speed; // npc������ƶ�
                transform.localScale = new Vector3(Mathf.Sign(direction.x), 1f, 1f); // npc�������
            }
        }
    }

    // ���ڶ����¼��е��õĺ�������ʾnpc��������
    public void AttackEnd()
    {
        isAttacking = false;
    }
}
