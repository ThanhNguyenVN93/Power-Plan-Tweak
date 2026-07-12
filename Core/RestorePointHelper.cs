using System;
using System.Management;

namespace frm_PowerPlanTweak.Core
{
    internal static class RestorePointHelper
    {
        // EventType: 100 = BEGIN_SYSTEM_CHANGE, RestorePointType: 12 = MODIFY_SETTINGS
        private const uint EventTypeBeginSystemChange = 100;
        private const uint RestorePointTypeModifySettings = 12;

        /// <summary>
        /// Best-effort restore point creation. Returns false (without throwing) if System Restore
        /// is disabled or unavailable — callers should log a warning and continue rather than
        /// hard-failing the whole run over this.
        /// </summary>
        public static bool TryCreate(string description, out string error)
        {
            error = null;
            try
            {
                var scope = new ManagementScope(@"\\.\root\default");
                var path = new ManagementPath("SystemRestore");
                using (var systemRestore = new ManagementClass(scope, path, null))
                using (var inParams = systemRestore.GetMethodParameters("CreateRestorePoint"))
                {
                    inParams["Description"] = description;
                    inParams["RestorePointType"] = RestorePointTypeModifySettings;
                    inParams["EventType"] = EventTypeBeginSystemChange;

                    using (var outParams = systemRestore.InvokeMethod("CreateRestorePoint", inParams, null))
                    {
                        uint returnValue = Convert.ToUInt32(outParams["ReturnValue"]);
                        if (returnValue != 0)
                        {
                            error = $"CreateRestorePoint trả về mã lỗi {returnValue}";
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
