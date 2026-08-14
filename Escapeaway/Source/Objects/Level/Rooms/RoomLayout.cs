using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Escapeaway.Source.Graphics;

namespace Escapeaway.Source.Objects.Level.Rooms
{
    internal class RoomLayout
    {
        private List<RoomGround> currentRoomLayout = new List<RoomGround>();

        private List<StaticSprite>
            room1 = new List<StaticSprite>(),
            room2 = new List<StaticSprite>(),
            room3 = new List<StaticSprite>();

        public RoomGround ground;

        public int selectedRoomLayout = 0;

        private Random random = new Random();
        private int
            maxRooms = 3;

        public RoomLayout()
        {
            // Set Rooms
            room1.Add(new StaticSprite(null, new Rectangle(0, 160, Global.resWidth, 120), CustomColor.Black));

            room2.Add(new StaticSprite(null, new Rectangle(0, 160, Global.resWidth / 2, 120), CustomColor.Black));
            room2.Add(new StaticSprite(null, new Rectangle(Global.resWidth / 2 + 60, 160, Global.resWidth / 3, 120), CustomColor.Black));

            room3.Add(new StaticSprite(null, new Rectangle(0, 160, Global.resWidth, 120), CustomColor.Black));
            room3.Add(new StaticSprite(null, new Rectangle(Global.resWidth / 2, 72, 20, 60), CustomColor.Black));

            // Add Rooms to List
            currentRoomLayout.Add(new RoomGround(room1));
            currentRoomLayout.Add(new RoomGround(room2));
            currentRoomLayout.Add(new RoomGround(room3));

            SetRoom();
        }

        /// <summary>
        /// Go back to the very first default room. Useful when restarting the game.
        /// </summary>
        public void GoToRoomOne()
        {
            // Set Current Room
            selectedRoomLayout = 0;
            SetRoom();
        }

        /// <summary>
        /// Set the layout of the room to a random layout.
        /// </summary>
        public void RandomizeRoom()
        {
            selectedRoomLayout = random.Next(0, maxRooms);
        }

        private void SetRoom()
        {
            // Set Ground
            ground = currentRoomLayout[selectedRoomLayout];
        }

        public void Update(GameTime gameTime)
        {
            SetRoom();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ground.Draw(spriteBatch);
        }
    }
}
