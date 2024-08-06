using UnityEngine;

namespace Assets.Packages.Bezier.Scripts.Temp {
    public class PositionConvertor {
        public Vector3 ConvertPixelToWorld(Vector2 pixelPosition, Texture2D texture2D, MeshRenderer meshRenderer) {
            float u = pixelPosition.x / texture2D.width;
            float v = pixelPosition.y / texture2D.height;

            Bounds bounds = meshRenderer.bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            Vector3 worldPosition = new Vector3(
                Mathf.Lerp(min.x, max.x, u),
                Mathf.Lerp(min.y, max.y, v),
                min.z
            );
            return worldPosition;
        }

        public Vector3 ConvertPixelToWorld(Vector3 point, float width, float height, Vector3 minGlobalPosition, Vector3 maxGlobalPosition) {
            float u = point.x / width;
            float v = point.y / height;

            Vector3 worldPosition = new Vector3(
                Mathf.Lerp(minGlobalPosition.x, maxGlobalPosition.x, u),
                Mathf.Lerp(minGlobalPosition.y, maxGlobalPosition.y, v),
                minGlobalPosition.z
            );

            return worldPosition;
        }

        public Vector2 ConvertWorldToPixel(Vector3 worldPosition, Texture2D texture2D, MeshRenderer meshRenderer) {
            Bounds bounds = meshRenderer.bounds;
            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            float u = (worldPosition.x - min.x) / (max.x - min.x);
            float v = (worldPosition.y - min.y) / (max.y - min.y);

            float pixelX = u * texture2D.width;
            float pixelY = v * texture2D.height;

            return new Vector2((int)pixelX, (int)pixelY);
        }
    }
}