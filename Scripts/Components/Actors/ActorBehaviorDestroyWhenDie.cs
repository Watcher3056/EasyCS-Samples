using EasyCS.EventSystem;
using System.Collections;
using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorBehaviorDestroyWhenDie : ActorComponent, IEventListener<EventDied>
    {
        public void HandleEvent(in EventContext<EventDied> ctx)
        {
            StartCoroutine(PlayAnim());
        }

        private IEnumerator PlayAnim()
        {
            yield return new WaitForSeconds(2f);

            float duration = 1f;

            Vector3 startPos = transform.position;
            Vector3 endPos = startPos + Vector3.down * 1.5f;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                transform.position = Vector3.Lerp(startPos, endPos, t);
                yield return null;
            }

            transform.position = endPos;

            Destroy(gameObject);
        }
    }
}
