using System;

namespace CZOA
{
    public class SNSession
    {
        public decimal OrgId;
        public decimal DeptId;
        public decimal UserId;
        public decimal? UserType;
        public decimal? DelegateUserId;
        public decimal[] RoleIds;
        public string UserName;
        public string OrgName;
        public string DeptName;

        public SNSession() { }

        public SNSession(decimal orgId, decimal deptId, decimal userId, decimal[] roleIds, string userName,
            string orgName, string deptName, decimal? delegateUserId = null,decimal? userType = null)
        {
            OrgId = orgId;
            DeptId = deptId;
            UserId = userId;
            UserType = userType;
            DelegateUserId = delegateUserId;
            RoleIds = roleIds ?? new decimal[]
            {
            };
            UserName = userName;
            OrgName = orgName;
            DeptName = deptName;
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
}
