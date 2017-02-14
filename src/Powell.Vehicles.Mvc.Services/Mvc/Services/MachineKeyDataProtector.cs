using System.Web.Security;

namespace Powell.Vehicles.Mvc.Services
{
    /// <summary>
    /// See notes concerning <see cref="MachineKeyDataProtector"/>.
    /// </summary>
    /// <a href="!:http://stackoverflow.com/questions/20497761/net-machinekey-protect-what-algorithm-is-used">
    /// .Net Machinekey.Protect - what algorithm is used?</a>
    public class MachineKeyDataProtector : IMachineKeyDataProtector
    {
        private string[] Purposes { get; }

        public MachineKeyDataProtector(string[] purposes)
        {
            Purposes = purposes;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, Purposes);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, Purposes);
        }
    }
}
