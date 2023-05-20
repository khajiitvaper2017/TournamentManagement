using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TournamentManagement.Models.DbContext;

public class TriggerAddingConvention : IModelFinalizingConvention
{
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            var table = StoreObjectIdentifier.Create(entityType: entityType, type: StoreObjectType.Table);
            if (table != null
                && entityType.GetDeclaredTriggers().All(predicate: t => t.GetDatabaseName(storeObject: table.Value) == null))
                entityType.Builder.HasTrigger(modelName: table.Value.Name + "_Trigger");

            foreach (var fragment in entityType.GetMappingFragments(storeObjectType: StoreObjectType.Table))
                if (entityType.GetDeclaredTriggers().All(predicate: t => t.GetDatabaseName(storeObject: fragment.StoreObject) == null))
                    entityType.Builder.HasTrigger(modelName: fragment.StoreObject.Name + "_Trigger");
        }
    }
}