using UnityEngine;
using UnityEngine.UI;

namespace StageObject
{
    public class CharacterStatusUI : MonoBehaviour
    {
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider staminaSlider;

        private CharacterBase target;

        public void Initalize(CharacterBase target)
        {
            this.target = target;

            target.OnSetHP += OnSetHP;
            target.OnSetStamina += OnSetStamina;
            target.OnSetMaxHP += OnSetMaxHP;
            target.OnSetMaxStamina += OnSetMaxStamina;

            target.OnDead += () => Destroy(gameObject);

            hpSlider.maxValue = target.MaxHP;
            hpSlider.value = target.HP;

            staminaSlider.maxValue = target.MaxStamina;
            staminaSlider.value = target.Stamina;
        }

        private void Update()
        {
            transform.position = (Vector2)target.transform.position + target.CharacterStatusUIOffset;
        }

        private void OnSetHP(int hp)
        {
            hpSlider.value = hp;
        }

        private void OnSetStamina(int stamina)
        {
            staminaSlider.value = stamina;
        }

        private void OnSetMaxHP(int maxHP)
        {
            hpSlider.maxValue = maxHP;
        }

        private void OnSetMaxStamina(int maxStamina)
        {
            staminaSlider.maxValue = maxStamina;
        }
    }
}