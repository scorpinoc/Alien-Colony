//using Controller;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Data.Data;
using Data.Data.Common;
using Data.Data.JobActions;
using Data.Data.Jobs;

namespace Controller.WindowsFormsTest
{
    public partial class TestForm : Form
    {
        private ColonyController ColonyController { get; }

        public TestForm()
        {
            InitializeComponent();

            ColonyController = new ColonyController(GenerateColony());
            ColonyController.CollectionChanged += ColonyControllerOnCollectionChanged_ToMap;
            ColonyController.CollectionChanged += ColonyControllerOnCollectionChanged_ToList;
        }

        private DataContainer GenerateColony()
        {
            var obj = new DataContainer("Test Colony", new Position((uint)mapPicture.Size.Width, (uint)mapPicture.Size.Height));
            var c = 'A';
            var job = new MultiModuleJob("Testing main job - {SingleTargetPositionAction to the center}", obj, new MovingModuleJob("test",
                new MoveAction(new SingleTargetPositionAction(obj, data => new Position((uint)(mapPicture.Size.Width / 2), (uint)(mapPicture.Size.Height / 2))))));
            obj.Add(job);
            var rand = new Random();
            for (var i = 0; i < 7; ++i)
                obj.Add(new Colonist((c++).ToString(), new Position((uint)rand.Next(0, mapPicture.Size.Width), (uint)rand.Next(0, mapPicture.Size.Height)), job));
            return obj;
        }

        private void ColonyControllerOnCollectionChanged_ToMap(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var pic = new Bitmap(mapPicture.Size.Width, mapPicture.Size.Height);
            using (var g = Graphics.FromImage(pic))
            {
                g.Clear(BackColor);
                var brush = new SolidBrush(Color.Crimson);

                foreach (var i in ColonyController.Colonists)
                {
                    var point = new Point((int)i.Position.X, (int)i.Position.Y);
                    g.FillEllipse(brush, new Rectangle(point, new Size(-10, -10)));
                    g.DrawString($"{i.Name} {{{i.CurrentDoing}}}", DefaultFont, brush, point);
                }
                g.FillEllipse(new SolidBrush(Color.ForestGreen), new Rectangle(new Point(mapPicture.Size.Width / 2, mapPicture.Size.Height / 2), new Size(-10, -10)));
                g.Dispose();
            }
            mapPicture.Image = pic;
        }

        private void ColonyControllerOnCollectionChanged_ToList(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var pic = new Bitmap(ColonistsList.Size.Width, ColonistsList.Size.Height);
            using (var g = Graphics.FromImage(pic))
            {
                g.Clear(BackColor);
                var brush = new SolidBrush(Color.Black);
                var line = 0;
                foreach (var i in ColonyController.Colonists.Select(colonist
                        => $"{colonist.Name} {colonist.Position.ToString().PadRight(15)} {colonist.Energy.ToString().PadLeft(4)} | {colonist.EnergyPersent:f1}%"))
                    g.DrawString(i, DefaultFont, brush, new Point(5, DefaultFont.Height * ++line));
                g.Dispose();
            }
            ColonistsList.Image = pic;
        }

        private void startButton_Click(object sender, EventArgs e)
            => ColonyController.Start();
    }
}
