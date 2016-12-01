using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Data;
using Data.Data;
using Data.Data.Common;
using Data.Data.JobActions;
using Data.Data.Jobs;
using Data.Interfaces;

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
            var obj = new DataContainer("Test Colony",
                new Position((uint)mapPicture.Size.Width, (uint)mapPicture.Size.Height));
            var c = 'A';
            var homes = new[]
            {
                "CenterHome",
                "WorkPlaceHome",
                "WorkPlace"
            };
            obj.Add(new MultiModuleJob(homes[0], obj,
                new MovingModuleJob("test",
                    new MoveAction(new SingleTargetPositionAction(obj,
                        data => new Position((uint)(mapPicture.Size.Width / 2), (uint)(mapPicture.Size.Height / 2)))))));
            obj.Add(new MultiModuleJob(homes[1], obj,
                new MovingModuleJob("test",
                    new MoveAction(new MultiTargetPositionAction(obj,
                        data =>
                            data.Buildings.Where(building => building.Name == homes[2])
                                .Select(building => building.Position))))));

            var rand = new Random();

            for (var i = 0; i < 7; ++i)
                obj.Add(new Colonist(c++.ToString(),
                    new Position((uint)rand.Next(mapPicture.Size.Width - 10),
                        (uint)rand.Next(mapPicture.Size.Height - 10)),
                    obj.Jobs.OrderBy(jobable => rand.Next(2) == 0).First()));

            foreach (var i in homes)
                obj.Add(new Building(i,
                    new Position((uint)rand.Next(mapPicture.Size.Width - 10),
                        (uint)rand.Next(mapPicture.Size.Height - 10))));

            for (var i = 0; i < 4; i++)
                obj.Add(new Building(homes.OrderBy(s => rand.Next(2) == 0).First(),
                    new Position((uint)rand.Next(mapPicture.Size.Width - 50),
                        (uint)rand.Next(mapPicture.Size.Height - 50))));

            BuildingsList.DataSource = obj.Buildings.Select(building => building.ListPrint()).ToList();
            ColonistsList.DataSource = obj.Colonists;

            return obj;
        }

        private void ColonistsListRefresh()
        {
            if (ColonistsList.InvokeRequired)
                ColonistsList.Invoke(new Action(ColonistsListRefresh));
            else
                ColonistsList.Refresh();
        }

        private void ColonyControllerOnCollectionChanged_ToMap(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var pic = new Bitmap(mapPicture.Size.Width, mapPicture.Size.Height);
            using (var g = Graphics.FromImage(pic))
            {
                g.Clear(BackColor);
                var brush = new[]
                {
                    new SolidBrush(Color.Crimson),
                    new SolidBrush(Color.LightSeaGreen)
                };
                foreach (var i in ColonyController.PhysicalObjects)
                {
                    var point = new Point((int)i.Position.X, (int)i.Position.Y);
                    var tmpBrush = brush[i.ObjectType == ObjectType.Unit ? 0 : 1];
                    g.FillEllipse(tmpBrush, new Rectangle(point, new Size(-10, -10)));
                    g.DrawString(i.Name, DefaultFont, tmpBrush, point);
                }
                g.FillEllipse(new SolidBrush(Color.ForestGreen),
                    new Rectangle(new Point(mapPicture.Size.Width / 2, mapPicture.Size.Height / 2), new Size(-10, -10)));
                g.Dispose();
            }
            mapPicture.Image = pic;
        }

        private void ColonyControllerOnCollectionChanged_ToList(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
            => ColonistsListRefresh();

        private void startButton_Click(object sender, EventArgs e)
            => ColonyController.Start();
    }
}
