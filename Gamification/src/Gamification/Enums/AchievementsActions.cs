using System.Runtime.Serialization;

namespace Gamification.Core.Enums
{
    [DataContract]
    public enum AssignUnassign : byte
    {
        Assign,

        Unassign
    }
}