using UnityEngine;

public class WebGLRotateDevicePopup : MonoBehaviour
{       
    [SerializeField] private GameObject warningPopup;
    [SerializeField] private RectTransform rectTransform;
    
    private void Update()
    {
        if (ExternalCallClass.Instance.SelectOrientation == ExternalCallClass.Orientation.Landscape)
        {
            Debug.Log("Landscape orientation selected.");
            //Perform landscape-specific operations here
            if (rectTransform.rect.height > rectTransform.rect.width)
            {
                if (!warningPopup.activeSelf)
                    warningPopup.SetActive(true);
            }
            else
            {
                if (warningPopup.activeSelf)
                    warningPopup.SetActive(false);
            }
        }
        else if (ExternalCallClass.Instance.SelectOrientation == ExternalCallClass.Orientation.Portrait)
        {
            Debug.Log("Portrait orientation selected.");
            if (rectTransform.rect.height < rectTransform.rect.width)
            {
                if (!warningPopup.activeSelf)
                    warningPopup.SetActive(true);
            }
            else
            {
                if (warningPopup.activeSelf)
                    warningPopup.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Invalid orientation selected.");
        }

        //if (ExternalCallClass.Instance.SelectOrientation == ExternalCallClass.Orientation.Landscape)
        //{
        //    Debug.Log("Landscape orientation selected.");
        //    warningPopup.SetActive(rectTransform.rect.height > rectTransform.rect.width);
        //}
        //else if (ExternalCallClass.Instance.SelectOrientation == ExternalCallClass.Orientation.Portrait)
        //{
        //    Debug.Log("Portrait orientation selected.");
        //    warningPopup.SetActive(rectTransform.rect.height < rectTransform.rect.width);
        //}
        //else
        //{
        //    Debug.Log("Invalid orientation selected.");
        //}
    }

    public void OpenPanelForAndroid()
    {
        rectTransform.transform.localScale = new Vector3(1.05f, 1, 1);
        this.Open();
    }

    public void OpenPanelForIOS()
    {
        rectTransform.transform.localScale = Vector3.one;
        this.Open();
    }
}
