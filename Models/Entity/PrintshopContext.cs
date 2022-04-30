using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PrintShop.Models.Entity
{
    public partial class PrintshopContext : DbContext
    {

        public PrintshopContext(DbContextOptions<PrintshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<CopyCenter> CopyCenters { get; set; }
        public virtual DbSet<DeptAlias> DeptAliases { get; set; }
        public virtual DbSet<FimsBilling> FimsBillings { get; set; }
        public virtual DbSet<FimsCostCenter> FimsCostCenters { get; set; }
        public virtual DbSet<MailTran> MailTrans { get; set; }
        public virtual DbSet<ImagingTrans> ImagingTrans { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<System> Systems { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WoCostCenterProjTask> WoCostCenterProjTasks { get; set; }
        public virtual DbSet<WorkOrder> WorkOrders { get; set; }
        public virtual DbSet<WorkOrderDeptVw> WorkOrderDeptVws { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SRVSQL2019Test;Initial Catalog=Printshop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountPkey);

                entity.ToTable("Account");

                entity.Property(e => e.Account1).HasColumnName("Account");
            });

            modelBuilder.Entity<CopyCenter>(entity =>
            {
                entity.HasKey(e => e.CopyCntrPkey)
                    .IsClustered(false);

                entity.ToTable("Copy_Center");

                entity.HasIndex(e => e.CreateDt, "Create_Dt_IDX")
                    .IsClustered()
                    .HasFillFactor((byte)90);

                entity.Property(e => e.CopyCntrPkey).HasColumnName("Copy_Cntr_Pkey");

                entity.Property(e => e.CopiesQty).HasColumnName("Copies_Qty");

                entity.Property(e => e.CreateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Create_Dt");

                entity.Property(e => e.ExtendedCost)
                    .HasColumnType("money")
                    .HasColumnName("Extended_Cost");

                entity.Property(e => e.ImpressionCnt).HasColumnName("Impression_Cnt");

                entity.Property(e => e.Note)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.NoteIndicator).HasColumnName("Note_Indicator");

                entity.Property(e => e.OriginalQty).HasColumnName("Original_Qty");

                entity.Property(e => e.PressLstUpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("PressLstUpdate_Dt");

                entity.Property(e => e.ProductCategory)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Category");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Product_Desc");

                entity.Property(e => e.ProductPkey).HasColumnName("Product_Pkey");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Product_Type");

                entity.Property(e => e.ProductWeight)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Product_Weight");

                entity.Property(e => e.StockSize)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Stock_Size");

                entity.Property(e => e.UnitCost)
                    .HasColumnType("money")
                    .HasColumnName("Unit_Cost");

                entity.Property(e => e.WoPkey).HasColumnName("WO_Pkey");
            });

            modelBuilder.Entity<DeptAlias>(entity =>
            {
                entity.HasKey(e => e.DeptAliasPkey);

                entity.ToTable("Dept_Alias");

                entity.Property(e => e.DeptAliasPkey)
                    .ValueGeneratedNever()
                    .HasColumnName("DeptAlias_Pkey");

                entity.Property(e => e.Alias)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FimsBilling>(entity =>
            {
                entity.HasKey(e => e.BillPkey);

                entity.ToTable("FIMS_Billing");

                entity.Property(e => e.BillPkey).HasColumnName("Bill_Pkey");

                entity.Property(e => e.BillBatchName)
                    .HasColumnType("datetime")
                    .HasColumnName("Bill_Batch_Name");

                entity.Property(e => e.BillBudget)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Budget")
                    .IsFixedLength(true);

                entity.Property(e => e.BillExpendComment)
                    .IsRequired()
                    .HasMaxLength(47)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Expend_Comment")
                    .IsFixedLength(true);

                entity.Property(e => e.BillExpendEndingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Bill_Expend_Ending_Date");

                entity.Property(e => e.BillExpendItemDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Bill_Expend_Item_Date");

                entity.Property(e => e.BillExpendType)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Expend_Type")
                    .IsFixedLength(true);

                entity.Property(e => e.BillNonLabor)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Non_Labor")
                    .IsFixedLength(true);

                entity.Property(e => e.BillNonLaborName)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Non_Labor_Name")
                    .IsFixedLength(true);

                entity.Property(e => e.BillObject)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Object")
                    .IsFixedLength(true);

                entity.Property(e => e.BillOrigTransRef)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Orig_Trans_Ref")
                    .IsFixedLength(true);

                entity.Property(e => e.BillProjectNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Project_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.BillQuantity)
                    .HasColumnType("numeric(1, 0)")
                    .HasColumnName("Bill_Quantity");

                entity.Property(e => e.BillRawCost)
                    .HasColumnType("money")
                    .HasColumnName("Bill_Raw_Cost");

                entity.Property(e => e.BillRawCostRate)
                    .HasColumnType("money")
                    .HasColumnName("Bill_Raw_Cost_Rate");

                entity.Property(e => e.BillStockNbr)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Stock_Nbr")
                    .IsFixedLength(true);

                entity.Property(e => e.BillTaskNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Task_Number")
                    .IsFixedLength(true);

                entity.Property(e => e.BillTransSource)
                    .IsRequired()
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Trans_Source")
                    .IsFixedLength(true);

                entity.Property(e => e.BillTransStatus)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Trans_Status")
                    .IsFixedLength(true);

                entity.Property(e => e.BillUnitMeasure)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Unit_Measure")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<FimsCostCenter>(entity =>
            {
                entity.HasKey(e => e.CostCenterPkey)
                    .IsClustered(false);

                entity.ToTable("FIMS_CostCenter");

                entity.HasIndex(e => e.ProjectNumber, "FIMS_CostCenter1")
                    .IsClustered()
                    .HasFillFactor((byte)90);

                entity.HasIndex(e => new { e.ProjectNumber, e.TaskNbr }, "FIMS_CostCenter3")
                    .HasFillFactor((byte)90);

                entity.Property(e => e.CostCenterPkey).HasColumnName("Cost_Center_Pkey");

                entity.Property(e => e.CcName)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("CC_Name");

                entity.Property(e => e.CostCntr)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OrganizationName)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("Organization_Name");

                entity.Property(e => e.ProjectNumber)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Project_Number");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("Task_Name");

                entity.Property(e => e.TaskNbr)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Task_Nbr");
            });

            modelBuilder.Entity<MailTran>(entity =>
            {
                entity.HasKey(e => e.MailPkey);

                entity.HasIndex(e => new { e.Activity, e.ProjectNumber, e.TaskNbr }, "MailroomIDX1 ")
                    .HasFillFactor((byte)90);

                entity.Property(e => e.MailPkey).HasColumnName("Mail_Pkey");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Activity)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.BillDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Bill_dt");

                entity.Property(e => e.Cost).HasColumnType("smallmoney");

                entity.Property(e => e.CostCntr)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.MailMeterBegin)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Mail_Meter_Begin");

                entity.Property(e => e.MailMeterEnd)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Mail_Meter_End");

                entity.Property(e => e.ProjectNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Project_Number");

                entity.Property(e => e.ReBillDt)
                    .HasColumnType("datetime")
                    .HasColumnName("ReBill_Dt");

                entity.Property(e => e.ReceiptNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Receipt_Number");

                entity.Property(e => e.RelPkey).HasColumnName("Rel_Pkey");

                entity.Property(e => e.TaskNbr)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Task_Nbr");

                entity.Property(e => e.TransDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Trans_Dt");
            });

            modelBuilder.Entity<ImagingTrans>(entity =>
            {
                entity.HasKey(e => e.Imaging_Pkey);

                entity.HasIndex(e => new { e.Activity, e.ProjectNumber, e.TaskNbr }, "MailroomIDX1 ")
                    .HasFillFactor((byte)90);

                entity.Property(e => e.Imaging_Pkey).HasColumnName("Imaging_Pkey");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Activity)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Cost).HasColumnType("smallmoney");

                entity.Property(e => e.CostCntr)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectNumber)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Project_Number");


                entity.Property(e => e.TaskNbr)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("Task_Nbr");

                entity.Property(e => e.TransDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Trans_Dt");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductPkey)
                    .HasName("PK_Product_1");

                entity.ToTable("Product");

                entity.Property(e => e.ProductPkey).HasColumnName("Product_Pkey");

                entity.Property(e => e.PressDrType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Press_DR_Type")
                    .IsFixedLength(true);

                entity.Property(e => e.ProductActive).HasColumnName("Product_Active");

                entity.Property(e => e.ProductCategory)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_Category");

                entity.Property(e => e.ProductCreateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Product_Create_Dt");

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Product_Desc");

                entity.Property(e => e.ProductItemNmbr)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Product_Item_Nmbr");

                entity.Property(e => e.ProductQuantity).HasColumnName("Product_Quantity");

                entity.Property(e => e.ProductType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Product_Type");

                entity.Property(e => e.ProductUpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Product_Update_DT");

                entity.Property(e => e.ProductVendor)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Product_Vendor");

                entity.Property(e => e.ProductVendorPrice)
                    .HasColumnType("money")
                    .HasColumnName("Product_Vendor_Price");

                entity.Property(e => e.ProductWeight)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Product_Weight");

                entity.Property(e => e.StockSize)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Stock_Size");

                entity.Property(e => e.UnitCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Unit_Cost");

                entity.Property(e => e.UnitType)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Unit_Type");
            });

            modelBuilder.Entity<System>(entity =>
            {
                entity.HasKey(e => e.SystemPkey);

                entity.ToTable("System");

                entity.Property(e => e.SystemPkey)
                    .HasColumnName("System_Pkey");

                entity.Property(e => e.BindTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Bind_Tbl");

                entity.Property(e => e.BinderyLaborCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Bindery_Labor_Cst");

                entity.Property(e => e.CopierCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Copier_Cost");

                entity.Property(e => e.CopierCostColor)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Copier_Cost_Color");

                entity.Property(e => e.FilmRollCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("FilmRoll_Cost");

                entity.Property(e => e.Imagingcost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Imaging_cost");

                entity.Property(e => e.CopierQtyLimit)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Copier_Qty_Limit");

                entity.Property(e => e.CopyCenterLimit).HasColumnType("money");

                entity.Property(e => e.CopyCntrLaborCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Copy_Cntr_Labor_Cst");

                entity.Property(e => e.CopyCntrPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Copy_Cntr_Pkey");

                entity.Property(e => e.CopyCntrTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Copy_Cntr_Tbl");

                entity.Property(e => e.CostCtrPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("CostCtr_Pkey");

                entity.Property(e => e.CostctrTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Costctr_Tbl");

                entity.Property(e => e.DarkRmTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("DarkRm_Tbl");

                entity.Property(e => e.DepartTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Depart_Tbl");

                entity.Property(e => e.DepartmentPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Department_Pkey");

                entity.Property(e => e.DeskTopLaborCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("DeskTop_Labor_Cost");

                entity.Property(e => e.DeskTopPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("DeskTop_Pkey");

                entity.Property(e => e.DeskTopTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("DeskTop_Tbl");

                entity.Property(e => e.DrAddedLaborCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("DR_AddedLabor_Cst");

                entity.Property(e => e.DrDoubleBurnCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("DR_DoubleBurn_Cst");

                entity.Property(e => e.DrPlateCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("DR_Plate_Cst");

                entity.Property(e => e.MailMeterBegin)
                    .HasColumnType("money")
                    .HasColumnName("Mail_Meter_Begin");

                entity.Property(e => e.MailMeterEnd)
                    .HasColumnType("money")
                    .HasColumnName("Mail_Meter_End");

                entity.Property(e => e.PostageDueLimit).HasColumnType("money");

                entity.Property(e => e.PressLaborCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Press_Labor_Cst");

                entity.Property(e => e.PressScoreCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Press_Score_Cst");

                entity.Property(e => e.PressSetupCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Press_Setup_Cst");

                entity.Property(e => e.PressTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Press_Tbl");

                entity.Property(e => e.PressWashupCst)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Press_Washup_Cst");

                entity.Property(e => e.ProductPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Product_Pkey");

                entity.Property(e => e.ProductTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Product_Tbl");

                entity.Property(e => e.SchedTbl)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("Sched_Tbl");

                entity.Property(e => e.UsmailLimit)
                    .HasColumnType("money")
                    .HasColumnName("USMailLimit");

                entity.Property(e => e.WoPkey)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("WO_Pkey");

                entity.Property(e => e.WoTableNo)
                    .HasColumnType("numeric(2, 0)")
                    .HasColumnName("WO_Table_No");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WoCostCenterProjTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("WO_CostCenterProjTask");

                entity.HasIndex(e => new { e.CostCenter, e.Project, e.Task }, "CostCenterProjTask");

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Cost_Center");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Dept_Name");

                entity.Property(e => e.Project)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Task)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<WorkOrder>(entity =>
            {
                entity.HasKey(e => e.WoPkey)
                    .IsClustered(false);

                entity.ToTable("Work_Order");

                entity.HasIndex(e => e.WoCreateDt, "WO_CreateDt_IDX")
                    .IsClustered()
                    .HasFillFactor((byte)90);

                entity.Property(e => e.WoPkey).HasColumnName("WO_Pkey");

                entity.Property(e => e.AcctCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("Acct_Code");

                entity.Property(e => e.BillDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Bill_Dt");

                entity.Property(e => e.BillReversalInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Reversal_Ind")
                    .IsFixedLength(true);

                entity.Property(e => e.CopyCntrCollate)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Copy_Cntr_Collate");

                entity.Property(e => e.CopyCntrDrill)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Copy_Cntr_Drill");

                entity.Property(e => e.CopyCntrPrint)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Copy_Cntr_Print");

                entity.Property(e => e.CopyCntrPunch)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Copy_Cntr_Punch");

                entity.Property(e => e.CopyCntrStaple)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Copy_Cntr_Staple");

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Cost_Center");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Dept_Name");

                entity.Property(e => e.DeptPh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Dept_Ph");

                entity.Property(e => e.DrAddLabor)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("DR_Add_Labor");

                entity.Property(e => e.DrDoubleBurnQty)
                    .HasColumnType("numeric(4, 0)")
                    .HasColumnName("DR_DoubleBurn_Qty");

                entity.Property(e => e.DrPlateQty)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("DR_Plate_Qty");

                entity.Property(e => e.DtAppovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_Appoval_Date");

                entity.Property(e => e.DtApprovalName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("DT_Approval_Name");

                entity.Property(e => e.DtDptDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_DPT_Date");

                entity.Property(e => e.DtFinalDueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_Final_Due_Date");

                entity.Property(e => e.DtFirstProofDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DT_First_Proof_Date");

                entity.Property(e => e.DtHours)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("DT_Hours");

                entity.Property(e => e.DtScans)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DT_Scans");

                entity.Property(e => e.ImpessionCount)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Impession_Count");

                entity.Property(e => e.JobDescription)
                    .HasMaxLength(47)
                    .IsUnicode(false)
                    .HasColumnName("Job_Description");

                entity.Property(e => e.LstUpdateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("LstUpdate_Dt");

                entity.Property(e => e.Note)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.OriginalNmbr)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("Original_Nmbr");

                entity.Property(e => e.OriginalsQuantity)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("Originals_Quantity");

                entity.Property(e => e.PanToneColorOrder1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PanTone_Color_Order1");

                entity.Property(e => e.PanToneColorOrder2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PanTone_Color_Order2");

                entity.Property(e => e.PanToneColorOrder3)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PanTone_Color_Order3");

                entity.Property(e => e.PanToneColorOrder4)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PanTone_Color_Order4");

                entity.Property(e => e.PanToneColorOrder5)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("PanTone_Color_Order5");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.PlateFileNmbr)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("Plate_File_Nmbr");

                entity.Property(e => e.PressLaborHrs)
                    .HasColumnType("numeric(5, 2)")
                    .HasColumnName("Press_Labor_Hrs");

                entity.Property(e => e.Project)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.ReadyBillInd).HasColumnName("Ready_Bill_Ind");

                entity.Property(e => e.RebillDt)
                    .HasColumnType("datetime")
                    .HasColumnName("Rebill_Dt");

                entity.Property(e => e.RebillWo)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("Rebill_WO");

                entity.Property(e => e.ScheduledDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Scheduled_Date");

                entity.Property(e => e.ScoreQty)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("Score_Qty");

                entity.Property(e => e.SenderName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("Sender_Name");

                entity.Property(e => e.SetUpUnitCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("SetUp_Unit_Cost");

                entity.Property(e => e.SetupQty)
                    .HasColumnType("numeric(3, 0)")
                    .HasColumnName("Setup_Qty");

                entity.Property(e => e.Task)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.WashUpUnitCost)
                    .HasColumnType("smallmoney")
                    .HasColumnName("Wash_Up_Unit_Cost");

                entity.Property(e => e.WashupQty)
                    .HasColumnType("numeric(5, 0)")
                    .HasColumnName("Washup_Qty");

                entity.Property(e => e.WoCopyCntrLaborChg)
                    .HasColumnType("smallmoney")
                    .HasColumnName("WO_Copy_Cntr_Labor_Chg");

                entity.Property(e => e.WoCreateDt)
                    .HasColumnType("datetime")
                    .HasColumnName("WO_CreateDT");

                entity.Property(e => e.WoDeptAlias)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("WO_Dept_Alias");

                entity.Property(e => e.WoNmbr)
                    .IsRequired()
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("WO_Nmbr")
                    .IsFixedLength(true);

                entity.Property(e => e.WoType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("WO_Type")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<WorkOrderDeptVw>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("WorkOrderDept_vw");

                entity.Property(e => e.CostCenter)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("Cost_Center");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Dept_Name");

                entity.Property(e => e.Project)
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
