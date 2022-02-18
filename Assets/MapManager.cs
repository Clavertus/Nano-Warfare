using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    Transform cam;
    bool lerping;

    float c_lerpTime;
    float lerpTime;

    bool panelLerp;
    public Image fadePanel;
    float c_lerpTime2;
    float lerpTime2;

    void Start()
    {
        cam = Camera.main.transform;
        lerpTime = 1f;
        c_lerpTime = 0;
        lerpTime2 = 0.75f;
        c_lerpTime2 = 0;
        selectedLevel = 1;
    }


    Vector3 currPos;
    Vector3 endPos;
    Vector3 finalPos;

    public Text stageName;
    public Text description;
    public Text territory;

    public int selectedLevel;
     
    public Territory territory_script;


    void Update()
    {

        if (lerping)
        {
            if(c_lerpTime < lerpTime)
            {
                c_lerpTime += Time.deltaTime;
                float perc = c_lerpTime / lerpTime;
                perc = 1 - Mathf.Pow(1 - perc, 5);
                finalPos = Vector3.Lerp(currPos, endPos, perc);

                cam.position = finalPos;
                

            } else
            {
                lerping = false;
            }


        }

        if (panelLerp)
        {
            if (c_lerpTime2 < lerpTime2)
            {
                c_lerpTime2 += Time.deltaTime;

                float perc = c_lerpTime2 / lerpTime2;

                fadePanel.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), perc);
                


            }
            else
            {
                panelLerp = false;
                SceneManager.LoadScene(selectedLevel + 2);
            }


        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Ended && Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y)) != null)
            {
                Traverse(Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y)).transform);
                GameObject currentLevel = Physics2D.OverlapPoint(new Vector2(touchPos.x, touchPos.y)).gameObject;
                selectedLevel = int.Parse(currentLevel.name);
                stage_UI(selectedLevel);

            }

        }
    }


    void stage_UI(int id)
    {
        switch (id)
        {
            case 1:
                stageName.text = "1-1" + Environment.NewLine + "Escape Tunnels";
                description.text = "Make your way out of the prison!" + Environment.NewLine + "Defeat the inbound enemy forces and push through the tunnels.";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 2:
                stageName.text = "1-2" + Environment.NewLine + "Escape Tunnels";
                description.text = "Keep on charging through the escape tunnels!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 3:
                stageName.text = "1-3" + Environment.NewLine + "Escape Tunnels";
                description.text = "You're almost there! Blast through the last section of the tunnels!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 4:
                stageName.text = "1-4" + Environment.NewLine + "Cerulean River";
                description.text = "Cross the Cerulean Bridge to find freedom. You may have some company!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 5:
                stageName.text = "2-1" + Environment.NewLine + "Feathery Meadows";
                description.text = "Traverse the Feathery Meadows. Stay aware of Siva's patrols!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 6:
                stageName.text = "2-2" + Environment.NewLine + "Feathery Meadows";
                description.text = "Aerial support inbound!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 7:
                stageName.text = "2-3" + Environment.NewLine + "Feathery Meadows";
                description.text = "Cleanse the Meadows of Siva's influence!";
                // territory.text = territory_script.tString[id];
                territory.text = "This area is Siva's territory.";
                break;

            case 8:
                stageName.text = "2-4" + Environment.NewLine + "Swift River";
                description.text = "Be careful! The currents here are deadly!";
                // territory.text = territory_script.tString[id];
                territory.text = "Territory unknown!";
                break;



        }
    }

    void Traverse(Transform t)
    {
        c_lerpTime = 0;
        lerping = true;

        endPos = new Vector3(t.position.x, t.position.y, -10);
        currPos = cam.position;



    }

    public void Load()
    {
        StartCoroutine(Load_C());
    }

    public IEnumerator Load_C()
    {
        panelLerp = true;
        yield return new WaitForSeconds(0.75f);
    }
}
