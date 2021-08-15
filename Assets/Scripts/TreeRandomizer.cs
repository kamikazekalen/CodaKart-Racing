using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{

    private void OnEnable()
    {
        // Dad assist
        foreach (Transform tree in transform)
        {
            // Change rotation
            Vector3 angles = tree.transform.eulerAngles;
            angles.y = Random.Range(-90f, 90f);
            tree.transform.eulerAngles = angles;

            // Change scale
            Vector3 treeScale = tree.transform.localScale;
            float thickness = treeScale.x * Random.Range(0.9f, 1.1f);
            tree.transform.localScale = new Vector3(thickness,
                                                    Random.Range(1.5f, 4.0f),
                                                    thickness);
        }
    }
}
