using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft;
using Newtonsoft.Json;
using Engine.Enum;
using Newtonsoft.Json.Linq;


namespace Engine.Utilities
{
	public class SpriteSheetData
	{
        private Dictionary< Directions, List<SpriteSheetCell>> _data;

		public SpriteSheetData(string JSONFile)
		{
			var jsonText = File.ReadAllText(JSONFile);
			var animationData = JsonConvert.DeserializeObject<JSONAnimationData>(jsonText);
            _data = animationData.GetConvertedData();
		}

        public SpriteSheetData(int textureWidth, int textureHeight)
        {
            _data = new Dictionary<Directions, List<SpriteSheetCell>>();
            var cells = new List<SpriteSheetCell>();
            var cell = new SpriteSheetCell(textureWidth, textureHeight);
            cells.Add(cell);
            _data[Directions.DIR_0] = cells;
        }

        public int FrameCount(Directions direction)
        {
            if (!_data.ContainsKey(direction)){return 0;}
            return _data[direction].Count;
            
        }

        public Vector2 GetAnchorPosition(int frame, Directions direction)
        {
            return GetCellVariable(frame, direction, "anchor");
        }

        public Vector2 GetSourcePosition(int frame, Directions direction)
        {
            return GetCellVariable(frame, direction, "sourcePos");
        }

        public Vector2 GetCurrentFrameSize(int frame, Directions direction)
        {
            return GetCellVariable(frame, direction, "frameSize");
        }

        private Vector2 GetCellVariable(int frame, Directions direction, string variable)
        {
            if (!_data.ContainsKey(direction)) { return new Vector2(0, 0); }
            var cells = _data[direction];
            if (variable == "anchor")
            {
                return cells[frame].Anchor;
            }
            if (variable == "sourcePos")
            {
                return cells[frame].SourcePosition;
            }
            if (variable == "frameSize")
            {
                return cells[frame].FrameSize;
            }
            return new Vector2(0, 0);
        }


        
    }

    internal class SpriteSheetCell
    {
        private List<int> _anchor;
        private List<int> _sourcePosition;
        private List<int> _frameSize;

        public SpriteSheetCell(List<int> anchor, List<int> sourcePosition, List<int> frameSize)
        {
            _anchor = anchor;
            _sourcePosition = sourcePosition;
            _frameSize = frameSize;
        }

        public SpriteSheetCell(int frameSizeX, int frameSizeY)
        {
            _anchor = new List<int>();
            _anchor.Add(0);
            _anchor.Add(0);
            _sourcePosition = new List<int>();
            _sourcePosition.Add(0);
            _sourcePosition.Add(0);
            _frameSize = new List<int>();
            _frameSize.Add(frameSizeX);
            _frameSize.Add(frameSizeY);
        }

        public Vector2 SourcePosition
        {
            get { return new Vector2(_sourcePosition[0], _sourcePosition[1]);}
        }

        public Vector2 FrameSize
        {
            get { return new Vector2(_frameSize[0], _frameSize[1]); }
        }

        public Vector2 Anchor
        {
            get { return new Vector2(_anchor[0], _anchor[1]); }
        }
    }

	internal class JSONAnimationData
	{
		public List<JSONCell> dir_0 { get; set;}
        public List<JSONCell> dir_1 { get; set; }
        public List<JSONCell> dir_2 { get; set; }
        public List<JSONCell> dir_3 { get; set; }
        public List<JSONCell> dir_4 { get; set; }
        public List<JSONCell> dir_5 { get; set; }
        public List<JSONCell> dir_6 { get; set; }
        public List<JSONCell> dir_7 { get; set; }
        public List<JSONCell> dir_8 { get; set; }
        public List<JSONCell> dir_9 { get; set; }
        public List<JSONCell> dir_10 { get; set; }
        public List<JSONCell> dir_11 { get; set; }
        public List<JSONCell> dir_12 { get; set; }
        public List<JSONCell> dir_13 { get; set; }
        public List<JSONCell> dir_14 { get; set; }
        public List<JSONCell> dir_15 { get; set; }
        public List<JSONCell> dir_16 { get; set; }
        public List<JSONCell> dir_17 { get; set; }
        public List<JSONCell> dir_18 { get; set; }
        public List<JSONCell> dir_19 { get; set; }
        public List<JSONCell> dir_20 { get; set; }
        public List<JSONCell> dir_21 { get; set; }
        public List<JSONCell> dir_22 { get; set; }
        public List<JSONCell> dir_23 { get; set; }
        public List<JSONCell> dir_24 { get; set; }
        public List<JSONCell> dir_25 { get; set; }
        public List<JSONCell> dir_26 { get; set; }
        public List<JSONCell> dir_27 { get; set; }
        public List<JSONCell> dir_28 { get; set; }
        public List<JSONCell> dir_29 { get; set; }
        public List<JSONCell> dir_30 { get; set; }
        public List<JSONCell> dir_31 { get; set; }

        public Dictionary<Directions, List<SpriteSheetCell>> GetConvertedData()
        {
            var data = new Dictionary<Directions, List<SpriteSheetCell>>();
            if (dir_0 != null) { data[Directions.DIR_0] = ConvertCells(dir_0); }
            if (dir_1 != null) { data[Directions.DIR_1] = ConvertCells(dir_1); }
            if (dir_2 != null) { data[Directions.DIR_2] = ConvertCells(dir_2); }
            if (dir_3 != null) { data[Directions.DIR_3] = ConvertCells(dir_3); }
            if (dir_4 != null) { data[Directions.DIR_4] = ConvertCells(dir_4); }
            if (dir_5 != null) { data[Directions.DIR_5] = ConvertCells(dir_5); }
            if (dir_6 != null) { data[Directions.DIR_6] = ConvertCells(dir_6); }
            if (dir_7 != null) { data[Directions.DIR_7] = ConvertCells(dir_7); }
            if (dir_8 != null) { data[Directions.DIR_8] = ConvertCells(dir_8); }
            if (dir_9 != null) { data[Directions.DIR_9] = ConvertCells(dir_9); }
            if (dir_10 != null) { data[Directions.DIR_10] = ConvertCells(dir_10); }
            if (dir_11 != null) { data[Directions.DIR_11] = ConvertCells(dir_11); }
            if (dir_12 != null) { data[Directions.DIR_12] = ConvertCells(dir_12); }
            if (dir_13 != null) { data[Directions.DIR_13] = ConvertCells(dir_13); }
            if (dir_14 != null) { data[Directions.DIR_14] = ConvertCells(dir_14); }
            if (dir_15 != null) { data[Directions.DIR_15] = ConvertCells(dir_15); }
            if (dir_16 != null) { data[Directions.DIR_16] = ConvertCells(dir_16); }
            if (dir_17 != null) { data[Directions.DIR_17] = ConvertCells(dir_17); }
            if (dir_18 != null) { data[Directions.DIR_18] = ConvertCells(dir_18); }
            if (dir_19 != null) { data[Directions.DIR_19] = ConvertCells(dir_19); }
            if (dir_20 != null) { data[Directions.DIR_20] = ConvertCells(dir_20); }
            if (dir_21 != null) { data[Directions.DIR_21] = ConvertCells(dir_21); }
            if (dir_22 != null) { data[Directions.DIR_22] = ConvertCells(dir_22); }
            if (dir_23 != null) { data[Directions.DIR_23] = ConvertCells(dir_23); }
            if (dir_24 != null) { data[Directions.DIR_24] = ConvertCells(dir_24); }
            if (dir_25  != null) { data[Directions.DIR_25] = ConvertCells(dir_25); }
            if (dir_26 != null) { data[Directions.DIR_26] = ConvertCells(dir_26); }
            if (dir_27 != null) { data[Directions.DIR_27] = ConvertCells(dir_27); }
            if (dir_28 != null) { data[Directions.DIR_28] = ConvertCells(dir_28); }
            if (dir_29 != null) { data[Directions.DIR_29] = ConvertCells(dir_29); }
            if (dir_30 != null) { data[Directions.DIR_30] = ConvertCells(dir_30); }
            if (dir_31 != null) { data[Directions.DIR_31] = ConvertCells(dir_31); }
            return data;
        }

        private List<SpriteSheetCell> ConvertCells(List<JSONCell> JSONCells)
        {
            var cells = new List<SpriteSheetCell>();
            foreach(JSONCell jsonCell in JSONCells)
            {
                var cell = new SpriteSheetCell(jsonCell.anchor, jsonCell.sourcePos, jsonCell.frameSize);
                cells.Add(cell);
            }
            return cells;
        }
    }

	internal class JSONCell
	{ 
        public List<int> anchor { get; set; }
        public List<int> sourcePos { get; set; }
		public List<int> frameSize { get; set; }
    }


	
}

