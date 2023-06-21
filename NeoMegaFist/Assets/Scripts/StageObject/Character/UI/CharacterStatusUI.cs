using StageObject.Buff;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StageObject
{
    public class CharacterStatusUI : MonoBehaviour
    {
        [SerializeField] private Transform buffSlotParent;
        [SerializeField] private BuffSlot buffSlotPrefab;
        [SerializeField] private Slider hpSlider;
        [SerializeField] private Slider staminaSlider;

        private CharacterBase target;
        private List<BuffSlot> buffSlots = new List<BuffSlot>();

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

            IStageObjectBuffManager buffManager = target.GetComponent<IStageObjectBuffManager>();
            buffManager.OnAdd += OnAddBuff;
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

        private void OnAddBuff(BuffBase buff)
        {
            BuffSlot slot = Instantiate(buffSlotPrefab, buffSlotParent);
            slot.Initalize(buff);
            buffSlots.Add(slot);
            buff.OnRemove += () => OnRemoveBuff(slot);
        }

        private void OnRemoveBuff(BuffSlot buffSlot)
        {
            buffSlots.Remove(buffSlot);
        }
    }
}