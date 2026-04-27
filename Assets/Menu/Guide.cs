using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [SerializeField] private GuideController.GuideName name;

    private void Start()
    {
        GuideController.OnGuideActive += ActivateGuide;
    }

    private void ActivateGuide(GuideController.GuideName obj)
    {
        if (name == obj)
        {
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        } else if(GetComponent<Image>().enabled == true)
        {
            Disable();
        }
    }

    public void Disable()
    {
        GuideController.activeGuides.Remove(name);

        GetComponent<Image>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
