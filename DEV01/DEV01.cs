using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using MDS00;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;

namespace DEV01
{
    public partial class DEV01 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        //Global Variable
        classConn db = new classConn();
        classTools ct = new classTools();
        SqlConnection mainConn = new classConn().MDS();
        SqlConnection conn; // Custom Query OtherDB
        string sql = string.Empty;
        string currenTab = string.Empty;

        private Functionality.Function FUNC = new Functionality.Function();
        public DEV01()
        {
            InitializeComponent();
            UserLookAndFeel.Default.StyleChanged += MyStyleChanged;
            iniConfig = new IniFile("Config.ini");
            UserLookAndFeel.Default.SetSkinStyle(iniConfig.Read("SkinName", "DevExpress"), iniConfig.Read("SkinPalette", "DevExpress"));
        }

        private IniFile iniConfig;

        private void MyStyleChanged(object sender, EventArgs e)
        {
            UserLookAndFeel userLookAndFeel = (UserLookAndFeel)sender;
            LookAndFeelChangedEventArgs lookAndFeelChangedEventArgs = (DevExpress.LookAndFeel.LookAndFeelChangedEventArgs)e;
            //MessageBox.Show("MyStyleChanged: " + lookAndFeelChangedEventArgs.Reason.ToString() + ", " + userLookAndFeel.SkinName + ", " + userLookAndFeel.ActiveSvgPaletteName);
            iniConfig.Write("SkinName", userLookAndFeel.SkinName, "DevExpress");
            iniConfig.Write("SkinPalette", userLookAndFeel.ActiveSvgPaletteName, "DevExpress");
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            //Tab : List of SMPL
            bbiNew.PerformClick();
            db.getGrid_SMPL(gridControl1);

            //Tab : Main Load
            radioGroup1.SelectedIndex = 0;
            radioGroup2.SelectedIndex = 0;
            radioGroup3.SelectedIndex = 0;
            radioGroup4.SelectedIndex = 1;
            radioGroup5.SelectedIndex = 1;
            radioGroup6.SelectedIndex = 1;
            dtRequestDate_Main.EditValue = DateTime.Now;
            dtDeliveryRequest_Main.EditValue = DateTime.Now;
            dtCustomerApproved_Main.EditValue = DateTime.Now;
            dtACPRBy_Main.EditValue = DateTime.Now;
            dtFBPRBy_Main.EditValue = DateTime.Now;
            txtCreateBy_Main.EditValue = "0";
            txtCreateDate_Main.EditValue = DateTime.Now;
            txtUpdateBy_Main.EditValue = "0";
            txtUpdateDate_Main.EditValue = DateTime.Now;
            db.getGl("Select OIDBranch,Branch From Branch", mainConn, glBranch_Main, "OIDBranch", "Branch");
            db.getGl("Select distinct OIDDEPT,d.Department From SMPLRequest smpl inner join Department d on d.OIDDepartment = smpl.OIDDEPT",mainConn,glSaleSection_Main, "OIDDEPT", "Department");
            db.getGl("Select distinct Season From SMPLRequest", mainConn, glSeason_Main, "Season", "Season");            
            db.getSl("Select distinct c.OIDCUST,c.ShortName,c.Name From SMPLRequest smpl inner join Customer c on c.OIDCUST = smpl.OIDCUST", mainConn, slCustomer_Main, "OIDCUST", "Name");
            db.getGl("Select distinct OIDGCATEGORY,CategoryName from SMPLRequest smpl inner join GarmentCategory g on g.OIDGCATEGORY = smpl.OIDCATEGORY", mainConn, glCategoryDivision_Main, "OIDGCATEGORY", "CategoryName");
            db.getSl("select distinct p.OIDSTYLE,StyleName From SMPLRequest smpl inner join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE", mainConn, slStyleName_Main, "OIDSTYLE", "StyleName");
            db.getGrid_QuantityReq(gridControl2);

            // Tab : Fabric Load
            db.getGrid_FBListSample(gridControl3);
            db.getSl("select distinct p.OIDSTYLE,StyleName From SMPLRequest smpl inner join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE", mainConn, slVendor_FB, "OIDSTYLE", "StyleName");
            db.getSl("select distinct p.OIDSTYLE,StyleName From SMPLRequest smpl inner join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE", mainConn, slFBColor_FB, "OIDSTYLE", "StyleName");
            db.getSl("select distinct p.OIDSTYLE,StyleName From SMPLRequest smpl inner join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE", mainConn, slFBCode_FB, "OIDSTYLE", "StyleName");
            db.getSl("select distinct p.OIDSTYLE,StyleName From SMPLRequest smpl inner join ProductStyle p on p.OIDSTYLE = smpl.OIDSTYLE", mainConn, slFGColor_FB, "OIDSTYLE", "StyleName");
            db.getGl("Select OIDBranch,Branch From Branch", mainConn, glCurrency_FB, "OIDBranch", "Branch");
        }

        private void LoadData()
        {
            //StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append("SELECT OIDPayment AS No, Name, Description, DuedateCalculation, Status, CreatedBy, CreatedDate ");
            //sbSQL.Append("FROM PaymentTerm ");
            //sbSQL.Append("ORDER BY OIDPayment ");
            //new ObjDevEx.setGridControl(gcPTerm, gvPTerm, sbSQL).getData(false, false, false, true);
        }

        private void NewData()
        {
            //txeName.Text = "";
            //lblStatus.Text = "* Add Payment Term";
            //lblStatus.ForeColor = Color.Green;

            //txeID.Text = new DBQuery("SELECT CASE WHEN ISNULL(MAX(OIDPayment), '') = '' THEN 1 ELSE MAX(OIDPayment) + 1 END AS NewNo FROM PaymentTerm").getString();
            //txeDescription.Text = "";
            //txeDueDate.Text = "";
            //rgStatus.SelectedIndex = -1;

            //txeCREATE.Text = "0";
            //txeDATE.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            //////txeID.Focus();
        }

        private void newMain()
        {
            MessageBox.Show("newMain");
        }

        private void newFabric()
        {
            MessageBox.Show("newFabric");
        }

        private void newMaterials()
        {
            MessageBox.Show("newMaterials");
        }

        private void bbiNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //LoadData();
            //NewData();

            switch (currenTab)
            {
                /*
                Page is : List of Sample
                Page is : Main
                Page is : Fabric
                Page is : Material
                */
                case "Main": newMain(); break;
                case "Fabric": newFabric(); break;
                case "Material": newMaterials(); break;
                default: break;
            }
        }

        private void saveMain()
        {
            MessageBox.Show("saveMain");
            MessageBox.Show(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Value.ToString());
            MessageBox.Show(radioGroup1.Properties.Items[radioGroup1.SelectedIndex].Description);
        }

        private void saveFabric()
        {
            MessageBox.Show("saveFabric");
        }

        private void saveMaterials()
        {
            MessageBox.Show("saveMaterials");
        }

        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (currenTab)
            {
                /*
                Page is : List of Sample
                Page is : Main
                Page is : Fabric
                Page is : Material
                */
                case "Main": saveMain(); break;
                case "Fabric": saveFabric(); break;
                case "Material": saveMaterials(); break;
                default: break;
            }

            //if (txeName.Text.Trim() == "")
            //{
            //    FUNC.msgWarning("Please name.");
            //    txeName.Focus();
            //}
            //else if (txeDescription.Text.Trim() == "")
            //{
            //    FUNC.msgWarning("Please input description.");
            //    txeDescription.Focus();
            //}
            //else
            //{
            //    if (FUNC.msgQuiz("Confirm save data ?") == true)
            //    {
            //        StringBuilder sbSQL = new StringBuilder();
            //        string strCREATE = "0";
            //        if (txeCREATE.Text.Trim() != "")
            //        {
            //            strCREATE = txeCREATE.Text.Trim();
            //        }

            //        bool chkGMP = chkDuplicate();
            //        if (chkGMP == true)
            //        {
            //            string Status = "NULL";
            //            if (rgStatus.SelectedIndex != -1)
            //            {
            //                Status = rgStatus.Properties.Items[rgStatus.SelectedIndex].Value.ToString();
            //            }

            //            if (lblStatus.Text == "* Add Payment Term")
            //            {
            //                sbSQL.Append("  INSERT INTO PaymentTerm(Name, Description, DueDateCalculation, Status, CreatedBy, CreatedDate) ");
            //                sbSQL.Append("  VALUES(N'" + txeName.Text.Trim().Replace("'", "''") + "', N'" + txeDescription.Text.Trim().Replace("'", "''") + "', N'" + txeDueDate.Text.Trim().Replace("'", "''") + "', " + Status + ", '" + strCREATE + "', GETDATE()) ");
            //            }
            //            else if (lblStatus.Text == "* Edit Payment Term")
            //            {
            //                sbSQL.Append("  UPDATE PaymentTerm SET ");
            //                sbSQL.Append("      Name=N'" + txeName.Text.Trim().Replace("'", "''") + "', ");
            //                sbSQL.Append("      Description=N'" + txeDescription.Text.Trim().Replace("'", "''") + "', ");
            //                sbSQL.Append("      DueDateCalculation=N'" + txeDueDate.Text.Trim().Replace("'", "''") + "', ");
            //                sbSQL.Append("      Status=" + Status + " ");
            //                sbSQL.Append("  WHERE(OIDPayment = '" + txeID.Text.Trim() + "') ");
            //            }

            //            //MessageBox.Show(sbSQL.ToString());
            //            if (sbSQL.Length > 0)
            //            {
            //                try
            //                {
            //                    bool chkSAVE = new DBQuery(sbSQL).runSQL();
            //                    if (chkSAVE == true)
            //                    {
            //                        FUNC.msgInfo("Save complete.");
            //                        bbiNew.PerformClick();
            //                    }
            //                }
            //                catch (Exception)
            //                { }
            //            }
            //        }
            //    }
            //}
        }

        private void gvGarment_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            
        }

        private void selectStatus(int value)
        {
            //switch (value)
            //{
            //    case 0:
            //        rgStatus.SelectedIndex = 0;
            //        break;
            //    case 1:
            //        rgStatus.SelectedIndex = 1;
            //        break;
            //    default:
            //        rgStatus.SelectedIndex = -1;
            //        break;
            //}
        }

        private bool chkDuplicate()
        {
            bool chkDup = true;
            //if (txeName.Text != "")
            //{
            //    txeName.Text = txeName.Text.Trim();
            //    if (txeName.Text.Trim() != "" && lblStatus.Text == "* Add Payment Term")
            //    {
            //        StringBuilder sbSQL = new StringBuilder();
            //        sbSQL.Append("SELECT TOP(1) Name FROM PaymentTerm WHERE (Name = N'" + txeName.Text.Trim() + "') ");
            //        if (new DBQuery(sbSQL).getString() != "")
            //        {
            //            FUNC.msgWarning("Duplicate payment term. !! Please Change.");
            //            txeName.Text = "";
            //            chkDup = false;
            //        }
            //    }
            //    else if (txeName.Text.Trim() != "" && lblStatus.Text == "* Edit Payment Term")
            //    {
            //        StringBuilder sbSQL = new StringBuilder();
            //        sbSQL.Append("SELECT TOP(1) OIDPayment ");
            //        sbSQL.Append("FROM PaymentTerm ");
            //        sbSQL.Append("WHERE (Name = N'" + txeName.Text.Trim() + "') ");
            //        string strCHK = new DBQuery(sbSQL).getString();
            //        if (strCHK != "" && strCHK != txeID.Text.Trim())
            //        {
            //            FUNC.msgWarning("Duplicate payment term. !! Please Change.");
            //            txeName.Text = "";
            //            chkDup = false;
            //        }
            //    }
            //}
            return chkDup;
        }

        private void txeName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    txeDescription.Focus();
            //}
        }

        private void txeName_LostFocus(object sender, EventArgs e)
        {
            //txeName.Text = txeName.Text.ToUpper().Trim();
            //bool chkDup = chkDuplicate();
            //if (chkDup == false)
            //{
            //    txeName.Text = "";
            //    txeName.Focus();
            //}
            //else
            //{
            //    txeDescription.Focus();
            //}
        }

        private void txeDescription_KeyDown(object sender, KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        txeDueDate.Focus();
        //    }
        }

        private void txeDueDate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    rgStatus.Focus();
            //}
        }

        private void gvPTerm_RowStyle(object sender, RowStyleEventArgs e)
        {
            
        }

        private void bbiExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if(gvMain.RowCount > 0){
            //string pathFile = new ObjSet.Folder(@"C:\MDS\Export\").GetPath() + "PaymentTermList_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            //gvPTerm.ExportToXlsx(pathFile);
            //Process.Start(pathFile);
            //}
        }

        private void gvPTerm_RowClick(object sender, RowClickEventArgs e)
        {
            //lblStatus.Text = "* Edit Payment Term";
            //lblStatus.ForeColor = Color.Red;

            //txeID.Text = gvPTerm.GetFocusedRowCellValue("No").ToString();
            //txeName.Text = gvPTerm.GetFocusedRowCellValue("Name").ToString();
            //txeDescription.Text = gvPTerm.GetFocusedRowCellValue("Description").ToString();
            //txeDueDate.Text = gvPTerm.GetFocusedRowCellValue("DuedateCalculation").ToString();

            //int status = -1;
            //if (gvPTerm.GetFocusedRowCellValue("Status").ToString() != "")
            //{
            //    status = Convert.ToInt32(gvPTerm.GetFocusedRowCellValue("Status").ToString());
            //}

            //selectStatus(status);

            //txeCREATE.Text = gvPTerm.GetFocusedRowCellValue("CreatedBy").ToString();
            //txeDATE.Text = gvPTerm.GetFocusedRowCellValue("CreatedDate").ToString();
        }

        private void bbiPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gcPTerm.ShowPrintPreview();
        }

        private void bbiPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //gcPTerm.Print();
        }

         private void simpleButton2_Click(object sender, EventArgs e)
        {
            var frm = new DEV01_M04();
            frm.ShowDialog(this);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var frm = new DEV01_M06();
            frm.ShowDialog(this);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var frm = new DEV01_M11();
            frm.ShowDialog(this);
        }

        private void tabbedControlGroup1_SelectedPageChanged(object sender, DevExpress.XtraLayout.LayoutTabPageChangedEventArgs e)
        {
            currenTab = e.Page.Text;
            /*
                Page is : List of Sample
                Page is : Main
                Page is : Fabric
                Page is : Material
            */
            //Console.WriteLine("Page is : " + currenTab);
        }

        private void btnGenSMPLNo_Click(object sender, EventArgs e)
        {
            txtSMPLNo.EditValue = db.genSMPLNo();
            txtStatus.EditValue = "New";
        }
    }
}