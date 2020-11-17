using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEV01
{
    public class classConn
    {
        public string _server;
        public string _dbname;
        public string _user;
        public string _password;

        SqlConnection conn;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;
        SqlDataReader dr;
        DataTable dt;
        StringBuilder sql;

        public SqlConnection MDS()
        {
            _server = "172.16.0.30";
            _dbname = "MDS";
            _user = "sa";
            _password = "gik8nv@tpf";
            return new SqlConnection("Data Source=" + _server + ";Initial Catalog=" + _dbname + ";Persist Security Info=True;User ID=" + _user + ";Password=" + _password + "");
        }

        public SqlConnection DellInspiron15()
        {
            _server = "S410717NB0201\\MSSQLSERVER2";
            _dbname = "GSS_Test";
            _user = "sa";
            _password = "gik8nv@tpf";
            return new SqlConnection("Data Source=" + _server + ";Initial Catalog=" + _dbname + ";Persist Security Info=True;User ID=" + _user + ";Password=" + _password + "");
        }

        // getData to GridControl
        public void getGc(StringBuilder sql, GridControl dgvName, SqlConnection conn)
        {
            cmd = new SqlCommand(sql.ToString(), conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvName.DataSource = dt;
        }

        // getData to GridView
        public void getDgv(string sql, GridControl dgvName, SqlConnection conn)
        {
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            dgvName.DataSource = dt;
        }

        // get Data to LookupEdit
        public void getGl(string sql, SqlConnection conn, GridLookUpEdit glName, string valName, string displayName)
        {
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            glName.Properties.DataSource = dt;
            glName.Properties.DisplayMember = displayName;
            glName.Properties.ValueMember = valName;
        }

        // get Data to SearchLookupEdit
        public void getSl(string sql, SqlConnection conn, SearchLookUpEdit slName, string valName, string displayName)
        {
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            slName.Properties.DataSource = dt;
            slName.Properties.DisplayMember = displayName;
            slName.Properties.ValueMember = valName;
        }

        // Load Data to ComboBox
        //public void getCbo(string sql, ComboBox cboName, SqlConnection conn)
        //{
        //    cboName.Items.Clear();
        //    //conn = new dbConn().GSSv2_Prod();
        //    cmd = new SqlCommand(sql, conn);
        //    cmd.CommandText = sql;
        //    conn.Open();
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        cboName.Items.Add(dr[0].ToString());
        //    }
        //    conn.Close();
        //}

        // dbQuery Select : Check True/False
        public bool get(string sql, SqlConnection conn)
        {
            bool b = false;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                b = true;
            }
            cmd.Dispose();
            conn.Close();
            return b;
        }

        // Select One Columns
        public string get_oneParameter(string sql, SqlConnection conn, string colName)
        {
            string rs = string.Empty;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                rs = dr[colName].ToString();
            }
            dr.Close();
            cmd.Dispose();
            conn.Close();
            return rs;
        }

        // Select One Columns
        public string getsb_oneParameter(StringBuilder sql, SqlConnection conn, string colName)
        {
            string rs = string.Empty;
            cmd = new SqlCommand(sql.ToString(), conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read() == true)
            {
                rs = dr[colName].ToString();
            }
            dr.Close();
            cmd.Dispose();
            conn.Close();
            return rs;
        }

        // dbQuery Insert/Update/Delete
        public int Query(string sql, SqlConnection conn)
        {
            int i;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            cmd.CommandType = CommandType.Text;
            i = cmd.ExecuteNonQuery();
            conn.Close();
            return i;
        }




        /* ----------------------------------- Special Query This Project Only ----------------------------------- */
        public void getGrid_SMPL(GridControl glName)
        {
            sql = new StringBuilder();
            sql.Append("SELECT OIDSMPL,Status, SMPLNo ,b.Branch as Branch ,RequestDate, SpecificationSize, Season ,c.Name as Customer, UseFor ,g.CategoryName as Category ,p.StyleName as Style,SMPLItem, SMPLPatternNo, PatternSizeZone, CustApproved FROM SMPLRequest smpl left join Branch b on b.OIDBranch = smpl.OIDBranch left join Customer c on c.OIDCUST = smpl.OIDCUST left join GarmentCategory g on g.OIDGCATEGORY = smpl.OIDCATEGORY left join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE Order By Status");
            getGc(sql, glName,MDS());
        }
        public void getGrid_QuantityReq(GridControl glName)
        {
            sql = new StringBuilder();
            sql.Append("SELECT ROW_NUMBER() OVER(order by OIDSMPLDT asc) as No, SMPLQuantityRequired.OIDSMPLDT, SMPLQuantityRequired.OIDSMPL, SMPLRequest.SMPLNo, SMPLRequest.SMPLRevise, SMPLRequest.SMPLItem, SMPLRequest.SMPLPatternNo, SMPLRequest.PatternSizeZone,ProductColor.ColorNo, ProductColor.ColorName, ProductSize.SizeNo, ProductSize.SizeName, SMPLQuantityRequired.Quantity, Unit.UnitName FROM SMPLQuantityRequired INNER JOIN SMPLRequest ON SMPLQuantityRequired.OIDSMPL = SMPLRequest.OIDSMPL INNER JOIN ProductColor ON SMPLQuantityRequired.OIDCOLOR = ProductColor.OIDCOLOR INNER JOIN ProductSize ON SMPLQuantityRequired.OIDSIZE = ProductSize.OIDSIZE INNER JOIN Unit ON SMPLQuantityRequired.OIDUnit = Unit.OIDUNIT/*Where*/Order by SMPLQuantityRequired.OIDSMPLDT");
            getGc(sql, glName, MDS());
        }
        public void getGrid_FBListSample(GridControl gcName)
        {
            sql = new StringBuilder();
            sql.Append("SELECT SMPLQuantityRequired.OIDSMPLDT, SMPLQuantityRequired.OIDSMPL, SMPLRequest.SMPLNo, SMPLRequest.SMPLRevise, SMPLRequest.SMPLItem, SMPLRequest.SMPLPatternNo, SMPLRequest.PatternSizeZone,ProductColor.ColorNo, ProductColor.ColorName, ProductSize.SizeNo, ProductSize.SizeName, SMPLQuantityRequired.Quantity, Unit.UnitName FROM SMPLRequest INNER JOIN SMPLQuantityRequired ON SMPLRequest.OIDSMPL = SMPLQuantityRequired.OIDSMPL INNER JOIN ProductColor ON SMPLQuantityRequired.OIDCOLOR = ProductColor.OIDCOLOR INNER JOIN ProductSize ON SMPLQuantityRequired.OIDSIZE = ProductSize.OIDSIZE INNER JOIN Unit ON SMPLQuantityRequired.OIDUnit = Unit.OIDUNIT /*Where*/ Order By SMPLQuantityRequired.OIDSMPLDT");
            getGc(sql, gcName,MDS());
        }
        public string genSMPLNo()
        {
            string SMPLNo = string.Empty;
            sql = new StringBuilder();
            sql.Append("Select SUBSTRING(Season,1,4)+'S'+cast(OIDDEPT as nvarchar(10))+SUBSTRING( /*string*/'0000'+cast(SUBSTRING(SMPLNo,7,4)+1 as nvarchar(max)) ,/*start*/LEN('0000'+cast(SUBSTRING(SMPLNo,7,4)+1 as nvarchar(max)))-3 ,/*length*/4)+'-0'/*+cast(SUBSTRING(SMPLNo,12,1)+1 as nvarchar(max))*/ as SMPLNo From SMPLRequest Where OIDSMPL =(Select MAX(OIDSMPL) From SMPLRequest)");
            SMPLNo = getsb_oneParameter(sql,MDS(), "SMPLNo");
            return SMPLNo;
        }
    }
}
