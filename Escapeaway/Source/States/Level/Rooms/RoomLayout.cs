using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.States.Level.Rooms
{
    internal class RoomLayout
    {
        private List<RoomGround> currentRoomLayout = new List<RoomGround>();

        private List<StaticSprite>
            room1 = new List<StaticSprite>(),
            room2 = new List<StaticSprite>();

        public RoomGround ground;

        public int selectedRoomLayout = 0;

        private Random random = new Random();
        private int
            maxRooms = 2;

        public RoomLayout()
        {
            // Set Rooms
            room1.Add(new StaticSprite(null, new Rectangle(0, 160, Global.resWidth, 120), Color.Black));

            room2.Add(new StaticSprite(null, new Rectangle(0, 160, Global.resWidth / 2, 120), Color.Black));
            room2.Add(new StaticSprite(null, new Rectangle(Global.resWidth / 2 + 60, 160, Global.resWidth / 3, 120), Color.Black));

            currentRoomLayout.Add(new RoomGround(room1));
            currentRoomLayout.Add(new RoomGround(room2));
        }

        public void RandomizeRoom()
        {
            selectedRoomLayout = random.Next(0, maxRooms);
        }

        public void Update(GameTime gameTime)
        {
            // Set Ground
            ground = currentRoomLayout[selectedRoomLayout];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ground.Draw(spriteBatch);
        }
    }
}
