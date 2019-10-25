﻿using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Virgo.DependencyInjection;

namespace Virgo.Web.Interceptors
{
    public class CustomInterceptor : IInterceptor, ITransientDependency
    {
        public void Intercept(IInvocation invocation)
        {
            System.Diagnostics.Debug.WriteLine(invocation.Method + "执行之前。。。");
            invocation.Proceed();
            System.Diagnostics.Debug.WriteLine(invocation.Method + "执行之后。。。");
        }
    }
}
