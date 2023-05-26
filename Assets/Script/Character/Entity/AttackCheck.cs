using UnityEngine;

namespace Script.Character.Entity
{
    public class AttackCheck : MonoBehaviour  //检查攻击检测框是否与目标对象发生碰撞
    {
        private GameObject _attackTarget;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Entity>())  //当对象中有Entity类或者其子类的脚本时，将该对象设置为目标对象
            {
                _attackTarget = other.gameObject;
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<Entity>())  //当对象中有Entity类或者其子类的脚本离开时，删除对象
            {
                _attackTarget = null;
            }
        }

        public GameObject GetAttackTarget()
        {
            return _attackTarget;
        }
    }
}
