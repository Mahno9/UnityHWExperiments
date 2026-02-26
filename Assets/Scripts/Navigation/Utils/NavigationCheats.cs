using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

namespace Navigation.Utils
{
    public class NavigationCheats : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Mine.Mine[] mines = FindObjectsByType<Mine.Mine>(FindObjectsSortMode.None);
                foreach (Mine.Mine mine in mines)
                {
                    CallPrivateMethod(mine, "Explode");
                }
            }
        }

        private static void CallPrivateMethod(object targetInstance, string methodName)
        {
            // 1. Получаем тип объекта
            var type = targetInstance.GetType();

            // 2. Ищем метод.
            // BindingFlags.NonPublic | BindingFlags.Instance — это "ключи",
            // которые говорят рефлексии искать именно скрытые методы экземпляра.
            MethodInfo method = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                // 3. Вызываем метод (null, если аргументов нет)
                method.Invoke(targetInstance, null);
                Debug.Log($"[Cheat] Метод {methodName} успешно вызван!");
            }
            else
            {
                Debug.LogError($"[Cheat] Метод {methodName} не найден в {type.Name}");
            }
        }
    }
}