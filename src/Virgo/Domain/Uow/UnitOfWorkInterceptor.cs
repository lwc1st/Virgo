﻿using Castle.DynamicProxy;
using System.Reflection;
using Virgo.DependencyInjection;

namespace Virgo.Domain.Uow
{
    /// <summary>
    /// <see cref="IUnitOfWork"/>AOP模式执行
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor, ITransientDependency
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkInterceptor()
        {
        }
        public UnitOfWorkInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 工作单元
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var uowAttr = invocation.MethodInvocationTarget.GetCustomAttribute(typeof(UnitOfWorkAttribute)) as UnitOfWorkAttribute;
            if (invocation.MethodInvocationTarget.IsDefined(typeof(UnitOfWorkAttribute), true))
            {
                try
                {
                    _unitOfWork.BeginTransaction();
                    if (uowAttr.IsolationLevel.HasValue)
                    {
                        _unitOfWork.BeginTransaction(uowAttr.IsolationLevel.Value);
                    }
                    invocation.Proceed();
                    _unitOfWork.Commit();
                }
                catch (System.Exception)
                {
                    _unitOfWork.Rollback();
                }
            }
            invocation.Proceed();
            return;
        }
    }
}
