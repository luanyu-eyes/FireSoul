using UnityEngine;

namespace Script.Character.Entity.Player
{
    public class StrengthCreate : MonoBehaviour
    {
        public GameObject strength;
        
        public void Create()
        {
            Instantiate(strength, transform.position - new Vector3(0,0.8f,0), transform.rotation);
            Destroy(gameObject);
        }
    }
}
