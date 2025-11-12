using System;
using System.Collections.Generic;
using UnityEngine;

namespace UPatterns
{
    [Serializable]
    public class Pool<T> where T : Component
    {
        [field: SerializeField] public Transform Parent { private set; get; }
        [field: SerializeField] public T Prefab { private set; get; }

        private List<T> items = new List<T>();
        private Func<Transform, T> factory;

        public void SetFactory(Func<Transform, T> factory) =>
            this.factory = factory;

        /// <summary>
        /// Property: Return all active items (expensive)
        /// </summary>
        public T[] ActiveItems
        {
            get
            {
                List<T> activeList = new List<T>();
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].gameObject.activeSelf)
                        activeList.Add(items[i]);
                }
                return activeList.ToArray();
            }
        }

        /// <summary>
        /// Get an inactive item, or create new one if none is available
        /// </summary>
        public T Get
        {
            get
            {
                for (int i = 0; i < items.Count; i++)
                {
                    if (!items[i].gameObject.activeSelf)
                        return items[i];
                }

                return AddNewItem();
            }
        }

        /// <summary>
        /// Get an active item
        /// </summary>
        public T GetActive
        {
            get
            {
                var item = Get;
                item.gameObject.SetActive(true);
                return item;
            }
        }

        private T AddNewItem()
        {
            var item = factory != null ? factory(Parent) : UnityEngine.Object.Instantiate(Prefab, Parent);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            items.Add(item);
#if UNITY_EDITOR
            item.name = typeof(T) + "_" + items.Count;
#endif
            item.gameObject.SetActive(true);
            return item;
        }

        public void RemoveInstance(T item)
        {
            items.Remove(item);
            if (item != null)
            {
                UnityEngine.Object.Destroy(item.gameObject);
            }
        }

        public void RemoveAllInstance()
        {
            while (items.Count > 0)
                RemoveInstance(items[0]);
        }

        public void DeactiveAllInstance()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] != null)
                    items[i].gameObject.SetActive(false);
            }
        }

        public void SetParent(Transform parent) =>
            Parent = parent;

        public void SetPrefab(T prefab)
        {
            RemoveAllInstance();
            Prefab = prefab;
        }
    }
}
