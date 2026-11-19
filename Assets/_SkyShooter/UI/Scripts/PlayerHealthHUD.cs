using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using SkyShooter.Player;

namespace SkyShooter.UI
{
    public sealed class PlayerHealthHUD : MonoBehaviour
    {
        // プレイヤー情報 | 플레이어 정보
        [SerializeField]
        private PlayerStats playerStats;

        // HPゲージ | HP 게이지
        [SerializeField]
        private Slider hpBar;

        // HP表示 | HP 수치 표시
        [SerializeField]
        private TMP_Text hpText;

        // ゲームオーバー画面 | 게임 오버 화면
        [SerializeField]
        private GameObject gameOverPanel;

        private void Update()
        {
            // HPゲージの更新 | HP 게이지 갱신
            hpBar.maxValue = playerStats.MaxHp;
            hpBar.value = playerStats.CurrentHp;

            // HP表示の更新 | HP 수치 갱신
            hpText.text =
                $"HP : {playerStats.CurrentHp} / {playerStats.MaxHp}";

            // ゲーム終了判定 | 게임 종료 판정
            if (playerStats.IsDead &&
                !gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // ゲームの再開 | 게임 재시작
        public void Retry()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(
                SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}