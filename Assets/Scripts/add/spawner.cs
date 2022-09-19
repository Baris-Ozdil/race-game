using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{

    public GameObject spawnObje;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {

        yield return new WaitForSeconds(5);

        spawnAndDestroy();

    }

    private void spawnAndDestroy()
    {
        Instantiate(spawnObje, gameObject.transform.position, gameObject.transform.rotation);

        Destroy(gameObject);

    }

}
