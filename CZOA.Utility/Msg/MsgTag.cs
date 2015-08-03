namespace CZOA.Msg
{
    public struct WorkMsg
    {
        public const string Error_WorkNameNull = "事项名称不可为空";
        public const string Error_WorkLevelNull = "必须设置事项等级";
        public const string Error_WorkTypeNull = "必须设置事项阅办类型";
        public const string Error_WorkCategoryNull = "必须设置事项分类";
        public const string Error_WorkSourceNull = "必须设置事项来源";
        public const string Error_WorkHostDeptNull = "必须设置主办处室";
    }

    public struct FlowMsg
    {
        public const string Error_FlowNameNUll = "流程名称不可为空";
        public const string Success_Flow = "流程保存成功";
    }
}
