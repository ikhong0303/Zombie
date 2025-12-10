using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    public Animator playerAnimator;

    public GameObject bulletPrefab;
    public Transform spawnPos;

    public GameObject muzzleEffect;
    public GameObject particlePrefab;

    public int k;

    public int life = 10;
    public TextMeshProUGUI lifeText;
    public GameObject panel;

    public bool alive;

    private void Start()
    {
        life = 10;
        alive = true;
    }

    public void AddScore()
    {
        k = k +1;
    }

    public void Damage()
    {
        Debug.Log(1);
        if (life > 0)
        {
            life--;
            lifeText.text = $"life : {life}";
        }

        else if (life <= 0)
        {
            lifeText.text = $"life : 0";
            playerAnimator.SetTrigger("IsDead");
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            Restart();
            panel.SetActive(true);
           alive = false;
        }
    }

    public void Restart()
    {
        StartCoroutine("RestartScene");
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //파티클 소환
            GameObject _bullet = Instantiate(bulletPrefab, spawnPos.position, this.transform.rotation);   //총알 소환
            GameObject _muzzle = Instantiate(muzzleEffect, spawnPos.position, this.transform.rotation);   //총알 소환
            GameObject _particle = Instantiate(particlePrefab, spawnPos.position, this.transform.rotation);   //총알 소환

            //Destroy(_muzzle, 0.5f); 
            // Destroy(_particle, 0.5f);
        }
    }
    void FixedUpdate()// 게임 플레이 버튼을 누르고 게임이 끝날 때 까지 실행
    {
        if (alive)
        {
            // 위쪽 키를 눌렀을 때
            if (Input.GetKey(KeyCode.UpArrow)) // 인풋시스템의 (위쪽방향키 키값) 키를 받오는 기능일때 
            {
                //게임 오브젝트를 (방향(속도*시간)) 이동시켜라
                gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                playerAnimator.SetBool("IsRun", true);
                this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else if (Input.GetKey(KeyCode.DownArrow)) // 아래 방향키를 눌렀을 때
            {
                gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime)); // 게임오브젝트를 - 이동시켜라.
                playerAnimator.SetBool("IsRun", true);
                this.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

            }

            else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 방향키를 눌렀을 때
            {
                gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                playerAnimator.SetBool("IsRun", true);
                this.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }

            else if (Input.GetKey(KeyCode.LeftArrow)) // 왼쪽 방향키를 눌렀을 때
            {
                gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
                playerAnimator.SetBool("IsRun", true);
                this.gameObject.transform.rotation = Quaternion.Euler(0, -90, 0);

            }
            else
            {
                playerAnimator.SetBool("IsRun", false);
            }


        }
       



    }
}
