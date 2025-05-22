using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;

public class TestRequestCookieCollection : IRequestCookieCollection
{
    private readonly Dictionary<string, string> _cookies;

    public TestRequestCookieCollection(Dictionary<string, string> cookies)
    {
        _cookies = cookies ?? new Dictionary<string, string>();
    }

    public string this[string key] => _cookies.ContainsKey(key) ? _cookies[key] : null;

    public int Count => _cookies.Count;

    public ICollection<string> Keys => _cookies.Keys;

    public bool ContainsKey(string key) => _cookies.ContainsKey(key);

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => _cookies.GetEnumerator();

    public bool TryGetValue(string key, out string value) => _cookies.TryGetValue(key, out value);

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _cookies.GetEnumerator();
}
