using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.Model
{
    [Serializable]
    public class FL_Planner
    {
        #region Model

        public int PlannerID
        { get; set; }
        public string PlannerName
        { get; set; }
        public int? PlannerSex
        { get; set; }
        public int? PlannerJob
        { get; set; }
        public string PlannerImageName
        { get; set; }
        public string PlannerImagePath
        { get; set; }
        public string PlannerSpecial
        { get; set; }
        public string PlannerJobDescription
        { get; set; }
        public string PlannerIntrodution
        { get; set; }
        public DateTime? CreateDate
        { get; set; }
        public int? CreateEmployee
        { get; set; }
        public Nullable<bool> IsDelete
        { get; set; }
        public string Remark
        { get; set; }

        #endregion Model
    }
}
