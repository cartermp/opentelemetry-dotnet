// <copyright file="InMemoryMetricExporter.cs" company="OpenTelemetry Authors">
// Copyright The OpenTelemetry Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

using System.Collections.Generic;
using OpenTelemetry.Metrics;

namespace OpenTelemetry.Exporter
{
    public class InMemoryMetricExporter : MetricExporter
    {
        private readonly ICollection<Metric> exportedItems;
        private InMemoryExporterOptions options;

        public InMemoryMetricExporter(ICollection<Metric> exportedItems, InMemoryExporterOptions options)
        {
            this.exportedItems = exportedItems;
            this.options = options;
        }

        public override AggregationTemporality GetAggregationTemporality()
        {
            return this.options.AggregationTemporality;
        }

        public override ExportResult Export(IEnumerable<Metric> metrics)
        {
            if (this.exportedItems == null)
            {
                return ExportResult.Failure;
            }

            foreach (var metric in metrics)
            {
                this.exportedItems.Add(metric);
            }

            return ExportResult.Success;
        }
    }
}