using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    internal class FPM
    {
        public static string[] GetFolderProfileNames()
        {
            RemoveNullFolderProfile();
            if (GlobalVars.folderProfiles == null || GlobalVars.folderProfiles.Count == 0)
                return new string[] { };
            return GlobalVars.folderProfiles.Select(profile => profile.name).ToArray();
        }
        public static FolderProfile? GetProfile(string name, List<FolderProfile> folderProfiles)
        {
            foreach (var profile in folderProfiles.Where(profile => profile.name.Equals(name)))
            {
                return profile;
            }
            return null;
        }
        
        public static void RemoveNullFolderProfile()
        {
            for (var i = GlobalVars.folderProfiles.Count - 1; i >= 0; i--)
            {
                if (GlobalVars.folderProfiles[i].name != null) continue;
                GlobalVars.folderProfiles.RemoveAt(i);
            }
        }
    }
}
