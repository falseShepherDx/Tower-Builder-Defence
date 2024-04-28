using TMPro;
using UnityEngine;

public class PlayerTipUI : MonoBehaviour
{
    public static PlayerTipUI Instance { get; private set; }
    [SerializeField] private RectTransform backgroundRectTransform;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Vector2 offset;
    private TipMessageTimer tipMessageTimer;
    private void Awake()
    {
        gameObject.SetActive(false);
        Instance = this;
        Hide();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            UpdatePosition();
        }

        if (tipMessageTimer != null)
        {
            tipMessageTimer.timer -= Time.deltaTime;
            if (tipMessageTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    public void Show(string tipText,TipMessageTimer tipMessageTimer=null)
    {
        this.tipMessageTimer = tipMessageTimer;
        gameObject.SetActive(true);
        SetText(tipText);
        UpdatePosition();
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetText(string tipText)
    {
        text.SetText(tipText);
        text.ForceMeshUpdate();
        Vector2 textSize = text.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(8, 8);
        backgroundRectTransform.sizeDelta = textSize + paddingSize;
    }
    //copilot
    private void UpdatePosition()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        transform.localPosition = localPoint + offset;
    }

    public class TipMessageTimer
    {
        public float timer;
    }
}