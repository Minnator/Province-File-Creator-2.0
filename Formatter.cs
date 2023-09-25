using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EU4_Province_Creator
{
    internal class Formatter
    {
        private static readonly string open = " = {";
        private static int tabIndex = 0;

        public static string ListToString(List<string> list)
        {
            StringBuilder sb = new();
            foreach (var item in list)
            {
                sb.AppendLine(item);
            }
            return sb.ToString();
        }

        public static void FormatAllRulerHistory()
        {
            Debug.WriteLine("Started Formatting rulers");
            foreach (var item in GlobalVars.leaderList)
            {
                StringBuilder sb = new ();
                //Debug.WriteLine(item.ToString());
                sb.AppendLine(item.dateOfEntry.ToString("yyyy.MM.dd") + open);
                sb.AppendLine($"\t{item.typeOfRuler}{open}");
                sb.AppendLine($"\t\tname = \"{item.rulerName}\"");
                sb.AppendLine(GlobalVars.featuresWindow.NameIsDynasty.Checked
                    ? $"\t\tdynasty = \"{item.rulerName}\""
                    : $"\t\tdynasty = \"{item.dynasty}\"");
                sb.AppendLine(GlobalVars.featuresWindow.RandomSkillCheckbox.Checked
                    ? $"\t\tadm = {Util.GenerateRandomIntGaussian()}{Environment.NewLine}" +
                      $"\t\tdip = {Util.GenerateRandomIntGaussian()}{Environment.NewLine}" +
                      $"\t\tmil = {Util.GenerateRandomIntGaussian()}"
                    : $"\t\tadm = {item.adm}{Environment.NewLine}" +
                      $"\t\tdip = {item.dip}{Environment.NewLine}" +
                      $"\t\tmil = {item.mil}"); 
                sb.AppendLine($"\t\tbirth_date = {item.dateOfBirth:yyyy.MM.dd}");
                if (item.typeOfRuler.Equals("monarch") && item.dateOfDeath != GlobalVars.defaultDate)
                    sb.AppendLine($"\t\tdeath_date = {item.dateOfDeath:yyyy.MM.dd}");
                if (!item.typeOfRuler.Equals("monarch"))
                    sb.AppendLine(item.dateOfDeath != GlobalVars.defaultDate
                        ? $"\t\tdeath_date = {item.dateOfDeath:yyyy.MM.dd}"
                        : $"\t\tdeath_date = {item.dateOfDeath.AddYears(1):yyyy.MM.dd}");
                if (item.female)
                    sb.AppendLine("\t\tfemale = yes");
                if (item.typeOfRuler.Equals("queen"))
                    sb.AppendLine($"country_of_origin = {item.countryOfOrigin}");
                if (!item.religion.Equals(string.Empty))
                    sb.AppendLine($"\t\treligion  = {item.religion}");
                if (!item.culture.Equals(string.Empty))
                    sb.AppendLine($"\t\treligion  = {item.culture}");
                if (!item.leaderType.Equals(string.Empty))
                    sb.Append(GetLeaderFormat(item));
                sb.AppendLine("\t}");
                sb.AppendLine("}");
                
                GlobalVars.formattedLeaders.Add(new KeyValuePair<string, string>(item.tag, sb.ToString()));
                /*
                if (GlobalVars.formattedRulers.TryGetValue(item.tag, out var ruler))
                    ruler.Add(sb.ToString());
                else
                    GlobalVars.formattedRulers.Add(item.tag, new List<string>{ sb.ToString() });
                */
            }

            StringBuilder sbb = new();

            foreach (var item in GlobalVars.formattedLeaders)
            {
                sbb.AppendLine($"{item.Key} - {item.Value}");
            }
            Util.WriteLog(sbb.ToString(), "readInLeaders");
            Debug.WriteLine("Wrote Fromatted to Files");
        }

        private static string GetLeaderFormat(Leader item)
        {
            StringBuilder sb = new ();
            sb.AppendLine("\t\tleader = {");
            sb.AppendLine(GlobalVars.featuresWindow.NameIsLeaderName.Checked
            ? $"\t\t\tname = \"{item.rulerName}\""
            : $"\t\t\tname = \"{item.leaderName}\"");
            sb.AppendLine($"\t\t\ttype = {item.leaderType}");
            sb.AppendLine(GlobalVars.featuresWindow.PipsFromSkill.Checked
                ? $"\t\t\tfire = {Util.DistributePipsRandomly(item.mil * 2)}{Environment.NewLine}" +
                  $"\t\t\tshock = {Util.DistributePipsRandomly(item.mil * 2)}{Environment.NewLine}" +
                  $"\t\t\tmaneuver = {Util.DistributePipsRandomly(item.mil * 2)}{Environment.NewLine}" +
                  $"\t\t\tsiege = {Util.DistributePipsRandomly(item.mil * 2)}"
                : $"\t\t\tfire = {item.fire}{Environment.NewLine}" +
                  $"\t\t\tshock = {item.shock}{Environment.NewLine}" +
                  $"\t\t\tmanuever = {item.maneuver}{Environment.NewLine}" +
                  $"\t\t\tsiege = {item.siege}");
            sb.AppendLine("\t\t}");
            return sb.ToString();
        }
        
        public static string GetTabs()
        {
            StringBuilder sb = new();
            for (var i = 0; i < tabIndex; i++)
                sb.Append("\t");
            return sb.ToString();
        }
    }
}
