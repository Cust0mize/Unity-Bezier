using UnityEngine;
using System;

namespace Bezier.Scripts.Models {
    public class BezierPointModel {
        public Vector3 MainPointPosition { get; private set; }
        public Vector3 HelpPointPosition { get; private set; }
        public Vector3 MainLocalPointPosition { get; private set; }
        public Vector3 HelpLocalPointPosition { get; private set; }
        public event Action<Vector3> OnUpdateMainPosition;
        public event Action<Vector3> OnUpdateHelpPosition;
        public event Action<Vector3> OnUpdateMainLocalPosition;
        public event Action<Vector3> OnUpdateHelpLocalPosition;

        public BezierPointModel(Vector3 mainPointPosition, Vector3 helpPointPosition) {
            MainPointPosition = mainPointPosition;
            HelpPointPosition = helpPointPosition;
        }

        public void UpdateMainPosition(Vector3 newPosition) {
            MainPointPosition = newPosition;
            OnUpdateMainPosition?.Invoke(MainPointPosition);
        }

        public void UpdateHelpPosition(Vector3 newPosition) {
            HelpPointPosition = newPosition;
            OnUpdateHelpPosition?.Invoke(HelpPointPosition);
        }

        public void UpdateMainLocalPosition(Vector3 newPosition) {
            MainLocalPointPosition = newPosition;
            OnUpdateMainLocalPosition?.Invoke(MainLocalPointPosition);
        }

        public void UpdateHelpLocalPosition(Vector3 newPosition) {
            HelpLocalPointPosition = newPosition;
            OnUpdateHelpLocalPosition?.Invoke(HelpLocalPointPosition);
        }
    }
}