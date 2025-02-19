﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

namespace Microsoft.Azure.Cosmos.Telemetry
{
    using System;
    using global::Azure.Core;

    internal class DatabaseDupAttributeKeys : IActivityAttributePopulator
    {
        private readonly IActivityAttributePopulator appInsightPopulator;
        private readonly IActivityAttributePopulator otelPopulator;

        public DatabaseDupAttributeKeys() 
        { 
            this.otelPopulator = new OpenTelemetryAttributeKeys();
            this.appInsightPopulator = new AppInsightClassicAttributeKeys();
        }

        public void PopulateAttributes(DiagnosticScope scope, string operationName, string databaseName, string containerName, Uri accountName, string userAgent, string machineId, string clientId, string connectionMode)
        {
            this.appInsightPopulator.PopulateAttributes(scope, operationName, databaseName, containerName, accountName, userAgent, machineId, clientId, connectionMode);
            this.otelPopulator.PopulateAttributes(scope, operationName, databaseName, containerName, accountName, userAgent, machineId, clientId, connectionMode);
        }

        public void PopulateAttributes(DiagnosticScope scope, Exception exception)
        {
            this.appInsightPopulator.PopulateAttributes(scope, exception);
            this.otelPopulator.PopulateAttributes(scope, exception);
        }

        public void PopulateAttributes(DiagnosticScope scope, QueryTextMode? queryTextMode, string operationType, OpenTelemetryAttributes response)
        {
            this.appInsightPopulator.PopulateAttributes(scope, queryTextMode, operationType, response);
            this.otelPopulator.PopulateAttributes(scope, queryTextMode, operationType, response);
        }
    }
}
