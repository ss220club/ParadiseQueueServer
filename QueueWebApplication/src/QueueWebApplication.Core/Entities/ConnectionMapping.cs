namespace QueueWebApplication.Core.Entities;

public class ConnectionMapping<T>
{
    private readonly Dictionary<T, HashSet<string>> _connections =
    	new Dictionary<T, HashSet<string>>();

    public int Count => _connections.Count;

    public void Add(T key, string connectionId)
    {
    	lock (_connections)
    	{
    		if (!_connections.TryGetValue(key, out var connections))
    		{
    			connections = new HashSet<string>();
    			_connections.Add(key, connections);
    		}

    		lock (connections)
    		{
    			connections.Add(connectionId);
    		}
    	}
    }

    public IEnumerable<string> GetConnections(T key)
    {
	    lock (_connections)
	    {
		    if (_connections.TryGetValue(key, out var connections))
		    {
			    return connections;
		    }
	    }

	    return Enumerable.Empty<string>();
    }

    public void Remove(T key, string connectionsId)
    {
    	lock (_connections)
    	{
    		if (!_connections.TryGetValue(key, out var connections))
    		{
    			return;
    		}

    		lock (connections)
    		{
    			connections.Remove(connectionsId);
    			if (connections.Count == 0)
    			{
    				_connections.Remove(key);
    			}
    		}
    	}

    }
}
