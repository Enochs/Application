using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HA.PMS.BLLInterface;
using HA.PMS.DataAssmblly;


namespace HA.PMS.BLLAssmblly.Flow
{
    public class DispatchingState : ICRUDInterface<FL_DispatchingState>
    {
        PMS_WeddingEntities objEntity = new PMS_WeddingEntities();

        public void Handle()
        {
            List<FL_Celebration> celeList = objEntity.FL_Celebration.ToList(); ;
            foreach (var item in celeList)
            {
                var m_dis = objEntity.FL_Dispatching.FirstOrDefault(c => c.CelebrationID == item.CelebrationID);

                if (m_dis != null)
                {
                    var m_disState = objEntity.FL_DispatchingState.FirstOrDefault(c => c.DispatchingID == m_dis.DispatchingID);
                    if (m_disState == null)
                    {
                        var m_quo = objEntity.FL_QuotedPrice.FirstOrDefault(c => c.CustomerID == item.CustomerID);

                        FL_DispatchingState model = new FL_DispatchingState();
                        model.DispatchingID = m_dis.DispatchingID;
                        model.State = 1;
                        model.IsUse = false;
                        model.CreateDate = DateTime.Now;
                        if (m_quo != null)
                        {
                            model.CreateEmpLoyee = Convert.ToInt32(m_quo.EmpLoyeeID.ToString());
                            model.StateEmpLoyee = Convert.ToInt32(m_quo.EmpLoyeeID.ToString());
                        }

                        objEntity.FL_DispatchingState.Add(model);
                        objEntity.SaveChanges();
                    }
                }
            }
        }

        public void CheckState(int DispatchingID, int keyID, int EmpLoyeeID)
        {
            var ObjModel = objEntity.FL_DispatchingState.Where(C => C.IsUse == true && C.DispatchingID == DispatchingID && C.StateEmpLoyee == EmpLoyeeID);

            if (ObjModel.Count() > 1)
            {
                var ObjDeleteModel = objEntity.FL_DispatchingState.FirstOrDefault(C => C.StateKey == keyID);
                objEntity.FL_DispatchingState.Remove(ObjDeleteModel);
                objEntity.SaveChanges();
            }
            else
            {
                var ObjUpdateModel = objEntity.FL_DispatchingState.FirstOrDefault(C => C.StateKey == keyID);
                if (ObjUpdateModel != null)
                {
                    ObjUpdateModel.IsUse = true;
                    objEntity.SaveChanges();

                    var Islist = objEntity.FL_DispatchingState.Where(C => C.IsUse == true && C.DispatchingID == DispatchingID && C.StateEmpLoyee == EmpLoyeeID);
                    if (Islist.Count() > 1)
                    {
                        objEntity.FL_DispatchingState.Remove(Islist.First());
                        objEntity.SaveChanges();
                    }
                }
            }
        }

        public void DeleteforEmployee(int keyID)
        {
            var ObjDeleteList = objEntity.FL_DispatchingState.FirstOrDefault(C => C.StateKey == keyID);
            //foreach (var Objitem in ObjDeleteList)
            //{
            objEntity.FL_DispatchingState.Remove(ObjDeleteList);
            objEntity.SaveChanges();
            //}
        }

        public int Delete(FL_DispatchingState ObjectT)
        {
            throw new NotImplementedException();
        }

        public List<FL_DispatchingState> GetByAll()
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 验证并且删除前任任务
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <param name="CatogryID"></param>
        /// <returns></returns>
        public void SetInsertByEmployeeAndCatogry(int EmployeeID, int CatogryID, int DispatchingID)
        {

            var ObjUpdateModel = objEntity.FL_DispatchingState.FirstOrDefault(C => C.DispatchingID == DispatchingID && C.StateEmpLoyee == EmployeeID && C.StateCatgoryID == CatogryID);
            if (ObjUpdateModel != null)
            {
                var DeleteModel = objEntity.FL_DispatchingState.FirstOrDefault(C => C.StateKey == ObjUpdateModel.StateKey);
                objEntity.FL_DispatchingState.Remove(DeleteModel);
                objEntity.SaveChanges();
            }

        }

        /// <summary>
        /// 获取与本人相关的订单
        /// </summary>
        /// <param name="EmpLoyeeID"></param>
        /// <returns></returns>
        public List<int> GetDispatchingIDKeylist(int EmpLoyeeID)
        {
            var ObjModelList = objEntity.FL_DispatchingState.Where(C => C.StateEmpLoyee == EmpLoyeeID);
            List<int> ObjKeyList = new List<int>();
            foreach (var Objitem in ObjModelList)
            {
                ObjKeyList.Add(Objitem.DispatchingID);
            }
            return ObjKeyList;
        }


        /// <summary>
        /// 根据ID查找
        /// </summary>
        public FL_DispatchingState GetByID(int? KeyID)
        {
            return objEntity.FL_DispatchingState.FirstOrDefault(C => C.StateKey == KeyID);
        }

        public List<FL_DispatchingState> GetByIndex(int PageSize, int PageIndex, out int SourceCount)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Insert(FL_DispatchingState ObjectT)
        {
            objEntity.FL_DispatchingState.Add(ObjectT);
            return objEntity.SaveChanges();

        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="ObjectT"></param>
        /// <returns></returns>
        public int Update(FL_DispatchingState ObjectT)
        {
            //objEntity.FL_DispatchingState.Add(ObjectT);
            return objEntity.SaveChanges();
        }
    }
}
