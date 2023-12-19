using Cinemachine;
using UnityEngine;

namespace Extention
{
    public class CinemachinePOVExtension : CinemachineExtension
    {
        private Vector3 _startingRotation;

        public void SetRotation(Vector3 startTransformRotation)
        {
            _startingRotation = startTransformRotation;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    state.RawOrientation = Quaternion.Euler(-_startingRotation.y, _startingRotation.x,0f);
                }
            }
        }
    }
}
