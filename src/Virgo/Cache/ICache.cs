﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Virgo.Cache
{
    /// <summary>
    /// 定义可以按键存储和获取项目的缓存
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// 缓存项的默认滑动过期时间。
        /// 默认值：60分钟（1小时）
        /// 可以通过配置更改
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 缓存项的默认绝对过期时间
        /// 默认值：空（未使用）
        /// </summary>
        TimeSpan? DefaultAbsoluteExpireTime { get; set; }

        /// <summary>
        /// 从缓存中获取项目,如果缓存提供程序失败，则使用工厂方法获取对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存的项目</returns>
        TValue Get<TValue>(string key, Func<string, TValue> factory) where TValue : class;

        /// <summary>
        /// 从缓存中获取一个项目，如果缓存提供程序失败，则使用工厂方法获取对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="factory">如果不存在，则创建缓存项的工厂方法</param>
        /// <returns>缓存的项目</returns>
        Task<TValue> GetAsync<TValue>(string key, Func<string, Task<TValue>> factory) where TValue : class;

        /// <summary>
        /// 从缓存中获取项目，如果未找到，则返回null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>缓存项目，如果未找到则为null</returns>
        TValue GetOrDefault<TValue>(string key) where TValue : class;

        /// <summary>
        /// 从缓存中获取项目，如果未找到，则返回null
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>缓存项目，如果未找到则为null</returns>
        Task<TValue> GetOrDefaultAsync<TValue>(string key) where TValue : class;

        /// <summary>
        /// 通过键保存/覆盖缓存中的项目
        /// 最多使用一个过期时间 (<paramref name="slidingExpireTime"/> 或 <paramref name="absoluteExpireTime"/>)。
        /// 如果没有指定它们，那么<see cref="DefaultAbsoluteExpireTime"/> 如果不为null将被使用。
        /// 否则, <see cref="DefaultSlidingExpireTime"/>将会被使用。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        void Set<TValue>(string key, TValue value, TimeSpan? slidingExpireTime = null, TimeSpan? absoluteExpireTime = null) where TValue : class;

        /// <summary>
        /// 通过键保存/覆盖缓存中的项目
        /// 最多使用一个过期时间 (<paramref name="slidingExpireTime"/> 或 <paramref name="absoluteExpireTime"/>)。
        /// 如果没有指定它们，那么<see cref="DefaultAbsoluteExpireTime"/> 如果不为null将被使用。 
        /// 否则, <see cref="DefaultSlidingExpireTime"/>将会被使用。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">滑动到期时间</param>
        /// <param name="absoluteExpireTime">绝对到期时间</param>
        Task SetAsync<TValue>(string key, TValue value, TimeSpan? slidingExpireTime = null,
            TimeSpan? absoluteExpireTime = null) where TValue : class;

        /// <summary>
        /// 通过它的键删除缓存项（如果缓存中不存在给定键则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        void Remove(string key);

        /// <summary>
        /// 按键删除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        void Remove(string[] keys);

        /// <summary>
        /// 通过它的键删除缓存项（如果缓存中不存在给定键则不执行任何操作）。
        /// </summary>
        /// <param name="key">Key</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// 按键删除缓存项。
        /// </summary>
        /// <param name="keys">Keys</param>
        Task RemoveAsync(string[] keys);

        /// <summary>
        /// 清除此缓存中的所有项目。
        /// </summary>
        void Clear();

        /// <summary>
        /// 清除此缓存中的所有项目。
        /// </summary>
        Task ClearAsync();
    }
}