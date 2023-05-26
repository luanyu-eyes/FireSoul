using System.IO;
using Script.Character.Entity;
using Script.Character.Soul;
using UnityEngine;

namespace Script.Character
{
    public class ControllerChange : MonoBehaviour
    {
        private Transform _transform;
        private Vector2 TempPosition { set; get;}  //位置信息临时存储

        private ControllerChangeJ _controllerChangeJ;
        private GameObject _entity;

        private void Start()
        {
            string filePath = Application.dataPath + "/JSON/Character/ControllerChangeJ.json";
            string dataAsJson = File.ReadAllText(filePath);
            _controllerChangeJ = JsonUtility.FromJson<ControllerChangeJ>(dataAsJson);

            _transform = transform;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.E) && _controllerChangeJ.isStay && !_controllerChangeJ.canEnter)
            {
                SoulExitEntity();
            }
            if (Input.GetKey(KeyCode.F) && _controllerChangeJ.canEnter && !_controllerChangeJ.isStay)
            {
                SoulEnterEntity();
            }

            if (_controllerChangeJ.canSoulFlash)
            {
                SoulFlash();
                if ((TempPosition - (Vector2)_transform.position).magnitude < 0.2F)
                {
                    _controllerChangeJ.canSoulFlash = false;
                    GetComponent<SoulController>().CanControl = true;
                }
            }
        }

        private void SoulEnterEntity() //灵魂体进入实体
        {
            if (_entity != null)
            {
                var vector2 = _entity.GetComponent<Transform>().position;
                _transform.position = Vector2.Lerp(transform.position,vector2,_controllerChangeJ.soulEnterEntitySpeed);
            }

            var distance = _entity.transform.position - transform.position;
            if (_entity != null && Mathf.Abs(distance.magnitude) < _controllerChangeJ.changeControlDistance)
            {
                _controllerChangeJ.canEnter = false;
                _controllerChangeJ.isStay = true;

                _transform.position = _entity.transform.position;
                _transform.parent = _entity.transform;

                GetComponent<SoulController>().CanControl = false;
                _entity.GetComponent<EntityController>().CanControl = true;
            }
        }

        private void SoulFlash()
        {
            _transform.position = Vector2.Lerp(_transform.position, TempPosition,_controllerChangeJ.soulFlashSpeed);
        }
        
        private void SoulExitEntity()  //灵魂体离开实体
        {
            if (_entity != null)
            {
                transform.parent = null;

                _controllerChangeJ.canSoulFlash = true;
                GetComponent<SoulController>().CanControl = false;
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");
                if (moveX + moveY == 0) moveY = 1;
                TempPosition = _transform.position + new Vector3(moveX * _controllerChangeJ.soulFlashDistance, moveY * _controllerChangeJ.soulFlashDistance, 0);

                _controllerChangeJ.canEnter = true;
                _controllerChangeJ.isStay = false;

                GetComponent<SoulController>().CanControl = true;
                _entity.GetComponent<EntityController>().CanControl = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)  //实体进入可附身范围
        {
            if (col.CompareTag("Entity"))
            {
                _controllerChangeJ.canEnter = true;
                _entity = col.gameObject;
            }
        }

        private void OnTriggerExit2D(Collider2D other)  //实体离开可附身范围
        {
            if (other.CompareTag("Entity"))
            {
                _controllerChangeJ.canEnter = false;
            }
        }

        [System.Serializable]
        private class ControllerChangeJ
        {
            public bool canEnter;  //灵魂体能否进入实体
            public bool isStay;  //灵魂体是否位于灵魂体中
            public float soulEnterEntitySpeed;  //灵魂体进入实体的速度
            public float changeControlDistance; //切换控制所需的距离
            public bool canSoulFlash;  //灵魂体能否闪现
            public float soulFlashSpeed;  //灵魂闪现的速度
            public float soulFlashDistance;  //灵魂闪现的距离
        }
    }
}
