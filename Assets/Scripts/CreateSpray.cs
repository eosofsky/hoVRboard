using Kvant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpray : MonoBehaviour {

    public GameObject spray;
    public GameObject sprayContainer;

    private GameObject player;
    private List<GameObject> sprayList;
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
        for (int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                for(int k = -2; k < 3; k++)
                {
                    Vector3 spraypos = center + new Vector3(i * sprayLen, j * sprayLen, k * sprayLen);
                    sprayList.Add(Instantiate(spray, spraypos, new Quaternion(), sprayContainer.transform));
                }
            }
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
                Destroy(tmpSpray);
                sprayList.RemoveAt(i);
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
                        sprayList.Add(Instantiate(spray, spraypos, new Quaternion(), sprayContainer.transform));
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
