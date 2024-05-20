using MMS.Common;
using MMS.Core;
using MMS.Core.Entities;
using MMS.Core.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMS.Data.Context
{
    public class MMSContext : DbContext
    {

        public MMSContext()
           : base("name=MMSConnectionString")
        {
            //((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        static MMSContext()
        {
            Database.SetInitializer<MMSContext>(null);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        //   protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //   {
        //       var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
        //.Where(type => !String.IsNullOrEmpty(type.Namespace))
        //.Where(type => type.BaseType != null && type.BaseType.IsGenericType
        //    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

        //       foreach (var type in typesToRegister)
        //       {
        //           dynamic configurationInstance = Activator.CreateInstance(type);
        //           modelBuilder.Configurations.Add(configurationInstance);

        //           // Assuming you want to set the table name to lowercase
        //           string tableName = type.Name.Replace("Configuration", "");
        //           var entityType = type.BaseType.GetGenericArguments().First();
        //           modelBuilder.Entity(entityType).ToTable(tableName.ToLowerInvariant());
        //       }
        //   }
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
