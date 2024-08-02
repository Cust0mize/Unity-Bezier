using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Extension {
    public static class ButtonExtension {
        public static void RemoveAllAndSubscribeButton(this Button button, UnityAction unityAction) {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(unityAction);
        }
    }
}
