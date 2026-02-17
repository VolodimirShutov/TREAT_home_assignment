using UnityEngine;

namespace ShootCommon.InteractiveObjectsSpawnerService
{
    public class InteractiveObjectContainerWithID : MonoBehaviour, IInteractiveObjectContainer
    {
        [SerializeField] private string containerId;
        private IInteractiveObjectsManager _interactiveObjectContainerManager;
        private bool _isRegistered;
        
        private void Awake()
        {
            Registrate();
        }
        
        private IInteractiveObjectsManager InteractiveObjectContainerManager =>
            _interactiveObjectContainerManager ??= InteractiveObjectsManager.Instance;

        private void Registrate()
        {
            if (_isRegistered)
            {
                return;
            }
            _isRegistered = true;
            InteractiveObjectContainerManager.AddContainer(containerId, this);
        }

        public GameObject CreateItem(GameObject inst)
        {
            GameObject item = Instantiate(inst, transform, false);
            return item;
        }

        public bool ContainerIsExist()
        {
            return gameObject != null;
        }

        public void AddItem(GameObject item)
        {
            item.transform.parent = transform;
        }

        public void OnDestroy()
        {
            InteractiveObjectContainerManager.RemoveContainer(containerId);
        }
    }
}