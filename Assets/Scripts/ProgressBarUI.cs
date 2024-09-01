using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject; //Made because Unity does not display in the editor variables of interface type
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress; // this is the workaround

    private void Start()
    {
        hasProgress=hasProgressGameObject.GetComponent<IHasProgress>();//Here we transfer it to the IHasProgress type
        if (hasProgress == null)//Safety measure so that we make sure that the reference is to an object that actually implements IHasProgress
        {
            Debug.LogError("Game Object " + hasProgressGameObject + " does not have a component that implements IHasProgress");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage .fillAmount = 0f;
        Hide();
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        } else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
