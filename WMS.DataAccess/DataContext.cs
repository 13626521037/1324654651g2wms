using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Attributes;
using WalkingTec.Mvvm.Core.Extensions;
using WalkingTec.Mvvm.Core.Models;
using WMS.Model;
using WMS.Model._Admin;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Model.KnifeManagement;
using WMS.Model.PrintManagement;
using WMS.Model.ProductionManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;

namespace WMS.DataAccess
{
    public partial class DataContext : FrameworkContext
    {
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }
        public DbSet<BaseDepartment> BaseDepartments { get; set; }
        public DbSet<BaseOrganization> BaseOrganizations { get; set; }
        public DbSet<BaseUnit> BaseUnits { get; set; }
        public DbSet<BaseWareHouse> BaseWareHouses { get; set; }
        public DbSet<BaseWhArea> BaseWhAreas { get; set; }
        public DbSet<BaseWhLocation> BaseWhLocations { get; set; }
        public DbSet<BaseItemCategory> BaseItemCategorys { get; set; }
        public DbSet<BaseAnalysisType> BaseAnalysisTypes { get; set; }
        public DbSet<BaseItemMaster> BaseItemMasters { get; set; }
        public DbSet<BaseItemMasterTemp> BaseItemMasterTemps { get; set; }
        public DbSet<BaseInventory> BaseInventorys { get; set; }
        public DbSet<BaseInventoryLog> BaseInventoryLogs { get; set; }
        public DbSet<BaseUserWhRelation> BaseUserWhRelations { get; set; }
        public DbSet<BaseSysPara> BaseSysParas { get; set; }
        public DbSet<BaseSupplier> BaseSuppliers { get; set; }
        public DbSet<FrameworkUserRole> FrameworkUserRoles { get; set; }
        public DbSet<FrameworkUserGroup> FrameworkUserGroups { get; set; }
        public DbSet<BaseSequenceDefine> BaseSequenceDefines { get; set; }
        public DbSet<BaseSequenceDefineLine> BaseSequenceDefineLines { get; set; }
        public DbSet<BaseSequenceRecords> BaseSequenceRecordss { get; set; }
        public DbSet<BaseSequenceRecordsDetail> BaseSequenceRecordsDetails { get; set; }
        public DbSet<BaseBarCode> BaseBarCodes { get; set; }
        public DbSet<BaseDocInventoryRelation> BaseDocInventoryRelations { get; set; }
        public DbSet<BaseOperator> BaseOperators { get; set; }

        public DbSet<BaseShortcut> BaseShortcuts { get; set; }
        public DbSet<BaseSysNotice> BaseSysNotices { get; set; }

        public DbSet<BaseCustomer> BaseCustomers { get; set; }

        public DbSet<BaseSeibanCustomerRelation> BaseSeibanCustomerRelations { get; set; }

        #region 库存管理

        // 组托
        public DbSet<InventoryPalletVirtual> InventoryPalletVirtuals { get; set; }
        public DbSet<InventoryPalletVirtualLine> InventoryPalletVirtualLines { get; set; }

        // 冻结、解冻
        public DbSet<InventoryFreeze> InventoryFreezes { get; set; }
        public DbSet<InventoryFreezeLine> InventoryFreezeLines { get; set; }
        public DbSet<InventoryUnfreeze> InventoryUnfreezes { get; set; }
        public DbSet<InventoryUnfreezeLine> InventoryUnfreezeLines { get; set; }

        // 库存拆分
        public DbSet<InventorySplit> InventorySplits { get; set; }
        public DbSet<InventoryAdjustDirect> InventoryAdjustDirects { get; set; }

        // 库存拆零
        public DbSet<InventorySplitSingle> InventorySplitSingles { get; set; }
        public DbSet<InventorySplitSingleLine> InventorySplitSingleLines { get; set; }

        // 移库
        public DbSet<InventoryMoveLocation> InventoryMoveLocations { get; set; }
        public DbSet<InventoryMoveLocationLine> InventoryMoveLocationLines { get; set; }

        // 调入、调出
        public DbSet<InventoryTransferOutDirectDocType> InventoryTransferOutDirectDocTypes { get; set; }
        public DbSet<InventoryTransferOutDirect> InventoryTransferOutDirects { get; set; }
        public DbSet<InventoryTransferOutDirectLine> InventoryTransferOutDirectLines { get; set; }
        public DbSet<InventoryTransferOutManual> InventoryTransferOutManuals { get; set; }
        public DbSet<InventoryTransferOutManualLine> InventoryTransferOutManualLines { get; set; }
        public DbSet<InventoryTransferIn> InventoryTransferIns { get; set; }
        public DbSet<InventoryTransferInLine> InventoryTransferInLines { get; set; }

        // 杂发、杂收
        public DbSet<InventoryOtherShipDocType> InventoryOtherShipDocTypes { get; set; }
        public DbSet<InventoryOtherShip> InventoryOtherShips { get; set; }
        public DbSet<InventoryOtherShipLine> InventoryOtherShipLines { get; set; }
        public DbSet<InventoryOtherReceivement> InventoryOtherReceivements { get; set; }
        public DbSet<InventoryOtherReceivementLine> InventoryOtherReceivementLines { get; set; }

        // 盘点
        public DbSet<InventoryStockTaking> InventoryStockTakings { get; set; }
        public DbSet<InventoryStockTakingAreas> InventoryStockTakingAreass { get; set; }
        public DbSet<InventoryStockTakingLocations> InventoryStockTakingLocationss { get; set; }
        public DbSet<InventoryStockTakingLine> InventoryStockTakingLines { get; set; }
        public DbSet<InventoryStockTakingErpDiffLine> InventoryStockTakingErpDiffLines { get; set; }

        // 库存调整
        public DbSet<InventoryAdjust> InventoryAdjusts { get; set; }
        public DbSet<InventoryAdjustLine> InventoryAdjustLines { get; set; }

        // ERP对账
        public DbSet<InventoryErpDiff> InventoryErpDiffs { get; set; }
        public DbSet<InventoryErpDiffLine> InventoryErpDiffLines { get; set; }

        #endregion

        #region 销售管理

        public DbSet<SalesShip> SalesShips { get; set; }
        
        public DbSet<SalesShipLine> SalesShipLines { get; set; }

        public DbSet<SalesRMA> SalesRMAs { get; set; }
        public DbSet<SalesRMALine> SalesRMALines { get; set; }

        public DbSet<SalesReturnReceivement> SalesReturnReceivements { get; set; }
        public DbSet<SalesReturnReceivementLine> SalesReturnReceivementLines { get; set; }

        #endregion

        #region 采购管理

        public DbSet<PurchaseReceivement> PurchaseReceivements { get; set; }
        public DbSet<PurchaseReceivementLine> PurchaseReceivementLines { get; set; }

        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchaseReturnLine> PurchaseReturnLines { get; set; }

        public DbSet<PurchaseOutsourcingIssue> PurchaseOutsourcingIssues { get; set; }
        public DbSet<PurchaseOutsourcingIssueLine> PurchaseOutsourcingIssueLines { get; set; }

        public DbSet<PurchaseOutsourcingReturn> PurchaseOutsourcingReturns { get; set; }
        public DbSet<PurchaseOutsourcingReturnLine> PurchaseOutsourcingReturnLines { get; set; }

        #endregion

        #region 生产管理

        // 生产领料单
        public DbSet<ProductionIssue> ProductionIssues { get; set; }
        public DbSet<ProductionIssueLine> ProductionIssueLines { get; set; }

        // 生产退料单
        public DbSet<ProductionReturnIssue> ProductionReturnIssues { get; set; }
        public DbSet<ProductionReturnIssueLine> ProductionReturnIssueLines { get; set; }

        // 生产报告单
        public DbSet<ProductionRcvRpt> ProductionRcvRpts { get; set; }
        public DbSet<ProductionRcvRptLine> ProductionRcvRptLines { get; set; }

        #endregion

        #region 刀具模块
        public DbSet<Knife> Knifes { get; set; }
        public DbSet<KnifeCheckOut> KnifeCheckOuts { get; set; }
        public DbSet<KnifeOperation> KnifeOperations { get; set; }
        public DbSet<KnifeCheckOutLine> KnifeCheckOutLines { get; set; }
        public DbSet<KnifeScrap> KnifeScraps { get; set; }
        public DbSet<KnifeScrapLine> KnifeScrapLines { get; set; }
        public DbSet<KnifeCheckIn> KnifeCheckIns { get; set; }
        public DbSet<KnifeCheckInLine> KnifeCheckInLines { get; set; }
        public DbSet<KnifeGrindRequest> KnifeGrindRequests { get; set; }
        public DbSet<KnifeGrindRequestLine> KnifeGrindRequestLines { get; set; }
        public DbSet<KnifeGrindOut> KnifeGrindOuts { get; set; }
        public DbSet<KnifeGrindIn> KnifeGrindIns { get; set; }
        public DbSet<KnifeGrindInLine> KnifeGrindInLines { get; set; }
        public DbSet<KnifeCombine> KnifeCombines { get; set; }
        public DbSet<KnifeCombineLine> KnifeCombineLines { get; set; }
        public DbSet<KnifeTransferIn> KnifeTransferIns { get; set; }
        public DbSet<KnifeGrindOutLine> KnifeGrindOutLines { get; set; }
        public DbSet<KnifeTransferInLine> KnifeTransferInLines { get; set; }
        public DbSet<KnifeTransferOut> KnifeTransferOuts { get; set; }
        public DbSet<KnifeTransferOutLine> KnifeTransferOutLines { get; set; }
        public DbSet<KnifeTaskLog> KnifeTaskLogs { get; set; }
        public DbSet<KnifeLifes> KnifeLifess { get; set; }


        #endregion

        #region 打印管理

        public DbSet<PrintDocument> PrintDocuments { get; set; }

        public DbSet<PrintSupplier> PrintSuppliers { get; set; }

        public DbSet<PrintMO> PrintMOs { get; set; }

        #endregion

        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype) : base(cs, dbtype)
        {

        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null) : base(cs, dbtype, version)
        {

        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin",

                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };
                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);
                await SaveChangesAsync();

                try
                {
                    Dictionary<string, List<object>> data = new Dictionary<string, List<object>>();
                    new Task(() =>
                    {
                    }).Start();
                }
                catch { }
            }
            return state;
        }

        private void SetWorkflowData(string name, string modelname)
        {
            using (var dc = this.CreateNew())
            {
                dc.Set<Elsa_WorkflowDefinition>().Add(new Elsa_WorkflowDefinition
                {
                    ID = Guid.NewGuid().ToString("N").ToLower(),
                    DefinitionId = Guid.NewGuid().ToString("N").ToLower(),
                    Name = name,
                    Version = 1,
                    PersistenceBehavior = 1,
                    IsPublished = true,
                    IsLatest = true,
                    CreatedAt = DateTime.Now,
                    Data = $@"{{
                      ""$id"": ""1"",
                      ""activities"": [
                        {{
                          ""$id"": ""2"",
                          ""activityId"": ""eb10789a-536b-4335-acfe-ee2bfb888cbc"",
                          ""type"": ""WtmApproveActivity"",
                          ""displayName"": ""审批"",
                          ""persistWorkflow"": false,
                          ""loadWorkflowContext"": false,
                          ""saveWorkflowContext"": false,
                          ""properties"": [
                            {{
                              ""$id"": ""3"",
                              ""name"": ""ApproveUsers"",
                              ""expressions"": {{
                                ""$id"": ""4"",
                                ""Json"": ""[\""admin\""]""
                              }}
                            }},
                            {{
                              ""$id"": ""5"",
                              ""name"": ""ApproveRoles"",
                              ""expressions"": {{
                                ""$id"": ""6""
                              }}
                            }},
                            {{
                              ""$id"": ""7"",
                              ""name"": ""ApproveGroups"",
                              ""expressions"": {{
                                ""$id"": ""8""
                              }}
                            }},
                            {{
                              ""$id"": ""9"",
                              ""name"": ""ApproveManagers"",
                              ""expressions"": {{
                                ""$id"": ""10""
                              }}
                            }},
                            {{
                              ""$id"": ""11"",
                              ""name"": ""ApproveSpecials"",
                              ""expressions"": {{
                                ""$id"": ""12""
                              }}
                            }},
                            {{
                              ""$id"": ""13"",
                              ""name"": ""Tag"",
                              ""expressions"": {{
                                ""$id"": ""14""
                              }}
                            }}
                          ],
                          ""propertyStorageProviders"": {{
                            ""$id"": ""15""
                          }}
                        }},
                        {{
                          ""$id"": ""16"",
                          ""activityId"": ""e52df4f2-2da7-43ac-973a-76618072eec2"",
                          ""type"": ""Finish"",
                          ""displayName"": ""结束"",
                          ""persistWorkflow"": false,
                          ""loadWorkflowContext"": false,
                          ""saveWorkflowContext"": false,
                          ""properties"": [
                            {{
                              ""$id"": ""17"",
                              ""name"": ""ActivityOutput"",
                              ""expressions"": {{
                                ""$id"": ""18""
                              }}
                            }},
                            {{
                              ""$id"": ""19"",
                              ""name"": ""OutcomeNames"",
                              ""expressions"": {{
                                ""$id"": ""20""
                              }}
                            }}
                          ],
                          ""propertyStorageProviders"": {{
                            ""$id"": ""21""
                          }}
                        }}
                      ],
                      ""connections"": [
                        {{
                          ""$id"": ""22"",
                          ""sourceActivityId"": ""eb10789a-536b-4335-acfe-ee2bfb888cbc"",
                          ""targetActivityId"": ""e52df4f2-2da7-43ac-973a-76618072eec2"",
                          ""outcome"": ""同意""
                        }},
                        {{
                          ""$id"": ""23"",
                          ""sourceActivityId"": ""eb10789a-536b-4335-acfe-ee2bfb888cbc"",
                          ""targetActivityId"": ""e52df4f2-2da7-43ac-973a-76618072eec2"",
                          ""outcome"": ""拒绝""
                        }}
                      ],
                      ""variables"": {{
                        ""$id"": ""24"",
                        ""data"": {{}}
                      }},
                      ""contextOptions"": {{
                        ""$id"": ""25"",
                        ""contextType"": ""{modelname}"",
                        ""contextFidelity"": ""Burst""
                      }},
                      ""customAttributes"": {{
                        ""$id"": ""26"",
                        ""data"": {{}}
                      }}
                    }}"
                });
                try
                {
                    dc.SaveChanges();
                }
                catch { }
            }
        }


        private void SetTestData(Type modelType, Dictionary<string, List<object>> data, int count = 100)
        {
            int exist = 0;
            if (data.ContainsKey(modelType.FullName))
            {
                exist = data[modelType.FullName].Count;
                if (exist > 0)
                    return;
            }
            using (var dc = this.CreateNew())
            {
                Random r = new Random();
                data[modelType.FullName] = new List<object>();
                int retry = 0;
                List<string> ids = new List<string>();
                for (int i = 0; i < count - exist; i++)
                {
                    var modelprops = modelType.GetRandomValuesForTestData();
                    var newobj = modelType.GetConstructor(Type.EmptyTypes).Invoke(null);
                    var idvalue = modelprops.Where(x => x.Key == "ID").Select(x => x.Value).SingleOrDefault();
                    if (idvalue != null)
                    {
                        if (ids.Contains(idvalue.ToLower()) == false)
                        {
                            ids.Add(idvalue.ToLower());
                        }
                        else
                        {
                            retry++;
                            i--;
                            if (retry > count)
                            {
                                break;
                            }
                            continue;
                        }
                    }
                    foreach (var pro in modelprops)
                    {
                        if (pro.Value == "$fk$")
                        {
                            var fkpro = modelType.GetSingleProperty(pro.Key[0..^2]);
                            var fktype = fkpro?.PropertyType;
                            if (fktype != modelType && typeof(TopBasePoco).IsAssignableFrom(fktype) == true)
                            {
                                try
                                {
                                    SetTestData(fktype, data, count);
                                    newobj.SetPropertyValue(pro.Key, (data[fktype.FullName][r.Next(0, data[fktype.FullName].Count)] as TopBasePoco).GetID());

                                }
                                catch { }
                            }
                        }
                        else
                        {
                            var v = pro.Value;
                            if (v.StartsWith("\""))
                            {
                                v = v[1..];
                            }
                            if (v.EndsWith("\""))
                            {
                                v = v[..^1];
                            }
                            newobj.SetPropertyValue(pro.Key, v);
                        }
                    }
                    if (modelType == typeof(FileAttachment))
                    {
                        newobj.SetPropertyValue("Path", "./wwwroot/logo.png");
                        newobj.SetPropertyValue("SaveMode", "local");
                        newobj.SetPropertyValue("Length", 16728);
                    }
                    if (typeof(IBasePoco).IsAssignableFrom(modelType))
                    {
                        newobj.SetPropertyValue("CreateTime", DateTime.Now);
                        newobj.SetPropertyValue("CreateBy", "admin");
                    }
                    if (typeof(IPersistPoco).IsAssignableFrom(modelType))
                    {
                        newobj.SetPropertyValue("IsValid", true);
                    }
                    try
                    {
                        (dc as DbContext).Add(newobj);
                        data[modelType.FullName].Add(newobj);
                    }
                    catch
                    {
                        retry++;
                        i--;
                        if (retry > count)
                        {
                            break;
                        }
                    }
                }
                try
                {
                    dc.SaveChanges();
                }
                catch { }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // “系统参数”实体Code唯一键
            modelBuilder.Entity<BaseSysPara>()
                .HasIndex(p => new { p.Code })
                .IsUnique(true);

            // “库区”实体Code+存储地点ID唯一键（2025-07-05 改为Code唯一键）
            modelBuilder.Entity<BaseWhArea>()
                .HasIndex(x => new { x.Code })
                .IsUnique(true);

            // “库位”实体Code+库区ID唯一键（2025-07-05 改为Code唯一键）
            modelBuilder.Entity<BaseWhLocation>()
                .HasIndex(x => new { x.Code })
                .IsUnique(true);

            // 物料来源系统ID索引
            modelBuilder.Entity<BaseItemMaster>()
                .HasIndex(x => x.SourceSystemId)
                .HasDatabaseName("IX_BaseItemMaster_SourceSystemId");

            // 库存信息表的非作废的序列号唯一索引
            modelBuilder.Entity<BaseInventory>()
                .HasIndex(x => x.SerialNumber)
                .IsUnique(true)
                .HasFilter($"{nameof(BaseInventory.IsAbandoned)} = 0");

            /*
             * 有过滤条件的唯一键索引创建方法：
             * CREATE UNIQUE INDEX IX_BaseInventory_SerialNumber 
             * ON BaseInventory(SerialNumber) 
             * WHERE IsAbandoned=0
             * */

            #region 并发令牌

            modelBuilder.Entity<Knife>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<BaseInventory>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<BaseSequenceRecords>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<BaseBarCode>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<InventoryPalletVirtualLine>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<InventoryStockTakingLine>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<PurchaseReceivementLine>()
                .Property(p => p.RowVersion)
                .IsRowVersion();

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }

    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            return new DataContext("your full connection string", DBTypeEnum.SqlServer);
        }
    }

}