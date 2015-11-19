using System;
using System.Data.Common;
using Kassandra.Core;

namespace Kassandra.Data.SqlServer
{
    internal class Reader : IReader
    {
        private readonly DbDataReader _reader;

        public Reader(DbDataReader reader)
        {
            _reader = reader;
        }

        public bool Read()
        {
            return _reader.Read();
        }

        public TOutput ValueAs<TOutput>(string key, TOutput defValue = default(TOutput))
        {
            if (_reader[key] == DBNull.Value)
            {
                return defValue;
            }

            return (TOutput) _reader[key];
        }
    }
}