using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace EasyCS.Samples
{
    public static class NearestFinder
    {
        public static T FindNearest<T>(this Transform obj, IEnumerable<T> objects,
            float maxSearchDistance = float.MaxValue,
            params T[] exclude)
            where T : MonoBehaviour
        {
            return FindNearestFromPoint(obj.position, objects, maxSearchDistance, exclude);
        }

        public static Actor FindNearestActor(this Actor actor, IEnumerable<Entity> entities,
            float maxSearchDistance = float.MaxValue, params Entity[] exclude)
        {
            using var pooledList = ListPool<Actor>.Get(out var actorComponents);
            using var pooledExclude = HashSetPool<Entity>.Get(out var excludeSet);

            if (exclude is { Length: > 0 })
            {
                foreach (var e in exclude)
                    if (e != null)
                        excludeSet.Add(e);
            }

            foreach (var entity in entities)
            {
                if (entity == null || excludeSet.Contains(entity)) continue;
                if (entity.Actor is Actor a) actorComponents.Add(a);
            }

            return FindNearestFromPoint(actor.transform.position, actorComponents, maxSearchDistance, actor);
        }

        public static Actor FindNearestActor(this Actor actor, IEnumerable<Actor> actors,
            float maxSearchDistance = float.MaxValue, params Actor[] exclude)
        {
            using var pooledList = ListPool<Transform>.Get(out var transforms);
            using var pooledExclude = HashSetPool<Actor>.Get(out var excludeSet);

            if (exclude is { Length: > 0 })
            {
                foreach (var e in exclude)
                    if (e != null)
                        excludeSet.Add(e);
            }

            foreach (var a in actors)
            {
                if (a != null && !excludeSet.Contains(a))
                    transforms.Add(a.transform);
            }

            var nearestTransform = FindNearestFromPoint(actor.transform.position, transforms, maxSearchDistance);
            return nearestTransform ? nearestTransform.GetComponent<Actor>() : null;
        }

        public static T FindNearestFromPoint<T>(this Vector3 point, IEnumerable<T> objects,
            float maxSearchDistance = float.MaxValue, params T[] exclude)
            where T : MonoBehaviour
        {
            using var pooledList = ListPool<Transform>.Get(out var transforms);
            using var pooledExclude = HashSetPool<Transform>.Get(out var excludeSet);

            if (exclude is { Length: > 0 })
            {
                foreach (var e in exclude)
                    if (e != null)
                        excludeSet.Add(e.transform);
            }

            foreach (var obj in objects)
            {
                if (obj != null && !excludeSet.Contains(obj.transform))
                    transforms.Add(obj.transform);
            }

            var nearestTransform = FindNearestFromPoint(point, transforms, maxSearchDistance);
            return nearestTransform ? nearestTransform.GetComponent<T>() : null;
        }

        public static Transform FindNearest(this Transform obj, IEnumerable<Transform> objects,
            float maxSearchDistance = float.MaxValue, params Transform[] exclude)
        {
            using var pooledExclude = HashSetPool<Transform>.Get(out var excludeSet);

            if (exclude != null)
            {
                foreach (var t in exclude)
                    if (t != null)
                        excludeSet.Add(t);
            }

            excludeSet.Add(obj);
            return FindNearestFromPoint(obj.position, objects, maxSearchDistance, excludeSet);
        }

        public static Transform FindNearestFromPoint(this Vector2 pointFrom, IEnumerable<Transform> objects,
            float maxSearchDistance = float.MaxValue, params Transform[] exclude)
        {
            return FindNearestFromPoint((Vector3)pointFrom, objects, maxSearchDistance, exclude);
        }

        public static Transform FindNearestFromPoint(this Vector3 pointFrom, IEnumerable<Transform> objects,
            float maxSearchDistance = float.MaxValue, params Transform[] exclude)
        {
            using var pooledExclude = HashSetPool<Transform>.Get(out var excludeSet);

            if (exclude != null)
            {
                foreach (var t in exclude)
                    if (t != null)
                        excludeSet.Add(t);
            }

            return FindNearestFromPoint(pointFrom, objects, maxSearchDistance, excludeSet);
        }

        private static Transform FindNearestFromPoint(Vector3 pointFrom, IEnumerable<Transform> objects,
            float maxSearchDistance, HashSet<Transform> excludeSet)
        {
            Transform nearest = null;
            float nearestDistSq = maxSearchDistance * maxSearchDistance;

            foreach (var obj in objects)
            {
                if (obj == null || excludeSet.Contains(obj))
                    continue;

                float distSq = (pointFrom - obj.position).sqrMagnitude;
                if (distSq < nearestDistSq)
                {
                    nearest = obj;
                    nearestDistSq = distSq;
                }
            }

            return nearest;
        }
    }
}