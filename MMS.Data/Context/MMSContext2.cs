using MMS.Common;
using MMS.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Context
{
    public class MMSContext2 : DbContext
    {
        public MMSContext2()
            : base("name=MMSConnectionString")
        {

        }

        static MMSContext2()
        {
            Database.SetInitializer<MMSContext2>(null);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity2
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var entityname = "";
            try
            {


                //     modelBuilder.Entity<Users>()
                //.ToTable("users", schemaName: "public");
                //     base.OnModelCreating(modelBuilder);
                var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        .Where(type => !String.IsNullOrEmpty(type.Namespace))
        .Where(type => type.BaseType != null && type.BaseType.IsGenericType
            && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);

                    // Get the entity type from the configuration instance
                    Type entityType = type.BaseType.GenericTypeArguments.FirstOrDefault();

                    if (entityType != null)
                    {
                        // Setting entity name to lowercase
                        configurationInstance.ToTable(entityType.Name.ToLower(), schemaName: "public");
                        entityname = entityType.Name.ToLower();
                        // Setting column names to lowercase
                        foreach (var property in type.GetProperties().Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Expression<>)))
                        {
                            dynamic expression = property.GetValue(configurationInstance);
                            expression.PropertyName = expression.PropertyName.ToLower();
                        }
                    }
                    modelBuilder.Configurations.Add(configurationInstance);
                }
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message.ToString(), entityname, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
    }
}
