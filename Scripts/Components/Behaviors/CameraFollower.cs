using EasyCS.Groups;
using System.Linq;
using TriInspector;
using UnityEngine;

namespace EasyCS.Samples
{
    public class CameraFollower : EasyCSBehavior, IUpdate
    {
        [SerializeField, Required]
        private Camera _camera;
        [SerializeField]
        private Vector3 _offset;

        private IGroup _groupPlayer;
        private GroupsSystem _groupsSystem;

        protected override void HandleSetupContainer()
        {
            base.HandleSetupContainer();

            _groupsSystem = EasyCsContainer.Resolve<GroupsSystem>();
        }

        protected override void HandleAwake()
        {
            base.HandleAwake();

            _groupPlayer = CustomGroupBuilder
                .Create(_groupsSystem)
                .WithActor<Actor>()
                .WithActorComponent<ActorBehaviorPlayerInput>()
                .Build();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_groupPlayer.Actors.Count() == 0)
                return;

            transform.position = _groupPlayer.Actors.First().transform.position + _offset;
        }
    }
}
