using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Packages.GameControl
{
    public class RestartButtonPresenter: MonoBehaviour
    {
        [Inject] private SignalService _signalService;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnRestart);
        }

        private void OnRestart()
        {
            Debug.Log("Restart");
            _signalService.Send(new RestartGameSignal());
        }
    }
}