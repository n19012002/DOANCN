using System;
using Microsoft.AspNetCore.Http;

public static class SessionExtensions
{
	public static void SetLong(this ISession session, string key, long value)
	{
		session.SetString(key, value.ToString());
	}

	public static long? GetLong(this ISession session, string key)
	{
		var value = session.GetString(key);
		return value == null ? (long?)null : long.Parse(value);
	}
}
