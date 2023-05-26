using UnityEngine;

namespace Script.Character.Soul
{
    public class SoulController : MonoBehaviour
    {
        public float moveSpeed;   //移动速度
        public bool CanControl { get; set; }  //能否控制移动

        private void Start()
        {
            CanControl = true;
        }

        private void Update()
        {
            if(CanControl)
            {
                var moveX = Input.GetAxis("Horizontal");
                var moveY = Input.GetAxis("Vertical");

                var v2 = new Vector3(moveX, moveY,0) * moveSpeed;

                transform.position += v2 * Time.deltaTime;
            }
        }
    }
}
