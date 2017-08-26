using System;

namespace Business
{
    public class BaseDomainObject
    {
        public virtual Guid UniqueId { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            var baseObj = obj as BaseDomainObject;
            if (baseObj == null) return false;
            return baseObj.UniqueId == UniqueId;
        }

        public override int GetHashCode()
        {
            return UniqueId.GetHashCode();
        }
    }
}
