

using UnityEngine;

namespace M1.ProjectTest
{
    [System.Serializable]
    public struct Stats
    {
        public int _atk;
        public int _def;
        public int _res;
        public int _spd;
        public int _crt;
        public int _aim;
        public int _eva;

        public Stats(int atk, int def, int res, int spd, int crt, int aim, int eva)
        {
            _atk = Mathf.Max(0, atk);
            _def = Mathf.Max(0, def);
            _res = Mathf.Max(0, res);
            _spd = Mathf.Max(0, spd);
            _crt = Mathf.Max(0, crt);
            _aim = Mathf.Max(0, aim);
            _eva = Mathf.Max(0, eva);
        }

        //-------------------------------- Functions() --------------------------------//
        public static Stats Sum(Stats A, Stats B)
        {
            Stats result = new Stats();

            result._atk = A._atk + B._atk;
            result._def = A._def + B._def;
            result._res = A._res + B._res;
            result._spd = A._spd + B._spd;
            result._crt = A._crt + B._crt;
            result._aim = A._aim + B._aim;
            result._eva = A._eva + B._eva;

            return result;
        }
    }
}
