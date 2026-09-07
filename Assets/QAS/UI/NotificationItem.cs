//// NotificationItem.cs
//using System.Collections;
//using UnityEngine;
//using UnityEngine.Playables;
//using UnityEngine.UI;

//public class NotificationItem : MonoBehaviour
//{
//    [SerializeField] private Text titleText;
//    [SerializeField] private Text descriptionText;
//    [SerializeField] private Image iconImage;
//    [SerializeField] private Image backgroundImage;

//    [Header("Colors for types")]
//    [SerializeField] private Color questColor = Color.blue;
//    [SerializeField] private Color achievementColor = Color.gold;

//    public void Initialize(Notification notification)
//    {
//        titleText.text = notification.title;
//        descriptionText.text = notification.description;
//        iconImage.sprite = notification.icon;

//        // Меняем цвет фона в зависимости от типа
//        if (notification.type == NotificationType.Quest)
//            backgroundImage.color = questColor;
//        else if (notification.type == NotificationType.Achievement)
//            backgroundImage.color = achievementColor;

//        // Можно добавить анимацию появления (FadeIn) и запустить корутину на автоматическое закрытие
//    }
//}