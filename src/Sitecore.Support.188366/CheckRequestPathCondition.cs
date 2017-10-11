using Sitecore.Diagnostics;
using Sitecore.FXM.Rules.Contexts;
using Sitecore.Rules.Conditions;
using Sitecore.StringExtensions;
using System;
using System.Web;
using Sitecore.FXM.Rules;

namespace Sitecore.Support.FXM.Rules.Conditions
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
            var decodedAbsolutePath = HttpContext.Current.Server.UrlDecode(ruleContext.Url.AbsolutePath);
            return base.Compare(decodedAbsolutePath, this.Path);
        }
    }
}
