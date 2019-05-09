using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollList : MonoBehaviour
{
    public float Sensibility = 1;
    float Value;
    public RectTransform Content;
    float DefauldPosY;
    float ContentHeight;
    float MyHeight;
    float AxisY;
    public Scrollbar scrollbar;
    float ValueExpend;

    void Start()
    {
        DefauldPosY = Content.anchoredPosition.y;
        ContentHeight = Content.rect.height;
        RectTransform MyRectTransform = GetComponent<RectTransform>();
        MyHeight = MyRectTransform.rect.height;
        ValueExpend = ContentHeight - MyHeight;
    }

    public void ScrollTrigger ()
    {
        float InputScroll = Input.GetAxis("Mouse ScrollWheel");
        Value += InputScroll * 10 * -Sensibility;      
        Value = Mathf.Clamp(Value, 0, ValueExpend);
        AxisY = Value / (ValueExpend);
        scrollbar.value = AxisY;
        Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, DefauldPosY + Value);
    }

    public void changedValue()
    {
        AxisY = scrollbar.value;
        float _value = AxisY * ValueExpend;
        Content.anchoredPosition = new Vector2(Content.anchoredPosition.x, DefauldPosY + _value);
    }
}
