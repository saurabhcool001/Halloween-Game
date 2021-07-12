using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    internal static EnemyManager Instance;
    public GameObject zombiePrefab;
    public float zMoveSpeed;
    Animator animator;

    internal float spawnNum;

    internal List<GameObject> zees;

    Player player;

    bool stopAllEnemies;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        zees = new List<GameObject>();
        player = Player.Instance;
    }

    void Update()
    {
        for (int i = 0; i < zees.Count; i++)
        {
            if (zees != null && !player.isLost)
            {
                zees[i].transform.position
                 = Vector3.MoveTowards(zees[i].transform.position,
                 new Vector3(player.transform.position.x, 0, player.transform.position.z), zMoveSpeed * Time.deltaTime);
                zees[i].transform.LookAt(player.transform);
            }
            else if(zees != null && player.isLost && !stopAllEnemies)
            {
                stopAllEnemies = true;
                zees[i].GetComponent<Animator>().Play("idle01");
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Spwan();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Player.Instance.Reset();
        }
         if(Input.GetKeyDown(KeyCode.S))
        {
            Player.Instance.ResetWithoutMove();
        }
    }


    IEnumerator SpawnWave(float no, float delay)
    {
        for (int i = 0; i < no; i++)
        {
            yield return new WaitForSeconds(delay);

            GameObject z = Instantiate(zombiePrefab, transform);
            z.transform.position = new Vector3(Random.Range(-20, 20), 0, Random.Range(-50, 0));
            zees.Add(z);
            yield return new WaitForSeconds(1f);
            z.GetComponent<Animator>().SetBool("isWalk_" + Random.Range(3, 6), true);
        }
    }


    internal void Dead(GameObject z)
    {
        zees.Remove(z);
        Destroy(z, 2.5f);
    }
}
