
using UnityEngine;

namespace IgnSDK
{
    public class Rotator : MonoBehaviour
    {
        void Update()
        {
            var speed = 50 * Time.deltaTime;
            transform.Rotate(0, speed, 0);
        }
    }
}
