using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [Header("설정")]
    public int maxHealth = 3; // 최대 체력
    public Image[] heartImages; // 하트 이미지들을 담을 배열

    [Header("게임 오버 UI")]
    public GameObject gameOverText; // 게임 오버 텍스트(또는 패널)를 넣을 변수

    private int currentHealth; // 현재 체력
    private bool isGameOver = false;

    void Start()
    {
        // 게임 시작 시 현재 체력을 최대 체력으로 설정
        currentHealth = maxHealth;
        // 초기 UI 상태 업데이트
        UpdateHeartUI();

        if (gameOverText != null)
        {
            gameOverText.SetActive(false);
        }
    }

    // 외부(다른 스크립트)에서 이 함수를 호출하면 데미지를 받음
    public void TakeDamage(int damageAmount)
    {
        // 체력 감소
        currentHealth -= damageAmount;

        // 체력이 0 밑으로 내려가지 않게 막음
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnGameOver();
            
        }

        // 체력이 변했으니 UI 업데이트
        UpdateHeartUI();
    }

    // 현재 체력에 맞춰 하트 이미지를 켜고 끄는 함수
    private void UpdateHeartUI()
    {
        // 배열에 있는 모든 하트 이미지를 하나씩 검사
        for (int i = 0; i < heartImages.Length; i++)
        {
            // 배열의 인덱스가 현재 체력보다 작으면 하트 키기
            // 체력이 2라면, 인덱스 0, 1번 하트는 켜지고(true), 2번 하트는 꺼짐(false).
            if (i < currentHealth)
            {
                heartImages[i].gameObject.SetActive(true);
            }
            else
            {
                heartImages[i].gameObject.SetActive(false);
            }
        }
    }

    void OnGameOver()
    {
        isGameOver = true;
        Debug.Log("GAME OVER"); 
       
        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
        }
    }
    
    // 테스트를 위한 임시 기능 (나중에 삭제)
    void Update()
    {
        // 게임 오버 상태일 때 로직
        if (isGameOver)
        {
            // 마우스 왼쪽 버튼 클릭
            if (Input.GetMouseButtonDown(0))
            {
                // 현재 Scene을 다시 불러와서 게임 재시작
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            return; // 게임 오버 상태면 아래 테스트 코드는 실행하지 않음
        }
        
        // 스페이스바를 누르면 데미지를 입음
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
    }
}