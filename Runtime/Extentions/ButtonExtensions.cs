using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

namespace CommonUtility.Extensions
{
    public static class ButtonExtensions
    {
        public static void SetListener(this Button[] buttons, UnityAction call)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].SetListener(call);
        }

        public static void SetListener(this IList<Button> buttons, UnityAction call)
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].SetListener(call);
        }

        public static void SetListener(this Button button, UnityAction call)
        {
            button.onClick.SetListener(call);
        }

        public static void SetListener(this ButtonClickedEvent clickedEvent, UnityAction call)
        {
            clickedEvent.RemoveAllListeners();
            clickedEvent.AddListener(call);
        }
    }
}