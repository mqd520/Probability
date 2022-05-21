using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlSugar;

namespace Probability.Winform
{
    [SugarTable("probability_records")]
    public class ProbabilityRecords
    {
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
        public virtual int Id { get; set; }

        [SugarColumn(ColumnName = "expected_probability")]
        public virtual double ExpectedProbability { get; set; }

        [SugarColumn(ColumnName = "actual_probability")]
        public virtual double ActualProbability { get; set; }

        [SugarColumn(ColumnName = "probability_difference")]
        public virtual double ProbabilityDifference { get; set; }

        [SugarColumn(ColumnName = "count")]
        public virtual int Count { get; set; }

        [SugarColumn(ColumnName = "start_time")]
        public virtual long StartTime { get; set; }

        [SugarColumn(ColumnName = "end_time")]
        public virtual long EndTime { get; set; }
    }
}
