using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Samples.CodeAccessPolicy
{
    public class CaplCodeAccessAttribute : CodeAccessSecurityAttribute
    {
        public CaplCodeAccessAttribute(SecurityAction action) : base(action)
        {
        }

        public string PolicyId { get; set; }
        public override IPermission CreatePermission()
        {
            return new CaplCodeAccessPermission(this);
        }
    }
}
