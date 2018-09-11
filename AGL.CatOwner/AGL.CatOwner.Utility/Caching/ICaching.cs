using System;

namespace AGL.CatOwner.Utility
{
    public interface ICaching
    {
        Object Get(string key, bool isSessionSpecific, params string[] args);
        bool Set(string key, object value, bool isSessionSpecific, double? duration, params string[] args);
        bool Remove(string key, bool isSessionSpecific = true, params string[] args);
        bool Exists(string key, bool isSessionSpecific, params string[] args);
    }
}
