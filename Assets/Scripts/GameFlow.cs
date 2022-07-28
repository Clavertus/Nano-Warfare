using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour
{
    public int ee; //ethereal energy

    [Header("Priorities")]
    public int currentPriorityNumber = 0;
    public int currentPriorityNumber_A = 0;

    [Header("Health")]

    public float crystalMaxHealth;
    public float crystalHealth;

    [Header("Particle references")]

    public ParticleSystem crystalParticles;
    public ParticleSystem spawnps;
    public ParticleSystem aerialSpawnPS;

    [Header("Unit references")]

    public GameObject Byte;
    public GameObject Nanotrooper;
    public GameObject Commander;
    public GameObject Stinger;
    public GameObject Bulky;
    public GameObject A_SDrone;

    public Image textbox;


    [Header("Queue GUI")]

    public Image queueBar;
    public bool queuePos1;
    public bool queuePos2;
    public bool queuePos3;
    public bool queuePos4;
    public bool queuePos5;
    public bool queueReady;

    public Image queueBlob1;
    public Image queueConn2;
    public Image queueBlob2;
    public Image queueConn3;
    public Image queueBlob3;
    public Image queueConn4;
    public Image queueBlob4;

    [Header("Timers")]

    public float currCooldown;
    public float maxCooldown;

    [Header("Animations")]

    public Animator EEanim;
    public Animator textanim;


    public Text eeCountText;


    public Text sampleText;

    public Text loadedTextTop;
    public Text loadedTextBot;

    [Header("Buttons")]
    public Button b_Stinger;
    public Button b_Nanotrooper;
    public Button b_Byte;
    public Button b_Commander;
    public Button b_Bulky;
    public Button b_SDrone;

    [Header("Unit Costs")]
    public int stingerCost;
    public int nanotrooperCost;
    public int byteCost;
    public int commanderCost;
    public int bulkyCost;
    public int sdroneCost;

    public float unitMovementSpeed;

    public Text fpsText;
    public float deltaTime;

    public Transform aerialSpawn;





    public List<GameObject> queue = new List<GameObject>();



    void Start()
    {
       
        crystalHealth = crystalMaxHealth;
        StartCoroutine("GenerateEE");

        stingerCost = 10;
        nanotrooperCost = 20;
        byteCost = 10;
        commanderCost = 35;
        bulkyCost = 30;


}

    IEnumerator GenerateEE()
    {
        yield return new WaitForSeconds(3f);
        ee++;
        EEanim.SetTrigger("EEblue");
        StartCoroutine("GenerateEE");
    }

    public void addEE(int tba)  //to be added
    {
        ee += tba;
        EEanim.SetTrigger("EEblue");
    }


    void Update()
    {

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = Mathf.Ceil(fps).ToString();

        unitMovementSpeed = 25f * Time.fixedDeltaTime;

        
        


            if (ee < stingerCost)
            {
                b_Stinger.interactable = false;
            }
            else
            {
                b_Stinger.interactable = true;
            }

            if (ee < nanotrooperCost)
            {
                b_Nanotrooper.interactable = false;
            }
            else
            {
                b_Nanotrooper.interactable = true;

            }

            if (ee < byteCost)
            {
                b_Byte.interactable = false;
            }
            else
            {
                b_Byte.interactable = true;
            }

            if (ee < commanderCost)
            {
                b_Commander.interactable = false;
            }
            else
            {
                b_Commander.interactable = true;
            }

            if (ee < bulkyCost)
            {
                b_Bulky.interactable = false;
            }
            else
            {
                b_Bulky.interactable = true;
            }


            queueBar.GetComponent<RectTransform>().sizeDelta = new Vector2(currCooldown / maxCooldown * 100, 10);


            #region queueMechanism

            if (queue.Count > 0 && queueReady)
            {
                queueReady = false;
                currCooldown = maxCooldown;
            }

            currCooldown -= Time.deltaTime;
            if (currCooldown <= 0 && !queueReady)
            {
                queueReady = true;
                if (queue[0].name != "Byte")
                {
                    if (queue[0].name.Substring(0, 2) == "A_") //if unit is aerial
                    {
                    Instantiate(queue[0], aerialSpawn.position, transform.rotation).GetComponent<AerialPriority>().aerialPriority = currentPriorityNumber_A;
                    currentPriorityNumber_A++;
                    aerialSpawnPS.Play();
                    }
                     else
                     {

                     Instantiate(queue[0], transform.position, transform.rotation).GetComponent<Priority>().priority = currentPriorityNumber;
                     currentPriorityNumber++;
                     spawnps.Play();
                }


                } else
                {
                    Instantiate(queue[0], transform.position, transform.rotation);
                    spawnps.Play();
                }

                message(queue[0].name + " initialized!", 'a');
                queue.RemoveAt(0);
                
                queuePos1 = false;
            }

            #endregion


            var emission = crystalParticles.emission;
            emission.rateOverTime = ee / 10;
            eeCountText.text = ee.ToString();

            #region queueBlobs
            if (queuePos2)
            {
                queueBlob1.color = new Color(0.05f, 0.42f, 1);
            } else
            {
                queueBlob1.color = new Color(0.18f, 0.18f, 0.18f);
            }


            if (queuePos3)
            {
                queueBlob2.color = new Color(0.05f, 0.42f, 1);
                queueConn2.color = new Color(0.05f, 0.42f, 1);
            }
            else
            {
                queueBlob2.color = new Color(0.18f, 0.18f, 0.18f);
                queueConn2.color = new Color(0.18f, 0.18f, 0.18f);
            }

            if (queuePos4)
            {
                queueBlob3.color = new Color(0.05f, 0.42f, 1);
                queueConn3.color = new Color(0.05f, 0.42f, 1);
            }
            else
            {
                queueBlob3.color = new Color(0.18f, 0.18f, 0.18f);
                queueConn3.color = new Color(0.18f, 0.18f, 0.18f);
            }

            if (queuePos5)
            {
                queueBlob4.color = new Color(0.05f, 0.42f, 1);
                queueConn4.color = new Color(0.05f, 0.42f, 1);
            }
            else
            {
                queueBlob4.color = new Color(0.18f, 0.18f, 0.18f);
                queueConn4.color = new Color(0.18f, 0.18f, 0.18f);
            }
            #endregion


            #region queueMovingMechanism



            if (queuePos5 && !queuePos4)
            {
                queuePos5 = !queuePos5;
                queuePos4 = !queuePos4;
            }

            if (queuePos4 && !queuePos3)
            {
                queuePos4 = !queuePos4;
                queuePos3 = !queuePos3;
            }

            if (queuePos3 && !queuePos2)
            {
                queuePos3 = !queuePos3;
                queuePos2 = !queuePos2;
            }

            if (queuePos2 && !queuePos1)
            {
                queuePos2 = !queuePos2;
                queuePos1 = !queuePos1;
            }











            #endregion
        

        


    }

    public void SpawnStinger()
    {
        SpawnEntity(Stinger, 10);
    }

    public void SpawnNanotrooper()
    {
        SpawnEntity(Nanotrooper, 20);
    }

    public void SpawnByte()
    {
        SpawnEntity(Byte, 10);
    }

    public void SpawnCommander()
    {
        SpawnEntity(Commander, 35);
    }

    public void SpawnSDrone()
    {
        SpawnEntity(A_SDrone, 10);
    }

    public void SpawnEntity(GameObject entity, int cost)
    {
        if (queuePos1)
        {
            if (queuePos2)
            {
                if (queuePos3)
                {
                    if (queuePos4)
                    {
                        if (queuePos5)
                        {
                            message("Queue full!", 'e');
                        }
                        else
                        {
                            queuePos5 = true;
                            ee -= cost;
                            EEanim.SetTrigger("EEred");
                            queue.Add(entity);

                        }

                    }
                    else
                    {
                        queuePos4 = true;
                        ee -= cost;
                        EEanim.SetTrigger("EEred");
                        queue.Add(entity);
                    }
                }
                else
                {
                    queuePos3 = true;
                    ee -= cost;
                    EEanim.SetTrigger("EEred");
                    queue.Add(entity);
                }
            }
            else
            {
                queuePos2 = true;
                ee -= cost;
                EEanim.SetTrigger("EEred");
                queue.Add(entity);
            }
        }
        else
        {
            queuePos1 = true;
            ee -= cost;
            EEanim.SetTrigger("EEred");
            queue.Add(entity);
        }


    }


    public void message(string x, char type) // 32 characters is max
    {
        Text newText = Instantiate(sampleText, textbox.transform.position + new Vector3(2.5f, 0, 0), transform.rotation);
        newText.transform.SetParent(textbox.transform);
        newText.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(OpacityCorrection(newText));
        newText.GetComponent<Animator>().SetTrigger("FadeToTop");
        newText.text = x;
        switch (type)
        {
            case 'a': //ally
                newText.color = new Color(0, 0.79f, 1f);
                break;

            case 'e': //enemy
                newText.color = new Color(0.8f, 0.11f, 0.11f);
                break;

            case 'n': //neutral
                newText.color = new Color(1, 1f, 1f);
                break;

            case 's': //special
                newText.color = new Color(0.15f, 0.8f, 0.10f);
                break;
        }

        if(loadedTextTop != null)
        {
            loadedTextTop.GetComponent<Animator>().SetTrigger("TopToBot");
        }

        if (loadedTextBot != null)
        {
            loadedTextBot.GetComponent<Animator>().SetTrigger("BotToFade");
            Destroy(loadedTextBot.gameObject, 1f);
        }

        loadedTextBot = loadedTextTop;

        loadedTextTop = newText;

    }

    IEnumerator OpacityCorrection(Text text)
    {
        yield return new WaitForSeconds(0.25f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
    }

}
