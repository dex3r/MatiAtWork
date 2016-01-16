using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class ExpressionExtensions
{
    public static bool CheckCanWrite(this Expression expression)
    {
        if (expression == null)
        {
            return false;
        }

        bool canWrite = false;

        switch (expression.NodeType)
        {
            case ExpressionType.Index:
                IndexExpression index = (IndexExpression)expression;
                if (index.Indexer != null)
                {
                    canWrite = index.Indexer.CanWrite;
                }
                else
                {
                    return true;
                }
                break;
            case ExpressionType.MemberAccess:
                MemberExpression member = (MemberExpression)expression;
                switch (member.Member.MemberType)
                {
                    case MemberTypes.Property:
                        PropertyInfo prop = (PropertyInfo)member.Member;
                        canWrite = prop.CanWrite;
                        break;
                    case MemberTypes.Field:
                        FieldInfo field = (FieldInfo)member.Member;
                        canWrite = !(field.IsInitOnly || field.IsLiteral);
                        break;
                }
                break;
            case ExpressionType.Parameter:
                return true;
        }

        return canWrite;
    }
}