using UnityEngine;
using System;

namespace Assets.Scripts.Games.Free.ID29.Bezier.Mono {
    [Serializable]
    public struct OffsetSettings {
        [field: SerializeField] public float PerpendecularPointOffsetOfCenter { get; private set; }
        [field: SerializeField] public float InverseperpendecularPointOffsetOfCenter { get; private set; }

        public OffsetSettings(float perpendecularOffset, float invercePerpendecularOffset) {
            InverseperpendecularPointOffsetOfCenter = invercePerpendecularOffset;
            PerpendecularPointOffsetOfCenter = perpendecularOffset;
        }
    }
}
