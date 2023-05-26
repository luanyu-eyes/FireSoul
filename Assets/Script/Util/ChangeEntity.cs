using System.Collections;
using UnityEngine;

namespace Script.Util
{
    public class ChangeEntity : MonoBehaviour
    {
        public float changeDistance;

        private GameObject[] _position;
        private GameObject _player;
        private GameObject _canCreate;

        private void Start()
        {
            _position = FindObj();
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            Change();
            if (_player == null)
            {
                _position = FindObj();
                _player = GameObject.FindWithTag("Player");
            }
        }

        private void Change()
        {
            if (Input.GetKeyDown(KeyCode.F) && CanChange())
            {
                _player.transform.position = new Vector2(_canCreate.transform.position.x,_player.transform.position.y);
                StartCoroutine(DestroyPlayer(0.1f));
                _canCreate.GetComponent<Animator>().enabled = true;
            }
        }

        IEnumerator DestroyPlayer(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(_player);
        }

        private bool CanChange()
        {
            var position = _player.transform.position;
            foreach (var obj in _position)
            {
                var temp = obj.transform.position;
                if (Mathf.Abs(temp.x - position.x) < changeDistance && Mathf.Abs(temp.y - position.y) < 3.0f)
                {
                    _canCreate = obj;
                    return true;
                }
            }

            return false;
        }

        private GameObject[] FindObj()
        {
            var entityCreate = GameObject.FindGameObjectsWithTag("EntityCreate");

            return entityCreate;
        }
    }
}
