using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] obstacles;
    public int minNumberOfObstacles;
    public int maxNumberOfObstacles;
    public List<GameObject> randomObstacles;

    

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) 
        {
            transform.position = new Vector3(0,0,transform.position.z+135*2);
            placeObstacles();
        }
    }
    
    void Start()
    {
        placeObstacles();
    }

    void placeObstacles()
    {
        
        for(int i=0;i<randomObstacles.Count;i++)
        {
            Destroy(randomObstacles[i]);
        }
        randomObstacles.Clear();

        
        int randomNumberOfObstacles = UnityEngine.Random.Range(minNumberOfObstacles, maxNumberOfObstacles);
        for (int i = 0; i < randomNumberOfObstacles; i++)
        {
            
            int randomObstacleNumber = UnityEngine.Random.Range(0, obstacles.Length);
            GameObject obstacle = Instantiate(obstacles[randomObstacleNumber], transform);

            obstacle.SetActive(false);
            randomObstacles.Add(obstacle);
        }
        
        for (int i=0;i<randomObstacles.Count;i++)
        {
            float minPosZ = (135f / randomObstacles.Count) + (135f / randomObstacles.Count) * i;
            float maxPosZ = minPosZ + (135f / randomObstacles.Count) - 1;

            float pos = UnityEngine.Random.Range(minPosZ, maxPosZ);

            randomObstacles[i].transform.localPosition = new Vector3(0, 0, pos);
            randomObstacles[i].SetActive(true);

            
            if(randomObstacles[i].GetComponent<ChangeLane>() != null)
            {
                randomObstacles[i].GetComponent<ChangeLane>().ChooseLane();
            }
        }
    }
}
