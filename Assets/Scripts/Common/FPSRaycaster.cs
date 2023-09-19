using System;
using UnityEngine;

namespace Common
{
    public static class FPSRaycaster
    {
        public static Camera FpCamera = null;
        
        public static Ray GenerateRay()
        {
            if (FpCamera == null)
            {
                throw new Exception("You must set First person camera before using FPSRaycaster.Raycast");
            }
            else
            {
                return new Ray(FpCamera.transform.position, FpCamera.transform.forward);
            }
        }
    }
}