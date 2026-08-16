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
        public Color screenColor = CustomColor.Red;

        private List<StaticSprite>
            firstRoom = new List<StaticSprite>(),
            room1 = new List<StaticSprite>(),
            room2 = new List<StaticSprite>(),
            room3 = new List<StaticSprite>(),
            room4 = new List<StaticSprite>(),
            room5 = new List<StaticSprite>(),
            room6 = new List<StaticSprite>(),
            room7 = new List<StaticSprite>(),
            room8 = new List<StaticSprite>(),
            room9 = new List<StaticSprite>(),
            room10 = new List<StaticSprite>(),
            room11 = new List<StaticSprite>(),
            room12 = new List<StaticSprite>(),
            room13 = new List<StaticSprite>(),
            room14 = new List<StaticSprite>(),
            room15 = new List<StaticSprite>(),
            room16 = new List<StaticSprite>(),
            room17 = new List<StaticSprite>(),
            room18 = new List<StaticSprite>(),
            room19 = new List<StaticSprite>(),
            room20 = new List<StaticSprite>(),
            lastRoom = new List<StaticSprite>();

        public RoomGround ground;

        public int selectedRoomLayout = 0;

        private Random random = new Random();
        private int
            maxRooms = 0;

        public RoomLayout()
        {
            // Set Rooms
            firstRoom.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth, 120), screenColor, true));

            room1.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth / 4, 120), screenColor, true));
            room1.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 - 20, 160, 40, 120), screenColor, true));
            room1.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 60, 160, 80, 120), screenColor, true));
            room1.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 42, 32, 20, 106), screenColor, true));

            room2.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, 50, 120), screenColor, true));
            room2.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 20, 160, 20, 120), screenColor, true));
            room2.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 - 35, 140, 35, 20), screenColor, true));
            room2.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 45, 140, 35, 20), screenColor, true));

            room3.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth - 60, 120), screenColor, true));
            room3.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 20, 160, 20, 120), screenColor, true));
            room3.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 4 - 10, 32, 20, 100), screenColor, true));

            room4.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, 60, 120), screenColor, true));
            room4.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 20, 160, 20, 120), screenColor, true));
            room4.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 - 25, 140, 30, 120), screenColor, true));
            room4.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 35, 120, 30, 120), screenColor, true));

            room5.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth / 2, 120), screenColor, true));
            room5.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 60, 160, 80, 120), screenColor, true));

            room6.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth / 2, 120), screenColor, true));
            room6.Add(new StaticSprite(Global.ground, new Rectangle(88, 131, 40, 30), screenColor, true));
            room6.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 60, 160, 80, 120), screenColor, true));
            room6.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 60, 131, 40, 30), screenColor, true));
            room6.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 48, 28, 20, 78), screenColor, true));

            room7.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth / 4, 120), screenColor, true));
            room7.Add(new StaticSprite(Global.ground, new Rectangle(120, 160, 40, 120), screenColor, true));
            room7.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 40, 160, 40, 120), screenColor, true));

            room8.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth / 2, 120), screenColor, true));
            room8.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth / 2 + 60, 160, 80, 120), screenColor, true));
            room8.Add(new StaticSprite(Global.ground, new Rectangle(Global.resWidth - 42, 32, 20, 98), screenColor, true));

            lastRoom.Add(new StaticSprite(Global.ground, new Rectangle(0, 160, Global.resWidth, 120), screenColor, true));

            // Add Rooms to List
            currentRoomLayout.Add(new RoomGround(firstRoom));
            currentRoomLayout.Add(new RoomGround(room1));
            currentRoomLayout.Add(new RoomGround(room2));
            currentRoomLayout.Add(new RoomGround(room3));
            currentRoomLayout.Add(new RoomGround(room4));
            currentRoomLayout.Add(new RoomGround(room5));
            currentRoomLayout.Add(new RoomGround(room6));
            currentRoomLayout.Add(new RoomGround(room7));
            currentRoomLayout.Add(new RoomGround(room8));
            currentRoomLayout.Add(new RoomGround(lastRoom));

            // Set Max Rooms
            maxRooms = (currentRoomLayout.Count() - 1);

            SetRoom();
        }

        public void GoToFirstRoom()
        {
            // Set Current Room to First Room
            selectedRoomLayout = 0;
            SetRoom();
        }

        public void GoToLastRoom()
        {
            // Set Current Room to Last Room
            selectedRoomLayout = currentRoomLayout.Count() - 1;
            SetRoom();
        }

        /// <summary>
        /// Set the layout of the room to a random layout.
        /// </summary>
        public void RandomizeRoom()
        {
            // Randomize next room
            int nextRoomLayout = random.Next(1, maxRooms);
            
            // Randomize again if next room is the same
            if (nextRoomLayout == selectedRoomLayout) RandomizeRoom();
            else selectedRoomLayout = nextRoomLayout;
        }

        private void SetRoom()
        {
            ground = currentRoomLayout[selectedRoomLayout];
        }

        private void UpdateColor(Color color)
        {
            // I know it's bad, I KNOW, this was temporary code for the game jam. PLEASE don't smite me

            foreach (var room in firstRoom) room.SetColor(color);
            foreach (var room in room1) room.SetColor(color);
            foreach (var room in room2) room.SetColor(color);
            foreach (var room in room3) room.SetColor(color);
            foreach (var room in room4) room.SetColor(color);
            foreach (var room in room5) room.SetColor(color);
            foreach (var room in room6) room.SetColor(color);
            foreach (var room in room7) room.SetColor(color);
            foreach (var room in room8) room.SetColor(color);
            foreach (var room in room9) room.SetColor(color);
            foreach (var room in room10) room.SetColor(color);
            foreach (var room in room11) room.SetColor(color);
            foreach (var room in room12) room.SetColor(color);
            foreach (var room in room13) room.SetColor(color);
            foreach (var room in room14) room.SetColor(color);
            foreach (var room in room15) room.SetColor(color);
            foreach (var room in room16) room.SetColor(color);
            foreach (var room in room17) room.SetColor(color);
            foreach (var room in room18) room.SetColor(color);
            foreach (var room in room19) room.SetColor(color);
            foreach (var room in room20) room.SetColor(color);
            foreach (var room in lastRoom) room.SetColor(color);
        }

        public void Update(GameTime gameTime, Color screenColor, Player player)
        {
            UpdateColor(screenColor);
            SetRoom();

            // If in Final Boss Room
            if (selectedRoomLayout == maxRooms)
            {
                if (player.centered)
                {
                    // Move Ground Texture to Simulate Room Movement
                    foreach (var sprite in lastRoom)
                    {
                        // Slower Move Speed
                        if (player.slowingDown) sprite.xOffset += 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        // Regular Move Speed
                        else sprite.xOffset += 120f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                }
                else
                {
                    foreach (var sprite in lastRoom) sprite.xOffset = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            ground.Draw(spriteBatch);
        }
    }
}
