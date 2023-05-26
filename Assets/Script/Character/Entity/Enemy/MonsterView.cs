using Script.Character.Entity.Player;
using UnityEngine;

namespace Script.Character.Entity.Enemy
{
    public class MonsterView : MonoBehaviour
    {
        public bool PlayerInView { private set; get; }  //玩家是否在视野中
        public GameObject Player { private set; get; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Basic>())  //当玩家进入视野中时将PlayerInView设置为真
            {
                PlayerInView = true;
                Player = other.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Basic>())
            {
                PlayerInView = false;
                Player = null;
            }
        }
    }
}
