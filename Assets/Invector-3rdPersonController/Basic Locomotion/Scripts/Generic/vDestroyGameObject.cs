using UnityEngine;
using System.Collections;
namespace Invector
{
    public class vDestroyGameObject : MonoBehaviour
    {
        public float delay;
        private float Destorytime = 0;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
        private void Update()
        {
            Destorytime += Time.deltaTime;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (Destorytime > 0.01f)
            {
                Destroy(this.gameObject);

            }        
        }
    }
}