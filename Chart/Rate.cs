using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chart
{
    class Rate
    {
        private DateTime _date;
        private float _popen;

        public DateTime date
        {
            set { this._date = value; }
            get { return this._date; }
        }
        public float popen
        {
            set { this._popen = value; }
            get { return this._popen; }
        }

    }
}
