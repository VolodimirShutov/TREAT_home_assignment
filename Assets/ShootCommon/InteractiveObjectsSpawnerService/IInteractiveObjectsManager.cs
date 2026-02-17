using System;
using UnityEngine;

namespace ShootCommon.InteractiveObjectsSpawnerService
{
    public interface IInteractiveObjectsManager
    {
        public static IInteractiveObjectsManager Instance { get; }
        void AddContainer(string key, IInteractiveObjectContainer container);
        void RemoveContainer(string key);
        void Instantiate(string prefabId, string containerKey, Action<GameObject> callback = null);
        void Instantiate(string prefabId, IInteractiveObjectContainer container, Action<GameObject> callback = null);
        bool ContainerIsExists(string containerKey);
        IInteractiveObjectContainer GetContainer(string containerKey);
    }
}