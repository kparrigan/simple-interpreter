using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SimpleInterpreter.Core.Exceptions;
using SimpleInterpreter.Core.Nodes;

namespace SimpleInterpreter.Core
{
    public abstract class NodeVisitor
    {
        #region Public

        public object Visit(ASTNode node)
        {
            var methodName = "Visit" + node.GetType().Name;
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (method == null)
            {
                throw new InvalidNodeTypeException($"Method {methodName} not found." );
            }

            return method.Invoke(this, new object [] {node});
        }
        #endregion
    }
}
