using Aliyun.OSS;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using WMS.Model.BaseData;
using WMS.Model.InventoryManagement;
using WMS.Model.KnifeManagement;
using WMS.Model.PrintManagement;
using WMS.Model.ProductionManagement;
using WMS.Model.PurchaseManagement;
using WMS.Model.SalesManagement;
using WMS.Util.U9Para;
using WMS.Util.U9Para.Knife;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WMS.Util
{
    /// <summary>
    /// 访问U9接口的帮助类
    /// </summary>
    public class U9ApiHelper
    {
        /// <summary>
        /// U9地址
        /// </summary>
        public string U9Url { get; set; }

        /// <summary>
        /// U9账套
        /// </summary>
        public string EntCode { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public string OrgCode { get; set; }

        /// <summary>
        /// 用户中文名称
        /// </summary>
        public string WmsUser { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="u9Url">U9地址</param>
        /// <param name="entCode">账套</param>
        /// <param name="orgCode">组织</param>
        /// <param name="wmsUser">当前用户</param>
        public U9ApiHelper(string u9Url, string entCode, string orgCode, string wmsUser)
        {
            U9Url = u9Url;
            EntCode = entCode;
            OrgCode = orgCode;
            WmsUser = wmsUser;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="u9Url">U9地址</param>
        /// <param name="entCode">账套</param>
        /// <param name="orgCode">组织</param>
        public U9ApiHelper(string u9Url, string entCode, string orgCode)
        {
            U9Url = u9Url;
            EntCode = entCode;
            OrgCode = orgCode;
            WmsUser = "";
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="u9Url">U9地址</param>
        /// <param name="entCode">账套</param>
        public U9ApiHelper(string u9Url, string entCode)
        {
            U9Url = u9Url;
            EntCode = entCode;
            OrgCode = "0300";
            WmsUser = "";
        }

        /// <summary>
        /// 通用获取ERP数据方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private U9Return<T> GetData<T>(string url, string paras)
        {
            U9Return<T>? u9Return;
            try
            {
                ReturnResult<U9ApiBaseReturn> rr = NetHelper.Post<U9ApiBaseReturn>(url, paras, new List<KeyValuePair<string, string>>());
                if (rr.Success)
                {
                    if (rr.Entity != null)
                    {
                        string str = rr.Entity.d;
                        u9Return = JsonConvert.DeserializeObject<U9Return<T>>(value: str);
                        if(u9Return == null)
                        {
                           u9Return = new U9Return<T>
                            {
                                Success = false,
                                Msg = "U9返回数据格式转换错误"
                            };
                        }
                    }
                    else
                    {
                        u9Return = new U9Return<T>()
                        {
                            Success = false,
                            Msg = "U9正确返回，但数据为空"
                        };
                    }
                }
                else
                {
                    u9Return = new U9Return<T>()
                    {
                        //Entity = null,
                        Success = false,
                        Msg = rr.Msg
                    };
                }
            }
            catch (Exception ex)
            {
                u9Return = new U9Return<T>
                {
                    Success = false,
                    Msg = $"调用U9接口失败。{ex.Message}"
                };
            }
            return u9Return;
        }

        /// <summary>
        /// 通用提交ERP数据方法（无需返回实体）
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        private U9Return SubmitData(string url, string paras)
        {
            U9Return? u9Return;
            try
            {
                ReturnResult<U9ApiBaseReturn> rr = NetHelper.Post<U9ApiBaseReturn>(url, paras, new List<KeyValuePair<string, string>>());
                if (rr.Success)
                {
                    if (rr.Entity != null)
                    {
                        string str = rr.Entity.d;
                        u9Return = JsonConvert.DeserializeObject<U9Return>(value: str);
                        if (u9Return == null)
                        {
                            u9Return = new U9Return
                            {
                                Success = false,
                                Msg = "U9返回数据格式转换错误"
                            };
                        }
                    }
                    else
                    {
                        u9Return = new U9Return()
                        {
                            Success = false,
                            Msg = "U9正确返回，但数据为空"
                        };
                    }
                }
                else
                {
                    u9Return = new U9Return()
                    {
                        //Entity = null,
                        Success = false,
                        Msg = rr.Msg
                    };
                }
            }
            catch (Exception ex)
            {
                u9Return = new U9Return
                {
                    Success = false,
                    Msg = $"调用U9接口失败。{ex.Message}"
                };
            }
            return u9Return;
        }

        /// <summary>
        /// 根据U9地址和SVC地址构建URL地址
        /// </summary>
        /// <param name="u9url"></param>
        /// <param name="svcStr"></param>
        /// <returns></returns>
        private string GenerateUrl(string u9url, string svcStr)
        {
            if(!u9url.EndsWith("/"))
            {
                u9url += "/";
            }
            return u9url + "RestServices/" + svcStr + "/do";
        }

        #region 基础资料

        /// <summary>
        /// 获取组织信息
        /// </summary>
        /// <param name="u9url"></param>
        /// <param name="entCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="code">要获取的组织编码</param>
        /// <returns></returns>
        public U9Return<List<BaseOrganization>> GetOrganizations(string code)
        {
            GetOrganizationsPara para = new GetOrganizationsPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseOrganization>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncOrg.svc"), paras);
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="u9url"></param>
        /// <param name="entCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="code">要获取的部门编码</param>
        /// <returns></returns>
        public U9Return<List<BaseDepartment>> GetDepartments(string code)
        {
            GetDepartmentsPara para = new GetDepartmentsPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseDepartment>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncDept.svc"), paras);
        }

        /// <summary>
        /// 获取料品分析类别
        /// </summary>
        /// <param name="u9url"></param>
        /// <param name="entCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="code">要获取的分析类别编码</param>
        /// <returns></returns>
        public U9Return<List<BaseAnalysisType>> GetAnalysisTypes(string code)
        {
            GetAnalysisTypesPara para = new GetAnalysisTypesPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseAnalysisType>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncAnalysisType.svc"), paras);
        }

        /// <summary>
        /// 获取料品分类
        /// </summary>
        /// <param name="u9url"></param>
        /// <param name="entCode"></param>
        /// <param name="orgCode"></param>
        /// <param name="code">要获取的料品分类编码</param>
        /// <returns></returns>
        public U9Return<List<BaseItemCategory>> GetItemCategorys(string code, DateTime? lastUpdateTime)
        {
            GetItemCategorysPara para = new GetItemCategorysPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseItemCategory>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncItemCategory.svc"), paras);
        }

        /// <summary>
        /// 获取计量单位
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public U9Return<List<BaseUnit>> GetUnits(string code, DateTime? lastUpdateTime)
        {
            GetUnitsPara para = new GetUnitsPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseUnit>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncUOM.svc"), paras);
        }

        /// <summary>
        /// 获取存储地点
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public U9Return<List<BaseWareHouse>> GetWareHouses(string code, DateTime? lastUpdateTime)
        {
            GetWareHousesPara para = new GetWareHousesPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseWareHouse>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncWh.svc"), paras);
        }

        /// <summary>
        /// 同步前获取料品最后更新时间
        /// </summary>
        /// <returns></returns>
        public U9Return<List<BaseItemMasterTemp>> BeforeSyncGetItemTime()
        {
            U9ApiBasePara para = new U9ApiBasePara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseItemMasterTemp>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IBeforeSyncGetItemTime.svc"), paras);
        }

        /// <summary>
        /// 获取料品
        /// </summary>
        /// <param name="code"></param>
        /// <param name="lastUpdateTime"></param>
        /// <returns></returns>
        public U9Return<List<BaseItemMaster>> GetItems(string code, DateTime? lastUpdateTime, string ids = "")
        {
            GetItemsPara para = new GetItemsPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            para.iDS = ids;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseItemMaster>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncItem.svc"), paras);
        }

        /// <summary>
        /// 获取供应商信息
        /// </summary>
        /// <param name="code">要获取的供应商编码</param>
        /// <returns></returns>
        public U9Return<List<BaseSupplier>> GetSuppliers(string code)
        {
            GetSuppliersPara para = new GetSuppliersPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseSupplier>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncSupplier.svc"), paras);
        }

        /// <summary>
        /// 获取业务员信息
        /// </summary>
        /// <param name="code">要获取的业务员编码</param>
        /// <returns></returns>
        public U9Return<List<BaseOperator>> GetOperators(string code)
        {
            GetOperatorsPara para = new GetOperatorsPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseOperator>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncOperator.svc"), paras);
        }

        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <param name="code">要获取的客户编码</param>
        /// <returns></returns>
        public U9Return<List<BaseCustomer>> GetCustomers(string code, DateTime? lastUpdateTime)
        {
            GetCustomersPara para = new GetCustomersPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseCustomer>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncCustomer.svc"), paras);
        }

        /// <summary>
        /// 获取番号信息
        /// </summary>
        /// <param name="code">要获取的番号</param>
        /// <returns></returns>
        public U9Return<List<BaseSeibanCustomerRelation>> GetSeibans(string code, DateTime? lastUpdateTime)
        {
            GetSeibansPara para = new GetSeibansPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.lastUpdateTime = lastUpdateTime?.ToString("yyyy-MM-dd HH:mm:ss.fff");
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<BaseSeibanCustomerRelation>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncSeiban.svc"), paras);
        }

        #endregion

        #region 采购管理

        /// <summary>
        /// 获取采购收货单
        /// </summary>
        /// <param name="docNo">收货单号</param>
        /// <returns></returns>
        public U9Return<PurchaseReceivement> GetPurchaseReceivement(string docNo)
        {
            GetPurchaseReceivementPara para = new GetPurchaseReceivementPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement>  ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPurchaseReceivement.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 保存检验数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public U9Return SaveInspectionData(PurchaseReceivement data)
        {
            SaveInspectionDataPara para = new SaveInspectionDataPara(data);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.inspector = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateRcvSubLine.svc"), paras);
            return ret;
        }

        public U9Return ApproveRcv(string docNo)
        {
            ApproveRcvPara para = new ApproveRcvPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.docNo = docNo;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveRcv.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取采购退货单
        /// </summary>
        /// <param name="docNo">单号</param>
        /// <returns></returns>
        public U9Return<PurchaseReturn> GetPurchaseReturn(string docNo)
        {
            GetPurchaseReturnPara para = new GetPurchaseReturnPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<PurchaseReturn> ret = GetData<PurchaseReturn>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPurchaseReturnSV.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取委外发料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<PurchaseOutsourcingIssue> GetPurchaseOutsourcingIssue(string docNo)
        {
            GetPurchaseOutsourcingIssuePara para = new GetPurchaseOutsourcingIssuePara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<PurchaseOutsourcingIssue> ret = GetData<PurchaseOutsourcingIssue>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPurchaseOutsourcingIssue.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 审核委外发料/退料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return ApproveOutsourcingOrReturn(string docNo)
        {
            ApprovePurchaseOutsourcingIssuePara para = new ApprovePurchaseOutsourcingIssuePara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.docNo = docNo;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveOutsourcingOrReturn.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取委外退料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<PurchaseOutsourcingReturn> GetPurchaseOutsourcingReturn(string docNo)
        {
            GetPurchaseOutsourcingReturnPara para = new GetPurchaseOutsourcingReturnPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<PurchaseOutsourcingReturn> ret = GetData<PurchaseOutsourcingReturn>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPurchaseOutsourcingReturn.svc"), paras);
            return ret;
        }

        #endregion

        #region 销售管理

        /// <summary>
        /// 获取发货单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<SalesShip> GetSalesShip(string docNo)
        {
            GetSalesShipPara para = new GetSalesShipPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<SalesShip> ret = GetData<SalesShip>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetShip.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 审核发货单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return ApproveShip(ApproveShipPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveShip.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取退回处理单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<SalesRMA> GetRma(string docNo)
        {
            GetRmaPara para = new GetRmaPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<SalesRMA> ret = GetData<SalesRMA>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetRma.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取销售退回收货单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<SalesReturnReceivement> GetReturnRcv(string docNo)
        {
            GetReturnRcvPara para = new GetReturnRcvPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<SalesReturnReceivement> ret = GetData<SalesReturnReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetReturnRcv.svc"), paras);
            return ret;
        }

        #endregion

        #region 生产管理

        /// <summary>
        /// 获取生产发料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<ProductionIssue> GetProductionIssue(string docNo)
        {
            U9GetDocPara para = new U9GetDocPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<ProductionIssue> ret = GetData<ProductionIssue>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetIssueDoc.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 审核生产领料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return ApproveProductionIssue(string docNo)
        {
            ApprovePurchaseOutsourcingIssuePara para = new ApprovePurchaseOutsourcingIssuePara();   // 共用发料单的参数
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.docNo = docNo;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveIssueDoc.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取生产退料单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<ProductionReturnIssue> GetProductionReturnIssue(string docNo)
        {
            U9GetDocPara para = new();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<ProductionReturnIssue> ret = GetData<ProductionReturnIssue>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetReturnIssueDoc.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 生产报工单创建前扫描获取MO信息
        /// </summary>
        /// <param name="moDocNo"></param>
        /// <returns></returns>
        public U9Return<ProductionRcvRptMO> GetRcvRptMo(string moDocNo)
        {
            GetRcvRptMoPara para = new();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.moDocNo = moDocNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<ProductionRcvRptMO> ret = GetData<ProductionRcvRptMO>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetRcvRptMO.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 创建生产报工单（成品入库单）
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public U9Return<CreateRcvRptDocResult> CreateRcvRptDoc(CreateRcvRptDocPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<CreateRcvRptDocResult> ret = GetData<CreateRcvRptDocResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateRcvRptDoc.svc"), paras);
            return ret;
        }

        #endregion

        #region 库存管理

        /// <summary>
        /// （直接调出单）获取调出单单据类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public U9Return<List<InventoryTransferOutDirectDocType>> GetTransferOutDocTypes(string code)
        {
            GetTransferOutDocTypePara para = new GetTransferOutDocTypePara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<InventoryTransferOutDirectDocType>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncTransferOutDocType.svc"), paras);
        }

        /// <summary>
        /// （其它调出单——杂发）获取调出单单据类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public U9Return<List<InventoryOtherShipDocType>> GetMiscShipDocTypes(string code)
        {
            GetTransferOutDocTypePara para = new GetTransferOutDocTypePara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.code = code;

            string paras = JsonConvert.SerializeObject(para);
            return GetData<List<InventoryOtherShipDocType>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ISyncMiscShipDocType.svc"), paras);
        }

        /// <summary>
        /// （直接调出单）创建调出单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public U9Return<CreateTransferOutResult> CreateTransferOut(InventoryTransferOutDirect data)
        {
            CreateTransferOutPara para = new CreateTransferOutPara(data);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return<CreateTransferOutResult> ret = GetData<CreateTransferOutResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateTransferOut.svc"), paras);
            return ret;
        }

        /// <summary>
        /// （手动调出单）获取调出单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return<InventoryTransferOutManual> GetTransferOut(string docNo)
        {
            GetTransferOutPara para = new GetTransferOutPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<InventoryTransferOutManual> ret = GetData<InventoryTransferOutManual>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetTransferOut.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 审核手动调出单 [直接传接口所需的参数]
        /// </summary>
        /// <param name="para">接口所需的参数</param>
        /// <returns></returns>
        public U9Return<List<ApproveTransferOutResult>> ApproveTransferOut(ApproveTransferOutPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            // 处理逻辑已发生变化。WMS不再根据U9回传的ID进行重新绑定行关系（后续调入单扫调出单时直接从U9获取单据信息）
            U9Return<List<ApproveTransferOutResult>> ret = GetData<List<ApproveTransferOutResult>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveTransferOut.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取调出单（用于生成调入单）
        /// </summary>
        /// <param name="docNo">调出单号</param>
        /// <returns></returns>
        public U9Return<GetTransferOutForTransferInResult> GetTransferOutForTransferIn(string docNo)
        {
            GetTransferOutForTransferInPara para = new GetTransferOutForTransferInPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<GetTransferOutForTransferInResult> ret = GetData<GetTransferOutForTransferInResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetTransferOutForTransferIn.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 创建调入单
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public U9Return<CreateTransferInResult> CreateTransferIn(CreateTransferInPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return<CreateTransferInResult> ret = GetData<CreateTransferInResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateTransferIn.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 删除调入单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return DeleteTransferIn(string id) 
        {
            DeleteTransferInPara para = new DeleteTransferInPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.iD = id;

            string paras = para.ToJson();
            U9Return ret = GetData<string>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IDeleteTransferIn.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 审核调入单
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public U9Return ApproveTransferIn(string docNo)
        {
            ApproveTransferInPara para = new ApproveTransferInPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.docNo = docNo;

            string paras = para.ToJson();
            U9Return<PurchaseReceivement> ret = GetData<PurchaseReceivement>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IApproveTransferIn.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 创建并审核杂发单（其它调出单）
        /// </summary>
        /// <returns></returns>
        public U9Return<MiscShipmentData> CreateMiscShipment(InventoryOtherShip data)
        {
            CreateAndApproveMiscShipmentData para = new CreateAndApproveMiscShipmentData(data);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<MiscShipmentData> ret = GetData<MiscShipmentData>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateMiscShipment.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取杂发单（用于生成杂收单）
        /// </summary>
        /// <param name="docNo">调出单号</param>
        /// <returns></returns>
        public U9Return<GetMiscShipForMiscRcvResult> GetMiscShipForMiscRcv(string docNo)
        {
            GetMiscShipForMiscRcvPara para = new GetMiscShipForMiscRcvPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<GetMiscShipForMiscRcvResult> ret = GetData<GetMiscShipForMiscRcvResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetMiscShipForMiscRcv.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 创建杂收单
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public U9Return<CreateMiscRcvResult> CreateMiscRcvTrans(CreateMiscRcvPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return<CreateMiscRcvResult> ret = GetData<CreateMiscRcvResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateMiscRcvTrans.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取WMS、U9库存差异数据
        /// </summary>
        /// <param name="whId"></param>
        /// <param name="entities"></param>
        /// <returns></returns>
        public U9Return<List<InventoryErpDiff>> StockDataMatch(long whId, List<InventoryErpDiff> entities)
        {
            StockDataMatchPara para = new StockDataMatchPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.lines = entities;
            para.whId = whId;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<List<InventoryErpDiff>> ret = GetData<List<InventoryErpDiff>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IStockDataMatch.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 创建盘点单
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public U9Return CreateInventorySheet(CreateInventorySheetPara para)
        {
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return ret = GetData<string>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateInventorySheet.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 判断U9盘点单是否存在
        /// </summary>
        /// <param name="docNo">U9盘点单号</param>
        /// <returns></returns>
        public U9Return<int> IsInventorySheetExist(string docNo)
        {
            IsInventorySheetExistPara para = new IsInventorySheetExistPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.docNo = docNo;

            string paras = para.ToJson();
            U9Return<int> ret = GetData<int>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IIsInventSheetExist.svc"), paras);
            return ret;
        }
        #endregion

        #region 刀具管理
        //创建并审核杂发单
        public U9Return<MiscShipmentData> CreateAndApprovedMiscShipment(BaseOperator handledBy_operator, List<BaseInventory> inventorys, BaseOperator checkOutBy, bool isLowValue)
        {
            CreateAndApproveMiscShipmentData para = new CreateAndApproveMiscShipmentData(handledBy_operator, inventorys, checkOutBy, isLowValue);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<MiscShipmentData> ret = GetData<MiscShipmentData>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateMiscShipment.svc"), paras);
            return ret;
        }
        //创建并审核杂收单
        public U9Return<MiscRcvTransData> CreateAndApprovedMiscRcvTrans(BaseOperator handledBy_operator, List<WMS.Model.KnifeManagement.Knife> knifes)
        {
            CreateAndApproveMiscRcvTransData para = new CreateAndApproveMiscRcvTransData(handledBy_operator, knifes);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<MiscRcvTransData> ret = GetData<MiscRcvTransData>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.ICreateMiscRcvTrans.svc"), paras);
            return ret;
        }
        //创建并审核请购单
        public U9Return<PRInfoReturn> CreateAndApprovedPR(BaseOperator handledBy_operator, List<(Knife knife, BaseItemMaster itemMaster)> knifes_actualItem, string grindRequestDocNo,string Memo)
        {
            CreateAndApprovedPR para = new CreateAndApprovedPR(handledBy_operator, knifes_actualItem, grindRequestDocNo,Memo);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); //JsonConvert.SerializeObject(para);
            U9Return<PRInfoReturn> ret = GetData<PRInfoReturn>(GenerateUrl(U9Url, "UFIDA.U9.Cust.GPG.CSY.SV.WtmGrindSV.ICreateAndApprovePRForKnife.svc"), paras);
            return ret;
        }
        //通过采购单号获取刀具信息
        public U9Return<List<GetKnifeNosByPODocNoResult>> GetKnifesByPODocNo(string pODocNo)
        {
            GetKnifesByPODocNoData para = new GetKnifesByPODocNoData(pODocNo);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson(); 
            U9Return<List<GetKnifeNosByPODocNoResult>> ret = GetData<List<GetKnifeNosByPODocNoResult>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.GPG.CSY.SV.WtmGrindSV.IGetKnifeNosByPODocNo.svc"), paras);
            return ret;
        }
        //通过收货单号获取刀具信息
        public U9Return<List<GetKnifeInfosByRcvDocNoResult>> GetKnifeInfosByRcvDocNo(string rcvDocNo)
        {
            GetKnifeInfosByRcvDocNoData para = new GetKnifeInfosByRcvDocNoData(rcvDocNo);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return<List<GetKnifeInfosByRcvDocNoResult>> ret = GetData<List<GetKnifeInfosByRcvDocNoResult>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.GPG.CSY.SV.WtmGrindSV.IGetKnifeInfosByRcvDocNo.svc"), paras);
            return ret;
        }
        //修磨出库审核时回写U9采购单行子行的已修磨出库字段
        public U9Return<List<string>> POShipLineGrindOutDone(List<System.Int64> Ids)
        {
            POShipLineGrindOutDoneData para = new POShipLineGrindOutDoneData(Ids);
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;

            string paras = para.ToJson();
            U9Return<List<string>> ret = GetData<List<string>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.GPG.CSY.SV.WtmGrindSV.IPOShipLineGrindOutDone.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取刀具配置信息
        /// </summary>
        /// <param name="itemCodes">料号集合</param>
        /// <returns></returns>
        public U9Return<GetKnifeInfoResult> GetKnifeInfo(List<string> itemCodes)
        {
            GetKnifeInfoData para = new GetKnifeInfoData();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.wms_user = WmsUser;
            para.itemCodes = itemCodes;

            string paras = para.ToJson();
            U9Return<GetKnifeInfoResult> ret = GetData<GetKnifeInfoResult>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetKnifeInfo.svc"), paras);
            return ret;
        }


        #endregion

        #region 打印管理

        /// <summary>
        /// 获取单据打印数据
        /// </summary>
        /// <param name="docNo">单号</param>
        /// <param name="itemCode">料号</param>
        /// <returns></returns>
        public U9Return<List<PrintDocument>> GetPrintDocument(string docNo, string itemCode)
        {
            GetPrintDocumentPara para = new GetPrintDocumentPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;
            para.itemCode = itemCode;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<List<PrintDocument>> ret = GetData<List<PrintDocument>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPrintDocument.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取供应商打印数据
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public U9Return<List<PrintSupplier>> GetPrintSupplier(string docNo, string itemCode)
        {
            GetPrintSupplierPara para = new GetPrintSupplierPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;
            para.itemCode = itemCode;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<List<PrintSupplier>> ret = GetData<List<PrintSupplier>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPrintSupplier.svc"), paras);
            return ret;
        }

        /// <summary>
        /// 获取生产订单打印数据
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public U9Return<List<PrintMO>> GetPrintMO(string docNo, string itemCode)
        {
            GetPrintMOPara para = new GetPrintMOPara();
            para.context.EntCode = EntCode;
            para.context.OrgCode = OrgCode;
            para.docNo = docNo;
            para.itemCode = itemCode;

            string paras = JsonConvert.SerializeObject(para);
            U9Return<List<PrintMO>> ret = GetData<List<PrintMO>>(GenerateUrl(U9Url, "UFIDA.U9.Cust.API.WMSSV.NewWmsSV.IGetPrintMO.svc"), paras);
            return ret;
        }

        #endregion
    }
}
