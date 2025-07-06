using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Video;

    public class PlayerStats : MonoBehaviour
    {
    public int maxHealth = 100;//最大生命
    public float maxStamina = 100f;//最大體力
    public int currentHealth;//當前生命
    public float currentStamina;//當前體力
    public float staminaDrainRate = 20f;//每秒消耗
    public float staminaRecoverRate = 10f;//每秒回復
    public Image staminaBarFill;//體力條UI元件
    public int attackPower = 10;//攻擊力，可以延伸至戰鬥系統
    public Image healthBarFill;//血條UI元件
        void Start()
        {
            currentStamina = maxStamina;//剛開始滿血
            UpdateStaminaUI();//一進遊戲就先更新一次 UI
            currentHealth = maxHealth;//剛開始滿體力
            UpdateHealthUI();//一開始就先更新一次
        }
        void Update()
        {
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)// 當按住左Shift「而且」體力還有剩的時候，才可以跑步並消耗體力
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;//當前體力減 每秒體力消耗乘以每秒=每秒扣20體力
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);//限制體力為當前體力值，最小值為0最大值為我們預設的體力最大值
            UpdateStaminaUI();//每偵更新
        }
        else if (!Input.GetKey(KeyCode.LeftShift))//當玩家沒按住左shift時
        {
            currentStamina += staminaRecoverRate * Time.deltaTime; //每秒回復體力
            currentStamina = Mathf.Clamp(currentStamina, 0 ,maxStamina);//最大值跟最小值限制
            UpdateStaminaUI();//每偵更新
        }
            if (Input.GetKeyDown(KeyCode.H))//按下H受傷
        {
            TakeDamage(10);
        }
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;//扣血
            currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);//不會低於0
            UpdateHealthUI();//扣血後同步更新UI

            if (currentHealth <= 0)//判斷死亡條件
            {
                Die();//執行死亡
            }
        }
        void UpdateHealthUI()
        {
            if (healthBarFill != null)//確定是否有偵測ui
            {
                healthBarFill.fillAmount = (float)currentHealth / maxHealth;//用小數(float)表示血量比例
            }
        }
    void UpdateStaminaUI()
    {
        if (staminaBarFill != null)//確定是否有偵測ui
        {
            staminaBarFill.fillAmount = currentStamina / maxStamina;//用小數(float)來表示體力比例
            }
        }
        void Die()//死亡
        {
            Debug.Log("死亡");
        }
    }