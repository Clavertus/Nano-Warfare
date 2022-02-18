using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public string seed;

    public float spawnDelay;

    public GameObject[] enemy;
    // 0 - stinger
    // 1 - nanotrooper

    public int currentPriorityNumber;
    public bool swiftStart;


    void Start()
    {
        if (swiftStart)
        {
            StartCoroutine(AnalyzeSeed(seed));
            
        }

      
    }

    public IEnumerator AnalyzeSeed(string s)
    {

        for(int i = 0; i < s.Length; i++)
        {
            switch (s[i])
            {
                case 's':
                        Spawn(enemy[0]);
                    break;

                case 'n':
                        Spawn(enemy[1]);
                    break;

                case '|':
                    StartCoroutine(Repeat(i, s));
                    break;


                #region numbers
                case '1':
                    yield return new WaitForSeconds(1f);
                    break;

                case '2':
                    yield return new WaitForSeconds(2f);
                    break;

                case '3':
                    yield return new WaitForSeconds(3f);
                    break;

                case '4':
                    yield return new WaitForSeconds(4f);
                    break;

                case '5':
                    yield return new WaitForSeconds(5f);
                    break;

                case '6':
                    yield return new WaitForSeconds(6f);
                    break;

                case '7':
                    yield return new WaitForSeconds(7f);
                    break;

                case '8':
                    yield return new WaitForSeconds(8f);
                    break;

                case '9':
                    yield return new WaitForSeconds(9f);
                    break;
                    #endregion


            }
            yield return new WaitForSeconds(spawnDelay);
        }


    }

    public IEnumerator Repeat(int startIndex, string s)
    {
        for (int j = startIndex + 1; j < s.Length; j++)
        {
            
            switch (s[j])
            {
                case 's':
                    Spawn(enemy[0]);
                 //   Debug.Log("spawning a stinger from index " + j);
                    break;

                case 'n':
                    Spawn(enemy[1]);
                   // Debug.Log("spawning a nano from index " + j);
                    break;

                #region numbers
                case '1':
                    yield return new WaitForSeconds(1f);
                    break;

                case '2':
                    yield return new WaitForSeconds(2f);
                    break;

                case '3':
                    yield return new WaitForSeconds(3f);
                    break;

                case '4':
                    yield return new WaitForSeconds(4f);
                    break;

                case '5':
                    yield return new WaitForSeconds(5f);
                    break;

                case '6':
                    yield return new WaitForSeconds(6f);
                    break;

                case '7':
                    yield return new WaitForSeconds(7f);
                    break;

                case '8':
                    yield return new WaitForSeconds(8f);
                    break;

                case '9':
                    yield return new WaitForSeconds(9f);
                    break;
                    #endregion
            }
            
          //  Debug.Log(s.Length);
        }

        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(Repeat(startIndex, s));

    }


    public void Spawn(GameObject entity)
    {
        Instantiate(entity, transform.position, transform.rotation).GetComponent<Priority>().priority = currentPriorityNumber;
        currentPriorityNumber++;

    }

   
}
