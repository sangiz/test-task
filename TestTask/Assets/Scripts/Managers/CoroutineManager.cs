using System.Collections;
using UnityEngine;

namespace IgnSDK
{
    public class CoroutineManager : MonoBehaviour
    {  
        public Coroutine Begin(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }

        public void Begin(IEnumerator coroutine, ref Coroutine current)
        {
            current = StartCoroutine(coroutine);
        }

        public void Stop(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        public static CoroutineManager Create()
        {
            var coroutineObj = new GameObject();

            var coroutineManager = coroutineObj.AddComponent<CoroutineManager>();

            coroutineObj.name = "CoroutineManager";

            return coroutineManager;
        }
    }
}