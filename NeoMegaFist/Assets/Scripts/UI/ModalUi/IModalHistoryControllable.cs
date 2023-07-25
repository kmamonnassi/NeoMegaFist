using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

namespace Ui.Modal
{
    interface IModalHistoryControllable
    {
        /// <summary>
        /// ���[�_����ǉ�
        /// </summary>
        /// <param name="makeObj">�������郂�[�_���I�u�W�F�N�g</param>
        /// <param name="rootTransform">�e�ƂȂ�Transform</param>
        /// <param name="enterStateName">�������̃A�j���[�V������State�̖��O</param>
        public GameObject Add(GameObject makeObj, Transform rootTransform, string enterStateName);

        /// <summary>
        /// ���[�_��������
        /// </summary>
        /// <param name="exitStateNamem">�����Ƃ��̃A�j���[�V������State�̖��O</param>
        /// <param name="targetModalName">�߂肽�����[�_���̖��O</param>
        public UniTask Remove(string exitStateName, string targetModalName = "");

        /// <summary>
        /// ������S������
        /// </summary>
        public UniTask RemoveAll();

        /// <summary>
        /// ���[�_�����������Ƃ��ɑI������ׂ�UI��o�^����
        /// </summary>
        /// <param name="selectable">���[�_�����������Ƃ��ɑI������ׂ�UI</param>
        public void RegisterSelectedUiWhenRemove(Selectable selectable);
    }
}
