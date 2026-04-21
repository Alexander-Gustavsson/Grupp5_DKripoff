using UnityEngine;

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


        throw new System.NotImplementedException();
    }
}
