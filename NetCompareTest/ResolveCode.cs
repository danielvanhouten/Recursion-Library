using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCompareTest
{
    public interface IDifferenceResolver<in TTarget, in TItem>
    {
        void Resolve(TTarget target, TItem left, TItem right);
    }
}
