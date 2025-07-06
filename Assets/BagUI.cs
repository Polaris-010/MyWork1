using UnityEngine;

public class BagUI : MonoBehaviour
{
    public GameObject bagPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("案o!!!");
            bagPanel.SetActive(!bagPanel.activeSelf);
        }
    }
}