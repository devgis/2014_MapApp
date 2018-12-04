using System;
using System.Collections.Generic;
using System.Text;

namespace MapApp.Entities
{
    public class JKINFO
    {
        private Int32 _MAPINFO_ID;
        private Int32 _FZH;
        private Int32 _DKH;
        private Int32 _NDH;
        private Int32 _SBLX;
        private String _DW;
        private String _ADDR;
        private String _SJ;
        private Int32 _ZT;
        private Int32 _BJZ;
        private Int32 _DDZ;

        /// <summary>
        /// ��Ӧmapinfo����
        /// </summary>
        public Int32 MAPINFO_ID
        {
            get
            {
                return _MAPINFO_ID;
            }
            set
            {
                _MAPINFO_ID = value;
            }
        }
        /// <summary>
        /// ��վ��
        /// </summary>
        public Int32 FZH
        {
            get
            {
                return _FZH;
            }
            set
            {
                _FZH = value;
            }
        }
        /// <summary>
        /// �˿ں�
        /// </summary>
        public Int32 DKH
        {
            get
            {
                return _DKH;
            }
            set
            {
                _DKH = value;
            }
        }
        /// <summary>
        ///  �ڵ��
        /// </summary>
        public Int32 NDH
        {
            get
            {
                return _NDH;
            }
            set
            {
                _NDH = value;
            }
        }
        /// <summary>
        /// �豸����
        /// </summary>
        public Int32 SBLX
        {
            get
            {
                return _SBLX;
            }
            set
            {
                _SBLX = value;
            }
        }
        /// <summary>
        /// ��λ
        /// </summary>
        public String DW
        {
            get
            {
                return _DW;
            }
            set
            {
                _DW = value;
            }
        }
        /// <summary>
        /// ��װ�ص�
        /// </summary>
        public String ADDR
        {
            get
            {
                return _ADDR;
            }
            set
            {
                _ADDR = value;
            }
        }
        /// <summary>
        /// ʵʱ����
        /// </summary>
        public String SJ
        {
            get
            {
                return _SJ;
            }
            set
            {
                _SJ = value;
            }
        }
        /// <summary>
        /// ״̬
        /// </summary>
        public Int32 ZT
        {
            get
            {
                return _ZT;
            }
            set
            {
                _ZT = value;
            }
        }
        /// <summary>
        /// ����ֵ�趨
        /// </summary>
        public Int32 BJZ
        {
            get
            {
                return _BJZ;
            }
            set
            {
                _BJZ = value;
            }
        }
        /// <summary>
        /// �ϵ�ֵ�趨
        /// </summary>
        public Int32 DDZ
        {
            get
            {
                return _DDZ;
            }
            set
            {
                _DDZ = value;
            }
        }

    }
}
