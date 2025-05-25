
using EasyCS.EventSystem;
using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public class ActorBehaviorProgressBarHealth : ActorComponent, ILateUpdate, IEventListener<EventHealthChanged>
    {
        [SerializeField, Required]
        private ProgressBar _progressBarPrefab;
        [SerializeField, Required]
        private Transform _holder;

        [Bind]
        private EntityDataHealth _dataHealth;
        [Bind]
        private EntityDataHealthMax _dataHealthMax;

        private ProgressBar _progressBarInstance;


        protected override void HandleAwake()
        {
            base.HandleAwake();

            _progressBarInstance = 
                EasyCsContainer.Instantiate(_progressBarPrefab.gameObject, _holder)
                .GetComponent<ProgressBar>();

            UpdateView();
        }

        public void HandleEvent(in EventContext<EventHealthChanged> ctx)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            _progressBarInstance.SetProgress(_dataHealth.Value / _dataHealthMax.Value);
        }

        public void OnLateUpdate(float deltaTime)
        {
            _holder.transform.LookAt(Camera.main.transform.position);
            _holder.transform.forward = -_holder.transform.forward;
        }

    }
}
