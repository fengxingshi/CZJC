using CZJC.Ctrl.API;

namespace CZJC.Ctrl
{
    /// <summary>
    ///     控制器工厂
    /// </summary>
    public class CtrlFactory : API.CtrlFactory
    {
        public override ITestCtrl TestCtrl()
        {
            return new TestCtrl();
        }

    }
}
