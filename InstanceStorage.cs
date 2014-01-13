
using System;
using System.Collections.Generic;
using System.Threading;

using IHI.Server.Database.Actions;
using IHI.Server.Plugins;

namespace IHI.Server
{
    public class InstanceStorage
    {
        private readonly Dictionary<string, dynamic> _storage = new Dictionary<string,dynamic>();
        public dynamic this[Plugin plugin, string name]
        {
            get
            {
                return this[plugin.Id + ':' + name];
            }
            set
            {
                this[plugin.Id + ':' + name] = value;
            }
        }

        public dynamic this[string name]
        {
            get
            {
                lock (_storage)
                {
                    dynamic value;
                    if (_storage.TryGetValue(name, out value))
                        return value;
                }
                return null;
            }
            set
            {
                lock (_storage)
                {
                    if (value == null)
                        _storage.Remove(name);
                    else
                        _storage[name] = value;
                }
            }
        }
    }
}
