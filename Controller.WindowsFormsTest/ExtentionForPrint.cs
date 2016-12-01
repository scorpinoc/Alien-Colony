using System.Globalization;
using Data.Data;
using Data.Interfaces;

namespace Controller.WindowsFormsTest
{
    public static class ExtentionForPrint
    {
        public static string ListPrint(this Colonist colonist)
            =>
                ((IPhysicalObject) colonist).ListPrint().PadRight(25) +
                $" {colonist.CurrentDoing.ToString().PadRight(20)} {colonist.JobName.PadRight(15)} {colonist.Energy:0##} | {colonist.EnergyPersent:F3}%";

        public static string ListPrint(this Building building)
            => ((IPhysicalObject)building).ListPrint();

        private static string ListPrint(this IPhysicalObject obj)
            => $"{obj.Name.PadRight(5)} {obj.Position.ToString().PadRight(15)}";
    }
}