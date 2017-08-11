using System;
using System.Linq.Expressions;


public static class VariableInfo
{
    public static string Name<T>(Expression<Func<T>> memberExpression)
    {
        MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
        return expressionBody.Member.Name;
    }
}