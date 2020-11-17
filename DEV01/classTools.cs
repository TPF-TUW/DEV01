using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEV01
{
    public class classTools
    {
        // show Information Message
        public void showInfoMessage(string StrText)
        {
            XtraMessageBox.Show(StrText, "Programs Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // show Error Message
        public void showErrorMessage(string StrText)
        {
            XtraMessageBox.Show(StrText, "Programs Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Show Warning Message
        public void showWarningMessage(string StrText)
        {
            XtraMessageBox.Show(StrText, "Programs Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Show Question Message
        public void showQuestionMessage(string StrText)
        {
            XtraMessageBox.Show(StrText, "Programs Warning!", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        // Show ConfirmDialog YesNo Option
        public bool doConfirm(string text)
        {
            bool b = false;
            DialogResult status = XtraMessageBox.Show(text, "Confirm Dialog", MessageBoxButtons.YesNo);
            if (status == DialogResult.Yes)
            {
                b = true;
            }
            return b;

            // การใช้งาน
            /*if(obj.showConfirm("Confirm Del?") == true)
            {
                //
            }*/
        }

        // Replace String 
        public string rep(string s)
        {
            return s.Trim().Replace("'", "''");
        }
    }
}
