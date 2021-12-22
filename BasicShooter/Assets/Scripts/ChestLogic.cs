using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLogic : MonoBehaviour
{

    public GameObject chestOpen;
    public List<GameObject> gun;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(chestOpen, transform.position, transform.rotation);
            Vector3 gunPos = new Vector3(transform.position.x + 1, transform.position.y - 1, transform.position.z);
            Instantiate(gun[Random.Range(0, 2)], gunPos, transform.rotation);
           
            Destroy(gameObject);
        }
        
    }

}
