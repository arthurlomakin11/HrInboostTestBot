namespace HrInboostTestBot.Data.ViberUsersHash;

public static class UsersHashTable
{
    private static Dictionary<string, string> _hashTable = new();
    
    public static string GetValue(string key)
    {
        return _hashTable.GetValueOrDefault(key)!;
    }
    
    public static void Add(string key, string value)
    {
        _hashTable.Add(key, value);
    }
    
    public static bool ContainsKey(string key)
    {
        return _hashTable.ContainsKey(key);
    }
}