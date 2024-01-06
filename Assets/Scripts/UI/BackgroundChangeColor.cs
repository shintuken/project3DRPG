using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChangeColor : MonoBehaviour
{
    public List<Color> colors;
    public float transitionDuration = 2f;
    public float delayDuration = 2f;

    private int currentIndex = 0;
    private float transitionTimer = 0f;
    private float delayTimer = 0f;
    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        imageComponent.color = colors[currentIndex];
    }

    private void Update()
    {
        // Kiểm tra xem có đang trong khoảng dừng hay không
        if (delayTimer < delayDuration)
        {
            delayTimer += Time.deltaTime;
        }
        else
        {
            // Nếu không, thực hiện chuyển đổi màu
            transitionTimer += Time.deltaTime;

            // Chuyển đổi màu hiện tại sang màu tiếp theo theo thời gian
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            imageComponent.color = Color.Lerp(colors[currentIndex], colors[(currentIndex + 1) % colors.Count], t);

            // Kiểm tra xem quá trình chuyển đổi màu đã hoàn thành chưa
            if (transitionTimer >= transitionDuration)
            {
                // Hoàn thành quá trình chuyển đổi màu
                transitionTimer = 0f;
                currentIndex = (currentIndex + 1) % colors.Count;
                delayTimer = 0f;
            }
        }
    }
}
