using System;

namespace SF.Entitys
{
    /// <summary>
    /// 告知审核引擎不审核此属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class MapIgnoreAttribute : Attribute
    {
    }
}
