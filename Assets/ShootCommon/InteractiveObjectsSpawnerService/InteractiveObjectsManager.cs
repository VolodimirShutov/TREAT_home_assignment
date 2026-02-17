using System;
using System.Collections.Generic;
using ShootCommon.AssetReferences;
using UnityEngine;

namespace ShootCommon.InteractiveObjectsSpawnerService
{
    public class InteractiveObjectsManager: IInteractiveObjectsManager
    {
        private readonly Dictionary<string, IInteractiveObjectContainer> _containers = new Dictionary<string, IInteractiveObjectContainer>();
        private static InteractiveObjectsManager _instance;
        
        public static IInteractiveObjectsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InteractiveObjectsManager();
                }

                return _instance;
            }
        }
        
        public void AddContainer(string key, IInteractiveObjectContainer container)
        {
            if (_containers.ContainsKey(key))
            {
                RemoveContainer(key);
            }
            _containers.Add(key, container);
        }
        
        public void RemoveContainer(string key)
        {
             _containers.Remove(key);
        }

        public bool ContainerIsExists(string containerKey)
        {
            return _containers.ContainsKey(containerKey);
        }

        public IInteractiveObjectContainer GetContainer(string containerKey)
        {
            return _containers.ContainsKey(containerKey)?_containers[containerKey] : null;
        }
        
        public void Instantiate(string prefabId, string containerKey, Action<GameObject> callback = null)
        {
            if (!_containers.ContainsKey(containerKey))
            {
                Debug.LogError($"Container {containerKey} don't exist");
                return;
            }
            IInteractiveObjectContainer container = _containers[containerKey];
            Instantiate(prefabId, container, callback);
        }

        public void Instantiate(string prefabId, IInteractiveObjectContainer container,
            Action<GameObject> callback = null)
        {
            if (container == null)
            {
                Debug.LogError("InteractiveObjectsManager   Instantiate  container is null. ");
                return;
            }
            
            AssetReferenceStorage.Instance.SpawnById(prefabId, go =>
            {
                GameObject item = container.CreateItem(go);
                item.transform.localScale = Vector3.one;
                callback?.Invoke(item);
            });
            
        }
    }
}