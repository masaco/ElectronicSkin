using System;
using UnityEngine;

public class SZVRHeadBob : MonoBehaviour
    {
        public Camera Camera;
        public SZVRCurveControlledBob motionBob = new SZVRCurveControlledBob();
        public SZVRLerpControlledBob jumpAndLandingBob = new SZVRLerpControlledBob();
        public SZVRFirstPersonController szvrFirstPersonController;
        public float StrideInterval;
        [Range(0f, 1f)] public float RunningStrideLengthen;

       // private CameraRefocus m_CameraRefocus;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;


        private void Start()
        {
            motionBob.Setup(Camera, StrideInterval);
            m_OriginalCameraPosition = Camera.transform.localPosition;
       //     m_CameraRefocus = new CameraRefocus(Camera, transform.root.transform, Camera.transform.localPosition);
        }


        private void Update()
        {
          //  m_CameraRefocus.GetFocusPoint();
            Vector3 newCameraPosition;
            if (szvrFirstPersonController.Velocity.magnitude > 0 && szvrFirstPersonController.Grounded)
            {
                Camera.transform.localPosition = motionBob.DoHeadBob(szvrFirstPersonController.Velocity.magnitude * (szvrFirstPersonController.Running ? RunningStrideLengthen : 1f));
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
            }
            else
            {
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();
            }
            Camera.transform.localPosition = newCameraPosition;

            if (!m_PreviouslyGrounded && szvrFirstPersonController.Grounded)
            {
                StartCoroutine(jumpAndLandingBob.DoBobCycle());
            }

            m_PreviouslyGrounded = szvrFirstPersonController.Grounded;
          //  m_CameraRefocus.SetFocusPoint();
        }
    }
