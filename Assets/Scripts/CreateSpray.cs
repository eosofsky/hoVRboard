using Kvant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpray : MonoBehaviour {

    public GameObject spray;
    public GameObject sprayContainer;

    private GameObject player;
    private List<GameObject> sprayList;
    private List<GameObject> sprayStorage;
    private float sprayLen;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        sprayLen = spray.GetComponent<Spray>().emitterSize.x;
        initializeSprays();
	}
	
	// Update is called once per frame
	void Update () {
        updateSprays();
	}

    void initializeSprays()
    {
        sprayList = new List<GameObject>();
        print("spraylist created");
        Vector3 center = new Vector3(FloorToTen(player.transform.position.x), FloorToTen(player.transform.position.y), FloorToTen(player.transform.position.z));
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for(int k = -1; k < 2; k++)
                {
                    Vector3 spraypos = center + new Vector3(i * sprayLen, j * sprayLen, k * sprayLen);
                    sprayList.Add(Instantiate(spray, spraypos, new Quaternion(), sprayContainer.transform));
                }
            }
        }

        for (int l = 0; l < 10; l++)
        {
            GameObject extraSpray = Instantiate(spray, sprayContainer.transform);
            extraSpray.SetActive(false);
            sprayStorage.Add(extraSpray);
        }
    }

    void updateSprays()
    {
        Vector3 center = new Vector3(FloorToTen(player.transform.position.x), FloorToTen(player.transform.position.y), FloorToTen(player.transform.position.z));

        //remove sprays outside of radius around player
        for (int i = 0; i < sprayList.Count; i++)
        { 
            GameObject tmpSpray = sprayList[i];
            Vector3 diff = center - tmpSpray.transform.position;
            if(Mathf.Abs(diff.x) > 10 || Mathf.Abs(diff.y) > 10 || Mathf.Abs(diff.z) > 10)
            {
                sprayList.RemoveAt(i);
                sprayStorage.Add(tmpSpray);
                tmpSpray.SetActive(false);
            }
        }

        //create sprays within 9x9 block around player
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                for (int k = -1; k < 2; k++)
                {
                    Vector3 spraypos = center + new Vector3(i * sprayLen, j * sprayLen, k * sprayLen);
                    if (!FindSpray(spraypos))
                    {
                        GameObject tmpSpray = sprayStorage[0];
                        tmpSpray.transform.position = spraypos;
                        sprayList.Add(tmpSpray);
                        sprayStorage.RemoveAt(0);
                    }
                }
            }
        }
    }

    bool FindSpray(Vector3 pos) {
        for (int i = 0; i < sprayList.Count; i++)
        {
            GameObject tmpSpray = sprayList[i];
            if (pos == tmpSpray.transform.position)
            {
                return true;
            }
        }
        return false;
    }

    int FloorToTen(float val)
    {
        return Mathf.FloorToInt(val / 10) * 10;
    }
}
