using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using CriWare;
using System;

namespace Audio
{
    interface IAudioLoadable
    {
        /// <summary>
        /// �L���[�V�[�g�����[�h����
        /// </summary>
        /// <param name="acbNameList">�L���[�V�[�g�����i�[���ꂽList</param>
        public UniTask LoadCueSheet(List<string> acbNameList);

        /// <summary>
        /// �����̃f�[�^�̃��[�h�����̃R�[���o�b�N
        /// </summary>
        public event Action OnCompleteAudioLoad;

        /// <summary>
        /// Acb���擾����
        /// </summary>
        /// <param name="cueSheetName">�L���[�V�[�g��</param>
        public CriAtomExAcb GetAcbData(string cueSheetName);
    }
}