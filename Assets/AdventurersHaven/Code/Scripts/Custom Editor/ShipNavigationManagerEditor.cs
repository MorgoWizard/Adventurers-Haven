#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(ShipNavigationManager))]
public class ShipNavigationManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Отрисовка стандартных полей
        DrawDefaultInspector();

        // Получаем ссылку на компонент
        ShipNavigationManager shipNavigationManager = (ShipNavigationManager)target;

        // Отступ перед нашей секцией
        EditorGUILayout.Space(15);
        EditorGUILayout.LabelField("Navigation Stats", EditorStyles.boldLabel);

        // Вычисляем параметры
        float angle = Vector3.Angle(shipNavigationManager.GetSailDirection(), shipNavigationManager.GetWindDirection());
        float dot = Vector3.Dot(shipNavigationManager.GetSailDirection(), shipNavigationManager.GetWindDirection());
        float efficiency = shipNavigationManager.GetSailEfficiency();
        float speed = shipNavigationManager.CalculateShipSpeed();

        // Выводим параметры
        EditorGUILayout.LabelField($"Угол к ветру: {angle:F1}°");
        EditorGUILayout.LabelField($"Dot продукт: {dot:F2}");
        EditorGUILayout.LabelField($"Эффективность: {efficiency * 100:F0}%");
        EditorGUILayout.LabelField($"Скорость: {speed:F1}");

        // Кнопка для обновления в реальном времени
        if (GUILayout.Button("Refresh Data"))
        {
            Repaint();
        }

        // Автообновление в Play Mode
        if (EditorApplication.isPlaying)
        {
            Repaint();
        }
    }
}