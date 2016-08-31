using System;
using NHibernate;
using NHibernate.Type;

namespace Business.NHibernate.Interceptors
{
    public class AuditInterceptor : EmptyInterceptor
    {
        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames,
            IType[] types)
        {
            IAuditable auditableEntity = entity as IAuditable;
            if (auditableEntity != null)
            {
                auditableEntity.SetUpdatedDate(DateTime.UtcNow);
                return true;
            }
            return false;
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            IAuditable auditableEntity = entity as IAuditable;
            if (auditableEntity != null)
            {
                DateTime currentDate = DateTime.UtcNow;
                auditableEntity.SetCreatedDate(currentDate);
                auditableEntity.SetUpdatedDate(currentDate);

                return true;
            }
            return false;
        }
    }
}
