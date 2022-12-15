using System.Runtime.Serialization;
using Common.Attributes;

namespace AuthDomain.Enums.Roles
{

    public enum RolesKey
    {
        [EnumMember(Value = "SuperAdmin")]
        SuperAdmin,
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Moderator")]
        Moderator,
        [EnumMember(Value = "ServiceProvider")]
        ServiceProvider,
        [EnumMember(Value = "Client")]
        Client,
        [EnumMember(Value = "AdvertisementOwner")]
        AdvertisementOwner
    }
}