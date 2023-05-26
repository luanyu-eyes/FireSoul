using UnityEngine;

namespace Script.Character.Entity.Player
{
    public class QuickCreate : MonoBehaviour
    {
        public GameObject quick;
        
        public void Create()
        {
            Instantiate(quick, transform.position - new Vector3(0,0.8f,0), transform.rotation);
            Destroy(gameObject);
        }
    }
}
