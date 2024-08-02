using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


#if UNITY_EDITOR
using UnityEditor.UIElements;
using UnityEngine.UIElements;
#endif

namespace Assets.Scripts.Extension {
    public static class DictionaryExtensions {
        public static void TryAddOrPlus<T1, T2>(this Dictionary<T1, T2> keyValuePairs, T1 key, T2 value) {
            if (!keyValuePairs.TryAdd(key, value)) {
                keyValuePairs.Add(key, value);
            }
        }
    }

    public static class ListExtensions {
        public static T GetRandomItem<T>(this IList<T> list) {
            return list[Random.Range(0, list.Count)];
        }

        public static bool ContainsAnyValue<T>(this IList<T> list) {
            return list.Count > 0;
        }

        public static bool IsEmpty<T>(this IList<T> list) {
            return list.Count == 0;
        }

        public static void Shuffle<T>(this IList<T> list) {
            for (var i = list.Count - 1; i > 1; i--) {
                var j = Random.Range(0, i + 1);
                var value = list[j];
                list[j] = list[i];
                list[i] = value;
            }
        }

        public static void Shuffle<T>(this T[] array) {
            int n = array.Length;

            for (int i = n - 1; i > 0; i--) {
                int j = Random.Range(0, i + 1);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        public static SearchType FindFirstOfType<InterfaceType, SearchType>(this List<InterfaceType> values) where SearchType : InterfaceType {
            SearchType result = default;

            for (int index = 0; index < values.Count; index++) {
                if (values[index] is SearchType resultItem) {
                    result = resultItem;
                    break;
                }
            }

            return result;
            //Дублирует функционал:
            //values.OfType<ScoreModel>().FirstOrDefault();
        }

        public static bool HasDuplicate<T>(this List<T> list) {
            HashSet<T> elementsSet = new HashSet<T>(list);
            return elementsSet.Count != list.Count;
        }
    }

    public static class TransformExtensions {
        public static void DestroyChildren(this Transform transform) {
            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
        }

        public static void ResetTransformation(this Transform transform) {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public static float GetCurrentValueBuAxis(this Transform transform, RotateAxis rotateAxis) {
            float result = 0f;

            switch (rotateAxis) {
                case RotateAxis.X:
                    result = transform.rotation.eulerAngles.x;
                    break;
                case RotateAxis.Y:
                    result = transform.rotation.eulerAngles.y;
                    break;
                case RotateAxis.Z:
                    result = transform.rotation.eulerAngles.z;
                    break;
            }

            return result;
        }

        public static void SetSmallSize(this Transform transform) {
            transform.localScale = Vector3.one * 0.01f;
        }
    }

    public static class Vector3Extensions {
        public static Vector3 WithX(this Vector3 value, float x) {
            value.x = x;
            return value;
        }

        public static Vector3 WithY(this Vector3 value, float y) {
            value.y = y;
            return value;
        }

        public static Vector3 WithZ(this Vector3 value, float z) {
            value.z = z;
            return value;
        }

        public static Vector3 AddX(this Vector3 value, float x) {
            value.x += x;
            return value;
        }

        public static Vector3 AddY(this Vector3 value, float y) {
            value.y += y;
            return value;
        }

        public static Vector3 AddZ(this Vector3 value, float z) {
            value.z += z;
            return value;
        }

        public static Vector2[] ToVector2(this Vector3[] vector3s) {
            Vector2[] vectors = new Vector2[vector3s.Length];

            for (int i = 0; i < vectors.Length; i++) {
                vectors[i] = vector3s[i];
            }
            return vectors;
        }

        public static Vector3[] ToVector3(this Vector2[] vector2s) {
            Vector3[] vectors = new Vector3[vector2s.Length];

            for (int i = 0; i < vectors.Length; i++) {
                vectors[i] = vector2s[i];
            }
            return vectors;
        }
    }

    public static class ActionExtension {
        public static int GetSubscriberCount<T>(this Action<T> action) {
            return action?.GetInvocationList().Length ?? 0;
        }
    }

    public static class IntExtension {
        public static bool IsEven(this int value) {
            return value % 2 == 0;
        }

        public static bool IsLastIn<T>(this int value, T[] array) {
            return array.Length - 1 == value;
        }

        public static bool IsLastIn<T>(this int value, List<T> list) {
            return list.Count - 1 == value;
        }

        public static bool IsFirst(this int value) {
            return value == 0;
        }

        public static int GetSum(this int first, int second) {
            return first + second;
        }

        public static int GetDivide(this int first, int second) {
            if (first == 0) {
                return int.MinValue;
            }
            if (first % second == 0) {
                return first / second;
            }
            else {
                return int.MinValue;
            }
        }

        public static int GetSubstract(this int first, int second) {
            return first - second;
        }

        public static int GetMultiply(this int first, int second) {
            return first * second;
        }

        public static float ConvertFrameToTime(this int frameCount, int framePerSecond) {
            return (float)frameCount / framePerSecond;
        }
    }

    public static class StringExtension {
        public static string Reverse(this string text) {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    public static class BoolExtension {
        public static bool IsClear(this bool[] bools) {
            bool result = true;

            for (int i = 0; i < bools.Length; i++) {
                if (bools[i]) {
                    return false;
                }
            }

            return result;
        }
    }

    public static class SerializObjectExtension {
#if UNITY_EDITOR
        public static void BindProperty(this SerializedObject serializedObject, VisualElement root, string propertyName, string uiFieldName) {
            SerializedProperty property = serializedObject.FindProperty(propertyName);
            PropertyField simpleMinField = root.Q<PropertyField>(uiFieldName);
            simpleMinField.BindProperty(property);
        }
#endif
    }
}

public enum RotateAxis {
    X,
    Y,
    Z,
}