using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosetTarget : MonoBehaviour
{
     public List<GameObject> enemies = new List<GameObject>();
    public GameObject closetEnemyInDirection;
    public Transform body;
   
    public void FindClosetGameObect()
    {
        GameObject closest = null;
        float m_distance = 41f;
        Vector3 position =body.transform.position;
        foreach(GameObject go in enemies)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if(curDistance <m_distance)
            {
                closest = go;
                m_distance =curDistance;
            }
        }
        closetEnemyInDirection = closest;
    }

    // Update is called once per frame
   

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Zombie")
        {
            for(int i=0; i< enemies.Count; i++)
            {
                if(other.gameObject == enemies[i])
                {
                    return;
                }
            }
            enemies.Add(other.gameObject);
            FindClosetGameObect();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Zombie")
        {
            for(int i=0; i<enemies.Count; i++)
            {
                if(other.gameObject == enemies[i])
                {
                    if(enemies[i]==closetEnemyInDirection)
                    {
                        closetEnemyInDirection =null;
                    }
                    enemies.RemoveAt(i);
                }
            }
        }
    }
}
