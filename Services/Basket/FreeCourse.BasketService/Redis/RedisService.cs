﻿using StackExchange.Redis;

namespace FreeCourse.BasketService.Redis
{
    public class RedisService
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb(int db=1) => _connectionMultiplexer.GetDatabase(db);
    }
}
