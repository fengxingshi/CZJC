using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CZJC.DB;
using CZJC.Ctrl;

namespace CZJC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (var ctx = new OAContext())
            //{
            //    var re =
            //        from r in ctx.T_TAG.AsNoTracking()
            //        where r.TAG_CATEGORY_ID == 40
            //        select r;
            //    var x = re.Count();
            //}

            //var entity = new EntityCtrl();
            //var ms = entity.GetEntityListByWork(111);
            //var m = ms;
            //var eis = entity.GetEntityItems(1, 2);
            //var si = eis;

            //var ctrl = new UserCtrl();
            //var ds = ctrl.GetUserAllDepts(5344527867880563311);
            //var x = ds;

            //var c = new FlowCtrl();
            //var fs = c.GetFlowByAuthUser(5344527867880563311);
            //var y = fs;

            //var d = new CtrlFactory().UserCtrl();
            //var t = d.GetUserSession(5344527867880563311);

            //Test_BeginWork();

            using (var ctx = new JCContext())
            {
                var re = ctx.TEST.Where(p => p.ID > 0);
                var r = re.ToList();

                //var o = ctx.TEST.SingleOrDefault(p => p.ID == 7344527867880563388);
                //o.NAME += "x";
                //o.TS = DateTime.Now.AddDays(10);
                //o.RS = 0;

                //var cnt = ctx.Database.ExecuteSqlCommand(@"update test set ts=:p0,rs=:p1 where id=:p3", DateTime.Now,
                //    1, 6344527867880563399);

                //var o = new TEST
                //{
                //    ID = 7344527867880563388,
                //    NAME = "J",
                //    AGE = 10,
                //    TS = DateTime.Now.AddDays(10),
                //    RS = 1
                //};
                //ctx.TEST.Add(o);

                //ctx.SaveChanges();
            }
        }

        //private void Test_BeginWork()
        //{
        //    //吴堃5344527867880563311 办公室8862120765255046916 主管副处长7399835161575447264
        //    //6309323265841860375	厅收文
        //    //7615481353620500826	处室收文
        //    //6742160060413950519	厅发文

        //    //生成访问session
        //    var session = new SNSession(orgId: 0, deptId: 8862120765255046916, userId: 5344527867880563311,roleIds:null,delegateUserId:null);

        //    //获取流程列表，根据用户
        //    var flowCtrl = CtrlFactory.FlowCtrl();
        //    var flows = flowCtrl.GetFlowByAuthUser(5344527867880563311);
        //    //用户操作，选择入口流程
        //    var enterFlow = flows[0];
        //    //获取入口流程的入口步骤
        //    var stepCtrl = CtrlFactory.StepCtrl();
        //    var steps = stepCtrl.GetEnterInStepByFlow(enterFlow.FLOW_ID);
        //    //用户操作，选择入口步骤
        //    var enterStep = steps[0];
        //    //--------------
        //    //执行动作
        //    //-------------
        //    var action = new DoAction();
        //    var am = action.BeginWork(session,enterFlow, enterStep);

        //    action.QuickSave(am);
        //}
    }
}
