using UnityEngine;
using System;

namespace Bezier.Scripts.Mono {
    [Serializable]
    public struct OffsetSettings {
        [field: SerializeField] public float PerpendicularPointOffsetOfCenter { get; private set; }
        [field: SerializeField] public float InversePerpendicularPointOffsetOfCenter { get; private set; }

        public OffsetSettings(float perpendicularOffset, float inversePerpendicularOffset) {
            InversePerpendicularPointOffsetOfCenter = inversePerpendicularOffset;
            PerpendicularPointOffsetOfCenter = perpendicularOffset;
        }
    }
}
