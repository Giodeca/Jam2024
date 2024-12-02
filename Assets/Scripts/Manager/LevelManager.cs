using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{

    [SerializeField]
    private LevelDataEnemies[] levelData;


    [SerializeField]
    private GameObject[] spawnArray;

    [SerializeField]
    private CinemachineVirtualCamera cameraCustom;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject boss;
    private int index = 0;
    private int totalEnemiesCount;

    private float damping;
    private float defaultDamping = 10;

    [SerializeField] bool spawnInBossRoom;

    private void OnEnable()
    {
        EventManager.LessEnemie += EnemiesDieEvent;
        EventManager.FinishMovePlayer  += PlayerReady;
    }

    private void OnDisable()
    {
        EventManager.LessEnemie -= EnemiesDieEvent;
        EventManager.FinishMovePlayer -= PlayerReady;
    }

    private void Awake()
    {
        damping = cameraCustom.GetCinemachineComponent<Cinemachine3rdPersonFollow>().Damping.y = 0f;
    }
    void Start()
    {
        totalEnemiesCount = levelData[index].countEnemies;

        if(spawnInBossRoom) player.transform.position = spawnArray[spawnArray.Length - 1].transform.position;
    }

    private void EnemiesDieEvent()
    {
        totalEnemiesCount--;
        if(totalEnemiesCount == 0)
        {
            if(index < levelData.Length-1)
            {
                print("change scene");
                EventManager.ChangeLevel?.Invoke();
                MovePlayer();
            }
            else
            {
                print("WINNNN");
            }

        }
    }

    private void MovePlayer()
    {
        GameUI.Get().OnLevelStart();
        index++;
        damping = 0f;
        player.transform.position = spawnArray[index].transform.position;
        totalEnemiesCount = levelData[index].countEnemies;
        if (index == levelData.Length - 1) {
            if (boss != null)
            {
                StartCoroutine(PlayBossSoundtracks());
                GameUI.Get().ShowBossHealth(boss.GetComponent<Enemy>());
            }
        }
    }

    IEnumerator PlayBossSoundtracks()
    {
        AudioManager.Instance.StopSoundtrack();
        AudioManager.Instance.PlaySoundtrack("BOSSENTRANCE");
        yield return new WaitForSeconds(AudioManager.Instance.soundtrackSource.clip.length);
        AudioManager.Instance.StopSoundtrack();
        AudioManager.Instance.PlaySoundtrack("BOSSTHEME");
    }

    private void PlayerReady()
    {
        damping = defaultDamping;
    }
}
