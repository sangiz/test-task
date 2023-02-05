using System;
using System.Collections;
using UnityEngine;

namespace IgnSDK
{
    public static class Async
    {
        public static void SkipFrame(Action onComplete)
        {
            App.CoroutineManager.Begin(SkipFrameCoroutine(onComplete));
        }

        public static void WaitUntil(Func<bool> condition, Action onComplete)
        {
            App.CoroutineManager.Begin(WaitUntilCoroutine(condition, onComplete));
        }

        public static Coroutine Delay(float duration, Action onComplete)
        {
            return App.CoroutineManager.Begin(DelayCoroutine(duration, onComplete));
        }

        private static IEnumerator SkipFrameCoroutine(Action onComplete)
        {
            yield return new WaitForEndOfFrame();
            onComplete?.Invoke();
        }

        private static IEnumerator DelayCoroutine(float duration, Action onComplete)
        {
            yield return new WaitForSeconds(duration);

            onComplete?.Invoke();
        }

        private static IEnumerator WaitUntilCoroutine(Func<bool> condition, Action onComplete)
        {
            yield return new WaitUntil(condition);
            onComplete?.Invoke();
        }
    }
}