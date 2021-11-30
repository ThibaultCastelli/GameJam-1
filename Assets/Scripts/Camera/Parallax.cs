using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds;

    [SerializeField] [Range(1f, 20f)] float speedBackground1 = 5f;
    [SerializeField] [Range(1f, 20f)] float speedBackground2 = 5f;
    [SerializeField] [Range(1f, 20f)] float speedBackground3 = 5f;

    private List<float> posGoals = new List<float>();
    private List<Vector2> startPos = new List<Vector2>();

    private void Start()
    {   
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Get infos of the background
            float width = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x;
            posGoals.Add(backgrounds[i].transform.position.x - width);
            startPos.Add(backgrounds[i].transform.position);

            // Create clone
            GameObject secondBackground = Instantiate(backgrounds[i]);
            secondBackground.transform.SetParent(backgrounds[i].transform);
            secondBackground.transform.position = new Vector3(width + backgrounds[i].transform.position.x, backgrounds[i].transform.position.y, backgrounds[i].transform.position.z);
        }
    }

    private void LateUpdate()
    {
        // Move the backgrounds
        backgrounds[0].transform.position -= new Vector3(speedBackground1 * Time.deltaTime, backgrounds[0].transform.position.y);
        backgrounds[1].transform.position -= new Vector3(speedBackground2 * Time.deltaTime, backgrounds[1].transform.position.y);
        backgrounds[2].transform.position -= new Vector3(speedBackground3 * Time.deltaTime, backgrounds[2].transform.position.y);

        // Check if the backgrounds are off limits
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Reset position of the background
            if (backgrounds[i].transform.position.x <= posGoals[0])
                backgrounds[i].transform.position = startPos[i];
        }
    }
}
