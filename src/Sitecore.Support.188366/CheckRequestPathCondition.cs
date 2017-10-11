using Sitecore.Diagnostics;
using Sitecore.FXM.Rules.Contexts;
using Sitecore.Rules.Conditions;
using Sitecore.StringExtensions;
using System;

namespace Sitecore.FXM.Rules.Conditions
{
    public class CheckRequestPathCondition<T> : StringOperatorCondition<T> where T : RequestRuleContext
    {
        public string Path
        {
            get;
            set;
        }

        protected override bool Execute(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "RuleContext cannot be empty.");
            if(this.Path.IsNullOrEmpty())
            {
                return false;
            }
            if((base.OperatorId == RulesOperatorHelper.EqualsOperator || base.OperatorId == RulesOperatorHelper.StartsWithOperator || base.OperatorId == RulesOperatorHelper.CaseInsensitivelyEqualsOperator || base.OperatorId == RulesOperatorHelper.NotCaseInsensitivelyEqualsOperator || base.OperatorId == RulesOperatorHelper.NotEqualOperator) && !this.Path.StartsWith("/"))
            {
                this.Path = "/" + this.Path;
            }
            return base.Compare(ruleContext.Url.AbsolutePath, this.Path);
        }
    }
}
