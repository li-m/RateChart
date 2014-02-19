using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;



namespace Chart
{
    class ReadData
    {
        private DataSet _ds = new DataSet();
        private List<Rate> _rates = new List<Rate>();
        private bool _isempty = true;
        private float _min;
        private float _max;

        public List<Rate> rates
        {
            set { this._rates = value; }
            get { return this._rates; }
        }

        public bool isempty
        {
            set { this._isempty = value; }
            get { return this._isempty; }
        }

        public float min
        {
            set { this._min = value; }
            get { return this._min; }
        }

        public float max
        {
            set { this._max = value; }
            get { return this._max; }
        }

        public ReadData(string date1, string date2)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection("Server=SENOTHE-PC;Database=eurusd;uid=sa;pwd=saadmin");
                string str = "SELECT * FROM Eurusd_2012_daily WHERE date BETWEEN '" + date1 + "' AND '" + date2 + "'";

                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(str, sqlConnection);
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = sqlCommand;

                da.Fill(_ds);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            if (!(_ds.Tables.Count == 1 && _ds.Tables[0].Rows.Count == 0))
            {
                for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                {
                    Rate rate = new Rate();
                    rate.date = Convert.ToDateTime(_ds.Tables[0].Rows[i][0]);
                    rate.popen = Convert.ToSingle(_ds.Tables[0].Rows[i][1]);
                    rates.Add(rate);
                }

                float f = 0;
                for (int i = 0; i < rates.Count; i++)
                {
                    if (f < rates[i].popen)
                    {
                        f = rates[i].popen;
                    }
                }
                this.max = f;

                for (int i = 0; i < rates.Count; i++)
                {
                    if (f > rates[i].popen)
                    {
                        f = rates[i].popen;
                    }
                }
                this.min = f;

                isempty = false;
            }
        }
    }
}
