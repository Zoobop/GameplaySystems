using System;
using System.Collections.Generic;

namespace Entity
{
    public interface IStats
    {
        public event Action<IStats> OnStatsChanged;
        public void ModifyStat(StatAttributeType type, int modification);
        public StatAttributeElement GetStat(StatAttributeType type);
        public IList<StatAttributeElement> AsList();
        public IDictionary<StatAttributeType, StatAttributeElement> AsDictionary();
    }
}